﻿using Core.Exception;
using Core.HttpModels;
using Core.NewFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Models;
using System.Net;

namespace WebAPI.Controllers
{
    [Route("api/clinic")]
    [ApiController]
    [AllowAnonymous]
    public class ClinicsController : ControllerBase
    {
        private readonly DentalClinicPlatformContext _DbContext;
        private readonly IConfiguration _configuration;
        private readonly UnitOfWork _unitOfWork;

        public ClinicsController(IConfiguration config, DentalClinicPlatformContext context)
        {
            _configuration = config;
            _DbContext = context;
            _unitOfWork = new UnitOfWork(_DbContext);
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterClinic([FromBody] ClinicRegistrationModel requestObject)
        {
            if (!_unitOfWork.CheckClinicAvailability(requestObject.Name, out var responseMessage))
            {
                return BadRequest(new HttpErrorResponse()
                {
                    statusCode = 400,
                    message = "Phòng khám với tên này đã tồn tại",
                    errorDetail = responseMessage
                });
            }
            try
            {
                Clinic newClinic = new Clinic()
                {
                    Name = requestObject.Name,
                    Address = requestObject.Address,
                    Phone = requestObject.Phone,
                    Email = requestObject.Email,
                    OpenHour = TimeOnly.Parse(requestObject.OpenHour),
                    CloseHour = TimeOnly.Parse(requestObject.CloseHour),
                    Status = true,
                    Owner = _unitOfWork.UserRepository.GetById(3)

                };

                // Add clinic services
                foreach (var serviceId in requestObject.ClinicServices)
                {
                    var service = await _DbContext.Services.FindAsync(serviceId);
                    if (service != null)
                    {
                        newClinic.ClinicServices.Add(new ClinicService
                        {
                            ServiceId = service.ServiceId,
                            Clinic = newClinic,
                            Service = service
                        });
                    }
                    else
                    {
                        return BadRequest(new HttpErrorResponse()
                        {
                            statusCode = 400,
                            message = $"Service with ID {serviceId} does not exist"
                        });
                    }
                }

                _unitOfWork.ClinicRepository.Add(newClinic);
                _unitOfWork.Save();

                return Ok(new HttpValidResponse()
                {
                    statusCode = 202,
                    message = "Yêu cầu tạo mới phòng khám đang được xử lí trong hệ thống"
                });
            }
            catch (DbUpdateException dbEx)
            {
                // Log the exception (you can use a logging library like Serilog or NLog here)
                var innerExceptionMessage = dbEx.InnerException?.Message ?? dbEx.Message;
                return new JsonResult(new HttpErrorResponse()
                {
                    statusCode = 500,
                    message = "Lỗi hệ thống trong lúc xử lí yêu cầu",
                    errorDetail = innerExceptionMessage
                })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    ContentType = "application/json"
                };
            }
            catch (Exception e)
            {
                return new JsonResult(new HttpErrorResponse()
                {
                    statusCode = 500,
                    message = "Lỗi hệ thống trong lúc xử lí yêu cầu",
                    errorDetail = e.Message
                })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    ContentType = "application/json"
                };
            }
        }
    }
}
