using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class DefaultMasterPage : System.Web.UI.MasterPage
{
    public FormRights fright;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.User.Identity.IsAuthenticated)
        {
            
            Response.Redirect("~/login.aspx");
        }

        MembershipUser currentUser;
        currentUser = Membership.GetUser();
        if (currentUser == null)
            Response.Redirect("~/login.aspx");

        
        var clientID = currentUser.ProviderUserKey.ToString();
        string pageName = "AdminDisplayClient";
        fright = FormRightsManager.GetFormRightsByUserIDFormID(pageName, clientID);



        string name = HttpContext.Current.User.Identity.Name;
        if (!string.IsNullOrEmpty(name))
        {
            if (name.Equals("admin"))
            {
                //HyperLink1.NavigateUrl = "Admin.aspx";

            }
            else
            {
                //HyperLink1.NavigateUrl = "SalesAgent.aspx";


            }
        }
        
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminResidentDisplay.aspx?SearchKey="+txtSearchKey.Text);
    }
}
