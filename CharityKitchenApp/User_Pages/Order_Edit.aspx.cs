using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CharityKitchenApp.CharityKitchenDataServiceProxy;

namespace CharityKitchenApp.User_Pages
{
    public partial class Order_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //get ID from URL and set label text to it
            //create new service instance
            //set the grid view to show the meals in an order
            string idFromQueryString = Request.QueryString["id"];
            lblOrderID.Text = idFromQueryString;
            var service = new CharityKitchenDataServiceSoapClient();
            gvMealList.DataSource = service.GetMealsForOrder(int.Parse(lblOrderID.Text));
            gvMealList.DataBind();

            //if page is not a post back
            if (!Page.IsPostBack)
            {
                //get an order based on the ID passed and set the text fields accordingly
                //set the drop down list to the list of meals
                Order order = service.GetOneOrder(int.Parse(lblOrderID.Text));
                txtCustomerName.Text = order.OrderCustomer;
                txtOrderDate.Text = order.OrderDate.ToShortDateString();

                ddlMeals.DataSource = service.GetAllMeals();
                ddlMeals.DataTextField = "MealName";
                ddlMeals.DataValueField = "MealID";
                ddlMeals.DataBind();
            }
            //set total cost of Order
            SetTotal();
        }

        protected void gvMealList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //create new insance of service
            //get id from selected meal in the order
            //try to delete the meal from the order and let the user know of any changes
            var service = new CharityKitchenDataServiceSoapClient();
            string idFromGridview = gvMealList.SelectedRow.Cells[0].Text;
            int id = int.Parse(idFromGridview);
            int rowsAffected = service.DeleteOrderMeal(id);
            if (rowsAffected == 0)
            {
                lblMealSuccessful.Text = string.Empty;
                lblMealError.Text = "Error deleting meal from order..";
            }
            else
            {
                lblMealSuccessful.Text = "Meal deleted from order successfully.";
                lblMealError.Text = string.Empty;
            }
            //rebind the datasource and set the total order cost
            gvMealList.DataSource = null;
            gvMealList.DataSource = service.GetMealsForOrder(int.Parse(lblOrderID.Text));
            gvMealList.DataBind();

            SetTotal();
        }

        protected void btnAddMeal_Click(object sender, EventArgs e)
        {
            //create new service instance
            //create new meal equal to the meal selected in the drop down list
            //get total for all the meal ingredients
            var service = new CharityKitchenDataServiceSoapClient();
            Meal mealToAdd = service.GetOneMeal(int.Parse(ddlMeals.SelectedValue));
            decimal mealTotal = service.GetTotalForMealIngredients(int.Parse(ddlMeals.SelectedValue));
            
            //if the text fields are not null or empty and the quantity is numeric:
            //insert the meal on to the order and let the user know of any changes
            int rowsAffected = 0;
            if (!string.IsNullOrEmpty(txtQuantity.Text) && service.IsNumeric(txtQuantity.Text))
            {
                rowsAffected = service.InsertOrderMeal(int.Parse(lblOrderID.Text), mealToAdd.MealID, int.Parse(txtQuantity.Text), mealTotal);
            }
            if (rowsAffected == 0)
            {
                lblMealSuccessful.Text = string.Empty;
                lblMealError.Text = "Error adding meal to order..";
            }
            else
            {
                lblMealSuccessful.Text = "Meal added to order successfully.";
                lblMealError.Text = string.Empty;
            }
            //empty the textfields and rebind the datasources
            //set the order total cost
            txtQuantity.Text = string.Empty;

            gvMealList.DataSource = null;
            gvMealList.DataSource = service.GetMealsForOrder(int.Parse(lblOrderID.Text));
            gvMealList.DataBind();

            SetTotal();
        }

        protected void btnSaveOrder_Click(object sender, EventArgs e)
        {
            //check the date entered is valid
            //if the data entered is valid and the customer name is not null or empty
            dateValidator.Validate();
            if (dateValidator.IsValid && !string.IsNullOrEmpty(txtCustomerName.Text))
            {
                //create new service instance
                //create new order and set the values equal to what is inside the text fields
                //update the existing order and let the user know of any changes
                var service = new CharityKitchenDataServiceSoapClient();
                Order orderToUpdate = new Order();
                orderToUpdate.OrderID = int.Parse(lblOrderID.Text);
                orderToUpdate.OrderCustomer = txtCustomerName.Text;
                orderToUpdate.OrderDate = DateTime.Parse(txtOrderDate.Text);
                int rowsAffected = service.UpdateExistingOrder(orderToUpdate);
                if (rowsAffected == 0)
                {
                    lblChangesSaved.Text = string.Empty;
                    lblErrorSaving.Text = "Error saving changes..";
                }
                else
                {
                    lblChangesSaved.Text = "Changes saved successfully.";
                    lblErrorSaving.Text = string.Empty;
                }
            }
            else
            {
                //do not update order
            }
        }

        private void SetTotal()
        {
            //create new service instance
            //set total cost of order through calculation done in database
            var service = new CharityKitchenDataServiceSoapClient();
            lblTotal.Text = service.GetTotalForOrderMeals(int.Parse(lblOrderID.Text)).ToString("C");
        }
    }
}