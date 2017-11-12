<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Order_View.aspx.cs" Inherits="CharityKitchenApp.User_Pages.Order_View" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
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
        .tbl-row-padding
        {
            padding-top: 0.3%;
            padding-bottom: 0.3%;
        }
        .form-control
        {
            padding: 0px 6px;
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
                    <h1 style="margin-bottom:1.5%;">Order Details</h1>
                    <table style="width:100%;">
                        <tr>
                            <td class="tbl-row-padding" style="width:50%;"><label class="label-css">Order ID:</label></td>
                            <td class="tbl-row-padding" style="width:50%;"><asp:Label CssClass="lblID-css" ID="lblOrderID" Text="0" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tbl-row-padding" style="width:50%;"><label class="label-css">Customer Name:</label></td>
                            <td class="tbl-row-padding" style="width:50%;"><asp:Label CssClass="lblID-css" ID="lblCustomerName" Text="0" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tbl-row-padding" style="width:50%;"><label class="label-css">Order Date:</label></td>
                            <td class="tbl-row-padding" style="width:50%;"><asp:Label CssClass="lblID-css" ID="lblOrderDate" Text="0" runat="server" DataFormatString="{0:d}" /></td>
                        </tr>
                    </table>
                </div>
                <div class="well">
                    <h1 style="margin-bottom:1.5%;">Order Items</h1>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:GridView CssClass="table table-hover text-left" ID="gvIngredientList" runat="server" GridLines="None" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                <Columns>
                                    <asp:BoundField HeaderText="Ingredient ID" DataField="ID" />
                                    <asp:BoundField HeaderText="Ingredient Name" DataField="Name" />
                                    <asp:BoundField HeaderText="Quantity Required" DataField="Quantity" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
