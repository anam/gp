<%@ Page Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true"
    CodeFile="IssueModification.aspx.cs" Inherits="AdminInv_IssueDetailDisplay" Title="Display Inv_IssueDetail By Admin" %>

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
                    <asp:Label ID="Label1" runat="server" Text="Last Issue ID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="lblLastIssueID" runat="server" Text="" Enabled="false">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblIssueDate" runat="server" Text="Issue Date: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtIssueDate" runat="server" Text="">
                    </asp:TextBox>
                    <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender2" runat="server" TargetControlID="txtIssueDate">
                                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr style="display:none;">
                <td>
                    <asp:Label ID="lblEmployeeID" runat="server" Text="Employee: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlEmployee" runat="server" >
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblWorkSatationID" runat="server" Text="WorkSatation: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlWorkSatation" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblParticulars" runat="server" Text="Particulars: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtParticulars" runat="server" Text="">
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
