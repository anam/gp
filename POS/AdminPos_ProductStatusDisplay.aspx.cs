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

public partial class AdminPos_ProductStatusDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showPos_ProductStatusGrid();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminPos_ProductStatusInsertUpdate.aspx?pos_ProductStatusID=0");
    }
    protected void lbSelect_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id;
        id = Convert.ToInt32(linkButton.CommandArgument);
        Response.Redirect("AdminPos_ProductStatusInsertUpdate.aspx?pos_ProductStatusID=" + id);
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = Pos_ProductStatusManager.DeletePos_ProductStatus(Convert.ToInt32(linkButton.CommandArgument));
        showPos_ProductStatusGrid();
    }

    private void showPos_ProductStatusGrid()
    {
        gvPos_ProductStatus.DataSource = Pos_ProductStatusManager.GetAllPos_ProductStatuss();
        gvPos_ProductStatus.DataBind();
    }
}
