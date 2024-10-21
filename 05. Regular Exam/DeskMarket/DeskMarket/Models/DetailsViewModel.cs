using static DeskMarket.Common.ValidationConstants.Product;

namespace DeskMarket.Models
{
    public class DetailsViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string CategoryName { get; set; } = null!;
        public string AddedOn { get; set; } = DateTime.Today.ToString(AddedOnFormat);
        public string Seller { get; set; } = null!;
        public bool HasBought { get; set; }
    }
}
