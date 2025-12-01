using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DbConn;
using DAL.Helpers;
using DAL.Repositories.Interfaces;
using Dapper;
using Domain.Entities;

namespace DAL.Repositories
{
    public class WorkSubmissionRepository : GenericRepository<WorkSubmission>, IWorkSubmissionRepository
    {
        public WorkSubmissionRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory, "work_submissions")
        {

        }

        public async Task<PagedList<WorkSubmission>> GetPagedAsync(WorkSubmissionQueryParams queryParams, CancellationToken cancellationToken = default)
        {
            using var conn = await GetOpenConnectionAsync();

            var filters = new List<string>();
            var parameters = new DynamicParameters();

            if (queryParams.TaskId.HasValue)
            {
                filters.Add("taskid = @TaskId");
                parameters.Add("TaskId", queryParams.TaskId.Value);
            }
            if (queryParams.UserId.HasValue)
            {
                filters.Add("userid = @UserId");
                parameters.Add("UserId", queryParams.UserId.Value);
            }
            if (queryParams.StatusId.HasValue)
            {
                filters.Add("statusid = @StatusId");
                parameters.Add("StatusId", queryParams.StatusId.Value);
            }

            string whereClause = filters.Any() ? "WHERE " + string.Join(" AND ", filters) : "";
            string orderClause = queryParams.SortDescending ? "ORDER BY submissiondate DESC" : "ORDER BY submissiondate ASC";

            string countSql = $"SELECT COUNT(*) FROM {tableName} {whereClause}";
            int totalCount = await conn.ExecuteScalarAsync<int>(new CommandDefinition(countSql, parameters, cancellationToken: cancellationToken));

            int offset = (queryParams.PageNumber - 1) * queryParams.PageSize;
            string dataSql = $@"
                SELECT * 
                FROM {tableName} 
                {whereClause} 
                {orderClause} 
                LIMIT @PageSize OFFSET @Offset";

            parameters.Add("PageSize", queryParams.PageSize);
            parameters.Add("Offset", offset);

            var items = (await conn.QueryAsync<WorkSubmission>(new CommandDefinition(dataSql, parameters, cancellationToken: cancellationToken))).ToList();

            return new PagedList<WorkSubmission>(items, totalCount, queryParams.PageNumber, queryParams.PageSize);
        }

        public async Task<WorkSubmissionDetail?> GetDetailAsync(Guid id, CancellationToken cancellationToken = default)
        {
            using var conn = await GetOpenConnectionAsync();

            string sqlSubmission = $@"
                SELECT 
                    ws.id,
                    ws.taskid,
                    ws.taskname,
                    ws.userid,
                    ws.userfirstname,
                    ws.userlastname,
                    ws.statusid,
                    ws.submissiondate,
                    wss.id,
                    wss.name,
                    wss.createdat
                FROM work_submissions ws
                INNER JOIN work_submission_statuses wss ON ws.statusid = wss.id
                WHERE ws.id = @Id";

            WorkSubmissionDetail? submission = null;

            var result = await conn.QueryAsync<WorkSubmissionDetail, WorkSubmissionStatus, WorkSubmissionDetail>(
                sqlSubmission,
                (workSub, status) =>
                {
                    workSub.WorkSubmissionStatus = status;
                    return workSub;
                },
                new { Id = id },
                splitOn: "id" 
            );

            submission = result.FirstOrDefault();

            if (submission == null) return null;

            string sqlFiles = @"
                SELECT 
                    wsf.id,
                    wsf.worksubmissionid,
                    wsf.deliverymethodid,
                    wsf.fileurl,
                    sdm.id,
                    sdm.name,
                    sdm.createdat
                FROM work_submission_files wsf
                INNER JOIN submission_delivery_methods sdm ON wsf.deliverymethodid = sdm.id
                WHERE wsf.worksubmissionid = @Id";

            var files = await conn.QueryAsync<WorkSubmissionFile, SubmissionDeliveryMethod, WorkSubmissionFile>(
                sqlFiles,
                (file, method) =>
                {
                    file.DeliveryMethod = method;
                    return file;
                },
                new { Id = id },
                splitOn: "id" 
            );

            submission.Files = files.ToList();

            return submission;
        }
    }
}
