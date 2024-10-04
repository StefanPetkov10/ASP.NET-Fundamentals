using System.Reflection;
using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore; // Internal project namespace

namespace CinemaApp.Data

{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext()
        {

        }
        public CinemaDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<Cinema> Cinemas { get; set; } = null!;
        public virtual DbSet<CinemaMovie> CinemasMovies { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBilder)
        {
            modelBilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
