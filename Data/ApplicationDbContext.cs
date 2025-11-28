using Microsoft.EntityFrameworkCore;
using PawPal_QualityOfLife.Models;

namespace PawPal_QualityOfLife.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetAssessment> PetAssessments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Type).IsRequired().HasMaxLength(30);
                entity.Property(e => e.Breed).IsRequired().HasMaxLength(50);
                entity.Property(e => e.OwnerName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.OwnerEmail).HasMaxLength(100);
                entity.Property(e => e.OwnerPhone).HasMaxLength(20);
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.LastUpdated).HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<PetAssessment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.AssessmentDate).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.AdditionalNotes).HasMaxLength(500);
                entity.Property(e => e.QualityRating).HasMaxLength(20);
                entity.Property(e => e.OverallScore).HasPrecision(4, 2);

                entity.HasOne(e => e.Pet)
                      .WithMany(p => p.Assessments)
                      .HasForeignKey(e => e.PetId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Pet>().HasData(
                new Pet
                {
                    Id = 1,
                    Name = "Buddy",
                    Type = "Dog",
                    Age = 5,
                    Breed = "Golden Retriever",
                    OwnerName = "John Smith",
                    OwnerEmail = "john.smith@email.com",
                    OwnerPhone = "555-123-4567",
                    CreatedDate = DateTime.Now.AddMonths(-6),
                    LastUpdated = DateTime.Now.AddMonths(-6)
                },
                new Pet
                {
                    Id = 2,
                    Name = "Whiskers",
                    Type = "Cat",
                    Age = 3,
                    Breed = "Persian",
                    OwnerName = "Jane Doe",
                    OwnerEmail = "jane.doe@email.com",
                    CreatedDate = DateTime.Now.AddMonths(-3),
                    LastUpdated = DateTime.Now.AddMonths(-3)
                }
            );
        }
    }
}