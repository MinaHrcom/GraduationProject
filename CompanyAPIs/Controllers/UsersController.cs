using Azure.Core;
using CompanyAPIs.Services.UsersService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersServices _usersService;
        public UsersController(IUsersServices UsersService)
        {
            _usersService = UsersService;
        }


        [HttpGet]

        public async Task<ActionResult<List<Users>>> GetAllUsers()
        {

            return _usersService.GetAllUsers();
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<Users>> GetSingleUsers(int id)
        {
            var result = _usersService.GetSingleUsers(id);
            if (result is null)
                return NotFound("User isn't found");

            return Ok(result);
        }

        [HttpPost]

        public async Task<ActionResult<List<Users>>> AddUser(Users user)
        {
            var result = _usersService.AddUser(user);

            return Ok(result);
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<List<Users>>> UpdateUser(int id , Users request)
        {
            var result = _usersService.UpdateUser(id, request);
            if (result is null)
                return NotFound("User ism't found");

            return Ok(result);
        }


        [HttpDelete("{id}")]

        public async Task<ActionResult<List<Users>>> DeleteUser(int id)
        {
            var result = _usersService.DeleteUser(id);
            if (result is null)
                return NotFound("User ism't found");

            return Ok(result);
        }

    }
}
