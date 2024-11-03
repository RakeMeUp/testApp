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

        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionGrade> QuestionGrades { get; set; }
        public DbSet<UserTestResult> UserTestResults { get; set; }
        public DbSet<UserCreatedTest> UserCreatedTests { get; set; }
        public DbSet<UserParticipatedTest> UserParticipatedTests { get; set; }


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

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Test)
                .WithMany(t => t.Questions)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserTestResult>()
                .HasOne(utr => utr.Test)
                .WithMany(t => t.TestResults)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuestionGrade>()
                .HasKey(qg => new { qg.UserId, qg.ResultId, qg.QuestionId });

            modelBuilder.Entity<QuestionGrade>()
                .HasOne(qg => qg.UserTestResult)
                .WithMany(utr => utr.QuestionGrades) 
                .HasForeignKey(qg => qg.ResultId);

            modelBuilder.Entity<QuestionGrade>()
                .HasOne(qg => qg.Question)
                .WithMany(q => q.QuestionGrades)
                .HasForeignKey(qg => qg.QuestionId);
        }
    }


}
