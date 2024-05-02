using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyAPIs.Models
{
    public class Documents
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        [ForeignKey("Operation")]
        public Guid OperationID { get; set; }

        [Required]
        public string Name { get; set; }

        public string VoyageNumber { get; set; }

        public string ContainerNumber { get; set; }

        [ForeignKey("Ship")]
        public Guid ShipID { get; set; }

        public bool IsDeleted { get; set; } = false;

        public Guid? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Operations Operation { get; set; }

        public Ships Ship { get; set; }
    }
}
