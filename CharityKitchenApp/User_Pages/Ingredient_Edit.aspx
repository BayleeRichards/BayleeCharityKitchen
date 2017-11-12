<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ingredient_Edit.aspx.cs" Inherits="CharityKitchenApp.User_Pages.Ingredient_Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .label-css
        {
            margin-right: 10%;
            margin-bottom: 0px;
            float: right;
        }
        .sub-label-css
        {
             margin-right: 11%;
             line-height: 0.9;
             font-weight: 400;
             color: #777;
        }
        .lblID-css
        {
            float: left;
            margin-left: 10%;
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
                    <h1 style="margin-bottom:1.5%;">Edit Ingredient</h1>
                    <table style="width:100%;">
                        <tr>
                            <td style="width:50%;"><label class="label-css">Ingredient ID:</label></td>
                            <td style="width:50%;"><asp:Label CssClass="lblID-css" ID="lblIngredientID" Text="0" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><label class="label-css">Ingredient Name:</label></td>
                            <td class="tbl-row-padding"><asp:TextBox CssClass="textfield-css form-control" ID="txtIngredientName" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><label class="label-css">Ingredient Price:</label><br /><label class="label-css sub-label-css"><i>Per Unit</i></label></td>
                            <td class="tbl-row-padding"><asp:TextBox CssClass="textfield-css form-control" ID="txtIngredientPrice" runat="server" /></td>
                        </tr>
                    </table>
                </div>
                <asp:Button CssClass="btn btn-primary create-btn" ID="btnSaveIngredient" Text="Save Ingredient" runat="server" OnClick="btnSaveIngredient_Click" />
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
                                        <h4 class="modal-title">Edit Ingredients - Help</h4>
                                    </div>
                                    <div class="modal-body text-left">
                                        <p>To modify the current Ingredient, enter appropriate data into the fields provided and click the <b>Save Ingredient</b> button.<br />
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
