using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.SeedData
{
    public static class TagSeeder
    {
        public static async Task SeedAsync(DbContext context)
        {
            if (await context.Set<Tag>().AnyAsync()) return;

            List<Tag> existingTags = await context.Set<Tag>().ToListAsync();

            List<string> tagNames = new List<string>();
            Faker<Tag> faker = new Faker<Tag>()
                .RuleFor(t => t.Id, f => Guid.NewGuid())
                .RuleFor(t => t.Name, f =>
                {
                    string name;
                    do
                    {
                        name = f.Hacker.Noun();
                    } while (tagNames.Contains(name) || existingTags.Any(t => t.Name == name));
                    tagNames.Add(name);
                    return name;
                });

            List<Tag> tags = faker.Generate(10);

            await context.Set<Tag>().AddRangeAsync(tags);
            await context.SaveChangesAsync();
        }
    }
}
