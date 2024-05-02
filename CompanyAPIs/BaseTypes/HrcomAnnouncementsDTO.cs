using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HRCom.Domain.BaseTypes
{
    public class HrcomAnnouncementsDTO
    {
        public int Id { get; set; }

        [JsonPropertyName("body")]
        [Required(ErrorMessage = "{0}_IS_REQUIRED")]
        [Display(Name = "BODY")]
        public string Body { get; set; }

        [JsonPropertyName("announce_date")]
        [Required(ErrorMessage = "{0}_IS_REQUIRED")]
        [Display(Name = "ANNOUNCEDATE")]
        public DateTime AnnouncementDate { get; set; }

        [JsonPropertyName("title")]
        [Required(ErrorMessage = "{0}_IS_REQUIRED")]
        [Display(Name = "TITLE")]
        public string Title { get; set; }
    }
}
