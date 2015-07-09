<%@ Page Language="C#" MasterPageFile="~/login/AdminMaster.master" AutoEventWireup="true"
    CodeFile="ProductSearchShowRoom.aspx.cs" Inherits="AdminPos_ProductDisplay" Title="Display Pos_Product By Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .gridCss
        {
            width: 100%;
            padding: 20px 10px 10px 10px;
            text-align: center;
        }
        .alignRight{text-align:right;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <h1>
        <asp:Label ID="lblVoucherType" runat="server" Text=""></asp:Label>
    </h1>
        <asp:HiddenField ID="hfLoginID" runat="server" />
            <table>
                <tr>
                    <td>
                        Product
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProduct" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        Type
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPos_ProductStatus" runat="server">
                        </asp:DropDownList>
                    </td>

                    <td>
                        Design Code
                    </td>
                    <td>
                        <asp:TextBox ID="txtDesignCode" runat="server" Width="100px"></asp:TextBox>
                        BarCode
                    </td>
                    <td colspan="3">
                    
                        <asp:TextBox ID="txtBarCode" runat="server" Width="330px" Height="60px" AutoPostBack="true" TextMode="MultiLine"
                            ontextchanged="txtBarCode_TextChanged" ></asp:TextBox>
                        <asp:HiddenField ID="hfBarcodeSearch" runat="server" />
                    </td>
                </tr>
                <tr>
                <td>
                        Color
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlColorSearch" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        Size
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPos_Size" runat="server">
                        </asp:DropDownList>
                    </td>
                    
                    <td>
                        Workstation
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlWorkStationSearch" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td >
                        <asp:Button ID="btnClearSearch" runat="server" Text="Reset" OnClick="btnClearSearch_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="Add/Search" OnClick="btnSearch_Click" />
                    </td>
                    <td>
                    
                    <asp:CheckBox ID="chkComaSystem" runat="server" Text="( , ) Sys"/>
                    </td>
                </tr>
                
            </table>

        <asp:GridView ID="gvPos_Product" runat="server" 
            ShowFooter="true"
            AutoGenerateColumns="false" CssClass="gridCss">
            <Columns>
                <asp:TemplateField HeaderText="">
                     <ItemTemplate>
                        <%# Container.DataItemIndex +1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Product">
                    <ItemTemplate>
                        <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("ProductName") %>'>
                        </asp:Label>
                        <asp:HiddenField ID="hfPos_ProductID" runat="server"  Value='<%#Eval("Pos_ProductID") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="BarCode">
                    <ItemTemplate>
                        <asp:Label ID="lblBarCode" runat="server" Text='<%#Eval("BarCode") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Size">
                    <ItemTemplate>
                        <asp:Label ID="lblSizeName" runat="server" Text='<%#Eval("SizeName") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Color">
                    <ItemTemplate>
                        <asp:Label ID="lblColorName" runat="server" Text='<%#Eval("ColorName") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lblProductStatusName" runat="server" Text='<%#Eval("ProductStatusName") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Stock">
                    <ItemTemplate>
                        <asp:Label ID="lblStock" runat="server" Text='<%#Eval("Stock","{0:0,0}") %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblStockFooter" runat="server">
                        </asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Qty">
                    <ItemTemplate>
                        <asp:TextBox ID="txtQty" runat="server" Width="100px"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sale Price">
                    <ItemTemplate>
                        <asp:Label ID="lblSalePrice" runat="server" CssClass="alignRight" Text='<%#Eval("SalePrice","{0:0,0.00}") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount">
                    <ItemTemplate>
                        <asp:Label ID="lblStockSalePrice" runat="server" Text='<%#Eval("StockSalePrice","{0:0,0.00}") %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblStockSalePriceFooter" runat="server">
                        </asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <span style="color:Red;font-size:20px;text-align:center;">No Product available in stock for the above search condition</span>
            </EmptyDataTemplate>
        </asp:GridView>
        
        </ContentTemplate>
    </asp:UpdatePanel>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table>
        <tr>
            <td>Old ID</td>
            <td>
                <asp:TextBox ID="txtOldTransactionID" Width="50px" Text="" runat="server"></asp:TextBox>
            </td>
            <td>Work Station</td>
            <td>
                <asp:DropDownList ID="ddlWorkStation" runat="server">
                        </asp:DropDownList>
            </td>
            <td>Date</td>
            <td>
                
                <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender2" runat="server"
                            TargetControlID="txtDate">
                        </ajaxToolkit:CalendarExtender>
            </td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                    onclick="btnSubmit_Click" />
            </td>
            <td>
                        <asp:HyperLink ID="hlnkProductionPrint" Visible="false" Target="_blank" runat="server">Print Voucher</asp:HyperLink>
            </td>
        </tr>
    </table>

        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
