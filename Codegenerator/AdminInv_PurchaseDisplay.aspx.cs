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

public partial class AdminInv_PurchaseDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInv_PurchaseGrid();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminInv_PurchaseInsertUpdate.aspx?inv_PurchaseID=0");
    }
    protected void lbSelect_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id;
        id = Convert.ToInt32(linkButton.CommandArgument);
        Response.Redirect("AdminInv_PurchaseInsertUpdate.aspx?inv_PurchaseID=" + id);
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = Inv_PurchaseManager.DeleteInv_Purchase(Convert.ToInt32(linkButton.CommandArgument));
        showInv_PurchaseGrid();
    }

    private void showInv_PurchaseGrid()
    {
        gvInv_Purchase.DataSource = Inv_PurchaseManager.GetAllInv_Purchases();
        gvInv_Purchase.DataBind();
    }
}
