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
using System.Collections.Generic;

public partial class AdminInv_IssueDetailDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInv_IssueDetailGrid();
        }
    }
    

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminInv_IssueDetailInsertUpdate.aspx?inv_IssueDetailID=0");
    }
    protected void lbSelect_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id;
        id = Convert.ToInt32(linkButton.CommandArgument);
        Response.Redirect("AdminInv_IssueDetailInsertUpdate.aspx?inv_IssueDetailID=" + id);
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = Inv_IssueDetailManager.DeleteInv_IssueDetail(Convert.ToInt32(linkButton.CommandArgument));
        showInv_IssueDetailGrid();
    }

    private void showInv_IssueDetailGrid()
    {
        Inv_Utilization issueMaster = Inv_UtilizationManager.GetInv_UtilizationByID(int.Parse(Request.QueryString["UtilizationID"]));
        lblLastIssueID.Text = Request.QueryString["UtilizationID"];
        txtIssueDate.Text = issueMaster.UtilizationDate.ToString("dd MMM yyyy");
        
    }
    protected void btnSaveIssueMaster_Click(object sender, EventArgs e)
    {
        string SQL = @"Update Inv_Utilization set UpdatedDate=GETDATE(), UtilizationDate='" + DateTime.Parse(txtIssueDate.Text).ToString("yyyy-MM-dd") + @"'"
            + @" where Inv_UtilizationID=" + Request.QueryString["UtilizationID"]
            +@"
update ACC_JournalMaster set UpdatedDate=GETDATE(),JournalDate='" + DateTime.Parse(txtIssueDate.Text).ToString("yyyy-MM-dd") + @"' where Note='Utilization ID = " + Request.QueryString["UtilizationID"] + @"';
";
        CommonManager.SQLExec(SQL);

        a_IssueMasterPreview.HRef = "UtilizationPrint.aspx?UtilizationID=" + Request.QueryString["UtilizationID"];
        a_IssueMasterPreview.Visible = true;
    }
}
