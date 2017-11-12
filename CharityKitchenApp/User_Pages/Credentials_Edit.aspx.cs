using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CharityKitchenApp.CharityKitchenDataServiceProxy;

namespace CharityKitchenApp.User_Pages
{
    public partial class Credentials_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string id
            //if oldpassword.text == password in database,
            //  check if new password is not blank
            //  check if confirm password matches new password
            //  create user
        }

        protected void btnSaveCredentials_Click(object sender, EventArgs e)
        {
            //if old password, new password and confirmation password are not blank:
            if (!string.IsNullOrEmpty(txtOldPassword.Text) && !string.IsNullOrEmpty(txtNewPassword.Text) && !string.IsNullOrEmpty(txtConfirmPassword.Text))
            {
                //create new server instance
                //create new user instance and validate his credentials
                var service = new CharityKitchenDataServiceSoapClient();
                var user = service.ValidateLoginCredentials(Context.User.Identity.Name, txtOldPassword.Text);
                //if the returned username is not empty:
                if (!string.IsNullOrEmpty(user.Username))
                {
                    //update the user's password
                    //let the user know if changes were successful or not
                    User userToUpdate = service.GetOneUserByUsername(Context.User.Identity.Name);
                    userToUpdate.UserPassword = txtConfirmPassword.Text;
                    int rowsAffected = service.UpdateExistingUser(userToUpdate);
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
                    lblErrorSaving.Text = "Credentials entered were not valid..";
                }
            }
            

        }
    }
}