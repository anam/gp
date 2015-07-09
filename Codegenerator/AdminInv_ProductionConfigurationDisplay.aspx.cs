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

public partial class AdminInv_ProductionConfigurationDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInv_ProductionConfigurationGrid();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminInv_ProductionConfigurationInsertUpdate.aspx?inv_ProductionConfigurationID=0");
    }
    protected void lbSelect_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id;
        id = Convert.ToInt32(linkButton.CommandArgument);
        Response.Redirect("AdminInv_ProductionConfigurationInsertUpdate.aspx?inv_ProductionConfigurationID=" + id);
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = Inv_ProductionConfigurationManager.DeleteInv_ProductionConfiguration(Convert.ToInt32(linkButton.CommandArgument));
        showInv_ProductionConfigurationGrid();
    }

    private void showInv_ProductionConfigurationGrid()
    {
        gvInv_ProductionConfiguration.DataSource = Inv_ProductionConfigurationManager.GetAllInv_ProductionConfigurations();
        gvInv_ProductionConfiguration.DataBind();
    }
}
