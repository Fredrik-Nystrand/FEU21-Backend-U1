using backend.Models.Comment;
using backend.Models.Customer;
using backend.Models.Status;
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
        public StatusEntity Status { get; set; }
        public CustomerEntity Customer { get; set; }
        public IEnumerable<CommentEntity> Comments { get; set; }

    }
}
