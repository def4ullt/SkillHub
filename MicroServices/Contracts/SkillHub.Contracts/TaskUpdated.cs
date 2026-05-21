namespace SkillHub.Contracts
{
	public record TaskUpdated
	{
		public Guid TaskId { get; init; }
		public string TaskName { get; init; } = string.Empty;
	}
}
