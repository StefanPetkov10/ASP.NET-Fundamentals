using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Web.ViewModels.Movie
{
    public class AddMovieFromModel
    {
        [Required]
        [MaxLength]
        public string Title { get; set; } = null!;
    }
}
