<%@ Page Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true"
    CodeFile="AdminPageInsertUpdate.aspx.cs" Inherits="AdminPageInsertUpdate" Title="Page Insert/Update By Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .tableCss
        {
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <div class="tableCss">
                    <h1>
                        Add / Update Page</h1>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblModuleID" runat="server" Text="ModuleID: ">
                </asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlModule" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hfPageID" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPageTitle" runat="server" Text="PageTitle: ">
                </asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPageTitle" runat="server" Text="">
                </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPageURL" runat="server" Text="PageURL: ">
                </asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPageURL" runat="server" Text="">
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
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                                        <asp:Button ID="btnClear" runat="server" Text="Back To Add" Visible="false" OnClick="btnClear_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvPage" runat="server" AutoGenerateColumns="false" CssClass="gridCss">
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbSelect" runat="server" CommandArgument='<%#Eval("PageID") %>'
                                                OnClick="lbSelect_Click">
                        Select
                    </asp:LinkButton>
                                            | <a href='AdminMenuInsertUpdate.aspx?PageID=<%#Eval("PageID") %>&ModuleID=<%#Eval("ModuleID") %>'>
                                                Add Menu</a> | <a href='AdminButtonInsertUpdate.aspx?PageID=<%#Eval("PageID") %>&ModuleID=<%#Eval("ModuleID") %>'>
                                                    Add Button</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PageTitle">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPageTitle" runat="server" Text='<%#Eval("PageTitle") %>'>
                    </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PageURL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPageURL" runat="server" Text='<%#Eval("PageURL") %>'>
                    </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ModuleID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblModuleID" runat="server" Text='<%#Eval("ModuleID") %>'>
                    </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AddedBy">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAddedBy" runat="server" Text='<%#Eval("AddedBy") %>'>
                    </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AddedDate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAddedDate" runat="server" Text='<%#Eval("AddedDate") %>'>
                    </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UpdatedBy">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUpdatedBy" runat="server" Text='<%#Eval("UpdatedBy") %>'>
                    </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UpdatedDate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUpdatedDate" runat="server" Text='<%#Eval("UpdatedDate") %>'>
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
                                            <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%#Eval("PageID") %>'
                                                OnClick="lbDelete_Click">
                        Delete
                    </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
