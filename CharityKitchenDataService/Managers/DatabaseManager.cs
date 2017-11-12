using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;

namespace CharityKitchenDataService.Managers
{
    public static class DatabaseManager
    {
        static string CONNECTION_STRING;

        static DatabaseManager()
        {
            string path = HttpContext.Current.Server.MapPath("~/App_Data/charity_kitchen.accdb");
            string conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}";
            CONNECTION_STRING = string.Format(conn, path);
        }

        public static OleDbConnection GetOpenedConnection()
        {
            OleDbConnection conn = new OleDbConnection(CONNECTION_STRING);
            conn.Open();
            return conn;
        }

        public static int ExecuteNonQuery(string query)
        {
            OleDbConnection conn = GetOpenedConnection();
            OleDbCommand cmd = new OleDbCommand(query, conn);
            int rowsAffected = cmd.ExecuteNonQuery();
            conn.Close();
            return rowsAffected;
        }

        public static object ExecuteScalar(string query)
        {
            OleDbConnection conn = GetOpenedConnection();
            OleDbCommand cmd = new OleDbCommand(query, conn);
            object data = cmd.ExecuteScalar();
            conn.Close();
            return data;
        }
    }
}