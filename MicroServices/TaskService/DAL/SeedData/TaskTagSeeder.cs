using System;
using System.Collections.Generic;
using System.Linq;
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

            List<TaskEntity> tasks = await context.Set<TaskEntity>().ToListAsync();
            List<Tag> tags = await context.Set<Tag>().ToListAsync();

            if (!tasks.Any() || !tags.Any()) return; 

            List<TaskTag> taskTags = new List<TaskTag>();
            Faker faker = new Faker();

            foreach (TaskEntity task in tasks)
            {
                List<Tag> selectedTags = faker.PickRandom(tags, faker.Random.Int(1, Math.Min(5, tags.Count))).Distinct().ToList();

                foreach (Tag tag in selectedTags)
                {
                    if (!taskTags.Any(tt => tt.TaskId == task.Id && tt.TagId == tag.Id))
                    {
                        taskTags.Add(new TaskTag
                        {
                            TaskId = task.Id,
                            TagId = tag.Id
                        });
                    }
                }
            }

            if (taskTags.Any())
            {
                await context.Set<TaskTag>().AddRangeAsync(taskTags);
                await context.SaveChangesAsync();
            }
        }
    }
}