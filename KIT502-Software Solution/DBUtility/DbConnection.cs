using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace KIT502_Software_Solution.DBUtility
{
    internal class DbConnection
    {
        private static MySqlConnection? conn;

        private const string db = "group24";
        private const string user = "group24";
        private const string pass = "gpass";
        private const string server = "131.217.174.230";

        private static MySqlConnection GetConnection()
        {
            if (conn == null || (conn.State == System.Data.ConnectionState.Broken || 
                conn.State == System.Data.ConnectionState.Closed))
            {
                string connectionString = String.Format("Database={0};Data Source={1};User Id={2};Password={3}", db, server, user, pass);
                conn = new MySqlConnection(connectionString);
            }

            return conn;
        }
    }
}
