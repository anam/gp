<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockReportInShowroomByItemWise.aspx.cs"
    Inherits="Inventory_PurchasePrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Showroom stock</title>
    <style type="text/css">
        .PurchaseHeaderCss
        {
            font-weight: bold;
        }
        td{padding:5px;}
        #tableHeader td
        {
            border-left: 1px solid black;
            font-weight: bold;
            text-align: center;
            border-bottom: 1px solid black;
            border-top: 1px solid black;
        }
        .itemCss td
        {
            border-bottom: 1px solid black;
            border-left: 1px solid black;
            text-align: center;
        }
        #itemList_tbl td
        {
            padding: 2px 10px;
        }
        #lastRow td
        {
            border-top: 1px solid black;
            border-bottom: 1px solid black;
            font-weight: bold;
            text-align: right;
        }
        .subtotalRow
        {
            font-weight: bold;
            text-align: right;
            border-top: 1px solid black;
            border-bottom: 1px solid black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="font-family: Verdana; font-size: 12px;">
        <table width="800px">
            <tr>
                <td align="center" style="border-bottom: 1px solid Black;">
                    <b style='font-size: 20px'>GENTLE PARK</b>
                    <br />
                    334, Ferdousi Plaza, (4th Floor).<br />
                    Elephant Road, Dhaka 1205, Tel: 02-9612452
                </td>
            </tr>
            <tr>
                <td align="center">
                    <span style='font-size: 14px; font-weight: bold; text-decoration: unerline;'>Current
                        Product in Showroom Stock</span>
                    <br />
                    <b>Date: </b>
                    <asp:Label ID="lblStockDate" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <b>Print Date: </b>
                    <asp:Label ID="lblPrintDate" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <table width="800px">
            <tr>
                <td>
                    <asp:GridView ID="gvItem" runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="S.N.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product">
                                <ItemTemplate>
                                    <%#Eval("Product")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Style">
                                <ItemTemplate>
                                    <%#Eval("Style")%>
                                </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate>
                                    <%#Eval("Size")%>
                                </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:TemplateField HeaderText="Color">
                                <ItemTemplate>
                                    <%#Eval("Color")%>
                                </ItemTemplate>
                            </asp:TemplateField>                          
                            <asp:TemplateField HeaderText="SalePrice">
                                <ItemTemplate>
                                    <%#Eval("SalePrice")%>
                                </ItemTemplate>
                            </asp:TemplateField>                        
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <%#Eval("Amount","{0:0,0.00}")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="BarCode">
                                <ItemTemplate>
                                   <asp:Label ID="lblBarcode" runat="server" Text='<%#Eval("BarCode")%>'></asp:Label> 
                                </ItemTemplate>
                            </asp:TemplateField>                         
                            <asp:TemplateField HeaderText="Stock">
                                <ItemTemplate>
                                   <asp:Label ID="lblStock" runat="server" Text='<%#Eval("Stock","{0:0}")%>'></asp:Label> 
                                    
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="inventory">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtInventory" Width="50px" runat="server" Text='<%#Eval("Stock","{0:0}")%>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Adjustment">
                                <ItemTemplate>
                                    <asp:Label ID="lblAdjustment" runat="server" Text="0"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>  
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Preview Adjustment" 
                        onclick="btnSubmit_Click" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        <table width="800px">
            <tr>
                <td align="left" style="padding: 0 50px; border-top: 1px dashed black; width: 80px;">
                    Received By
                </td>
                <td>
                </td>
                <td align="center" style="padding: 0 50px; border-top: 1px dashed black; width: 110px;">
                    Store in Charge
                </td>
                <td>
                </td>
                <td align="right" style="padding: 0 50px; border-top: 1px dashed black; width: 140px;">
                    Authorised Signature
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
