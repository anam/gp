<%@ Page Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true" 
CodeFile="AdminButtonInsertUpdate.aspx.cs" Inherits="AdminButtonInsertUpdate" Title="Button Insert/Update By Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .tableCss
        {
        	text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table>
<tr>
<td>
    <div class="tableCss">
        <table>
            <tr>
                <td valign="top">
                    <asp:Label ID="lblButtonName" runat="server" Text="ButtonName: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDefaultButton" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="ddlDefaultButton_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtButtonName" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblButtonText" runat="server" Text="ButtonText: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtButtonText" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPageID" runat="server" Text="PageID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPage" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="ddlPage_SelectedIndexChanged">
                    </asp:DropDownList>
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
                </td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" 
                        OnClick="btnUpdate_Click" />
                
                    <asp:Button ID="btnClear" runat="server" Text="Back To Add" Visible="false" OnClick="btnClear_Click" />
                </td>
            </tr>
        </table>
    </div>
    </td>
    </tr>
        <tr>
            <td>
            <div>
            <asp:GridView ID="gvButton" runat="server" AutoGenerateColumns="false" CssClass="gridCss">
            <Columns>
                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbSelect" runat="server" CommandArgument='<%#Eval("ButtonID") %>' OnClick="lbSelect_Click">
                            Select
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ButtonName">
                    <ItemTemplate>
                        <asp:Label ID="lblButtonName" runat="server" Text='<%#Eval("ButtonName") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ButtonText">
                    <ItemTemplate>
                        <asp:Label ID="lblButtonText" runat="server" Text='<%#Eval("ButtonText") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PageID">
                    <ItemTemplate>
                        <asp:Label ID="lblPageID" runat="server" Text='<%#Eval("PageID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RowStatusID">
                    <ItemTemplate>
                        <asp:Label ID="lblRowStatusID" runat="server" Text='<%#Eval("RowStatusID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%#Eval("ButtonID") %>' OnClick="lbDelete_Click">
                            Delete
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
            </td>
        </tr>
    </table>
</asp:Content>
