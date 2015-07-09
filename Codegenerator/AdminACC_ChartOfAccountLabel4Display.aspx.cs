using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class AdminACC_ChartOfAccountLabel4Display : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showACC_ChartOfAccountLabel4Grid();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminACC_ChartOfAccountLabel4InsertUpdate.aspx?aCC_ChartOfAccountLabel4ID=0");
    }
    protected void lbSelect_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id;
        id = Convert.ToInt32(linkButton.CommandArgument);
        Response.Redirect("AdminACC_ChartOfAccountLabel4InsertUpdate.aspx?aCC_ChartOfAccountLabel4ID=" + id);
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = ACC_ChartOfAccountLabel4Manager.DeleteACC_ChartOfAccountLabel4(Convert.ToInt32(linkButton.CommandArgument));
        showACC_ChartOfAccountLabel4Grid();
    }

    private void showACC_ChartOfAccountLabel4Grid()
    {
        gvACC_ChartOfAccountLabel4.DataSource = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4s();
        gvACC_ChartOfAccountLabel4.DataBind();
    }
}
