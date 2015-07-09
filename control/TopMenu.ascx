<%@ Control Language='C#' AutoEventWireup='true' CodeFile='TopMenu.ascx.cs' Inherits='Control_TopMenu' %>
<asp:Label ID="lblMenu" runat="server" Text=""></asp:Label>
<%--<ul class='menu' id='menu'>
    <li><a href='#' class='menulink'>Dropdown One</a>
        <ul>
            <li><a href='#'>Navigation Item 1 </a></li>
            <li><a href='#' class='sub'>Navigation Item 1.1</a>
                <ul>
                    <li class='topline'><a href='#'>Navigation Item 1.1.1</a></li>
                    <li><a href='#' class='sub'>Navigation Item 1.1.2</a>
                        <ul>
                            <li class='topline'><a href='#'>Navigation Item 1.1.2.1</a></li>
                            <li><a href='#'>Navigation Item 1.1.2.2</a> </li>
                            <li><a href='#'>Navigation Item 1.1.2.3</a></li>
                        </ul>
                    </li>
                    <li><a href='#'>Navigation Item 1.1.3</a></li>
                    <li><a href='#'>Navigation Item 1.1.4</a></li>
                    <li><a href='#'>Navigation Item 1.1.5</a></li>
                </ul>
            </li>
            <li><a href='#'>Navigation Item 4</a></li>
            <li><a href='#'>Navigation Item 5</a></li>
        </ul>
    </li>
</ul>--%>

 <script type='text/javascript'>
     var menu = new menu.dd('menu');
     menu.init('menu', 'menuhover');
</script>