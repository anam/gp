<%@ Page Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true" 
CodeFile="AdminModuleInsertUpdate.aspx.cs" Inherits="AdminModuleInsertUpdate" Title="Module Insert/Update By Admin" %>

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
                    <asp:Label ID="lblModuleName" runat="server" Text="ModuleName: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtModuleName" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Folder Name: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFolderName" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDefaultURL" runat="server" Text="DefaultURL: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDefaultURL" runat="server" Text="">
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
