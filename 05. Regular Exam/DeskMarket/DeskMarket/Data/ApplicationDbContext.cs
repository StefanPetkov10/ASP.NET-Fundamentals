using DeskMarket.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeskMarket.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<ProductClient> ProductsClients { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProductClient>()
                .HasKey(pc => new { pc.ProductId, pc.ClientId });

            builder.Entity<ProductClient>()
                .HasOne(pc => pc.Product)
                .WithMany(pc => pc.ProductsClients)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ProductClient>()
                .HasOne(pc => pc.Client)
                .WithMany()
                .HasForeignKey(pc => pc.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            builder
                .Entity<Category>()
                .HasData(
                    new Category { Id = 1, Name = "Laptops" },
                    new Category { Id = 2, Name = "Workstations" },
                    new Category { Id = 3, Name = "Accessories" },
                    new Category { Id = 4, Name = "Desktops" },
                    new Category { Id = 5, Name = "Monitors" });


            //builder
            //    .Entity<Product>()
            //    .HasData(
            //        new Product
            //        {
            //            Id = 3,
            //            ProductName = "Dell XPS 13",
            //            Description = "The Dell XPS 13 is a premium laptop with a high-resolution display, a super-slim design, and a powerful Intel Core processor.",
            //            Price = 2000,
            //            ImageUrl = "https://cdn.izotcomputers.com/10141-large_default/laptop-vtora-upotreba-dell-latitude-5420.jpg",
            //            SellerId = "ccd39692-fe1e-47b7-8b39-c801338f5c67",
            //            AddedOn = new System.DateTime(2021, 1, 1),
            //            CategoryId = 1
            //        }
            //    );
        }
    }
}
