namespace Contacts_API.Models
{
    public class Item
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Amount { get; set; }

        public Contact Contact { get; set; }
    }
}
