<%@ Page Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true" 
CodeFile="JournalEntrySpecial.aspx.cs" Inherits="AdminACC_JournalDetailInsertUpdate" Title="ACC_JournalDetail Insert/Update By Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .tableCss
        {
        	text-align: left;
        }
        .gridCss
        {width:100%}
        #ctl00_ContentPlaceHolder1_rbtnRootAccounts td span
        {
            background: none;
            width:auto;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:HiddenField ID="hfLoginID" runat="server" Value=""/>
    <div class="tableCss">
    <h1>Journal Entry</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table>
            
            <tr runat="server" id="trRootAccounts">
                <td><asp:Label ID="Label4" runat="server" Text="Type: ">
                    </asp:Label></td>
                <td>
                <asp:RadioButtonList ID="rbtnRootAccounts" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="rbtnRootAccounts_SelectedIndexChanged" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem>Asset</asp:ListItem>
                        <asp:ListItem>Expense</asp:ListItem>
                        <asp:ListItem>Income</asp:ListItem>
                        <asp:ListItem>Liability & Equity</asp:ListItem>
                    </asp:RadioButtonList>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblACC_ChartOfAccountLabel4ID" runat="server" Text="Account: ">
                    </asp:Label>
                </td>
                <td>
                    
                    <asp:DropDownList ID="ddlACC_ChartOfAccountLabel3" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlAllACC_ChartOfAccountLabel3" Visible="false" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Type: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rbtnHeadType" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="rbtnHeadType_SelectedIndexChanged" RepeatColumns="7"
                        RepeatDirection="Horizontal">
                    </asp:RadioButtonList>
                    
                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Head: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlACC_ChartOfAccountLabel4" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlAllACC_ChartOfAccountLabel4" Visible="false" runat="server">
                    </asp:DropDownList>
                      
                </td>
            </tr>
            <tr id="trWorkStation" runat="server">
                <td>
                    <asp:Label ID="Label5" runat="server" Text="For: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlWorkSatation" runat="server">
                    </asp:DropDownList>
                   
                </td>
            </tr>
            <tr runat="server" id="trDrCr">
                <td>
                    <asp:Label ID="lblDebit" runat="server" Text="Dr/Cr: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rbtnDebitOrCredit" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Debit">Debit</asp:ListItem>
                    <asp:ListItem Value="Credit">Credit</asp:ListItem>
                    </asp:RadioButtonList>
                   
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Amount: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAmount" runat="server" Text="">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                <asp:Label ID="Label18" runat="server" Text="Check no<br/>(if by Cheque): ">
                    </asp:Label>
                </td>
                <td>
                
                    <asp:TextBox ID="txtChequeNo" runat="server"></asp:TextBox>
                    Date:
                    <asp:TextBox ID="txtChequeDate" runat="server" Text="" Width="80px">
                    </asp:TextBox>
                     <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender1" runat="server" TargetControlID="txtChequeDate">
                                    </ajaxToolkit:CalendarExtender>
                    Bank & Branch
                    <asp:TextBox ID="txtBankNBranchDetails" runat="server" Text="" Width="410px">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                </td>
            </tr>
            </table>
           
            <table>
            <tr>
                <td colspan="2">
                    <h1 style="width:931px;">Journal Preview</h1>
                </td>
            </tr>
            <tr>
                <%--<td>
                </td>--%>
                <td colspan="2">
                    <asp:GridView ID="gvACC_JournalDetail" runat="server" AutoGenerateColumns="false" CssClass="gridCss">
            <Columns>
                <asp:TemplateField HeaderText="Accoutns">
                    <ItemTemplate>
                        <asp:Label ID="lblACC_ChartOfAccountLabel3ID" runat="server" Text='<%#Eval("ACC_ChartOfAccountLabel3Text") %>'>
                        </asp:Label>
                        <asp:HiddenField ID="hfACC_JournalDetailID" runat="server" Value='<%#Eval("ACC_JournalDetailID") %>'/>
                        <asp:HiddenField ID="hfACC_ChartOfAccountLabel3ID" runat="server" Value='<%#Eval("ACC_ChartOfAccountLabel3ID") %>'/>
                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Head">
                    <ItemTemplate>
                        <asp:Label ID="lblACC_ChartOfAccountLabel4ID" runat="server" Text='<%#Eval("ACC_ChartOfAccountLabel4Text") %>'>
                        </asp:Label>
                        <asp:HiddenField ID="hfExtraField1" runat="server" Value='<%#Eval("ExtraField1") %>'/>
                        <asp:HiddenField ID="hfExtraField2" runat="server" Value='<%#Eval("ExtraField2") %>'/>
                        <asp:HiddenField ID="hfExtraField3" runat="server" Value='<%#Eval("ExtraField3") %>'/>
                        <asp:HiddenField ID="hfACC_ChartOfAccountLabel4ID" runat="server" Value='<%#Eval("ACC_ChartOfAccountLabel4ID") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="WorkStation">
                    <ItemTemplate>
                        <asp:Label ID="lblWorkStation" runat="server" Text='<%#Eval("WorkStationName") %>'>
                        </asp:Label>
                        <asp:HiddenField ID="hfWorkStation" runat="server" Value='<%#Eval("WorkStation") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Debit">
                    <ItemTemplate>
                        <asp:Label ID="lblDebit" runat="server" Text='<%#Eval("Debit") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Credit">
                    <ItemTemplate>
                        <asp:Label ID="lblCredit" runat="server" Text='<%#Eval("Credit") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%#Eval("ACC_JournalDetailID") %>' OnClick="lbDelete_Click">
                            Delete
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <asp:Label ID="lbltotal" runat="server" ></asp:Label>
                </td>
            </tr>
            </table>
            <table runat="server" id="tblJournalMasterDetails">
            <tr>
                <td>
                    <asp:Label ID="Label14" runat="server" Text="Date:"></asp:Label>
                </td>
                <td>
                    
                            <asp:TextBox ID="txtJournalMasterDate" runat="server" Text=""></asp:TextBox>
                            <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="ajCal" runat="server" TargetControlID="txtJournalMasterDate">
                                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            
            <tr  runat="server" id="trReceivedOrPayto">
                <td>
                    <asp:Label ID="lblReceivedOrPayto" runat="server" Text="Received From:"></asp:Label>
                </td>
                <td>
                    
                            <asp:TextBox ID="txtReceivedOrPayto" runat="server" Text=""></asp:TextBox>
                </td>
            </tr>
            <tr runat="server" id="trAddress">
                <td>
                    <asp:Label ID="Label16" runat="server" Text="Address:"></asp:Label>
                </td>
                <td>
                    
                            <asp:TextBox ID="txtAddress" runat="server" Text=""></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td>
                    <asp:Label ID="Label13" runat="server" Text="Explanations: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNote" runat="server" Text="" Width="794px">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    
                            <asp:Button ID="btnJournalSubmit" runat="server" Text="Submit" 
                                onclick="btnJournalSubmit_Click" />
                                <br />
                                <asp:Label ID="lblMsg" runat="server" ></asp:Label>
                                <br />
                    <asp:HyperLink ID="hlnkPrintVoucher" CssClass="buttonCss" Visible="false" runat="server" Target="_blank">Print Voucher</asp:HyperLink>
                
                </td>
            </tr>
        </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    
    
</asp:Content>
