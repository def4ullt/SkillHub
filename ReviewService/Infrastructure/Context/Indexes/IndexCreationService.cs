using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure.Context.Indexes
{
    public class IndexCreationService : IIndexCreationService
    {
        private MongoDbContext dbContext;

        public IndexCreationService(MongoDbContext context)
        {
            dbContext = context;
        }

        public void CreateIndexes()
        {
            var reviewCollection = dbContext.TaskReviews;
            var reviewKeys = Builders<TaskReview>.IndexKeys;

            reviewCollection.Indexes.CreateOne(new CreateIndexModel<TaskReview>(reviewKeys.Ascending(r => r.User.UserId)));
            reviewCollection.Indexes.CreateOne(new CreateIndexModel<TaskReview>(reviewKeys.Ascending(r => r.TaskId)));
            reviewCollection.Indexes.CreateOne(new CreateIndexModel<TaskReview>(reviewKeys.Ascending(r => r.Rating)));

            var questionCollection = dbContext.TaskQuestions;
            var questionKeys = Builders<TaskQuestion>.IndexKeys;

            questionCollection.Indexes.CreateOne(new CreateIndexModel<TaskQuestion>(questionKeys.Ascending(q => q.User.UserId)));
            questionCollection.Indexes.CreateOne(new CreateIndexModel<TaskQuestion>(questionKeys.Ascending(q => q.TaskId)));
        }
    }   
}
