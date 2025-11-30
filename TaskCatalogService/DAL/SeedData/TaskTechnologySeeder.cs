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
    public static class TaskTechnologySeeder
    {
        public static async Task SeedAsync(DbContext context)
        {
            if (await context.Set<TaskTechnology>().AnyAsync()) return;

            var tasks = await context.Set<TaskEntity>().ToListAsync();
            var techs = await context.Set<Technology>().ToListAsync();

            var taskTechs = new List<TaskTechnology>();
            var faker = new Faker();

            foreach (var task in tasks)
            {
                var selectedTechs = faker.PickRandom(techs, faker.Random.Int(1, 3)).ToList();
                foreach (var tech in selectedTechs)
                {
                    taskTechs.Add(new TaskTechnology
                    {
                        TaskId = task.Id,
                        TechnologyId = tech.Id
                    });
                }
            }

            await context.Set<TaskTechnology>().AddRangeAsync(taskTechs);
            await context.SaveChangesAsync();
        }
    }
}
