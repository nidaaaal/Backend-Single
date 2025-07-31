

using Emocare.Domain.Entities.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emocare.Infrastructure.Extensions.Relations
{
    public class ChatSessionRelation: IEntityTypeConfiguration<ChatSession>
    {
        public void Configure(EntityTypeBuilder<ChatSession> entity) 
        { 
            entity.HasMany(x=>x.Messages).WithOne(x=>x.ChatSession)
                .HasForeignKey(x=>x.ChatSessionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        
    }
}
