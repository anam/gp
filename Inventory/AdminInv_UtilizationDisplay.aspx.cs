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

public partial class AdminInv_UtilizationDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInv_UtilizationGrid();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminInv_UtilizationInsertUpdate.aspx?inv_UtilizationID=0");
    }
    protected void lbSelect_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id;
        id = Convert.ToInt32(linkButton.CommandArgument);
        Response.Redirect("AdminInv_UtilizationInsertUpdate.aspx?inv_UtilizationID=" + id);
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = Inv_UtilizationManager.DeleteInv_Utilization(Convert.ToInt32(linkButton.CommandArgument));
        showInv_UtilizationGrid();
    }

    private void showInv_UtilizationGrid()
    {
        gvInv_Utilization.DataSource = Inv_UtilizationManager.GetAllInv_Utilizations();
        gvInv_Utilization.DataBind();
    }
}
