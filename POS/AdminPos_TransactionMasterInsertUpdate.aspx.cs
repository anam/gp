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

public partial class AdminPos_TransactionMasterInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadPos_TransactionType();
            loadTransaction();
            loadToOrFrom();
            loadWorkSatation();
            loadRowStatus();
            if (Request.QueryString["pos_TransactionMasterID"] != null)
            {
                int pos_TransactionMasterID = Int32.Parse(Request.QueryString["pos_TransactionMasterID"]);
                if (pos_TransactionMasterID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showPos_TransactionMasterData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Pos_TransactionMaster pos_TransactionMaster = new Pos_TransactionMaster();

        pos_TransactionMaster.TransactionDate = txtTransactionDate.Text;
        pos_TransactionMaster.Pos_TransactionTypeID = Int32.Parse(ddlPos_TransactionType.SelectedValue);
        pos_TransactionMaster.TransactionID = Int32.Parse(ddlTransaction.SelectedValue);
        pos_TransactionMaster.ToOrFromID = Int32.Parse(ddlToOrFrom.SelectedValue);
        pos_TransactionMaster.Record = txtRecord.Text;
        pos_TransactionMaster.Particulars = txtParticulars.Text;
        pos_TransactionMaster.WorkSatationID = Int32.Parse(ddlWorkSatation.SelectedValue);
        pos_TransactionMaster.ExtraField1 = txtExtraField1.Text;
        pos_TransactionMaster.ExtraField2 = txtExtraField2.Text;
        pos_TransactionMaster.ExtraField3 = txtExtraField3.Text;
        pos_TransactionMaster.ExtraField4 = txtExtraField4.Text;
        pos_TransactionMaster.ExtraField5 = txtExtraField5.Text;
        pos_TransactionMaster.AddedBy = Int32.Parse(txtAddedBy.Text);
        pos_TransactionMaster.AddedDate = DateTime.Now;
        pos_TransactionMaster.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        pos_TransactionMaster.UpdatedDate = txtUpdatedDate.Text;
        pos_TransactionMaster.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = Pos_TransactionMasterManager.InsertPos_TransactionMaster(pos_TransactionMaster);
        Response.Redirect("AdminPos_TransactionMasterDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Pos_TransactionMaster pos_TransactionMaster = new Pos_TransactionMaster();
        pos_TransactionMaster = Pos_TransactionMasterManager.GetPos_TransactionMasterByID(Int32.Parse(Request.QueryString["pos_TransactionMasterID"]));
        Pos_TransactionMaster tempPos_TransactionMaster = new Pos_TransactionMaster();
        tempPos_TransactionMaster.Pos_TransactionMasterID = pos_TransactionMaster.Pos_TransactionMasterID;

        tempPos_TransactionMaster.TransactionDate = txtTransactionDate.Text;
        tempPos_TransactionMaster.Pos_TransactionTypeID = Int32.Parse(ddlPos_TransactionType.SelectedValue);
        tempPos_TransactionMaster.TransactionID = Int32.Parse(ddlTransaction.SelectedValue);
        tempPos_TransactionMaster.ToOrFromID = Int32.Parse(ddlToOrFrom.SelectedValue);
        tempPos_TransactionMaster.Record = txtRecord.Text;
        tempPos_TransactionMaster.Particulars = txtParticulars.Text;
        tempPos_TransactionMaster.WorkSatationID = Int32.Parse(ddlWorkSatation.SelectedValue);
        tempPos_TransactionMaster.ExtraField1 = txtExtraField1.Text;
        tempPos_TransactionMaster.ExtraField2 = txtExtraField2.Text;
        tempPos_TransactionMaster.ExtraField3 = txtExtraField3.Text;
        tempPos_TransactionMaster.ExtraField4 = txtExtraField4.Text;
        tempPos_TransactionMaster.ExtraField5 = txtExtraField5.Text;
        tempPos_TransactionMaster.AddedBy = Int32.Parse(txtAddedBy.Text);
        tempPos_TransactionMaster.AddedDate = DateTime.Now;
        tempPos_TransactionMaster.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        tempPos_TransactionMaster.UpdatedDate = txtUpdatedDate.Text;
        tempPos_TransactionMaster.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = Pos_TransactionMasterManager.UpdatePos_TransactionMaster(tempPos_TransactionMaster);
        Response.Redirect("AdminPos_TransactionMasterDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtTransactionDate.Text = "";
        ddlPos_TransactionType.SelectedIndex = 0;
        ddlTransaction.SelectedIndex = 0;
        ddlToOrFrom.SelectedIndex = 0;
        txtRecord.Text = "";
        txtParticulars.Text = "";
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
    private void showPos_TransactionMasterData()
    {
        Pos_TransactionMaster pos_TransactionMaster = new Pos_TransactionMaster();
        pos_TransactionMaster = Pos_TransactionMasterManager.GetPos_TransactionMasterByID(Int32.Parse(Request.QueryString["pos_TransactionMasterID"]));

        txtTransactionDate.Text = pos_TransactionMaster.TransactionDate;
        ddlPos_TransactionType.SelectedValue = pos_TransactionMaster.Pos_TransactionTypeID.ToString();
        ddlTransaction.SelectedValue = pos_TransactionMaster.TransactionID.ToString();
        ddlToOrFrom.SelectedValue = pos_TransactionMaster.ToOrFromID.ToString();
        txtRecord.Text = pos_TransactionMaster.Record;
        txtParticulars.Text = pos_TransactionMaster.Particulars;
        ddlWorkSatation.SelectedValue = pos_TransactionMaster.WorkSatationID.ToString();
        txtExtraField1.Text = pos_TransactionMaster.ExtraField1;
        txtExtraField2.Text = pos_TransactionMaster.ExtraField2;
        txtExtraField3.Text = pos_TransactionMaster.ExtraField3;
        txtExtraField4.Text = pos_TransactionMaster.ExtraField4;
        txtExtraField5.Text = pos_TransactionMaster.ExtraField5;
        txtAddedBy.Text = pos_TransactionMaster.AddedBy.ToString();
        txtUpdatedBy.Text = pos_TransactionMaster.UpdatedBy.ToString();
        txtUpdatedDate.Text = pos_TransactionMaster.UpdatedDate;
        ddlRowStatus.SelectedValue = pos_TransactionMaster.RowStatusID.ToString();
    }
    private void loadPos_TransactionType()
    {
        ListItem li = new ListItem("Select Pos_TransactionType...", "0");
        ddlPos_TransactionType.Items.Add(li);

        List<Pos_TransactionType> pos_TransactionTypes = new List<Pos_TransactionType>();
        pos_TransactionTypes = Pos_TransactionTypeManager.GetAllPos_TransactionTypes();
        foreach (Pos_TransactionType pos_TransactionType in pos_TransactionTypes)
        {
            ListItem item = new ListItem(pos_TransactionType.Pos_TransactionTypeName.ToString(), pos_TransactionType.Pos_TransactionTypeID.ToString());
            ddlPos_TransactionType.Items.Add(item);
        }
    }
    private void loadTransaction()
    {
        ListItem li = new ListItem("Select Transaction...", "0");
        ddlTransaction.Items.Add(li);

        List<Transaction> transactions = new List<Transaction>();
        transactions = TransactionManager.GetAllTransactions();
        foreach (Transaction transaction in transactions)
        {
            ListItem item = new ListItem(transaction.TransactionName.ToString(), transaction.TransactionID.ToString());
            ddlTransaction.Items.Add(item);
        }
    }
    private void loadToOrFrom()
    {
        ListItem li = new ListItem("Select ToOrFrom...", "0");
        ddlToOrFrom.Items.Add(li);

        List<ToOrFrom> toOrFroms = new List<ToOrFrom>();
        toOrFroms = ToOrFromManager.GetAllToOrFroms();
        foreach (ToOrFrom toOrFrom in toOrFroms)
        {
            ListItem item = new ListItem(toOrFrom.ToOrFromName.ToString(), toOrFrom.ToOrFromID.ToString());
            ddlToOrFrom.Items.Add(item);
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
