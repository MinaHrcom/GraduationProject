using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CompanyAPIs.Models;
using CompanyAPIs.Dtos;
using BCrypt.Net;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using CompanyAPIs.Services;
using Microsoft.AspNetCore.Authorization;
using HRCom.Domain.BaseTypes;
using System.Net;
using System.Collections.Generic;

namespace CompanyAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly IOperationsService _operationService;

        public OperationsController(IOperationsService operationService)
        {
            _operationService = operationService;
        }

        //[Authorize(Roles = "admin")]
        [HttpPost("AddEdit_Operation")]
        public async Task<IActionResult> AddOperation([FromBody] OperationDTO model)
        {

            var result = await _operationService.AddOperation(model);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return Ok(new DataResponse<string>
                {
                    Data = result.Data
                });
            }
            else
            {
                return BadRequest(new ErrorResponse
                {
                    Error = new Error
                    {
                        Message = result.ErrorMessageKey
                    }
                });
            }
        }


        [HttpPost("DeleteOperation/{id}")]
        [ProducesResponseType(typeof(SuccessResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteOperation(Guid id)
        {
            var result = await _operationService.DeleteOperation(id);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return Ok(new SuccessResponse
                {
                    Success = true
                });
            }
            else if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(new ErrorResponse
                {
                    Error = new Error
                    {
                        Message = result.ErrorMessageKey
                    }
                });
            }
            else
            {
                return BadRequest(new ErrorResponse
                {
                    Error = new Error
                    {
                        Message = result.ErrorMessageKey
                    }
                });
            }
        }


        [HttpGet("Operation/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(DataResponse<OperationDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetOperation(Guid id)
        {
            var result = await _operationService.GetOperation(id);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return Ok(new DataResponse<OperationDTO>
                {
                    Data = result.Data
                });
            }
            else if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(new ErrorResponse
                {
                    Error = new Error
                    {
                        Message = result.ErrorMessageKey
                    }
                });
            }
            else
            {
                return BadRequest(new ErrorResponse
                {
                    Error = new Error
                    {
                        Message = result.ErrorMessageKey
                    }
                });
            }
        }





        [HttpGet("UserOperation/{UserId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(DataResponse<List<OperationDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetUserOperations(Guid UserId)
        {
            var result = await _operationService.GetUserOperations(UserId);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return Ok(new DataResponse<List<OperationDTO>>
                {
                    Data = result.Data
                });
            }
            else if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(new ErrorResponse
                {
                    Error = new Error
                    {
                        Message = result.ErrorMessageKey
                    }
                });
            }
            else
            {
                return BadRequest(new ErrorResponse
                {
                    Error = new Error
                    {
                        Message = result.ErrorMessageKey
                    }
                });
            }
        }



        [HttpGet("monthly-profits")]
        public async Task<IActionResult> GetMonthlyProfits([FromQuery] int year)
        {
            if (year <= 0)
            {
                return BadRequest("Invalid year specified.");
            }

            var monthlyProfits = await _operationService.GetMonthlyProfitsByYearAsync(year);
            return Ok(monthlyProfits);
        }

        [HttpGet("Statistics")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(DataResponse<StatisticsDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetStatistics()
        {
            var result = await _operationService.GetStatistics();

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return Ok(new DataResponse<StatisticsDTO>
                {
                    Data = result.Data
                });
            }
            else if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(new ErrorResponse
                {
                    Error = new Error
                    {
                        Message = result.ErrorMessageKey
                    }
                });
            }
            else
            {
                return BadRequest(new ErrorResponse
                {
                    Error = new Error
                    {
                        Message = result.ErrorMessageKey
                    }
                });
            }
        }

    }
}
