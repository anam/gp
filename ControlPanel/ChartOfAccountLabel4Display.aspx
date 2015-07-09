<%@ Page Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true"
    CodeFile="ChartOfAccountLabel4Display.aspx.cs" Inherits="AdminACC_ChartOfAccountLabel4Display" Title="Chart Of Account Label-3" %>

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
 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
 <div class="tableCss">
 <h1>Chart Of Account Label-4</h1>
        <table>
            <tr class="invisible">
                <td>
                    <asp:Label ID="lblCode" runat="server" Text="Code: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCode" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblACC_ChartOfAccountLabel2ID" runat="server" Text="Type: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlACC_HeadType" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlCOAL3" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblChartOfAccountLabel4Text" runat="server" Text="Title: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtChartOfAccountLabel4Text" runat="server" Text="" Width="800px">
                    </asp:TextBox>
                </td>
            </tr>
            <tr >
                <td>
                    <asp:Label ID="lblExtraField1" runat="server" Text="Item Code<br/>/Designation: ">
                    </asp:Label>
                </td>
                <td>
                    
                    <asp:Button ID="btnLoadItemCode" runat="server" Text="load Item Code" 
                        onclick="btnLoadItemCode_Click" />
                    <asp:TextBox ID="txtExtraField1" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblExtraField2" runat="server" Text="Last #: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExtraField2" runat="server" Text="0" Width="800px">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblExtraField3" runat="server" Text="Contact #: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExtraField3" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr class="invisible">
                <td>
                    <asp:Label ID="lblAddedBy" runat="server" Text="AddedBy: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAddedBy" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr class="invisible">
                <td>
                    <asp:Label ID="lblUpdatedBy" runat="server" Text="UpdatedBy: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUpdatedBy" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr class="invisible">
                <td>
                    <asp:Label ID="lblUpdatedDate" runat="server" Text="UpdatedDate: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUpdatedDate" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblRowStatusID" runat="server" Text="Status: ">
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
                    
                    <asp:HiddenField ID="hfaCC_ChartOfAccountLabel4ID" runat="server" />
                    <asp:Button ID="btnClear" Visible="false" runat="server" Text="Clear" OnClick="btnClear_Click" />
                </td>
            </tr>
        </table>
    </div>

    <div>
    <h1><asp:DropDownList ID="ddlStatusSearch" runat="server" AutoPostBack="True"  onselectedindexchanged="ddlStatusSearch_SelectedIndexChanged">
                    </asp:DropDownList>
                    Chart Of Account Label-4 List</h1>
        <asp:GridView ID="gvACC_ChartOfAccountLabel4" runat="server" AutoGenerateColumns="false" CssClass="gridCss">
            <Columns>
                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbSelect" runat="server" CommandArgument='<%#Eval("ACC_ChartOfAccountLabel4ID") %>' OnClick="lbSelect_Click">
                            Select
                        </asp:LinkButton>
                        <asp:HiddenField ID="hfChartOfAccountLabel4ID" runat="server" Value='<%#Eval("ACC_ChartOfAccountLabel4ID") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="Code">
                    <ItemTemplate>
                        <asp:Label ID="lblCode" runat="server" Text='<%#Eval("Code") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <%--<asp:TemplateField HeaderText="Label-1">
                    <ItemTemplate>
                        <asp:Label ID="lblACC_ChartOfAccountLabel2ID" runat="server" Text='<%#Eval("ExtraField1") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <%--<asp:TemplateField HeaderText="Label-1">
                    <ItemTemplate>
                        <asp:Label ID="lblACC_ChartOfAccountLabel2ID" runat="server" Text='<%#Eval("ACC_ChartOfAccountLabel2ID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Title">
                    <ItemTemplate>
                        <asp:Label ID="lblChartOfAccountLabel4Text" runat="server" Text='<%#Eval("ChartOfAccountLabel4Text") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Hide?">
                    <ItemTemplate>                        
                            <asp:CheckBox ID="chkVisibility" runat="server" Checked='<%#Eval("IsVisible") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="ExtraField1">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraField1" runat="server" Text='<%#Eval("ExtraField1") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ExtraField2">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraField2" runat="server" Text='<%#Eval("ExtraField2") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ExtraField3">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraField3" runat="server" Text='<%#Eval("ExtraField3") %>'>
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
                        <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%#Eval("ACC_ChartOfAccountLabel4ID") %>' OnClick="lbDelete_Click">
                            Delete
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
        <center>
            
                                <asp:Button ID="btnVisibilitySave" runat="server" Text="Save" 
                                    onclick="btnVisibilitySave_Click" />
        </center>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
