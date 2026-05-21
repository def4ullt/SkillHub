using Application.Interfaces.CommandInterfaces;
using Domain.Helpers;
using Domain.Interfaces;
using MediatR;

namespace Application.SubmissionReviews.Commands.DeleteSubmissionReview
{
    public class DeleteSubmissionReviewHandler : ICommandHandler<DeleteSubmissionReviewCommand, Unit>
    {
        private readonly ISubmissionReviewRepository repo;

        public DeleteSubmissionReviewHandler(ISubmissionReviewRepository repo)
        {
            this.repo = repo;
        }

        public async Task<Unit> Handle(DeleteSubmissionReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await repo.GetByIdAsync(request.ReviewId, cancellationToken);
            if (review == null)
                throw new NotFoundException("SubmissionReview", request.ReviewId);

            await repo.DeleteAsync(request.ReviewId, cancellationToken);
            return Unit.Value;
        }
    }
}
