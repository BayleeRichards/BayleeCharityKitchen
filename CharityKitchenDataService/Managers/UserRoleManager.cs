using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using CharityKitchenDataService.Models;

namespace CharityKitchenDataService.Managers
{
    public static class UserRoleManager
    {
        #region fields and queries

        //tblUserRoles
        const string UR_ID = "UserRoleID";
        const string UR_USER = "UserRole_UserID";
        const string UR_ROLE = "UserRole_RoleID";

        //qryUserRoles
        const string QRY_UR_ID = "UserRoleID";
        const string QRY_NAME = "UserName";
        const string QRY_ROLE = "RoleDescription";
        const string QRY_USER = "UserRole_UserID";

        const string QUERY_SELECT_ROLES_FOR_DISPLAY = "SELECT * FROM qryUserRoles";
        const string QUERY_SELECT_ROLES_FOR_USERS = "SELECT * FROM qryUserRoles WHERE UserRole_UserID = {0}";
        const string QUERY_INSERT_UR = "INSERT INTO tblUserRoles (UserRole_UserID, UserRole_RoleID) VALUES ({0}, {1})";
        const string QUERY_DELETE_ROLE_FROM_USER = "DELETE FROM tblUserRoles WHERE UserRoleID = {0}";

        #endregion fields and queries

        #region data methods

        //SELECT queries are usually to get data for displaying
        //displayed User Roles are to be derived from qryUserRoles

        //UPDATE/INSERT/DELETE queries are for modifying whats in the db
        //these queries will operate on tblUserRoles

        private static UserRole ReaderToUserRole(OleDbDataReader reader)
        {
            UserRole userRole = new UserRole();
            userRole.ID = (int)reader[QRY_UR_ID];
            userRole.Name = (string)reader[QRY_NAME];
            userRole.Role = (string)reader[QRY_ROLE];
            userRole.UserID = (int)reader[QRY_USER];
            return userRole;
        }

        //dont need ANOTHER ReaderToUserRole method as we arent modelleing tblUserRoles
        public static List<UserRole> GetRolesForDisplay()
        {
            OleDbConnection conn = DatabaseManager.GetOpenedConnection();
            OleDbCommand cmd = new OleDbCommand(QUERY_SELECT_ROLES_FOR_DISPLAY, conn);
            OleDbDataReader reader = cmd.ExecuteReader();

            List<UserRole> userRoles = new List<UserRole>();

            while (reader.Read())
            {
                userRoles.Add(ReaderToUserRole(reader));
            }
            reader.Close();
            conn.Close();
            return userRoles;
        }


        public static List<UserRole> GetRolesForUser(int userID)
        {
            string query = string.Format(QUERY_SELECT_ROLES_FOR_USERS, userID);
            OleDbConnection conn = DatabaseManager.GetOpenedConnection();
            OleDbCommand cmd = new OleDbCommand(query, conn);
            OleDbDataReader reader = cmd.ExecuteReader();

            List<UserRole> userRoles = new List<UserRole>();

            while (reader.Read())
            {
                userRoles.Add(ReaderToUserRole(reader));
            }
            reader.Close();
            conn.Close();
            return userRoles;
        }

        public static int InsertUserRole(int userID, int roleID)
        {
            string query = string.Format(QUERY_INSERT_UR, userID, roleID);
            int results = DatabaseManager.ExecuteNonQuery(query);
            return results;
        }

        public static int DeleteUserRole(int userRoleID)
        {
            string query = string.Format(QUERY_DELETE_ROLE_FROM_USER, userRoleID);
            int results = DatabaseManager.ExecuteNonQuery(query);
            return results;
        }

        #endregion data methods
    }
}