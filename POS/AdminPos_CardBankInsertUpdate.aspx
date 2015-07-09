<%@ Page Language="C#" MasterPageFile="~/login/AdminMaster.master" AutoEventWireup="true" 
CodeFile="AdminPos_CardBankInsertUpdate.aspx.cs" Inherits="AdminPos_CardBankInsertUpdate" Title="Pos_CardBank Insert/Update By Admin" %>

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
                    <asp:Label ID="lblBankName" runat="server" Text="BankName: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtBankName" runat="server" Text="">
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
                    <asp:Label ID="lblCharOfAccountLabel4ID" runat="server" Text="CharOfAccountLabel4ID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCharOfAccountLabel4" runat="server">
                    </asp:DropDownList>
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
