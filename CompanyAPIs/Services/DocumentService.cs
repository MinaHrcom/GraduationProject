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
    public class DocumentService : IDocumentsService
    {


        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IUserDataProvider _userDataProvider;

        //Constructor to Use it to check on the User found or no 
        public DocumentService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt, ApplicationDbContext applicationDbContext , IUserDataProvider userDataProvider)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _roleManager = roleManager;
            _applicationDbContext = applicationDbContext;
            _userDataProvider = userDataProvider;

        }

        public async Task<OperationResult<string>> Adddocument(DocumentsDTO model)
        {
            var result = new OperationResult<string>();

            var DocumentRequest = new Models.Documents();

            if (model.ID != null)
            {

                var DocumentDb = await _applicationDbContext.Document.Where(x => x.ID == model.ID).FirstOrDefaultAsync();



                DocumentDb.Name = model.Name;
                DocumentDb.ShipID = model.ShipID;
                DocumentDb.VoyageNumber = model.VoyageNumber;
                DocumentDb.ContainerNumber = model.ContainerNumber;
                DocumentDb.OperationID = model.OperationID;



                await _applicationDbContext.SaveChangesAsync();

                result.Data = model.ID.ToString();


            }
            else // Add Mode
            {


                DocumentRequest = new Models.Documents
                {
                    Name = model.Name,
                    ShipID = model.ShipID,
                    VoyageNumber = model.VoyageNumber,
                    ContainerNumber = model.ContainerNumber,
                    OperationID = model.OperationID

                };



                _applicationDbContext.Document.Add(DocumentRequest);
                await _applicationDbContext.SaveChangesAsync();

                result.Data = DocumentRequest.ID.ToString();
            }



          
            result.StatusCode = HttpStatusCode.OK;
            return result;
        }     
        
        
        
        
        public async Task<OperationResult<string>> AddPayment(OperationPayment model)
        {

            var result = new OperationResult<string>();

            var PaymentRequest = new Models.OperationPayment();

            var operation = await _applicationDbContext.Operation.Where(x => x.ID == model.OperationID).FirstOrDefaultAsync();

            operation.IsPaid = true;

            PaymentRequest = new Models.OperationPayment
            {
                    OperationID = model.OperationID,
                    PaymentValue = model.PaymentValue,
                    CreatedAt = DateTime.Now,
            };

             _applicationDbContext.Operation_Payment.Add(PaymentRequest);
             await _applicationDbContext.SaveChangesAsync();

            result.Data = PaymentRequest.ID.ToString();

            result.StatusCode = HttpStatusCode.OK;
            return result;
        
        }



          
        


        public async Task<OperationResult<bool>> Deletedocument(Guid id)
        {
            OperationResult<bool> result = new OperationResult<bool>();

            var updatedRecord = await _applicationDbContext.Document.Where(x => x.ID == id).FirstOrDefaultAsync();

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





        public async Task<OperationResult<DocumentsDTO>> GetDocument(Guid id)
        {
            var Document = await _applicationDbContext.Document.Where(x=> x.ID == id && x.IsDeleted != true).FirstOrDefaultAsync();

            

            if (Document == null)
            {
                return new OperationResult<DocumentsDTO>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessageKey = LocalizationKeys.DataNotFound
                };
            }

            return new OperationResult<DocumentsDTO>
            {
                Data = new DocumentsDTO
                {
                        ID = Document.ID,
                        Name = Document.Name,  
                        ContainerNumber = Document.ContainerNumber,
                        OperationID = Document.OperationID,
                        ShipID = Document.ShipID,
                        VoyageNumber = Document.VoyageNumber,
                       IsDelete = Document.IsDeleted
                        


                },
                StatusCode = HttpStatusCode.OK
            };
        }


        public async Task<OperationResult<DocumentsDTO>> GetUserDocuments(Guid UserId)
        {
            OperationResult<DocumentsDTO> result =
                new OperationResult<DocumentsDTO>();


            var  userId = _userDataProvider.GetUserId().ToString();

            var OperationId = _applicationDbContext.Operation.Where(x=> x.UserId == userId).Select(x=> x.ID).FirstOrDefault();

            var Document = _applicationDbContext.Document.Where(x => x.OperationID == OperationId && x.IsDeleted != true).FirstOrDefault();


            var DocumentObj = new DocumentsDTO();

            DocumentObj.ID = Document.ID;
            DocumentObj.VoyageNumber = Document.VoyageNumber;
            DocumentObj.ContainerNumber = Document.ContainerNumber;
            DocumentObj.OperationID = Document.OperationID;
            DocumentObj.ShipID = Document.ShipID;
            DocumentObj.Name = Document.Name;
          DocumentObj.IsDelete = Document.IsDeleted;

            result = DocumentObj;
            result.StatusCode = HttpStatusCode.OK;
            return result;
        }


    }
}
