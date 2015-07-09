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

public partial class AdminPos_TransactionInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadPos_Product();
            loadPos_ProductTrasactionType();
            loadPos_ProductTransactionMaster();
            loadWorkStation();
            loadRowStatus();
            if (Request.QueryString["pos_TransactionID"] != null)
            {
                int pos_TransactionID = Int32.Parse(Request.QueryString["pos_TransactionID"]);
                if (pos_TransactionID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showPos_TransactionData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Pos_Transaction pos_Transaction = new Pos_Transaction();

        pos_Transaction.Pos_ProductID = Int32.Parse(ddlPos_Product.SelectedValue);
        pos_Transaction.Quantity = Decimal.Parse(txtQuantity.Text);
        pos_Transaction.Pos_ProductTrasactionTypeID = Int32.Parse(ddlPos_ProductTrasactionType.SelectedValue);
        pos_Transaction.Pos_ProductTransactionMasterID = Int32.Parse(ddlPos_ProductTransactionMaster.SelectedValue);
        pos_Transaction.WorkStationID = Int32.Parse(ddlWorkStation.SelectedValue);
        pos_Transaction.ExtraField1 = txtExtraField1.Text;
        pos_Transaction.ExtraField2 = txtExtraField2.Text;
        pos_Transaction.ExtraField3 = txtExtraField3.Text;
        pos_Transaction.ExtraField4 = txtExtraField4.Text;
        pos_Transaction.ExtraField5 = txtExtraField5.Text;
        pos_Transaction.AddedBy = Int32.Parse(txtAddedBy.Text);
        pos_Transaction.AddedDate = DateTime.Now;
        pos_Transaction.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        pos_Transaction.UpdatedDate = txtUpdatedDate.Text;
        pos_Transaction.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = Pos_TransactionManager.InsertPos_Transaction(pos_Transaction);
        Response.Redirect("AdminPos_TransactionDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Pos_Transaction pos_Transaction = new Pos_Transaction();
        pos_Transaction = Pos_TransactionManager.GetPos_TransactionByID(Int32.Parse(Request.QueryString["pos_TransactionID"]));
        Pos_Transaction tempPos_Transaction = new Pos_Transaction();
        tempPos_Transaction.Pos_TransactionID = pos_Transaction.Pos_TransactionID;

        tempPos_Transaction.Pos_ProductID = Int32.Parse(ddlPos_Product.SelectedValue);
        tempPos_Transaction.Quantity = Decimal.Parse(txtQuantity.Text);
        tempPos_Transaction.Pos_ProductTrasactionTypeID = Int32.Parse(ddlPos_ProductTrasactionType.SelectedValue);
        tempPos_Transaction.Pos_ProductTransactionMasterID = Int32.Parse(ddlPos_ProductTransactionMaster.SelectedValue);
        tempPos_Transaction.WorkStationID = Int32.Parse(ddlWorkStation.SelectedValue);
        tempPos_Transaction.ExtraField1 = txtExtraField1.Text;
        tempPos_Transaction.ExtraField2 = txtExtraField2.Text;
        tempPos_Transaction.ExtraField3 = txtExtraField3.Text;
        tempPos_Transaction.ExtraField4 = txtExtraField4.Text;
        tempPos_Transaction.ExtraField5 = txtExtraField5.Text;
        tempPos_Transaction.AddedBy = Int32.Parse(txtAddedBy.Text);
        tempPos_Transaction.AddedDate = DateTime.Now;
        tempPos_Transaction.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        tempPos_Transaction.UpdatedDate = txtUpdatedDate.Text;
        tempPos_Transaction.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = Pos_TransactionManager.UpdatePos_Transaction(tempPos_Transaction);
        Response.Redirect("AdminPos_TransactionDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlPos_Product.SelectedIndex = 0;
        txtQuantity.Text = "";
        ddlPos_ProductTrasactionType.SelectedIndex = 0;
        ddlPos_ProductTransactionMaster.SelectedIndex = 0;
        ddlWorkStation.SelectedIndex = 0;
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
    private void showPos_TransactionData()
    {
        Pos_Transaction pos_Transaction = new Pos_Transaction();
        pos_Transaction = Pos_TransactionManager.GetPos_TransactionByID(Int32.Parse(Request.QueryString["pos_TransactionID"]));

        ddlPos_Product.SelectedValue = pos_Transaction.Pos_ProductID.ToString();
        txtQuantity.Text = pos_Transaction.Quantity.ToString();
        ddlPos_ProductTrasactionType.SelectedValue = pos_Transaction.Pos_ProductTrasactionTypeID.ToString();
        ddlPos_ProductTransactionMaster.SelectedValue = pos_Transaction.Pos_ProductTransactionMasterID.ToString();
        ddlWorkStation.SelectedValue = pos_Transaction.WorkStationID.ToString();
        txtExtraField1.Text = pos_Transaction.ExtraField1;
        txtExtraField2.Text = pos_Transaction.ExtraField2;
        txtExtraField3.Text = pos_Transaction.ExtraField3;
        txtExtraField4.Text = pos_Transaction.ExtraField4;
        txtExtraField5.Text = pos_Transaction.ExtraField5;
        txtAddedBy.Text = pos_Transaction.AddedBy.ToString();
        txtUpdatedBy.Text = pos_Transaction.UpdatedBy.ToString();
        txtUpdatedDate.Text = pos_Transaction.UpdatedDate;
        ddlRowStatus.SelectedValue = pos_Transaction.RowStatusID.ToString();
    }
    private void loadPos_Product()
    {
        ListItem li = new ListItem("Select Pos_Product...", "0");
        ddlPos_Product.Items.Add(li);

        List<Pos_Product> pos_Products = new List<Pos_Product>();
        pos_Products = Pos_ProductManager.GetAllPos_Products();
        foreach (Pos_Product pos_Product in pos_Products)
        {
            ListItem item = new ListItem(pos_Product.Pos_ProductName.ToString(), pos_Product.Pos_ProductID.ToString());
            ddlPos_Product.Items.Add(item);
        }
    }
    private void loadPos_ProductTrasactionType()
    {
        ListItem li = new ListItem("Select Pos_ProductTrasactionType...", "0");
        ddlPos_ProductTrasactionType.Items.Add(li);

        List<Pos_ProductTrasactionType> pos_ProductTrasactionTypes = new List<Pos_ProductTrasactionType>();
        pos_ProductTrasactionTypes = Pos_ProductTrasactionTypeManager.GetAllPos_ProductTrasactionTypes();
        foreach (Pos_ProductTrasactionType pos_ProductTrasactionType in pos_ProductTrasactionTypes)
        {
            ListItem item = new ListItem(pos_ProductTrasactionType.Pos_ProductTrasactionTypeName.ToString(), pos_ProductTrasactionType.Pos_ProductTrasactionTypeID.ToString());
            ddlPos_ProductTrasactionType.Items.Add(item);
        }
    }
    private void loadPos_ProductTransactionMaster()
    {
        ListItem li = new ListItem("Select Pos_ProductTransactionMaster...", "0");
        ddlPos_ProductTransactionMaster.Items.Add(li);

        List<Pos_ProductTransactionMaster> pos_ProductTransactionMasters = new List<Pos_ProductTransactionMaster>();
        pos_ProductTransactionMasters = Pos_ProductTransactionMasterManager.GetAllPos_ProductTransactionMasters();
        foreach (Pos_ProductTransactionMaster pos_ProductTransactionMaster in pos_ProductTransactionMasters)
        {
            ListItem item = new ListItem(pos_ProductTransactionMaster.Pos_ProductTransactionMasterName.ToString(), pos_ProductTransactionMaster.Pos_ProductTransactionMasterID.ToString());
            ddlPos_ProductTransactionMaster.Items.Add(item);
        }
    }
    private void loadWorkStation()
    {
        ListItem li = new ListItem("Select WorkStation...", "0");
        ddlWorkStation.Items.Add(li);

        List<WorkStation> workStations = new List<WorkStation>();
        workStations = WorkStationManager.GetAllWorkStations();
        foreach (WorkStation workStation in workStations)
        {
            ListItem item = new ListItem(workStation.WorkStationName.ToString(), workStation.WorkStationID.ToString());
            ddlWorkStation.Items.Add(item);
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
