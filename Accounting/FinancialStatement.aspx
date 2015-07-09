<%@ Page Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true" 
CodeFile="FinancialStatement.aspx.cs" Inherits="AdminACC_JournalDetailInsertUpdate" Title="ACC_JournalDetail Insert/Update By Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .tableCss
        {
        	text-align: left;
        }
        .gridCss
        {width:100%}
        #ctl00_ContentPlaceHolder1_rbtnRootAccounts td span
        {
            background: none;
            width:auto;
            }
            
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="tableCss">
    <h1>Transaction Summary</h1>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>
        <table>
            
           <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Date:"></asp:Label>
                </td>
                <td>
                    
                            <asp:TextBox ID="txtFromDateTransactionSummary" runat="server" Text=""></asp:TextBox>
                            <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender3" runat="server" TargetControlID="txtFromDateTransactionSummary">
                                    </ajaxToolkit:CalendarExtender>
                                    &nbsp;To&nbsp;
                        <asp:TextBox ID="txtToDateTransactionSummary" runat="server" Text=""></asp:TextBox>
                            <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender4" runat="server" TargetControlID="txtToDateTransactionSummary">
                                    </ajaxToolkit:CalendarExtender>
                                    
                </td>
            </tr>
            
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnTransactionSummary" runat="server" Text="Generate Link For Transaction Summary" OnClick="btnTransactionSummary_Click" />
                    <br />
                    <asp:HyperLink ID="hlnkTransactionSummary" CssClass="buttonCss" Visible="false" runat="server" Target="_blank">Print Transaction Summary</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnDateVoucherListByPostedDate" runat="server" Text="Generate Link For Voucher List by Posted Date" OnClick="btnDateVoucherListByPostedDate_Click" />
                    <br />
                    <asp:HyperLink ID="hlnkDateVoucherListByPostedDate" CssClass="buttonCss" Visible="false" runat="server" Target="_blank">Print Voucher List by Posted Date</asp:HyperLink>
                </td>
            </tr>
            
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnDateVoucherListByVoucherDate" runat="server" Text="Generate Link For Voucher List by voucher Date" OnClick="btnDateVoucherListByVoucherDate_Click" />
                    <br />
                    <asp:HyperLink ID="hlnkDateVoucherListByVoucherDate" CssClass="buttonCss" Visible="false" runat="server" Target="_blank">Print Voucher List by voucher Date</asp:HyperLink>
                </td>
            </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    
    <div class="tableCss">
    <h1>Transaction Summary By Lables</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table>
            
            <tr runat="server" id="trRootAccounts">
                <td><asp:Label ID="Label4" runat="server" Text="Lable-0: ">
                    </asp:Label></td>
                <td>
                <asp:RadioButtonList ID="rbtnRootAccounts" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="rbtnRootAccounts_SelectedIndexChanged" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Value="0">All</asp:ListItem>
                        <asp:ListItem Value="Asset">Asset</asp:ListItem>
                        <asp:ListItem Value="Expense">Expense</asp:ListItem>
                        <asp:ListItem Value="Income">Income</asp:ListItem>
                        <asp:ListItem Value="Liability & Equity">Liability & Equity</asp:ListItem>
                    </asp:RadioButtonList>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Lable-1: ">
                    </asp:Label>
                </td>
                <td>
                    
                    <asp:DropDownList ID="ddlACC_ChartOfAccountLabel1" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="ddlACC_ChartOfAccountLabel1_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlAllACC_ChartOfAccountLabel1" Visible="false" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Lable-2: ">
                    </asp:Label>
                </td>
                <td>
                    
                    <asp:DropDownList ID="ddlACC_ChartOfAccountLabel2" runat="server" AutoPostBack="true"
                        onselectedindexchanged="ddlACC_ChartOfAccountLabel2_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlAllACC_ChartOfAccountLabel2" Visible="false" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblACC_ChartOfAccountLabel4ID" runat="server" Text="Label-3: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlACC_ChartOfAccountLabel3" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlAllACC_ChartOfAccountLabel3" Visible="false" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Type: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rbtnHeadType" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="rbtnHeadType_SelectedIndexChanged" RepeatColumns="7"
                        RepeatDirection="Horizontal">
                    </asp:RadioButtonList>
                    
                    
                </td>
            </tr>
            <tr >
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Head: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlACC_ChartOfAccountLabel4" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlAllACC_ChartOfAccountLabel4" Visible="false" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
             <tr id="trWorkStation" runat="server" >
                <td>
                    <asp:Label ID="Label5" runat="server" Text="For: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlWorkSatation" runat="server">
                    </asp:DropDownList>
                   
                </td>
            </tr>
           <tr>
                <td>
                    <asp:Label ID="Label14" runat="server" Text="Date:"></asp:Label>
                </td>
                <td>
                    
                            <asp:TextBox ID="txtFromDate" runat="server" Text=""></asp:TextBox>
                            <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="ajCal" runat="server" TargetControlID="txtFromDate">
                                    </ajaxToolkit:CalendarExtender>
                                    &nbsp;To &nbsp;
                                    <asp:TextBox ID="txtToDate" runat="server" Text=""></asp:TextBox>
                            <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender1" runat="server" TargetControlID="txtToDate">
                                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Journal Type: ">
                    </asp:Label>
                </td>
                <td>
                    
                        <asp:CheckBox ID="chkJournalTypeRV" runat="server"  Text="RV"/>
                        <asp:CheckBox ID="chkJournalTypePV" runat="server"  Text="PV"/>
                        <asp:CheckBox ID="chkJournalTypeJV" runat="server"  Text="JV"/>
                        <asp:CheckBox ID="chkJournalTypeCV" runat="server"  Text="CV"/>
                                        
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnShowLedger" runat="server" Text="Generate Link For Transaction Summary" OnClick="btnShowLedger_Click" />
                            <br />
                            <asp:HyperLink ID="hlnkPrintGeneralLedger" CssClass="buttonCss" Visible="false" runat="server" Target="_blank">Print Transaction Summary</asp:HyperLink>    
                        </td>
                        <td>
                            <asp:Button ID="btnSummary" runat="server" Text="Generate Link For Summary" OnClick="btnSummary_Click" />
                            <br />
                            <asp:HyperLink ID="hlnkSummary" CssClass="buttonCss" Visible="false" runat="server" Target="_blank">Print Summary</asp:HyperLink>    
                        </td>
                    </tr>
                    </table>
                    
                </td>
            </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    
     <div class="tableCss">
    <h1>Standard Statements</h1>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="Date:"></asp:Label>
                </td>
                <td>
                    
                            <asp:TextBox ID="txtTrialBanaceFromDate" runat="server" Text=""></asp:TextBox>
                            <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender7" runat="server" TargetControlID="txtTrialBanaceFromDate">
                                    </ajaxToolkit:CalendarExtender>
                                    &nbsp;To &nbsp;
                                   <asp:TextBox ID="txtTrialBanaceDate" runat="server" Text=""></asp:TextBox>
                            <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender2" runat="server" TargetControlID="txtTrialBanaceDate">
                                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnTrialBanalce" runat="server" Text="Generate Link For Trial Balance" OnClick="btnTrialBanalce_Click" />
                    <br />
                    <asp:HyperLink ID="hlnkTrialBanalce" CssClass="buttonCss" Visible="false" runat="server" Target="_blank">Print Trial Balance</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnBalanceSheet" runat="server" Text="Generate Link For Balance Sheet" OnClick="btnBalanceSheet_Click" />
                    <br />
                    <asp:HyperLink ID="hlnkBalanceSheet" CssClass="buttonCss" Visible="false" runat="server" Target="_blank">Print Balance Sheet</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnIncomeStatement" runat="server" Text="Generate Link For Income Statement" OnClick="btnIncomeStatement_Click" />
                    <br />
                    <asp:HyperLink ID="hlnkIncomeStatement" CssClass="buttonCss" Visible="false" runat="server" Target="_blank">Print Income Statement</asp:HyperLink>
                </td>
            </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    
   
     <div class="tableCss">
    <h1>ShowRoom Summary</h1>
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
    <ContentTemplate>
        <table>
            <tr >
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Show Room: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlShowRoom" runat="server">
                    </asp:DropDownList>
                      
                </td>
            </tr>
           <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Date:"></asp:Label>
                </td>
                <td>
                    
                            <asp:TextBox ID="txtShowroomSummaryFromDate" runat="server" Text=""></asp:TextBox>
                            <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender5" runat="server" TargetControlID="txtShowroomSummaryFromDate">
                                    </ajaxToolkit:CalendarExtender>
                                    &nbsp;To&nbsp;
                        <asp:TextBox ID="txtShowroomSummaryToDate" runat="server" Text=""></asp:TextBox>
                            <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender6" runat="server" TargetControlID="txtShowroomSummaryToDate">
                                    </ajaxToolkit:CalendarExtender>
                                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="Journal Type: ">
                    </asp:Label>
                </td>
                <td>
                    
                        <asp:CheckBox ID="chkShowroomRV" runat="server"  Text="RV"/>
                        <asp:CheckBox ID="chkShowroomPV" runat="server"  Text="PV"/>
                        <asp:CheckBox ID="chkShowroomJV" runat="server"  Text="JV"/>
                        <asp:CheckBox ID="chkShowroomCV" runat="server"  Text="CV"/>
                                        
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnShowroomSummary" runat="server" Text="Generate Link For Showroom Summary" OnClick="btnShowroomSummary_Click" />
                    <br />
                    <asp:HyperLink ID="hlnkShowroomSummary" CssClass="buttonCss" Visible="false" runat="server" Target="_blank">Print Showroom Summary</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                 <asp:Button ID="btnBackup" runat="server" Text="Download Backkup" 
                                onclick="btnBackup_Click" />
                </td>
            </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
</asp:Content>
