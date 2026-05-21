using DAL.Repositories.Interfaces;
using MassTransit;
using SkillHub.Contracts;

namespace API.Consumers
{
	public class TaskUpdatedConsumer : IConsumer<TaskUpdated>
	{
		private readonly IWorkSubmissionRepository submissionRepository;
		private readonly ILogger<TaskUpdatedConsumer> logger;

		public TaskUpdatedConsumer(IWorkSubmissionRepository submissionRepository, ILogger<TaskUpdatedConsumer> logger)
		{
			this.submissionRepository = submissionRepository;
			this.logger = logger;
		}

		public async Task Consume(ConsumeContext<TaskUpdated> context)
		{
			var msg = context.Message;
			await submissionRepository.UpdateTaskNameAsync(msg.TaskId, msg.TaskName, context.CancellationToken);
			logger.LogInformation("Updated taskName to '{TaskName}' for taskId {TaskId}", msg.TaskName, msg.TaskId);
		}
	}
}
