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

public partial class AdminACC_ChartOfAccountLabel3Display : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showACC_ChartOfAccountLabel3Grid();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminACC_ChartOfAccountLabel3InsertUpdate.aspx?aCC_ChartOfAccountLabel3ID=0");
    }
    protected void lbSelect_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id;
        id = Convert.ToInt32(linkButton.CommandArgument);
        Response.Redirect("AdminACC_ChartOfAccountLabel3InsertUpdate.aspx?aCC_ChartOfAccountLabel3ID=" + id);
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = ACC_ChartOfAccountLabel3Manager.DeleteACC_ChartOfAccountLabel3(Convert.ToInt32(linkButton.CommandArgument));
        showACC_ChartOfAccountLabel3Grid();
    }

    private void showACC_ChartOfAccountLabel3Grid()
    {
        gvACC_ChartOfAccountLabel3.DataSource = ACC_ChartOfAccountLabel3Manager.GetAllACC_ChartOfAccountLabel3s();
        gvACC_ChartOfAccountLabel3.DataBind();
    }
}
