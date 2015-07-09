<%@ Page Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true"
    CodeFile="UtilizationModification.aspx.cs" Inherits="AdminInv_IssueDetailDisplay" Title="Display Inv_IssueDetail By Admin" %>

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
    <h1>Update Utilization</h1>
     <table>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Utilization ID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="lblLastIssueID" runat="server" Text="" Enabled="false">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblIssueDate" runat="server" Text="Date: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtIssueDate" runat="server" Text="">
                    </asp:TextBox>
                    <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender2" runat="server" TargetControlID="txtIssueDate">
                                    </ajaxToolkit:CalendarExtender>
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
                    <a ID="a_IssueMasterPreview" runat="server" visible="false" target="_blank">Print Utilization Voucher</a>
                </td>
            </tr>
        </table>
        
   </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
