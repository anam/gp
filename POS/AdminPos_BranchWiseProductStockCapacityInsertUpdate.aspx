<%@ Page Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true" 
CodeFile="AdminPos_BranchWiseProductStockCapacityInsertUpdate.aspx.cs" Inherits="AdminPos_BranchWiseProductStockCapacityInsertUpdate" Title="Pos_BranchWiseProductStockCapacity Insert/Update By Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .tableCss
        {
        	text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tableCss">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblProductID" runat="server" Text="ProductID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlProduct" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblWorkStationID" runat="server" Text="WorkStationID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlWorkStation" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblStockAmount" runat="server" Text="StockAmount: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtStockAmount" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" 
                        OnClick="btnUpdate_Click" />
                </td>
                <td>
                    <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
