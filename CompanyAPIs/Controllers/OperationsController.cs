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


        [HttpPost("Operation/{id}")]
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


    }
}
