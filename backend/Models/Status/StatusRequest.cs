using System.ComponentModel.DataAnnotations;

namespace backend.Models.Status
{
    public class StatusRequest
    {
        [Required]
        public string Status { get; set; }
    }
}
