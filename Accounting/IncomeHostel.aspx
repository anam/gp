<%@ Page Title="" Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true" CodeFile="IncomeHostel.aspx.cs" Inherits="Accounting_DayendSummaryEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        --%>
        
<asp:HiddenField ID="hfLoginID" runat="server" Value=""/>
<asp:DropDownList ID="ddlAllACC_ChartOfAccountLabel3" Visible="false" runat="server">
</asp:DropDownList>
<asp:DropDownList ID="ddlAllACC_ChartOfAccountLabel4" Visible="false" runat="server">
</asp:DropDownList>

<h1>Income Entry</h1>
   From: <asp:DropDownList ID="ddlBalance" runat="server" ></asp:DropDownList>
        <%--<asp:DropDownList ID="ddlBalance" runat="server" AutoPostBack="true"
        onselectedindexchanged="ddlBalance_SelectedIndexChanged"></asp:DropDownList>--%>
      <br />
      
    <asp:Label ID="lblGL" runat="server" Text=""></asp:Label>
<%--</ContentTemplate>
    </asp:UpdatePanel>
    <div>--%>
    Date  :<asp:TextBox ID="txtJournalDate" runat="server" Width="87px"></asp:TextBox>
    <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender1" runat="server" TargetControlID="txtJournalDate">
                        </ajaxToolkit:CalendarExtender>
 <asp:GridView ID="gvACC_ChartOfAccountLabel3" runat="server" AutoGenerateColumns="false" CssClass="gridCss">
            <Columns>
                <asp:TemplateField HeaderText="Head">
                    <ItemTemplate>
                        
                        <asp:HiddenField ID="hfChartOfAccountLabel3ID" Value='<%#Eval("ACC_ChartOfAccountLabel3ID") %>' runat="server" />
                        <asp:Label ID="lblChartOfAccountLabel3Text" runat="server" Text='<%#Eval("ChartOfAccountLabel3Text") %>'>
                        </asp:Label>
                        <div style="display:none;">
                        <br />
                        <asp:DropDownList ID="ddlACC_ChartOfAccountLabel4" runat="server" Width="200px">
                        </asp:DropDownList>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount">
                    <ItemTemplate>
                        <asp:TextBox ID="txtAmount" runat="server" Width="75px" Text="0" style="text-align:right;"></asp:TextBox>
                        <div style="display:none;">
                        <br />
                        Date  :<asp:TextBox ID="txtJournalDate" runat="server" Width="87px"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender1" runat="server" TargetControlID="txtJournalDate">
                        </ajaxToolkit:CalendarExtender>
                         <br />   <asp:Label ID="lblJournalMasterID" runat="server" Text=""></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Details" Visible="false">
                    <ItemTemplate>
                        
                        Address:<asp:TextBox ID="txtAddress" runat="server" Width="480px"></asp:TextBox>
                        <br />
                        Note   :<asp:TextBox ID="txtNote" runat="server" Width="496px"></asp:TextBox>
                        <br />
                        Pay to :<asp:TextBox ID="txtPayto" runat="server" Width="186px"></asp:TextBox>
                       
                        Check Date  :<asp:TextBox ID="txtCheckDate" runat="server" Width="80px"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender2" runat="server" TargetControlID="txtCheckDate">
                        </ajaxToolkit:CalendarExtender>
                        Check #:<asp:TextBox ID="txtCheckNo" runat="server" Width="87px"></asp:TextBox>
                         <br />
                         Bank:<asp:TextBox ID="txtBank" runat="server" Width="496px"></asp:TextBox>
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
                        <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%#Eval("ACC_ChartOfAccountLabel3ID") %>' OnClick="lbDelete_Click">
                            Delete
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>

         <asp:Button ID="btnJournalSubmit" runat="server" Text="Submit" 
        onclick="btnJournalSubmit_Click" />
     </div>  
</asp:Content>

