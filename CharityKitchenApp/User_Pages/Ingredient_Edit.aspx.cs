using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CharityKitchenApp.CharityKitchenDataServiceProxy;

namespace CharityKitchenApp.User_Pages
{
    public partial class Ingredient_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //when page loads, grabbed the passed id value
            //set label to ID value
            //create new service instance
            string idFromQueryString = Request.QueryString["id"];
            lblIngredientID.Text = idFromQueryString;
            var service = new CharityKitchenDataServiceSoapClient();

            //if the page is not a post back:
            //get an ingredient and set the textfields
            if (!Page.IsPostBack)
            {
                Ingredient ingredient = service.GetOneIngredient(int.Parse(lblIngredientID.Text));
                txtIngredientName.Text = ingredient.IngredientName;
                txtIngredientPrice.Text = ingredient.IngredientPrice.ToString();
            }
            
        }

        protected void btnSaveIngredient_Click(object sender, EventArgs e)
        {
            //create new service instance
            //create a new ingredient and fill it with the information to update
            var service = new CharityKitchenDataServiceSoapClient();
            Ingredient ingredientToUpdate = new Ingredient();
            ingredientToUpdate.IngredientID = int.Parse(lblIngredientID.Text);
            ingredientToUpdate.IngredientName = txtIngredientName.Text;
            ingredientToUpdate.IngredientPrice = decimal.Parse(txtIngredientPrice.Text);

            //if textfields are not null or empty, update ingredient and let the user know if the changes were successful or not
            int rowsAffected = 0;
            if (!string.IsNullOrEmpty(txtIngredientName.Text) && !string.IsNullOrEmpty(txtIngredientPrice.Text) && service.IsNumeric(txtIngredientPrice.Text))
            {
                rowsAffected = service.UpdateExistingIngredient(ingredientToUpdate);
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
    }
}