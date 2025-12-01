using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Context.Config;
using Infrastructure.Context.Indexes;
using Infrastructure.Context.Unit_of_work;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Infrastructure.Context.FakeData;

namespace Infrastructure.Context
{
    public static class ServiceCollectionExtensions
    {
        private static bool isGuidSerializerRegistered = false;
        private static readonly object _lock = new object();

        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureMongoDbSerialization();

            var connectionString = configuration.GetConnectionString("ClothyReviewsDb");

            MongoDbSettings mongoSettings = new MongoDbSettings
            {
                ConnectionString = connectionString,
                DatabaseName = "ClothyReviewsDb"
            };

            services.AddSingleton(mongoSettings);

            MongoDbContext context = new MongoDbContext(mongoSettings);
            services.AddSingleton(context);

            services.AddScoped<IUnitOfWork>(sp =>
            {
                var mongoDb = sp.GetRequiredService<MongoDbContext>().Database;
                return new UnitOfWork(mongoDb);
            });

            services.AddSingleton<IIndexCreationService, IndexCreationService>();
            services.AddSingleton<TaskReviewSeeder>();
            services.AddSingleton<TaskQuestionSeeder>();

            return services;
        }

        private static void ConfigureMongoDbSerialization()
        {
            lock (_lock)
            {
                if (!isGuidSerializerRegistered)
                {
                    BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
                    isGuidSerializerRegistered = true;
                }
            }
        }

        public static void EnsureIndexes(this IServiceProvider serviceProvider)
        {
            var indexService = serviceProvider.GetRequiredService<IIndexCreationService>();
            indexService.CreateIndexes();
        }
    }
}
