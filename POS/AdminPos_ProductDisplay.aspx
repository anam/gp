<%@ Page Language="C#" MasterPageFile="~/login/AdminMaster.master" AutoEventWireup="true"
    CodeFile="AdminPos_ProductDisplay.aspx.cs" Inherits="AdminPos_ProductDisplay" Title="Display Pos_Product By Admin" %>

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
    <div>
        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
        <asp:GridView ID="gvPos_Product" runat="server" AutoGenerateColumns="false" CssClass="gridCss">
            <Columns>
                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbSelect" runat="server" CommandArgument='<%#Eval("Pos_ProductID") %>' OnClick="lbSelect_Click">
                            Select
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ProductID">
                    <ItemTemplate>
                        <asp:Label ID="lblProductID" runat="server" Text='<%#Eval("ProductID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ReferenceID">
                    <ItemTemplate>
                        <asp:Label ID="lblReferenceID" runat="server" Text='<%#Eval("ReferenceID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pos_ProductTypeID">
                    <ItemTemplate>
                        <asp:Label ID="lblPos_ProductTypeID" runat="server" Text='<%#Eval("Pos_ProductTypeID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Inv_UtilizationDetailsIDs">
                    <ItemTemplate>
                        <asp:Label ID="lblInv_UtilizationDetailsIDs" runat="server" Text='<%#Eval("Inv_UtilizationDetailsIDs") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ProductStatusID">
                    <ItemTemplate>
                        <asp:Label ID="lblProductStatusID" runat="server" Text='<%#Eval("ProductStatusID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ProductName">
                    <ItemTemplate>
                        <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("ProductName") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DesignCode">
                    <ItemTemplate>
                        <asp:Label ID="lblDesignCode" runat="server" Text='<%#Eval("DesignCode") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pos_SizeID">
                    <ItemTemplate>
                        <asp:Label ID="lblPos_SizeID" runat="server" Text='<%#Eval("Pos_SizeID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pos_BrandID">
                    <ItemTemplate>
                        <asp:Label ID="lblPos_BrandID" runat="server" Text='<%#Eval("Pos_BrandID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Inv_QuantityUnitID">
                    <ItemTemplate>
                        <asp:Label ID="lblInv_QuantityUnitID" runat="server" Text='<%#Eval("Inv_QuantityUnitID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FabricsCost">
                    <ItemTemplate>
                        <asp:Label ID="lblFabricsCost" runat="server" Text='<%#Eval("FabricsCost") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="AccesoriesCost">
                    <ItemTemplate>
                        <asp:Label ID="lblAccesoriesCost" runat="server" Text='<%#Eval("AccesoriesCost") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Overhead">
                    <ItemTemplate>
                        <asp:Label ID="lblOverhead" runat="server" Text='<%#Eval("Overhead") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="OthersCost">
                    <ItemTemplate>
                        <asp:Label ID="lblOthersCost" runat="server" Text='<%#Eval("OthersCost") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PurchasePrice">
                    <ItemTemplate>
                        <asp:Label ID="lblPurchasePrice" runat="server" Text='<%#Eval("PurchasePrice") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SalePrice">
                    <ItemTemplate>
                        <asp:Label ID="lblSalePrice" runat="server" Text='<%#Eval("SalePrice") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="OldSalePrice">
                    <ItemTemplate>
                        <asp:Label ID="lblOldSalePrice" runat="server" Text='<%#Eval("OldSalePrice") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Note">
                    <ItemTemplate>
                        <asp:Label ID="lblNote" runat="server" Text='<%#Eval("Note") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="BarCode">
                    <ItemTemplate>
                        <asp:Label ID="lblBarCode" runat="server" Text='<%#Eval("BarCode") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pos_ColorID">
                    <ItemTemplate>
                        <asp:Label ID="lblPos_ColorID" runat="server" Text='<%#Eval("Pos_ColorID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pos_FabricsTypeID">
                    <ItemTemplate>
                        <asp:Label ID="lblPos_FabricsTypeID" runat="server" Text='<%#Eval("Pos_FabricsTypeID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="StyleCode">
                    <ItemTemplate>
                        <asp:Label ID="lblStyleCode" runat="server" Text='<%#Eval("StyleCode") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pic1">
                    <ItemTemplate>
                        <asp:Label ID="lblPic1" runat="server" Text='<%#Eval("Pic1") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pic2">
                    <ItemTemplate>
                        <asp:Label ID="lblPic2" runat="server" Text='<%#Eval("Pic2") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pic3">
                    <ItemTemplate>
                        <asp:Label ID="lblPic3" runat="server" Text='<%#Eval("Pic3") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VatPercentage">
                    <ItemTemplate>
                        <asp:Label ID="lblVatPercentage" runat="server" Text='<%#Eval("VatPercentage") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IsVatExclusive">
                    <ItemTemplate>
                        <asp:Label ID="lblIsVatExclusive" runat="server" Text='<%#Eval("IsVatExclusive") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DiscountPercentage">
                    <ItemTemplate>
                        <asp:Label ID="lblDiscountPercentage" runat="server" Text='<%#Eval("DiscountPercentage") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DiscountAmount">
                    <ItemTemplate>
                        <asp:Label ID="lblDiscountAmount" runat="server" Text='<%#Eval("DiscountAmount") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FabricsNo">
                    <ItemTemplate>
                        <asp:Label ID="lblFabricsNo" runat="server" Text='<%#Eval("FabricsNo") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ExtraField1">
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
                <asp:TemplateField HeaderText="ExtraField6">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraField6" runat="server" Text='<%#Eval("ExtraField6") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ExtraField7">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraField7" runat="server" Text='<%#Eval("ExtraField7") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ExtraField8">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraField8" runat="server" Text='<%#Eval("ExtraField8") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ExtraField9">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraField9" runat="server" Text='<%#Eval("ExtraField9") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ExtraField10">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraField10" runat="server" Text='<%#Eval("ExtraField10") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="AddedBy">
                    <ItemTemplate>
                        <asp:Label ID="lblAddedBy" runat="server" Text='<%#Eval("AddedBy") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="AddedDate">
                    <ItemTemplate>
                        <asp:Label ID="lblAddedDate" runat="server" Text='<%#Eval("AddedDate") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UpdatedBy">
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
                        <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%#Eval("Pos_ProductID") %>' OnClick="lbDelete_Click">
                            Delete
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
