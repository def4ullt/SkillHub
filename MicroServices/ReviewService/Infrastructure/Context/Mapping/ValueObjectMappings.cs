using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.ValueObjects;
using MongoDB.Bson.Serialization;

namespace Infrastructure.Context.Mapping
{
    public static class ValueObjectMappings
    {
        public static void Register()
        {
            BsonClassMap.RegisterClassMap<UserInformation>(cm =>
            {
                cm.AutoMap();
                cm.MapCreator(u => new UserInformation(u.UserId, u.FirstName, u.LastName));
            });

            BsonClassMap.RegisterClassMap<TaskAnswer>(cm => cm.AutoMap());
            BsonClassMap.RegisterClassMap<TaskQuestion>(cm => cm.AutoMap());
            BsonClassMap.RegisterClassMap<TaskReview>(cm => cm.AutoMap());
        }
    }   
}
