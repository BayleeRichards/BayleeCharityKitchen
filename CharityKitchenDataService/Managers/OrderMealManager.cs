using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using CharityKitchenDataService.Models;

namespace CharityKitchenDataService.Managers
{
    public static class OrderMealManager
    {
        #region fields and queries

        //tblOrderMeals
        const string OM_ID = "OrderMealID";
        const string OM_ORDER = "OrderMeal_OrderID";
        const string OM_MEAL = "OrderMeal_MealID";
        const string OM_QUANTITY = "OrderMealQuantity";
        const string OM_PRICE = "OrderMealPrice";

        //qryOrderMeals
        const string QRY_OM_ID = "OrderMealID";
        const string QRY_NAME = "MealName";
        const string QRY_QUANTITY = "OrderMealQuantity";
        const string QRY_PRICE = "OrderMealPrice";
        const string QRY_SUBTOTAL = "Subtotal";
        const string QRY_ORDER = "OrderMeal_OrderID";

        const string QUERY_SELECT_MEALS_FOR_ORDER = "SELECT * FROM qryOrderMeals WHERE OrderMeal_OrderID = {0}";
        const string QUERY_INSERT_OM = "INSERT INTO tblOrderMeals (OrderMeal_OrderID, OrderMeal_MealID, OrderMealQuantity, OrderMealPrice) VALUES ({0}, {1}, {2}, {3})";
        const string QUERY_DELETE_MEAL_FROM_ORDER = "DELETE FROM tblOrderMeals WHERE OrderMealID = {0}";
        const string QUERY_GET_TOTAL = "SELECT * FROM qryOrderMealsTotal WHERE OrderID = {0}";

        #endregion fields and queries

        #region data methods

        //SELECT queries are usually to get data for displaying
        //displayed Order meals are to be derived from qryOrderMeals

        //UPDATE/INSERT/DELETE queries are for modifying whats in the db
        //these queries will operate on tblOrderMeals

        private static OrderMeal ReaderToOrderMeal(OleDbDataReader reader)
        {
            OrderMeal orderMeal = new OrderMeal();
            orderMeal.ID = (int)reader[QRY_OM_ID];
            orderMeal.Name = (string)reader[QRY_NAME];
            orderMeal.Quantity = (int)reader[QRY_QUANTITY];
            orderMeal.Price = (decimal)reader[QRY_PRICE];
            orderMeal.Subtotal = (decimal)reader[QRY_SUBTOTAL];
            orderMeal.OrderID = (int)reader[QRY_ORDER];
            return orderMeal;
        }

        //dont need ANOTHER ReaderToOrderMeal method as we arent modelleing tblOrderMeals

        public static List<OrderMeal> GetMealsForOrder(int orderID)
        {
            string query = string.Format(QUERY_SELECT_MEALS_FOR_ORDER, orderID);
            OleDbConnection conn = DatabaseManager.GetOpenedConnection();
            OleDbCommand cmd = new OleDbCommand(query, conn);
            OleDbDataReader reader = cmd.ExecuteReader();
            List<OrderMeal> meals = new List<OrderMeal>();

            while (reader.Read())
            {
                meals.Add(ReaderToOrderMeal(reader));
            }
            reader.Close();
            conn.Close();
            return meals;
        }

        public static int InsertOrderMeal(int orderID, int mealID, int quantity, decimal price)
        {
            string query = string.Format(QUERY_INSERT_OM, orderID, mealID, quantity, price);
            int results = DatabaseManager.ExecuteNonQuery(query);
            return results;
        }

        public static int DeleteOrderMeal(int orderMealID)
        {
            string query = string.Format(QUERY_DELETE_MEAL_FROM_ORDER, orderMealID);
            int results = DatabaseManager.ExecuteNonQuery(query);
            return results;
        }

        public static decimal GetTotalForOrder(int order)
        {
            string query = string.Format(QUERY_GET_TOTAL, order);
            return (decimal)DatabaseManager.ExecuteScalar(query);
        }

        #endregion data methods
    }
}