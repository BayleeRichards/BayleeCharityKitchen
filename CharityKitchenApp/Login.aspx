<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CharityKitchenApp.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .label-css
        {
            margin-right: 10%;
            margin-bottom: 0px;
            float: right;
        }
        .textfield-css
        {
            width: 50%;
            float:left;
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
        .create-btn
        {
            width: 30%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="container-fluid">
        <div class="row text-center">
            <div class="col-sm-8 col-sm-offset-2">
                <div class="well">
                    <h1 style="margin-bottom:1.5%;">Login</h1>
                    <table style="width:100%;">
                        <tr>
                            <td style="width:50%;"><label class="label-css">Username:</label></td>
                            <td class="tbl-row-padding"><asp:TextBox CssClass="textfield-css form-control" ID="txtUsername" runat="server" /></td>
                        </tr>
                        <tr>
                            <td style="width:50%;"><label class="label-css">Password:</label></td>
                            <td class="tbl-row-padding"><asp:TextBox CssClass="textfield-css form-control" ID="txtPassword" runat="server" TextMode="Password" /></td>
                        </tr>
                    </table>
                </div>
                <asp:Button CssClass="btn btn-primary create-btn" ID="btnLogin" Text="Login" runat="server" OnClick="btnLogin_Click" />
                <br />
                <asp:Label ID="lblLoginFailed" runat="server" ForeColor="Red" />
                <asp:CheckBox ID="persist" Visible="false" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
