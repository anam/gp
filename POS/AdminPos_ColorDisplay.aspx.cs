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

public partial class AdminPos_ColorDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showPos_ColorGrid();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminPos_ColorInsertUpdate.aspx?pos_ColorID=0");
    }
    protected void lbSelect_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id;
        id = Convert.ToInt32(linkButton.CommandArgument);
        Response.Redirect("AdminPos_ColorInsertUpdate.aspx?pos_ColorID=" + id);
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = Pos_ColorManager.DeletePos_Color(Convert.ToInt32(linkButton.CommandArgument));
        showPos_ColorGrid();
    }

    private void showPos_ColorGrid()
    {
        gvPos_Color.DataSource = Pos_ColorManager.GetAllPos_Colors();
        gvPos_Color.DataBind();
    }
}
