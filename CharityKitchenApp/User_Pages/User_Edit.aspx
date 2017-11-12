<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User_Edit.aspx.cs" Inherits="CharityKitchenApp.User_Pages.User_Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .edit-btns 
        {
            width: 60%;
        }

        .create-btn 
        {
            width: 30%;
            margin-top: 1%;
        }
        .remove-btns
        {
            width: 60%;
        }

        .label-css 
        {
            margin-right: 10%;
            margin-bottom: 0px;
            float: right;
        }
        .lblID-css
        {
            float: left;
            margin-left: 10%;
        }

        .textfield-css 
        {
            width: 50%;
            float: left;
            margin-left: 10%;
            height: 26px;
        }

        .tbl-row-padding 
        {
            padding-top: 0.3%;
            padding-bottom: 0.3%;
        }

        .form-control 
        {
            padding: 0px 6px;
        }

        .ddl-css
        {
            display: inline-block;
            width: 30%;
            vertical-align: middle;
        }
        .hidden-col
        {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="container-fluid">
        <div class="row text-center">
            <div class="col-sm-8 col-sm-offset-2">
                <div class="well">
                    <h1 style="margin-bottom:1.5%;">Edit User</h1>
                    <table style="width:100%;">
                        <tr>
                            <td style="width:50%;"><label class="label-css">User ID:</label></td>
                            <td style="width:50%;"><asp:Label CssClass="lblID-css" ID="lblUserID" Text="0" runat="server" /></td>
                        </tr>
                        <tr>
                            <td style="width:50%;"><label class="label-css">Username:</label></td>
                            <td style="width:50%;"><asp:Label CssClass="lblID-css" ID="lblUsername" Text="0" runat="server" /></td>
                        </tr>
                    </table>
                </div>
                <div class="well">
                    <h1 style="margin-bottom:1.5%;">User Roles</h1>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:GridView CssClass="table table-hover text-left" ID="gvRoleList" runat="server" GridLines="None" AutoGenerateColumns="false" OnSelectedIndexChanged="gvRoleList_SelectedIndexChanged" ShowHeaderWhenEmpty="true">
                                <Columns>
                                    <asp:BoundField HeaderText="User Role ID" DataField="ID" ItemStyle-CssClass="hidden-col" HeaderStyle-CssClass="hidden-col" />
                                    <asp:BoundField HeaderText="Role" DataField="Role" />
                                    <asp:CommandField ShowSelectButton="true" SelectText="Remove" ButtonType="Button" ControlStyle-CssClass="btn btn-default remove-btns" />
                                </Columns>
                            </asp:GridView>
                            <hr />
                            <asp:DropDownList CssClass="form-control ddl-css" ID="ddlRoles" runat="server">
                                <asp:ListItem Enabled="true" Text="Select a role.." Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button CssClass="btn btn-default" ID="btnAddRole" Text="Add" runat="server" OnClick="btnAddRole_Click" /><br />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Label ID="lblRoleSuccessful" runat="server" />
                    <asp:Label ID="lblRoleError" runat="server" ForeColor="Red" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
