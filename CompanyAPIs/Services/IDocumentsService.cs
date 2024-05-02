using CompanyAPIs.Dtos;
using HRCom.Domain.BaseTypes;

namespace CompanyAPIs.Services
{
    public interface IDocumentsService
    {
         Task<OperationResult<string>> Adddocument(DocumentsDTO model);

        Task<OperationResult<bool>> Deletedocument(Guid id);
        Task<OperationResult<DocumentsDTO>> GetDocument(Guid id);

    }
}
