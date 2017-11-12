using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using CharityKitchenDataService.Models;

namespace CharityKitchenDataService.Managers
{
    public static class MealIngredientManager
    {
        #region fields and queries

        //tblMealIngredients
        const string MI_ID = "MealIngredientsID";
        const string MI_MEAL = "MealIngredients_MealID";
        const string MI_INGREDIENT = "MealIngredients_IngredientID";
        const string MI_QUANTITY = "MealIngredientsQuantity";
        const string MI_PRICE = "MealIngredientsPrice";

        //qryMealIngredients
        const string QRY_MI_ID = "MealIngredientsID";
        const string QRY_NAME = "IngredientName";
        const string QRY_QUANTITY = "MealIngredientsQuantity";
        const string QRY_PRICE = "MealIngredientsPrice";
        const string QRY_SUBTOTAL = "Subtotal";
        const string QRY_MEAL = "MealIngredients_MealID";

        const string QUERY_SELECT_INGREDIENTS_FOR_MEAL = "SELECT * FROM qryMealIngredients WHERE MealIngredients_MealID = {0}";
        const string QUERY_INSERT_MI = "INSERT INTO tblMealIngredients (MealIngredients_MealID, MealIngredients_IngredientID, MealIngredientsQuantity, MealIngredientsPrice) VALUES ({0}, {1}, {2}, {3})";
        const string QUERY_DELETE_INGREDIENT_FROM_MEAL = "DELETE FROM tblMealIngredients WHERE MealIngredientsID = {0}";
        const string QUERY_GET_TOTAL = "SELECT * FROM qryMealIngredientsTotal WHERE Meal = {0}";

        #endregion fields and queries

        #region data methods

        //SELECT queries are usually to get data for displaying
        //displayed Meal Ingredients are to be derived from qryMealIngredients

        //UPDATE/INSERT/DELETE queries are for modifying whats in the db
        //these queries will operate on tblMealIngredients

        private static MealIngredient ReaderToMealIngredient(OleDbDataReader reader)
        {
            MealIngredient mealIngredient = new MealIngredient();
            mealIngredient.ID = (int)reader[QRY_MI_ID];
            mealIngredient.Name = (string)reader[QRY_NAME];
            mealIngredient.Quantity = (int)reader[QRY_QUANTITY];
            mealIngredient.Price = (decimal)reader[QRY_PRICE];
            mealIngredient.Subtotal = (decimal)reader[QRY_SUBTOTAL];
            mealIngredient.MealID = (int)reader[QRY_MEAL];
            return mealIngredient;
        }

        //dont need ANOTHER ReaderToMealIngredient method as we arent modelleing tblMealIngredients

        public static List<MealIngredient> GetIngredientsForMeal(int mealID)
        {
            string query = string.Format(QUERY_SELECT_INGREDIENTS_FOR_MEAL, mealID);
            OleDbConnection conn = DatabaseManager.GetOpenedConnection();
            OleDbCommand cmd = new OleDbCommand(query, conn);
            OleDbDataReader reader = cmd.ExecuteReader();
            List<MealIngredient> ingredients = new List<MealIngredient>();

            while (reader.Read())
            {
                ingredients.Add(ReaderToMealIngredient(reader));
            }
            reader.Close();
            conn.Close();
            return ingredients;
        }

        public static int InsertMealIngredient(int mealID, int IngredientID, int quantity, decimal price)
        {
            string query = string.Format(QUERY_INSERT_MI, mealID, IngredientID, quantity, price);
            int results = DatabaseManager.ExecuteNonQuery(query);
            return results;
        }

        public static int DeleteMealIngredient(int mealIngredientID)
        {
            string query = string.Format(QUERY_DELETE_INGREDIENT_FROM_MEAL, mealIngredientID);
            int results = DatabaseManager.ExecuteNonQuery(query);
            return results;
        }

        public static decimal GetTotalForMeal(int meal)
        {
            string query =  string.Format(QUERY_GET_TOTAL, meal);
            return (decimal)DatabaseManager.ExecuteScalar(query);
        }

        #endregion data methods
    }
}