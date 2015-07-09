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

public partial class AdminInv_WastageInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadIssue();
            loadWorkSatation();
            loadRowStatus();
            if (Request.QueryString["inv_WastageID"] != null)
            {
                int inv_WastageID = Int32.Parse(Request.QueryString["inv_WastageID"]);
                if (inv_WastageID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showInv_WastageData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Inv_Wastage inv_Wastage = new Inv_Wastage();

        inv_Wastage.WastageDate = txtWastageDate.Text;
        inv_Wastage.IssueIDs = Int32.Parse(ddlIssue.SelectedValue);
        inv_Wastage.WorkSatationID = Int32.Parse(ddlWorkSatation.SelectedValue);
        inv_Wastage.ExtraField1 = txtExtraField1.Text;
        inv_Wastage.ExtraField2 = txtExtraField2.Text;
        inv_Wastage.ExtraField3 = txtExtraField3.Text;
        inv_Wastage.ExtraField4 = txtExtraField4.Text;
        inv_Wastage.ExtraField5 = txtExtraField5.Text;
        inv_Wastage.AddedBy = Int32.Parse(txtAddedBy.Text);
        inv_Wastage.AddedDate = DateTime.Now;
        inv_Wastage.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        inv_Wastage.UpdatedDate = txtUpdatedDate.Text;
        inv_Wastage.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = Inv_WastageManager.InsertInv_Wastage(inv_Wastage);
        Response.Redirect("AdminInv_WastageDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Inv_Wastage inv_Wastage = new Inv_Wastage();
        inv_Wastage = Inv_WastageManager.GetInv_WastageByID(Int32.Parse(Request.QueryString["inv_WastageID"]));
        Inv_Wastage tempInv_Wastage = new Inv_Wastage();
        tempInv_Wastage.Inv_WastageID = inv_Wastage.Inv_WastageID;

        tempInv_Wastage.WastageDate = txtWastageDate.Text;
        tempInv_Wastage.IssueIDs = Int32.Parse(ddlIssue.SelectedValue);
        tempInv_Wastage.WorkSatationID = Int32.Parse(ddlWorkSatation.SelectedValue);
        tempInv_Wastage.ExtraField1 = txtExtraField1.Text;
        tempInv_Wastage.ExtraField2 = txtExtraField2.Text;
        tempInv_Wastage.ExtraField3 = txtExtraField3.Text;
        tempInv_Wastage.ExtraField4 = txtExtraField4.Text;
        tempInv_Wastage.ExtraField5 = txtExtraField5.Text;
        tempInv_Wastage.AddedBy = Int32.Parse(txtAddedBy.Text);
        tempInv_Wastage.AddedDate = DateTime.Now;
        tempInv_Wastage.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        tempInv_Wastage.UpdatedDate = txtUpdatedDate.Text;
        tempInv_Wastage.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = Inv_WastageManager.UpdateInv_Wastage(tempInv_Wastage);
        Response.Redirect("AdminInv_WastageDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtWastageDate.Text = "";
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
    private void showInv_WastageData()
    {
        Inv_Wastage inv_Wastage = new Inv_Wastage();
        inv_Wastage = Inv_WastageManager.GetInv_WastageByID(Int32.Parse(Request.QueryString["inv_WastageID"]));

        txtWastageDate.Text = inv_Wastage.WastageDate;
        ddlIssue.SelectedValue = inv_Wastage.IssueIDs.ToString();
        ddlWorkSatation.SelectedValue = inv_Wastage.WorkSatationID.ToString();
        txtExtraField1.Text = inv_Wastage.ExtraField1;
        txtExtraField2.Text = inv_Wastage.ExtraField2;
        txtExtraField3.Text = inv_Wastage.ExtraField3;
        txtExtraField4.Text = inv_Wastage.ExtraField4;
        txtExtraField5.Text = inv_Wastage.ExtraField5;
        txtAddedBy.Text = inv_Wastage.AddedBy.ToString();
        txtUpdatedBy.Text = inv_Wastage.UpdatedBy.ToString();
        txtUpdatedDate.Text = inv_Wastage.UpdatedDate;
        ddlRowStatus.SelectedValue = inv_Wastage.RowStatusID.ToString();
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
