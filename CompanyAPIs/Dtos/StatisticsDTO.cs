using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyAPIs.Dtos
{
    public class StatisticsDTO
    {
        

        public int ClientsCount { get; set; }
        public int EmployeeCount { get; set; }
        public int PaidOperationCount { get; set; }
        public int UnPaidOperationCount { get; set; }

    };


}
