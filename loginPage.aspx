<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginPage.aspx.cs" Inherits="LoginPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login</title>
    <link href="App_Themes/Default/styleWithAsrafviCodeGeneratedPage.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="loginWrapper">
		<div style="margin:auto;width:333px;">
            <h1>ERP Developers</h1>
            <h2>Gentle Park</h2>
            
            <div class="clear"></div>
        </div>
        <div class="loginbg">
        
            <ul>
                <li>
                    <img src="App_Themes/Default/images/userIcon.png" alt="" />
                        <asp:TextBox ID="txtLoginName" runat="server"></asp:TextBox>
                </li>
                <li>
                    <img src="App_Themes/Default/images/passwordIcon.png" alt="" />
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                </li>
            </ul>
            <span class="fpassword">
                <span style="width:30px;padding-left:90px;"><asp:CheckBox ID="chkRememberme" Checked="true" Visible="false" runat="server" Text="Remember me"/></span>
                    <%--<br /><br />
            	<a href="#">Forgot your password?</a> | <a href="#">Sign Up</a>--%>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            </span>
            <div class="login_btn">
                <asp:Button ID="btnLogin" runat="server" Text="Log In" 
                    onclick="btnLogin_OnClick"/>
            </div>
        </div><!-- END CLASS LOGINBG -->    
    
    </div><!-- END ID LOGINWRAPPER -->

    </form>
</body>
</html>
