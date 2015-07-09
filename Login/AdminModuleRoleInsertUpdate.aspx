<%@ Page Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true" 
CodeFile="AdminModuleRoleInsertUpdate.aspx.cs" Inherits="AdminModuleRoleInsertUpdate" Title="ModuleRole Insert/Update By Admin" %>

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
                    <asp:Label ID="lblModuleID" runat="server" Text="ModuleID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlModule" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblRoleID" runat="server" Text="RoleID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlRole" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAddedBy" runat="server" Text="AddedBy: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAddedBy" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblModifyDate" runat="server" Text="ModifyDate: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtModifyDate" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblModifyBy" runat="server" Text="ModifyBy: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtModifyBy" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblRowStatusID" runat="server" Text="RowStatusID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlRowStatus" runat="server">
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
                    <asp:Button ID="btnClear" runat="server" Text="Clear" Visible="false" OnClick="btnClear_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
