using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DbConn;
using DAL.Repositories.Interfaces;
using Dapper;
using Domain.Entities;

namespace DAL.Repositories
{
    public class WorkSubmissionStatusRepository : GenericRepository<WorkSubmissionStatus>, IWorkSubmissionStatusRepository
    {
        public WorkSubmissionStatusRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory, "work_submission_statuses")
        {

        }

        public async Task<WorkSubmissionStatus?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            using IDbConnection conn = await GetOpenConnectionAsync();
            string sql = $"SELECT * FROM {tableName} WHERE name = @Name LIMIT 1";

            IEnumerable<WorkSubmissionStatus> result = await conn.QueryAsync<WorkSubmissionStatus>(
                new CommandDefinition(
                    sql,
                    new { Name = name },
                    cancellationToken: cancellationToken
                )
            );

            return result.FirstOrDefault();
        }

        public async Task<bool> IsNameUniqueAsync(string name, Guid? idToExclude = null, CancellationToken cancellationToken = default)
        {
            using var conn = await GetOpenConnectionAsync();
            string sql = idToExclude.HasValue
                ? $"SELECT COUNT(1) FROM {tableName} WHERE name = @Name AND id <> @Id"
                : $"SELECT COUNT(1) FROM {tableName} WHERE name = @Name";

            long count = await conn.ExecuteScalarAsync<int>(
                new CommandDefinition(
                    sql,
                    new { Name = name, Id = idToExclude },
                    cancellationToken: cancellationToken
                )
            );

            return count == 0;
        }
    }
}
