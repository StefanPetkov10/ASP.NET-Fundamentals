using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SeminarHub.Data.Models
{
    //•	Has SeminarId – integer, PrimaryKey, foreign key(required)
    //•	Has Seminar – Seminar
    //•	Has ParticipantId – string, PrimaryKey, foreign key(required)
    //•	Has Participant – IdentityUser

    public class SeminarParticipant
    {
        public int SeminarId { get; set; }

        [ForeignKey(nameof(SeminarId))]
        public virtual Seminar Seminar { get; set; } = null!;

        public string ParticipantId { get; set; } = null!;

        [ForeignKey(nameof(ParticipantId))]
        public virtual IdentityUser Participant { get; set; } = null!;

    }
}