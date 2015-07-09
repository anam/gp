<%@ Page Language="C#" MasterPageFile="~/login/AdminMaster.master" AutoEventWireup="true" 
CodeFile="AdminPos_BrandInsertUpdate.aspx.cs" Inherits="AdminPos_BrandInsertUpdate" Title="Pos_Brand Insert/Update By Admin" %>

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
                    <asp:Label ID="lblBrandName" runat="server" Text="BrandName: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtBrandName" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDetails" runat="server" Text="Details: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDetails" runat="server" Text="">
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
