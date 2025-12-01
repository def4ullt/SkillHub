using System;
using System.Collections.Generic;
using System.Linq;
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

            List<TaskEntity> tasks = await context.Set<TaskEntity>().ToListAsync();
            List<Technology> techs = await context.Set<Technology>().ToListAsync();

            if (!tasks.Any() || !techs.Any()) return; 

            List<TaskTechnology> taskTechs = new List<TaskTechnology>();
            Faker faker = new Faker();

            foreach (TaskEntity task in tasks)
            {
                List<Technology> selectedTechs = faker.PickRandom(techs, faker.Random.Int(1, Math.Min(3, techs.Count))).Distinct().ToList();

                foreach (Technology tech in selectedTechs)
                {
                    if (!taskTechs.Any(tt => tt.TaskId == task.Id && tt.TechnologyId == tech.Id))
                    {
                        taskTechs.Add(new TaskTechnology
                        {
                            TaskId = task.Id,
                            TechnologyId = tech.Id
                        });
                    }
                }
            }

            if (taskTechs.Any())
            {
                await context.Set<TaskTechnology>().AddRangeAsync(taskTechs);
                await context.SaveChangesAsync();
            }
        }
    }
}