<%@ Page Title="" Language="C#" MasterPageFile="~/WithoutLeftMenuPrint.master" AutoEventWireup="true" CodeFile="DateRangeVouchersPrint.aspx.cs" Inherits="Accounting_Voucher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
    .tdUnderline{border-bottom:1px solid black;}
    .tdBorder,#tblJournalDetails td{border:1px solid black;}
    .signature{border-top:1px dotted black;text-align:center;width:175px;}
    td table tr td{padding:5px;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lblDateRangeVoucherPrint" runat="server" Text=""></asp:Label>
</asp:Content>

