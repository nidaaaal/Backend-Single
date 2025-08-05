using Emocare.Domain.Entities.Auth;
using Emocare.Domain.Entities.Habits;
using Emocare.Domain.Entities.Journal;
using Emocare.Domain.Entities.Task;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Emocare.Infrastructure.Persistence
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; } 
        public DbSet<PsychologistProfile> PsychologistProfiles { get; set; }
        public DbSet<PasswordHistory> PasswordHistory { get; set; } 
        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<WellnessTask> WellnessTasks { get; set; }
        public DbSet<Habit> Habits { get; set; }
        public DbSet<HabitCategory> HabitCategories { get; set; }   
        public DbSet<HabitCompletion> HabitCompletion { get; set; }
        public DbSet<UserPushSubscription> UserPushSubscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


    }
}
