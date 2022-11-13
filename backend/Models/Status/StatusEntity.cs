using System.ComponentModel.DataAnnotations;

namespace backend.Models.Status
{
    public class StatusEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
