using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using CharityKitchenApp.CharityKitchenDataServiceProxy;

namespace CharityKitchenApp
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            var service = new CharityKitchenDataServiceSoapClient();
            //bool authenticated = service.ValidateCredentials(txtUsername.Text, txtPassword.Text);

            //if (authenticated)
            //{
            //    FormsAuthentication.RedirectFromLoginPage(txtUsername.Text, persist.Checked);
            //}
            //else
            //{
            //    lblLoginFailed.Text = "Invalid username and/or password..";
            //}

            var user = service.ValidateLoginCredentials(txtUsername.Text, txtPassword.Text);

            if (string.IsNullOrEmpty(user.Username))
            {
                lblLoginFailed.Text = "Invalid username and/or password..";
            }
            else
            {
                FormsAuthentication.RedirectFromLoginPage(user.Username, persist.Checked);
            }
        }
    }
}