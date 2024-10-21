namespace DeskMarket.Models
{
    public class AddCartViewModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
