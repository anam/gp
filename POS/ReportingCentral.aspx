<%@ Page Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true"
    CodeFile="ReportingCentral.aspx.cs" Inherits="AdminACC_JournalDetailInsertUpdate" Title="ACC_JournalDetail Insert/Update By Admin" %>

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
        <h1>
                    Report</h1>
            <div class="tableCss">
                
                <table>
                    <tr>
                        <td valign="top" colspan="2">
                            <asp:RadioButtonList ID="rbtnTransactionType" style="font-size:18px;" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    
                    <tr id="trViewStyle" runat="server">
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="View Style:"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rbtnlViewStyle" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" Selected="True">Item Group wise</asp:ListItem>
                                <asp:ListItem Value="1">Item wise</asp:ListItem>
                                <asp:ListItem Value="2">10 Digit</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr id="tr4" runat="server" >
                        <td>
                            <asp:Label ID="Label17" runat="server" Text="VAT Status:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlVATStatus" runat="server">
                            <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                            <asp:ListItem Value="1">Inclusive</asp:ListItem>
                            <asp:ListItem Value="2">Exclusive</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trDate" runat="server">
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
                    <tr id="trSupplier" runat="server" >
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Supplier:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlACC_ChartOfAccountLabel4" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trSalesMan" runat="server" >
                        <td>
                            <asp:Label ID="Label13" runat="server" Text="Sales man:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSalesMan" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trWorkStation" runat="server">
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="WorkStation:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlWorkStationForTransactionReport" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trID" runat="server">
                        <td width="132px">
                            <asp:Label ID="Label9" runat="server" Text="ID:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtID" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr  id="trPrintVoucher" runat="server">
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
                    <tr id="tr1" runat="server">
                        <td width="132px">
                            <asp:Label ID="Label12" runat="server" Text="Card No:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCardNo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr  id="trReport" runat="server">
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Reports:"></asp:Label>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnCentralProductStockReport" runat="server" Text="Stock Inventory Date-2-Date"
                                            OnClick="btnCentralProductStockReport_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnWorkStationStockReport" runat="server" Text="Showroom Stock Date-2-Date"
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
                                    
                                </tr>
                                <tr>
                                    <td colspan="4" style="height: 5px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:HyperLink ID="hlinkCentralProductStockReport" CssClass="buttonCss" Visible="false" runat="server"
                                            Target="_blank">Print Central Stock</asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hlnkWorkStationStockReport" CssClass="buttonCss" Visible="false"
                                            runat="server" Target="_blank">Print WorkStation Stock</asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hlnkLedger" CssClass="buttonCss" Visible="false" runat="server"
                                            Target="_blank">Print Ledger</asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hlnkLedgerSup" CssClass="buttonCss" Visible="false"
                                            runat="server" Target="_blank">Print Ledger(Sup)</asp:HyperLink>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td>
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
                                        <asp:Button ID="btnLedgerBR" runat="server" Text="Link For Ledger(Br)"  Visible="false"
                                            onclick="btnLedgerBR_Click" />
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td colspan="4" style="height: 5px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:HyperLink ID="hlnkCentralStockReport" CssClass="buttonCss" Visible="false" runat="server"
                                            Target="_blank">Print Central Stock</asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hlnkShowroomStockReport" CssClass="buttonCss" Visible="false"
                                            runat="server" Target="_blank">Print WorkStation Stock</asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hlnkShowroomSalesReport" CssClass="buttonCss" Visible="false" runat="server"
                                            Target="_blank">Print Sales Report(Invo)</asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hlnkLedgerBr" CssClass="buttonCss" Visible="false" runat="server"
                                            Target="_blank">Print Ledger(Br)</asp:HyperLink>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td >
                                        <asp:Button ID="btnDayWiseSalesSummary" runat="server" 
                                            Text="Link for Sales Summary(Day)" onclick="btnDayWiseSalesSummary_Click"
                                             />
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
                                   
                                </tr>

                                <tr>
                                    <td colspan="4" style="height: 5px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td >
                                       <asp:HyperLink ID="hlnkDayWiseSalesSummary" CssClass="buttonCss" Visible="false" runat="server"
                                            Target="_blank">Print Sales Summary(Day)</asp:HyperLink>
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
                                    
                                </tr>
                                 <tr>
                                    <td >
                                        <asp:Button ID="btnTransactionID" runat="server" 
                                            Text="Link for IDs By Date" onclick="btnTransactionID_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSalesReturnInvoice" runat="server" Text="Link For Sales Return Report(Inv)"
                                            OnClick="btnShowroomSalesReturnReport_Click" />
                                    </td>
                                    <td>
                                        
                                    </td>
                                    <td>
                                         
                                    </td>
                                   
                                </tr>

                                <tr>
                                    <td colspan="4" style="height: 5px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td >
                                      <asp:HyperLink ID="hlnkTransactionIDsByDate" CssClass="buttonCss" Visible="false" runat="server"
                                            Target="_blank">Print TransactionIDs(Date)</asp:HyperLink>
                                    </td>
                                    <td>
                                      <asp:HyperLink ID="hlnkTransactionSalesInvoice" CssClass="buttonCss" Visible="false" runat="server"
                                            Target="_blank">Print Sales Return(Inv)</asp:HyperLink>
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
            
            <h1>Product Analysis</h1>
            <div>
                <table>
                    <tr id="trTop" runat="server">
                        <td width="132px">
                            <asp:Label ID="Label7" runat="server" Text="Display Count:"></asp:Label>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                    <asp:RadioButtonList ID="rbtnHeighestOrLowest" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" Selected="True">Top Selling</asp:ListItem>
                                <asp:ListItem Value="1">Slow Moving</asp:ListItem>
                            </asp:RadioButtonList>
                                    </td>
                                    <td>
                            <asp:TextBox ID="txtDisplayCount" runat="server" Text="10"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                             
                        </td>
                    </tr>
                    <tr id="trPriceBasedTop" runat="server">
                        <td width="132px">
                            <asp:Label ID="Label8" runat="server" Text="Price:"></asp:Label>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                    <asp:RadioButtonList ID="rbtnQuantityOrNetSale" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" Selected="True">Quantity</asp:ListItem>
                                <asp:ListItem Value="1">NetSale Price</asp:ListItem>
                            </asp:RadioButtonList>
                                    </td>
                                    <td>
                            Min
                            <asp:TextBox ID="txtPriceMin" runat="server" Text="0"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;Max
                            <asp:TextBox ID="txtPriceMax" runat="server" Text="0"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            
                        </td>
                    </tr>
                    <tr id="tr2" runat="server">
                        <td width="132px">
                            <asp:Label ID="Label15" runat="server" Text="Condition Manual:"></asp:Label>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                    <asp:TextBox ID="txtConditionManual" runat="server" Width="30px" Text="45"></asp:TextBox>Days
                                    </td>
                                    <td width="30px">&nbsp;</td>
                                    <td>
                                        <b>Percentage</b>
                                    </td>
                                    <td>
                                    <asp:TextBox ID="txtPercentage" runat="server" Width="30px" Text="50"></asp:TextBox>%
                                    </td>
                                </tr>
                            </table>
                             
                        </td>
                    </tr>
                    <tr id="trItem" runat="server">
                        <td>
                            <asp:Label ID="Label14" runat="server" Text="Product:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProduct" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                     <tr  id="trTopSaleReport" runat="server">
                        <td width="132px">
                        <asp:Label ID="Label10" runat="server" Text="Report:"></asp:Label>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnTopSaleRport" runat="server" OnClick="btnTopSaleRport_Click"
                                            Text="Report For Sale" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 5px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:HyperLink ID="hlnkTopSaleRport" CssClass="buttonCss" Visible="false"
                                            runat="server" Target="_blank">Print Report</asp:HyperLink>
                                    </td>                                    
                                </tr>
                            </table>
                        </td>
                    </tr>
                    
                    <tr  id="trAnalysis" runat="server">
                        <td width="132px">
                            <asp:Label ID="Label11" runat="server" Text="Size Wise"></asp:Label>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnRunningSizeStockReport" runat="server" OnClick="btnRunningSizeStockReport_Click"
                                            Text="Link->Running Size Stock Report " />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 5px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:HyperLink ID="hlnkRunningSizeStockReport" CssClass="buttonCss" Visible="false"
                                            runat="server" Target="_blank">Print Report</asp:HyperLink>
                                    </td>                                    
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr  id="tr3" runat="server">
                        <td width="132px">
                            <asp:Label ID="Label16" runat="server" Text="Single Product Search"></asp:Label>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSingleProductSearchInHeadOffice" runat="server"
                                            Text="Link->Head Office" 
                                            onclick="btnSingleProductSearchInHeadOffice_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="Button1" runat="server"
                                            Text="Link->Single branch" 
                                            onclick="btnSingleProductSearchInHeadOffice_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="Button2" runat="server"
                                            Text="Link->All branch" 
                                            onclick="btnSingleProductSearchInHeadOffice_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 5px;" colspan="3">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:HyperLink ID="hlnkSingleProductSearchInHeadOffice" CssClass="buttonCss" Visible="false"
                                            runat="server" Target="_blank">Print Report</asp:HyperLink>
                                    </td>  
                                    <td>
                                        <asp:HyperLink ID="HyperLink1" CssClass="buttonCss" Visible="false"
                                            runat="server" Target="_blank">Print Report</asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink2" CssClass="buttonCss" Visible="false"
                                            runat="server" Target="_blank">Print Report</asp:HyperLink>
                                    </td>                                  
                                </tr>
                            </table>
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
