using Domain.ValueObjects;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class SubmissionReview : BaseEntity
    {
        [BsonElement("submissionId")]
        public Guid SubmissionId { get; private set; }

        [BsonElement("taskId")]
        public Guid TaskId { get; private set; }

        [BsonElement("feedback")]
        public string Feedback { get; private set; }

        [BsonElement("mentor")]
        public UserInformation Mentor { get; set; }

        private SubmissionReview() { }

        public SubmissionReview(Guid submissionId, Guid taskId, UserInformation mentor, string feedback)
        {
            SubmissionId = submissionId;
            TaskId = taskId;
            Mentor = mentor;
            Feedback = feedback.Trim();
        }

        public void UpdateFeedback(string newFeedback)
        {
            Feedback = newFeedback.Trim();
        }
    }
}
