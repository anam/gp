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
            loadACC_ChartOfAccountLabel4();
            showInv_IssueDetailGrid();
        }
    }
    private void loadACC_ChartOfAccountLabel4()
    {

        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        aCC_ChartOfAccountLabel4s = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4sForJournalEntry();

        ddlEmployee.Items.Add(new ListItem("Select Employee", "0"));
        ddlWorkSatation.Items.Add(new ListItem("Select WorkStation", "0"));
        ddlProductAll.Items.Add(new ListItem("Select Product", "0"));

        foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
        {
            ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_HeadTypeID.ToString() + "@" + aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 4)
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlEmployee.Items.Add(item);
            }

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 1)
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlWorkSatation.Items.Add(item);
            }

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 3)
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlProductAll.Items.Add(item);
            }
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
        Inv_IssueMaster issueMaster = Inv_IssueMasterManager.GetInv_IssueMasterByID(int.Parse(Request.QueryString["IssueMasterID"]));
        txtIssueDate.Text = issueMaster.IssueDate.ToString("dd MMM yyyy");
        ddlEmployee.SelectedValue = issueMaster.EmployeeID.ToString();
        ddlWorkSatation.SelectedValue = issueMaster.WorkSatationID.ToString();


        List<Inv_IssueDetail> inv_IssueDetailsFromDB = Inv_IssueDetailManager.GetAllInv_IssueDetailsByIssueMasterID(Request.QueryString["IssueMasterID"]);

        gvInv_IssueDetail.DataSource = inv_IssueDetailsFromDB;
        gvInv_IssueDetail.DataBind();
    }
}
