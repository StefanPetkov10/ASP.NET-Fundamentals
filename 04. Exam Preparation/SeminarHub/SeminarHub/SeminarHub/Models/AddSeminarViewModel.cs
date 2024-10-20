using System.ComponentModel.DataAnnotations;

using static SeminarHub.Common.ValidationContracts.Seminar;

namespace SeminarHub.Models
{
    public class AddSeminarViewModel
    {
        [Required]
        [MinLength(SeminarTopicMinLength)]
        [MaxLength(SeminarTopicMaxLength)]
        public string Topic { get; set; } = null!;

        [Required]
        [MinLength(SeminarLecturerMinLength)]
        [MaxLength(SeminarLecturerMaxLength)]
        public string Lecturer { get; set; } = null!;

        [Required]
        [MinLength(SeminarDetailsMinLength)]
        [MaxLength(SeminarDetailsMaxLength)]
        public string Details { get; set; } = null!;

        [Required]
        public string DateAndTime { get; set; } = null!;

        [Required(ErrorMessage = "Duration is Required")]
        [Range(SeminarDurationMinValue, SeminarDurationMaxValue)]
        public int? Duration { get; set; }

        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        public virtual IEnumerable<CategoryViewModel>? Categories { get; set; }

    }
}
