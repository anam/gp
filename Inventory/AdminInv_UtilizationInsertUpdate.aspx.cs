using System;
using System.Collections;
using System.Collections.Generic;
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

public partial class AdminInv_UtilizationInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadIssue();
            loadWorkSatation();
            loadRowStatus();
            if (Request.QueryString["inv_UtilizationID"] != null)
            {
                int inv_UtilizationID = Int32.Parse(Request.QueryString["inv_UtilizationID"]);
                if (inv_UtilizationID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showInv_UtilizationData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Inv_Utilization inv_Utilization = new Inv_Utilization();

        inv_Utilization.UtilizationDate = txtUtilizationDate.Text;
        inv_Utilization.IssueIDs = Int32.Parse(ddlIssue.SelectedValue);
        inv_Utilization.WorkSatationID = Int32.Parse(ddlWorkSatation.SelectedValue);
        inv_Utilization.ExtraField1 = txtExtraField1.Text;
        inv_Utilization.ExtraField2 = txtExtraField2.Text;
        inv_Utilization.ExtraField3 = txtExtraField3.Text;
        inv_Utilization.ExtraField4 = txtExtraField4.Text;
        inv_Utilization.ExtraField5 = txtExtraField5.Text;
        inv_Utilization.AddedBy = Int32.Parse(txtAddedBy.Text);
        inv_Utilization.AddedDate = DateTime.Now;
        inv_Utilization.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        inv_Utilization.UpdatedDate = txtUpdatedDate.Text;
        inv_Utilization.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = Inv_UtilizationManager.InsertInv_Utilization(inv_Utilization);
        Response.Redirect("AdminInv_UtilizationDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Inv_Utilization inv_Utilization = new Inv_Utilization();
        inv_Utilization = Inv_UtilizationManager.GetInv_UtilizationByID(Int32.Parse(Request.QueryString["inv_UtilizationID"]));
        Inv_Utilization tempInv_Utilization = new Inv_Utilization();
        tempInv_Utilization.Inv_UtilizationID = inv_Utilization.Inv_UtilizationID;

        tempInv_Utilization.UtilizationDate = txtUtilizationDate.Text;
        tempInv_Utilization.IssueIDs = Int32.Parse(ddlIssue.SelectedValue);
        tempInv_Utilization.WorkSatationID = Int32.Parse(ddlWorkSatation.SelectedValue);
        tempInv_Utilization.ExtraField1 = txtExtraField1.Text;
        tempInv_Utilization.ExtraField2 = txtExtraField2.Text;
        tempInv_Utilization.ExtraField3 = txtExtraField3.Text;
        tempInv_Utilization.ExtraField4 = txtExtraField4.Text;
        tempInv_Utilization.ExtraField5 = txtExtraField5.Text;
        tempInv_Utilization.AddedBy = Int32.Parse(txtAddedBy.Text);
        tempInv_Utilization.AddedDate = DateTime.Now;
        tempInv_Utilization.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        tempInv_Utilization.UpdatedDate = txtUpdatedDate.Text;
        tempInv_Utilization.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = Inv_UtilizationManager.UpdateInv_Utilization(tempInv_Utilization);
        Response.Redirect("AdminInv_UtilizationDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtUtilizationDate.Text = "";
        ddlIssue.SelectedIndex = 0;
        ddlWorkSatation.SelectedIndex = 0;
        txtExtraField1.Text = "";
        txtExtraField2.Text = "";
        txtExtraField3.Text = "";
        txtExtraField4.Text = "";
        txtExtraField5.Text = "";
        txtAddedBy.Text = "";
        txtUpdatedBy.Text = "";
        txtUpdatedDate.Text = "";
        ddlRowStatus.SelectedIndex = 0;
    }
    private void showInv_UtilizationData()
    {
        Inv_Utilization inv_Utilization = new Inv_Utilization();
        inv_Utilization = Inv_UtilizationManager.GetInv_UtilizationByID(Int32.Parse(Request.QueryString["inv_UtilizationID"]));

        txtUtilizationDate.Text = inv_Utilization.UtilizationDate;
        ddlIssue.SelectedValue = inv_Utilization.IssueIDs.ToString();
        ddlWorkSatation.SelectedValue = inv_Utilization.WorkSatationID.ToString();
        txtExtraField1.Text = inv_Utilization.ExtraField1;
        txtExtraField2.Text = inv_Utilization.ExtraField2;
        txtExtraField3.Text = inv_Utilization.ExtraField3;
        txtExtraField4.Text = inv_Utilization.ExtraField4;
        txtExtraField5.Text = inv_Utilization.ExtraField5;
        txtAddedBy.Text = inv_Utilization.AddedBy.ToString();
        txtUpdatedBy.Text = inv_Utilization.UpdatedBy.ToString();
        txtUpdatedDate.Text = inv_Utilization.UpdatedDate;
        ddlRowStatus.SelectedValue = inv_Utilization.RowStatusID.ToString();
    }
    private void loadIssue()
    {
        ListItem li = new ListItem("Select Issue...", "0");
        ddlIssue.Items.Add(li);

        List<Issue> issues = new List<Issue>();
        issues = IssueManager.GetAllIssues();
        foreach (Issue issue in issues)
        {
            ListItem item = new ListItem(issue.IssueName.ToString(), issue.IssueIDs.ToString());
            ddlIssue.Items.Add(item);
        }
    }
    private void loadWorkSatation()
    {
        ListItem li = new ListItem("Select WorkSatation...", "0");
        ddlWorkSatation.Items.Add(li);

        List<WorkSatation> workSatations = new List<WorkSatation>();
        workSatations = WorkSatationManager.GetAllWorkSatations();
        foreach (WorkSatation workSatation in workSatations)
        {
            ListItem item = new ListItem(workSatation.WorkSatationName.ToString(), workSatation.WorkSatationID.ToString());
            ddlWorkSatation.Items.Add(item);
        }
    }
    private void loadRowStatus()
    {
        ListItem li = new ListItem("Select RowStatus...", "0");
        ddlRowStatus.Items.Add(li);

        List<RowStatus> rowStatuss = new List<RowStatus>();
        rowStatuss = RowStatusManager.GetAllRowStatuss();
        foreach (RowStatus rowStatus in rowStatuss)
        {
            ListItem item = new ListItem(rowStatus.RowStatusName.ToString(), rowStatus.RowStatusID.ToString());
            ddlRowStatus.Items.Add(item);
        }
    }
}
