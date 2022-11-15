using backend.Models.Comment;
using backend.Models.Issue;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace backend.Models.Customer
{
    public class CustomerEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        public string PhoneNumber { get; set; } = null;
        [JsonIgnore]
        public ICollection<IssueEntity> Issues { get; set; }
        [JsonIgnore]
        public ICollection<CommentEntity> Comments { get; set; }
    }
}
