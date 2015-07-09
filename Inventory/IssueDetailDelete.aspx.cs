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

            if (Request.QueryString["IssueID"] != null)
            {
                if (Request.QueryString["IssueDetailID"] != null)
                {
                    string loginID = getLogin().LoginID.ToString();

                    string sql = @"update Inv_IssueDetail set RowStatusID=3, UpdatedBy=" + loginID + @"
                        ,UpdatedDate=GETDATE() where Inv_IssueDetailID=" + Request.QueryString["IssueDetailID"] + @"
                        update Inv_ItemTransaction set RowStatusID=3 , UpdatedBy=" + loginID + @"
                        ,UpdatedDate=GETDATE()
                        where ExtraField2='" + Request.QueryString["IssueDetailID"] + @"'
                        update Inv_Item 
                        set UpdatedBy=" + loginID + @"
                        ,UpdatedDate=GETDATE()
                        ,ExtraFieldQuantity1+= (select Quantity from Inv_IssueDetail where Inv_IssueDetailID=" + Request.QueryString["IssueDetailID"] + @")
                        ,ExtraFieldQuantity5-=(select Quantity from Inv_IssueDetail where Inv_IssueDetailID=" + Request.QueryString["IssueDetailID"] + @")
                        where Inv_ItemID =(select ItemID from Inv_IssueDetail where Inv_IssueDetailID=" + Request.QueryString["IssueDetailID"] + @")";

                    CommonManager.SQLExec(sql);

                    Response.Redirect("IssueForAdminPrint.aspx?IssueID=" + Request.QueryString["IssueID"]);
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