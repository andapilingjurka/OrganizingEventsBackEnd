using Microsoft.EntityFrameworkCore;
using OrganizingEvents.Models;

namespace OrganizingEvents.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        { }

        public DbSet<EventCategories> EventCategories { get; set; }

        public DbSet<EventThemes> EventThemes { get; set; }

        public DbSet<Events> Events { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Contact> Contact { get; set; }

        public DbSet<Staff> Staff { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Events>()
            .HasOne(p => p.EventThemes)
            .WithMany()
            .HasForeignKey(p => p.ThemeId) //Foreign Key
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Events>()
           .HasOne(p => p.EventCategories)
           .WithMany()
           .HasForeignKey(p => p.CategoryId) //Foreign Key
           .OnDelete(DeleteBehavior.Restrict);

        }

    }
}