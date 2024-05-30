using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Models;
using System.Text.Json.Nodes;
using WebAPI.Models;
using WebAPI.Services.JwtManager;
using WebAPI.Helper.AuthorizationPolicy;

namespace WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly DentalClinicPlatformContext _context;
        private readonly IConfiguration _config;
        private readonly UnitOfWork unitOfWork;

        public AuthenticationController(IConfiguration configuration, DentalClinicPlatformContext context)
        {
            _context = context;
            _config = configuration;
            unitOfWork = new UnitOfWork(_context);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult LogUserIn([FromBody] AuthModel requestObject)
        {
            JsonObject responseJson = new JsonObject();

            User? user = unitOfWork.Authenticate(requestObject.username, requestObject.password);

            if (user != null)
            {
                try
                {
                    var token = HttpContext.RequestServices.GetService<IJwtTokenManager>()?.GenerateAccessToken(user);
                    responseJson.Add("jwt", token ?? throw new Exception(""));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return this.Problem(statusCode: 500, title: "Can't generate new token!", detail:ex.Message, instance: ex.Source);
                }

                return Ok(responseJson);
            }
            else
            {
                responseJson.Add("error", "Invalid username or password.");
                return Unauthorized(responseJson);
            }
        }


        [HttpPost]
        [Route("register/user")]
        [AllowAnonymous]
        public IActionResult RegisterCustomer([FromBody] RegisterModel requestObject)
        {
            User newUser = new User()
            {
                Username = requestObject.username,
                Password = requestObject.password,
                Email = requestObject.email,
                Role = 4,
                Status = 3
            };

            JsonObject returnObject = new JsonObject();

            try
            {
                unitOfWork.UserRepository.Add(newUser);
                unitOfWork.Save();

                returnObject.Add("message", "user added to database");

                return Ok(returnObject);
            }
            catch (Exception)
            {
                returnObject.Add("message", "something wrong happened while we try to add your account");
                return BadRequest(returnObject);
            }
        }

        [HttpPost]
        [Route("register/clinic")]
        [AllowAnonymous]
        public IActionResult RegisterClinc([FromBody] RegisterModel requestObject)
        {
            User newUser = new User()
            {
                Username = requestObject.username,
                Password = requestObject.password,
                Email = requestObject.email,
                Role = 2,
                Status = 3
            };

            JsonObject returnObject = new JsonObject();

            try
            {
                unitOfWork.UserRepository.Add(newUser);
                unitOfWork.Save();

                returnObject.Add("message", "user added to database");

                return Ok(returnObject);
            }
            catch (Exception)
            {
                returnObject.Add("message", "something wrong happened while we try to add your account to the system.");
                return BadRequest(returnObject);
            }
        }

        [HttpPost]
        [Route("register/doctor")]
        [Authorize]
        public IActionResult RegisterDoctor([FromBody] RegisterModel requestObject)
        {
            User newUser = new User()
            {
                Username = requestObject.username,
                Password = requestObject.password,
                Email = requestObject.email,
                Role = 3,
                Status = 3
            };

            JsonObject returnObject = new JsonObject();

            try
            {
                unitOfWork.UserRepository.Add(newUser);
                unitOfWork.Save();

                returnObject.Add("message", "user added to database");

                return Ok(returnObject);
            }
            catch (Exception)
            {
                returnObject.Add("message", "something wrong happened while we try to add your account to the system.");
                return BadRequest(returnObject);
            }
        }


        [HttpGet]
        [Route("check-login")]
        [JwtTokenAuthorization]
        public ActionResult<IEnumerable<string>> CheckLogin()
        {
            JsonResult response = new JsonResult(new {message = "Authorized", time = DateTime.UtcNow})
            {
                StatusCode = 200,
                ContentType = "application/json",
            };

            return response;
        }


    }
}
