using MassTransit;
using SkillHub.Contracts;

namespace API.Consumers
{
	public class TaskCreatedConsumer : IConsumer<TaskCreated>
	{
		private readonly ILogger<TaskCreatedConsumer> logger;

		public TaskCreatedConsumer(ILogger<TaskCreatedConsumer> logger)
		{
			this.logger = logger;
		}

		public async Task Consume(ConsumeContext<TaskCreated> context)
		{
			logger.LogInformation("Task created: {TaskId} - {TaskName}",
				context.Message.TaskId,
				context.Message.TaskName);

			await Task.CompletedTask;
		}
	}
}