<%@ Page Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true"
    CodeFile="ItemHistory.aspx.cs" Inherits="AdminInv_ItemTransactionDisplay" Title="Display Inv_ItemTransaction By Admin" %>

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
        <h1>
            Item Summary</h1>
        <asp:GridView ID="gvInv_Item" runat="server" AutoGenerateColumns="false" CssClass="gridCss">
            <Columns>
                <%--<asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbSelect" runat="server" CommandArgument='<%#Eval("Inv_ItemID") %>' OnClick="lbSelect_Click">
                            Select
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:Label ID="lblItemName" runat="server" Text='<%#Eval("ItemName") %>'>
                        </asp:Label>
                        <%--(<%#Eval("RawMaterialTypeName") %>)--%>
                        <br />
                        <asp:Label ID="lblItemCode" runat="server" Text='<%#Eval("ItemCode") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Prch ID">
                    <ItemTemplate>
                        <a style="text-decoration:underline;color:blue;" href='PurchasePrint.aspx?PurchaseID=<%#Eval("PurchaseID") %>' target="_blank"><asp:Label ID="lblPurchaseID" runat="server" Text='<%#Eval("PurchaseID") %>'>
                        </asp:Label></a>
                        <asp:HiddenField ID="hfPurchaseID" runat="server" Value='<%#Eval("PurchaseID") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="RawMaterialID">
                    <ItemTemplate>
                        <asp:Label ID="lblRawMaterialID" runat="server" Text='<%#Eval("RawMaterialID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="StoreID">
                    <ItemTemplate>
                        <asp:Label ID="lblStoreID" runat="server" Text='<%#Eval("StoreID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Quality">
                    <ItemTemplate>
                        <asp:Label ID="lblQualityValue" runat="server" Text='<%#Eval("QualityValue") %>'>
                        </asp:Label>
                        &nbsp;
                        <asp:Label ID="lblQualityUnitID" runat="server" Text='<%#Eval("QualityUnitName") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit">
                    <ItemTemplate>
                        <asp:Label ID="lblQuantityUnitID" runat="server" Text='<%#Eval("QuantityUnitName") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Prch Qty">
                    <ItemTemplate>
                        <asp:Label ID="lblPurchasedQuantity" runat="server" Text='<%#Eval("PurchasedQuantity") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Prch Adj">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraFieldQuantity2" runat="server" Text='<%#Eval("ExtraFieldQuantity2") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Prch Rtn">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraFieldQuantity3" runat="server" Text='<%#Eval("ExtraFieldQuantity3") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Issued Qty">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraFieldQuantity5" runat="server" Text='<%#Eval("ExtraFieldQuantity5") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Issue Rtn">
                    <ItemTemplate>
                        <asp:Label ID="lblIssueReturedQuantity" runat="server" Text='<%#Eval("IssueReturedQuantity") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Wasted Qty">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraFieldQuantity4" runat="server" Text='<%#Eval("ExtraFieldQuantity4") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="Lost Qty">
                    <ItemTemplate>
                        <asp:Label ID="lblLostQuantity" runat="server" Text='<%#Eval("LostQuantity") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Utilized Qty">
                    <ItemTemplate>
                        <asp:Label ID="lblUtilizedQuantity" runat="server" Text='<%#Eval("UtilizedQuantity") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Stock">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraFieldQuantity1" runat="server" Text='<%#Eval("ExtraFieldQuantity1") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="ExtraField1">
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
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Price PerUnit">
                    <ItemTemplate>
                        <asp:Label ID="lblPricePerUnit" runat="server" Text='<%#Eval("PricePerUnit") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Added Date">
                    <ItemTemplate>
                        <asp:Label ID="lblAddedDate" runat="server" Text='<%#Eval("AddedDate") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="UpdatedBy">
                    <ItemTemplate>
                        <asp:Label ID="lblUpdatedBy" runat="server" Text='<%#Eval("UpdatedBy") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Updated Date">
                    <ItemTemplate>
                        <asp:Label ID="lblUpdatedDate" runat="server" Text='<%#Eval("UpdatedDate") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Note" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblNote" runat="server" Text='<%#Eval("ExtraField7") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="RowStatusID">
                    <ItemTemplate>
                        <asp:Label ID="lblRowStatusID" runat="server" Text='<%#Eval("RowStatusID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%#Eval("Inv_ItemID") %>' OnClick="lbDelete_Click">
                            Delete
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
        <table>

             <tr>
                 <td>
                    Unite Price
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtUnitePrice" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Quantity
                </td>
                <td>
                    <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
                </td>
            
                <td>
                    Width/GSM
                </td>
                <td>
                    <asp:TextBox ID="txtQualityValue" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
            
                <td>
                    Quantity Unit
                </td>
                <td>
                    <asp:DropDownList ID="ddlQuantityUnit" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    Quality Unit
                </td>
                <td>
                    <asp:DropDownList ID="ddlQualityUnit" runat="server">
                    </asp:DropDownList>
                </td>
            
            </tr>
            <tr>
                <td>
                    Note:
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNote" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
           
            <tr>
                <td>
                    
                </td>
                <td colspan="3">
                    <asp:HiddenField ID="hfItemID" runat="server" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
                </td>
            </tr>
        </table>
    
        <h1>
            Transaction History</h1>
        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" Visible="false" />
        <asp:GridView ID="gvInv_ItemTransaction" runat="server" AutoGenerateColumns="false"
            CssClass="gridCss">
            <Columns>
                <asp:TemplateField HeaderText="Issue">
                    <ItemTemplate>
                        <asp:Label ID="lblItemTrasactionTypeID" runat="server" Text='<%#Eval("ExtraField5") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity","{0:0,0.00}") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="WorkStation">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraField4" runat="server" Text='<%#Eval("ExtraField4") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reference ID">
                    <ItemTemplate>
                        <asp:Label ID="lblReferenceID" runat="server" Text='<%#Eval("ReferenceID","{0:0,0}") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Time">
                    <ItemTemplate>
                        <asp:Label ID="lblAddedDate" runat="server" Text='<%#Eval("AddedDate","{0:dd MMM yyyy hh:mm tt}") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
   </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
