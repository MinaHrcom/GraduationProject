using CompanyAPIs.Dtos;
using HRCom.Domain.BaseTypes;

namespace CompanyAPIs.Services
{
    public interface IOperationsService
    {
         Task<OperationResult<string>> AddOperation(OperationDTO model);

        Task<OperationResult<bool>> DeleteOperation(Guid id);
        Task<OperationResult<OperationDTO>> GetOperation(Guid id);

    }
}
