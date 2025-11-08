namespace MiniShop.WebApi.Models.DTOs
{
    public class SaleItemRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class SaleCreateRequest
    {
        public List<SaleItemRequest> Items { get; set; } = new();
    }

    public class SaleResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public string Username { get; set; } = default!;
        public List<SaleItemResponse> Items { get; set; } = new();
    }

    public class SaleItemResponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
    }
}
