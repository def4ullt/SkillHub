using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Entities;

namespace DAL.Repositories.Interfaces
{
	public interface IUserXpRepository : IGenericRepository<UserXp>
	{
		Task<IEnumerable<UserXpHistoryEntry>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
		Task<int> GetTotalXpByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
		Task<IEnumerable<DAL.Repositories.UserXpLeaderboardEntry>> GetLeaderboardAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
		Task<int> GetLeaderboardCountAsync(CancellationToken cancellationToken = default);
		Task<IEnumerable<DAL.Repositories.UserXpLeaderboardEntry>> GetAllUsersWithXpAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
		Task<int> GetAllUsersCountAsync(CancellationToken cancellationToken = default);
		Task RenameUserAsync(Guid userId, string firstName, string lastName, CancellationToken cancellationToken = default);
	}
}
