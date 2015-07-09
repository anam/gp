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

public partial class AdminInv_PurchaseReturenInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadPurchase();
            loadWorkSatation();
            loadRowStatus();
            if (Request.QueryString["inv_PurchaseReturenID"] != null)
            {
                int inv_PurchaseReturenID = Int32.Parse(Request.QueryString["inv_PurchaseReturenID"]);
                if (inv_PurchaseReturenID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showInv_PurchaseReturenData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Inv_PurchaseReturen inv_PurchaseReturen = new Inv_PurchaseReturen();

        inv_PurchaseReturen.PurchseReturenDate = txtPurchseReturenDate.Text;
        inv_PurchaseReturen.PurchaseIDs = Int32.Parse(ddlPurchase.SelectedValue);
        inv_PurchaseReturen.WorkSatationID = Int32.Parse(ddlWorkSatation.SelectedValue);
        inv_PurchaseReturen.ExtraField1 = txtExtraField1.Text;
        inv_PurchaseReturen.ExtraField2 = txtExtraField2.Text;
        inv_PurchaseReturen.ExtraField3 = txtExtraField3.Text;
        inv_PurchaseReturen.ExtraField4 = txtExtraField4.Text;
        inv_PurchaseReturen.ExtraField5 = txtExtraField5.Text;
        inv_PurchaseReturen.AddedBy = Int32.Parse(txtAddedBy.Text);
        inv_PurchaseReturen.AddedDate = DateTime.Now;
        inv_PurchaseReturen.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        inv_PurchaseReturen.UpdatedDate = txtUpdatedDate.Text;
        inv_PurchaseReturen.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = Inv_PurchaseReturenManager.InsertInv_PurchaseReturen(inv_PurchaseReturen);
        Response.Redirect("AdminInv_PurchaseReturenDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Inv_PurchaseReturen inv_PurchaseReturen = new Inv_PurchaseReturen();
        inv_PurchaseReturen = Inv_PurchaseReturenManager.GetInv_PurchaseReturenByID(Int32.Parse(Request.QueryString["inv_PurchaseReturenID"]));
        Inv_PurchaseReturen tempInv_PurchaseReturen = new Inv_PurchaseReturen();
        tempInv_PurchaseReturen.Inv_PurchaseReturenID = inv_PurchaseReturen.Inv_PurchaseReturenID;

        tempInv_PurchaseReturen.PurchseReturenDate = txtPurchseReturenDate.Text;
        tempInv_PurchaseReturen.PurchaseIDs = Int32.Parse(ddlPurchase.SelectedValue);
        tempInv_PurchaseReturen.WorkSatationID = Int32.Parse(ddlWorkSatation.SelectedValue);
        tempInv_PurchaseReturen.ExtraField1 = txtExtraField1.Text;
        tempInv_PurchaseReturen.ExtraField2 = txtExtraField2.Text;
        tempInv_PurchaseReturen.ExtraField3 = txtExtraField3.Text;
        tempInv_PurchaseReturen.ExtraField4 = txtExtraField4.Text;
        tempInv_PurchaseReturen.ExtraField5 = txtExtraField5.Text;
        tempInv_PurchaseReturen.AddedBy = Int32.Parse(txtAddedBy.Text);
        tempInv_PurchaseReturen.AddedDate = DateTime.Now;
        tempInv_PurchaseReturen.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        tempInv_PurchaseReturen.UpdatedDate = txtUpdatedDate.Text;
        tempInv_PurchaseReturen.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = Inv_PurchaseReturenManager.UpdateInv_PurchaseReturen(tempInv_PurchaseReturen);
        Response.Redirect("AdminInv_PurchaseReturenDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtPurchseReturenDate.Text = "";
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
    private void showInv_PurchaseReturenData()
    {
        Inv_PurchaseReturen inv_PurchaseReturen = new Inv_PurchaseReturen();
        inv_PurchaseReturen = Inv_PurchaseReturenManager.GetInv_PurchaseReturenByID(Int32.Parse(Request.QueryString["inv_PurchaseReturenID"]));

        txtPurchseReturenDate.Text = inv_PurchaseReturen.PurchseReturenDate;
        ddlPurchase.SelectedValue = inv_PurchaseReturen.PurchaseIDs.ToString();
        ddlWorkSatation.SelectedValue = inv_PurchaseReturen.WorkSatationID.ToString();
        txtExtraField1.Text = inv_PurchaseReturen.ExtraField1;
        txtExtraField2.Text = inv_PurchaseReturen.ExtraField2;
        txtExtraField3.Text = inv_PurchaseReturen.ExtraField3;
        txtExtraField4.Text = inv_PurchaseReturen.ExtraField4;
        txtExtraField5.Text = inv_PurchaseReturen.ExtraField5;
        txtAddedBy.Text = inv_PurchaseReturen.AddedBy.ToString();
        txtUpdatedBy.Text = inv_PurchaseReturen.UpdatedBy.ToString();
        txtUpdatedDate.Text = inv_PurchaseReturen.UpdatedDate;
        ddlRowStatus.SelectedValue = inv_PurchaseReturen.RowStatusID.ToString();
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
