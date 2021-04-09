using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InventoryApp.DataService
{
    public class DBConnector
    {
        private SqlConnection sqlConnection;
        public SqlConnection GetConnection
        {
            get { return sqlConnection; }
            set { sqlConnection = value; }
        }

        public DBConnector()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);
        }
    }
}