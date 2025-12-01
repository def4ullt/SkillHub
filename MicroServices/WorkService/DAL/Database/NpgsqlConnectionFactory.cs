using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DbConn;
using Npgsql;

namespace DAL.Database
{
    public class NpgsqlConnectionFactory : IDbConnectionFactory
    {
        private string connectionString;

        public NpgsqlConnectionFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
