using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using CharityKitchenDataService.Models;

namespace CharityKitchenDataService.Managers
{
    public static class OrderManager
    {
        #region fields and queries

        const string FIELD_ID = "OrderID";
        const string FIELD_CUSTOMER = "OrderCustomer";
        const string FIELD_DATE = "OrderDate";

        //Not in database, used to display total order price only
        const string FIELD_PRICE = "Price";

        const string QUERY_GET_ALL = "SELECT * FROM tblOrders";
        const string QUERY_GET_ONE = "SELECT * FROM tblOrders WHERE OrderID = {0}";
        const string QUERY_INSERT = "INSERT INTO tblOrders (OrderCustomer, OrderDate) VALUES ('{0}', '{1}')";
        const string QUERY_UPDATE = "UPDATE tblOrders SET OrderCustomer = '{0}', OrderDate = '{1}' WHERE OrderID = {2}";
        const string QUERY_GET_ALL_TO_DISPLAY = "SELECT * FROM qryOrderTotalDisplay";

        #endregion fields and queries

        #region data methods

        private static Order ReaderToOrder(OleDbDataReader reader)
        {
            Order order = new Order();
            order.OrderID = (int)reader[FIELD_ID];
            order.OrderCustomer = (string)reader[FIELD_CUSTOMER];
            order.OrderDate = (DateTime)reader[FIELD_DATE];
            return order;
        }

        private static OrderDisplay ReaderToOrderDisplay(OleDbDataReader reader)
        {
            OrderDisplay order = new OrderDisplay();
            order.OrderID = (int)reader[FIELD_ID];
            order.OrderCustomer = (string)reader[FIELD_CUSTOMER];
            order.OrderDate = (DateTime)reader[FIELD_DATE];
            try
            {
                order.Price = (decimal)reader[FIELD_PRICE];
            }
            catch
            {
                //order has no price yet (because no meals)
                order.Price = 0m;
            }
            return order;
        }

        public static List<Order> GetAllOrders()
        {
            OleDbConnection conn = DatabaseManager.GetOpenedConnection();
            OleDbCommand cmd = new OleDbCommand(QUERY_GET_ALL, conn);
            OleDbDataReader reader = cmd.ExecuteReader();

            List<Order> listOfOrders = new List<Order>();

            while (reader.Read())
            {
                listOfOrders.Add(ReaderToOrder(reader));
            }
            reader.Close();
            conn.Close();
            return listOfOrders;
        }

        public static Order GetOneOrder(int id)
        {
            string query = string.Format(QUERY_GET_ONE, id);
            OleDbConnection conn = DatabaseManager.GetOpenedConnection();
            OleDbCommand cmd = new OleDbCommand(query, conn);
            OleDbDataReader reader = cmd.ExecuteReader();
            reader.Read();

            Order oneOrder = ReaderToOrder(reader);
            reader.Close();
            conn.Close();
            return oneOrder;
        }

        public static int InsertNewOrder(Order order)
        {
            string query = string.Format(QUERY_INSERT, order.OrderCustomer, order.OrderDate);
            int results = DatabaseManager.ExecuteNonQuery(query);
            return results;
        }

        public static int UpdateExistingOrder(Order order)
        {
            string query = string.Format(QUERY_UPDATE, order.OrderCustomer, order.OrderDate, order.OrderID);
            int results = DatabaseManager.ExecuteNonQuery(query);
            return results;
        }

        public static List<OrderDisplay> GetOrdersToDisplay()
        {
            OleDbConnection conn = DatabaseManager.GetOpenedConnection();
            OleDbCommand cmd = new OleDbCommand(QUERY_GET_ALL_TO_DISPLAY, conn);
            OleDbDataReader reader = cmd.ExecuteReader();

            List<OrderDisplay> listOfOrders = new List<OrderDisplay>();

            while (reader.Read())
            {
                listOfOrders.Add(ReaderToOrderDisplay(reader));
            }
            reader.Close();
            conn.Close();
            return listOfOrders;
        }

        #endregion data methods
    }
}