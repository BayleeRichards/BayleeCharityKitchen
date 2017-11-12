using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace CharityKitchenApp
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                try
                {
                    CharityKitchenDataServiceProxy.UserLogin user = new CharityKitchenDataServiceProxy.CharityKitchenDataServiceSoapClient().GetLoggedInUserByUsername(Context.User.Identity.Name);

                    //enable appropriate buttons for user roles
                    if (user.Roles.Contains("Admin")) { btnOrders.Visible = true; btnUserManagement.Visible = true; }
                    if (user.Roles.Contains("Kitchen")) { btnKitchen.Visible = true; }
                    if (user.Roles.Contains("Warehouse")) { btnOrders.Visible = true; }

                }
                catch
                {
                    FormsAuthentication.SignOut();
                    Response.Redirect("~/Default.aspx");
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("../Default.aspx");
        }

        protected void btnOrders_Click(object sender, EventArgs e)
        {
            Response.Redirect("Orders.aspx");
        }

        protected void btnKitchen_Click(object sender, EventArgs e)
        {
            Response.Redirect("Kitchen.aspx");
        }

        protected void btnUserManagement_Click(object sender, EventArgs e)
        {
            Response.Redirect("User_Management.aspx");
        }

        protected void btnCredentials_Click(object sender, EventArgs e)
        {
            Response.Redirect("Credentials_Edit.aspx");
        }
    }
}