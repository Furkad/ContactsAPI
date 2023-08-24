using Contacts_API.Data;
using Contacts_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contacts_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        private readonly ContactsAPIDbContext dbContext;

        public ItemsController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetItemsOfContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            bool hasItems = await dbContext.Items.AnyAsync(x => x.Contact.Id == contact.Id);
            if (hasItems)
                return Ok(dbContext.Items.Where(x => x.Contact.Id == contact.Id).ToList());
            else
                return Ok("No items");

        }

        [HttpGet]
        [Route("{id:guid}/{oneItem:bool}")]
        public async Task<IActionResult> GetItem([FromRoute] Guid id, bool oneItem)
        {
            var item = await dbContext.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        [Route("{id:Guid}")]
        public async Task<IActionResult> AddItemToContact(AddItemRequest addItemRequest, [FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);

            var item = new Item()
            {
                Id = Guid.NewGuid(),
                Name = addItemRequest.Name,
                Description = addItemRequest.Description,
                Amount = addItemRequest.Amount,
                Contact = contact,
            };

            dbContext.Items.Add(item);

            await dbContext.SaveChangesAsync();

            return Ok(item);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateItem([FromRoute] Guid id, AddItemRequest updateItemRequest)
        {
            var item = await dbContext.Items.FindAsync(id);

            if (item != null)
            {
                item.Name = updateItemRequest.Name;
                item.Description = updateItemRequest.Description;
                item.Amount = updateItemRequest.Amount;

                await dbContext.SaveChangesAsync();

                return Ok(item);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteItem([FromRoute] Guid id)
        {
            var item = await dbContext.Items.FindAsync(id);

            if (item != null)
            {
                dbContext.Remove(item);
                await dbContext.SaveChangesAsync();
                return Ok(item);
            }

            return NotFound();
        }
    }
}
