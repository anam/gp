<%@ Page Title="" Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true" CodeFile="JournalModification.aspx.cs" Inherits="Accounting_Voucher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
    .tdUnderline{border-bottom:1px solid black;}
    .tdBorder,#tblJournalDetails td{border:1px solid black;}
    .signature{border-top:1px dotted black;text-align:center;width:175px;}
    td table tr td{padding:5px;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
Journal ID:<asp:TextBox ID="txtJournalID" runat="server"></asp:TextBox>
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
        onclick="btnSubmit_Click" />
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
                <asp:Label ID="lblVoucherName" runat="server" Text="" ></asp:Label> VOUCHER
                </span></p>
            </td>
        </tr>
        <tr>
            <td>
                <table style="font-weight:bold;margin:10px 0;">
                    <tr>
                        <td>NAME OF OFFICE:</td>
                        <td width="200px" class="tdUnderline">
                            <asp:Label ID="lblOfficeName" runat="server" Text=""></asp:Label>
                        </td>
                        <td><asp:Label ID="lblVoucherType" runat="server" Text=""></asp:Label>. No.:</td>
                        <td width="200px" class="tdUnderline">
                            <asp:Label ID="lblJournalMasterID" runat="server" Text=""></asp:Label>
                        </td>
                        <td>DATE:</td>
                        <td class="tdUnderline"  width="150px"><asp:Label ID="lblDate" runat="server" Text=""></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr ID="trAddress" runat="server">
            <td>
                <table style="border:1px solid Black; width:100%;font-weight:bold;margin:10px 0;">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblReceivedfromOrPayto" Font-Bold="true" runat="server" Text=""></asp:Label>:</td>
                        <td class="tdUnderline" width="683px">
                            <asp:Label ID="lblCustomerName" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Address:</td>
                        <td class="tdUnderline" >
                            <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblJournalDetials" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvJournal" runat="server" AutoGenerateColumns="false">
                    <Columns>
                    <asp:TemplateField HeaderText="WorkStation">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                                <%--<asp:HiddenField ID="hfWorkStationID" runat="server" value='<%#Eval("WorkStation") %>'/>--%>
                                <asp:HiddenField ID="hfJournalDetailsID" runat="server" value='<%#Eval("ACC_JournalDetailID") %>'/>
                                <%--<asp:DropDownList ID="ddlWorkStation" runat="server" Width="100px">
                                </asp:DropDownList>--%>
                               <%#Eval("WorkStationName")%>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="L3">
                            <ItemTemplate>
                               <%#Eval("ACC_ChartOfAccountLabel3Text")%>
                                <%--<asp:DropDownList ID="ddlL3" runat="server" Width="200px">
                                </asp:DropDownList>--%>
                                <%--<asp:HiddenField ID="hfACC_ChartOfAccountLabel3ID" runat="server" value='<%#Eval("ACC_ChartOfAccountLabel3ID") %>'/>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="L4">
                            <ItemTemplate>
                                <%--<asp:DropDownList ID="ddlL4" runat="server" Width="200px">
                                </asp:DropDownList>--%>
                                <%#Eval("ACC_ChartOfAccountLabel4Text")%>
                                <%--<asp:HiddenField ID="hfACC_ChartOfAccountLabel4ID" runat="server" value='<%#Eval("ACC_ChartOfAccountLabel4ID") %>'/>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Debit">
                            <ItemTemplate>
                                <%#Eval("Debit") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Credit">
                            <ItemTemplate>
                                <%#Eval("Credit") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <hr />
                <table>
                    <tr>
                        <td>Work Station</td>
                        <td>
                            <asp:DropDownList ID="ddlWorkStation" runat="server" Width="100px">
                                </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>L3</td>
                        <td>
                        <asp:DropDownList ID="ddlL3" runat="server" Width="200px">
                                </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>L4</td>
                        <td>
                        <asp:DropDownList ID="ddlL4" runat="server" Width="200px">
                        </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><asp:Button ID="btnSave" runat="server" Text="Save Details" onclick="btnSave_Click" /></td>
                    </tr>
                    <tr>
                        <td>Journal Type</td>
                        <td>
                            <asp:DropDownList ID="ddlJournalType" runat="server">
                                <asp:ListItem Value="1">RV</asp:ListItem>
                                <asp:ListItem Value="2">PV</asp:ListItem>
                                <asp:ListItem Value="3">JV</asp:ListItem>
                                <asp:ListItem Value="4">CV</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><asp:Button ID="btnJournalMasterSave" runat="server" Text="Save Master" 
                                onclick="btnJournalMasterSave_Click"  /></td>
                    </tr>
                </table>
                
            </td>
        </tr>
        <tr  style="padding-top:50px;">
            <td>
                <table style="border:1px solid Black; width:100%;font-weight:bold;">
                    <tr>
                        <td>
                        Explanation:
                        <br />
                            <asp:Label ID="lblExplanation" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr ID="trCheck" runat="server">
            <td>
                <table width="100%" cellpadding="0" cellspacing="0" style="padding-top:10px;">
                    <tr>
                        <td colspan="2" class="tdBorder">Received in Cash/ by Cheque:</td>
                    </tr>
                    <tr>
                        <td class="tdBorder">Cheque No.:
                            <asp:Label ID="lblChequeNo" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="tdBorder">Date:
                            <asp:Label ID="lblChequeDate" runat="server" Text=""></asp:Label>                            
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBorder">Bank:
                            <asp:Label ID="lblChequeBank" runat="server" Text=""></asp:Label>                            
                        </td>
                        <td class="tdBorder">Branch:
                            <asp:Label ID="lblChequeBranch" runat="server" Text=""></asp:Label>                            
                        </td>
                    </tr>
                </table>
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

