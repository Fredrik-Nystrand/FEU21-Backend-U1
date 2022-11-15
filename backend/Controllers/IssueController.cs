using backend.Contexts;
using backend.Models.Comment;
using backend.Models.Customer;
using backend.Models.Issue;
using backend.Models.Status;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private SqlContext _context;

        public IssueController(SqlContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(IssueRequest req)
        {
            try
            {
                var newDate = DateTime.Now;
                var issueEntity = new IssueEntity
                {
                    Subject = req.Subject,
                    Description = req.Description,
                    CustomerId = req.CustomerId,
                    StatusId = 1,
                    Created = newDate,
                    Modified = newDate,

                };

                _context.Issues.Add(issueEntity);
                await _context.SaveChangesAsync();

                return new OkObjectResult(issueEntity);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { message = e, error = "Could not create issue" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var issues = new List<IssueResponse>();
                foreach (var issue in await _context.Issues.Include(x => x.Status).Include(x => x.Customer).ToListAsync())
                {
                    var issueResponse = new IssueResponse
                    {
                        Id = issue.Id,
                        Subject = issue.Subject,
                        Description = issue.Description,
                        Created = issue.Created,
                        Modified = issue.Modified,
                        Status = new StatusResponse
                        {
                            Id = issue.Status.Id,
                            Status = issue.Status.Status,
                        },
                        Customer = new CustomerResponse
                        {
                            Id = issue.Customer.Id,
                            FirstName = issue.Customer.FirstName,
                            LastName = issue.Customer.LastName,
                            Email = issue.Customer.Email,
                            PhoneNumber = issue.Customer.PhoneNumber
                        }

                    };

                    issues.Add(issueResponse);
                }

                return new OkObjectResult(issues);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { message = e, error = "Could not get all the issues" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var issueEntity = await _context.Issues.Include(x => x.Status).Include(x => x.Customer).Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == id);
                if(issueEntity == null)
                {
                    return new BadRequestObjectResult(new { message = "No issue with the id " + id + " exists", error = "Could not get the issue with id: " + id });
                }

                var comments = new List<CommentResponse>();
                foreach (var comment in issueEntity.Comments)
                {
                    var commentResponse = new CommentResponse
                    {
                        Id = comment.Id,
                        Comment = comment.Comment,
                        Created = comment.Created,
                        CustomerId = comment.Customer.Id,
                        CustomerName = issueEntity.Customer.FirstName + " " + issueEntity.Customer.LastName,
                    };

                    comments.Add(commentResponse);
                }

                var issueResponse = new IssueResponse
                {
                    Id = issueEntity.Id,
                    Subject = issueEntity.Subject,
                    Description = issueEntity.Description,
                    Created = issueEntity.Created,
                    Modified = issueEntity.Modified,
                    Status = new StatusResponse
                    {
                        Id = issueEntity.Status.Id,
                        Status = issueEntity.Status.Status,
                    },
                    Customer = new CustomerResponse
                    {
                        Id = issueEntity.Customer.Id,
                        FirstName = issueEntity.Customer.FirstName,
                        LastName = issueEntity.Customer.LastName,
                        Email = issueEntity.Customer.Email,
                        PhoneNumber = issueEntity.Customer.PhoneNumber
                    },
                    Comments = comments
                };

                return new OkObjectResult(issueResponse);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { message = e, error = "Could not get the issue with id: " + id });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Edit(IssueUpdateRequest req)
        {
            try
            {
                var issueEntity = await _context.Issues.FindAsync(req.Id);
                if (issueEntity == null)
                {
                    return new BadRequestObjectResult(new { message = "No issue with id: " + req.Id + " exists", error = "Could not edit issue with id: " + req.Id });
                }
                

                var updatedIssue = new IssueEntity
                {
                    Id = req.Id,
                    Subject = req.Subject,
                    Description = req.Description,
                    Created = issueEntity.Created,
                    Modified = DateTime.Now,
                    StatusId = req.StatusId,
                    CustomerId = req.CustomerId,

                };
                _context.ChangeTracker.Clear();

                _context.Entry(updatedIssue).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new OkObjectResult(updatedIssue);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { message = e, error = "Could not edit issue with id: " + req.Id });
            }
        }
    }
}
