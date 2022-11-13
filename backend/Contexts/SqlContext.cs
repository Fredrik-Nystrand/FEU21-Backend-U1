
using backend.Models.Comment;
using backend.Models.Customer;
using backend.Models.Issue;
using backend.Models.Status;

namespace backend.Contexts
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
        }

        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<IssueEntity> Issues { get; set; }
        public DbSet<StatusEntity> Statuses { get; set; }


    }
}
