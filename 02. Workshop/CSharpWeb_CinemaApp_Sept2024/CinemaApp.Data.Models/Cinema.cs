namespace CinemaApp.Data.Models
{
    public class Cinema
    {
        public Cinema()
        {
            CinemaMovies = new HashSet<CinemaMovie>();
        }
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public virtual ICollection<CinemaMovie> CinemaMovies { get; set; }
    }
}
