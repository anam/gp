<%@ Page Title="" Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true" CodeFile="UploadStock.aspx.cs" Inherits="POS_UploadStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table>
    <tr>
        <td>Date</td>
        <td>
            <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
            <ajaxToolkit:CalendarExtender Format="dd MMM yyyy" ID="CalendarExtender4" runat="server" TargetControlID="txtDate">
            </ajaxToolkit:CalendarExtender>
        </td>
        <td>Show Room</td>
        <td>
        <asp:DropDownList ID="ddlShowRoom" runat="server">
                    </asp:DropDownList>
        </td>
        <td>
            <asp:FileUpload ID="fuldStock" runat="server" />
        </td>
        <td>
            <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
            <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
        </td>
    </tr>
</table>
    <asp:TextBox ID="txtCursor" runat="server" TextMode="MultiLine"></asp:TextBox>
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>

</asp:Content>

