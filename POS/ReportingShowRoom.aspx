<%@ Page Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true"
    CodeFile="ReportingShowRoom.aspx.cs" Inherits="AdminACC_JournalDetailInsertUpdate" Title="ACC_JournalDetail Insert/Update By Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .tableCss
        {
            text-align: left;
        }
        .gridCss
        {
            width: 100%;
        }
        #ctl00_ContentPlaceHolder1_rbtnRootAccounts td span
        {
            background: none;
            width: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="tableCss">
                <h1>
                    Report For</h1>
                <table>
                    <tr>
                        <td valign="top" colspan="2">
                            <asp:RadioButtonList ID="rbtnTransactionType" style="font-size:18px;" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="View Style:"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rbtnlViewStyle" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" Selected="True">Item Group wise</asp:ListItem>
                                <asp:ListItem Value="1">Item wise</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Date:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFromDate" runat="server" Text=""></asp:TextBox>
                            <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender3" runat="server"
                                TargetControlID="txtFromDate">
                            </ajaxToolkit:CalendarExtender>
                            &nbsp;To&nbsp;
                            <asp:TextBox ID="txtToDate" runat="server" Text=""></asp:TextBox>
                            <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender4" runat="server"
                                TargetControlID="txtToDate">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr style="display:none;">
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Supplier:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlACC_ChartOfAccountLabel4" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="display:none;">
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="WorkStation:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlWorkStationForTransactionReport" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="132px">
                            <asp:Label ID="Label9" runat="server" Text="ID:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtID" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="132px">
                        <asp:Label ID="Label4" runat="server" Text="Voucher Print:"></asp:Label>
                        </td>
                        <td>
                           <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnGenerateLinkForTransacationByID" runat="server" OnClick="btnGenerateLinkForTransacationByID_Click"
                                            Text="Link ForTransaction Print" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnDelivaryChalan" runat="server" OnClick="btnDelivaryChalan_Click"
                                            Text="Link For Delivary Chalan Print" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnDeleteTransaction" runat="server" OnClick="btnDeleteTransaction_Click"
                                            Text="Link For Delete Transaction" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="height: 5px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:HyperLink ID="hlnkLinkForTransacationByID" CssClass="buttonCss" Visible="false"
                                            runat="server" Target="_blank">Print Transaction</asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hlnkLinkForDelivaryChalan" CssClass="buttonCss" Visible="false"
                                            runat="server" Target="_blank">Print Delivary Chalan</asp:HyperLink>
                                    </td>                                    
                                    <td>
                                        <asp:HyperLink ID="hlnkLinkForDeleteTransaction" CssClass="buttonCss" Visible="false"
                                            runat="server" Target="_blank">Delete Link</asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Reports:"></asp:Label>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td style="display:none;">
                                        <asp:Button ID="btnCentralProductStockReport" runat="server" Text="Link For Product Stock Inventory Date To Date By Item Group"
                                            OnClick="btnCentralProductStockReport_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnWorkStationStockReport" runat="server" Text="Link For WorkStation Stock"
                                            OnClick="btnWorkStationStockReport_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnLedger" runat="server" Text="Link For Ledger" 
                                            onclick="btnLedger_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnLedgerSup" runat="server" Text="Link For Ledger(Sup)" Visible="false"
                                            onclick="btnLedgerSup_Click"/>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnLedgerBR" runat="server" Text="Link For Ledger(Br)"  Visible="false"
                                            onclick="btnLedgerBR_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="height: 5px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="display:none;">
                                        <asp:HyperLink ID="hlinkCentralProductStockReport" CssClass="buttonCss" Visible="false" runat="server"
                                            Target="_blank">Print Central Current Stock</asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hlnkWorkStationStockReport" CssClass="buttonCss" Visible="false"
                                            runat="server" Target="_blank">Print WorkStation  Stock</asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hlnkLedger" CssClass="buttonCss" Visible="false" runat="server"
                                            Target="_blank">Print Ledger</asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hlnkLedgerSup" CssClass="buttonCss" Visible="false"
                                            runat="server" Target="_blank">Print Ledger(Sup)</asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hlnkLedgerBr" CssClass="buttonCss" Visible="false" runat="server"
                                            Target="_blank">Print Ledger(Br)</asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="display:none;">
                                        <asp:Button ID="btnCentralStockReport" runat="server" Text="Link For Central Current Stock"
                                            OnClick="btnCentralStockReport_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnShowroomStockReport" runat="server" Text="Link For Showroom Current Stock"
                                            OnClick="btnShowroomStockReport_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnShowroomSalesReport" runat="server" Text="Link For Sales Report(Inv)"
                                            OnClick="btnShowroomSalesReport_Click" />
                                    </td>
                                    <td>
                                        
                                    </td>
                                    <td>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="height: 5px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="display:none;">
                                        <asp:HyperLink ID="hlnkCentralStockReport" CssClass="buttonCss" Visible="false" runat="server"
                                            Target="_blank">Print Central Stock</asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hlnkShowroomStockReport" CssClass="buttonCss" Visible="false"
                                            runat="server" Target="_blank">Print WorkStation Stock</asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hlnkShowroomSalesReport" CssClass="buttonCss" Visible="false" runat="server"
                                            Target="_blank">Print Sales Report(Inv)</asp:HyperLink>
                                    </td>
                                    <td>
                                       
                                    </td>
                                    <td>
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td style="display:none;">
                                        
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSalesPersonWiseSalesReport" runat="server" 
                                            Text="Link for Sales Ledger(S. P.)" onclick="btnSalesPersonWiseSalesReport_Click"
                                             />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnDateWiseSalesReport" runat="server" 
                                            Text="Link for Sales Ledger(Date)" onclick="btnDateWiseSalesReport_Click"
                                             />
                                    </td>
                                    <td>
                                        
                                    </td>
                                    <td>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="height: 5px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="display:none;">
                                       
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hlnkSalesPersonWiseSalesReport" CssClass="buttonCss" Visible="false"
                                            runat="server" Target="_blank">Print Sales Ledger(S. P.)</asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hlnkDateWiseSalesReport" CssClass="buttonCss" Visible="false" runat="server"
                                            Target="_blank">Print Sales Ledger(Date)</asp:HyperLink>
                                    </td>
                                    <td>
                                       
                                    </td>
                                    <td>
                                       
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
