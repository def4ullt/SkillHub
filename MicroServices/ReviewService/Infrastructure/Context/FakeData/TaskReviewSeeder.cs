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
    public class TaskReviewSeeder : IDataSeeder
    {
        private readonly IMongoCollection<TaskReview> _reviews;

        public TaskReviewSeeder(MongoDbContext context)
        {
            _reviews = context.TaskReviews;
        }

        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            var count = await _reviews.CountDocumentsAsync(FilterDefinition<TaskReview>.Empty, cancellationToken: cancellationToken);
            if (count > 0) return;

            var fakeData = new List<TaskReview>
            {
                new TaskReview(
                    Guid.NewGuid(),
                    new UserInformation(Guid.NewGuid(), "Alice", "King"),
                    5,
                    "Awesome task! Learned a lot!"
                ),
                new TaskReview(
                    Guid.NewGuid(),
                    new UserInformation(Guid.NewGuid(), "Bob", "Marsh"),
                    4,
                    "Pretty good, but wish it had more examples."
                ),
                new TaskReview(
                    Guid.NewGuid(),
                    new UserInformation(Guid.NewGuid(), "Leo", "Parker"),
                    3,
                    "Was okay. A bit too easy for me."
                ),
                new TaskReview(
                    Guid.NewGuid(),
                    new UserInformation(Guid.NewGuid(), "Ella", "Stone"),
                    5,
                    "Super useful task"
                )
            };

            await _reviews.InsertManyAsync(fakeData, cancellationToken: cancellationToken);
        }
    }
}
