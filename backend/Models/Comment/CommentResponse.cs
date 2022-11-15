namespace backend.Models.Comment
{
    public class CommentResponse
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime Created { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}