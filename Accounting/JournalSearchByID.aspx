<%@ Page Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true"
    CodeFile="JournalSearchByID.aspx.cs" Inherits="AdminACC_JournalDetailInsertUpdate"
    Title="ACC_JournalDetail Insert/Update By Admin" %>

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
    <div class="tableCss">
        <h1>
            Journal By ID</h1>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hfLoginID" runat="server" />
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Vourchar #:"></asp:Label>
                        </td>
                        
                        <td>
                            <asp:TextBox ID="txtJournalMasterID" runat="server" Text=""></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Inv Purchase #:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPurchaseID" runat="server" Text=""></asp:TextBox>
                        </td>
                        </tr>
                    
                    <tr>
                        <td>
                        </td>
                        <td>
                            
                            <asp:Button ID="btnSearchJournalByID" runat="server" Text="Show Journal" OnClick="btnTransactionSummary_Click" />
                            </td>
                            <td></td>
                            <td>
                            <asp:Button ID="btnSearchByPurchaseID" runat="server" Text="Show Journal" OnClick="btnSearchByPurchaseID_Click" />
                            </td>
                            </tr>
                            <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnDeleteJournal" runat="server" Text="Delete" 
                                onclick="btnDeleteJournal_Click" />
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnReGenerateJournal" runat="server" 
                                onclick="btnReGenerateJournal_Click" Text="Re-Generate Journal" />
                        </td>
                        </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:Button ID="btnJournalEdit" runat="server" onclick="btnJournalEdit_Click" 
                                        Text="Edit" />
                                    <asp:Label ID="lblEditLink" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                    </tr>
                            <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="POS Purchase #:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPosPurchaseID" runat="server" Text=""></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Inventory Issue #:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtIssueID" runat="server" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                            <td></td>
                            <td>
                            <asp:Button ID="btnSearchByPosPurchaseID" runat="server" Text="Show Journal" OnClick="btnSearchByPosPurchaseID_Click" />
                            </td>
                            <td></td>
                            <td>
                            <asp:Button ID="btnSearchByIssueID" runat="server" Text="Show Journal" OnClick="btnSearchByIssueID_Click" />
                            </td>
                    </tr>
                     <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Utilization #:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUtilizationID" runat="server" Text=""></asp:TextBox>
                        </td>
                        <td>
                            
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                            <td></td>
                            <td>
                            <asp:Button ID="btnSearchByUtilizationID" runat="server" Text="Show Journal" 
                                    onclick="btnSearchByUtilizationID_Click"  />
                            </td>
                            <td></td>
                            <td>
                            </td>
                    </tr>
                    
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>

                        <td>
                            <asp:HyperLink ID="hlnkJournalEdit" CssClass="buttonCss" Visible="false" runat="server" Target="_blank">Edit</asp:HyperLink>
                        </td>
                        </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                          <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <asp:Literal ID="ltrlJournalPreview" runat="server"></asp:Literal>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
