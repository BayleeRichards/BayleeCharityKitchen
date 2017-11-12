using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Data.OleDb;
using System.Web.Helpers;

//using Sodium;
using CharityKitchenDataService.Models;

namespace CharityKitchenDataService.Managers
{
    public static class AuthenticationManager
    {
        private const string SALT = "plumbusgazorpazorp";

        public static bool IsAlphaNumeric(string text)
        {
            return Regex.IsMatch(text, "^[a-zA-Z0-9]+$");
            // only alphabetical: return Regex.IsMatch(text, "^[a-zA-Z]+$");
            // only numeric: return Regex.IsMatch(text, "^[0-9]+$");
        }

        public static bool IsAlphabetical(string text)
        {
            return Regex.IsMatch(text, "^[a-zA-Z]+$");
        }

        public static bool IsNumeric(string text)
        {
            return Regex.IsMatch(text, "^[0-9]+$");
        }

        public static bool ValidateCredentials(string username, string password)
        {
            bool returnValue = false;

            if(IsAlphaNumeric(username))
            {
                OleDbConnection conn = null;

                try
                {
                    string query = "SELECT COUNT(*) FROM tblUsers WHERE UserName = @username and UserPassword = @password";
                    conn = DatabaseManager.GetOpenedConnection();
                    OleDbCommand cmd = new OleDbCommand(query, conn);

                    OleDbParameter user = new OleDbParameter();
                    user.ParameterName = "@username";
                    user.Value = username.Trim();
                    cmd.Parameters.Add(user);

                    OleDbParameter pass = new OleDbParameter();
                    pass.ParameterName = "@password";
                    pass.Value = Crypto.SHA256(password + SALT);
                    //pass.Value = PasswordHash.ArgonHashString(password.Trim());
                    //pass.Value = CryptoHash.Sha256(password.Trim() + SALT);
                    cmd.Parameters.Add(pass);

                    int results = (int)cmd.ExecuteScalar();
                    if (results > 0)
                    {
                        returnValue = true;
                    }
                }
                catch (Exception ex)
                {
                    //log error
                }
                conn.Close();
            }
            else
            {
                //Log error - username is not alpha-numeric
            }
            return returnValue;
        }

        public static UserLogin ValidateLoginCredentials(string username, string password)
        {
            UserLogin loggedIn = new UserLogin();

            if (IsAlphaNumeric(username))
            {
                OleDbConnection conn = null;

                string query = "SELECT * FROM qryLogin Where UserName = @username AND UserPassword = @password";
                conn = DatabaseManager.GetOpenedConnection();
                OleDbCommand cmd = new OleDbCommand(query, conn);

                OleDbParameter user = new OleDbParameter();
                user.ParameterName = "@username";
                user.Value = username.Trim();
                cmd.Parameters.Add(user);

                OleDbParameter pass = new OleDbParameter();
                pass.ParameterName = "@password";
                pass.Value = Crypto.SHA256(password + SALT);
                cmd.Parameters.Add(pass);

                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    loggedIn.Roles = new List<string>();

                    while (reader.Read())
                    {
                        loggedIn.Username = (string)reader["UserName"];
                        loggedIn.Roles.Add((string)reader["RoleDescription"]);
                    }
                }
                reader.Close();
                conn.Close(); 
            }
            return loggedIn;
        }

        public static string HashPassword(string password)
        {
            //password = PasswordHash.ArgonHashString(password.Trim());

            //var newHashedPassword = CryptoHash.Sha256(password.Trim() + SALT);
            //Crypto.
            //return password;
            //return newHashedPassword.ToString();
            return Crypto.SHA256(password + SALT);
        }

        public static void CreateMasterUser()
        {
            #region MASTER CODE

            //USED ONCE FOR CREATING A MASTER ACCOUNT
            //var service = new CharityKitchenDataServiceSoapClient();
            //User userToCreate = new User();
            //userToCreate.UserName = "Master";
            //userToCreate.UserPassword = service.HashPassword("password");
            //service.InsertNewUser(userToCreate);

            User master = new User();
            master.UserName = "Master";
            master.UserPassword = HashPassword("password");
            UserManager.InsertNewUser(master);

            #endregion MASTER
        }

        public static UserLogin GetLoggedInUserByUsername(string username)
        {
            string query = "SELECT * FROM qryLogin Where UserName = @username";
            OleDbConnection conn = DatabaseManager.GetOpenedConnection();
            OleDbCommand cmd = new OleDbCommand(query, conn);

            OleDbParameter user = new OleDbParameter();
            user.ParameterName = "@username";
            user.Value = username.Trim();
            cmd.Parameters.Add(user);

            UserLogin loggedIn = new UserLogin();

            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                loggedIn.Roles = new List<string>();

                while (reader.Read())
                {
                    loggedIn.Username = (string)reader["UserName"];
                    loggedIn.Roles.Add((string)reader["RoleDescription"]);
                }
            }
            reader.Close();
            conn.Close();
            return loggedIn;
        }
    }
}