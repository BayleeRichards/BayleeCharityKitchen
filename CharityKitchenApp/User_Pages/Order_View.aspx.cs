using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CharityKitchenApp.CharityKitchenDataServiceProxy;

namespace CharityKitchenApp.User_Pages
{
    public partial class Order_View : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //get ID passed from URL and get an order from the service using that ID
            //populate the labels for viewing
            string idFromQueryString = Request.QueryString["id"];
            var service = new CharityKitchenDataServiceSoapClient();
            var order = service.GetOneOrder(int.Parse(idFromQueryString));
            lblOrderID.Text = order.OrderID.ToString();
            lblCustomerName.Text = order.OrderCustomer;
            lblOrderDate.Text = order.OrderDate.ToShortDateString();

            //set the list of ingredients by getting the list of ingredients for the current order
            gvIngredientList.DataSource = service.GetIngredientsForOrder(order.OrderID);
            gvIngredientList.DataBind();
        }
    }
}