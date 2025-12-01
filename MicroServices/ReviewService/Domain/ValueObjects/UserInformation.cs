using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.ValueObjects
{
    public class UserInformation : ValueObject
    {
        [BsonElement("userId")]
        public Guid UserId { get; }

        [BsonElement("firstName")]
        public string FirstName { get; }

        [BsonElement("lastName")]
        public string LastName { get; }

        [JsonConstructor]
        public UserInformation(Guid userId, string firstName, string lastName)
        {
            UserId = userId;
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserId;
            yield return FirstName;
            yield return LastName;
        }
    }
}
