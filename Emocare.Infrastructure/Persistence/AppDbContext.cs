using Emocare.Domain.Entities.Auth;
using Emocare.Domain.Entities.Journal;
using Emocare.Domain.Entities.Task;
using Emocare.Infrastructure.Extensions.Relations;
using Microsoft.EntityFrameworkCore;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersRelation());
            modelBuilder.ApplyConfiguration(new JournalRelation()); 
            modelBuilder.ApplyConfiguration(new ChatRelation());
            modelBuilder.ApplyConfiguration(new ChatSessionRelation());
        }


    }
}
