using backend.Models.Comment;
using backend.Models.Customer;
using backend.Models.Status;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace backend.Models.Issue
{
    public class IssueEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime Modified { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public int CustomerId { get; set; }

        [JsonIgnore]
        public StatusEntity Status { get; set; }
        [JsonIgnore]
        public CustomerEntity Customer { get; set; }
        [JsonIgnore]
        public IEnumerable<CommentEntity> Comments { get; set; }

    }
}
