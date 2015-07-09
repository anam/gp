<%@ Page Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true" 
CodeFile="Purchase.aspx.cs" Inherits="AdminInv_ItemInsertUpdate" Title="Inv_Item Insert/Update By Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .tableCss
        {
        	text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   
    
    
 <div class="tableCss">
<h1>Inventory Purchase</h1>
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Last Purchase ID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="lblLastPurchaseID" runat="server" Text="" Enabled="false">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Old Purchase ID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtOldPurchaseID" runat="server" Text="" >
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
                    <asp:Label ID="lblSuppierID" runat="server" Text="Suppier: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSuppier" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Type: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rbtnlPaymentType" Enabled="false" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Credit" Selected="True">Credit</asp:ListItem>
                    <asp:ListItem Value="Cash">Cash</asp:ListItem>
                    </asp:RadioButtonList>
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
        </table>
    </div>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="tableCss">
    
        
        <asp:HiddenField ID="hfLoginID" runat="server" />
        <table>
            
            <tr>
                <td>
                    <asp:Label ID="lblRawMaterialID" runat="server" Text="Name: ">
                    </asp:Label>
                </td>
                <td>
                 <asp:RadioButtonList ID="rbtnlRawmaterialsType" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                        onselectedindexchanged="rbtnlRawmaterialsType_SelectedIndexChanged">
        <asp:ListItem Value="2" Selected="True" Text="Fabrics"></asp:ListItem>
        <asp:ListItem Value="9" Text="Accessories(Prod)"></asp:ListItem>
        <asp:ListItem Value="10" Text="Accessories(Non Prod)"></asp:ListItem>
        </asp:RadioButtonList>
        <asp:DropDownList ID="ddlRawMaterialAll" Visible="false" runat="server" >
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlRawMaterial" runat="server">
                    </asp:DropDownList>
            <asp:Button ID="btnLoadNewlyAddedRawmaterials" runat="server" 
                        Text="Reload Rawmaterials" onclick="btnLoadNewlyAddedRawmaterials_Click" />
                </td>
            </tr>
             <tr id="tr_StockItemList" runat="server" visible="false">
                <td>
                    <asp:Label ID="lblItemID" runat="server" Text="Item: ">
                    </asp:Label>
                </td>
                <td>
                    
       
                    <asp:DropDownList ID="ddlItem" runat="server" AutoPostBack="true"
                        onselectedindexchanged="ddlItem_SelectedIndexChanged">
                    </asp:DropDownList>
                    
                </td>
            </tr>
            
            <tr id="trProduct" runat="server">
                <td>
                    <asp:Label ID="Label2" runat="server" Text="For: ">
                    </asp:Label>
                </td>
                <td>
                 <asp:DropDownList ID="ddlProduct" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trFabricsType" runat="server" style="display:none;">
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Fabrics Type: ">
                    </asp:Label>
                </td>
                <td>
                 <asp:DropDownList ID="ddlFabricsTypeID" runat="server">
                 <asp:ListItem Value="1">100% Cotton</asp:ListItem>
                 <asp:ListItem Value="2">Blended</asp:ListItem>
                 <asp:ListItem Value="3">Mixed Fabrics</asp:ListItem>
                 <asp:ListItem Value="4">ND</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trFabricsColor" runat="server" style="display:none;">
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Color: ">
                    </asp:Label>
                </td>
                <td>
                 <asp:DropDownList ID="ddlColor" runat="server">
                        <asp:ListItem Value="8">ND</asp:ListItem>
                        <asp:ListItem Value="5">Black</asp:ListItem>
                        <asp:ListItem Value="1">deepBlack</asp:ListItem>
                        <asp:ListItem Value="2">GrayWhite</asp:ListItem>
                        <asp:ListItem Value="6">Red </asp:ListItem>
                        <asp:ListItem Value="7">Reds</asp:ListItem>
                        <asp:ListItem Value="3">White</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPricePerUnit" runat="server" Text="PricePerUnit: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPricePerUnit" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            
            <tr id="trQuality" runat="server">
                <td>
                    <asp:Label ID="lblQualityUnitID" runat="server" Text="Quality: ">
                    </asp:Label>
                </td>
                <td>
                 <asp:TextBox ID="txtQualityValue" runat="server" Text="">
                    </asp:TextBox>
                    <asp:DropDownList ID="ddlQualityUnit" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            
            <tr>
                <td>
                    <asp:Label ID="lblQuantityUnitID" runat="server" Text="Quantity:">
                    </asp:Label>
                </td>
                <td>
                <asp:TextBox ID="txtPurchasedQuantity" runat="server" Text="">
                    </asp:TextBox>
                    <asp:DropDownList ID="ddlQuantityUnit" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Serial:">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtItemCode" runat="server" Text="0">
                    </asp:TextBox>
                </td>
            </tr>
            
            <tr>
               <td></td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                     
                            <asp:HiddenField ID="hfPurchaseIDEdit" runat="server" Value="0"/>
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false"
                        OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnBackToAdd" runat="server" Text="Back to Add" Visible="false" onclick="btnBackToAdd_Click"
                         />
                            <asp:Label ID="lblMsgWornInput"  runat="server" Text="Wrong Input!!" Visible="false"></asp:Label>
               
                    <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click"  Visible="false"/>
                </td>
            </tr>
        </table>
       
    </div>
    <div>
    <h1>Preview Purchase List</h1>
   
    <asp:GridView ID="gvInv_Item" runat="server" AutoGenerateColumns="false" ShowFooter="true" CssClass="gridCss">
            <Columns>
             <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:Label ID="lblPurchaseID" runat="server" Text='<%#Eval("PurchaseID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ItemName">
                    <ItemTemplate>
                        <asp:Label ID="lblItemName" runat="server" Text='<%#Eval("ItemName") %>'>
                        </asp:Label>
                        <asp:HiddenField ID="hfRawMaterialID" runat="server" Value='<%#Eval("RawMaterialID") %>'/>
                        <asp:HiddenField ID="hfQualityUnitID" runat="server" Value='<%#Eval("QualityUnitID") %>'/>
                        <asp:HiddenField ID="hfQuantityUnitID" runat="server" Value='<%#Eval("QuantityUnitID") %>'/>
                        <asp:HiddenField ID="hfExtraField3" runat="server" Value='<%#Eval("ExtraField3") %>'/>
                        <asp:HiddenField ID="hfExtraField4" runat="server" Value='<%#Eval("ExtraField4") %>'/>
                        <asp:HiddenField ID="hfExtraField5" runat="server" Value='<%#Eval("ExtraField5") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Serial">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%#Eval("ItemCode") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Quality">
                    <ItemTemplate>
                        <asp:Label ID="lblQualityValue" runat="server" Text='<%#Eval("QualityValue") %>'>
                        </asp:Label>
                        -
                        <asp:Label ID="lblQualityUnitName" runat="server" Text='<%#Eval("QualityUnitName") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Purchased Quantity">
                    <ItemTemplate>
                        <asp:Label ID="lblPurchasedQuantity" runat="server" Text='<%#Eval("PurchasedQuantity") %>'>
                        </asp:Label>
                        -
                        <asp:Label ID="lblQuantityUnitName" runat="server" Text='<%#Eval("QuantityUnitName") %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblPurchasedQuantityFooter" runat="server">
                        </asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price/Unit">
                    <ItemTemplate>
                        <asp:Label ID="lblPricePerUnit" runat="server" Text='<%#Eval("PricePerUnit") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Price">
                    <ItemTemplate>
                        <asp:Label ID="lblPurchasedQuantityPrice" runat="server" Text='<%#Eval("PurchasedQuantityPrice","{0:0,0.00}") %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblPurchasedQuantityPriceFooter" runat="server">
                        </asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%#Eval("PurchaseID") %>' OnClick="lbEdit_Click">
                            Edit
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbDelete"  runat="server" CommandArgument='<%#Eval("PurchaseID") %>' OnClick="lbDelete_Click">
                            Delete
                        </asp:LinkButton>
                        
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <table>
            <tr>
                <td><asp:Button ID="btnSubmit" runat="server" Text="Submit" 
            onclick="btnSubmit_Click" /></td>
                <td><asp:HyperLink ID="hlnkPurchasePrint" CssClass="buttonCss" Visible="false" runat="server" Target="_blank">Print Purchase Report</asp:HyperLink>
                </td>
            </tr>
        </table>
        
           
    </div>
     </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
