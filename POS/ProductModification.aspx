<%@ Page Title="" Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true" CodeFile="ProductModification.aspx.cs" Inherits="POS_ProductModification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
<h1>Product modification</h1>
    <table >
        <tr>
            <td>
                Barcode</td>
            <td>
                <asp:TextBox ID="txtBarCode" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Search" 
                    onclick="btnSearch_Click" /></td>
        </tr>
        <tr>
            <td>
                Status</td>
            <td>
                <asp:DropDownList ID="ddlStatus" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Unit Price</td>
            <td>
                <asp:TextBox ID="txtUnitPrice" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Unit Price(+/-)</td>
            <td>
                <asp:TextBox ID="txtUnitPriceAdjustment" runat="server" ></asp:TextBox>
                <asp:Button ID="btnPriceUpdate" runat="server" Text="Update Price" 
                    onclick="btnPriceUpdate_Click" />
            </td>
        </tr>
        <tr>
            <td>
                Purchase Qty</td>
            <td>
                <asp:TextBox ID="txtPurchaseQty" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Central Stock</td>
            <td>
                <asp:TextBox ID="txtCentralStock" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Purchase Qty
                (+/-)</td>
            <td>
                <asp:TextBox ID="txtPurchaseQtyAdjustment" runat="server" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="btnupdate" runat="server" Text="Update" 
                    onclick="btnupdate_Click" /></td>
        </tr>
    </table>
    </div>
</asp:Content>

