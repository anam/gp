<%@ Page Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true" 
CodeFile="Modification.aspx.cs" Inherits="AdminACC_JournalDetailInsertUpdate" Title="ACC_JournalDetail Insert/Update By Admin" %>

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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
   <div class="tableCss">
    <h1>ID wise</h1>
    
        <table>
            <tr>
                <td>
                    
            <asp:Label ID="Label9" runat="server" Text="ID / ItemCode:"></asp:Label>
                </td>
                <td >
                    
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
                    <asp:Button ID="btnItemHistory" runat="server"  CssClass="buttonCss"
                        Text="Generate Link For Item Hisotry" onclick="btnItemHistory_Click" 
                        />
                    <br />
                    <asp:HyperLink ID="hlItemHistory" CssClass="buttonCss" Visible="false"  runat="server" Target="_blank">Print ItemHistory</asp:HyperLink>
                </td>
                <td>
                    
                </td>
                
                
            </tr>
            <tr>
                <td></td>
                <td>
               <asp:Button ID="btnPrintPurchaseForAdmin" runat="server"  CssClass="buttonCss"
                        Text="Generate Link For Purchase(admin) Voucher" 
                        onclick="btnPrintPurchaseForAdmin_Click"  />
                    <br />
                    <asp:HyperLink ID="hlPrintPurchaseForAdmin" CssClass="buttonCss" Visible="false"  runat="server" Target="_blank">Print Purchase Voucher</asp:HyperLink>
                
                </td>
                <td>
                <asp:Button ID="btnPrintIssueForAdmin" runat="server"  CssClass="buttonCss"
                        Text="Generate Link For Issue(admin) Voucher" 
                        onclick="btnPrintIssueForAdmin_Click"  />
                    <br />
                    <asp:HyperLink ID="hlPrintIssueForAdmin" CssClass="buttonCss" Visible="false"  runat="server" Target="_blank">Print Issue Voucher</asp:HyperLink>
                </td>
                
            <td>
                
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
               
                </td>
                <td>
                <asp:Button ID="btnPrintIssueReturenForAdmin" runat="server"  CssClass="buttonCss"
                        Text="Generate Link For Issue Return(admin) Voucher" 
                        onclick="btnPrintIssueReturnForAdmin_Click"  />
                    <br />
                    <asp:HyperLink ID="hlPrintIssueReturnForAdmin" CssClass="buttonCss" Visible="false"  runat="server" Target="_blank">Print Issue Voucher</asp:HyperLink>
                </td>
                
            <td>
                
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
               
                </td>
                <td>
                <asp:Button ID="btnPrintUtilizationForAdmin" runat="server"  CssClass="buttonCss"
                        Text="Generate Link For Utilization(admin) Voucher" 
                        onclick="btnPrintUtilizationForAdmin_Click"  />
                    <br />
                    <asp:HyperLink ID="hlPrintUtilizationForAdmin" CssClass="buttonCss" Visible="false"  runat="server" Target="_blank">Print Utilization Voucher</asp:HyperLink>
                </td>
                
            <td>
                
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                <asp:Button ID="btnDeletePurchase" runat="server"  CssClass="buttonCss"
                        Text="Delete Purchase" onclick="btnDeletePurchase_Click" />
                </td>
            <td>
                <asp:Button ID="btnDeletePurchaseReturn" runat="server"  CssClass="buttonCss"
                        Text="Delete Purchase Return" 
                    onclick="btnDeletePurchaseReturn_Click" />
                    <br />
                    <asp:HyperLink ID="hlPrintPurchaseReturnForAdmin" CssClass="buttonCss" Visible="false"  runat="server" Target="_blank">Print Purchase Return Voucher</asp:HyperLink>
                </td>
            </tr>
            
            
            <tr>
                <td></td>

            <td>
                <asp:Button ID="btnDeleteAdjustment" runat="server"  CssClass="buttonCss"
                        Text="Delete Adjustment" onclick="btnDeleteAdjustment_Click" />
                </td>
             <td>
                <asp:Button ID="btnDeleteIssue" runat="server"  CssClass="buttonCss"
                        Text="Delete Issue" onclick="btnDeleteIssue_Click" />
                </td>
            </tr>
            </table>
          
    </div>
   
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
