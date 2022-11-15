using System.ComponentModel.DataAnnotations;

namespace backend.Models.Issue
{
    public class IssueUpdateRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public int CustomerId { get; set; }
    }
}
