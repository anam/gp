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

public partial class AdminInv_IssueMasterReturnInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadEmployee();
            loadWorkSatation();
            loadRowStatus();
            if (Request.QueryString["inv_IssueMasterReturnID"] != null)
            {
                int inv_IssueMasterReturnID = Int32.Parse(Request.QueryString["inv_IssueMasterReturnID"]);
                if (inv_IssueMasterReturnID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showInv_IssueMasterReturnData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Inv_IssueMasterReturn inv_IssueMasterReturn = new Inv_IssueMasterReturn();

        inv_IssueMasterReturn.IssueReturnName = txtIssueReturnName.Text;
        inv_IssueMasterReturn.IssueReturnDate = txtIssueReturnDate.Text;
        inv_IssueMasterReturn.EmployeeID = Int32.Parse(ddlEmployee.SelectedValue);
        inv_IssueMasterReturn.WorkSatationID = Int32.Parse(ddlWorkSatation.SelectedValue);
        inv_IssueMasterReturn.Particulars = txtParticulars.Text;
        inv_IssueMasterReturn.IsIssue = cbIsIssue.Checked;
        inv_IssueMasterReturn.ExtraField1 = txtExtraField1.Text;
        inv_IssueMasterReturn.ExtraField2 = txtExtraField2.Text;
        inv_IssueMasterReturn.ExtraField3 = txtExtraField3.Text;
        inv_IssueMasterReturn.ExtraField4 = txtExtraField4.Text;
        inv_IssueMasterReturn.ExtraField5 = txtExtraField5.Text;
        inv_IssueMasterReturn.AddedBy = Int32.Parse(txtAddedBy.Text);
        inv_IssueMasterReturn.AddedDate = DateTime.Now;
        inv_IssueMasterReturn.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        inv_IssueMasterReturn.UpdatedDate = txtUpdatedDate.Text;
        inv_IssueMasterReturn.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = Inv_IssueMasterReturnManager.InsertInv_IssueMasterReturn(inv_IssueMasterReturn);
        Response.Redirect("AdminInv_IssueMasterReturnDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Inv_IssueMasterReturn inv_IssueMasterReturn = new Inv_IssueMasterReturn();
        inv_IssueMasterReturn = Inv_IssueMasterReturnManager.GetInv_IssueMasterReturnByID(Int32.Parse(Request.QueryString["inv_IssueMasterReturnID"]));
        Inv_IssueMasterReturn tempInv_IssueMasterReturn = new Inv_IssueMasterReturn();
        tempInv_IssueMasterReturn.Inv_IssueMasterReturnID = inv_IssueMasterReturn.Inv_IssueMasterReturnID;

        tempInv_IssueMasterReturn.IssueReturnName = txtIssueReturnName.Text;
        tempInv_IssueMasterReturn.IssueReturnDate = txtIssueReturnDate.Text;
        tempInv_IssueMasterReturn.EmployeeID = Int32.Parse(ddlEmployee.SelectedValue);
        tempInv_IssueMasterReturn.WorkSatationID = Int32.Parse(ddlWorkSatation.SelectedValue);
        tempInv_IssueMasterReturn.Particulars = txtParticulars.Text;
        tempInv_IssueMasterReturn.IsIssue = cbIsIssue.Checked;
        tempInv_IssueMasterReturn.ExtraField1 = txtExtraField1.Text;
        tempInv_IssueMasterReturn.ExtraField2 = txtExtraField2.Text;
        tempInv_IssueMasterReturn.ExtraField3 = txtExtraField3.Text;
        tempInv_IssueMasterReturn.ExtraField4 = txtExtraField4.Text;
        tempInv_IssueMasterReturn.ExtraField5 = txtExtraField5.Text;
        tempInv_IssueMasterReturn.AddedBy = Int32.Parse(txtAddedBy.Text);
        tempInv_IssueMasterReturn.AddedDate = DateTime.Now;
        tempInv_IssueMasterReturn.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        tempInv_IssueMasterReturn.UpdatedDate = txtUpdatedDate.Text;
        tempInv_IssueMasterReturn.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = Inv_IssueMasterReturnManager.UpdateInv_IssueMasterReturn(tempInv_IssueMasterReturn);
        Response.Redirect("AdminInv_IssueMasterReturnDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtIssueReturnName.Text = "";
        txtIssueReturnDate.Text = "";
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
    private void showInv_IssueMasterReturnData()
    {
        Inv_IssueMasterReturn inv_IssueMasterReturn = new Inv_IssueMasterReturn();
        inv_IssueMasterReturn = Inv_IssueMasterReturnManager.GetInv_IssueMasterReturnByID(Int32.Parse(Request.QueryString["inv_IssueMasterReturnID"]));

        txtIssueReturnName.Text = inv_IssueMasterReturn.IssueReturnName;
        txtIssueReturnDate.Text = inv_IssueMasterReturn.IssueReturnDate;
        ddlEmployee.SelectedValue = inv_IssueMasterReturn.EmployeeID.ToString();
        ddlWorkSatation.SelectedValue = inv_IssueMasterReturn.WorkSatationID.ToString();
        txtParticulars.Text = inv_IssueMasterReturn.Particulars;
        cbIsIssue.Checked = inv_IssueMasterReturn.IsFeature;
        txtExtraField1.Text = inv_IssueMasterReturn.ExtraField1;
        txtExtraField2.Text = inv_IssueMasterReturn.ExtraField2;
        txtExtraField3.Text = inv_IssueMasterReturn.ExtraField3;
        txtExtraField4.Text = inv_IssueMasterReturn.ExtraField4;
        txtExtraField5.Text = inv_IssueMasterReturn.ExtraField5;
        txtAddedBy.Text = inv_IssueMasterReturn.AddedBy.ToString();
        txtUpdatedBy.Text = inv_IssueMasterReturn.UpdatedBy.ToString();
        txtUpdatedDate.Text = inv_IssueMasterReturn.UpdatedDate;
        ddlRowStatus.SelectedValue = inv_IssueMasterReturn.RowStatusID.ToString();
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
