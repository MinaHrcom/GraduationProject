
using HRCom.Domain.Contracts.Interfaces.Services;

using System;
using System.Linq;
using System.Security.Claims;

namespace HRCom.Utilities.Services;

public class UserDataProvider : IUserDataProvider
{
    private readonly IHttpContextAccessor _accessor;
    private readonly IConfiguration _configuration;

    public UserDataProvider(
        IHttpContextAccessor accessor,
        IConfiguration configuration)
    {
        _accessor = accessor;
        _configuration = configuration;
    }


    public Guid? GetUserId()
    {
        Guid? userId = null;

        if (_accessor.HttpContext != null)
        {
            var user = _accessor.HttpContext.User;

            if (user != null)
            {
                var claim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (claim != null)
                {
                    userId = Guid.Parse(claim.Value);
                }
            }
        }

        return userId;
    }



    //public UserTypes? GetUserType()
    //{
    //    var claim = _accessor?.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == Constants.UserTypeClaim);

    //    if (claim is not null && int.TryParse(claim.Value, out var userType))
    //        return (UserTypes)userType;

    //    return default;
    //}



}
