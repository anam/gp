<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TopSaleReportPrint.aspx.cs"
    Inherits="POS_TopSaleReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        td
        {
            padding: 5px;
        }
        th
        {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
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
                   <span style='font-size:14px;font-weight:bold;text-decoration:unerline;'>
                        <asp:Label ID="lblVoucherType" runat="server" Text=""></asp:Label>
                     Report</span>
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
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="BarCode">
                    <ItemTemplate>
                        <%#Eval("BarCode")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Product Name">
                    <ItemTemplate>
                        <%#Eval("ProductName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount">
                    <ItemTemplate>
                        <%#Eval("netSale","{0:0,0.00}")%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:GridView ID="GridView2" runat="server"  AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="ACC_ChartOfAccountLabel4ID" Visible="false">
                    <ItemTemplate>
                        <%#Eval("ACC_ChartOfAccountLabel4ID")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Product Name">
                    <ItemTemplate>
                        <%#Eval("ChartOfAccountLabel4Text")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount">
                    <ItemTemplate>
                        <%#Eval("netSale", "{0:0,0.00}")%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
