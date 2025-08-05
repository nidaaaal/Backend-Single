using Emocare.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Infrastructure.Extensions.Relations
{
    public class UsersRelation : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> entity)
        {
            entity.Property(x => x.Role).HasConversion<string>();
            entity.Property(x=>x.Status).HasConversion<string>();

            entity.HasOne(x=>x.PsychologistProfile).WithOne(x=>x.Users)
                .HasForeignKey<PsychologistProfile>(x=>x.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(x => x.PasswordHistory).WithOne(x => x.Users)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(x=>x.ChatSessions).WithOne(x=>x.User)
                .HasForeignKey(x=>x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(x=>x.UserPushSubscriptions).WithOne(x=>x.Users)
                .HasForeignKey(x=>x.UserId)
                .OnDelete(DeleteBehavior.Cascade).IsRequired(false);   
        }
    }
}
