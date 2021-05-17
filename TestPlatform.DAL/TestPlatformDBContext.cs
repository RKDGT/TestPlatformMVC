using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestPlatform.DAL.Entities;
using TestPlatform.DAL.Identity;

namespace TestPlatform.DAL
{
    public class TestPlatformDBContext : IdentityDbContext<User>
    {
        public TestPlatformDBContext(DbContextOptions<TestPlatformDBContext> options) : base(options) { }
        public DbSet<Answers> Answers { get; set; }
        public DbSet<CourseInfo> CoursesInfo { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<GroupsCourses> GroupsCourses { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<JournalSubJournal> JournalSubJournals { get; set; }
        public DbSet<QuestionAdditional> QuestionAdditionals { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<QuestionsAnswers> QuestionsAnswers { get; set; }
        public DbSet<Tests> Tests { get; set; }
        public DbSet<Theory> Theories { get; set; }
        public DbSet<TheoryFiles> TheoryFiles { get; set; }
        public DbSet<UsersCourses> UsersCourses { get; set; }
        public DbSet<UsersGroups> UsersGroups { get; set; }
        public DbSet<UsersJournals> UsersJournals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=TestPlatformDB;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
