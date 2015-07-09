<%@ Page Language="C#" MasterPageFile="~/login/AdminMaster.master" AutoEventWireup="true"
    CodeFile="Sales.aspx.cs" Inherits="AdminPos_ProductDisplay" Title="Display Pos_Product By Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .gridCss
        {
            width: 100%;
            padding: 20px 10px 10px 10px;
            text-align: center;
        }
        .alignRight
        {
            text-align: right;
        }
        input[type=text]
        {
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
        
        <h1>
            Sales Return In
        </h1>
        <table>
            <tr>
                <td>
                    Invoice #:
                </td>
                <td>
                    <asp:TextBox ID="txtInvoiceNo" runat="server" AutoPostBack="True" 
                        ontextchanged="txtInvoiceNo_TextChanged" ></asp:TextBox>
                    <asp:HiddenField ID="hfInvoiceNo" runat="server" />
                </td>
                <td>
                    Barcode:
                </td>
                <td>
                    <asp:TextBox ID="txtBarCodeSalesReturn" runat="server" AutoPostBack="True" 
                        ontextchanged="txtBarCodeSalesReturn_TextChanged" ></asp:TextBox>
                    <asp:HiddenField ID="hfBarcodeReturn" runat="server" />
                </td>
                <td>
                    <asp:Button ID="btnProcessReturn" runat="server" Text="Process Return" 
                        onclick="btnProcessReturn_Click" />
                </td>
                <td>
                    <asp:Button ID="btnClearReturn" runat="server" Text="Reset" 
                        onclick="btnClearReturn_Click" />
                </td>
            </tr>
        </table>
         <asp:GridView ID="gvReturnInvoice" runat="server" ShowFooter="true" AutoGenerateColumns="false"
                CssClass="gridCss">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <%# Container.DataItemIndex +1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Invoice #">
                        <ItemTemplate>
                            <asp:Label ID="lblInvoiceNo" runat="server" Text='<%#Eval("TransactionID") %>'>
                        </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mobile">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerMobileNo" runat="server" Text='<%#Eval("CustomerMobileNo") %>'>
                        </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Product">
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("ProductName") %>'>
                        </asp:Label>
                            <asp:HiddenField ID="hfPos_TransactionID" runat="server" Value='<%#Eval("Pos_TransactionID") %>' />
                            <asp:HiddenField ID="hfPos_TransactionMasterID" runat="server" Value='<%#Eval("Pos_TransactionMasterID") %>' />
                            <asp:HiddenField ID="hfPos_ProductID" runat="server" Value='<%#Eval("Pos_ProductID") %>' />
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
                    <asp:TemplateField HeaderText="Sold Qty">
                        <ItemTemplate>
                            <asp:Label ID="lblSoldQty" runat="server" Text='<%#Eval("Stock","{0:0,0}") %>'>
                        </asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblSoldQtyFooter" runat="server">
                        </asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rtn Qty">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRtnQty" runat="server" Text="0" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit Price">
                        <ItemTemplate>
                            <asp:Label ID="lblSalePrice" runat="server" CssClass="alignRight" Text='<%#Eval("SalePrice","{0:0,0}") %>'>
                        </asp:Label>
                            <asp:HiddenField ID="hfTotalCostPerProduct" runat="server" Value='<%#Eval("TotalCostPerProduct") %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="total Sale Price">
                        <ItemTemplate>
                            <asp:Label ID="lblStockSalePrice" runat="server" Text='<%#Eval("StockSalePrice","{0:0,0}") %>'>
                        </asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblStockSalePriceFooter" runat="server">
                        </asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Discount">
                        <ItemTemplate>
                            <asp:Label ID="lblDiscountAmount" runat="server"  Text='<%#Eval("DiscountAmount","{0:0}") %>'></asp:Label>
                           
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblDiscountFooter" runat="server">
                        </asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vat">
                        <ItemTemplate>
                            <asp:Label ID="lblVat" runat="server"  Text='<%#Eval("VatAmount","{0:0}") %>'></asp:Label>
                            <asp:HiddenField ID="hfVatPercentageAlways" runat="server" Value='<%#Eval("VatPercentageAlways") %>'/>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblVatFooter" runat="server">
                        </asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount Paid">
                        <ItemTemplate>
                            <asp:Label ID="lblTotal" runat="server"  Text='<%#Eval("Total","{0:0,0}") %>'></asp:Label>
                           
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotalFooter" runat="server">
                        </asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <span style="color: Red; font-size: 20px; text-align: center;">Invalid invoice # or Barcode</span>
                </EmptyDataTemplate>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfLoginID" runat="server" />

            <h1>
        <asp:Label ID="lblVoucherType" runat="server" Text=""></asp:Label>
    </h1>
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
                    </td>
                    <td>
                        BarCode
                    </td>
                    <td>
                        <asp:TextBox ID="txtBarCode" runat="server" Width="100px" AutoPostBack="True" 
                            ontextchanged="txtBarCode_TextChanged"></asp:TextBox>
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
                    <td colspan="2">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    </td>
                    
                </tr>
                
            </table>
            <asp:GridView ID="gvPos_Product" runat="server" ShowFooter="true" AutoGenerateColumns="false"
                CssClass="gridCss">
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
                            <asp:HiddenField ID="hfPos_ProductID" runat="server" Value='<%#Eval("Pos_ProductID") %>' />
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
                            <asp:TextBox ID="txtQty" runat="server" Text="1" Width="50px" Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sale Price">
                        <ItemTemplate>
                            <asp:Label ID="lblSalePrice" runat="server" CssClass="alignRight" Text='<%#Eval("SalePrice","{0:0,0.00}") %>'>
                        </asp:Label>
                            <asp:HiddenField ID="hfTotalCostPerProduct" runat="server" Value='<%#Eval("TotalCostPerProduct") %>'/>
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
                    <asp:TemplateField HeaderText="Vat %">
                        <ItemTemplate>
                            <asp:TextBox ID="txtVatGrid"  runat="server" Text='<%#Eval("VatPercentage","{0:0.00}") %>' Enabled="false"
                                Width="50px"></asp:TextBox>
                            <%--<asp:HiddenField ID="hfVatPercentageAlways" runat="server" Value="5"/>--%>
                            <asp:HiddenField ID="hfVatPercentageAlways" runat="server" Value='<%#Eval("VatPercentageAlways") %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="D %">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDiscountGrid"  Enabled="false" runat="server" Text='<%#Eval("DiscountPercentage","{0:0.00}") %>'
                                Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Discount">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDiscountAmountGrid" runat="server" Enabled="false" Text='<%#Eval("DiscountAmount","{0:0.00}") %>'
                                Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <span style="color: Red; font-size: 20px; text-align: center;">No Product available
                        in stock for the above search condition</span>
                </EmptyDataTemplate>
            </asp:GridView>
            <table width="100%">
                <tr>
                    <td align="center">
            <asp:Button ID="btnAddToPreview" runat="server" Text="Add To Preview" OnClick="btnAddToPreview_Click" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        Customer
                    </td
                    <td>
                        <asp:TextBox ID="txtCustomerName" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td>
                        Customer ID
                    </td>
                    <td>
                        <asp:TextBox ID="txtCustomerID" runat="server" AutoPostBack="true" OnTextChanged="txtCustomerID_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        invoice #
                    </td>
                    <td>
                        <asp:TextBox ID="txtOldTransactionID" Text="" runat="server" Enabled="false" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Card no
                    </td>
                    <td>
                        <asp:TextBox ID="txtCardNo" runat="server" AutoPostBack="true" OnTextChanged="txtCardNo_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        Contact #
                    </td>
                    <td>
                        <asp:TextBox ID="txtContactNo" runat="server" AutoPostBack="true" OnTextChanged="txtContactNo_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        Discount %
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtDiscountPercentage" runat="server" Width="50px" Text="0.00" ></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnApplyDiscount" runat="server" Text="Apply" OnClick="btnApplyDiscount_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnAuto" runat="server" Visible="false" Text="Auto" OnClick="btnAuto_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        Address
                    </td>
                    <td>
                        <asp:TextBox ID="txtCustomerAddress" runat="server" Width="200px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td>
                        Remarks
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtNote" runat="server" Width="400px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvPreview" runat="server" ShowFooter="true" AutoGenerateColumns="false"
                CssClass="gridCss">
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
                            <asp:HiddenField ID="hfPos_ProductID" runat="server" Value='<%#Eval("Pos_ProductID") %>' />
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
                            <asp:Label ID="lblSizeName" runat="server" Text='<%#Eval("ExtraField10") %>'>
                        </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Color">
                        <ItemTemplate>
                            <asp:Label ID="lblColorName" runat="server" Text='<%#Eval("ExtraField2") %>'>
                        </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblProductStatusName" runat="server" Text='<%#Eval("ExtraField3") %>'>
                        </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Stock">
                        <ItemTemplate>
                            <asp:Label ID="lblStock" runat="server" Text='<%#Eval("ExtraField4") %>'>
                        </asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblStockFooter" runat="server">
                        </asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty">
                        <ItemTemplate>
                            <asp:TextBox ID="txtQty" runat="server"  Enabled="false" Width="50px" Text='<%#Eval("ExtraField1") %>'
                                AutoPostBack="true" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblQtyFooter" runat="server">
                        </asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sale Price">
                        <ItemTemplate>
                            <asp:Label ID="lblSalePrice" runat="server" CssClass="alignRight" Text='<%#Eval("ExtraField5") %>'>
                        </asp:Label>
                            <asp:HiddenField ID="hfTotalCostPerProduct" runat="server" Value='<%#Eval("OthersCost") %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sale Amount">
                        <ItemTemplate>
                            <asp:Label ID="lblStockSalePrice" runat="server" Text='<%#Eval("ExtraField6") %>'>
                        </asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblStockSalePriceFooter" runat="server">
                        </asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="D %">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDiscountGrid" runat="server" Enabled="false" AutoPostBack="true" OnTextChanged="txtQty_TextChanged"
                                Text='<%#Eval("DiscountPercentage","{0:0.00}") %>' Width="40px"></asp:TextBox>
                            <asp:HiddenField ID="hfDiscountGrid" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Discount<br/>Amount">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDiscountAmountGrid" runat="server" Enabled="false" AutoPostBack="true" OnTextChanged="txtQty_TextChanged"
                                Text='<%#Eval("DiscountAmount","{0:0.00}") %>' Width="50px"></asp:TextBox>
                            <asp:HiddenField ID="hfDiscountAmountGrid" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total<br/>Discount<br/>Amount">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDiscountAmountTotal" runat="server" Enabled="false" Text='<%#Eval("FabricsCost","{0:0.00}") %>'
                                Width="50px"></asp:TextBox>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblDiscountAmountTotalFooter" runat="server">
                        </asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vat %">
                        <ItemTemplate>
                            <asp:TextBox ID="txtVatGrid" runat="server" AutoPostBack="true"  OnTextChanged="txtQty_TextChanged" Enabled="false"
                                Text='<%#Eval("ExtraField8") %>' Width="40px"></asp:TextBox>
                            <asp:HiddenField ID="hfVatGrid" runat="server" />
                            <asp:HiddenField ID="hfVatPercentageAlways" runat="server" Value='<%#Eval("VatPercentage") %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vat Amount">
                        <ItemTemplate>
                            <asp:TextBox ID="txtVatGridTotal" runat="server" Enabled="false" Text='<%#Eval("AccesoriesCost","{0:0.00}") %>'
                                Width="40px"></asp:TextBox>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblVatGridTotalFooter" runat="server">
                        </asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sub Total">
                        <ItemTemplate>
                            <asp:Label ID="lblSubTotalSalePrice" runat="server" Text='<%#Eval("ExtraField9") %>'>
                        </asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblSubTotalSalePriceFooter" runat="server">
                        </asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%#Eval("Pos_ProductID") %>'
                                OnClick="lbDeletePreviewGrid_Click">
                                X
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <span style="color: Red; font-size: 20px; text-align: center;">No Product Added</span>
                </EmptyDataTemplate>
            </asp:GridView>
            <table>
                <tr>
                    <td width="730px" valign="top">
                        <div>
                            <table>
                                <tr>
                                    <td colspan="2">
                                        <b style="font-size: 20px; text-align: right;">Payment Mode</b>
                                    </td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="ddlPaymentMood" runat="server" AutoPostBack="false" Enabled="false" OnSelectedIndexChanged="ddlPaymentMood_SelectedIndexChanged">
                                            <asp:ListItem Text="Cash & Credit" Value="Cash & Credit"></asp:ListItem>
                                            <asp:ListItem Text="Cash" Value="Cash"></asp:ListItem>
                                            <asp:ListItem Text="Credit" Value="Credit"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td colspan="5">
                                        <table>
                                            <tr>
                                                <td>
                                                    Total Qty:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTotalQty" runat="server" Text="0" Font-Size="20"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Card no
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCardNoPayment" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        Card Type
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCardType" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddlCardType_SelectedIndexChanged">
                                            <asp:ListItem Text="Master Card" Value="Master Card"></asp:ListItem>
                                            <asp:ListItem Text="VISA Card" Value="VISA Card"></asp:ListItem>
                                            <asp:ListItem Text="AMEX" Value="AMEX"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Issue From
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlIssueBank" runat="server">
                                            <asp:ListItem Text="DBBL" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="City Bank" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Amount
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAmount" Width="75px" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAddCardPayment" runat="server" Text="Add" Enabled="true" OnClick="btnAddCardPayment_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="9">
                                        <asp:GridView ID="gvPayment" runat="server" ShowFooter="true" AutoGenerateColumns="false"
                                            CssClass="gridCss">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Card No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCardNo" runat="server" Text='<%#Eval("ExtraField1") %>'>
                                                    </asp:Label>
                                                        <asp:HiddenField ID="hfSerial" runat="server" Value='<%#Eval("Pos_ProductID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Card Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCardType" runat="server" Text='<%#Eval("ExtraField2") %>'>
                                                    </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issue From">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIssueFrom" runat="server" Text='<%#Eval("ExtraField3") %>'>
                                                    </asp:Label>
                                                        <asp:HiddenField ID="hfIssueFromID" runat="server" Value='<%#Eval("ExtraField4") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("ExtraField5") %>'>
                                                    </asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotalCardAmountFooter" runat="server" Text=""></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%#Eval("Pos_ProductID") %>'
                                                            OnClick="lbDeletePayment_Click">
                                                        X
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td valign="top">
                        <div>
                            <table style="text-align: right;">
                                <tr>
                                    <td>
                                        Sub Total
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSubTotal" Width="75px" Enabled="false" runat="server" Text="0.00"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Discount
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDiscount" Width="75px" runat="server" Enabled="false" Text="0.00"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        VAT
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtVat" Width="75px" runat="server" Enabled="false" Text="0.00"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Amt Payable
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPayableAmount" Width="75px" runat="server" Enabled="false" Text="0.00"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Cash Amt
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCashAmount" Width="75px" runat="server" AutoPostBack="true" OnTextChanged="txtCashAmount_TextChanged"
                                            Text="0.00"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Rtn Amt
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReturnAmount" Width="75px" runat="server" Enabled="false" AutoPostBack="true" OnTextChanged="txtCashAmount_TextChanged"
                                            Text="0.00"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Card Amt
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCardAmount" Width="75px" runat="server" Enabled="false" Text="0.00"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Paid Amount
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPaidAmount" Width="75px" runat="server" Enabled="false" Text="0.00"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Refund/Due
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRefundOrDue" Width="75px" runat="server" Enabled="false" Text="0.00"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:DropDownList ID="ddlSalesPersonAll" runat="server" Visible="false">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlSalesPerson" runat="server">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlRefference" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBackDatedSale" runat="server"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender4" runat="server" TargetControlID="txtBackDatedSale">
                                    </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:HyperLink ID="hlnkProductionPrint" Visible="false" Target="_blank" runat="server">Print Invoice</asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
