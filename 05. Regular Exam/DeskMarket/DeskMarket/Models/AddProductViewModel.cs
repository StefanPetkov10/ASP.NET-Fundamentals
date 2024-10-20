using System.ComponentModel.DataAnnotations;

using static DeskMarket.Common.ValidationConstants.Product;

namespace DeskMarket.Models
{
    public class AddProductViewModel
    {
        [Required]
        [MaxLength(ProductNameMaxLength)]
        [MinLength(ProductNameMinLength)]
        public string ProductName { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        [MinLength(DescriptionMinLength)]
        public string Description { get; set; } = null!;

        public string? ImageUrl { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string AddedOn { get; set; } = DateTime.Today.ToString("dd-MM-yyyy");

        [Required]
        [Range((double)PriceMinValue, (double)PriceMaxValue)]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public virtual IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
