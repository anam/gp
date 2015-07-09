<%@ Page Title="" Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true" CodeFile="DayendSummaryBankEntry.aspx.cs" Inherits="Accounting_DayendSummaryEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
<asp:HiddenField ID="hfLoginID" runat="server" Value=""/>
<asp:DropDownList ID="ddlAllACC_ChartOfAccountLabel3" Visible="false" runat="server">
</asp:DropDownList>
<asp:DropDownList ID="ddlAllACC_ChartOfAccountLabel4" Visible="false" runat="server">
</asp:DropDownList>

<h1>Bank Book</h1>
Date: <asp:TextBox ID="txtJournalDate" runat="server" Width="87px"></asp:TextBox>
<ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender1" runat="server" TargetControlID="txtJournalDate">
</ajaxToolkit:CalendarExtender> 
 <asp:GridView ID="gvACC_ChartOfAccountLabel4" runat="server" AutoGenerateColumns="false" CssClass="gridCss">
            <Columns>
                
                <asp:TemplateField HeaderText="Head">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfChartOfAccountLabel4ID" Value='<%#Eval("ACC_ChartOfAccountLabel4ID") %>' runat="server" />
                        <asp:Label ID="lblChartOfAccountLabel4Text" Font-Bold="true" Font-Size="15px" runat="server" Text='<%#Eval("ChartOfAccountLabel4Text") %>'>
                        </asp:Label>
                        <br />
                        Balance:<asp:TextBox ID="txtOpiningBalance" runat="server" Width="150px" Enabled="false" Text="0" style="text-align:right;"></asp:TextBox>
                        Withdrawal:<asp:TextBox ID="txtCashWithdraw" runat="server" Width="75px" Text="0" style="text-align:right;"></asp:TextBox>
                    
                    </ItemTemplate>
                </asp:TemplateField>
                
               <asp:TemplateField HeaderText="Supplyer Payment">
                    <ItemTemplate>
                    Amount:<asp:TextBox ID="txtSypplyerPayment" runat="server" Width="75px" Text="0" style="text-align:right;"></asp:TextBox>
                    Discount:<asp:TextBox ID="txtDiscountIncome" runat="server" Width="75px" Text="0" style="text-align:right;"></asp:TextBox>
                    Check #:<asp:TextBox ID="txtCheckNo" runat="server" Width="75px" Text="0" style="text-align:right;"></asp:TextBox>
                    <br />Check Date:<asp:TextBox ID="txtCheckDate" runat="server" Width="75px" Text="0" style="text-align:right;"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender2" runat="server" TargetControlID="txtCheckDate">
                    </ajaxToolkit:CalendarExtender>
                        <br />
                        <asp:DropDownList ID="ddlSupplier" runat="server" Width="260px">
                        <asp:ListItem Value="0" Text="Select Supplier"></asp:ListItem>
                        </asp:DropDownList>
                 </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fund Transfer">
                    <ItemTemplate>
                    Amount:<asp:TextBox ID="txtFundTransfer" runat="server" Width="75px" Text="0" style="text-align:right;"></asp:TextBox>
                        <asp:DropDownList ID="ddlBanks" runat="server" Width="200px">
                        </asp:DropDownList>
                 </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>

         <asp:Button ID="btnJournalSubmit" runat="server" Text="Submit" 
        onclick="btnJournalSubmit_Click" />
     <asp:HyperLink ID="hlnkPrintVoucher" CssClass="buttonCss" Visible="false" runat="server" Target="_blank">Print Voucher</asp:HyperLink>
                
 </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

