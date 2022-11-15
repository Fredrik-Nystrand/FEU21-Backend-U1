using System.ComponentModel.DataAnnotations;

namespace backend.Models.Comment
{
    public class CommentRequest
    {
        [Required]
        public string Comment { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int IssueId { get; set; }
    }
}
