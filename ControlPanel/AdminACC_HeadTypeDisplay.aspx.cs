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

public partial class AdminACC_HeadTypeDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showACC_HeadTypeGrid();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminACC_HeadTypeInsertUpdate.aspx?aCC_HeadTypeID=0");
    }
    protected void lbSelect_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id;
        id = Convert.ToInt32(linkButton.CommandArgument);
        Response.Redirect("AdminACC_HeadTypeInsertUpdate.aspx?aCC_HeadTypeID=" + id);
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = ACC_HeadTypeManager.DeleteACC_HeadType(Convert.ToInt32(linkButton.CommandArgument));
        showACC_HeadTypeGrid();
    }

    private void showACC_HeadTypeGrid()
    {
        gvACC_HeadType.DataSource = ACC_HeadTypeManager.GetAllACC_HeadTypes();
        gvACC_HeadType.DataBind();
    }
}
