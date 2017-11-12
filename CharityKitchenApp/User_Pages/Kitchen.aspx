<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Kitchen.aspx.cs" Inherits="CharityKitchenApp.User_Pages.Kitchen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .img-sizing {
            width: 240px;
            height: 180px;
            overflow: hidden;
            padding: 0px;
            border-radius: 20px;
        }

        .img-positioning img {
            height: 100%;
            transform: translateX(-50%);
            margin-left: 50%;
        }

        .nav-pills > li > a {
            border: 1px solid #337ab7;
        }

        .nav-pills {
            margin-bottom: 2%;
        }

        .btn-properties {
            display: block;
            width: 12%;
            margin-left: 2%;
            margin-top: 0.5%;
        }

        .edit-btns {
            width: 60%;
        }

        .create-btn {
            width: 30%;
            margin-top: 1%;
        }

        hr {
            margin-bottom: 5%;
        }

        .label-css {
            margin-right: 10%;
            margin-bottom: 0px;
            float: right;
        }

        .sub-label-css {
            margin-right: 11%;
            line-height: 0.9;
            font-weight: 400;
            color: #777;
        }

        .textfield-css {
            width: 50%;
            float: left;
            margin-left: 10%;
            height: 26px;
        }

        .tbl-row-padding {
            padding-top: 0.3%;
            padding-bottom: 0.3%;
        }

        .form-control {
            padding: 0px 6px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="container-fluid">
        <div class="row text-center">
            <div class="col-sm-8 col-sm-offset-2">
                <h1>Kitchen</h1>
                <!--<button type="button" class="btn"-->
                <ul class="nav nav-pills nav-justified">
                    <% if (ACTIVETAB == "mealsTab")
                        {
                            string x = "<li id='mealsTab' class='active'><a data-toggle='pill' href='#mealsPage'>Meals</a></li><li id='ingredientsTab'><a data-toggle='pill' href='#ingredientsPage'>Ingredients</a></li>";
                            Response.Write(x);
                        }
                        else
                        {
                            string x = "<li id='mealsTab'><a data-toggle='pill' href='#mealsPage'>Meals</a></li><li id='ingredientsTab' class='active'><a data-toggle='pill' href='#ingredientsPage'>Ingredients</a></li>";
                            Response.Write(x);
                        }
                    %>
                   <%-- <li id="mealsTab" class="active"><a data-toggle="pill" href="#mealsPage">Meals</a></li>
                    <li id="ingredientsTab"><a data-toggle="pill" href="#ingredientsPage">Ingredients</a></li>--%>
                </ul>

                <div class="tab-content">
                    <% if (ACTIVETAB == "mealsTab")
                        {
                            string x = "<div id='mealsPage' class='tab-pane fade in active'>";
                            Response.Write(x);
                        }
                        else
                        {
                            string x = "<div id='mealsPage' class='tab-pane fade'>";
                            Response.Write(x);
                        }
                    %>
                    <%--<div id="mealsPage" class="tab-pane fade in active">--%>
                        <div class="well">
                            <h2>Meals</h2>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:GridView CssClass="table table-hover text-left" ID="gvMealList" runat="server" GridLines="none" AutoGenerateColumns="false" OnSelectedIndexChanged="gvList_SelectedIndexChanged" ShowHeaderWhenEmpty="true">
                                        <Columns>
                                            <asp:BoundField HeaderText="Meal ID" DataField="MealID" />
                                            <asp:BoundField HeaderText="Meal Name" DataField="MealName" />
                                            <asp:BoundField HeaderText="Price" DataField="Price" DataFormatString="{0:C}" />
                                            <asp:CommandField ShowSelectButton="true" SelectText="Edit" ButtonType="Button" ControlStyle-CssClass="btn btn-default edit-btns" />
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="well">
                            <h2>Create Meal</h2>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <label class="label-css">Meal Name:</label></td>
                                    <td class="tbl-row-padding">
                                        <asp:TextBox CssClass="textfield-css form-control" ID="txtMealName" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <label class="label-css">Meal Description:</label></td>
                                    <td class="tbl-row-padding">
                                        <asp:TextBox CssClass="textfield-css form-control" ID="txtMealDesc" runat="server" TextMode="MultiLine" Rows="3" Wrap="true" Style="overflow: hidden;" /></td>
                                </tr>
                            </table>
                            <asp:Button CssClass="btn btn-primary create-btn" ID="btnCreateNewMeal" Text="Create New Meal" runat="server" OnClick="btnCreateNewMeal_Click" />
                            <br />
                            <asp:Label ID="lblMealSaved" runat="server" />
                            <asp:Label ID="lblErrorCreatingMeal" runat="server" ForeColor="Red" />
                        </div>
                    </div>
                    
                    <% if (ACTIVETAB == "mealsTab")
                        {
                            string x = "<div id='ingredientsPage' class='tab-pane fade'>";
                            Response.Write(x);
                        }
                        else
                        {
                            string x = "<div id='ingredientsPage' class='tab-pane fade in active'>";
                            Response.Write(x);
                        }
                    %>
                    <%--<div id="ingredientsPage" class="tab-pane fade">--%>
                        <div class="well">
                            <h2>Ingredients</h2>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:GridView CssClass="table table-hover text-left" ID="gvIngredientList" runat="server" GridLines="none" AutoGenerateColumns="false" OnSelectedIndexChanged="gvIngredientList_SelectedIndexChanged" ShowHeaderWhenEmpty="true">
                                        <Columns>
                                            <asp:BoundField HeaderText="Ingredient ID" DataField="IngredientID" />
                                            <asp:BoundField HeaderText="Ingredient Name" DataField="IngredientName" />
                                            <asp:BoundField HeaderText="Price Per Unit" DataField="IngredientPrice" DataFormatString="{0:C}" />
                                            <asp:CommandField ShowSelectButton="true" SelectText="Edit" ButtonType="Button" ControlStyle-CssClass="btn btn-default edit-btns" />
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="well">
                            <h2>Create Ingredient</h2>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <label class="label-css">Ingredient Name:</label></td>
                                    <td class="tbl-row-padding">
                                        <asp:TextBox CssClass="textfield-css form-control" ID="txtIngredientName" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <label class="label-css">Ingredient Price:</label><br />
                                        <label class="label-css sub-label-css"><i>Per Unit</i></label></td>
                                    <td class="tbl-row-padding">
                                        <asp:TextBox CssClass="textfield-css form-control" ID="txtIngredientPrice" runat="server" /></td>
                                </tr>
                            </table>
                            <asp:Button CssClass="btn btn-primary create-btn" ID="btnCreateNewIngredient" Text="Create New Ingredient" runat="server" OnClick="btnCreateNewIngredient_Click" />
                            <br />
                            <asp:Label ID="lblIngredientSaved" runat="server" />
                            <asp:Label ID="lblErrorCreatingIngredient" runat="server" ForeColor="Red" />
                        </div>
                    </div>
                </div>
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
                                        <h4 class="modal-title">Kitchen - Help</h4>
                                    </div>
                                    <div class="modal-body text-left">
                                        <p>To swap between <b>Meals View</b> and <b>Ingredients View</b>, use the Meals and Ingredients tabs near the top of the page.<br /><br />
                                            To modify a meal or an ingredient, use the <b>Edit</b> button beside the item you wish to modify.<br />
                                            To Create a new Meal or Ingrdient, enter appropriate data into the fields provided.<br />
                                            <b>Meal:</b><br />-Name of the meal<br />-Description of the meal<br /><br />
                                            <b>Ingredient:</b><br />-Name of the ingredient<br />-Price of the ingredient (Per Unit: Expects $0.00 Value)</p>
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
