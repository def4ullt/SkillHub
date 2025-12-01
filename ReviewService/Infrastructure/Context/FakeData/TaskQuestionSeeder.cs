using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.ValueObjects;
using MongoDB.Driver;

namespace Infrastructure.Context.FakeData
{
    public class TaskQuestionSeeder : IDataSeeder
    {
        private readonly IMongoCollection<TaskQuestion> _questions;

        public TaskQuestionSeeder(MongoDbContext context)
        {
            _questions = context.TaskQuestions;
        }

        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            var count = await _questions.CountDocumentsAsync(FilterDefinition<TaskQuestion>.Empty, cancellationToken: cancellationToken);
            if (count > 0) return;

            var q1 = new TaskQuestion(
                Guid.NewGuid(),
                new UserInformation(Guid.NewGuid(), "Alice", "Walker"),
                "How do I correctly structure MVC project?"
            );

            q1.AddAnswer(new TaskAnswer(
                new UserInformation(Guid.NewGuid(), "Mike", "Brown"),
                "Keep controllers thin, use services for business logic."
            ));

            q1.AddAnswer(new TaskAnswer(
                new UserInformation(Guid.NewGuid(), "Nina", "Smith"),
                "Separate DTOs from entities to avoid leaking domain models."
            ));


            var q2 = new TaskQuestion(
                Guid.NewGuid(),
                new UserInformation(Guid.NewGuid(), "John", "Doe"),
                "Is MongoDB good for storing task submissions?"
            );

            q2.AddAnswer(new TaskAnswer(
                new UserInformation(Guid.NewGuid(), "Laura", "Green"),
                "Yes, it's great for schema-flexible data like submissions."
            ));


            var q3 = new TaskQuestion(
                Guid.NewGuid(),
                new UserInformation(Guid.NewGuid(), "Sara", "White"),
                "Should I use DTOs or return domain models directly?"
            );

            var list = new List<TaskQuestion> { q1, q2, q3 };

            await _questions.InsertManyAsync(list, cancellationToken: cancellationToken);
        }
    }
}
