using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using CharityKitchenDataService.Models;

namespace CharityKitchenDataService.Managers
{
    public static class OrderIngredientManager
    {
        #region fields and queries

        //qryOrderIngredients
        const string QRY_ID = "IngredientID";
        const string QRY_NAME = "IngredientName";
        const string QRY_QUANTITY = "Quantity";
        const string QRY_ORDER = "OrderMeal_OrderID";

        const string QUERY_SELECT_INGREDIENTS_FOR_ORDER = "SELECT * FROM qryOrderIngredients WHERE OrderMeal_OrderID = {0}";

        #endregion fields and queries

        #region data methods

        private static OrderIngredient ReaderToOrderIngredient(OleDbDataReader reader)
        {
            OrderIngredient orderIngredient = new OrderIngredient();
            orderIngredient.ID = (int)reader[QRY_ID];
            orderIngredient.Name = (string)reader[QRY_NAME];
            orderIngredient.Quantity = Convert.ToInt32((double)reader[QRY_QUANTITY]);
            orderIngredient.OrderID = (int)reader[QRY_ORDER];
            return orderIngredient;
        }

        public static List<OrderIngredient> GetIngredientsForOrder(int orderID)
        {
            string query = string.Format(QUERY_SELECT_INGREDIENTS_FOR_ORDER, orderID);
            OleDbConnection conn = DatabaseManager.GetOpenedConnection();
            OleDbCommand cmd = new OleDbCommand(query, conn);
            OleDbDataReader reader = cmd.ExecuteReader();
            List<OrderIngredient> ingredients = new List<OrderIngredient>();

            while (reader.Read())
            {
                ingredients.Add(ReaderToOrderIngredient(reader));
            }
            reader.Close();
            conn.Close();
            return ingredients;
        }

        #endregion data methods
    }
}