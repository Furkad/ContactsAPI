namespace Contacts_API.Models
{
    public class Account
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public List<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
