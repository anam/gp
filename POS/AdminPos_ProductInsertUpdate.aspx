<%@ Page Language="C#" MasterPageFile="~/login/AdminMaster.master" AutoEventWireup="true" 
CodeFile="AdminPos_ProductInsertUpdate.aspx.cs" Inherits="AdminPos_ProductInsertUpdate" Title="Pos_Product Insert/Update By Admin" %>

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
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblProductID" runat="server" Text="ProductID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlProduct" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblReferenceID" runat="server" Text="ReferenceID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlReference" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPos_ProductTypeID" runat="server" Text="Pos_ProductTypeID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPos_ProductType" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblInv_UtilizationDetailsIDs" runat="server" Text="Inv_UtilizationDetailsIDs: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlInv_UtilizationDetails" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblProductStatusID" runat="server" Text="ProductStatusID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlProductStatus" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblProductName" runat="server" Text="ProductName: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtProductName" runat="server" Text="">
                    </asp:TextBox>
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
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPos_SizeID" runat="server" Text="Pos_SizeID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPos_Size" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPos_BrandID" runat="server" Text="Pos_BrandID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPos_Brand" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblInv_QuantityUnitID" runat="server" Text="Inv_QuantityUnitID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlInv_QuantityUnit" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFabricsCost" runat="server" Text="FabricsCost: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFabricsCost" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAccesoriesCost" runat="server" Text="AccesoriesCost: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAccesoriesCost" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblOverhead" runat="server" Text="Overhead: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtOverhead" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblOthersCost" runat="server" Text="OthersCost: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtOthersCost" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPurchasePrice" runat="server" Text="PurchasePrice: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPurchasePrice" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSalePrice" runat="server" Text="SalePrice: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSalePrice" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblOldSalePrice" runat="server" Text="OldSalePrice: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtOldSalePrice" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblNote" runat="server" Text="Note: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNote" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblBarCode" runat="server" Text="BarCode: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtBarCode" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPos_ColorID" runat="server" Text="Pos_ColorID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPos_Color" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPos_FabricsTypeID" runat="server" Text="Pos_FabricsTypeID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPos_FabricsType" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblStyleCode" runat="server" Text="StyleCode: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtStyleCode" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPic1" runat="server" Text="Pic1: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPic1" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPic2" runat="server" Text="Pic2: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPic2" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPic3" runat="server" Text="Pic3: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPic3" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblVatPercentage" runat="server" Text="VatPercentage: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtVatPercentage" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblIsVatExclusive" runat="server" Text="IsVatExclusive: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="cbIsVatExclusive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDiscountPercentage" runat="server" Text="DiscountPercentage: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDiscountPercentage" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDiscountAmount" runat="server" Text="DiscountAmount: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDiscountAmount" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFabricsNo" runat="server" Text="FabricsNo: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFabricsNo" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblExtraField1" runat="server" Text="ExtraField1: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExtraField1" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblExtraField2" runat="server" Text="ExtraField2: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExtraField2" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblExtraField3" runat="server" Text="ExtraField3: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExtraField3" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblExtraField4" runat="server" Text="ExtraField4: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExtraField4" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblExtraField5" runat="server" Text="ExtraField5: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExtraField5" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblExtraField6" runat="server" Text="ExtraField6: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExtraField6" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblExtraField7" runat="server" Text="ExtraField7: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExtraField7" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblExtraField8" runat="server" Text="ExtraField8: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExtraField8" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblExtraField9" runat="server" Text="ExtraField9: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExtraField9" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblExtraField10" runat="server" Text="ExtraField10: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExtraField10" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAddedBy" runat="server" Text="AddedBy: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAddedBy" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUpdatedBy" runat="server" Text="UpdatedBy: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUpdatedBy" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUpdatedDate" runat="server" Text="UpdatedDate: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUpdatedDate" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblRowStatusID" runat="server" Text="RowStatusID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlRowStatus" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" 
                        OnClick="btnUpdate_Click" />
                </td>
                <td>
                    <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
