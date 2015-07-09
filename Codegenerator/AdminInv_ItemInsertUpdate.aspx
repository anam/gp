<%@ Page Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true" 
CodeFile="AdminInv_ItemInsertUpdate.aspx.cs" Inherits="AdminInv_ItemInsertUpdate" Title="Inv_Item Insert/Update By Admin" %>

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
                    <asp:Label ID="lblItemName" runat="server" Text="ItemName: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtItemName" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPurchaseID" runat="server" Text="PurchaseID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPurchase" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblItemCode" runat="server" Text="ItemCode: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtItemCode" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblRawMaterialID" runat="server" Text="RawMaterialID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlRawMaterial" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblStoreID" runat="server" Text="StoreID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStore" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblQualityUnitID" runat="server" Text="QualityUnitID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlQualityUnit" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblQualityValue" runat="server" Text="QualityValue: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtQualityValue" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblQuantityUnitID" runat="server" Text="QuantityUnitID: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlQuantityUnit" runat="server">
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
            <tr>
                <td>
                    <asp:Label ID="lblPurchasedQuantity" runat="server" Text="PurchasedQuantity: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPurchasedQuantity" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblIssueReturedQuantity" runat="server" Text="IssueReturedQuantity: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtIssueReturedQuantity" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUtilizedQuantity" runat="server" Text="UtilizedQuantity: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUtilizedQuantity" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLostQuantity" runat="server" Text="LostQuantity: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLostQuantity" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblExtraFieldQuantity1" runat="server" Text="ExtraFieldQuantity1: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExtraFieldQuantity1" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblExtraFieldQuantity2" runat="server" Text="ExtraFieldQuantity2: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExtraFieldQuantity2" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblExtraFieldQuantity3" runat="server" Text="ExtraFieldQuantity3: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExtraFieldQuantity3" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblExtraFieldQuantity4" runat="server" Text="ExtraFieldQuantity4: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExtraFieldQuantity4" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblExtraFieldQuantity5" runat="server" Text="ExtraFieldQuantity5: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExtraFieldQuantity5" runat="server" Text="">
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
