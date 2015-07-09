<%@ Page Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true" 
CodeFile="AdminACC_ChartOfAccountLabel1InsertUpdate.aspx.cs" Inherits="AdminACC_ChartOfAccountLabel1InsertUpdate" Title="ACC_ChartOfAccountLabel1 Insert/Update By Admin" %>

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
    <h1>Chart Of Account Label-1</h1>
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
                    <asp:Label ID="lblChartOfAccountLabel1Text" runat="server" Text="Text: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtChartOfAccountLabel1Text" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr class="invisible">
                <td>
                    <asp:Label ID="lblExtraField1" runat="server" Text="ExtraField1: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExtraField1" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr class="invisible">
                <td>
                    <asp:Label ID="lblExtraField2" runat="server" Text="ExtraField2: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExtraField2" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr class="invisible">
                <td>
                    <asp:Label ID="lblExtraField3" runat="server" Text="ExtraField3: ">
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
                
                    <asp:Button ID="btnClear" runat="server" class="invisible" Text="Clear" OnClick="btnClear_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
