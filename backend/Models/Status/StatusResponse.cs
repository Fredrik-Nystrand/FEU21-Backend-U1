using System.ComponentModel.DataAnnotations;

namespace backend.Models.Status
{
    public class StatusResponse
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
