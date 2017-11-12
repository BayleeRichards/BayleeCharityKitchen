using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CharityKitchenApp.CharityKitchenDataServiceProxy;

namespace CharityKitchenApp.User_Pages
{
    public partial class Orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            //create a new service instance
            //bind the orders to display to the grid view
            var service = new CharityKitchenDataServiceSoapClient();
            gvOrderList.DataSource = service.GetOrdersToDisplay();
            gvOrderList.DataBind();
        }

        protected void gvOrderList_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void btnCreateNewOrder_Click(object sender, EventArgs e)
        {
            //validate the date format
            //if the date is valid and the customer name is not empty
            dateValidator.Validate();
            if (dateValidator.IsValid && !string.IsNullOrEmpty(txtCustomerName.Text))
            {
                //create a new service instance
                //create a new order and populate it with the text fields values
                //insert the new order and let the user know of any changes
                var service = new CharityKitchenDataServiceSoapClient();
                Order orderToCreate = new Order();
                orderToCreate.OrderCustomer = txtCustomerName.Text;
                orderToCreate.OrderDate = DateTime.Parse(txtOrderDate.Text);
                int rowsAffected = service.InsertNewOrder(orderToCreate);
                if (rowsAffected == 0)
                {
                    lblOrderSaved.Text = string.Empty;
                    lblErrorCreatingOrder.Text = "Error creating order..";
                }
                else
                {
                    lblOrderSaved.Text = "Order created successfully.";
                    lblErrorCreatingOrder.Text = string.Empty;
                }
                //empty the text fields
                //rebind the grid view datasource
                txtCustomerName.Text = string.Empty;
                txtOrderDate.Text = string.Empty;

                gvOrderList.DataSource = null;
                gvOrderList.DataSource = service.GetOrdersToDisplay();
                gvOrderList.DataBind();
            }
            else
            {
                //do not insert new order
            }
        }

        public void GridButtons_Command(object sender, CommandEventArgs e)
        {
            //if the button clicked was the edit button
            if (e.CommandName == "Edit")
            {
                //get the index of the row clicked
                //use the index to get the order ID
                //redirect the page and pass the order ID
                int index = Convert.ToInt32(e.CommandArgument);
                string idFromGridview = gvOrderList.Rows[index].Cells[0].Text;
                int id = int.Parse(idFromGridview);
                Response.Redirect("~/User_Pages/Order_Edit.aspx?id=" + id);
            }
            //if the button clicked was the view button
            if (e.CommandName == "View")
            {
                //get the index of the row clicked
                //use the index to get the order ID
                //redirect the page and pass the order ID
                int index = Convert.ToInt32(e.CommandArgument);
                string idFromGridview = gvOrderList.Rows[index].Cells[0].Text;
                int id = int.Parse(idFromGridview);
                Response.Redirect("~/User_Pages/Order_View.aspx?id=" + id);
            }
        }

        //enable button if user has appropriate roles
        protected bool AuthorizedToEdit()
        {
            //if the user has the ADMIN role, allow them to see the EDIT button
            var service = new CharityKitchenDataServiceSoapClient();
            UserLogin user = service.GetLoggedInUserByUsername(Context.User.Identity.Name);
            if (user.Roles.Contains("Admin"))
            {
                return true;
            }
            else
            {
                return false;
            } 
        }

        //enable button if user has appropriate roles
        protected bool AuthorizedToView()
        {
            //if the user has the WAREHOUSE role, allow them to see the VIEW button
            var service = new CharityKitchenDataServiceSoapClient();
            UserLogin user = service.GetLoggedInUserByUsername(Context.User.Identity.Name);
            if (user.Roles.Contains("Warehouse"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //http://www.johnchapman.net/technology/coding/asp-net-multiple-command-select-delete-etc-fields-in-a-gridview //reference
        //https://stackoverflow.com/questions/1461302/conditionally-hide-commandfield-or-buttonfield-in-gridview //reference
        //https://forums.asp.net/t/1791563.aspx?Input+string+was+not+in+a+correct+format+when+LinkButton+is+clicked+in+GridView //reference
    }
}