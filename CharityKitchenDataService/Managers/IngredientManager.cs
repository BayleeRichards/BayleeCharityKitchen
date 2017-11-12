using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using CharityKitchenDataService.Models;

namespace CharityKitchenDataService.Managers
{
    public static class IngredientManager
    {
        #region fields and queries

        const string FIELD_ID = "IngredientID";
        const string FIELD_NAME = "IngredientName";
        const string FIELD_PRICE = "IngredientPrice";

        const string QUERY_GET_ALL = "SELECT * FROM tblIngredients";
        const string QUERY_GET_ONE = "SELECT * FROM tblIngredients WHERE IngredientID = {0}";
        const string QUERY_INSERT = "INSERT INTO tblIngredients (IngredientName, IngredientPrice) VALUES ('{0}', {1})";
        const string QUERY_UPDATE = "UPDATE tblIngredients SET IngredientName = '{0}', IngredientPrice = {1} WHERE IngredientID = {2}";

        #endregion fields and queries

        #region data methods

        private static Ingredient ReaderToIngredient(OleDbDataReader reader)
        {
            Ingredient ingredient = new Ingredient();
            ingredient.IngredientID = (int)reader[FIELD_ID];
            ingredient.IngredientName = (string)reader[FIELD_NAME];
            ingredient.IngredientPrice = (decimal)reader[FIELD_PRICE];
            return ingredient;
        }

        public static List<Ingredient> GetAllIngredients()
        {
            OleDbConnection conn = DatabaseManager.GetOpenedConnection();
            OleDbCommand cmd = new OleDbCommand(QUERY_GET_ALL, conn);
            OleDbDataReader reader = cmd.ExecuteReader();

            List<Ingredient> ingredients = new List<Ingredient>();

            while (reader.Read())
            {
                ingredients.Add(ReaderToIngredient(reader));
            }
            reader.Close();
            conn.Close();

            return ingredients;
        }

        public static Ingredient GetOneIngredient(int id)
        {
            string query = string.Format(QUERY_GET_ONE, id);
            OleDbConnection conn = DatabaseManager.GetOpenedConnection();
            OleDbCommand cmd = new OleDbCommand(query, conn);
            OleDbDataReader reader = cmd.ExecuteReader();
            reader.Read();

            Ingredient ingredient = ReaderToIngredient(reader);
            reader.Close();
            conn.Close();
            return ingredient;
        }

        public static int InsertNewIngredient(Ingredient ingredient)
        {
            string query = string.Format(QUERY_INSERT, ingredient.IngredientName, ingredient.IngredientPrice, ingredient.IngredientID);
            int results = DatabaseManager.ExecuteNonQuery(query);
            return results;
        }

        public static int UpdateExistingIngredient(Ingredient ingredient)
        {
            string query = string.Format(QUERY_UPDATE, ingredient.IngredientName, ingredient.IngredientPrice, ingredient.IngredientID);
            int results = DatabaseManager.ExecuteNonQuery(query);
            return results;
        }

        #endregion data methods
    }
}