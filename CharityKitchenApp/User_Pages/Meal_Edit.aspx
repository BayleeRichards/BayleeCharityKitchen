<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Meal_Edit.aspx.cs" Inherits="CharityKitchenApp.User_Pages.Meal_Edit" %>
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
        .lblTotal-css
        {
            font-size: 18px;
            margin-left: 1%;
            vertical-align: text-bottom;
            float: left;
        }
        .qty-label-css
        {
            margin-left: 14.5%;
        }
        .total-label-css
        {
            margin-right: 1%;
            margin-bottom: 0%;
            font-size: 21px;
            float: right;
        }
        .textfield-css
        {
            width: 50%;
            float:left;
            margin-left: 10%;
            height: 26px;
        }
        .qty-textfield-css
        {
            display: inline-block;
            width: 8%;
            margin-left: 0.5%;
            margin-top: 0.5%;
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
        .remove-btns
        {
            width: 60%;
        }
        .create-btn
        {
            width: 30%;
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
                    <h1 style="margin-bottom:1.5%;">Edit Meal</h1>
                        <table style="width:100%;">
                            <tr>
                                <td style="width:50%;"><label class="label-css">Meal ID:</label></td>
                                <td style="width:50%;"><asp:Label CssClass="lblID-css" ID="lblMealID" Text="0" runat="server" /></td>
                            </tr>
                            <tr>
                                <td><label class="label-css">Meal Name:</label></td>
                                <td class="tbl-row-padding"><asp:TextBox CssClass="textfield-css form-control" ID="txtMealName" runat="server" /></td>
                            </tr>
                            <tr>
                                <td><label class="label-css">Meal Description:</label></td>
                                <td class="tbl-row-padding"><asp:TextBox CssClass="textfield-css form-control" ID="txtMealDesc" runat="server" TextMode="MultiLine" Rows="3" Wrap="true" style="overflow:hidden;" /></td>
                            </tr>
                        </table>
                </div>
                <div class="well">
                    <h1 style="margin-bottom:1.5%;">Ingredient List</h1>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:GridView CssClass="table table-hover text-left" ID="gvIngredientList" runat="server" GridLines="None" AutoGenerateColumns="false" OnSelectedIndexChanged="ingredientList_SelectedIndexChanged" ShowHeaderWhenEmpty="true">
                                <Columns>
                                    <asp:BoundField HeaderText="Meal Ingredient ID" DataField="ID" ItemStyle-CssClass="hidden-col" HeaderStyle-CssClass="hidden-col" />
                                    <asp:BoundField HeaderText="Ingredient" DataField="Name" />
                                    <asp:BoundField HeaderText="Quantity Required" DataField="Quantity" />
                                    <asp:BoundField HeaderText="Price Per Unit" DataField="Price" DataFormatString="{0:C}" />
                                    <asp:BoundField HeaderText="Subtotal" DataField="Subtotal"  DataFormatString="{0:C}" />
                                    <asp:CommandField ShowSelectButton="true" SelectText="Remove" ButtonType="Button" ControlStyle-CssClass="btn btn-default remove-btns" />
                                </Columns>
                            </asp:GridView>
                            <hr />
                            <table style="width:100%;">
                                <tr>
                                    <td style="width:95%;"><label class="total-label-css">Total:</label></td>
                                    <td><asp:Label CssClass="lblTotal-css" ID="lblTotal" Text="$0" runat="server" /></td>
                                </tr>
                            </table>
                            <hr />
                            <asp:DropDownList CssClass="form-control ddl-css" ID="ddlIngredients" runat="server">
                                <asp:ListItem Enabled="true" Text="Select an ingredient.." Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button CssClass="btn btn-default" ID="btnAddIngredient" Text="Add" runat="server" OnClick="btnAddIngredient_Click" /><br />
                            <label class="qty-label-css">Qty:</label><asp:TextBox CssClass="qty-textfield-css form-control" ID="txtQuantity" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Label ID="lblIngredientSuccessful" runat="server" />
                    <asp:Label ID="lblIngredientError" runat="server" ForeColor="Red" />
                </div>
                <asp:Button CssClass="btn btn-primary create-btn" ID="btnSaveMeal" Text="Save Meal" runat="server" OnClick="btnSaveMeal_Click" />
                <br />
                <asp:Label ID="lblChangesSaved" runat="server" />
                <asp:Label ID="lblErrorSaving" runat="server" ForeColor="Red" />

                <footer class="footer">
                    <div class="container">
                        <span class="text-muted">
                            <button type="button" class="btn btn-default btn-lg btn-help" data-toggle="modal" data-target="#helpModal"><span class="glyphicon glyphicon-info-sign"></span></button>
                        </span>
                        <div class="modal fade" id="helpModal" role="dialog">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Edit Meals - Help</h4>
                                    </div>
                                    <div class="modal-body text-left">
                                        <p>To modify the current Meal, enter appropriate data into the fields provided and click the <b>Save Meal</b> button.<br />
                                            <b>Meal:</b><br />-Name of the meal<br />-Description of the meal<br /><br />
                                            To add ingredients to a meal, select the ingredient you wish to add from the <b>Drop Down List</b> provided. Then, enter the <b>quantity</b> required in the text field provided 
                                            (<b>NOTE: </b>Quantity entered must be a whole number).<br />
                                            To remove an ingredient from a meal, simply click the <b>Remove</b> button next to the item you wish to remove.
                                        </p>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </footer>
            </div>
        </div>
    </div>
</asp:Content>
