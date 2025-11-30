using System;
using System.Collections.Generic;
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
