using backend.Models.Customer;
using backend.Models.Issue;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace backend.Models.Comment
{
    public class CommentEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int IssueId { get; set; }
        [JsonIgnore]
        public IssueEntity Issue { get; set; }
        [JsonIgnore]
        public CustomerEntity Customer { get; set; }
    }
}
