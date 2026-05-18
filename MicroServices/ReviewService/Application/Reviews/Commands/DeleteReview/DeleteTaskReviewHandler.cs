using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.CommandInterfaces;
using Domain.Helpers;
using Domain.Interfaces;
using MediatR;

namespace Application.Reviews.Commands.DeleteReview
{
    public class DeleteTaskReviewHandler : ICommandHandler<DeleteTaskReviewCommand>
    {
        private ITaskReviewRepository repo;

        public DeleteTaskReviewHandler(ITaskReviewRepository repo)
        {
            this.repo = repo;
        }

        public async Task<Unit> Handle(DeleteTaskReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await repo.GetByIdAsync(request.ReviewId, cancellationToken);
            if (review == null)
                throw new NotFoundException("TaskReview", request.ReviewId);

            if (!request.IsAdmin && review.User.UserId != request.RequestingUserId)
                throw new ForbiddenException("You can only delete your own reviews.");

            await repo.DeleteAsync(request.ReviewId, cancellationToken);

            return Unit.Value;
        }
    }
}
