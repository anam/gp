<%@ Page Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true" 
CodeFile="IssueOld.aspx.cs" Inherits="AdminInv_IssueDetailInsertUpdate" Title="Inv_IssueDetail Insert/Update By Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .tableCss
        {
        	text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
  <h1>Issue</h1>
  <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblIssueDate" runat="server" Text="Issue Date: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtIssueDate" runat="server" Text="">
                    </asp:TextBox>
                    <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender1" runat="server" TargetControlID="txtIssueDate">
                                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEmployeeID" runat="server" Text="Employee: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlEmployee" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlEmployee_SelectedIndexChanged">
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
        </table>
    </div> <div>
        <table>
        
            <tr id="trProduct" runat="server">
                <td>
                    <asp:Label ID="lblProductID" runat="server" Text="Product: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlProduct_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtItemCode" runat="server" Text="" AutoPostBack="True" 
                        ontextchanged="txtItemCode_TextChanged" >
                    </asp:TextBox>
                   
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblItemID" runat="server" Text="Item: ">
                    </asp:Label>
                </td>
                <td>
                
                    <asp:DropDownList ID="ddlItem" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Issed Item: ">
                    </asp:Label>
                </td>
                <td>
                    
                    <asp:DropDownList ID="ddlIssuedItem" runat="server" AutoPostBack="true"
                        onselectedindexchanged="ddlIssuedItem_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr >
                <td>
                    <asp:Label ID="lblQuantity" runat="server" Text="Quantity: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtQuantity" runat="server" Text="0" AutoPostBack="true"  
                        ontextchanged="txtQuantity_TextChanged">
                    </asp:TextBox>
                </td>
            </tr>
            <tr id="trConfigPerProductRequiredQuantity" runat="server" visible="false" style="display:none;">
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Per produt Quantity: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtConfigPerProductRequiredQuantity" runat="server" Text="0" 
                        AutoPostBack="True" 
                        ontextchanged="txtConfigPerProductRequiredQuantity_TextChanged"></asp:TextBox> 
                </td>
            </tr>
            <tr id="trClaculateApproximateQuantity" runat="server">
                <td>
                    
                </td>
                <td>
                    <asp:Button ID="btnClaculateApproximateQuantity" Visible="false" runat="server" Text="Calculate" OnClick="btnClaculateApproximateQuantity_Click" />
                     
                </td>
            </tr>
            <tr id="trApproximateQuantity" runat="server" >
                <td>
                    <asp:Label ID="lblApproximateQuantity" runat="server" Text="ApproximateQuantity: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtApproximateQuantity" runat="server" Text="0">
                    </asp:TextBox>
                </td>
            </tr>

            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                     
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false"
                        OnClick="btnUpdate_Click" />
               
                    <asp:HiddenField ID="hfInv_IssueMasterID" runat="server" Value="0"/>
                    <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click"  Visible="false"/>
                </td>
            </tr>
        </table>
   </div>
        <h1>Issue Item List preview</h1>
        <asp:GridView ID="gvInv_IssueDetail" runat="server" AutoGenerateColumns="false" CssClass="gridCss">
            <Columns>
                <asp:TemplateField HeaderText="S.I.">
                    <ItemTemplate>
                        <asp:Label ID="gap" runat="server" Text='<%#Eval("ParentChildGap") %>'>
                        </asp:Label>
                        <%# Container.DataItemIndex +1 %>
            <asp:HiddenField ID="hfInv_IssueDetailID" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Added With">
                    <ItemTemplate>
                        <asp:Label ID="lblAdditionalWithIssueDetailID" runat="server" Text='<%#Eval("AdditionalWithIssueDetailID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Code">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%#Eval("ItemCode") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="lblItemName" runat="server" Text='<%#Eval("ItemName") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit Price">
                    <ItemTemplate>
                        <asp:Label ID="lblPricePerUnit" runat="server" Text='<%#Eval("PricePerUnit") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount">
                    <ItemTemplate>
                        <asp:Label ID="lblTotalPrice" runat="server" Text='<%#Eval("TotalPrice","{0:0,0.00}") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quality">
                    <ItemTemplate>
                        <asp:Label ID="lblQualityUnitValue" runat="server" Text='<%#Eval("QualityUnitValue") %>'>
                        </asp:Label>&nbsp;-&nbsp;
                        <asp:Label ID="lblQualityUnitName" runat="server" Text='<%#Eval("QualityUnitName") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Issue For">
                    <ItemTemplate>
                        <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("ProductName") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ApproximateQuantity">
                    <ItemTemplate>
                        <asp:Label ID="lblApproximateQuantity" runat="server" Text='<%#Eval("ApproximateQuantity") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%#Eval("Inv_IssueDetailID") %>' OnClick="lbDelete_Click">
                            Delete
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                </td>
                <td>
                    <asp:HyperLink ID="hlnkIssuePrint" CssClass="buttonCss" Visible="false" runat="server" Target="_blank">Print Issue Report</asp:HyperLink>
                </td>
            </tr>
        </table>
        
   </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
