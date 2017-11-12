<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="CharityKitchenApp.User_Pages.Orders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .edit-btns 
        {
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

        .date-textfield-css
        {
            width: 25%;
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
                <h1>Orders</h1>
                <div class="well">
                    <h2>Customer Orders</h2>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <!-- if user has admin role, show edit button. if user has warehouse role, show view button instead -->
                            <asp:GridView CssClass="table table-hover text-left" ID="gvOrderList" runat="server" GridLines="None" AutoGenerateColumns="false" OnSelectedIndexChanged="gvOrderList_SelectedIndexChanged" ShowHeaderWhenEmpty="true" >
                                <Columns>
                                    <asp:BoundField HeaderText="Order ID" DataField="OrderID" />
                                    <asp:BoundField HeaderText="Customer Name" DataField="OrderCustomer" />
                                    <asp:BoundField HeaderText="Order Date" DataField="OrderDate" DataFormatString="{0:d}" />
                                    <asp:BoundField HeaderText="Price" DataField="Price" DataFormatString="{0:C}" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-default edit-btns" CommandName="Edit" CommandArgument='<%# Container.DataItemIndex %>' OnCommand="GridButtons_Command" Visible='<%# AuthorizedToEdit() %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-default edit-btns" CommandName="View" CommandArgument='<%# Container.DataItemIndex %>' OnCommand="GridButtons_Command" Visible='<%# AuthorizedToView() %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="well">
                    <h2>Create Order</h2>
                    <table style="width: 100%;">
                        <tr>
                            <td><label class="label-css">Customer Name:</label></td>
                            <td class="tbl-row-padding"><asp:TextBox CssClass="textfield-css form-control" ID="txtCustomerName" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><label class="label-css">Order Date:</label></td>
                            <td class="tbl-row-padding"><asp:TextBox CssClass="textfield-css date-textfield-css form-control" ID="txtOrderDate" runat="server" placeholder="DD/MM/YYYY" /></td>
                        </tr>
                    </table>
                    <asp:CompareValidator ID="dateValidator" runat="server" Type="Date" Operator="DataTypeCheck" ControlToValidate="txtOrderDate" ErrorMessage="Incorrect Date Format.." ForeColor="Red" />
                    <br />
                    <asp:Button CssClass="btn btn-primary create-btn" ID="btnCreateNewOrder" Text="Create New Order" runat="server" OnClick="btnCreateNewOrder_Click" />
                    <br />
                    <asp:Label ID="lblOrderSaved" runat="server" />
                    <asp:Label ID="lblErrorCreatingOrder" runat="server" ForeColor="Red" />
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
                                        <h4 class="modal-title">Orders - Help</h4>
                                    </div>
                                    <div class="modal-body text-left">
                                        <p>To modify an order, use the <b>Edit</b> button beside the order you wish to modify.<br />
                                            To see the list of ingredients in an order, use the <b>View</b> button beside the order you wish to see.<br />
                                            To Create a new Order, enter appropriate data into the fields provided.<br />
                                            <b>Order:</b><br />-Name of the customer<br />-Date of the order (<b>Date Format Expected:</b> 01/01/2000)</p>
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
