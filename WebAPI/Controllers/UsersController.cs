using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Core.HttpModels;
using Core.Misc;
using Core.NewFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Repositories;
using Repositories.Models;
using Services.EmailSerivce;
using Services.JwtManager;
using System.Security.Cryptography;
using WebAPI.Helper.AuthorizationPolicy;

namespace WebAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    [JwtTokenAuthorization]
    public class UsersController : ControllerBase
    {
        private readonly DentalClinicPlatformContext _context;
        private readonly IConfiguration _config;
        private readonly UnitOfWork _unitOfWork;

        public UsersController(IConfiguration config, DentalClinicPlatformContext context)
        {
            _context = context;
            _config = config;
            _unitOfWork = new UnitOfWork(context);
        }

        [HttpPost]
        [Route("request-reset")]
        [AllowAnonymous]
        public ActionResult RequestResetPassword([FromBody] PasswordResetModel target)
        {
            User? user = _unitOfWork.UserRepository.GetAll(filter: (ex) => ex.Email == target.Email, orderBy: null, pageSize:1, pageIndex: 1).FirstOrDefault();

            if (user == null)
            {
                return Ok(new HttpErrorResponse() {statusCode = 400, message="Email has not been used for account creation." });
            }

            string UserFullname = user.Fullname != null ? user.Fullname : $"người dùng {user.UserId}";

            var emailService = _context.GetService<IEmailService>();

            string newPassword = CreatePassword(24);

            user.Password = newPassword;

            _unitOfWork.UserRepository.Update(user); 
            
            string subject = $"Khôi phục mật khẩu cho tài khoản {user.Username}";

            string body = $"Xin chào, <b>{UserFullname}</b>!<br/>" +
                $"Chúng tôi đã nhận được yêu cầu thay đổi mật khẩu tài khoản của bạn, hãy sử dụng mật khẩu <b>{newPassword}</b> cho lần đăng nhập kế tiếp và thay đổi mật khẩu của bạn.<br/>";


            var configuration = emailService.CreateConfiguration(_config.GetValue<string>("EmailService:Email")!, _config.GetValue<string>("EmailService:Password")!, target.Email, subject, body);
              
            emailService.SendMailGoogleSmtp(configuration);

            return Ok(new HttpErrorResponse() { statusCode = 202, message = "Accepted" });
        }

        [HttpPost("reset-password")]
        [JwtTokenAuthorization]
        public ActionResult ResetPassword([FromBody] PasswordResetModel target)
        {
            User? user = _unitOfWork.UserRepository.GetAll(filter: (ex) => ex.Email == target.Email, orderBy: null, pageSize: 1, pageIndex: 1).FirstOrDefault();

            if (user == null)
            {
                return Ok(new HttpErrorResponse() { statusCode = 400, message = "Email not used for account registration" });
            }

            string UserFullname = user.Fullname != null ? user.Fullname : "Người dùng";

            var emailService = _context.GetService<IEmailService>();

            var configuration = new EmailServiceModel()
            {
                account = _config.GetValue<string>("EmailService:Email")!,
                password = _config.GetValue<string>("EmailService:Password")!,
                target = target.Email,
                subject = $"Thay đổi mật khẩu cho tài khoản ${user.Username}",
                body = $"$Xin chào, ${UserFullname}!\n" +
                $"Mật khẩu của bạn đã được thay đổi. nếu bạn không thực hiện thay đổi này, vui lòng bấm vào mục \"quên mật khẩu\" đề thực hiện thay đổi lại mật khẩu.",
            };

            emailService.SendMailGoogleSmtp(configuration);

            return Ok(new HttpErrorResponse() { statusCode = 202, message = "Accepted" });
        }

        [HttpGet("/info")]
        [JwtTokenAuthorization]
        public async Task<ActionResult<User>> GetUser()
        {
            var token = Request.Headers.Authorization.ToString().Split(' ').Last();

            var service = _context.GetService<IJwtTokenService>();

            var claims = service.GetPrincipalsFromToken(token);

            var user = await _context.Users.FindAsync(claims.Claims.First(claim => claim.Type == "id").Value);
            
            if (user == null)
            {
                return Ok(new HttpErrorResponse() {statusCode=404, message="User not found!" });
            }

            UserInfoModel userInfo = new UserInfoModel()
            {
                Id = user.UserId,
                Email = user.Email,
                Fullname = user.Fullname,
                JoinedDate = (DateTime) user.CreationDate!,
                Phone = user.Phone,
                ProfilePicture = user.ProfilePic,
                Role = claims.Claims.First(claim => claim.Type == "role").Value
            };

            return user;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public IActionResult RegisterCustomer([FromBody] UserRegistrationModel requestObject)
        {
            
            // Check for user availability before register them in the database.
            if (_unitOfWork.CheckAvailability(requestObject.Username, requestObject.Email, out var responseMessage))
            {

                var error = new HttpErrorResponse()
                {
                    statusCode = 400,
                    message = responseMessage,
                };

               return Ok(error);
            }

            try
            {
                User newUser = new User()
                {
                    Username = requestObject.Username,
                    Password = requestObject.Password,
                    Email = requestObject.Email,
                    Role = 4,
                    Status = 3
                };
                _unitOfWork.UserRepository.Add(newUser);
                _unitOfWork.Save();

                return new JsonResult(new { message = "User creation succeed!", time = DateTime.UtcNow })
                {
                    StatusCode = (int)HttpStatusCode.Created,
                    ContentType = "application/json",
                };
            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = "User creation failed!", time = DateTime.UtcNow, error = ex.Message })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    ContentType = "application/json",
                };


            }
        }


        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [JwtTokenAuthorization]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }



        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            var rng = new Random();
            while (0 < length--)
            {
                res.Append(valid[rng.Next(valid.Length)]);
            }
            
            return res.ToString();
        }
    }
}
