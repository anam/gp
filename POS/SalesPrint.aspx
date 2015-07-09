<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesPrint.aspx.cs" Inherits="Inventory_PurchasePrint" %>

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
    <script type="text/javascript">
        var win = null;
        function printIt(printThis) {
            win = window.open();
            self.focus();
            win.document.open();
            win.document.write('<' + 'html' + '><' + 'head' + '><' + 'style' + '>');
            win.document.write('body{padding: 0;margin: 0;font-size:12px;}');
            win.document.write('.bordered td{border:1px solid black;padding:2px;}');
            win.document.write('#dataTable td{text-align:right;}');
            win.document.write('.PurchaseHeaderCss{font-weight: bold;}');
            win.document.write('<' + '/' + 'style' + '><' + '/' + 'head' + '><' + 'body' + '>');
            win.document.write(printThis);
            win.document.write('<' + '/' + 'body' + '><' + '/' + 'html' + '>');
            win.document.close();
            win.print();
            win.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:LinkButton ID="lnkPrint" Visible="false"  Text="Print" runat="server" OnClientClick="javascript:printIt(document.getElementById('printSalesVoucher').innerHTML);"></asp:LinkButton>
    <div id='printSalesVoucher' align="left" style="font-family: Verdana; font-size: 9px;">
        <table width="230px">
            <tr>
                <td align="center" style="border-bottom: 1px solid Black;">
                    <b style='font-size: 17px;Color:red;'>GENTLE PARK</b>
                    <br />
                    <asp:Label ID="lblBranchLocation" runat="server" Text=""></asp:Label>
                    <%--334, Ferdousi Plaza, (4th Floor).<br />
                    Elephant Road, Dhaka 1205,<br /> Tel: 02-9612452--%>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <span style='font-size: 14px;color:Red; font-weight: bold; text-decoration: unerline;'>Sales 
                        <asp:Label ID="lblSalesReturn" runat="server" Text=""></asp:Label> Memo
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
