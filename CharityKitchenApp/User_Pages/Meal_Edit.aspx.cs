using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CharityKitchenApp.CharityKitchenDataServiceProxy;

namespace CharityKitchenApp.User_Pages
{
    public partial class Meal_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //get passed id from URL
            //set ID label text to the ID pulled
            //create new service instance
            //set the grid view data source to the ingredients in the meal selected
            string idFromQueryString = Request.QueryString["id"];
            lblMealID.Text = idFromQueryString;
            var service = new CharityKitchenDataServiceSoapClient();
            gvIngredientList.DataSource = service.GetIngredientsForMeal(int.Parse(lblMealID.Text));
            gvIngredientList.DataBind();

            //if page is not a post back:
            if (!Page.IsPostBack)
            {
                //get the meal that we are editing
                //populate the fields with the meal values
                //set the drop down list to contain all ingredients
                Meal meal = service.GetOneMeal(int.Parse(lblMealID.Text));
                txtMealName.Text = meal.MealName;
                txtMealDesc.Text = meal.MealDescription;

                ddlIngredients.DataSource = service.GetAllIngredients();
                ddlIngredients.DataTextField = "IngredientName";
                ddlIngredients.DataValueField = "IngredientID";
                ddlIngredients.DataBind();
            }
            SetTotal();
        }

        protected void btnSaveMeal_Click(object sender, EventArgs e)
        {
            //create new service instance
            //create new meal and pass it the text field values
            var service = new CharityKitchenDataServiceSoapClient();
            Meal mealToUpdate = new Meal();
            mealToUpdate.MealID = int.Parse(lblMealID.Text);
            mealToUpdate.MealName = txtMealName.Text;
            mealToUpdate.MealDescription = txtMealDesc.Text;

            //if text fields are not null or empty:
            //update existing meal and let the user know of any changes
            int rowsAffected = 0;
            if (!string.IsNullOrEmpty(txtMealName.Text) && !string.IsNullOrEmpty(txtMealDesc.Text))
            {
                rowsAffected = service.UpdateExistingMeal(mealToUpdate);
            }
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

        protected void ingredientList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //create new service instance
            //get ID of selected ingredient
            //try to remove the ingredient from the list and let the user know of any changes
            var service = new CharityKitchenDataServiceSoapClient();
            string idFromGridview = gvIngredientList.SelectedRow.Cells[0].Text;
            int id = int.Parse(idFromGridview);
            int rowsAffected = service.DeleteMealIngredient(id);
            if (rowsAffected == 0)
            {
                lblIngredientSuccessful.Text = string.Empty;
                lblIngredientError.Text = "Error deleting ingredient from meal..";
            }
            else
            {
                lblIngredientSuccessful.Text = "Ingredient deleted from meal successfully.";
                lblIngredientError.Text = string.Empty;
            }
            //reset the datasources
            //set the totals
            gvIngredientList.DataSource = null;
            gvIngredientList.DataSource = service.GetIngredientsForMeal(int.Parse(lblMealID.Text));
            gvIngredientList.DataBind();

            SetTotal();
        }

        protected void btnAddIngredient_Click(object sender, EventArgs e)
        {
            //create new service instance
            //create ingredient instance equal to the ingredient selected in the drop down list
            var service = new CharityKitchenDataServiceSoapClient();
            Ingredient ingredientToAdd = service.GetOneIngredient(int.Parse(ddlIngredients.SelectedValue));


            //if text fields are not null or empty:
            //insert an ingredient to a meal and let the user know if there were any changes
            int rowsAffected = 0;
            if (!string.IsNullOrEmpty(txtQuantity.Text) && service.IsNumeric(txtQuantity.Text))
            {
                rowsAffected = service.InsertMealIngredient(int.Parse(lblMealID.Text), ingredientToAdd.IngredientID, int.Parse(txtQuantity.Text), ingredientToAdd.IngredientPrice);
            }
            if (rowsAffected == 0)
            {
                lblIngredientSuccessful.Text = string.Empty;
                lblIngredientError.Text = "Error adding ingredient to meal..";
            }
            else
            {
                lblIngredientSuccessful.Text = "Ingredient added to meal successfully.";
                lblIngredientError.Text = string.Empty;
            }
            //reset text fields
            //reset the data sources and set the meal totals
            txtQuantity.Text = string.Empty;

            gvIngredientList.DataSource = null;
            gvIngredientList.DataSource = service.GetIngredientsForMeal(int.Parse(lblMealID.Text));
            gvIngredientList.DataBind();

            SetTotal();
        }

        private void SetTotal()
        {
            //create new service instance
            //set the total cost of the meal with a calculation in the database
            var service = new CharityKitchenDataServiceSoapClient();
            lblTotal.Text = service.GetTotalForMealIngredients(int.Parse(lblMealID.Text)).ToString("C");
        }
    }
}