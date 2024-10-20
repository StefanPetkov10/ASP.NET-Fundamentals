using System.ComponentModel.DataAnnotations;
using static DeskMarket.Common.ValidationConstants.Category;

namespace DeskMarket.Data.Models
{
    //    •	Has Id – a unique integer, Primary Key
    //•	Has Name – a string with min length 3 and max length 20 (required)
    //•	Has Products – a collection of type Product

    public class Category
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}