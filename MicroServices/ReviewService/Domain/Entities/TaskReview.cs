using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ValueObjects;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class TaskReview : BaseEntity
    {
        [BsonElement("taskId")]
        public Guid TaskId { get; private set; }

        [BsonElement("rating")]
        public int Rating { get; private set; }

        [BsonElement("comment")]
        public string Comment { get; private set; }


        [BsonElement("user")]
        public UserInformation User { get; set; }

        private TaskReview()
        {

        }

        public TaskReview(Guid taskId, UserInformation user, int rating, string comment)
        {
            TaskId = taskId;
            User = user;
            Rating = rating;
            Comment = comment.Trim();
        }

        public void UpdateComment(string newComment, int newRating)
        {
            Comment = newComment.Trim();
            Rating = newRating;
        }
    }
}
