using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DbConn;
using DAL.Repositories.Interfaces;

namespace DAL.Unit_of_work
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IDbConnectionFactory connectionFactory;
        private IDbConnection? connection;
        private IDbTransaction? transaction;

        public IWorkSubmissionRepository WorkSubmissions { get; }
        public IWorkSubmissionFileRepository WorkSubmissionFiles { get; }
        public IWorkSubmissionStatusRepository WorkSubmissionStatuses { get; }
        public ISubmissionDeliveryMethodRepository SubmissionDeliveryMethods { get; }

        public IDbConnection Connection => connection ??= connectionFactory.CreateConnection();
        public IDbTransaction? Transaction => transaction;

        public UnitOfWork(
            IDbConnectionFactory connectionFactory,
            IWorkSubmissionRepository workSubmissions,
            IWorkSubmissionFileRepository workSubmissionFiles,
            IWorkSubmissionStatusRepository workSubmissionStatuses,
            ISubmissionDeliveryMethodRepository submissionDeliveryMethods)
        {
            this.connectionFactory = connectionFactory;
            WorkSubmissions = workSubmissions;
            WorkSubmissionFiles = workSubmissionFiles;
            WorkSubmissionStatuses = workSubmissionStatuses;
            SubmissionDeliveryMethods = submissionDeliveryMethods;
        }

        public async Task BeginTransactionAsync()
        {
            if (connection == null) connection = connectionFactory.CreateConnection();
            if (connection.State != ConnectionState.Open) connection.Open();

            transaction = connection.BeginTransaction();
            await Task.CompletedTask;
        }

        public async Task CommitAsync()
        {
            try
            {
                transaction?.Commit();
            }
            catch
            {
                transaction?.Rollback();
                throw;
            }
            finally
            {
                transaction?.Dispose();
                transaction = null;
            }

            await Task.CompletedTask;
        }

        public async Task RollbackAsync()
        {
            transaction?.Rollback();
            transaction?.Dispose();
            transaction = null;

            await Task.CompletedTask;
        }

        public void Dispose()
        {
            transaction?.Dispose();
            connection?.Dispose();
        }
    }
}
