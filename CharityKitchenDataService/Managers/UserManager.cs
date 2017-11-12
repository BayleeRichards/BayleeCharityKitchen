using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using CharityKitchenDataService.Models;
using Sodium;
using CharityKitchenDataService.Managers;

namespace CharityKitchenDataService.Managers
{
    public static class UserManager
    {
        #region fields and queries

        const string FIELD_ID = "UserID";
        const string FIELD_NAME = "UserName";
        const string FIELD_PASS = "UserPassword";

        const string QUERY_GET_ALL = "SELECT * FROM tblUsers";
        const string QUERY_GET_ONE = "SELECT * FROM tblUsers WHERE UserID = {0}";
        const string QUERY_GET_ONE_BY_USERNAME = "SELECT * FROM tblUsers WHERE UserName = '{0}'";
        const string QUERY_INSERT = "INSERT INTO tblUsers (UserName, UserPassword) VALUES ('{0}', '{1}')";
        const string QUERY_UPDATE = "UPDATE tblUsers SET UserName = '{0}', UserPassword = '{1}' WHERE UserID = {2}";

        #endregion fields and queries

        #region data methods

        private static User ReaderToUser(OleDbDataReader reader)
        {
            User user = new User();
            user.UserID = (int)reader[FIELD_ID];
            user.UserName = (string)reader[FIELD_NAME];
            user.UserPassword = (string)reader[FIELD_PASS];
            return user;
        }

        public static List<User> GetAllUsers()
        {
            OleDbConnection conn = DatabaseManager.GetOpenedConnection();
            OleDbCommand cmd = new OleDbCommand(QUERY_GET_ALL, conn);
            OleDbDataReader reader = cmd.ExecuteReader();

            List<User> users = new List<User>();

            while (reader.Read())
            {
                users.Add(ReaderToUser(reader));
            }
            reader.Close();
            conn.Close();
            return users;
        }

        public static User GetOneUser(int id)
        {
            string query = string.Format(QUERY_GET_ONE, id);
            OleDbConnection conn = DatabaseManager.GetOpenedConnection();
            OleDbCommand cmd = new OleDbCommand(query, conn);
            OleDbDataReader reader = cmd.ExecuteReader();
            reader.Read();

            User user = ReaderToUser(reader);
            reader.Close();
            conn.Close();
            return user;
        }

        public static User GetOneUserByUsername(string username)
        {
            string query = string.Format(QUERY_GET_ONE_BY_USERNAME, username);
            OleDbConnection conn = DatabaseManager.GetOpenedConnection();
            OleDbCommand cmd = new OleDbCommand(query, conn);
            OleDbDataReader reader = cmd.ExecuteReader();
            reader.Read();

            User user = ReaderToUser(reader);
            reader.Close();
            conn.Close();
            return user;
        }

        public static int InsertNewUser(User user)
        {
            //string hashedPassword = hash.ToString();
            //string query = string.Format(QUERY_INSERT, user.UserName, hash);
            //int results = DatabaseManager.ExecuteNonQuery(query);

            // converted into parameterised query: using regular style had issues because of OleDB
            string hashedPassword = AuthenticationManager.HashPassword(user.UserPassword);

            var conn = DatabaseManager.GetOpenedConnection();
            var query = "INSERT INTO tblUsers (UserName, UserPassword) VALUES (?, ?)";
            var cmd = new OleDbCommand(query, conn);
            cmd.Parameters.AddWithValue("?", user.UserName);
            //cmd.Parameters.AddWithValue("?", user.UserPassword);
            cmd.Parameters.AddWithValue("?", hashedPassword);

            int results = cmd.ExecuteNonQuery();
            return results;
        }

        public static int UpdateExistingUser(User user)
        {
            //string query = string.Format(QUERY_UPDATE, user.UserName, user.UserPassword, user.UserID);
            //int results = DatabaseManager.ExecuteNonQuery(query);
            //return results;
            string hashedPassword = AuthenticationManager.HashPassword(user.UserPassword);

            var conn = DatabaseManager.GetOpenedConnection();
            var query = "UPDATE tblUsers SET UserName = ?, UserPassword = ? WHERE UserID = ?";
            var cmd = new OleDbCommand(query, conn);
            cmd.Parameters.AddWithValue("?", user.UserName);
            cmd.Parameters.AddWithValue("?", hashedPassword);
            cmd.Parameters.AddWithValue("?", user.UserID);

            int results = cmd.ExecuteNonQuery();
            return results;
        }

        #endregion data methods
    }
}