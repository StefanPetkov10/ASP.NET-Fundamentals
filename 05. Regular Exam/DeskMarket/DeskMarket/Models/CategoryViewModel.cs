using System.ComponentModel.DataAnnotations;

using static DeskMarket.Common.ValidationConstants.Category;

namespace DeskMarket.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [MaxLength(NameMaxLength)]
        [MinLength(NameMinLength)]
        public string Name { get; set; } = null!;
    }
}
