using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CharityKitchenApp.CharityKitchenDataServiceProxy;

namespace CharityKitchenApp.User_Pages
{
    public partial class User_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string idFromQueryString = Request.QueryString["id"];
            lblUserID.Text = idFromQueryString;
            var service = new CharityKitchenDataServiceSoapClient();
            gvRoleList.DataSource = service.GetRolesForUser(int.Parse(lblUserID.Text));
            gvRoleList.DataBind();

            User user = service.GetOneUser(int.Parse(lblUserID.Text));
            lblUsername.Text = user.UserName;

            if (!Page.IsPostBack)
            {
                ddlRoles.DataSource = service.GetAllRoles();
                ddlRoles.DataTextField = "RoleDescription";
                ddlRoles.DataValueField = "RoleID";
                ddlRoles.DataBind();
            }
        }

        protected void gvRoleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var service = new CharityKitchenDataServiceSoapClient();
            string idFromGridview = gvRoleList.SelectedRow.Cells[0].Text;
            int id = int.Parse(idFromGridview);
            int rowsAffected = service.DeleteUserRole(id);
            if (rowsAffected == 0)
            {
                lblRoleSuccessful.Text = string.Empty;
                lblRoleError.Text = "Error deleting role from user..";
            }
            else
            {
                lblRoleSuccessful.Text = "Role deleted from user successfully.";
                lblRoleError.Text = string.Empty;
            }
            gvRoleList.DataSource = null;
            gvRoleList.DataSource = service.GetRolesForUser(int.Parse(lblUserID.Text));
            gvRoleList.DataBind();
        }

        protected void btnAddRole_Click(object sender, EventArgs e)
        {
            var service = new CharityKitchenDataServiceSoapClient();
            Role roleToAdd = service.GetOneRole(int.Parse(ddlRoles.SelectedValue));

            int rowsAffected = service.InsertUserRole(int.Parse(lblUserID.Text), roleToAdd.RoleID);
            if (rowsAffected == 0)
            {
                lblRoleSuccessful.Text = string.Empty;
                lblRoleError.Text = "Error adding role to user..";
            }
            else
            {
                lblRoleSuccessful.Text = "Role added to user successfully.";
                lblRoleError.Text = string.Empty;
            }
            gvRoleList.DataSource = null;
            gvRoleList.DataSource = service.GetRolesForUser(int.Parse(lblUserID.Text));
            gvRoleList.DataBind();

        }
    }
}