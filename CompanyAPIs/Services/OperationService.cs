using CompanyAPIs.Data;
using CompanyAPIs.Dtos;
using CompanyAPIs.Helpers;
using HRCom.Domain.BaseTypes;
using HRCom.Domain.Contracts.Interfaces.Services;
using HRCom.Domain.Localization;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<MonthlyProfitDto>> GetMonthlyProfitsAsync()
        {

            var monthlyProfits = await _applicationDbContext.Operation_Payment
                .Where(op => !op.IsDeleted)
                .GroupBy(op => new { op.CreatedAt.Year, op.CreatedAt.Month })
                .Select(g => new MonthlyProfitDto
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalProfit = g.Sum(op => op.PaymentValue),
                    OperationCount = g.Count()  // New property to count the operations

                })
                .OrderBy(mp => mp.Year)
                .ThenBy(mp => mp.Month)
                .ToListAsync();

            return monthlyProfits;
        }



        public async Task<IEnumerable<MonthlyProfitDto>> GetMonthlyProfitsByYearAsync(int year)
        {
            // Generate a list of all months for the specified year
            var allMonths = GenerateAllMonths(year, year);

            // Get the monthly profits for the specified year from the database
            var monthlyProfits = await _applicationDbContext.Operation_Payment
                .Where(op => !op.IsDeleted && op.CreatedAt.Year == year)
                .GroupBy(op => new { op.CreatedAt.Year, op.CreatedAt.Month })
                .Select(g => new MonthlyProfitDto
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalProfit = g.Sum(op => op.PaymentValue),
                    OperationCount = g.Count()
                })
                .ToListAsync();

            // Perform a left join to include all months
            var result = from m in allMonths
                         join p in monthlyProfits
                         on new { m.Year, m.Month } equals new { p.Year, p.Month } into gj
                         from sub in gj.DefaultIfEmpty()
                         select new MonthlyProfitDto
                         {
                             Year = m.Year,
                             Month = m.Month,
                             TotalProfit = sub?.TotalProfit ?? 0,
                             OperationCount = sub?.OperationCount ?? 0
                         };

            return result.OrderBy(r => r.Year).ThenBy(r => r.Month).ToList();
        }

        private List<MonthlyProfitDto> GenerateAllMonths(int startYear, int endYear)
        {
            var allMonths = new List<MonthlyProfitDto>();
            for (int year = startYear; year <= endYear; year++)
            {
                for (int month = 1; month <= 12; month++)
                {
                    allMonths.Add(new MonthlyProfitDto { Year = year, Month = month });
                }
            }
            return allMonths;
        }




        public async Task<OperationResult<StatisticsDTO>> GetStatistics()
        {
            var EmployeeRoleId = await _applicationDbContext.Roles.Where(x=>x.Name == "EMPLOYEE").Select(x=>x.Id).FirstOrDefaultAsync();
            var CientRoleId = await _applicationDbContext.Roles.Where(x=>x.Name == "user").Select(x => x.Id).FirstOrDefaultAsync();

            var clientCount = await _applicationDbContext.UserRoles.CountAsync(u => u.RoleId == CientRoleId);
            var EmployeeCount = await _applicationDbContext.UserRoles.CountAsync(u => u.RoleId == EmployeeRoleId);

            var paidOperations = await _applicationDbContext.Operation.Where(x=> x.IsPaid == true).CountAsync();
            var UnpaidOperations = await _applicationDbContext.Operation.Where(x=> x.IsPaid == false).CountAsync();


            var StatisticsDTO = new StatisticsDTO
            {
                ClientsCount = clientCount,
                EmployeeCount  = EmployeeCount,
                UnPaidOperationCount = UnpaidOperations,
                PaidOperationCount = paidOperations,

            };
            
            return new OperationResult<StatisticsDTO>
            {
                Data = StatisticsDTO,
                StatusCode = HttpStatusCode.OK
            };

        }






    }
}
