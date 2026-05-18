using Application.Interfaces.CommandInterfaces;
using Domain.Entities;
using Infrastructure.Services;
using Domain.Helpers;
using Domain.Interfaces;

namespace Application.Reviews.Commands.CreateReview
{
    public class CreateTaskReviewHandler : ICommandHandler<CreateTaskReviewCommand, string>
    {
        private readonly ITaskReviewRepository repo;
        private readonly ISentimentAnalysisService sentimentService;

        public CreateTaskReviewHandler(ITaskReviewRepository repo, ISentimentAnalysisService sentimentService)
        {
            this.repo = repo;
            this.sentimentService = sentimentService;
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

            var (sentiment, keyIssues) = await sentimentService.AnalyzeAsync(request.Comment, cancellationToken);
            review.SetSentiment(sentiment, keyIssues);

            await repo.AddAsync(review, cancellationToken);

            return review.Id;
        }
    }
}
