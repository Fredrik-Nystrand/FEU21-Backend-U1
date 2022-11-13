using backend.Contexts;
using backend.Models.Customer;
using backend.Models.Status;
using backend.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private SqlContext _context;

        public CustomerController(SqlContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> Create(CustomerRequest req)
        {
            try
            {
                if (await _context.Customers.AnyAsync(x => x.Email == req.Email))
                {
                    return new BadRequestObjectResult(new { message = "A customer with that email already exists", error = "Could not create customer" });
                }

                var passwordGeneration = new passwordGeneration();

                passwordGeneration.GeneratePassword(req.Password);

                var customerEntity = new CustomerEntity {
                FirstName = req.FirstName,
                LastName = req.LastName,
                Email = req.Email,
                PasswordHash = passwordGeneration.PasswordHash,
                PasswordSalt = passwordGeneration.PasswordSalt,
                PhoneNumber = req.PhoneNumber,
                };

                _context.Customers.Add(customerEntity);
                await _context.SaveChangesAsync();

                return new OkObjectResult(customerEntity);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { message = e, error = "Could not create status" });
            }

        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest req)
        {
            try
            {
                var customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.Email == req.Email);

                if (customerEntity == null)
                {
                    return new BadRequestObjectResult(new { message = "Bad Credentials", error = "Could not login customer" });
                }

                var passwordGeneration = new passwordGeneration();

                if (!passwordGeneration.ValidatePassword(req.Password, customerEntity.PasswordHash, customerEntity.PasswordSalt))
                {
                    return new BadRequestObjectResult(new { message = "Bad Credentials", error = "Could not login customer" });
                }

                return new OkObjectResult(new {customerId = customerEntity.Id});
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { message = e, error = "Could not login customer" });
            }

        }







    }
}
