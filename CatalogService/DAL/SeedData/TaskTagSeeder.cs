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
    public static class TaskTagSeeder
    {
        public static async Task SeedAsync(DbContext context)
        {
            if (await context.Set<TaskTag>().AnyAsync()) return;

            var tasks = await context.Set<TaskEntity>().ToListAsync();
            var tags = await context.Set<Tag>().ToListAsync();

            var taskTags = new List<TaskTag>();
            var faker = new Faker();

            foreach (var task in tasks)
            {
                var selectedTags = faker.PickRandom(tags, faker.Random.Int(1, 5)).ToList();
                foreach (var tag in selectedTags)
                {
                    taskTags.Add(new TaskTag
                    {
                        TaskId = task.Id,
                        TagId = tag.Id
                    });
                }
            }

            await context.Set<TaskTag>().AddRangeAsync(taskTags);
            await context.SaveChangesAsync();
        }
    }
}
