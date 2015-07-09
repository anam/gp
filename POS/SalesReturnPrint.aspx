<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesReturnPrint.aspx.cs" Inherits="Inventory_PurchasePrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            padding: 0;
            margin: 0;
        }
        .PurchaseHeaderCss
        {
            font-weight: bold;
        }
        #dataTable td{text-align:right;}
        .bordered td{border:1px solid black;padding:2px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="left" style="font-family: Verdana; font-size: 9px;">
        <table width="230px">
            <tr>
                <td align="center" style="border-bottom: 1px solid Black;">
                    <b style='font-size: 17px;Color:red;'>GENTLE PARK (Pvt.) Ltd</b>
                    <br />
                    334, Ferdousi Plaza, (4th Floor).<br />
                    Elephant Road, Dhaka 1205,<br /> Tel: 02-9612452
                </td>
            </tr>
            <tr>
                <td align="center">
                    <span style='font-size: 14px;color:Red; font-weight: bold; text-decoration: unerline;'>Sales Return Meno
                    </span>
                </td>
            </tr>
            <tr>
                 <td align="left" style="border-bottom: 1px solid Black;">
                    <asp:Label ID="lblMaster" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                   <asp:Label ID="lblDetails" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
