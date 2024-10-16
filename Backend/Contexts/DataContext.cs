using Backend.Entities;
using Backend.Entities.Joins;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Contexts
{
    public class DataContext : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        // DbSets for your entities
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionGrade> QuestionGrades { get; set; }
        public DbSet<UserTestResult> UserTestResults { get; set; }
        public DbSet<UserCreatedTest> UserCreatedTests { get; set; }
        public DbSet<UserParticipatedTest> UserParticipatedTests { get; set; }

        // You can also override OnModelCreating if needed

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserCreatedTest>()
            .HasKey(uc => new { uc.UserId, uc.TestId });

            modelBuilder.Entity<UserCreatedTest>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.CreatedTests)
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserCreatedTest>()
                .HasOne(uc => uc.Test)
                .WithMany(t => t.CreatedByUsers)
                .HasForeignKey(uc => uc.TestId);

            // Similar configuration for UserParticipatedTest
            modelBuilder.Entity<UserParticipatedTest>()
                .HasKey(up => new { up.UserId, up.TestId });

            modelBuilder.Entity<UserParticipatedTest>()
                .HasOne(up => up.User)
                .WithMany(u => u.ParticipatedTests)
                .HasForeignKey(up => up.UserId);

            modelBuilder.Entity<UserParticipatedTest>()
                .HasOne(up => up.Test)
                .WithMany(t => t.ParticipatingUsers)
                .HasForeignKey(up => up.TestId);
        }
    }


}
