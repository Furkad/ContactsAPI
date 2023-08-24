using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Contacts_API.Data;
using Contacts_API.Models;
using System.Diagnostics;

namespace Contacts_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ContactsAPIDbContext dbContext;

        public AccountController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            return Ok(await dbContext.Accounts.ToListAsync());
        }

        [HttpGet]
        [Route("{login}/{password}")]
        public async Task<IActionResult> GetAccount([FromRoute]string login, string password)
        {
            var account = await dbContext.Accounts.FindAsync(new object[] { login, password });
            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        [HttpPost]
        public async Task<IActionResult> AddAccount(AddAccountRequest addAccountRequest)
        {
            var account = new Account()
            {
                Id = Guid.NewGuid(),
                Login = addAccountRequest.Login,
                Password = addAccountRequest.Password
            };

            await dbContext.Accounts.AddAsync(account);
            await dbContext.SaveChangesAsync();

            return Ok(account);
        }
    }
}
