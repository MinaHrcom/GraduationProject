using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyAPIs.Models
{
    public class Ships
    {
        
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public Guid ID { get; set; }

            [Required]
            
            public string Name { get; set; }
        
    }
}
