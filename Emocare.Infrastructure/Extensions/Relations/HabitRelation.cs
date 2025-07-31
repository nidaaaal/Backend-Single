using Emocare.Domain.Entities.Habits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Infrastructure.Extensions.Relations
{

    public class HabitRelation:IEntityTypeConfiguration<Habit>
    {
        public void Configure(EntityTypeBuilder<Habit> entity)
        {
            entity.HasOne(x=>x.User).WithMany(x=>x.Habits).HasForeignKey(x=>x.UserId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.Category).WithMany(x => x.Habits).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
            entity.HasMany(x =>x.Completions).WithOne(x => x.Habit).HasForeignKey(x => x.HabitId).OnDelete(DeleteBehavior.Cascade);

            entity.Property(x => x.Frequency).HasConversion<string>();

        }
        public void Configure(EntityTypeBuilder<HabitCategory> entity)
        {
            entity.HasData(
                new HabitCategory { Id=1,Name= "Health & Fitness",ColorCode= "#4CAF50" },
                new HabitCategory { Id = 2, Name = "Productivity", ColorCode = "#2196F3" },
                new HabitCategory { Id = 3, Name = "Mindfulness", ColorCode = "#9C27B0" },
                new HabitCategory { Id = 4, Name = "Learning", ColorCode = "#FF9800" },
                new HabitCategory { Id = 5, Name = "Relationships", ColorCode = "#E91E63" }
                );

        }
    }

    public class HabitCategorySeed : IEntityTypeConfiguration<HabitCategory>
    {
        public void Configure(EntityTypeBuilder<HabitCategory> entity)
        {
            entity.HasData(
                new HabitCategory { Id = 1, Name = "Health & Fitness", ColorCode = "#4CAF50" },
                new HabitCategory { Id = 2, Name = "Productivity", ColorCode = "#2196F3" },
                new HabitCategory { Id = 3, Name = "Mindfulness", ColorCode = "#9C27B0" },
                new HabitCategory { Id = 4, Name = "Learning", ColorCode = "#FF9800" },
                new HabitCategory { Id = 5, Name = "Relationships", ColorCode = "#E91E63" }
                );

        }
    }
}
