namespace Contacts_API.Models
{
    public class Contact
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public long Phone { get; set; }

        public string Address { get; set; }

        public List<Item> Items { get; set; } = new List<Item>();

        public Account Account { get; set; }
    }
}
