using Application.Interfaces.CommandInterfaces;
using MediatR;

namespace Application.SubmissionReviews.Commands.DeleteSubmissionReview
{
    public record DeleteSubmissionReviewCommand(string ReviewId) : ICommand<Unit>;
}
