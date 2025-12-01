using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.CommandInterfaces;
using Domain.Helpers;
using Domain.Interfaces;
using MediatR;

namespace Application.Reviews.Commands.UpdateReview
{
    public class UpdateTaskReviewHandler : ICommandHandler<UpdateTaskReviewCommand>
    {
        private ITaskReviewRepository repo;

        public UpdateTaskReviewHandler(ITaskReviewRepository repo)
        {
            this.repo = repo;
        }

        public async Task<Unit> Handle(UpdateTaskReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await repo.GetByIdAsync(request.ReviewId, cancellationToken);
            if (review == null)
            {
                throw new NotFoundException("TaskReview", request.ReviewId);
            }

            review.UpdateComment(request.Comment, request.Rating);

            await repo.UpdateAsync(review, cancellationToken);

            return Unit.Value;
        }
    }
}
