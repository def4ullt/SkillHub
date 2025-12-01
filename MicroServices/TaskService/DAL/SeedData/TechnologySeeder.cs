using System;
using System.Collections.Generic;
using System.Linq;
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

            List<string> techNames = new List<string>();
            Faker<Technology> faker = new Faker<Technology>()
                .RuleFor(t => t.Id, f => Guid.NewGuid())
                .RuleFor(t => t.Name, f =>
                {
                    string name;
                    do
                    {
                        name = f.Commerce.ProductName();
                    } while (techNames.Contains(name));
                    techNames.Add(name);
                    return name;
                });

            List<Technology> techs = faker.Generate(10);
            await context.Set<Technology>().AddRangeAsync(techs);
            await context.SaveChangesAsync();
        }
    }
}