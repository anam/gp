<%@ Page Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true" 
CodeFile="AdminInv_ProductionConfigurationInsertUpdate.aspx.cs" Inherits="AdminInv_ProductionConfigurationInsertUpdate" Title="Inv_ProductionConfiguration Insert/Update By Admin" %>

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
    <h1>Production Configuration</h1>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblProductID" runat="server" Text="Product: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlProduct" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            
            <tr>
                <td>
                    <asp:Label ID="lblRawMaterialID" runat="server" Text="RawMaterial: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlRawMaterial" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblQualityValue" runat="server" Text="Quality: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtQualityValue" runat="server" Text="0">
                    </asp:TextBox>
                    <asp:DropDownList ID="ddlQualityUnit" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            
            <tr>
                <td>
                    <asp:Label ID="lblQuantityValue" runat="server" Text="Quantity: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtQuantityValue" runat="server" Text="">
                    </asp:TextBox>
                    <asp:DropDownList ID="ddlQuantityUnit" runat="server">
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
                
                    <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" Visible="false"/>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
