using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CharityKitchenApp.CharityKitchenDataServiceProxy;
using Sodium;

namespace CharityKitchenApp.User_Pages
{
    public partial class User_Management : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var service = new CharityKitchenDataServiceSoapClient();
            gvUserList.DataSource = service.GetAllUsers();
            gvUserList.DataBind();
        }

        protected void gvUserList_SelectedIndexChanged(object sender, EventArgs e)
        { 
            string idFromGridview = gvUserList.SelectedRow.Cells[0].Text;
            int id = int.Parse(idFromGridview);
            Response.Redirect("~/User_Pages/User_Edit.aspx?id=" + id);
        }

        protected void btnCreateUser_Click(object sender, EventArgs e)
        {
            var service = new CharityKitchenDataServiceSoapClient();
            User userToCreate = new User();
            userToCreate.UserName = txtUsername.Text;
            //userToCreate.UserPassword = txtPassword.Text; //implement hashing algorithm and salt text first
            //userToCreate.UserPassword = service.HashPassword("password");
            userToCreate.UserPassword = "password";
            int rowsAffected = 0;
            if (!string.IsNullOrEmpty(txtUsername.Text))
            {
                rowsAffected = service.InsertNewUser(userToCreate);
            }
            if (rowsAffected == 0)
            {
                lblUserCreated.Text = string.Empty;
                lblUserError.Text = "Error creating user..";
            }
            else
            {
                lblUserCreated.Text = "User created successfully.";
                lblUserError.Text = string.Empty;
            }
            txtUsername.Text = string.Empty;

            gvUserList.DataSource = null;
            gvUserList.DataSource = service.GetAllUsers();
            gvUserList.DataBind();
        }
    }
}