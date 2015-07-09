<%@ Page Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true"
    CodeFile="PurchaseModification.aspx.cs" Inherits="AdminInv_IssueDetailDisplay" Title="Display Inv_IssueDetail By Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .gridCss
        {
            width: 100%;
            padding: 20px 10px 10px 10px;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <h1>Update Issue</h1>
     <table>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Purchase ID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="lblPurchaseID" runat="server" Text="" Enabled="false">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPurchseDate" runat="server" Text="PurchseDate: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPurchseDate" runat="server" Text="">
                    </asp:TextBox>
                    <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender1" runat="server" TargetControlID="txtPurchseDate">
                                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
           <tr>
                <td>
                    <asp:Label ID="lblWorkSatationID" runat="server" Text="Supplier: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSupplier" runat="server">
                    </asp:DropDownList>
                                        <asp:HiddenField ID="hfJournalMasterID" runat="server" />
                                        <asp:HiddenField ID="hfSupplier" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblInvoiceNo" runat="server" Text="InvoiceNo: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtInvoiceNo" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblParticulars" runat="server" Text="Particulars: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtParticulars" runat="server" Text="" Width="410px">
                    </asp:TextBox>
                </td>
            </tr>
       
            <tr>
                <td></td>
                <td>
                
                     <asp:Button ID="btnSaveIssueMaster" runat="server" Text="Save" 
                         onclick="btnSaveIssueMaster_Click" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <a ID="a_IssueMasterPreview" runat="server" visible="false" target="_blank">Print Issue Voucher</a>
                </td>
            </tr>
        </table>
        
   </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
