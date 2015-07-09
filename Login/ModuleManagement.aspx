<%@ Page Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true"
    CodeFile="ModuleManagement.aspx.cs" Inherits="AdminModuleDisplay" Title="Display Module By Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .gridCss
        {
            width: 100%;
            padding: 20px 10px 10px 10px;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <div class="tableCss">
                    <h1>
                        Add / Update Module</h1>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblModuleName" runat="server" Text="ModuleName: ">
                    </asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtModuleName" runat="server" Text="">
                    </asp:TextBox>
                                        <asp:HiddenField ID="hfModuleID" runat="server" />
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
                                        <asp:Label ID="Label2" runat="server" Text="Menu Order: ">
                    </asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMenuOrder" runat="server" Text="1">
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
                                        <asp:Button ID="btnClear" runat="server" Text="Back To add" Visible="false" OnClick="btnClear_Click" />
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
                            <asp:GridView ID="gvModule" runat="server" AutoGenerateColumns="false" CssClass="gridCss">
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbSelect" runat="server" CommandArgument='<%#Eval("ModuleID") %>'
                                                OnClick="lbSelect_Click">
                                                    Edit
                                                </asp:LinkButton>
                                            | <a href='AdminPageInsertUpdate.aspx?ModuleID=<%#Eval("ModuleID") %>'>Add Page</a>
                                            | <a href='AdminMenuInsertUpdate.aspx?ModuleID=<%#Eval("ModuleID") %>'>Add Menu</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ModuleName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblModuleName" runat="server" Text='<%#Eval("ModuleName") %>'>
                        </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DefaultURL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDefaultURL" runat="server" Text='<%#Eval("DefaultURL") %>'>
                        </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order">
                                        <ItemTemplate>
                                            <%#Eval("MenuOrder") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="AddedBy">
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
                </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowStatusID" runat="server" Text='<%#Eval("RowStatusID") %>'>
                        </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%#Eval("ModuleID") %>'
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
