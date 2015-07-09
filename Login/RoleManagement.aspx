<%@ Page Title="" Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true" CodeFile="RoleManagement.aspx.cs" Inherits="Login_RoleManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table>
<tr>
        <td>
        <div class="tableCss_roleManagement">
        <h1>Role Management</h1>
            
            <table>
            <tr>
                <td>
                    <asp:Label ID="lblRoleName" runat="server" Text="RoleName: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRoleName" runat="server" Text="">
                    </asp:TextBox>
                    <asp:HiddenField ID="hfRoleID" runat="server" />
                </td>
            </tr>
            <tr style="Display:none;">
                <td>
                    <asp:Label ID="lblRoleDescription" runat="server" Text="RoleDescription: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRoleDescription" runat="server" Text="" TextMode="MultiLine">
                    </asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td>
                
                </td>
                <td>
                    
                    <asp:Button ID="btnAddNewRole" runat="server" Text="Add New Role" 
                        onclick="btnAddNewRole_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update Role" 
                        onclick="btnUpdate_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="User: ">
                    </asp:Label>
                </td>
                <td>
                    
                <asp:DropDownList ID="ddlLogin" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="ddlLogin_SelectedIndexChanged">
                </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                   
                </td>
                <td>
                    <asp:GridView ID="gvRole" runat="server" AutoGenerateColumns="false" CssClass="gridCss">
                        <Columns>
                        
                            <asp:TemplateField HeaderText="Role">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" Text='<%#Eval("RoleName") %>' ToolTip='<%#Eval("RoleID") %>'/>                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbSelect" runat="server" CommandArgument='<%#Eval("RoleID") %>' OnClick="lbSelect_Click">
                                        Edit
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%#Eval("RoleID") %>' OnClick="lbDelete_Click">
                                        Delete
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    
                </td>
                <td>
                    <asp:Button ID="btnRoleAssignSave" runat="server" Text="Save" 
                        onclick="btnRoleAssignSave_Click" />
                </td>
            </tr>
            </table>
            </div>
        </td>
    </tr>
    <tr>
        <td>        
        
<div class="tableCss">
        <table>
            <tr>
                <td valign="top">
                    <asp:Label ID="Label1" runat="server" Text="Role: ">
                    </asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="ddlRole_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td  colspan="2">
                    <asp:GridView ID="gvModule" runat="server" AutoGenerateColumns="false" 
                        CssClass="gridCss" onrowdatabound="gvRole_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Module">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" Text='<%#Eval("ModuleName") %>' ToolTip='<%#Eval("ModuleID") %>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                   <a target="_blank" href='ModuleManagement.aspx?ModuleID=<%#Eval("ModuleID") %>'>Edit</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pages">
                                <ItemTemplate>
                                    <asp:GridView ID="gvPagenMenu" runat="server" AutoGenerateColumns="false" CssClass="gridCss"
                                     onrowdatabound="gvPagenMenu_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Page">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelectPageNMenu" runat="server" Text='<%#Eval("PageTitle") %>' ToolTip='<%#Eval("PageID") %>' ValidationGroup='<%#Eval("MenuID") %>'/>                                   
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                   <a target="_blank" href='AdminPageInsertUpdate.aspx?PageID=<%#Eval("PageID") %>'>Edit</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Menu">
                                                <ItemTemplate>
                                                    <%#Eval("MenuTitle") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                   <a target="_blank" href='AdminMenuInsertUpdate.aspx?MenuID=<%#Eval("MenuID") %>'>Edit</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Buttons">
                                                <ItemTemplate>
                                                    <asp:DataList ID="dlButton" runat="server" CssClass="tableCss_roleManagement" RepeatColumns="2" RepeatDirection="Horizontal">
                                                        <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelectButton" runat="server" Text='<%#Eval("ButtonText") %>' ToolTip='<%#Eval("ButtonID") %>'/>                                   
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                    <%--<asp:GridView ID="gvButton" runat="server" ShowHeader="true" AutoGenerateColumns="false" CssClass="gridCss">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelectButton" runat="server" Text='<%#Eval("ButtonText") %>' ToolTip='<%#Eval("ButtonID") %>'/>                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                   <a target="_blank" href='AdminButtonInsertUpdate.aspx?ButtonID=<%#Eval("ButtonID") %>'>Edit</a>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    
                </td>
                <td>
                    <asp:Button ID="btnRoleWisePageNMenuAccess" runat="server" Text="Save" 
                        onclick="btnRoleWisePageNMenuAccess_Click"/>
                </td>
            </tr>
        </table>

    </div>
</td>
    </tr>
    
</table>

</asp:Content>

