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

public partial class AdminACC_JournalDetailInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadJournalMaster();
            loadACC_ChartOfAccountLabel4();
            loadACC_ChartOfAccountLabel3();
            loadRowStatus();
            if (Request.QueryString["aCC_JournalDetailID"] != null)
            {
                int aCC_JournalDetailID = Int32.Parse(Request.QueryString["aCC_JournalDetailID"]);
                if (aCC_JournalDetailID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showACC_JournalDetailData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ACC_JournalDetail aCC_JournalDetail = new ACC_JournalDetail();

        aCC_JournalDetail.JournalMasterID = Int32.Parse(ddlJournalMaster.SelectedValue);
        aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = Int32.Parse(ddlACC_ChartOfAccountLabel4.SelectedValue);
        aCC_JournalDetail.ACC_ChartOfAccountLabel3ID = Int32.Parse(ddlACC_ChartOfAccountLabel3.SelectedValue);
        aCC_JournalDetail.WorkStation = Int32.Parse(txtWorkStation.Text);
        aCC_JournalDetail.Debit = Decimal.Parse(txtDebit.Text);
        aCC_JournalDetail.Credit = Decimal.Parse(txtCredit.Text);
        aCC_JournalDetail.ExtraField3 = txtExtraField3.Text;
        aCC_JournalDetail.ExtraField2 = txtExtraField2.Text;
        aCC_JournalDetail.ExtraField1 = txtExtraField1.Text;
        aCC_JournalDetail.AddedBy = Int32.Parse(txtAddedBy.Text);
        aCC_JournalDetail.AddedDate = DateTime.Now;
        aCC_JournalDetail.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        aCC_JournalDetail.UpdatedDate = DateTime.Now;
        aCC_JournalDetail.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = ACC_JournalDetailManager.InsertACC_JournalDetail(aCC_JournalDetail);
        Response.Redirect("AdminACC_JournalDetailDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        ACC_JournalDetail aCC_JournalDetail = new ACC_JournalDetail();
        aCC_JournalDetail = ACC_JournalDetailManager.GetACC_JournalDetailByID(Int32.Parse(Request.QueryString["aCC_JournalDetailID"]));
        ACC_JournalDetail tempACC_JournalDetail = new ACC_JournalDetail();
        tempACC_JournalDetail.ACC_JournalDetailID = aCC_JournalDetail.ACC_JournalDetailID;

        tempACC_JournalDetail.JournalMasterID = Int32.Parse(ddlJournalMaster.SelectedValue);
        tempACC_JournalDetail.ACC_ChartOfAccountLabel4ID = Int32.Parse(ddlACC_ChartOfAccountLabel4.SelectedValue);
        tempACC_JournalDetail.ACC_ChartOfAccountLabel3ID = Int32.Parse(ddlACC_ChartOfAccountLabel3.SelectedValue);
        tempACC_JournalDetail.WorkStation = Int32.Parse(txtWorkStation.Text);
        tempACC_JournalDetail.Debit = Decimal.Parse(txtDebit.Text);
        tempACC_JournalDetail.Credit = Decimal.Parse(txtCredit.Text);
        tempACC_JournalDetail.ExtraField3 = txtExtraField3.Text;
        tempACC_JournalDetail.ExtraField2 = txtExtraField2.Text;
        tempACC_JournalDetail.ExtraField1 = txtExtraField1.Text;
        tempACC_JournalDetail.AddedBy = Int32.Parse(txtAddedBy.Text);
        tempACC_JournalDetail.AddedDate = DateTime.Now;
        tempACC_JournalDetail.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        tempACC_JournalDetail.UpdatedDate = DateTime.Now;
        tempACC_JournalDetail.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = ACC_JournalDetailManager.UpdateACC_JournalDetail(tempACC_JournalDetail);
        Response.Redirect("AdminACC_JournalDetailDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlJournalMaster.SelectedIndex = 0;
        ddlACC_ChartOfAccountLabel4.SelectedIndex = 0;
        ddlACC_ChartOfAccountLabel3.SelectedIndex = 0;
        txtWorkStation.Text = "";
        txtDebit.Text = "";
        txtCredit.Text = "";
        txtExtraField3.Text = "";
        txtExtraField2.Text = "";
        txtExtraField1.Text = "";
        txtAddedBy.Text = "";
        txtUpdatedBy.Text = "";
        txtUpdatedDate.Text = "";
        ddlRowStatus.SelectedIndex = 0;
    }
    private void showACC_JournalDetailData()
    {
        ACC_JournalDetail aCC_JournalDetail = new ACC_JournalDetail();
        aCC_JournalDetail = ACC_JournalDetailManager.GetACC_JournalDetailByID(Int32.Parse(Request.QueryString["aCC_JournalDetailID"]));

        ddlJournalMaster.SelectedValue = aCC_JournalDetail.JournalMasterID.ToString();
        ddlACC_ChartOfAccountLabel4.SelectedValue = aCC_JournalDetail.ACC_ChartOfAccountLabel4ID.ToString();
        ddlACC_ChartOfAccountLabel3.SelectedValue = aCC_JournalDetail.ACC_ChartOfAccountLabel3ID.ToString();
        txtWorkStation.Text = aCC_JournalDetail.WorkStation.ToString();
        txtDebit.Text = aCC_JournalDetail.Debit.ToString();
        txtCredit.Text = aCC_JournalDetail.Credit.ToString();
        txtExtraField3.Text = aCC_JournalDetail.ExtraField3;
        txtExtraField2.Text = aCC_JournalDetail.ExtraField2;
        txtExtraField1.Text = aCC_JournalDetail.ExtraField1;
        txtAddedBy.Text = aCC_JournalDetail.AddedBy.ToString();
        txtUpdatedBy.Text = aCC_JournalDetail.UpdatedBy.ToString();
        txtUpdatedDate.Text = aCC_JournalDetail.UpdatedDate;
        ddlRowStatus.SelectedValue = aCC_JournalDetail.RowStatusID.ToString();
    }
    private void loadJournalMaster()
    {
        ListItem li = new ListItem("Select JournalMaster...", "0");
        ddlJournalMaster.Items.Add(li);

        List<JournalMaster> journalMasters = new List<JournalMaster>();
        journalMasters = JournalMasterManager.GetAllJournalMasters();
        foreach (JournalMaster journalMaster in journalMasters)
        {
            ListItem item = new ListItem(journalMaster.JournalMasterName.ToString(), journalMaster.JournalMasterID.ToString());
            ddlJournalMaster.Items.Add(item);
        }
    }
    private void loadACC_ChartOfAccountLabel4()
    {
        ListItem li = new ListItem("Select ACC_ChartOfAccountLabel4...", "0");
        ddlACC_ChartOfAccountLabel4.Items.Add(li);

        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        aCC_ChartOfAccountLabel4s = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4s();
        foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
        {
            ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4Name.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
            ddlACC_ChartOfAccountLabel4.Items.Add(item);
        }
    }
    private void loadACC_ChartOfAccountLabel3()
    {
        ListItem li = new ListItem("Select ACC_ChartOfAccountLabel3...", "0");
        ddlACC_ChartOfAccountLabel3.Items.Add(li);

        List<ACC_ChartOfAccountLabel3> aCC_ChartOfAccountLabel3s = new List<ACC_ChartOfAccountLabel3>();
        aCC_ChartOfAccountLabel3s = ACC_ChartOfAccountLabel3Manager.GetAllACC_ChartOfAccountLabel3s();
        foreach (ACC_ChartOfAccountLabel3 aCC_ChartOfAccountLabel3 in aCC_ChartOfAccountLabel3s)
        {
            ListItem item = new ListItem(aCC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel3Name.ToString(), aCC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel3ID.ToString());
            ddlACC_ChartOfAccountLabel3.Items.Add(item);
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
