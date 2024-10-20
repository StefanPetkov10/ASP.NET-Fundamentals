using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using static SeminarHub.Common.ValidationContracts.Seminar;

namespace SeminarHub.Data.Models
{
    //•	Has Id – a unique integer, Primary Key
    //•	Has Topic – string with min length 3 and max length 100 (required)
    //•	Has Lecturer – string with min length 5 and max length 60 (required)
    //•	Has Details – string with min length 10 and max length 500 (required)
    //•	Has OrganizerId – string (required)
    //•	Has Organizer – IdentityUser(required)
    //•	Has DateAndTime – DateTime with format "dd/MM/yyyy HH:mm" (required) (the DateTime format is recommended,
    //  if you are having troubles with this one, you are free to use another one)
    //•	Has Duration – integer value between 30 and 180
    //•	Has CategoryId – integer, foreign key (required)
    //•	Has Category – Category (required)
    //•	Has SeminarsParticipants – a collection of type SeminarParticipant

    public class Seminar
    {
        public Seminar()
        {
            SeminarsParticipants = new HashSet<SeminarParticipant>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(SeminarTopicMaxLength)]
        public string Topic { get; set; } = null!;

        [Required]
        [MaxLength(SeminarLecturerMaxLength)]
        public string Lecturer { get; set; } = null!;

        [Required]
        [MaxLength(SeminarDetailsMaxLength)]
        public string Details { get; set; } = null!;

        [Required]
        public string OrganizerId { get; set; } = null!;
        [ForeignKey(nameof(OrganizerId))]
        public IdentityUser Organizer { get; set; } = null!;

        public DateTime DateAndTime { get; set; }

        [MaxLength(SeminarDurationMaxValue)]
        public int Duration { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }

        public virtual ICollection<SeminarParticipant> SeminarsParticipants { get; set; }
    }
}
