using backend.Models.Issue;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.Models.Status
{
    public class StatusEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Status { get; set; }

        [JsonIgnore]
        public ICollection<IssueEntity> Issues { get; set; }
    }
}
