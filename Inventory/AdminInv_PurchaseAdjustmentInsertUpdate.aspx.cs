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

public partial class AdminInv_PurchaseAdjustmentInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadPurchase();
            loadWorkSatation();
            loadRowStatus();
            if (Request.QueryString["inv_PurchaseAdjustmentID"] != null)
            {
                int inv_PurchaseAdjustmentID = Int32.Parse(Request.QueryString["inv_PurchaseAdjustmentID"]);
                if (inv_PurchaseAdjustmentID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showInv_PurchaseAdjustmentData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Inv_PurchaseAdjustment inv_PurchaseAdjustment = new Inv_PurchaseAdjustment();

        inv_PurchaseAdjustment.PurchseAdjustmentDate = txtPurchseAdjustmentDate.Text;
        inv_PurchaseAdjustment.PurchaseIDs = Int32.Parse(ddlPurchase.SelectedValue);
        inv_PurchaseAdjustment.WorkSatationID = Int32.Parse(ddlWorkSatation.SelectedValue);
        inv_PurchaseAdjustment.ExtraField1 = txtExtraField1.Text;
        inv_PurchaseAdjustment.ExtraField2 = txtExtraField2.Text;
        inv_PurchaseAdjustment.ExtraField3 = txtExtraField3.Text;
        inv_PurchaseAdjustment.ExtraField4 = txtExtraField4.Text;
        inv_PurchaseAdjustment.ExtraField5 = txtExtraField5.Text;
        inv_PurchaseAdjustment.AddedBy = Int32.Parse(txtAddedBy.Text);
        inv_PurchaseAdjustment.AddedDate = DateTime.Now;
        inv_PurchaseAdjustment.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        inv_PurchaseAdjustment.UpdatedDate = txtUpdatedDate.Text;
        inv_PurchaseAdjustment.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = Inv_PurchaseAdjustmentManager.InsertInv_PurchaseAdjustment(inv_PurchaseAdjustment);
        Response.Redirect("AdminInv_PurchaseAdjustmentDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Inv_PurchaseAdjustment inv_PurchaseAdjustment = new Inv_PurchaseAdjustment();
        inv_PurchaseAdjustment = Inv_PurchaseAdjustmentManager.GetInv_PurchaseAdjustmentByID(Int32.Parse(Request.QueryString["inv_PurchaseAdjustmentID"]));
        Inv_PurchaseAdjustment tempInv_PurchaseAdjustment = new Inv_PurchaseAdjustment();
        tempInv_PurchaseAdjustment.Inv_PurchaseAdjustmentID = inv_PurchaseAdjustment.Inv_PurchaseAdjustmentID;

        tempInv_PurchaseAdjustment.PurchseAdjustmentDate = txtPurchseAdjustmentDate.Text;
        tempInv_PurchaseAdjustment.PurchaseIDs = Int32.Parse(ddlPurchase.SelectedValue);
        tempInv_PurchaseAdjustment.WorkSatationID = Int32.Parse(ddlWorkSatation.SelectedValue);
        tempInv_PurchaseAdjustment.ExtraField1 = txtExtraField1.Text;
        tempInv_PurchaseAdjustment.ExtraField2 = txtExtraField2.Text;
        tempInv_PurchaseAdjustment.ExtraField3 = txtExtraField3.Text;
        tempInv_PurchaseAdjustment.ExtraField4 = txtExtraField4.Text;
        tempInv_PurchaseAdjustment.ExtraField5 = txtExtraField5.Text;
        tempInv_PurchaseAdjustment.AddedBy = Int32.Parse(txtAddedBy.Text);
        tempInv_PurchaseAdjustment.AddedDate = DateTime.Now;
        tempInv_PurchaseAdjustment.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        tempInv_PurchaseAdjustment.UpdatedDate = txtUpdatedDate.Text;
        tempInv_PurchaseAdjustment.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = Inv_PurchaseAdjustmentManager.UpdateInv_PurchaseAdjustment(tempInv_PurchaseAdjustment);
        Response.Redirect("AdminInv_PurchaseAdjustmentDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtPurchseAdjustmentDate.Text = "";
        ddlPurchase.SelectedIndex = 0;
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
    private void showInv_PurchaseAdjustmentData()
    {
        Inv_PurchaseAdjustment inv_PurchaseAdjustment = new Inv_PurchaseAdjustment();
        inv_PurchaseAdjustment = Inv_PurchaseAdjustmentManager.GetInv_PurchaseAdjustmentByID(Int32.Parse(Request.QueryString["inv_PurchaseAdjustmentID"]));

        txtPurchseAdjustmentDate.Text = inv_PurchaseAdjustment.PurchseAdjustmentDate;
        ddlPurchase.SelectedValue = inv_PurchaseAdjustment.PurchaseIDs.ToString();
        ddlWorkSatation.SelectedValue = inv_PurchaseAdjustment.WorkSatationID.ToString();
        txtExtraField1.Text = inv_PurchaseAdjustment.ExtraField1;
        txtExtraField2.Text = inv_PurchaseAdjustment.ExtraField2;
        txtExtraField3.Text = inv_PurchaseAdjustment.ExtraField3;
        txtExtraField4.Text = inv_PurchaseAdjustment.ExtraField4;
        txtExtraField5.Text = inv_PurchaseAdjustment.ExtraField5;
        txtAddedBy.Text = inv_PurchaseAdjustment.AddedBy.ToString();
        txtUpdatedBy.Text = inv_PurchaseAdjustment.UpdatedBy.ToString();
        txtUpdatedDate.Text = inv_PurchaseAdjustment.UpdatedDate;
        ddlRowStatus.SelectedValue = inv_PurchaseAdjustment.RowStatusID.ToString();
    }
    private void loadPurchase()
    {
        ListItem li = new ListItem("Select Purchase...", "0");
        ddlPurchase.Items.Add(li);

        List<Purchase> purchases = new List<Purchase>();
        purchases = PurchaseManager.GetAllPurchases();
        foreach (Purchase purchase in purchases)
        {
            ListItem item = new ListItem(purchase.PurchaseName.ToString(), purchase.PurchaseIDs.ToString());
            ddlPurchase.Items.Add(item);
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
