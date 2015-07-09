<%@ Page Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true" 
CodeFile="AdminDefaultButtonInsertUpdate.aspx.cs" Inherits="AdminDefaultButtonInsertUpdate" Title="DefaultButton Insert/Update By Admin" %>

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
                    <asp:Label ID="lblDefaultButtonName" runat="server" Text="DefaultButtonName: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDefaultButtonName" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDefaultButtonText" runat="server" Text="DefaultButtonText: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDefaultButtonText" runat="server" Text="">
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
                    <asp:Button ID="btnClear" runat="server" Text="Clear" Visible="false" OnClick="btnClear_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
