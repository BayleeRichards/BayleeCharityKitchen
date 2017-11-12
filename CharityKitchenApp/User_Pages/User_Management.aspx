<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User_Management.aspx.cs" Inherits="CharityKitchenApp.User_Pages.User_Management" %>
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

        .label-css 
        {
            margin-right: 10%;
            margin-bottom: 0px;
            float: right;
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
            display: block;
            width: 50%;
            vertical-align: middle;
            margin-left: 10%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="container-fluid">
        <div class="row text-center">
            <div class="col-sm-8 col-sm-offset-2">
                <h1>User Management</h1>
                <div class="well">
                    <h2>Users</h2>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:GridView CssClass="table table-hover text-left" ID="gvUserList" runat="server" GridLines="none" AutoGenerateColumns="false" OnSelectedIndexChanged="gvUserList_SelectedIndexChanged" ShowHeaderWhenEmpty="true">
                                <Columns>
                                    <asp:BoundField HeaderText="User ID" DataField="UserID" />
                                    <asp:BoundField HeaderText="Username" DataField="UserName" />
                                    <asp:CommandField ShowSelectButton="true" SelectText="Edit" ButtonType="Button" ControlStyle-CssClass="btn btn-default edit-btns" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="well">
                    <h2>Create User</h2>
                    <table style="width:100%;">
                        <tr>
                            <td style="width:50%;"><label class="label-css">Username:</label></td>
                            <td class="tbl-row-padding"><asp:TextBox CssClass="textfield-css form-control" ID="txtUsername" runat="server" /></td>
                        </tr>
                    </table>
                </div>
                <asp:Button CssClass="btn btn-primary create-btn" ID="btnCreateUser" Text="Create User" runat="server" OnClick="btnCreateUser_Click" />
                <br />
                <asp:Label ID="lblUserCreated" runat="server" />
                <asp:Label ID="lblUserError" runat="server" ForeColor="Red" />
            </div>
        </div>
    </div>
</asp:Content>
