using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using static DeskMarket.Common.ValidationConstants.Product;
namespace DeskMarket.Data.Models
{
    //    •	Has Id – a unique integer, Primary Key
    //•	Has ProductName – a string with min length 2 and max length 60 (required)
    //•	Has Description – string with min length 10 and max length 250 (required)
    //•	Has Price – decimal in range[1.00m;3000.00m], (required)
    //•	Has ImageUrl – nullable string (not required)
    //•	Has SellerId – string (required)
    //•	Has Seller – IdentityUser(required)
    //•	Has AddedOn – DateTime with format "dd-MM-yyyy" (required)
    //o   The DateTime format is recommended, if you are having troubles with this one
    //o   You are free to use another one)
    //•	Has CategoryId – integer, foreign key (required)
    //•	Has Category – Category (required)
    //•	Has IsDeleted – bool (default value == false)
    //•	Has ProductsClients – a collection of type ProductClient

    public class Product
    {
        public Product()
        {
            IsDeleted = false;
            this.ProductsClients = new HashSet<ProductClient>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ProductNameMaxLength)]
        public string ProductName { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public string SellerId { get; set; } = null!;
        [ForeignKey(nameof(SellerId))]
        public IdentityUser Seller { get; set; } = null!;

        [Required]
        public DateTime AddedOn { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        public bool IsDeleted { get; set; }

        [Required]
        public virtual ICollection<ProductClient> ProductsClients { get; set; }

    }
}
