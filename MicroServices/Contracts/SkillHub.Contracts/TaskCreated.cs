using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillHub.Contracts
{
	public record TaskCreated
	{
		public Guid TaskId { get; init; }
		public string TaskName { get; init; } = string.Empty;
	}
}
