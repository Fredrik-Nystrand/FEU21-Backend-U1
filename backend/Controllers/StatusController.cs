using backend.Contexts;
using backend.Models.Status;
using backend.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private SqlContext _context;
        public StatusController(SqlContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(StatusRequest req)
        {
            try
            {
                if (await _context.Statuses.AnyAsync(x => x.Status == req.Status))
                {
                    return new BadRequestObjectResult(new ErrorHandler { Message = "A status with that name already exists", Error = "Could not create status" });
                }

                var statusEntity = new StatusEntity { Status = req.Status };
                _context.Statuses.Add(statusEntity);
                await _context.SaveChangesAsync();

                return new OkObjectResult(statusEntity);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new ErrorHandler { Message = e.Message, Error = "Could not create status" });
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var statuses = new List<StatusResponse>();
                foreach (var status in await _context.Statuses.ToListAsync())
                {
                    var statusResponse = new StatusResponse
                    {
                        Id = status.Id,
                        Status = status.Status,
                    };

                    statuses.Add(statusResponse);
                }

                return new OkObjectResult(statuses);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new ErrorHandler { Message = e.Message, Error = "Could not get all statuses" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var statusEntity = await _context.Statuses.FindAsync(id);

                if (statusEntity == null)
                {
                    return new BadRequestObjectResult(new ErrorHandler { Message = "No status with id: " + id + " exists", Error = "Could not get status with id: " + id });
                }

                var statusResponse = new StatusResponse
                {
                    Id = statusEntity.Id,
                    Status = statusEntity.Status,
                };
                return new OkObjectResult(statusResponse);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new ErrorHandler { Message = e.Message, Error = "Could not find status with id: " + id });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Edit(StatusEntity req)
        {
            try
            {
                var statusEntity = await _context.Statuses.FindAsync(req.Id);
                if (statusEntity == null)
                {
                    return new BadRequestObjectResult(new ErrorHandler { Message = "No status with id: " + req.Id + " exists", Error = "Could not edit status with id: " + req.Id });
                }
                _context.ChangeTracker.Clear();

                _context.Entry(req).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new OkObjectResult(req);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new ErrorHandler { Message = e.Message, Error = "Could not edit status with id: " + req.Id});
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var statusEntity = await _context.Statuses.FindAsync(id);
            if (statusEntity == null)
            {
                return new BadRequestObjectResult(new ErrorHandler { Message = "No status with id: " + id + " exists", Error = "Could not delete status with id: " + id });
            }

            try
            {
                _context.Statuses.Remove(statusEntity);
                await _context.SaveChangesAsync();

                return new OkObjectResult(statusEntity);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new ErrorHandler { Message = e.Message, Error = "Could not delete status with id: " + id });
            }
        }
    }
}
