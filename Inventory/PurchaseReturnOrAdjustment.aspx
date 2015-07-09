<%@ Page Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true" 
CodeFile="PurchaseReturnOrAdjustment.aspx.cs" Inherits="AdminInv_ProductInsertUpdate" Title="Inv_Product Insert/Update By Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .tableCss
        {
        	text-align: left;
        }
        .gridTxt
        {
            width:60px;
            text-align:right;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="tableCss">
     <h1>Purchase Return / Adjustment</h1>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblSuppierID" runat="server" Text="Suppier: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSuppier" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
             <tr style="display:none;">
                <td>
                    <asp:Label ID="lblWorkSatationID" runat="server" Text="WorkSatation: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlWorkSatation" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="display:none;">
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
    <h1>Items</h1>
    <table>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Date: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPurchaseReturnDate" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender1" runat="server" TargetControlID="txtPurchaseReturnDate">
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
                        <asp:Label ID="lblItemName" runat="server" Text='<%#Eval("ItemName") %>'>
                        </asp:Label>
                        <asp:HiddenField ID="hfInv_ItemID" runat="server" Value='<%#Eval("Inv_ItemID") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Purchase ID">
                    <ItemTemplate>
                        <asp:Label ID="lblPurchaseID" runat="server" Text='<%#Eval("PurchaseID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Code">
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
                <asp:TemplateField HeaderText="Purchased Quantity">
                    <ItemTemplate>
                        <asp:Label ID="lblPurchasedQuantity" runat="server" Text='<%#Eval("PurchasedQuantity") %>'>
                        </asp:Label>
                        -
                        <asp:Label ID="lblQuantityUnitID" runat="server" Text='<%#Eval("QuantityUnitName") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price/Unit">
                    <ItemTemplate>
                        <asp:Label ID="lblPricePerUnit" runat="server" Text='<%#Eval("PricePerUnit") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Available Stock">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraFieldQuantity1" runat="server" Text='<%#Eval("ExtraFieldQuantity1") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Return Quantity">
                    <ItemTemplate>
                       <asp:TextBox ID="txtReturnQuantity" runat="server" Text="0" style="width:50px;text-align:right;"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Adjustment">
                    <ItemTemplate>
                        <asp:TextBox ID="txtAdjustmentQuantity" runat="server" Text="0" style="width:50px;text-align:right;"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Price">
                    <ItemTemplate>
                        <asp:Label ID="lblPurchasedQuantityPrice" runat="server" Text='<%#Eval("PurchasedQuantityPrice","{0:0,0.00}") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>        </div>
    <div class="tableCss">
        <table>
         
            <tr>
                <td>
                 </td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="Submit" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false"
                        OnClick="btnUpdate_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click"  Visible="false" />
                </td>
                <td>
                    <asp:HyperLink ID="hlnkPurchasePrint" CssClass="buttonCss" Visible="false" runat="server" Target="_blank">Print Returen Voucher</asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="hlnkAdjustmentPrint" CssClass="buttonCss" Visible="false" runat="server" Target="_blank">Print Adjustment Voucher</asp:HyperLink>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
