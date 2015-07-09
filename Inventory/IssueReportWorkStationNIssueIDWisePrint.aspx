<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IssueReportWorkStationNIssueIDWisePrint.aspx.cs" Inherits="Inventory_PurchasePrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .PurchaseHeaderCss{font-weight:bold;}
        #tableHeader td{border-left:1px solid black;font-weight:bold;text-align:center;border-bottom:1px solid black;border-top:1px solid black;}
        .itemCss td{border-bottom:1px solid black;border-left:1px solid black;text-align:center;}
        #itemList_tbl td{padding:2px 10px;}
        #lastRow td{border-top:1px solid black;border-bottom:1px solid black;font-weight:bold;text-align:right;}
        .subtotalRow{font-weight:bold;text-align:right;border-top:1px solid black;border-bottom:1px solid black;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
    <asp:HiddenField ID="hfLoginID" runat="server" />
    <div align="center" style="font-family:Verdana;font-size:12px;">
        <table width="800px">
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
                    <span style='font-size:14px;font-weight:bold;text-decoration:unerline;'>Issue Details</span>
                    <br />
                   <b >Date: </b> 
                    <asp:Label ID="lblStockDate" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            
            <tr>
                <td align="right" >
                    <b >Print Date: </b> 
                    <asp:Label ID="lblPrintDate" runat="server" Text=""></asp:Label>

                </td>
            </tr>
            </table>

            <table width="800px">
            <tr>
                <td>
                    <asp:Label ID="lblItemList" runat="server" Text=""></asp:Label>
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
                <td align="center" style="padding:0 50px;border-top:1px dashed black;width:110px;">Issued By</td>
                <td></td>
                <td align="center" style="padding:0 50px;border-top:1px dashed black;width:110px;">Checked By</td>
                <td></td>
                <td align="right" style="padding:0 50px;border-top:1px dashed black;width:140px;">Authorised By</td>
            </tr>
        </table>
    </div>
   
    </form>
</body>
</html>
