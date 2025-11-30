using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.SeedData
{
    public static class TaskEntitySeeder
    {
        public static async Task SeedAsync(DbContext context)
        {
            if (await context.Set<TaskEntity>().AnyAsync()) return;

            var tasks = new Faker<TaskEntity>()
                .RuleFor(t => t.Id, f => Guid.NewGuid())
                .RuleFor(t => t.Title, f => f.Hacker.Verb() + " " + f.Hacker.Noun())
                .RuleFor(t => t.Description, f => f.Lorem.Paragraph())
                .RuleFor(t => t.Difficulty, f => f.PickRandom<Difficulty>())
                .RuleFor(t => t.EstimatedTimeMinutes, f => f.Random.Int(15, 180))
                .RuleFor(t => t.XpReward, f => f.Random.Int(10, 200))
                .RuleFor(t => t.CreatedAt, f => f.Date.Past())
                .RuleFor(t => t.UpdatedAt, f => f.Date.Recent())
                .RuleFor(t => t.IsActive, f => true);

            await context.Set<TaskEntity>().AddRangeAsync(tasks.Generate(15));
            await context.SaveChangesAsync();
        }
    }

}
