<%@ Page Title="" Language="C#" MasterPageFile="~/Login/AdminMaster.master" AutoEventWireup="true" CodeFile="HeadTransfer.aspx.cs" Inherits="ControlPanel_HeadTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
<h1>L-3 Transfer</h1>
    <table>
        <tr>
            <td>
                <asp:CheckBoxList ID="chkL3" runat="server">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="ddlL3" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnL3Transfer" runat="server" Text="L3 Transfer" 
                    onclick="btnL3Transfer_Click" />
            </td>
        </tr>
    </table>

<h1>L-4 Tranfer</h1>
    <table>
    
        <tr>
            <td>
                <asp:CheckBoxList ID="chkL4" runat="server">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="ddlL4" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnL4Transfer" runat="server" Text="L4 Transfer" 
                    onclick="btnL4Transfer_Click" />
            </td>
        </tr>
    </table>
    
</div>
</asp:Content>

