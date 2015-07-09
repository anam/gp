<%@ Page Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true"
    CodeFile="Stock.aspx.cs" Inherits="AdminInv_ProductInsertUpdate" Title="Inv_Product Insert/Update By Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .tableCss
        {
            text-align: left;
        }
        .gridTxt
        {
            width: 60px;
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="tableCss">
        <h1>
            Stock Report</h1>
        <table>
            <tr style="display: none;">
                <td>
                    <asp:Label ID="lblSuppierID" runat="server" Text="Suppier: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSuppier" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="display: none;">
                <td>
                    <asp:Label ID="lblWorkSatationID" runat="server" Text="WorkSatation: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlWorkSatation" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="display: none;">
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Item Code: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtItemCode" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnbtnSearch_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table style="display: none;">
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Date: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPurchaseReturnDate" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender1" runat="server"
                        TargetControlID="txtPurchaseReturnDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Note: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNote" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvInv_Item" runat="server" AutoGenerateColumns="false" CssClass="gridCss">
            <Columns>
                <asp:TemplateField HeaderText="Item">
                    <ItemTemplate>
                       <a href='ItemHistory.aspx?ItemID=<%#Eval("Inv_ItemID") %>' target="_blank"> <asp:Label ID="lblItemName" runat="server" Text='<%#Eval("ItemName") %>'>
                        </asp:Label></a>
                        <asp:HiddenField ID="hfInv_ItemID" runat="server" Value='<%#Eval("Inv_ItemID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Purchase<br/>ID">
                    <ItemTemplate>
                        <asp:Label ID="lblPurchaseID" runat="server" Text='<%#Eval("PurchaseID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item<br/>Code">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%#Eval("ItemCode") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quality">
                    <ItemTemplate>
                        <asp:Label ID="lblQualityValue" runat="server" Text='<%#Eval("QualityValue") %>'>
                        </asp:Label>
                        -
                        <asp:Label ID="lblQualityUnitID" runat="server" Text='<%#Eval("QualityUnitName") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit">
                    <ItemTemplate>
                        <asp:Label ID="lblQuantityUnitID" runat="server" Text='<%#Eval("QuantityUnitName") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Purchased<br/>Quantity">
                    <ItemTemplate>
                        <asp:Label ID="lblPurchasedQuantity" runat="server" Text='<%#Eval("PurchasedQuantity") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Adjustment">
                    <ItemTemplate>
                        <asp:Label ID="lblAdjustment" runat="server" Text='<%#Eval("ExtraFieldQuantity2") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Prchased<br/>return">
                    <ItemTemplate>
                        <asp:Label ID="lblPurchasedRerurn" runat="server" Text='<%#Eval("ExtraFieldQuantity3") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="issued">
                    <ItemTemplate>
                        <asp:Label ID="lblissued" runat="server" Text='<%#Eval("ExtraFieldQuantity5") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Issue Return">
                    <ItemTemplate>
                        <asp:Label ID="lblIssueReturn" runat="server" Text='<%#Eval("issueReturedQuantity") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Utlized">
                    <ItemTemplate>
                        <asp:Label ID="lblUtlized" runat="server" Text='<%#Eval("utilizedQuantity") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="lost">
                    <ItemTemplate>
                        <asp:Label ID="lbllostQuantity" runat="server" Text='<%#Eval("lostQuantity") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Wasted">
                    <ItemTemplate>
                        <asp:Label ID="lblWasted" runat="server" Text='<%#Eval("ExtraFieldQuantity4") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Available<br/>Stock">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraFieldQuantity1" runat="server" Text='<%#Eval("ExtraFieldQuantity1") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price<br/>per<br/>Unit">
                    <ItemTemplate>
                        <asp:Label ID="lblPricePerUnit" runat="server" Text='<%#Eval("PricePerUnit") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total<br/>Price">
                    <ItemTemplate>
                        <asp:Label ID="lblPurchasedQuantityPrice" runat="server" Text='<%#Eval("PurchasedQuantityPrice","{0:0,0.00}") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="tableCss" style="display: none;">
        <table>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="Submit" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" OnClick="btnUpdate_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" Visible="false" />
                </td>
                <td>
                    <asp:HyperLink ID="hlnkPurchasePrint" CssClass="buttonCss" Visible="false" runat="server"
                        Target="_blank">Print Returen report</asp:HyperLink>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
