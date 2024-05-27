using CompanyAPIs.Dtos;
using HRCom.Domain.BaseTypes;
using Microsoft.AspNetCore.Identity;

namespace CompanyAPIs.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<OperationResult<List<Ships>>> GetShips();
        Task<OperationResult<List<UsersDTO>>> GetUsersInRole();


    }
}
