using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using CharityKitchenDataService.Models;

namespace CharityKitchenDataService.Managers
{
    public static class MealManager
    {
        #region fields and queries

        const string FIELD_ID = "MealID";
        const string FIELD_NAME = "MealName";
        const string FIELD_DESC = "MealDescription";

        //Not in database, used to display total meal price only
        const string FIELD_PRICE = "Price";

        const string QUERY_GET_ALL = "SELECT * FROM tblMeals";
        const string QUERY_GET_ONE = "SELECT * FROM tblMeals WHERE MealID = {0}";
        const string QUERY_INSERT = "INSERT INTO tblMeals (MealName, MealDescription) VALUES ('{0}', '{1}')";
        const string QUERY_UPDATE = "UPDATE tblMeals SET MealName = '{0}', MealDescription = '{1}' WHERE MealID = {2}";
        const string QUERY_GET_ALL_TO_DISPLAY = "SELECT * FROM qryMealTotalDisplay";

        #endregion fields and queries

        #region data methods

        private static Meal ReaderToMeal(OleDbDataReader reader)
        {
            Meal meal = new Meal();
            meal.MealID = (int)reader[FIELD_ID];
            meal.MealName = (string)reader[FIELD_NAME];
            meal.MealDescription = (string)reader[FIELD_DESC];
            return meal;
        }

        private static MealDisplay ReaderToMealDisplay(OleDbDataReader reader)
        {
            MealDisplay meal = new MealDisplay();
            meal.MealID = (int)reader[FIELD_ID];
            meal.MealName = (string)reader[FIELD_NAME];
            try
            {
                meal.Price = (decimal)reader[FIELD_PRICE];
            }
            catch
            {
                // meal has no price yet (because no ingredients)
                meal.Price = 0m;
            }
            return meal;
        }

        public static List<Meal> GetAllMeals()
        {
            OleDbConnection conn = DatabaseManager.GetOpenedConnection();
            OleDbCommand cmd = new OleDbCommand(QUERY_GET_ALL, conn);
            OleDbDataReader reader = cmd.ExecuteReader();

            List<Meal> listOfMeals = new List<Meal>();

            while (reader.Read())
            {
                listOfMeals.Add(ReaderToMeal(reader));
            }
            reader.Close();
            conn.Close();
            return listOfMeals;
        }

        public static Meal GetOneMeal(int id)
        {
            string query = string.Format(QUERY_GET_ONE, id);
            OleDbConnection conn = DatabaseManager.GetOpenedConnection();
            OleDbCommand cmd = new OleDbCommand(query, conn);
            OleDbDataReader reader = cmd.ExecuteReader();
            reader.Read();

            Meal oneMeal = ReaderToMeal(reader);
            reader.Close();
            conn.Close();
            return oneMeal;
        }

        public static int InsertNewMeal(Meal meal)
        {
            string query = string.Format(QUERY_INSERT, meal.MealName, meal.MealDescription);
            int results = DatabaseManager.ExecuteNonQuery(query);
            return results;
        }

        public static int UpdateExistingMeal(Meal meal)
        {
            string query = string.Format(QUERY_UPDATE, meal.MealName, meal.MealDescription, meal.MealID);
            int results = DatabaseManager.ExecuteNonQuery(query);
            return results;
        }

        public static List<MealDisplay> GetMealsToDisplay()
        {
            OleDbConnection conn = DatabaseManager.GetOpenedConnection();
            OleDbCommand cmd = new OleDbCommand(QUERY_GET_ALL_TO_DISPLAY, conn);
            OleDbDataReader reader = cmd.ExecuteReader();

            List<MealDisplay> listOfMeals = new List<MealDisplay>();

            while (reader.Read())
            {
                listOfMeals.Add(ReaderToMealDisplay(reader));
            }
            reader.Close();
            conn.Close();
            return listOfMeals;
        }

        #endregion data methods
    }
}