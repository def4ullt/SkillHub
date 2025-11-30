using System;
using System.Collections.Generic;
using System.Data;


namespace DAL.DbConn
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
