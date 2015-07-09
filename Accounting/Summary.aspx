<%@ Page Title="" Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true" CodeFile="Summary.aspx.cs" Inherits="Accounting_Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
    td{padding:5px;text-align:right;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
<h1>Summary (<asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>)</h1>

    <asp:GridView ID="gvSupplier" runat="server" AutoGenerateColumns="false">
    <Columns>
        <asp:TemplateField HeaderText="Supplier">
            <ItemTemplate>
                <asp:Label ID="lblID" runat="server" Text='<%#Eval("Supplier") %>'>
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Balance">
            <ItemTemplate>
                <asp:Label ID="lblID" runat="server" Text='<%#Eval("Balance") %>'>
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    </asp:GridView>
</div>
</asp:Content>

