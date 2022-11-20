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
                    return new BadRequestObjectResult(new ErrorHandler{ Message = "A customer with that email already exists", Error = "Could not create customer" });
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

                return new OkObjectResult(new CustomerResponse
                {
                    Id = customerEntity.Id,
                    Email = customerEntity.Email,
                    FirstName = customerEntity.FirstName,
                    LastName = customerEntity.LastName,
                    PhoneNumber = customerEntity.PhoneNumber,
                });
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new ErrorHandler { Message = e.Message, Error = "Could not create status" });
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
                    return new BadRequestObjectResult(new ErrorHandler { Message = "Bad Credentials", Error = "Could not login customer" });
                }

                var passwordGeneration = new passwordGeneration();

                if (!passwordGeneration.ValidatePassword(req.Password, customerEntity.PasswordHash, customerEntity.PasswordSalt))
                {
                    return new BadRequestObjectResult(new ErrorHandler { Message = "Bad Credentials", Error = "Could not login customer" });
                }

                return new OkObjectResult(new CustomerResponse
                {
                    Id = customerEntity.Id,
                    Email = customerEntity.Email,
                    FirstName = customerEntity.FirstName,
                    LastName = customerEntity.LastName,
                    PhoneNumber = customerEntity.PhoneNumber,
                });
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new ErrorHandler { Message = e.Message, Error = "Could not login customer" });
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var customers = new List<CustomerResponse>();
                foreach (var customer in await _context.Customers.ToListAsync())
                {
                    var customerResponse = new CustomerResponse
                    {
                        Id = customer.Id,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Email = customer.Email,
                        PhoneNumber = customer.PhoneNumber
                    };
                    customers.Add(customerResponse);
                }

                return new OkObjectResult(customers);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new ErrorHandler { Message = e.Message, Error = "Could not get all customers" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var customerEntity = await _context.Customers.FindAsync(id);
                if (customerEntity == null)
                {
                    return new BadRequestObjectResult(new ErrorHandler { Message = "No customer with id: " + id + " exists", Error = "Could not get customer with id: " + id });
                }

                var customerResponse = new CustomerResponse
                {
                    Id = customerEntity.Id,
                    FirstName = customerEntity.FirstName,
                    LastName= customerEntity.LastName,
                    Email= customerEntity.Email,
                    PhoneNumber= customerEntity.PhoneNumber
                };
                return new OkObjectResult(customerResponse);

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new ErrorHandler { Message = e.Message, Error = "Could not find customer with id: " + id });
            }
        }







    }
}
