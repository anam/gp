<%@ Page Title="" Language="C#" MasterPageFile="~/WithoutLeftMenuPrint.master" AutoEventWireup="true" CodeFile="TrialBalancePrint.aspx.cs" Inherits="Accounting_Voucher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
    .tdUnderline{border-bottom:1px solid black;}
    .tdBorder,#tblJournalDetails td,#tableAccountTitle td,#tableAccountTitle{border:1px solid black;}
    .signature{border-top:1px dotted black;text-align:center;width:175px;}
    td table tr td{padding:5px;}
    
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div align="center" style="width:900px;font-weight:bold;">
    <table>
        <tr>
            <td align="center"  style="border-bottom:3px solid black;padding-bottom:20px;">
                <img src="../images/voucherBanner.png" width="400px" align="middle"/>        
            </td>
        </tr>
        <tr>
            <td align="center" style="margin:5px;">
                <p style="padding:10px;font-weight:bold;"><span style="border:1px double black;padding:5px;">
                    <asp:Label ID="lblStatement" runat="server" Text="TRIAL BALANCE"></asp:Label>
                </span></p>
                <br />
                <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblJournalDetials" runat="server" Text=""></asp:Label>
            </td>
        </tr>
       
        <tr>
            <td>
                <table style="padding-top:80px;">
                    <tr>
                        <td class="signature">Prepared By</td>
                        <td width="100px;"></td>
                        <td class="signature">Checked By</td>
                        <td width="100px;"></td>
                        <td class="signature">Approved By</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    
</div>
</asp:Content>

