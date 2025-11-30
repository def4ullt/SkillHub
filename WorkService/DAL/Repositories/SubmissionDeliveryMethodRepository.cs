using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using DAL.DbConn;
using DAL.Repositories.Interfaces;
using Domain.Entities;

namespace DAL.Repositories
{
    public class SubmissionDeliveryMethodRepository : ISubmissionDeliveryMethodRepository
    {
        private IDbConnectionFactory connectionFactory;
        private string tableName = "submission_delivery_methods";

        public SubmissionDeliveryMethodRepository(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        private async Task<IDbConnection> GetOpenConnectionAsync(CancellationToken cancellationToken = default)
        {
            IDbConnection connection = connectionFactory.CreateConnection();

            if (connection.State != ConnectionState.Open)
            {
                await Task.Run(() => connection.Open(), cancellationToken);
            }

            return connection;
        }

        public async Task<IEnumerable<SubmissionDeliveryMethod>> GetAllAsync(CancellationToken cancellationToken = default, IDbTransaction? transaction = null)
        {
            List<SubmissionDeliveryMethod> list = new List<SubmissionDeliveryMethod>();

            using IDbConnection connection = await GetOpenConnectionAsync(cancellationToken);
            using IDbCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT id, name, createdat FROM {tableName}";
            command.Transaction = transaction;

            using IDataReader reader = await Task.Run(() => command.ExecuteReader(), cancellationToken);
            while (reader.Read())
            {
                SubmissionDeliveryMethod method = new SubmissionDeliveryMethod
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1),
                    CreatedAt = reader.GetDateTime(2)
                };
                list.Add(method);
            }

            return list;
        }

        public async Task<SubmissionDeliveryMethod?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default, IDbTransaction? transaction = null)
        {
            using IDbConnection connection = await GetOpenConnectionAsync(cancellationToken);
            using IDbCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT id, name, createdat FROM {tableName} WHERE id = @Id";
            command.Transaction = transaction;

            IDbDataParameter param = command.CreateParameter();
            param.ParameterName = "@Id";
            param.Value = id;
            command.Parameters.Add(param);

            using IDataReader reader = await Task.Run(() => command.ExecuteReader(), cancellationToken);
            if (reader.Read())
            {
                return new SubmissionDeliveryMethod
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1),
                    CreatedAt = reader.GetDateTime(2)
                };
            }

            return null;
        }

        public async Task<Guid> AddAsync(SubmissionDeliveryMethod entity, CancellationToken cancellationToken = default, IDbTransaction? transaction = null)
        {
            using IDbConnection connection = await GetOpenConnectionAsync(cancellationToken);
            using IDbCommand command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = $"INSERT INTO {tableName} (name, createdat) VALUES (@Name, @CreatedAt) RETURNING id";

            IDbDataParameter nameParam = command.CreateParameter();
            nameParam.ParameterName = "@Name";
            nameParam.Value = entity.Name;
            command.Parameters.Add(nameParam);

            IDbDataParameter createdParam = command.CreateParameter();
            createdParam.ParameterName = "@CreatedAt";
            createdParam.Value = entity.CreatedAt;
            command.Parameters.Add(createdParam);

            object result = await Task.Run(() => command.ExecuteScalar(), cancellationToken);
            Guid insertedId = (Guid)result;
            entity.Id = insertedId;

            return insertedId;
        }

        public async Task<SubmissionDeliveryMethod?> UpdateAsync(SubmissionDeliveryMethod entity, CancellationToken cancellationToken = default, IDbTransaction? transaction = null)
        {
            using IDbConnection connection = await GetOpenConnectionAsync(cancellationToken);
            using IDbCommand command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = $"UPDATE {tableName} SET name = @Name, createdat = @CreatedAt WHERE id = @Id RETURNING id, name, createdat";

            IDbDataParameter idParam = command.CreateParameter();
            idParam.ParameterName = "@Id";
            idParam.Value = entity.Id;
            command.Parameters.Add(idParam);

            IDbDataParameter nameParam = command.CreateParameter();
            nameParam.ParameterName = "@Name";
            nameParam.Value = entity.Name;
            command.Parameters.Add(nameParam);

            IDbDataParameter createdParam = command.CreateParameter();
            createdParam.ParameterName = "@CreatedAt";
            createdParam.Value = entity.CreatedAt;
            command.Parameters.Add(createdParam);

            using IDataReader reader = await Task.Run(() => command.ExecuteReader(), cancellationToken);
            if (reader.Read())
            {
                return new SubmissionDeliveryMethod
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1),
                    CreatedAt = reader.GetDateTime(2)
                };
            }

            return null;
        }

        public async Task<int> DeleteAsync(Guid id, CancellationToken cancellationToken = default, IDbTransaction? transaction = null)
        {
            using IDbConnection connection = await GetOpenConnectionAsync(cancellationToken);
            using IDbCommand command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = $"DELETE FROM {tableName} WHERE id = @Id";

            IDbDataParameter param = command.CreateParameter();
            param.ParameterName = "@Id";
            param.Value = id;
            command.Parameters.Add(param);

            int affectedRows = await Task.Run(() => command.ExecuteNonQuery(), cancellationToken);
            return affectedRows;
        }

        public async Task<bool> IsNameUniqueAsync(string name, Guid? idToExclude = null, CancellationToken cancellationToken = default)
        {
            using IDbConnection connection = await GetOpenConnectionAsync(cancellationToken);
            using IDbCommand command = connection.CreateCommand();

            if (idToExclude.HasValue)
            {
                command.CommandText = $"SELECT COUNT(1) FROM {tableName} WHERE name = @Name AND id <> @Id";
                IDbDataParameter idParam = command.CreateParameter();
                idParam.ParameterName = "@Id";
                idParam.Value = idToExclude.Value;
                command.Parameters.Add(idParam);
            }
            else
            {
                command.CommandText = $"SELECT COUNT(1) FROM {tableName} WHERE name = @Name";
            }

            IDbDataParameter nameParam = command.CreateParameter();
            nameParam.ParameterName = "@Name";
            nameParam.Value = name;
            command.Parameters.Add(nameParam);

            object result = await Task.Run(() => command.ExecuteScalar(), cancellationToken);
            int count = Convert.ToInt32(result);

            return count == 0;
        }
    }
}