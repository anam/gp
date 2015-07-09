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

public partial class AdminACC_JournalMasterInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadRowStatus();
            if (Request.QueryString["aCC_JournalMasterID"] != null)
            {
                int aCC_JournalMasterID = Int32.Parse(Request.QueryString["aCC_JournalMasterID"]);
                if (aCC_JournalMasterID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showACC_JournalMasterData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ACC_JournalMaster aCC_JournalMaster = new ACC_JournalMaster();

        aCC_JournalMaster.JournalMasterName = txtJournalMasterName.Text;
        aCC_JournalMaster.ExtraField1 = txtExtraField1.Text;
        aCC_JournalMaster.ExtraField2 = txtExtraField2.Text;
        aCC_JournalMaster.ExtraField3 = txtExtraField3.Text;
        aCC_JournalMaster.Note = txtNote.Text;
        aCC_JournalMaster.JournalDate = txtJournalDate.Text;
        aCC_JournalMaster.AddedBy = Int32.Parse(txtAddedBy.Text);
        aCC_JournalMaster.AddedDate = DateTime.Now;
        aCC_JournalMaster.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        aCC_JournalMaster.UpdatedDate = DateTime.Now;
        aCC_JournalMaster.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = ACC_JournalMasterManager.InsertACC_JournalMaster(aCC_JournalMaster);
        Response.Redirect("AdminACC_JournalMasterDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        ACC_JournalMaster aCC_JournalMaster = new ACC_JournalMaster();
        aCC_JournalMaster = ACC_JournalMasterManager.GetACC_JournalMasterByID(Int32.Parse(Request.QueryString["aCC_JournalMasterID"]));
        ACC_JournalMaster tempACC_JournalMaster = new ACC_JournalMaster();
        tempACC_JournalMaster.ACC_JournalMasterID = aCC_JournalMaster.ACC_JournalMasterID;

        tempACC_JournalMaster.JournalMasterName = txtJournalMasterName.Text;
        tempACC_JournalMaster.ExtraField1 = txtExtraField1.Text;
        tempACC_JournalMaster.ExtraField2 = txtExtraField2.Text;
        tempACC_JournalMaster.ExtraField3 = txtExtraField3.Text;
        tempACC_JournalMaster.Note = txtNote.Text;
        tempACC_JournalMaster.JournalDate = txtJournalDate.Text;
        tempACC_JournalMaster.AddedBy = Int32.Parse(txtAddedBy.Text);
        tempACC_JournalMaster.AddedDate = DateTime.Now;
        tempACC_JournalMaster.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        tempACC_JournalMaster.UpdatedDate = DateTime.Now;
        tempACC_JournalMaster.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = ACC_JournalMasterManager.UpdateACC_JournalMaster(tempACC_JournalMaster);
        Response.Redirect("AdminACC_JournalMasterDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtJournalMasterName.Text = "";
        txtExtraField1.Text = "";
        txtExtraField2.Text = "";
        txtExtraField3.Text = "";
        txtNote.Text = "";
        txtJournalDate.Text = "";
        txtAddedBy.Text = "";
        txtUpdatedBy.Text = "";
        txtUpdatedDate.Text = "";
        ddlRowStatus.SelectedIndex = 0;
    }
    private void showACC_JournalMasterData()
    {
        ACC_JournalMaster aCC_JournalMaster = new ACC_JournalMaster();
        aCC_JournalMaster = ACC_JournalMasterManager.GetACC_JournalMasterByID(Int32.Parse(Request.QueryString["aCC_JournalMasterID"]));

        txtJournalMasterName.Text = aCC_JournalMaster.JournalMasterName;
        txtExtraField1.Text = aCC_JournalMaster.ExtraField1;
        txtExtraField2.Text = aCC_JournalMaster.ExtraField2;
        txtExtraField3.Text = aCC_JournalMaster.ExtraField3;
        txtNote.Text = aCC_JournalMaster.Note;
        txtJournalDate.Text = aCC_JournalMaster.JournalDate;
        txtAddedBy.Text = aCC_JournalMaster.AddedBy.ToString();
        txtUpdatedBy.Text = aCC_JournalMaster.UpdatedBy.ToString();
        txtUpdatedDate.Text = aCC_JournalMaster.UpdatedDate;
        ddlRowStatus.SelectedValue = aCC_JournalMaster.RowStatusID.ToString();
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
