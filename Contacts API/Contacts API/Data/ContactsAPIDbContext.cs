using Contacts_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Contacts_API.Data
{
    public class ContactsAPIDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Item> Items { get; set; }

        public ContactsAPIDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasKey(x => new { x.Login, x.Password});
        }
    }
}
