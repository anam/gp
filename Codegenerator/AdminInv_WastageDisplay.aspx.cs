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

public partial class AdminInv_WastageDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInv_WastageGrid();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminInv_WastageInsertUpdate.aspx?inv_WastageID=0");
    }
    protected void lbSelect_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id;
        id = Convert.ToInt32(linkButton.CommandArgument);
        Response.Redirect("AdminInv_WastageInsertUpdate.aspx?inv_WastageID=" + id);
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = Inv_WastageManager.DeleteInv_Wastage(Convert.ToInt32(linkButton.CommandArgument));
        showInv_WastageGrid();
    }

    private void showInv_WastageGrid()
    {
        gvInv_Wastage.DataSource = Inv_WastageManager.GetAllInv_Wastages();
        gvInv_Wastage.DataBind();
    }
}
