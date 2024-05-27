using CompanyAPIs.Data;
using CompanyAPIs.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using CompanyAPIs.Dtos;
using HRCom.Domain.BaseTypes;
using HRCom.Domain.Localization;
using System.Net;
using CompanyAPIs.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyAPIs.Services
{
    public class AuthService : IAuthService
    {


        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        private readonly ApplicationDbContext _applicationDbContext;

        //Constructor to Use it to check on the User found or no 
        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _roleManager = roleManager;
            _applicationDbContext = applicationDbContext;

        }

        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "Email ia already registered!" };

            if (await _userManager.FindByNameAsync(model.UserName) is not null)
                return new AuthModel { Message = "UserName ia already registered!" };


            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };


            var result = await _userManager.CreateAsync(user , model.Password );
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }

                return new AuthModel { Message = errors };

            }
            if (model.IsEmployee)
            {
                var roleName = "EMPLOYEE";
                var roleExists = await _roleManager.RoleExistsAsync(roleName);

                if (!roleExists)
                {
                    var role = new IdentityRole(roleName);
                    await _roleManager.CreateAsync(role);
                }
                await _userManager.AddToRoleAsync(user, "employee");



            }
            else
            {
                await _userManager.AddToRoleAsync(user, "user");

            }


            var jwtSecurityToken = await CreateJwtToken(user);


            var rolesList = await _userManager.GetRolesAsync(user);

            return new AuthModel
            {
                UserID = user.Id,
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = rolesList.FirstOrDefault(),
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = user.UserName
            };
        }

        //private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        //{
        //    var userClaims = await _userManager.GetClaimsAsync(user);
        //    var roles = await _userManager.GetRolesAsync(user);
        //    var roleClaims = new List<Claim>();

        //    foreach (var role in roles)
        //        roleClaims.Add(new Claim("roles", role));

        //    var claims = new[]
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        //        new Claim("uid", user.Id)
        //    }
        //    .Union(userClaims)
        //    .Union(roleClaims);

        //    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        //    var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        //    var jwtSecurityToken = new JwtSecurityToken(
        //        issuer: _jwt.Issuer,
        //        audience: _jwt.Audience,
        //        claims: claims,
        //        expires: DateTime.Now.AddDays(_jwt.DurationInDays),
        //        signingCredentials: signingCredentials);

        //    return jwtSecurityToken;
        //}



        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Id),
                new Claim(JwtRegisteredClaimNames.Sid, user.Id),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.UserName = user.UserName;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Roles = rolesList.FirstOrDefault();
            authModel.UserID = user.Id;

            return authModel;
        }
     
        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
                return "Invalid user ID or Role";

            if (await _userManager.IsInRoleAsync(user, model.Role))
                return "User already assigned to this role";

            var result = await _userManager.AddToRoleAsync(user, model.Role);

            return result.Succeeded ? string.Empty : "Something went wrong";
        }



        public async Task<OperationResult<List<Ships>>> GetShips()
        {
            var Ships = await _applicationDbContext.Ship.ToListAsync();

            if (Ships == null || !Ships.Any())
            {
                return new OperationResult<List<Ships>>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessageKey = LocalizationKeys.DataNotFound
                };
            }

            var ShipsDTOs = new List<Ships>();

            foreach (var Ship in Ships)
            {


                var ShipsDTO = new Ships
                {
                    ID = Ship.ID,
                    Name = Ship.Name,
                   
                };

                ShipsDTOs.Add(ShipsDTO);
            }

            return new OperationResult<List<Ships>>
            {
                Data = ShipsDTOs,
                StatusCode = HttpStatusCode.OK
            };
        }



        public async Task<OperationResult<List<UsersDTO>>> GetUsersInRole()
        {
            var usersInRole = new List<UsersDTO>();

            var role = await _roleManager.FindByNameAsync("user");
            if (role == null)
            {
                return usersInRole;
            }

            var userRoles = await _userManager.GetUsersInRoleAsync("user");
            if (userRoles == null)
            {
                return new OperationResult<List<UsersDTO>>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessageKey = LocalizationKeys.DataNotFound
                };
            }
            var Users = userRoles.ToList();
            var UsersDTO = new List<UsersDTO>();
            foreach (var userRole in userRoles)
            {

                var User = new UsersDTO
                {
                    Id = userRole.Id,
                    Name = userRole.UserName,

                };

                UsersDTO.Add(User);
            }

            return new OperationResult<List<UsersDTO>>
            {
                Data = UsersDTO,
                StatusCode = HttpStatusCode.OK
            };

        }


    }
}
