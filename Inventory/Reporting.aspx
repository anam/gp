<%@ Page Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true"
    CodeFile="Reporting.aspx.cs" Inherits="AdminACC_JournalDetailInsertUpdate" Title="ACC_JournalDetail Insert/Update By Admin" %>

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
                    ID wise Print</h1>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="ID / ItemCode:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtID" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblDeleteMessage" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnItemHistory" runat="server" CssClass="buttonCss" Text="Generate Link For Item Hisotry"
                                OnClick="btnItemHistory_Click" />
                            <br />
                            <asp:HyperLink ID="hlItemHistory" CssClass="buttonCss" Visible="false" runat="server"
                                Target="_blank">Print ItemHistory</asp:HyperLink>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnPrintPurchaseID" runat="server" CssClass="buttonCss" Text="Generate Link For Purchase Voucher"
                                OnClick="btnPrintPurchaseID_Click" />
                            <br />
                            <asp:HyperLink ID="hlPrintPurchaseID" CssClass="buttonCss" Visible="false" runat="server"
                                Target="_blank">Print Purchase Voucher</asp:HyperLink>
                        </td>
                        <td>
                            <asp:Button ID="btnPrintPurchaseReturnID" runat="server" CssClass="buttonCss" Text="Generate Link For Purchase Return Voucher"
                                OnClick="btnPrintPurchaseReturnID_Click" />
                            <br />
                            <asp:HyperLink ID="hlPrintPurchaseReturnID" CssClass="buttonCss" Visible="false"
                                runat="server" Target="_blank">Print Purchase Return Voucher</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnPrintAdjustment" runat="server" CssClass="buttonCss" Text="Generate Link For Adjustment Voucher"
                                OnClick="btnPrintAdjustment_Click" />
                            <br />
                            <asp:HyperLink ID="hlPrintAdjustment" CssClass="buttonCss" Visible="false" runat="server"
                                Target="_blank">Print Adjustment Voucher</asp:HyperLink>
                        </td>
                        <td>
                            <asp:Button ID="btnPrintIssue" runat="server" CssClass="buttonCss" Text="Generate Link For Issue Voucher"
                                OnClick="btnPrintIssue_Click" />
                            <br />
                            <asp:HyperLink ID="hlPrintIssue" CssClass="buttonCss" Visible="false" runat="server"
                                Target="_blank">Print Issue Voucher</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnPrintIssueReturn" runat="server" CssClass="buttonCss" Text="Generate Link For Issue Return Voucher"
                                OnClick="btnPrintIssueReturn_Click" />
                            <br />
                            <asp:HyperLink ID="hlPrintIssueReturn" CssClass="buttonCss" Visible="false" runat="server"
                                Target="_blank">Print Issue Return Voucher</asp:HyperLink>
                        </td>
                        <td>
                            <asp:Button ID="btnPrintIssueForAdmin" runat="server" CssClass="buttonCss" Text="Generate Link For Issue(admin) Voucher"
                                OnClick="btnPrintIssueForAdmin_Click" />
                            <br />
                            <asp:HyperLink ID="hlPrintIssueForAdmin" CssClass="buttonCss" Visible="false" runat="server"
                                Target="_blank">Print Issue Voucher</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnUtilizationPrint" runat="server" CssClass="buttonCss" Text="Generate Link For Utilization Voucher"
                                OnClick="btnUtilizationPrint_Click" />
                            <br />
                            <asp:HyperLink ID="hlUtilizationPrint" CssClass="buttonCss" Visible="false" runat="server"
                                Target="_blank">Print Utilization Voucher</asp:HyperLink>
                        </td>
                        <td>
                            <asp:Button ID="btnUtilizationPrintAdmin" runat="server" CssClass="buttonCss" Text="Generate Link For Utilization(admin) Voucher"
                                OnClick="btnUtilizationPrintAdmin_Click" />
                            <br />
                            <asp:HyperLink ID="hlUtilizationPrintAdmin" CssClass="buttonCss" Visible="false" runat="server"
                                Target="_blank">Print Utilization(Admin) Voucher</asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="tableCss">
                <table>
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
                            <asp:Label ID="Label7" runat="server" Text="Item Type:"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rbtnlRawmaterialsType" runat="server"
                                RepeatDirection="Horizontal" >
                                <asp:ListItem Value="2" Selected="True" Text="Fabrics"></asp:ListItem>
                                <asp:ListItem Value="9" Text="Accessories(Prod)"></asp:ListItem>
                                <asp:ListItem Value="10" Text="Accessories(Non Prod)"></asp:ListItem>
                                <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                
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
                </table>
            </div>
            <div class="tableCss">
                <h1>
                    Transaction report</h1>
                <table>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="Generate Link For All Supplier Item Group Purchase Report"
                                OnClick="btnAllSupplierItemReport_Click" />
                            <br />
                            <asp:HyperLink ID="hlnkPurchaseReportSupplierWise" CssClass="buttonCss" Visible="false"
                                runat="server" Target="_blank">Print All Supplier Item Group Purchase Report</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Supplier:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlACC_ChartOfAccountLabel4" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnSupplierwiseReport" runat="server" Text="Generate Link For Supplier Wise Purchase Report"
                                OnClick="btnSupplierwiseReport_Click" />
                            <br />
                            <asp:HyperLink ID="hlnkTransactionSummary" CssClass="buttonCss" Visible="false" runat="server"
                                Target="_blank">Print Supplier Wise Purchase Report</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnSupplierwisePurchaseReturnReport" runat="server" Text="Generate Link For Supplier Wise Purchase Return Report"
                                OnClick="btnSupplierwisePurchaseReturnReport_Click" />
                            <br />
                            <asp:HyperLink ID="hlnkSupplierwisePurchaseReturnReport" CssClass="buttonCss" Visible="false"
                                runat="server" Target="_blank">Print Supplier Wise Purchase Return Report</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnSupplierwiseAdjustmentReport" runat="server" Text="Generate Link For Supplier Wise Adjustment Report"
                                OnClick="btnSupplierwiseAdjustmentReport_Click" />
                            <br />
                            <asp:HyperLink ID="hlnkSupplierwisePurchaseAdjustment" CssClass="buttonCss" Visible="false"
                                runat="server" Target="_blank">Print Supplier Wise Adjustment Report</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="WorkStation:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlWorkStationForTransactionReport" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnWorkStationWiseIssueReport" runat="server" Text="Generate Link For WorkStation Wise Issue Report"
                                OnClick="btnWorkStationWiseIssueReport_Click" />
                            <br />
                            <asp:HyperLink ID="hlnkWorkStationWiseIssueReport" CssClass="buttonCss" Visible="false"
                                runat="server" Target="_blank">Print WorkStation Wise Issue Report</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnWorkStataionWiseIssueReturnReport" runat="server" Text="Generate Link For WorkStation Wise Issue return Report"
                                OnClick="btnWorkStationWiseIssueReturnReport_Click" />
                            <br />
                            <asp:HyperLink ID="hlnkWorkStationWiseIssueReturnReport" CssClass="buttonCss" Visible="false"
                                runat="server" Target="_blank">Print WorkStation Wise Issue return Report</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnWorkStationWiseUtilizationReport" runat="server" Text="Generate Link For WorkStation Wise Utilization Report(Rawmetarials Based)"
                                OnClick="btnWorkStationWiseUtilizationReport_Click" />
                            <br />
                            <asp:HyperLink ID="hlWorkStationWiseUtilizationReport" CssClass="buttonCss" Visible="false"
                                runat="server" Target="_blank">Print WorkStation Wise Utilization Report(Rawmetarials based)</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnWorkStationWiseUtilizationProductBasedReport" runat="server" Text="Generate Link For WorkStation Wise Utilization Report(Product Based)"
                                OnClick="btnWorkStationWiseUtilizationProductBasedReport_Click" />
                            <br />
                            <asp:HyperLink ID="hlnkWorkStationWiseUtilizationProductBasedReport" CssClass="buttonCss" Visible="false"
                                runat="server" Target="_blank">Print WorkStation Wise Utilization Report(Product based)</asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="tableCss">
                <h1>
                    WorkStation Wise Stock report</h1>
                <table>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnGenerateBackDatedStockReport" runat="server" CssClass="buttonCss"
                                Text="Generate Link For Date Wise Stock Report" OnClick="btnGenerateBackDatedStockReport_Click" />
                            <br />
                            <asp:HyperLink ID="hlnkbackDatedStockReport" CssClass="buttonCss" Visible="false"
                                runat="server" Target="_blank">Print Date Wise Stock Report</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <br />
                            <asp:HyperLink ID="HyperLink3" CssClass="buttonCss" runat="server" NavigateUrl="StockReportInCentralPrint.aspx"
                                Target="_blank">Central Stock Report(Item Group wise)</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <br />
                            <asp:HyperLink ID="HyperLink1" CssClass="buttonCss" runat="server" NavigateUrl="StockReportInCentralItemWisePrint.aspx"
                                Target="_blank">Central Stock Report(Item Wise)</asp:HyperLink>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="WorkStation:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlWorkStationID" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="WorkStation:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" Text=""></asp:TextBox>
                            <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender1" runat="server"
                                TargetControlID="txtFromDate">
                            </ajaxToolkit:CalendarExtender>
                            &nbsp;To&nbsp;
                            <asp:TextBox ID="TextBox2" runat="server" Text=""></asp:TextBox>
                            <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender2" runat="server"
                                TargetControlID="txtToDate">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnGenerateWorkStationWiseStockReport" runat="server" CssClass="buttonCss"
                                Text="Generate Link For WorkStation Wise Stock Report" OnClick="btnGenerateWorkStationWiseStockReport_Click" />
                            <br />
                            <asp:HyperLink ID="hlnkWorkStationWiseStockReport" CssClass="buttonCss" Visible="false"
                                runat="server" Target="_blank">Print WorkStation Wise Stock Report</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnGenerateBackDatedWorkStationStockReport" runat="server" CssClass="buttonCss"
                                Text="Generate Link For Date Wise WorkStation Stock Report" OnClick="btnGenerateBackDatedWorkStationStockReport_Click" />
                            <br />
                            <asp:HyperLink ID="hlnkWorkStationWiseStockReportDateWise" CssClass="buttonCss" Visible="false"
                                runat="server" Target="_blank">Print Date Wise WorkStation Stock Report</asp:HyperLink>
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
