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

public partial class AdminPos_TransactionTypeDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showPos_TransactionTypeGrid();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminPos_TransactionTypeInsertUpdate.aspx?pos_TransactionTypeID=0");
    }
    protected void lbSelect_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id;
        id = Convert.ToInt32(linkButton.CommandArgument);
        Response.Redirect("AdminPos_TransactionTypeInsertUpdate.aspx?pos_TransactionTypeID=" + id);
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = Pos_TransactionTypeManager.DeletePos_TransactionType(Convert.ToInt32(linkButton.CommandArgument));
        showPos_TransactionTypeGrid();
    }

    private void showPos_TransactionTypeGrid()
    {
        gvPos_TransactionType.DataSource = Pos_TransactionTypeManager.GetAllPos_TransactionTypes();
        gvPos_TransactionType.DataBind();
    }
}
