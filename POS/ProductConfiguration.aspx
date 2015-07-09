<%@ Page Language="C#" MasterPageFile="~/login/AdminMaster.master" AutoEventWireup="true"
    CodeFile="ProductConfiguration.aspx.cs" Inherits="AdminPos_ProductInsertUpdate"
    Title="Pos_Product Insert/Update By Admin" %>
    <%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .tableCss
        {
            text-align: left;
        }
        .gridCss td span
        {
            background: none repeat scroll 0 0 #FFF;
            border: 0px solid #F5F7F8;
            float: left;
            font-weight: normal;
            padding: 3px;
            text-align: left;
            width: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfLoginID" runat="server" />
            <h1>
                <%--Product Configuration--%>
                <asp:Label ID="lblVoucherType" runat="server" Text=""></asp:Label>
                </h1>
            
            <table runat="server" id="tblProduction1">
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Type: ">
                        </asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rbtnProductType" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Fesh" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Rejected" Value="1"></asp:ListItem>
                            <asp:ListItem Text="All" Value="All"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Color: ">
                    </asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlColorSearch" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProductionUnit" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table  runat="server" id="tblPurchase1">
                <tr >
                    <td>Size</td>
                    <td colspan="3">
                        <asp:DataList ID="dlSize"  runat="server" RepeatColumns="20" 
                        RepeatDirection="Horizontal" BackColor="White" BorderColor="#999999" 
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" ToolTip='<%#Eval("Pos_SizeID")%>' runat="server" Text='<%#Eval("SizeName")%>'/>
                        </ItemTemplate>
                        <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    </asp:DataList>
                    </td>
                </tr>
                <tr ID="tr_SuppierID" runat="server">
                <td>
                    <asp:Label ID="lblSuppierID" runat="server" Text="Suppier: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSuppier" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
                 </table>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblProductID" runat="server" Text="Product: ">
                    </asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProduct" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnPurchaseProcess" runat="server" Text="Next" OnClick="btnPurchaseProcess_Click" />
                        <asp:Button ID="btnSearch" runat="server" Text="Search Products" OnClick="btnSearch_Click" />
                    </td>
                </tr>   
            </table>
            <table   runat="server" id="tblProduction2">
                <tr>
                    <td>
                        <asp:GridView ID="gvInv_UtilizationDetails" runat="server" AutoGenerateColumns="false"
                            CssClass="gridCss">
                            <Columns>
                                <%--<asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbSelect" runat="server" CommandArgument='<%#Eval("Inv_UtilizationDetailsID") %>' OnClick="lbSelect_Click">
                            Select
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" Text='<%# Container.DataItemIndex +1 %>'
                                            ToolTip='<%#Eval("Inv_UtilizationDetailsID") %>' />
                                        <asp:HiddenField ID="hfPos_SizeID" runat="server" Value='<%#Eval("Pos_SizeID") %>' />
                                        <asp:HiddenField ID="hfRemainingQty" runat="server" Value='<%#Eval("Remaining") %>' />
                                        <asp:HiddenField ID="hfColorID" runat="server" Value='<%#Eval("ColorID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("ChartOfAccountLabel4Text") %>'>
                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fabrics">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemName" runat="server" Text='<%#Eval("ItemName") %>'>
                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Barcode">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemCode" runat="server" Text='<%#Eval("ItemCode") %>'>
                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSizeName" runat="server" Text='<%#Eval("SizeName") %>'>
                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Color">
                                    <ItemTemplate>
                                        <asp:Label ID="lblColorName" runat="server" Text='<%#Eval("ColorName") %>'>
                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Inv_UtilizationID">
                    <ItemTemplate>
                        <asp:Label ID="lblInv_UtilizationID" runat="server" Text='<%#Eval("Inv_UtilizationID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Inv_ItemTransactionID">
                    <ItemTemplate>
                        <asp:Label ID="lblInv_ItemTransactionID" runat="server" Text='<%#Eval("Inv_ItemTransactionID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Fabrics<br/>Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFabricsCost" runat="server" Text='<%#Eval("FabricsCost") %>'>
                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accesories<br/>Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccesoriesCost" runat="server" Text='<%#Eval("AccesoriesCost") %>'>
                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Overhead <br/>Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOverhead" runat="server" Text='<%#Eval("Overhead") %>'>
                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Others<br/>Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOthersCost" runat="server" Text='<%#Eval("OthersCost") %>'>
                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Production<br/>Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductionQuantity" runat="server" Text='<%#Eval("ProductionQuantity") %>'>
                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Processed<br/>Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProcessedQuantity" runat="server" Text='<%#Eval("ProcessedQuantity") %>'>
                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Processing<br/>Quantity">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblProcessedQuantity" runat="server" Text='<%#Eval("Remaining") %>'>
                        </asp:Label>--%>
                                        <asp:TextBox ID="txtCurrentProcess" runat="server" Width="50px" Text='<%#Eval("Remaining") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Condition">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsReject" runat="server" Text='<%#Eval("IsReject") %>'>
                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="ExtraField1">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraField1" runat="server" Text='<%#Eval("ExtraField1") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ExtraField2">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraField2" runat="server" Text='<%#Eval("ExtraField2") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ExtraField3">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraField3" runat="server" Text='<%#Eval("ExtraField3") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ExtraField4">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraField4" runat="server" Text='<%#Eval("ExtraField4") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ExtraField5">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraField5" runat="server" Text='<%#Eval("ExtraField5") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="AddedBy">
                    <ItemTemplate>
                        <asp:Label ID="lblAddedBy" runat="server" Text='<%#Eval("AddedBy") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAddedDate" runat="server" Text='<%#Eval("AddedDate","{0:dd/MM/yyyy}") %>'>
                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="UpdatedBy">
                    <ItemTemplate>
                        <asp:Label ID="lblUpdatedBy" runat="server" Text='<%#Eval("UpdatedBy") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UpdatedDate">
                    <ItemTemplate>
                        <asp:Label ID="lblUpdatedDate" runat="server" Text='<%#Eval("UpdatedDate") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RowStatusID">
                    <ItemTemplate>
                        <asp:Label ID="lblRowStatusID" runat="server" Text='<%#Eval("RowStatusID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%#Eval("Inv_UtilizationDetailsID") %>' OnClick="lbDelete_Click">
                            Delete
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnProcessProduction" runat="server" Text="Process Production" OnClick="btnProcessProduction_Click" />
                    </td>
                </tr>
                </table>
            <table>
                <tr>
                    <td>
                        <asp:GridView ID="gvProductConfiguration" runat="server" AutoGenerateColumns="false"
                            CssClass="gridCss">
                            <Columns>
                                <asp:TemplateField HeaderText="Size">
                                    <ItemTemplate>
                                        <%#Eval("ExtraField2")%>
                                        <asp:HiddenField ID="hfColorID" runat="server" Value='<%#Eval("Pos_ColorID") %>' />
                                        <asp:HiddenField ID="hfPos_SizeID" runat="server" Value='<%#Eval("Pos_SizeID") %>' />
                                        <asp:HiddenField ID="hfFabricsCost" runat="server" Value='<%#Eval("FabricsCost") %>' />
                                        <asp:HiddenField ID="hfAccesoriesCost" runat="server" Value='<%#Eval("AccesoriesCost") %>' />
                                        <asp:HiddenField ID="hfOverhead" runat="server" Value='<%#Eval("Overhead") %>' />
                                        <asp:HiddenField ID="hfOthersCost" runat="server" Value='<%#Eval("OthersCost") %>' />
                                        <asp:HiddenField ID="hfProductCode" runat="server" Value='<%#Eval("ExtraField4") %>' />
                                        <asp:HiddenField ID="hfDesignCode" runat="server" Value='<%#Eval("DesignCode") %>' />
                                        <asp:HiddenField ID="hfSizeCode" runat="server" Value='<%#Eval("ExtraField5") %>' />
                                        <asp:HiddenField ID="hfUtilizationIDs" runat="server" Value='<%#Eval("Inv_UtilizationDetailsIDs") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fabrics S.I.">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblBarCode" runat="server" Text='<%#Eval("BarCode") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Color">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlColor_Config" runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Costing/<br/>Purch Price">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblAgvCosting" runat="server" Width="75px" Text='<%#Eval("ExtraField3") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales Price">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSalesPrice_Config" Width="75px" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCurrentProcessing" Width="75px" runat="server" Text='<%#Eval("ExtraField1") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlPos_FabricsType" runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Last ID: " Visible="false">
                    </asp:Label>
                        Style:
                    </td>
                    <td>
                        <asp:TextBox ID="txtLastTransactionID" runat="server" Text=""  Visible="false">
                    </asp:TextBox>
                    <asp:TextBox ID="txtStyle" runat="server" Text=""  >
                    </asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Old ID: ">
                        </asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOldTrasactionID" runat="server" Text="" AutoPostBack="true" 
                            ontextchanged="txtOldTrasactionID_TextChanged">
                    </asp:TextBox>
                        <asp:HiddenField ID="hfJournalMasterID" runat="server" Value="0" />
                        <asp:HiddenField ID="hfTransactionMasterID" runat="server" Value="0" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDesignCode" runat="server" Text="DesignCode: ">
                    </asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDesignCode" runat="server" Text="">
                    </asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblPos_BrandID" runat="server" Text="Brand: ">
                    </asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPos_Brand" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblProductStatusID" runat="server" Text="Status: ">
                    </asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProductStatus" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblInv_QuantityUnitID" runat="server" Text="Unit: ">
                    </asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlInv_QuantityUnit" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNote" runat="server" Text="Note: ">
                    </asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtNote" runat="server" Text="" Width="325px" TextMode="MultiLine">
                    </asp:TextBox>
                        <asp:DropDownList ID="ddlPos_FabricsTypeAll" runat="server" Visible="false">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlPos_ColorAll" runat="server" Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblVatPercentage" runat="server" Text="Vat %: ">
                    </asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtVatPercentage" runat="server" Text="5">
                    </asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblIsVatExclusive" runat="server" Text="Vat Exclusive?: " Visible="false">
                    </asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="cbIsVatExclusive" runat="server" Checked="true" Visible="false"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDiscountPercentage" runat="server" Text="Discount %: ">
                    </asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDiscountPercentage" runat="server" Text="0">
                    </asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblIssueDate" runat="server" Text="Date: ">
                    </asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtProductionDate" runat="server" Text="">
                    </asp:TextBox>
                        <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender2" runat="server"
                            TargetControlID="txtProductionDate">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>Picture</td>
                    <td colspan="3">
                    <FCKeditorV2:FCKeditor ID="txtPicture" runat="server" BasePath="../fckeditor/"
                                        Height="300px" Width="700px">
                                    </FCKeditorV2:FCKeditor>
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td colspan="2">
                        <asp:Button ID="btnAdd" runat="server" Text="Submit" OnClick="btnAdd_Click" />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnClear" Visible="false" runat="server" Text="Clear" OnClick="btnClear_Click" />
                        <asp:HyperLink ID="hlnkProductionPrint" Visible="false" Target="_blank" runat="server">Print Voucher</asp:HyperLink>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
