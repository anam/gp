<%@ Page Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true" 
CodeFile="Issue.aspx.cs" Inherits="AdminInv_ProductInsertUpdate" Title="Issue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .tableCss
        {
        	text-align: left;
        }
        .gridTxt
        {
            width:60px;
            text-align:right;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
     <div class="tableCss">
     <h1>Issue</h1>
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
                    <asp:Label ID="Label2" runat="server" Text="Old Issue ID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtOldIssueID" runat="server" Text="" >
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
        </table>
    </div>
    <div>
    <h1>Stock</h1>
   
<asp:DropDownList ID="ddlProductAll" runat="server" Visible="false">
</asp:DropDownList>
 <asp:GridView ID="gvInv_Item" runat="server" AutoGenerateColumns="false" CssClass="gridCss">
            <Columns>
                
                <asp:TemplateField HeaderText="Item">
                    <ItemTemplate>
                        <asp:Label ID="lblItemName" runat="server" Text='<%#Eval("ItemName") %>'>
                        </asp:Label>
                        <asp:HiddenField ID="hfInv_ItemID" runat="server" Value='<%#Eval("Inv_ItemID") %>'/>
                        <br />
                        <asp:Label ID="lblRawMaterialTypeName" Font-Bold="true" ForeColor="Blue" runat="server" Text='<%#Eval("RawMaterialTypeName") %>'>
                        </asp:Label>
                        <asp:HiddenField ID="hfRawMaterialID" runat="server" Value='<%#Eval("RawMaterialID") %>'/>
                        <asp:HiddenField ID="hfRawMaterialTypeID" runat="server" Value='<%#Eval("RawMaterialTypeID") %>'/>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Code">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%#Eval("ItemCode") %>'>
                        </asp:Label>
                        <br />
                        Purchase ID:<asp:Label ID="lblPurchaseID" runat="server" Text='<%#Eval("PurchaseID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Quality">
                    <ItemTemplate>
                        <asp:Label ID="lblQualityValue" runat="server" Text='<%#Eval("QualityValue") %>'>
                        </asp:Label>
                        <br />
                        <asp:Label ID="lblQualityUnitName" runat="server" Text='<%#Eval("QualityUnitName") %>'>
                        </asp:Label>
                         <asp:HiddenField ID="hfQualityUnitID" runat="server" Value='<%#Eval("QualityUnitID") %>'/>

                        
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="Purchased<br>Quantity">
                    <ItemTemplate>
                        <asp:Label ID="lblPurchasedQuantity" runat="server" Text='<%#Eval("PurchasedQuantity") %>'>
                        </asp:Label>
                        -
                        <asp:Label ID="lblQuantityUnitID" runat="server" Text='<%#Eval("QuantityUnitName") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Price/Unit">
                    <ItemTemplate>
                        <asp:Label ID="lblPricePerUnit" runat="server" Text='<%#Eval("PricePerUnit") %>'>
                        </asp:Label> tk
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Available Stock">
                    <ItemTemplate>
                       Available: <asp:Label ID="lblExtraFieldQuantity1" runat="server" Text='<%#Eval("ExtraFieldQuantity1") %>'>
                        </asp:Label>-
                        <asp:Label ID="lblQuantityUnitID" runat="server" Text='<%#Eval("QuantityUnitName") %>'>
                        </asp:Label>
                        <asp:HiddenField ID="hfQuantityUnitID" runat="server" Value='<%#Eval("QuantityUnitID") %>'/>

                        <br />
                       Issue Qty:<asp:TextBox ID="txtIssueQuantity" runat="server"  style="width:50px;text-align:right;" Text=""></asp:TextBox>
                    
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Issue">
                    <ItemTemplate>
                        
                        <asp:DropDownList ID="ddlProduct" runat="server" Width="225px">
                        </asp:DropDownList>
                        <br />
                       Appx. Qty:<asp:TextBox ID="txtApproximateQuantity" runat="server"  style="width:50px;text-align:right;"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Price">
                    <ItemTemplate>
                        <asp:Label ID="lblPurchasedQuantityPrice" runat="server" Text='<%#Eval("PurchasedQuantityPrice","{0:0,0.00}") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView> 
        
        <asp:Button ID="btnPreviewTotal" runat="server" Text="Preview" 
            onclick="btnPreviewTotal_Click" />  
        
        <asp:Label ID="lblPreview" runat="server" Text=""></asp:Label>    
         </div>
    <div class="tableCss">
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnCalculateAppoximateQuantity" runat="server" 
                        Text="Calculate Appoximate Quantity" 
                        onclick="btnCalculateAppoximateQuantity_Click"/>
                 </td>
                <td>
                    <asp:HiddenField ID="hfLoginID" runat="server" />
                    <asp:Button ID="btnAdd" runat="server" Text="Submit" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false"
                        OnClick="btnUpdate_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click"  Visible="false" />
                </td>
                <td>
                    <asp:HyperLink ID="hlnkIssuePrint" CssClass="buttonCss" Visible="false" runat="server" Target="_blank">Print Issue Voucher</asp:HyperLink>
                </td>
                
            </tr>
        </table>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
