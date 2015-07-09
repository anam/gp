<%@ Page Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true"
    CodeFile="Production.aspx.cs" Inherits="AdminInv_ProductInsertUpdate" Title="Inv_Product Insert/Update By Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .tableCss
        {
            /*text-align: left;*/
        }
        .gridTxt
        {
            width: 60px;
            text-align: right;
        }
        .alignRight
        {
            text-align: right;
        }
        .alignRight input
        {
            width: 50px;
            text-align: right;
            border: 1px solid blue;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="hfLoginID" runat="server" />
        <h1>
           Issue Return / Production</h1>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblProductID" runat="server" Text="For Product: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlProduct" runat="server">
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
                    <asp:Label ID="Label2" runat="server" Text="Code: ">
                    </asp:Label>
                </td>
                <td>
                    For more than 1 please put (,)coma<br />
                    <asp:TextBox ID="txtCodes" runat="server" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr style="display:none;">
                <td>
                    <asp:Label ID="lblEmployeeID" runat="server" Text="Employee: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlEmployee" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblProductCode" runat="server" Text="Final Product: ">
                    </asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlFinalProduct" runat="server">
                    </asp:DropDownList>
                     <%--AutoPostBack="True" 
                        onselectedindexchanged="ddlFinalProduct_SelectedIndexChanged"--%>
                    <asp:CheckBox ID="chkWithAccsoriesUtilization" Text="Load the associated accsories" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
    

        <h1>
            Issued Items</h1>
        <asp:GridView ID="gvInv_IssueDetail" runat="server" AutoGenerateColumns="false" Width="100%"
            ShowFooter="true" style="overflow:none;">
            <Columns>
                <asp:TemplateField HeaderText="S.I.">
                    <ItemTemplate>
                        <asp:Label ID="gap" runat="server" Text='<%#Eval("ParentChildGap") %>'>
                        </asp:Label>
                        <asp:CheckBox ID="chkSelect" runat="server"  Text='<%# Container.DataItemIndex +1 %>'/>    
                        <asp:HiddenField ID="hfRawMaterialID" runat="server" Value='<%#Eval("RawMaterialID") %>'/>
                        <asp:HiddenField ID="hfItemID" runat="server" Value='<%#Eval("ItemID") %>'/>
                        <asp:HiddenField ID="hfACC_HeadTypeID" runat="server"  Value='<%#Eval("ACC_HeadTypeID") %>'/>
                        <asp:HiddenField ID="hfInv_IssueDetailID" runat="server"  Value='<%#Eval("Inv_IssueDetailID") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Main?">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkMain" runat="server" Text="Main?"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Added With">
                    <ItemTemplate>
                        <asp:Label ID="lblAdditionalWithIssueDetailID" runat="server" Text='<%#Eval("AdditionalWithIssueDetailID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Code" ControlStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%#Eval("ItemCode") %>'>
                        </asp:Label>
                    <br />
                        <asp:Label ID="lblItemName" runat="server" Text='<%#Eval("ItemName") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Initial<br/>issued<br/>Quantity" ItemStyle-CssClass="alignRight"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="In Stock" ItemStyle-CssClass="alignRight" FooterStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraField1" runat="server" Text='<%#Eval("ExtraField1") %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblStockQuantity" runat="server" Text=""></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Issue<br/>Return" ItemStyle-CssClass="alignRight">
                    <ItemTemplate>
                        <asp:TextBox ID="txtIssueReturn" runat="server" Text="0"></asp:TextBox>
                    </ItemTemplate>
                    <FooterTemplate>
                        Issue<br/>Return
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Utilization" ItemStyle-CssClass="alignRight">
                    <ItemTemplate>
                        <asp:TextBox ID="txtUtilization" runat="server" Text='<%#Eval("ExtraField1") %>'></asp:TextBox>
                    </ItemTemplate>
                    <FooterTemplate>
                        Utilization
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Wasted" ItemStyle-CssClass="alignRight">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="TextBox1" runat="server"  Text='<%#Eval("ExtraField1") %>'></asp:TextBox>--%>
                        <asp:TextBox ID="txtWasted" runat="server"  Text="0"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Production<br/>Quantity<br/>Fresh" ItemStyle-CssClass="alignRight">
                    <ItemTemplate>
                        <asp:TextBox ID="txtProductionQuantityFresh" runat="server" Text="0"></asp:TextBox>
                    </ItemTemplate>
                    <FooterTemplate>
                        Fresh
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Production<br/>Quantity<br/>Reject" ItemStyle-CssClass="alignRight">
                    <ItemTemplate>
                        <asp:TextBox ID="txtProductionQuantityReject" runat="server" Text="0"></asp:TextBox>
                    </ItemTemplate>
                    <FooterTemplate>
                        Reject
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Production<br/>Quantity<br/>Total" ItemStyle-CssClass="alignRight">
                    <ItemTemplate>
                        <asp:TextBox ID="txtProductionQuantity" runat="server" Text='<%#Eval("ApproximateQuantity") %>'></asp:TextBox>
                    </ItemTemplate>
                    <FooterTemplate>
                        Total
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="App.<br/> Qty" ItemStyle-CssClass="alignRight"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblApproximateQuantity" runat="server" Text='<%#Eval("ApproximateQuantity") %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label Style="text-align: right;" ID="lblTotalAppxQty" runat="server" Text=""></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Unite" ItemStyle-CssClass="alignRight">
                    <ItemTemplate>
                        <asp:Label ID="lblQuantityUnitName" runat="server" Text='<%#Eval("QuantityUnitName") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit<br/>Price" ItemStyle-CssClass="alignRight">
                    <ItemTemplate>
                        <asp:Label ID="lblPricePerUnit" runat="server" Text='<%#Eval("PricePerUnit") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount" ItemStyle-CssClass="alignRight">
                    <ItemTemplate>
                        <asp:Label ID="lblTotalPrice" runat="server" Text='<%#Eval("TotalPrice","{0:0,0.00}") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="Quality">
                    <ItemTemplate>
                        <asp:Label ID="lblQualityUnitValue" runat="server" Text='<%#Eval("QualityUnitValue") %>'>
                        </asp:Label>&nbsp;-&nbsp;
                        <asp:Label ID="lblQualityUnitName" runat="server" Text='<%#Eval("QualityUnitName") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Issue For">
                    <ItemTemplate>
                        <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("ProductName") %>'>
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
        <table class='tblSize'>
            <tr>
                <td width="80px">
                    <asp:Label ID="lblIssueDate" runat="server" Text="Date: ">
                    </asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtDate" runat="server" Text="">
                    </asp:TextBox>
                    <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender1" runat="server"
                        TargetControlID="txtDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Particulars: ">
                    </asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtParticulars" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td align="left">
                    <asp:Button ID="btnIssueReturn" runat="server" Text="Issue Return" 
                        onclick="btnIssueReturn_Click"/>
                    <asp:HyperLink ID="hlnkIssueReturnPrint" CssClass="buttonCss" Visible="false" runat="server" Target="_blank">Print Issue Report</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td colspan="2">Old Utilization ID:
                <asp:TextBox ID="txtUtilizationID" runat="server" Text="0">
                </asp:TextBox>
                    <asp:HyperLink ID="hlnkUtilizationPrint" CssClass="buttonCss" Visible="false" runat="server" Target="_blank">Print Utilization Report</asp:HyperLink>
                    <asp:HyperLink ID="hlnkWastagePrint" CssClass="buttonCss" Visible="false" runat="server" Target="_blank">Print Wastage Report</asp:HyperLink>
            </td>
            </tr>
            
            <tr>
                <td valign="top">
                    <asp:Label ID="Label4" runat="server" Text="Normal: ">
                    </asp:Label>
                </td>
                <td >
                    <asp:DataList ID="dlSize"  runat="server" RepeatColumns="10" 
                        RepeatDirection="Horizontal" BackColor="White" BorderColor="#999999" 
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <ItemTemplate>
                            <%#Eval("SizeName")%><asp:TextBox ID="txtQty" Width="40px" runat="server" ValidationGroup='<%#Eval("Pos_SizeID")%>'></asp:TextBox>

                        </ItemTemplate>
                        <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    </asp:DataList>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Label ID="Label3" runat="server" Text="Rejected: ">
                    </asp:Label>
                </td>
                <td >
                    <asp:DataList ID="dlSizeRejected"  runat="server" RepeatColumns="10" 
                        RepeatDirection="Horizontal" BackColor="White" BorderColor="#999999" 
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <ItemTemplate>
                           <%#Eval("SizeName")%> <asp:TextBox ID="txtQty" Width="40px" runat="server" ValidationGroup='<%#Eval("Pos_SizeID")%>'></asp:TextBox>

                        </ItemTemplate>
                        <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    </asp:DataList>
                </td>
            </tr>

            <tr>
                <td>
                </td>
                <td align="left">
                    <asp:Button ID="btnCostCalculation" runat="server" Text="Cost calculation" OnClick="btnCostCalculation_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAvgCosting" runat="server" Text="Fabrics Cost: ">
                    </asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtFabricsCost" runat="server" Text="" Width="75px"> 
                    </asp:TextBox>
                    Accessories Cost
                    <asp:TextBox ID="txtAccessoriesCost" runat="server" Text="" Width="75px">
                    </asp:TextBox>
                    Labour Cost
                    <asp:TextBox ID="txtLabourCost" runat="server" Text="" Width="75px">
                    </asp:TextBox>
                    Overhead Cost
                    <asp:TextBox ID="txtOverheadCost" runat="server" Text="" Width="75px">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSalePrice" runat="server" Text="Total: ">
                    </asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="lblTotal" runat="server" Text="">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPos_ColorID" runat="server" Text="Color: ">
                    </asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlPos_Color" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left">
                    <asp:Button ID="btnWastage" runat="server" Text="Submit for Wastage " OnClick="btnWastage_Click" />
                    <asp:Button ID="btnAdd" runat="server" Text="Submit for Utilization" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" OnClick="btnUpdate_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" Visible="false" />
                </td>
            </tr>
        </table>
  

  </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
