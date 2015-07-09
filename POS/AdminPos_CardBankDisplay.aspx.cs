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

public partial class AdminPos_CardBankDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showPos_CardBankGrid();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminPos_CardBankInsertUpdate.aspx?pos_CardBankID=0");
    }
    protected void lbSelect_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id;
        id = Convert.ToInt32(linkButton.CommandArgument);
        Response.Redirect("AdminPos_CardBankInsertUpdate.aspx?pos_CardBankID=" + id);
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = Pos_CardBankManager.DeletePos_CardBank(Convert.ToInt32(linkButton.CommandArgument));
        showPos_CardBankGrid();
    }

    private void showPos_CardBankGrid()
    {
        gvPos_CardBank.DataSource = Pos_CardBankManager.GetAllPos_CardBanks();
        gvPos_CardBank.DataBind();
    }
}
