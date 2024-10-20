namespace DeskMarket.Models
{
    public class AllProductViewModel
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public bool IsSeller { get; set; }
        public bool HasBought { get; set; }

    }
}
