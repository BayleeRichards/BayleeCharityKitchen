<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Credentials_Edit.aspx.cs" Inherits="CharityKitchenApp.User_Pages.Credentials_Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="container-fluid">
        <div class="row text-center">
            <div class="col-sm-8 col-sm-offset-2">
                <div class="well">
                    <h1 style="margin-bottom:1.5%;">Edit Credentials</h1>
                    <table style="width:100%;">
                        <tr>
                            <td style="width:50%;"><label class="label-css">Old Password:</label></td>
                            <td class="tbl-row-padding"><asp:TextBox CssClass="textfield-css form-control" ID="txtOldPassword" runat="server" TextMode="Password" /></td>
                        </tr>
                        <tr>
                            <td style="width:50%;"><label class="label-css">New Password:</label></td>
                            <td class="tbl-row-padding"><asp:TextBox CssClass="textfield-css form-control" ID="txtNewPassword" runat="server" TextMode="Password" /></td>
                        </tr>
                        <tr>
                            <td style="width:50%;"><label class="label-css">Confirm Password:</label></td>
                            <td class="tbl-row-padding"><asp:TextBox CssClass="textfield-css form-control" ID="txtConfirmPassword" runat="server" TextMode="Password" /></td>
                        </tr>
                    </table>
                    <asp:CompareValidator ID="passwordValidator" runat="server"  Type="String" Operator="Equal" ControlToValidate="txtConfirmPassword" ControlToCompare="txtNewPassword" ErrorMessage="New password and confirmation password do not match.." ForeColor="Red" />
                </div>
                <asp:Button CssClass="btn btn-primary create-btn" ID="btnSaveCredentials" Text="Save Credentials" runat="server" OnClick="btnSaveCredentials_Click" />
                <br />
                <asp:Label ID="lblChangesSaved" runat="server" />
                <asp:Label ID="lblErrorSaving" runat="server" ForeColor="Red" />
            </div>
        </div>
    </div>
</asp:Content>
