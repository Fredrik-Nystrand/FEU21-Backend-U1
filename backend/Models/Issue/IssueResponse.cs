using backend.Models.Comment;
using backend.Models.Customer;
using backend.Models.Status;
using Microsoft.AspNetCore.Http;

namespace backend.Models.Issue
{
    public class IssueResponse
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public StatusResponse Status { get; set; }
        public CustomerResponse Customer { get; set; }
        public IEnumerable<CommentResponse> Comments { get; set; }
    }
}
