using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace oracle_connectivity
{
    public class Database
    {
        public static IDbConnection makeConnection(string conn)
        {
            return new OracleConnection(conn);
        }
    }
}
