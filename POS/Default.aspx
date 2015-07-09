<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="POS_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        td,th{padding:5px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <asp:RadioButtonList ID="rbtnReport" runat="server" AutoPostBack="True" RepeatColumns="1" RepeatDirection="Horizontal"
            onselectedindexchanged="rbtnReport_SelectedIndexChanged">
            <asp:ListItem Value="1" Selected="True">Branch Closing Stock By Item Group</asp:ListItem>
            <asp:ListItem Value="2">Branch Closing Stock Report</asp:ListItem>
            <asp:ListItem Value="3">Current Product in (Central)Stock(Item Group)</asp:ListItem>
            <asp:ListItem Value="4">Current Product in (Central)Stock(Item Wise)</asp:ListItem>
            
        </asp:RadioButtonList>
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
    </div>
    </form>
</body>
</html>
