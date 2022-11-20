using backend.Contexts;
using backend.Models.Comment;
using backend.Models.Status;
using backend.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private SqlContext _context;

        public CommentController(SqlContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommentRequest req)
        {
            try
            {

                var commentEntity = new CommentEntity 
                { 
                    Comment = req.Comment,
                    Created = DateTime.Now,
                    CustomerId = req.CustomerId,
                    IssueId = req.IssueId,
                };

                _context.Comments.Add(commentEntity);
                await _context.SaveChangesAsync();

                return new OkObjectResult(commentEntity);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new ErrorHandler { Message = e.Message, Error = "Could not create status" });
            }

        }
    }
}
