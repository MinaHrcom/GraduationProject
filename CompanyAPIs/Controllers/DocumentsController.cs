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
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentsService _documentService;

        public DocumentsController(IDocumentsService documentService)
        {
            _documentService = documentService;
        }

        //[Authorize(Roles = "admin")]
        [HttpPost("AddEdit_Document")]
        public async Task<IActionResult> Adddocument([FromBody] DocumentsDTO model)
        {

            var result = await _documentService.Adddocument(model);

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


        [HttpPost("document/{id}")]
        [ProducesResponseType(typeof(SuccessResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Deletedocument(Guid id)
        {
            var result = await _documentService.Deletedocument(id);

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


        [HttpGet("document/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(DataResponse<DocumentsDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetDocument(Guid id)
        {
            var result = await _documentService.GetDocument(id);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return Ok(new DataResponse<DocumentsDTO>
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
