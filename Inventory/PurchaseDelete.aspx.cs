using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inventory_IssueDetailDelete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Request.QueryString["PurchaseID"] != null)
            {
                if (Request.QueryString["ItemID"] != null)
                {
                    string loginID = getLogin().LoginID.ToString();
                    Inv_ItemManager.DeleteInv_Item(int.Parse(Request.QueryString["ItemID"]));
                    Response.Redirect("PurchasePrint.aspx?PurchaseID=" + Request.QueryString["PurchaseID"]);
                }
            }
        }
    }

    private Login getLogin()
    {
        Login login = new Login();
        try
        {
            if (Session["Login"] == null) { Session["PreviousPage"] = HttpContext.Current.Request.Url.AbsoluteUri; Response.Redirect("../LoginPage.aspx"); }

            login = (Login)Session["Login"];
        }
        catch (Exception ex)
        { }

        return login;
    }
}