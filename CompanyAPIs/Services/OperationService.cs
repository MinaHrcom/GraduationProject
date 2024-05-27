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
using NuGet.Protocol.Core.Types;
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

            var UserId = _userDataProvider.GetUserId();

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
                OperationDb.IsDeleted = model.IsDeleted;
                OperationDb.IsPaid = model.IsPaid;
                OperationDb.EmployeeUserId = model.EmployeeId;
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
                    IsDeleted = false,
                    PortOfDistance = model.PortOfDistance,
                    NumberOfUnits = model.NumberOfUnits,
                    TotalWeight = model.TotalWeight,
                    UserId = model.UserId,
                    CreatedAt = DateTime.Now,
                    IsPaid = false,
                    EmployeeUserId = model.EmployeeId,

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
            var Operation = await _applicationDbContext.Operation
                                                    .Where(o => o.ID == id)
                                                    .FirstOrDefaultAsync();

            if (Operation == null)
            {
                return new OperationResult<OperationDTO>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessageKey = LocalizationKeys.DataNotFound
                };
            }

            var document = await _applicationDbContext.Document
                                                       .Where(d => d.OperationID == id)
                                                       .FirstOrDefaultAsync();





            var operationDTO = new OperationDTO
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
                IsDeleted = Operation.IsDeleted,
                IsPaid = Operation.IsPaid,
                EmployeeId = Operation.EmployeeUserId,
                Documents = document != null ? new Dtos.Documents
                {
                    ID = document.ID,
                    OperationID = document.OperationID,
                    Name = document.Name,
                    VoyageNumber = document.VoyageNumber,
                    ContainerNumber = document.ContainerNumber,
                    ShipID = document.ShipID,
                    IsDeleted = document.IsDeleted
                    
                } : null
            };

                 return new OperationResult<OperationDTO>
                 {
                     Data = operationDTO,
                     StatusCode = HttpStatusCode.OK
                 };
        }



        public async Task<OperationResult<List<OperationDTO>>> GetUserOperations(Guid UserId)
        {
            var operations = await _applicationDbContext.Operation
                                                 .Where(o => o.UserId == UserId.ToString() || o.EmployeeUserId == UserId.ToString())
                                                 .ToListAsync();

            if (operations == null || !operations.Any())
            {
                return new OperationResult<List<OperationDTO>>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = null
                };
            }

            var operationDTOs = new List<OperationDTO>();

            foreach (var operation in operations)
            {
                var document = await _applicationDbContext.Document
                                                             .Where(d => d.OperationID == operation.ID)
                                                             .FirstOrDefaultAsync();

                var operationDTO = new OperationDTO
                {
                    ID = operation.ID,
                    Name = operation.Name,
                    NumberOfCases = operation.NumberOfCases,
                    NumberOfUnits = operation.NumberOfUnits,
                    PortOfDistance = operation.PortOfDistance,
                    PortOfLoading = operation.PortOfLoading,
                    UserId = operation.UserId,
                    CreatedBy = operation.CreatedBy,
                    CreatedAt = operation.CreatedAt,
                    TotalWeight = operation.TotalWeight,
                    IsDeleted = operation.IsDeleted,
                    IsPaid = operation.IsPaid,
                    EmployeeId = operation.EmployeeUserId,
                    Documents = document != null ? new Dtos.Documents
                    {
                        ID = document.ID,
                        OperationID = document.OperationID,
                        Name = document.Name,
                        VoyageNumber = document.VoyageNumber,
                        ContainerNumber = document.ContainerNumber,
                        ShipID = document.ShipID,
                        IsDeleted = document.IsDeleted
                    } : null
                };

                operationDTOs.Add(operationDTO);
            }

            return new OperationResult<List<OperationDTO>>
            {
                Data = operationDTOs,
                StatusCode = HttpStatusCode.OK
            };

        }





    }
}
