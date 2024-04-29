using ASPNETAPITheStep.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNETAPITheStep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly TokenService service;

        private readonly BankContext _context;

        public LoginController(TokenService service, BankContext context)
        {
            this.service = service;
            _context = context;
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var response = this.service.GenerateToken(username, password);

            if (response.IsValid)
            {
                return Ok(new { Token = response.Token });
            }
            else
            {
                return BadRequest("Username and/or Password is wrong");
            }
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserRegistration userRegistration)
        {
            bool isAdmin = false;
            if (_context.UserAccounts.Count() == 0)
            {
                isAdmin = true;
            }

            var newAccount = UserAccount.Create(userRegistration.Username, userRegistration.Password, isAdmin);

            _context.UserAccounts.Add(newAccount);
            _context.SaveChanges();

            return Ok(new { Message = "User Created" });
        }
    }
}
