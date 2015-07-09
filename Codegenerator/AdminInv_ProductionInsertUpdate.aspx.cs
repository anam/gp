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

public partial class AdminInv_ProductionInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadIssue();
            loadWorkSatation();
            loadRowStatus();
            if (Request.QueryString["inv_ProductionID"] != null)
            {
                int inv_ProductionID = Int32.Parse(Request.QueryString["inv_ProductionID"]);
                if (inv_ProductionID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showInv_ProductionData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Inv_Production inv_Production = new Inv_Production();

        inv_Production.ProductionDate = txtProductionDate.Text;
        inv_Production.IssueIDs = Int32.Parse(ddlIssue.SelectedValue);
        inv_Production.WorkSatationID = Int32.Parse(ddlWorkSatation.SelectedValue);
        inv_Production.ExtraField1 = txtExtraField1.Text;
        inv_Production.ExtraField2 = txtExtraField2.Text;
        inv_Production.ExtraField3 = txtExtraField3.Text;
        inv_Production.ExtraField4 = txtExtraField4.Text;
        inv_Production.ExtraField5 = txtExtraField5.Text;
        inv_Production.AddedBy = Int32.Parse(txtAddedBy.Text);
        inv_Production.AddedDate = DateTime.Now;
        inv_Production.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        inv_Production.UpdatedDate = txtUpdatedDate.Text;
        inv_Production.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = Inv_ProductionManager.InsertInv_Production(inv_Production);
        Response.Redirect("AdminInv_ProductionDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Inv_Production inv_Production = new Inv_Production();
        inv_Production = Inv_ProductionManager.GetInv_ProductionByID(Int32.Parse(Request.QueryString["inv_ProductionID"]));
        Inv_Production tempInv_Production = new Inv_Production();
        tempInv_Production.Inv_ProductionID = inv_Production.Inv_ProductionID;

        tempInv_Production.ProductionDate = txtProductionDate.Text;
        tempInv_Production.IssueIDs = Int32.Parse(ddlIssue.SelectedValue);
        tempInv_Production.WorkSatationID = Int32.Parse(ddlWorkSatation.SelectedValue);
        tempInv_Production.ExtraField1 = txtExtraField1.Text;
        tempInv_Production.ExtraField2 = txtExtraField2.Text;
        tempInv_Production.ExtraField3 = txtExtraField3.Text;
        tempInv_Production.ExtraField4 = txtExtraField4.Text;
        tempInv_Production.ExtraField5 = txtExtraField5.Text;
        tempInv_Production.AddedBy = Int32.Parse(txtAddedBy.Text);
        tempInv_Production.AddedDate = DateTime.Now;
        tempInv_Production.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        tempInv_Production.UpdatedDate = txtUpdatedDate.Text;
        tempInv_Production.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = Inv_ProductionManager.UpdateInv_Production(tempInv_Production);
        Response.Redirect("AdminInv_ProductionDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtProductionDate.Text = "";
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
    private void showInv_ProductionData()
    {
        Inv_Production inv_Production = new Inv_Production();
        inv_Production = Inv_ProductionManager.GetInv_ProductionByID(Int32.Parse(Request.QueryString["inv_ProductionID"]));

        txtProductionDate.Text = inv_Production.ProductionDate;
        ddlIssue.SelectedValue = inv_Production.IssueIDs.ToString();
        ddlWorkSatation.SelectedValue = inv_Production.WorkSatationID.ToString();
        txtExtraField1.Text = inv_Production.ExtraField1;
        txtExtraField2.Text = inv_Production.ExtraField2;
        txtExtraField3.Text = inv_Production.ExtraField3;
        txtExtraField4.Text = inv_Production.ExtraField4;
        txtExtraField5.Text = inv_Production.ExtraField5;
        txtAddedBy.Text = inv_Production.AddedBy.ToString();
        txtUpdatedBy.Text = inv_Production.UpdatedBy.ToString();
        txtUpdatedDate.Text = inv_Production.UpdatedDate;
        ddlRowStatus.SelectedValue = inv_Production.RowStatusID.ToString();
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
