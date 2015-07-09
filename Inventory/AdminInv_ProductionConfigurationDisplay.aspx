<%@ Page Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true"
    CodeFile="AdminInv_ProductionConfigurationDisplay.aspx.cs" Inherits="AdminInv_ProductionConfigurationDisplay" Title="Display Inv_ProductionConfiguration By Admin" %>

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
        <asp:HiddenField ID="hfConfigID" Value="0" runat="server" />
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
    <br />
    <h1>Production Configuration</h1>
        <asp:GridView ID="gvInv_ProductionConfiguration" runat="server" AutoGenerateColumns="false" CssClass="gridCss">
            <Columns>
                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbSelect" runat="server" CommandArgument='<%#Eval("Inv_ProductionConfigurationID") %>' OnClick="lbSelect_Click">
                            Select
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Product">
                    <ItemTemplate>
                        <asp:Label ID="lblProductID" runat="server" Text='<%#Eval("ExtraField1") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RawMaterial">
                    <ItemTemplate>
                        <asp:Label ID="lblRawMaterialID" runat="server" Text='<%#Eval("ExtraField2") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Quality">
                    <ItemTemplate>
                        <asp:Label ID="lblQualityValue" runat="server" Text='<%#Eval("QualityValue") %>'>
                        </asp:Label> &nbsp;
                        <asp:Label ID="lblQualityUnitID" runat="server" Text='<%#Eval("ExtraField4") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:Label ID="lblQuantityValue" runat="server" Text='<%#Eval("QuantityValue") %>'>
                        </asp:Label>&nbsp;
                        <asp:Label ID="lblQuantityUnitID" runat="server" Text='<%#Eval("ExtraField3") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%#Eval("Inv_ProductionConfigurationID") %>' OnClick="lbDelete_Click">
                            Delete
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
   </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
