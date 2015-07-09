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

public partial class AdminInv_IssueMasterInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadEmployee();
            loadWorkSatation();
            loadRowStatus();
            if (Request.QueryString["inv_IssueMasterID"] != null)
            {
                int inv_IssueMasterID = Int32.Parse(Request.QueryString["inv_IssueMasterID"]);
                if (inv_IssueMasterID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showInv_IssueMasterData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Inv_IssueMaster inv_IssueMaster = new Inv_IssueMaster();

        inv_IssueMaster.IssueName = txtIssueName.Text;
        inv_IssueMaster.IssueDate = txtIssueDate.Text;
        inv_IssueMaster.EmployeeID = Int32.Parse(ddlEmployee.SelectedValue);
        inv_IssueMaster.WorkSatationID = Int32.Parse(ddlWorkSatation.SelectedValue);
        inv_IssueMaster.Particulars = txtParticulars.Text;
        inv_IssueMaster.IsIssue = cbIsIssue.Checked;
        inv_IssueMaster.ExtraField1 = txtExtraField1.Text;
        inv_IssueMaster.ExtraField2 = txtExtraField2.Text;
        inv_IssueMaster.ExtraField3 = txtExtraField3.Text;
        inv_IssueMaster.ExtraField4 = txtExtraField4.Text;
        inv_IssueMaster.ExtraField5 = txtExtraField5.Text;
        inv_IssueMaster.AddedBy =getLogin().LoginID;
        inv_IssueMaster.AddedDate = DateTime.Now;
        inv_IssueMaster.UpdatedBy = getLogin().LoginID;
        inv_IssueMaster.UpdatedDate = DateTime.Now;
        inv_IssueMaster.RowStatusID = 1;
        int resutl = Inv_IssueMasterManager.InsertInv_IssueMaster(inv_IssueMaster);
        Response.Redirect("AdminInv_IssueMasterDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Inv_IssueMaster inv_IssueMaster = new Inv_IssueMaster();
        inv_IssueMaster = Inv_IssueMasterManager.GetInv_IssueMasterByID(Int32.Parse(Request.QueryString["inv_IssueMasterID"]));
        Inv_IssueMaster tempInv_IssueMaster = new Inv_IssueMaster();
        tempInv_IssueMaster.Inv_IssueMasterID = inv_IssueMaster.Inv_IssueMasterID;

        tempInv_IssueMaster.IssueName = txtIssueName.Text;
        tempInv_IssueMaster.IssueDate = txtIssueDate.Text;
        tempInv_IssueMaster.EmployeeID = Int32.Parse(ddlEmployee.SelectedValue);
        tempInv_IssueMaster.WorkSatationID = Int32.Parse(ddlWorkSatation.SelectedValue);
        tempInv_IssueMaster.Particulars = txtParticulars.Text;
        tempInv_IssueMaster.IsIssue = cbIsIssue.Checked;
        tempInv_IssueMaster.ExtraField1 = txtExtraField1.Text;
        tempInv_IssueMaster.ExtraField2 = txtExtraField2.Text;
        tempInv_IssueMaster.ExtraField3 = txtExtraField3.Text;
        tempInv_IssueMaster.ExtraField4 = txtExtraField4.Text;
        tempInv_IssueMaster.ExtraField5 = txtExtraField5.Text;
        tempInv_IssueMaster.AddedBy =getLogin().LoginID;
        tempInv_IssueMaster.AddedDate = DateTime.Now;
        tempInv_IssueMaster.UpdatedBy = getLogin().LoginID;
        tempInv_IssueMaster.UpdatedDate = DateTime.Now;
        tempInv_IssueMaster.RowStatusID = 1;
        bool result = Inv_IssueMasterManager.UpdateInv_IssueMaster(tempInv_IssueMaster);
        Response.Redirect("AdminInv_IssueMasterDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtIssueName.Text = "";
        txtIssueDate.Text = "";
        ddlEmployee.SelectedIndex = 0;
        ddlWorkSatation.SelectedIndex = 0;
        txtParticulars.Text = "";
        cbIsIssue.Checked = false;
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
    private void showInv_IssueMasterData()
    {
        Inv_IssueMaster inv_IssueMaster = new Inv_IssueMaster();
        inv_IssueMaster = Inv_IssueMasterManager.GetInv_IssueMasterByID(Int32.Parse(Request.QueryString["inv_IssueMasterID"]));

        txtIssueName.Text = inv_IssueMaster.IssueName;
        txtIssueDate.Text = inv_IssueMaster.IssueDate;
        ddlEmployee.SelectedValue = inv_IssueMaster.EmployeeID.ToString();
        ddlWorkSatation.SelectedValue = inv_IssueMaster.WorkSatationID.ToString();
        txtParticulars.Text = inv_IssueMaster.Particulars;
        cbIsIssue.Checked = inv_IssueMaster.IsFeature;
        txtExtraField1.Text = inv_IssueMaster.ExtraField1;
        txtExtraField2.Text = inv_IssueMaster.ExtraField2;
        txtExtraField3.Text = inv_IssueMaster.ExtraField3;
        txtExtraField4.Text = inv_IssueMaster.ExtraField4;
        txtExtraField5.Text = inv_IssueMaster.ExtraField5;
        txtAddedBy.Text = inv_IssueMaster.AddedBy.ToString();
        txtUpdatedBy.Text = inv_IssueMaster.UpdatedBy.ToString();
        txtUpdatedDate.Text = inv_IssueMaster.UpdatedDate;
        ddlRowStatus.SelectedValue = inv_IssueMaster.RowStatusID.ToString();
    }
    private void loadEmployee()
    {
        ListItem li = new ListItem("Select Employee...", "0");
        ddlEmployee.Items.Add(li);

        List<Employee> employees = new List<Employee>();
        employees = EmployeeManager.GetAllEmployees();
        foreach (Employee employee in employees)
        {
            ListItem item = new ListItem(employee.EmployeeName.ToString(), employee.EmployeeID.ToString());
            ddlEmployee.Items.Add(item);
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
