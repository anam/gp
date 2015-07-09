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

public partial class AdminPos_CustomerDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initialLoad();
            //showPos_CustomerGrid();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminPos_CustomerInsertUpdate.aspx?pos_CustomerID=0");
    }
    protected void lbSelect_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id;
        id = Convert.ToInt32(linkButton.CommandArgument);
        Response.Redirect("AdminPos_CustomerInsertUpdate.aspx?pos_CustomerID=" + id);
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = Pos_CustomerManager.DeletePos_Customer(Convert.ToInt32(linkButton.CommandArgument));
        showPos_CustomerGrid();
    }

    private void showPos_CustomerGrid()
    {
        gvPos_Customer.DataSource = Pos_CustomerManager.GetAllPos_Customers();
        gvPos_Customer.DataBind();
        checkRole();
    }

    protected void btnSearch_Click(object sender,EventArgs e)
    {
        gvPos_Customer.DataSource = Pos_CustomerManager.GetAllPos_customersBySearchArg(txtCardNo.Text,txtMobileNo.Text);
        gvPos_Customer.DataBind();

        checkRole();
    }

    private void checkRole()
    {
        bool isAllowedEdit = false;
        bool isAllowedDelete = false;
        isAllowedEdit=ButtonManager.GetAllButtonsByPageURLnUserIDnButtonName("lbSelect", HttpContext.Current.Request.Url.AbsoluteUri, getLogin().LoginID.ToString());
        isAllowedDelete = ButtonManager.GetAllButtonsByPageURLnUserIDnButtonName("lbDelete", HttpContext.Current.Request.Url.AbsoluteUri, getLogin().LoginID.ToString());

        foreach (GridViewRow gvr in gvPos_Customer.Rows)
        {
            LinkButton lbSelect = (LinkButton)gvr.FindControl("lbSelect");
            LinkButton lbDelete = (LinkButton)gvr.FindControl("lbDelete");

            lbDelete.Visible = isAllowedDelete;
            lbSelect.Visible = isAllowedEdit;
        }
    }
    protected void btnLoadAll_Click(object sender, EventArgs e)
    {
        showPos_CustomerGrid();
    }

    private Login getLogin()
    {
        Login login = new Login();

        try
        {
            if (Session["Login"] != null)
            {
                login = (Login)Session["Login"];
            }
            else if (hfLoginID.Value != "")
            {
                login = LoginManager.GetLoginByID(int.Parse(hfLoginID.Value));
            }
            else
            { Session["PreviousPage"] = HttpContext.Current.Request.Url.AbsoluteUri; Response.Redirect("../LoginPage.aspx"); }

        }
        catch (Exception ex)
        { }

        return login;
    }

    private void initialLoad()
    {
        if (hfLoginID.Value == "")
        {
            hfLoginID.Value = getLogin().LoginID.ToString();
        }

        btnAdd.Visible = ButtonManager.GetAllButtonsByPageURLnUserIDnButtonName("btnAdd", HttpContext.Current.Request.Url.AbsoluteUri, getLogin().LoginID.ToString());
        btnLoadAll.Visible = ButtonManager.GetAllButtonsByPageURLnUserIDnButtonName("btnLoadAll", HttpContext.Current.Request.Url.AbsoluteUri, getLogin().LoginID.ToString());
        
    }
}
