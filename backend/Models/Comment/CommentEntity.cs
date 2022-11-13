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
    }
}
