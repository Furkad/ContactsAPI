using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Contacts_API.Data;
using Contacts_API.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Diagnostics;

namespace Contacts_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext dbContext;

        public ContactsController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());
        }

        [HttpGet]
        [Route("{login}/{password}")]
        public async Task<IActionResult> GetContactsOfAccount([FromRoute] string login, string password)
        {
            var account = await dbContext.Accounts.FindAsync(new object[]{ login, password});

            if (account == null)
            {
                return NotFound();
            }

            bool hasContacts = await dbContext.Contacts.AnyAsync(x => x.Account.Login == account.Login && x.Account.Password == account.Password);
            if (hasContacts)
                return Ok(dbContext.Contacts.Where(x => x.Account.Login == account.Login && x.Account.Password == account.Password).ToList());
            else
                return Ok("No contacts");

        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addContactRequest.Address,
                Email = addContactRequest.Email,
                FullName = addContactRequest.FullName,
                Phone = addContactRequest.Phone
            };

            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPost]
        [Route("{login}/{password}")]
        public async Task<IActionResult> AddContactToAccount(AddContactRequest addContactRequest, [FromRoute] string login, string password)
        {
            var account = await dbContext.Accounts.FindAsync(new object[] { login, password });

            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addContactRequest.Address,
                Email = addContactRequest.Email,
                FullName = addContactRequest.FullName,
                Phone = addContactRequest.Phone,
                Account = account,
            };

            dbContext.Contacts.Add(contact);

            await dbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact = await dbContext.Contacts.FindAsync(id);

            if (contact != null)
            {
                contact.FullName = updateContactRequest.FullName;
                contact.Address = updateContactRequest.Address;
                contact.Email = updateContactRequest.Email;
                contact.Phone= updateContactRequest.Phone;

                await dbContext.SaveChangesAsync();

                return Ok(contact);
            }
            
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);

            if (contact != null)
            {
                dbContext.Remove(contact);
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }

            return NotFound();
        }
    }
}
