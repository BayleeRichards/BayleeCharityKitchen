using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CharityKitchenApp.CharityKitchenDataServiceProxy;
using System.Web.UI.HtmlControls;

namespace CharityKitchenApp.User_Pages
{
    public partial class Kitchen : System.Web.UI.Page
    {
        //tab to remember which page we are currently looking at
        public static string ACTIVETAB = "mealsTab";

        protected void Page_Load(object sender, EventArgs e)
        {
            //create new service instance
            //set meal grid view datasource to the meals to display
            //set ingredient grid view datasource to all the ingredients in the database
            var service = new CharityKitchenDataServiceSoapClient();
            gvMealList.DataSource = service.GetMealsToDisplay();
            gvMealList.DataBind();

            gvIngredientList.DataSource = service.GetAllIngredients();
            gvIngredientList.DataBind();
        }

        protected void gvList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if the meal-edit button is selected:
            //redirect the page and pass it the meal ID
            string idFromGridview = gvMealList.SelectedRow.Cells[0].Text;
            int id = int.Parse(idFromGridview);
            Response.Redirect("~/User_Pages/Meal_Edit.aspx?id=" + id);
        }

        protected void btnCreateNewMeal_Click(object sender, EventArgs e)
        {
            //create new service instance
            //create new meal instance and set its values to the textfield values
            var service = new CharityKitchenDataServiceSoapClient();
            Meal mealToCreate = new Meal();
            mealToCreate.MealName = txtMealName.Text;
            mealToCreate.MealDescription = txtMealDesc.Text;

            //if textboxes are not null or empty
            //insert new meal and show user if any changes have been made
            int rowsAffected = 0;
            if (!string.IsNullOrEmpty(txtMealName.Text) && !string.IsNullOrEmpty(txtMealDesc.Text))
            { 
                rowsAffected = service.InsertNewMeal(mealToCreate);
            }
            if (rowsAffected == 0)
            {
                lblMealSaved.Text = string.Empty;
                lblErrorCreatingMeal.Text = "Error creating meal..";
            }
            else
            {
                lblMealSaved.Text = "Meal created successfully.";
                lblErrorCreatingMeal.Text = string.Empty;
            }
            //empty the textfields and reset the datasources
            //set the active tab
            txtMealName.Text = string.Empty;
            txtMealDesc.Text = string.Empty;

            gvMealList.DataSource = null;
            gvMealList.DataSource = service.GetMealsToDisplay();
            gvMealList.DataBind();
            ACTIVETAB = "mealsTab";
        }

        protected void gvIngredientList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if the ingredient-edit button is selected:
            //redirect the page and pass it the ingredient ID
            string idFromGridview = gvIngredientList.SelectedRow.Cells[0].Text;
            int id = int.Parse(idFromGridview);
            Response.Redirect("~/User_Pages/Ingredient_Edit.aspx?id=" + id);
        }

        protected void btnCreateNewIngredient_Click(object sender, EventArgs e)
        {
            //instance new service
            //create new instance of ingredient and populate data with textfields
            var service = new CharityKitchenDataServiceSoapClient();
            Ingredient ingredientToCreate = new Ingredient();
            ingredientToCreate.IngredientName = txtIngredientName.Text;
            ingredientToCreate.IngredientPrice = decimal.Parse(txtIngredientPrice.Text);

            //if text fields are not null or empty and the price is numeric
            //insert the new ingredient and let the user know of any changes
            int rowsAffected = 0;
            if (!string.IsNullOrEmpty(txtIngredientName.Text) && !string.IsNullOrEmpty(txtIngredientPrice.Text) && service.IsNumeric(txtIngredientPrice.Text))
            {
                rowsAffected = service.InsertNewIngredient(ingredientToCreate);
            }
            if (rowsAffected == 0)
            {
                lblIngredientSaved.Text = string.Empty;
                lblErrorCreatingIngredient.Text = "Error creating ingredient..";
            }
            else
            {
                lblIngredientSaved.Text = "Ingredient created successfully.";
                lblErrorCreatingIngredient.Text = string.Empty;
            }
            //empty the textfields
            //reset the datasource
            //set the current tab
            txtIngredientName.Text = string.Empty;
            txtIngredientPrice.Text = string.Empty;

            gvIngredientList.DataSource = null;
            gvIngredientList.DataSource = service.GetAllIngredients();
            gvIngredientList.DataBind();
            ACTIVETAB = "ingredientsTab";
        }
    }
}