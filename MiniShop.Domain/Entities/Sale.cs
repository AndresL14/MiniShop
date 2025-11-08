
namespace MiniShop.Domain.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; }

        public User? User { get; set; }
        public List<SaleItem> Items { get; set; } = new();
    }
}
