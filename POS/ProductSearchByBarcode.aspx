<%@ Page Title="" Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true" CodeFile="ProductSearchByBarcode.aspx.cs" Inherits="POS_ProductSearchByBarcode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
    td{padding:5px;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
    <table>
        <tr>
            <td>
                Bar Code:
            </td>
            <td>
                <asp:TextBox ID="txtBarCode" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Search" 
                    onclick="btnSearch_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="gvProductSearchBranch" runat="server" Width="100%">
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="gvProductSearchCentral" runat="server" ShowHeader="false"  Width="100%">
                </asp:GridView>
            </td>
        </tr>
    </table>
</div>
</asp:Content>

