using Emocare.Domain.Entities.Journal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emocare.Infrastructure.Extensions.Relations
{
    public class JournalRelation: IEntityTypeConfiguration<JournalEntry>
    {
       public void Configure(EntityTypeBuilder<JournalEntry> entity)
        {
            entity.Property(x => x.Mood).HasConversion<string>();
            entity.Property(x => x.Mode).HasConversion<string>();
            entity.HasOne(x => x.User).WithMany(x =>x.JournalEntries).HasForeignKey(x=>x.UserId);

            
        }

    }
}
