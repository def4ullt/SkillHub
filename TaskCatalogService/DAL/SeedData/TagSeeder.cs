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
    public static class TagSeeder
    {
        public static async Task SeedAsync(DbContext context)
        {
            if (await context.Set<Tag>().AnyAsync()) return;

            var tags = new Faker<Tag>()
                .RuleFor(t => t.Id, f => Guid.NewGuid())
                .RuleFor(t => t.Name, f => f.Hacker.Noun());

            await context.Set<Tag>().AddRangeAsync(tags.Generate(10));
            await context.SaveChangesAsync();
        }
    }
}
