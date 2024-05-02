using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyAPIs.Dtos
{

    public class DocumentsDTO
    {
        [Required(ErrorMessage = "{0}_IS_REQUIRED")]

        public Guid ID { get; set; }
        [Required(ErrorMessage = "{0}_IS_REQUIRED")]

        public Guid OperationID { get; set; }
        [Required(ErrorMessage = "{0}_IS_REQUIRED")]

        public string Name { get; set; }
        [Required(ErrorMessage = "{0}_IS_REQUIRED")]

        public string VoyageNumber { get; set; }
        [Required(ErrorMessage = "{0}_IS_REQUIRED")]

        public string ContainerNumber { get; set; }
        [Required(ErrorMessage = "{0}_IS_REQUIRED")]

        public Guid ShipID { get; set; }

    }


}
