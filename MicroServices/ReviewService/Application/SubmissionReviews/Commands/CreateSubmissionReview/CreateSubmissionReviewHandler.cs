using Application.Interfaces.CommandInterfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.SubmissionReviews.Commands.CreateSubmissionReview
{
    public class CreateSubmissionReviewHandler : ICommandHandler<CreateSubmissionReviewCommand, string>
    {
        private readonly ISubmissionReviewRepository repo;

        public CreateSubmissionReviewHandler(ISubmissionReviewRepository repo)
        {
            this.repo = repo;
        }

        public async Task<string> Handle(CreateSubmissionReviewCommand request, CancellationToken cancellationToken)
        {
            var review = new SubmissionReview(
                request.SubmissionId,
                request.TaskId,
                request.Mentor,
                request.Feedback
            );

            await repo.AddAsync(review, cancellationToken);
            return review.Id;
        }
    }
}
