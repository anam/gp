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

public partial class AdminACC_JournalDetailDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showACC_JournalDetailGrid();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminACC_JournalDetailInsertUpdate.aspx?aCC_JournalDetailID=0");
    }
    protected void lbSelect_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id;
        id = Convert.ToInt32(linkButton.CommandArgument);
        Response.Redirect("AdminACC_JournalDetailInsertUpdate.aspx?aCC_JournalDetailID=" + id);
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = ACC_JournalDetailManager.DeleteACC_JournalDetail(Convert.ToInt32(linkButton.CommandArgument));
        showACC_JournalDetailGrid();
    }

    private void showACC_JournalDetailGrid()
    {
        gvACC_JournalDetail.DataSource = ACC_JournalDetailManager.GetAllACC_JournalDetails();
        gvACC_JournalDetail.DataBind();
    }
}
