<%@ Page Title="" Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true" CodeFile="DayendSummarySalesEntry.aspx.cs" Inherits="Accounting_DayendSummaryEntry" %>

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

<h1>Day End Sales Entry</h1>
Date: <asp:TextBox ID="txtJournalDate" runat="server" Width="87px"></asp:TextBox>
<ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender1" runat="server" TargetControlID="txtJournalDate">
</ajaxToolkit:CalendarExtender> 
 <asp:GridView ID="gvACC_ChartOfAccountLabel4" runat="server" AutoGenerateColumns="false" CssClass="gridCss">
            <Columns>
                
                <asp:TemplateField HeaderText="Head">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfChartOfAccountLabel4ID" Value='<%#Eval("ACC_ChartOfAccountLabel4ID") %>' runat="server" />
                        <asp:Label ID="lblChartOfAccountLabel4Text" runat="server" Text='<%#Eval("ChartOfAccountLabel4Text") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Card Sale<br/>(DBBL)">
                    <ItemTemplate>
                        <asp:TextBox ID="txtCardSaleDBBL" runat="server" Width="75px" Text="0" style="text-align:right;"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Card Sale<br/>(CITY)">
                    <ItemTemplate>
                        <asp:TextBox ID="txtCardSaleCITY" runat="server" Width="75px" Text="0" style="text-align:right;"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="bKash">
                    <ItemTemplate>
                        <asp:TextBox ID="txtbKash" runat="server" Width="75px" Text="0" style="text-align:right;"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cash Sales">
                    <ItemTemplate>
                        <asp:TextBox ID="txtCashSales" runat="server" Width="75px" Text="0" style="text-align:right;"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Deposited">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlBanks" runat="server" Width="325px">
                        </asp:DropDownList>
                 </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Discount">
                    <ItemTemplate>
                       <asp:TextBox ID="txtDiscountAmount" runat="server" Width="75px" Text="0" style="text-align:right;"></asp:TextBox>
                       <asp:TextBox ID="txtBankDepostiedAmount" Visible="false" runat="server" Width="75px" Text="0" style="text-align:right;"></asp:TextBox>
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

