<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ItemHistoryPrint.aspx.cs" Inherits="Inventory_PurchasePrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .PurchaseHeaderCss{font-weight:bold;}
        #tableHeader td{border-left:1px solid black;font-weight:bold;text-align:center; border-bottom:1px solid black;}
        .itemCss td{border-bottom:1px solid black;border-left:1px solid black;text-align:center;}
        #itemList_tbl td{padding:2px 10px;}
        #lastRow td{border-top:1px solid black;border-bottom:1px solid black;font-weight:bold;text-align:right;}
        .subtotalRow{font-weight:bold;text-align:right;}
        th{padding:5px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="font-family:Verdana;font-size:12px;">
        <table width="900px">
            <tr>
                <td align="center"  style="border-bottom:1px solid Black;">
                    <b style='font-size:20px'>GENTLE PARK</b>
                    <br />
                    334, Ferdousi Plaza, (4th Floor).<br />Elephant Road,
                    
                    Dhaka 1205, Tel: 02-9612452
                </td>
            </tr>
            <tr>
                <td align="center" >
                    <span style='font-size:14px;font-weight:bold;text-decoration:unerline;'>Item History<span>

                </td>
            </tr>
            
            <tr>
                <td align="right" >
                    <b >Print Date: </b> 
                    <asp:Label ID="lblPrintDate" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:GridView ID="gvInv_Item" runat="server" AutoGenerateColumns="false" CssClass="gridCss">
            <Columns>
                
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:Label ID="lblItemName" runat="server" Text='<%#Eval("ItemName") %>'>
                        </asp:Label>
                        <%--(<%#Eval("RawMaterialTypeName") %>)--%>
                        <br />
                        <asp:Label ID="lblItemCode" runat="server" Text='<%#Eval("ItemCode") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Prch ID">
                    <ItemTemplate>
                        <a style="text-decoration:underline;color:blue;" href='PurchasePrint.aspx?PurchaseID=<%#Eval("PurchaseID") %>' target="_blank"><asp:Label ID="lblPurchaseID" runat="server" Text='<%#Eval("PurchaseID") %>'>
                        </asp:Label></a>
                        <asp:HiddenField ID="hfPurchaseID" runat="server" Value='<%#Eval("PurchaseID") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quality">
                    <ItemTemplate>
                        <asp:Label ID="lblQualityValue" runat="server" Text='<%#Eval("QualityValue") %>'>
                        </asp:Label>
                        &nbsp;
                        <asp:Label ID="lblQualityUnitID" runat="server" Text='<%#Eval("QualityUnitName") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit">
                    <ItemTemplate>
                        <asp:Label ID="lblQuantityUnitID" runat="server" Text='<%#Eval("QuantityUnitName") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Prch Qty">
                    <ItemTemplate>
                        <asp:Label ID="lblPurchasedQuantity" runat="server" Text='<%#Eval("PurchasedQuantity") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Prch Adj">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraFieldQuantity2" runat="server" Text='<%#Eval("ExtraFieldQuantity2") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Prch Rtn">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraFieldQuantity3" runat="server" Text='<%#Eval("ExtraFieldQuantity3") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Issued Qty">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraFieldQuantity5" runat="server" Text='<%#Eval("ExtraFieldQuantity5") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Issue Rtn">
                    <ItemTemplate>
                        <asp:Label ID="lblIssueReturedQuantity" runat="server" Text='<%#Eval("IssueReturedQuantity") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Wasted Qty">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraFieldQuantity4" runat="server" Text='<%#Eval("ExtraFieldQuantity4") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Utilized Qty">
                    <ItemTemplate>
                        <asp:Label ID="lblUtilizedQuantity" runat="server" Text='<%#Eval("UtilizedQuantity") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Stock">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraFieldQuantity1" runat="server" Text='<%#Eval("ExtraFieldQuantity1") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price PerUnit">
                    <ItemTemplate>
                        <asp:Label ID="lblPricePerUnit" runat="server" Text='<%#Eval("PricePerUnit") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="center">
                     <asp:GridView ID="gvInv_ItemTransaction" runat="server" AutoGenerateColumns="false"
            CssClass="gridCss">
            <Columns>
                <asp:TemplateField HeaderText="Issue">
                    <ItemTemplate>
                        <asp:Label ID="lblItemTrasactionTypeID" runat="server" Text='<%#Eval("ExtraField5") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label ID="lblAddedDate" runat="server" Text='<%#Eval("AddedDate","{0:dd MMM yyyy hh:mm tt}") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="WorkStation">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraField4" runat="server" Text='<%#Eval("ExtraField4") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity","{0:0,0.00}") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Reference ID">
                    <ItemTemplate>
                        <asp:Label ID="lblReferenceID" runat="server" Text='<%#Eval("ReferenceID","{0:0,0}") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>
                </td>
            </tr>
            
        </table>
        <br />
        <br />
        <br />
        <br />
        <table width="800px">
            <tr>
                <td align="left" style="padding:0 50px;border-top:1px dashed black;width:80px;">Received By</td>
                <td></td>
                <td align="center" style="padding:0 50px;border-top:1px dashed black;width:110px;">Store in Charge</td>
                <td></td>
                <td align="right" style="padding:0 50px;border-top:1px dashed black;width:140px;">Authorised Signature</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
