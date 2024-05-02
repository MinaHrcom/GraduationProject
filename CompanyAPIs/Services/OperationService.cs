using CompanyAPIs.Data;
using CompanyAPIs.Dtos;
using CompanyAPIs.Helpers;
using HRCom.Domain.BaseTypes;
using HRCom.Domain.Contracts.Interfaces.Services;
using HRCom.Domain.Localization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace CompanyAPIs.Services
{
    public class OperationService : IOperationsService
    {


        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IUserDataProvider _userDataProvider;

        //Constructor to Use it to check on the User found or no 
        public OperationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt, ApplicationDbContext applicationDbContext , IUserDataProvider userDataProvider)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _roleManager = roleManager;
            _applicationDbContext = applicationDbContext;
            _userDataProvider = userDataProvider;

        }

        public async Task<OperationResult<string>> AddOperation(OperationDTO model)
        {
            var result = new OperationResult<string>();

            var OperationsRequest = new Operations();

            if (model.ID != null)
            {

                var OperationDb = await _applicationDbContext.Operation.Where(x => x.ID == model.ID).FirstOrDefaultAsync();



                OperationDb.Name = model.Name;
                OperationDb.NumberOfCases = model.NumberOfCases;
                OperationDb.PortOfDistance = model.PortOfDistance;
                OperationDb.PortOfLoading = model.PortOfLoading;
                OperationDb.NumberOfUnits = model.NumberOfUnits;
                OperationDb.TotalWeight = model.TotalWeight;
                OperationDb.UserId = model.UserId;
                OperationDb.UpdatedAt = DateTime.Now;
                OperationDb.IsDeleted = false;

                await _applicationDbContext.SaveChangesAsync();

                result.Data = model.ID.ToString();


            }
            else // Add Mode
            {
                OperationsRequest = new Operations
                {
                    Name = model.Name,
                    NumberOfCases = model.NumberOfCases,
                    PortOfLoading = model.PortOfLoading,
                    IsDeleted = model.IsDeleted,
                    PortOfDistance = model.PortOfDistance,
                    NumberOfUnits = model.NumberOfUnits,
                    TotalWeight = model.TotalWeight,
                    UserId = model.UserId,
                    CreatedAt = DateTime.Now,

                };



                _applicationDbContext.Operation.Add(OperationsRequest);
                await _applicationDbContext.SaveChangesAsync();

                result.Data = OperationsRequest.ID.ToString();
            }



          
            result.StatusCode = HttpStatusCode.OK;
            return result;
        }


        public async Task<OperationResult<bool>> DeleteOperation(Guid id)
        {
            OperationResult<bool> result = new OperationResult<bool>();

            var updatedRecord = await _applicationDbContext.Operation.Where(x => x.ID == id).FirstOrDefaultAsync();

            if (updatedRecord == null)
            {
                result.StatusCode = HttpStatusCode.NotFound;
                result.ErrorMessageKey = LocalizationKeys.DataNotFound;
                return result;
            }
            updatedRecord.IsDeleted = true;


            await _applicationDbContext.SaveChangesAsync();

            result.StatusCode = HttpStatusCode.OK;
            return result;
        }





        public async Task<OperationResult<OperationDTO>> GetOperation(Guid id)
        {
            var Operation = await _applicationDbContext.Operation.Where(x=> x.ID == id).FirstOrDefaultAsync();

            var User = _userDataProvider.GetUserId();

            if (Operation == null)
            {
                return new OperationResult<OperationDTO>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessageKey = LocalizationKeys.DataNotFound
                };
            }

            return new OperationResult<OperationDTO>
            {
                Data = new OperationDTO
                {
                        ID = Operation.ID,
                        Name = Operation.Name,  
                        NumberOfCases = Operation.NumberOfCases,
                        NumberOfUnits = Operation.NumberOfUnits,    
                        PortOfDistance = Operation.PortOfDistance,
                        PortOfLoading = Operation.PortOfLoading,
                        UserId = Operation.UserId,
                        CreatedBy = Operation.CreatedBy,
                        CreatedAt = Operation.CreatedAt,
                        TotalWeight = Operation.TotalWeight,
                        IsDeleted = Operation.IsDeleted

                },
                StatusCode = HttpStatusCode.OK
            };
        }




    }
}
