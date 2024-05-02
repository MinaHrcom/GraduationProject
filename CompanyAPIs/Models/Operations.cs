using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyAPIs.Models
{
    public class Operations
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public string Name { get; set; }
        public string PortOfLoading { get; set; }
        public string PortOfDistance { get; set; }
        public int NumberOfCases { get; set; }
        public int NumberOfUnits { get; set; }
        public int TotalWeight { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

    }
}
