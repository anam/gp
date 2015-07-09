<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BarCodeGeneratePrint.aspx.cs" Inherits="Inventory_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
    body{padding:0;font-familly:Arial;
         margin:0;}
         .withOutDiscount
         {
             padding:8px 6px 7px;font-size:11px;
             }
             .withDiscount
             {
                 padding:8px 6px 7px;font-size:11px;
                 }
                 
                 .withOutDiscount1stRow
         {
             padding:7px 6px;font-size:11px;
             padding-top:0px;
             }
             .withDiscount1stRow
             {
                 padding:7px 6px;font-size:11px;
             padding-top:0px;
                 
                 }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblbarcode" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
