using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.CommandInterfaces;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;

namespace Application.Reviews.Commands.CreateReview
{
    public class CreateTaskReviewHandler : ICommandHandler<CreateTaskReviewCommand, string>
    {
        private ITaskReviewRepository repo;

        public CreateTaskReviewHandler(ITaskReviewRepository repo)
        {
            this.repo = repo;
        }

        public async Task<string> Handle(CreateTaskReviewCommand request, CancellationToken cancellationToken)
        {
            bool alreadyReviewed = await repo.HasUserReviewedTaskAsync(request.User.UserId, request.TaskId, cancellationToken);
            if (alreadyReviewed)
            {
                throw new AlreadyExistsException($"User {request.User.UserId} has already reviewed task {request.TaskId}");
            }

            var review = new TaskReview(
                request.TaskId,
                request.User,
                request.Rating,
                request.Comment
            );

            await repo.AddAsync(review, cancellationToken);

            return review.Id;
        }
    }
}
