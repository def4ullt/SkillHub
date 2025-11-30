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
    public static class TechnologySeeder
    {
        public static async Task SeedAsync(DbContext context)
        {
            if (await context.Set<Technology>().AnyAsync()) return;

            var techs = new Faker<Technology>()
                .RuleFor(t => t.Id, f => Guid.NewGuid())
                .RuleFor(t => t.Name, f => f.Commerce.ProductName());

            await context.Set<Technology>().AddRangeAsync(techs.Generate(10));
            await context.SaveChangesAsync();
        }
    }
}
