using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ValueObjects;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class TaskQuestion : BaseEntity
    {
        [BsonElement("taskId")]
        public Guid TaskId { get; private set; }

        [BsonElement("questionText")]
        public string QuestionText { get; private set; }

        [BsonElement("user")]
        public UserInformation User { get; set; }

        [BsonElement("answers")]
        public List<TaskAnswer> Answers { get; private set; } = new();

        private TaskQuestion()
        {

        }

        public TaskQuestion(Guid taskId, UserInformation user, string questionText)
        {
            TaskId = taskId;
            User = user;
            QuestionText = questionText.Trim();
        }

        public void UpdateQuestion(string newText)
        {
            QuestionText = newText.Trim();
        }

        public void AddAnswer(TaskAnswer answer)
        {
            if (answer == null) throw new ArgumentNullException(nameof(answer));

            Answers.Add(answer);
        }
    }
}
