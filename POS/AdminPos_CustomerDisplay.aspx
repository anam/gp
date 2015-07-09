<%@ Page Language="C#" MasterPageFile="~/login/AdminMaster.master" AutoEventWireup="true"
    CodeFile="AdminPos_CustomerDisplay.aspx.cs" Inherits="AdminPos_CustomerDisplay"
    Title="Display Pos_Customer By Admin" %>

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
        <asp:HiddenField ID="hfLoginID" runat="server" />
    <table>
        <tr>
        <td>Card No:</td>
        <td><asp:TextBox ID="txtCardNo" runat="server" Text=""></asp:TextBox></td>
        <td>Mobile No:</td>
        <td><asp:TextBox ID="txtMobileNo" runat="server" Text=""></asp:TextBox></td>
        <td><asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" /></td>
        <td><asp:Button ID="btnLoadAll" runat="server" Text="Load All Customer" 
            onclick="btnLoadAll_Click"  /></td>
        <td><asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" /></td>
        </tr>
    </table>
      
        
        <asp:GridView ID="gvPos_Customer" runat="server" AutoGenerateColumns="false" CssClass="gridCss">
            <Columns>
                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbSelect" runat="server" CommandArgument='<%#Eval("Pos_CustomerID") %>'
                            OnClick="lbSelect_Click">
                            Select
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Branch No" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraFiled1" runat="server" Text='<%#Eval("ExtraFiled1") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Customer's Name">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerName" runat="server" Text='<%#Eval("CustomerName") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Address">
                    <ItemTemplate>
                        <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("Address") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Company Name">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraFiled2" runat="server" Text='<%#Eval("ExtraFiled2") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mobile No">
                    <ItemTemplate>
                        <asp:Label ID="lblMobile" runat="server" Text='<%#Eval("Mobile") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Phone No">
                    <ItemTemplate>
                        <asp:Label ID="lblPhone" runat="server" Text='<%#Eval("Phone") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Card No">
                    <ItemTemplate>
                        <asp:Label ID="lblCardNo" runat="server" Text='<%#Eval("CardNo") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Card Type">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraFiled3" runat="server" Text='<%#Eval("CardType") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Discount">
                    <ItemTemplate>
                        <asp:Label ID="lblDiscountPersent" runat="server" Text='<%#Eval("DiscountPersent") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reference" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblReferenceID" runat="server" Text='<%#Eval("ReferenceID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Signature">
                    <ItemTemplate>
                        <%--<asp:Label ID="lblSignature" runat="server" Text='<%#Eval("Signature") %>'>
                        </asp:Label>--%>
                        <asp:Image ID="lblSignature" runat="server" ImageUrl='<%#Eval("Signature") %>' Width="100"
                            Height="50"></asp:Image>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="Phone">
                    <ItemTemplate>
                        <asp:Label ID="lblPhone" runat="server" Text='<%#Eval("Phone") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <%--<asp:TemplateField HeaderText="Note">
                    <ItemTemplate>
                        <asp:Label ID="lblNote" runat="server" Text='<%#Eval("Note") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <%--<asp:TemplateField HeaderText="ExtraFiled4">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraFiled4" runat="server" Text='<%#Eval("ExtraFiled4") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ExtraFiled5">
                    <ItemTemplate>
                        <asp:Label ID="lblExtraFiled5" runat="server" Text='<%#Eval("ExtraFiled5") %>'>
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
                <asp:TemplateField HeaderText="RowSatatusID">
                    <ItemTemplate>
                        <asp:Label ID="lblRowSatatusID" runat="server" Text='<%#Eval("RowSatatusID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%#Eval("Pos_CustomerID") %>'
                            OnClick="lbDelete_Click" OnClientClick="return confirm('Are You Sure, You Want to Delete this Content.')">
                            Delete
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                There are no data available................
            </EmptyDataTemplate>
        </asp:GridView>
   </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
