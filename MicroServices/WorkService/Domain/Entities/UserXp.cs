using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class UserXp
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public Guid TaskId { get; set; }
		public int XpAmount { get; set; }
		public DateTime EarnedAt { get; set; } = DateTime.UtcNow;
	}
}
