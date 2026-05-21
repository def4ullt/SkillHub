using Domain.Entities;
using Domain.ValueObjects;
using MongoDB.Driver;

namespace Infrastructure.Context.FakeData
{
    public class SubmissionReviewSeeder : IDataSeeder
    {
        private readonly IMongoCollection<SubmissionReview> _reviews;

        public SubmissionReviewSeeder(MongoDbContext context)
        {
            _reviews = context.SubmissionReviews;
        }

        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            var count = await _reviews.CountDocumentsAsync(FilterDefinition<SubmissionReview>.Empty, cancellationToken: cancellationToken);
            if (count >= 18) return;
            if (count > 0)
                await _reviews.DeleteManyAsync(FilterDefinition<SubmissionReview>.Empty, cancellationToken: cancellationToken);

            var taskTesting   = Guid.Parse("b3e52d67-aef5-443e-8023-eaa42ddc2b3a");
            var taskSensor    = Guid.Parse("ec6ddafc-3424-4131-b14c-dbb2a8c69327");
            var taskBandwidth = Guid.Parse("32e261e0-c061-4d36-967c-52e3b7fa17a6");
            var taskSystem    = Guid.Parse("f0b25a52-0a98-468d-aa5c-2ea93f4f5667");
            var taskProgram   = Guid.Parse("dd6d50e5-4b28-4e69-8ab1-9dc621a347d5");
            var taskBus       = Guid.Parse("2ff5acd4-5a25-453a-86ae-52c342c9e7e7");

            var fakeData = new List<SubmissionReview>
            {
                // Testing task submissions
                new SubmissionReview(Guid.NewGuid(), taskTesting,
                    new UserInformation(Guid.NewGuid(), "Ivan", "Mentor"),
                    "Good attempt! The async/await usage is mostly correct but you missed error handling in the database call. Consider extracting the retry logic into a separate method for readability."),

                new SubmissionReview(Guid.NewGuid(), taskTesting,
                    new UserInformation(Guid.NewGuid(), "Olena", "Coach"),
                    "The solution works but has a memory leak — you're not disposing the HttpClient properly. Use IHttpClientFactory instead. Fix that and resubmit for final approval."),

                new SubmissionReview(Guid.NewGuid(), taskTesting,
                    new UserInformation(Guid.NewGuid(), "Ivan", "Mentor"),
                    "Nice clean code! Separation of concerns is well done. The only thing missing is unit tests for the edge cases. Overall great work — approved!"),

                // Sensor task submissions
                new SubmissionReview(Guid.NewGuid(), taskSensor,
                    new UserInformation(Guid.NewGuid(), "Olena", "Coach"),
                    "Your sensor parsing handles the happy path well but breaks on null payloads. Add a null guard at the entry point and it will be solid. Good structure overall."),

                new SubmissionReview(Guid.NewGuid(), taskSensor,
                    new UserInformation(Guid.NewGuid(), "Ivan", "Mentor"),
                    "The debouncing logic is clever but has a race condition under high frequency input. Consider using a mutex or channel-based approach. Resubmit after fixing."),

                new SubmissionReview(Guid.NewGuid(), taskSensor,
                    new UserInformation(Guid.NewGuid(), "Olena", "Coach"),
                    "Excellent work! The sensor calibration implementation exceeded expectations. The code is clean, well-commented, and handles all edge cases. Approved with commendation."),

                // Bandwidth task submissions
                new SubmissionReview(Guid.NewGuid(), taskBandwidth,
                    new UserInformation(Guid.NewGuid(), "Ivan", "Mentor"),
                    "The throttling algorithm is correct but inefficient — O(n²) where O(n log n) is achievable. For production use this would be a bottleneck. Refactor the sort and resubmit."),

                new SubmissionReview(Guid.NewGuid(), taskBandwidth,
                    new UserInformation(Guid.NewGuid(), "Olena", "Coach"),
                    "Good bandwidth simulation. The packet loss handling is realistic. I'd recommend adding configurable retry limits but for the task scope this is solid work. Approved."),

                new SubmissionReview(Guid.NewGuid(), taskBandwidth,
                    new UserInformation(Guid.NewGuid(), "Ivan", "Mentor"),
                    "The implementation covers all required scenarios. The test coverage is thorough and the code is readable. Minor style issues but nothing blocking. Great submission."),

                // System task submissions
                new SubmissionReview(Guid.NewGuid(), taskSystem,
                    new UserInformation(Guid.NewGuid(), "Olena", "Coach"),
                    "The message queue implementation is sound but you're not handling backpressure. Under load the queue will grow unbounded. Add a capacity limit and rejection policy."),

                new SubmissionReview(Guid.NewGuid(), taskSystem,
                    new UserInformation(Guid.NewGuid(), "Ivan", "Mentor"),
                    "Impressive distributed system design! Fault tolerance is well thought out. The retry logic with exponential backoff is exactly what I'd expect in a production system. Approved!"),

                new SubmissionReview(Guid.NewGuid(), taskSystem,
                    new UserInformation(Guid.NewGuid(), "Olena", "Coach"),
                    "Good effort but the consensus algorithm has a split-brain edge case. Think about what happens when exactly half the nodes fail simultaneously. Fix and resubmit."),

                // Program parsing submissions
                new SubmissionReview(Guid.NewGuid(), taskProgram,
                    new UserInformation(Guid.NewGuid(), "Ivan", "Mentor"),
                    "The lexer is correct but the parser doesn't handle left-recursive grammars. This will cause infinite loops on certain inputs. Refactor to use Pratt parsing or precedence climbing."),

                new SubmissionReview(Guid.NewGuid(), taskProgram,
                    new UserInformation(Guid.NewGuid(), "Olena", "Coach"),
                    "Excellent parser implementation! The AST representation is clean and the error messages are informative. This is exactly the level of quality we expect. Fully approved."),

                // Bus quantification submissions
                new SubmissionReview(Guid.NewGuid(), taskBus,
                    new UserInformation(Guid.NewGuid(), "Ivan", "Mentor"),
                    "The bus utilization metrics are mostly correct but you're double-counting overlapping time windows. Review the interval merge logic and resubmit with corrected calculations."),

                new SubmissionReview(Guid.NewGuid(), taskBus,
                    new UserInformation(Guid.NewGuid(), "Olena", "Coach"),
                    "Great work on the quantification model! The statistical analysis is well done and the visualization of bus load patterns is a nice touch beyond the requirements. Approved!"),

                new SubmissionReview(Guid.NewGuid(), taskBus,
                    new UserInformation(Guid.NewGuid(), "Ivan", "Mentor"),
                    "Solid bus protocol implementation. The CAN frame encoding is accurate and the timing analysis correctly identifies the bottlenecks. Well structured and easy to follow."),

                new SubmissionReview(Guid.NewGuid(), taskBus,
                    new UserInformation(Guid.NewGuid(), "Olena", "Coach"),
                    "The quantification approach is sound but the report formatting is hard to read. Add proper headers and units to the output tables. Logic is correct — just clean up the presentation."),
            };

            await _reviews.InsertManyAsync(fakeData, cancellationToken: cancellationToken);
        }
    }
}
