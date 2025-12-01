using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ValueObjects;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class TaskAnswer : BaseEntity
    {

        [BsonElement("answerText")]
        public string AnswerText { get; private set; }

        [BsonElement("user")]
        public UserInformation User { get; set; }

        private TaskAnswer()
        {

        }

        public TaskAnswer(UserInformation user, string answerText)
        {
            User = user;
            AnswerText = answerText.Trim();
        }

        public void UpdateAnswer(string newText)
        {
            AnswerText = newText.Trim();
        }
    }
}
