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

public partial class AdminACC_JournalMasterDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showACC_JournalMasterGrid();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminACC_JournalMasterInsertUpdate.aspx?aCC_JournalMasterID=0");
    }
    protected void lbSelect_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id;
        id = Convert.ToInt32(linkButton.CommandArgument);
        Response.Redirect("AdminACC_JournalMasterInsertUpdate.aspx?aCC_JournalMasterID=" + id);
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = ACC_JournalMasterManager.DeleteACC_JournalMaster(Convert.ToInt32(linkButton.CommandArgument));
        showACC_JournalMasterGrid();
    }

    private void showACC_JournalMasterGrid()
    {
        gvACC_JournalMaster.DataSource = ACC_JournalMasterManager.GetAllACC_JournalMasters();
        gvACC_JournalMaster.DataBind();
    }
}
