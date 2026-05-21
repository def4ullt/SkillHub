using Application.Interfaces.CommandInterfaces;
using Domain.ValueObjects;

namespace Application.SubmissionReviews.Commands.CreateSubmissionReview
{
    public record CreateSubmissionReviewCommand(
        Guid SubmissionId,
        Guid TaskId,
        string Feedback,
        UserInformation Mentor
    ) : ICommand<string>;
}
