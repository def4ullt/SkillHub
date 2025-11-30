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
    public class WorkSubmissionFileRepository : GenericRepository<WorkSubmissionFile>, IWorkSubmissionFileRepository
    {
        public WorkSubmissionFileRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory, "work_submission_files")
        {

        }

        public async Task<List<WorkSubmissionFile>> GetBySubmissionIdAsync(Guid submissionId, CancellationToken cancellationToken = default)
        {
            using IDbConnection connection = await GetOpenConnectionAsync();
            string sql = $"SELECT * FROM {tableName} WHERE worksubmissionid = @SubmissionId";

            IEnumerable<WorkSubmissionFile> files = await connection.QueryAsync<WorkSubmissionFile>(
                new CommandDefinition(sql, new { SubmissionId = submissionId }, cancellationToken: cancellationToken)
            );

            return files.ToList();
        }
    }
}
