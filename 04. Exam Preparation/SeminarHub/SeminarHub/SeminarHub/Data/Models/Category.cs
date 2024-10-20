using System.ComponentModel.DataAnnotations;

using static SeminarHub.Common.ValidationContracts.Category;

namespace SeminarHub.Data.Models
{
    //•	Has Id – a unique integer, Primary Key
    //•	Has Name – string with min length 3 and max length 50 (required)
    //•	Has Seminars – a collection of type Seminar

    public class Category
    {
        public Category()
        {
            Seminars = new HashSet<Seminar>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Seminar> Seminars { get; set; }
    }
}