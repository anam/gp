<%@ Page Title="" Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true" CodeFile="SQL.aspx.cs" Inherits="Login_SQL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:TextBox ID="txtSQL" runat="server" TextMode="MultiLine" Height="300px" Width="100%"></asp:TextBox>
    <br />
    <table width="100%">
        <tr>
            <td align="left"> 
                <asp:Button ID="btnMSSQL" runat="server" Text="MS SQL" 
                 onclick="btnMSQL_Click" />
            </td>
            <td align="center">
                 <asp:Button ID="btnMSSQLSave" runat="server" Text="MS SQL & Save" 
                 onclick="btnMSQLSave_Click" />
            </td>
            <td align="right">
             <asp:Button ID="btnMyQL" runat="server" Text="My SQL" 
                 onclick="btnMyQL_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" style="overflow:scroll;">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" style="overflow:scroll;">
                <asp:GridView ID="gvResult" runat="server">
                </asp:GridView>
            </td>
        </tr>
    </table>
    

</asp:Content>

