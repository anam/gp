<%@ Control Language="C#" AutoEventWireup="true" CodeFile="header.ascx.cs" Inherits="control_header" %>
<div id="logo">
    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/banner.jpg" />
</div>
<div id="header">
    <asp:HyperLink runat="server" NavigateUrl="~/Default.aspx" class="logoLink" ID="linkLogo">
        <asp:Image runat="server" ImageUrl="~/images/Logo.png" AlternateText="Track Results"
            ID="imgLogo" />
    </asp:HyperLink>
    <div id="loginArea">
        <asp:LoginView ID="LoginView1" runat="server">
            <AnonymousTemplate>
                <div style="color: White; font-size: 12px; padding-left: 10px;">
                    <asp:HyperLink ID="hlnkSignIn" runat="server" Text="Sign In" NavigateUrl="~/login.aspx"
                        Font-Bold="true"></asp:HyperLink>&nbsp;|&nbsp;
                    <asp:HyperLink ID="hlnkSignUp" runat="server" Text="Sign Up" NavigateUrl="~/Register.aspx"
                        Font-Bold="true"></asp:HyperLink>
                </div>
            </AnonymousTemplate>
            <LoggedInTemplate>
                <div style="font-size: 12px; padding-left: 10px;">
                    <asp:LoginName ID="loginUserName" runat="server" FormatString="{0}|" Font-Bold="true" />
                    <asp:LoginStatus ID="loginUserStatus" runat="server" LogoutText="Log Out" LogoutPageUrl="~/login.aspx"
                        LogoutAction="Redirect" />
                </div>
            </LoggedInTemplate>
        </asp:LoginView>
    </div>
    <div class="navigationArea">
    </div>
    
</div>
