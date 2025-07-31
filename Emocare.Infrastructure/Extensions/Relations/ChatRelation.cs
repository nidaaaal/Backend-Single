using Emocare.Domain.Entities.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emocare.Infrastructure.Extensions.Relations
{
    public class ChatRelation:IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> entity)
        {
            entity.Property(x => x.Role).HasConversion<string>();
        }
    }
}
