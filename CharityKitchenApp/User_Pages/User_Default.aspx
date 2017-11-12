<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User_Default.aspx.cs" Inherits="CharityKitchenApp.User_Pages.User_Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="container-fluid">
        <div class="row text-center">
            <div class="jumbotron">
                <h1>Charity Kitchen</h1>
                <%  string x = "<p><i>Welcome, " + Context.User.Identity.Name + ".";
                    Response.Write(x); %>
            </div>
        </div>
    </div>
</asp:Content>
