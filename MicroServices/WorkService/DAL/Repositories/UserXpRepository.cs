using System.Data;
using DAL.DbConn;
using DAL.Repositories.Interfaces;
using Dapper;
using Domain.Entities;

namespace DAL.Repositories
{
	public class UserXpRepository : GenericRepository<UserXp>, IUserXpRepository
	{
		public UserXpRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory, "user_xp")
		{
		}

		public async Task<IEnumerable<UserXpHistoryEntry>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
		{
			using IDbConnection conn = await GetOpenConnectionAsync();
			string sql = @"
				SELECT ux.id, ux.userid, ux.taskid, ux.xpamount, ux.earnedat,
					(SELECT ws.taskname FROM work_submissions ws WHERE ws.taskid = ux.taskid LIMIT 1) AS taskname
				FROM user_xp ux
				WHERE ux.userid = @UserId
				ORDER BY ux.earnedat DESC";

			return await conn.QueryAsync<UserXpHistoryEntry>(new CommandDefinition(
				sql,
				new { UserId = userId },
				cancellationToken: cancellationToken));
		}

		public async Task<int> GetTotalXpByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
		{
			using IDbConnection conn = await GetOpenConnectionAsync();
			string sql = "SELECT COALESCE(SUM(xpamount), 0) FROM user_xp WHERE userid = @UserId";

			return await conn.ExecuteScalarAsync<int>(new CommandDefinition(
				sql,
				new { UserId = userId },
				cancellationToken: cancellationToken));
		}

		public async Task<IEnumerable<UserXpLeaderboardEntry>> GetLeaderboardAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
		{
			using IDbConnection conn = await GetOpenConnectionAsync();
			string sql = @"
				SELECT
					ux.userid,
					COALESCE(SUM(ux.xpamount), 0) AS totalxp,
					MAX(ws.userfirstname) AS firstname,
					MAX(ws.userlastname) AS lastname
				FROM user_xp ux
				LEFT JOIN work_submissions ws ON ux.userid = ws.userid
				GROUP BY ux.userid
				ORDER BY totalxp DESC
				LIMIT @PageSize OFFSET @Offset";

			int offset = (pageNumber - 1) * pageSize;
			return await conn.QueryAsync<UserXpLeaderboardEntry>(new CommandDefinition(
				sql,
				new { PageSize = pageSize, Offset = offset },
				cancellationToken: cancellationToken));
		}

		public async Task<int> GetLeaderboardCountAsync(CancellationToken cancellationToken = default)
		{
			using IDbConnection conn = await GetOpenConnectionAsync();
			string sql = "SELECT COUNT(DISTINCT userid) FROM user_xp";
			return await conn.ExecuteScalarAsync<int>(new CommandDefinition(sql, cancellationToken: cancellationToken));
		}

		public async Task<IEnumerable<UserXpLeaderboardEntry>> GetAllUsersWithXpAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
		{
			using IDbConnection conn = await GetOpenConnectionAsync();
			string sql = @"
				SELECT
					ws.userid,
					MAX(ws.userfirstname) AS firstname,
					MAX(ws.userlastname) AS lastname,
					COALESCE(SUM(ux.xpamount), 0) AS totalxp
				FROM work_submissions ws
				LEFT JOIN user_xp ux ON ws.userid = ux.userid
				GROUP BY ws.userid
				ORDER BY totalxp DESC
				LIMIT @PageSize OFFSET @Offset";

			int offset = (pageNumber - 1) * pageSize;
			return await conn.QueryAsync<UserXpLeaderboardEntry>(new CommandDefinition(
				sql,
				new { PageSize = pageSize, Offset = offset },
				cancellationToken: cancellationToken));
		}

		public async Task<int> GetAllUsersCountAsync(CancellationToken cancellationToken = default)
		{
			using IDbConnection conn = await GetOpenConnectionAsync();
			string sql = "SELECT COUNT(DISTINCT userid) FROM work_submissions";
			return await conn.ExecuteScalarAsync<int>(new CommandDefinition(sql, cancellationToken: cancellationToken));
		}

		public async Task RenameUserAsync(Guid userId, string firstName, string lastName, CancellationToken cancellationToken = default)
		{
			using IDbConnection conn = await GetOpenConnectionAsync();
			string sql = @"
				UPDATE work_submissions
				SET userfirstname = @FirstName, userlastname = @LastName
				WHERE userid = @UserId";

			await conn.ExecuteAsync(new CommandDefinition(
				sql,
				new { UserId = userId, FirstName = firstName, LastName = lastName },
				cancellationToken: cancellationToken));
		}
	}
}