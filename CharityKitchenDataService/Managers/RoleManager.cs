using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using CharityKitchenDataService.Models;

namespace CharityKitchenDataService.Managers
{
    public static class RoleManager
    {
        #region fields and queries

        const string FIELD_ID = "RoleID";
        const string FIELD_DESC = "RoleDescription";

        const string QUERY_GET_ALL = "SELECT * FROM tblRoles";
        const string QUERY_GET_ONE = "SELECT * FROM tblRoles WHERE RoleID = {0}";
        const string QUERY_INSERT = "INSERT INTO tblRoles (RoleDescription) VALUES ('{0}')";
        const string QUERY_UPDATE = "UPDATE tblRoles SET RoleDescription = '{0}' WHERE RoleID = {1}";

        #endregion fields and queries

        #region data methods

        private static Role ReaderToRole(OleDbDataReader reader)
        {
            Role role = new Role();
            role.RoleID = (int)reader[FIELD_ID];
            role.RoleDescription = (string)reader[FIELD_DESC];
            return role;
        }

        public static List<Role> GetAllRoles()
        {
            OleDbConnection conn = DatabaseManager.GetOpenedConnection();
            OleDbCommand cmd = new OleDbCommand(QUERY_GET_ALL, conn);
            OleDbDataReader reader = cmd.ExecuteReader();

            List<Role> roles = new List<Role>();

            while (reader.Read())
            {
                roles.Add(ReaderToRole(reader));
            }
            reader.Close();
            conn.Close();
            return roles;
        }

        public static Role GetOneRole(int id)
        {
            string query = string.Format(QUERY_GET_ONE, id);
            OleDbConnection conn = DatabaseManager.GetOpenedConnection();
            OleDbCommand cmd = new OleDbCommand(query, conn);
            OleDbDataReader reader = cmd.ExecuteReader();
            reader.Read();

            Role role = ReaderToRole(reader);
            reader.Close();
            conn.Close();
            return role;
        }

        public static int InsertNewRole(Role role)
        {
            string query = string.Format(QUERY_INSERT, role.RoleDescription);
            int results = DatabaseManager.ExecuteNonQuery(query);
            return results;
        }

        public static int UpdateExistingRole(Role role)
        {
            string query = string.Format(QUERY_UPDATE, role.RoleDescription, role.RoleID);
            int results = DatabaseManager.ExecuteNonQuery(query);
            return results;
        }

        #endregion data methods
    }
}