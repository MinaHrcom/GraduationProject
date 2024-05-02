using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyAPIs.Dtos
{
    public class OperationDTO
    {
        public Guid? ID { get; set; }

        [Required(ErrorMessage = "{0}_IS_REQUIRED")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "{0}_IS_REQUIRED")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0}_IS_REQUIRED")]
        public string PortOfLoading { get; set; }

        [Required(ErrorMessage = "{0}_IS_REQUIRED")]
        public string PortOfDistance { get; set; }

        [Required(ErrorMessage = "{0}_IS_REQUIRED")]
        public int NumberOfCases { get; set; }

        [Required(ErrorMessage = "{0}_IS_REQUIRED")]

        public int NumberOfUnits { get; set; }

        [Required(ErrorMessage = "{0}_IS_REQUIRED")]

        public int TotalWeight { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

    }
}
