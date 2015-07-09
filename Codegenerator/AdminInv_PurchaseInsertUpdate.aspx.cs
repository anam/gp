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

public partial class AdminInv_PurchaseInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadSuppier();
            loadWorkSatation();
            loadRowStatus();
            if (Request.QueryString["inv_PurchaseID"] != null)
            {
                int inv_PurchaseID = Int32.Parse(Request.QueryString["inv_PurchaseID"]);
                if (inv_PurchaseID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showInv_PurchaseData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Inv_Purchase inv_Purchase = new Inv_Purchase();

        inv_Purchase.PurchaseName = txtPurchaseName.Text;
        inv_Purchase.PurchseDate = txtPurchseDate.Text;
        inv_Purchase.SuppierID = Int32.Parse(ddlSuppier.SelectedValue);
        inv_Purchase.InvoiceNo = txtInvoiceNo.Text;
        inv_Purchase.Particulars = txtParticulars.Text;
        inv_Purchase.IsPurchase = cbIsPurchase.Checked;
        inv_Purchase.WorkSatationID = Int32.Parse(ddlWorkSatation.SelectedValue);
        inv_Purchase.ExtraField1 = txtExtraField1.Text;
        inv_Purchase.ExtraField2 = txtExtraField2.Text;
        inv_Purchase.ExtraField3 = txtExtraField3.Text;
        inv_Purchase.ExtraField4 = txtExtraField4.Text;
        inv_Purchase.ExtraField5 = txtExtraField5.Text;
        inv_Purchase.AddedBy =getLogin().LoginID;
        inv_Purchase.AddedDate = DateTime.Now;
        inv_Purchase.UpdatedBy = getLogin().LoginID;
        inv_Purchase.UpdatedDate = DateTime.Now;
        inv_Purchase.RowStatusID = 1;
        int resutl = Inv_PurchaseManager.InsertInv_Purchase(inv_Purchase);
        Response.Redirect("AdminInv_PurchaseDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Inv_Purchase inv_Purchase = new Inv_Purchase();
        inv_Purchase = Inv_PurchaseManager.GetInv_PurchaseByID(Int32.Parse(Request.QueryString["inv_PurchaseID"]));
        Inv_Purchase tempInv_Purchase = new Inv_Purchase();
        tempInv_Purchase.Inv_PurchaseID = inv_Purchase.Inv_PurchaseID;

        tempInv_Purchase.PurchaseName = txtPurchaseName.Text;
        tempInv_Purchase.PurchseDate = txtPurchseDate.Text;
        tempInv_Purchase.SuppierID = Int32.Parse(ddlSuppier.SelectedValue);
        tempInv_Purchase.InvoiceNo = txtInvoiceNo.Text;
        tempInv_Purchase.Particulars = txtParticulars.Text;
        tempInv_Purchase.IsPurchase = cbIsPurchase.Checked;
        tempInv_Purchase.WorkSatationID = Int32.Parse(ddlWorkSatation.SelectedValue);
        tempInv_Purchase.ExtraField1 = txtExtraField1.Text;
        tempInv_Purchase.ExtraField2 = txtExtraField2.Text;
        tempInv_Purchase.ExtraField3 = txtExtraField3.Text;
        tempInv_Purchase.ExtraField4 = txtExtraField4.Text;
        tempInv_Purchase.ExtraField5 = txtExtraField5.Text;
        tempInv_Purchase.AddedBy =getLogin().LoginID;
        tempInv_Purchase.AddedDate = DateTime.Now;
        tempInv_Purchase.UpdatedBy = getLogin().LoginID;
        tempInv_Purchase.UpdatedDate = DateTime.Now;
        tempInv_Purchase.RowStatusID = 1;
        bool result = Inv_PurchaseManager.UpdateInv_Purchase(tempInv_Purchase);
        Response.Redirect("AdminInv_PurchaseDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtPurchaseName.Text = "";
        txtPurchseDate.Text = "";
        ddlSuppier.SelectedIndex = 0;
        txtInvoiceNo.Text = "";
        txtParticulars.Text = "";
        cbIsPurchase.Checked = false;
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
    private void showInv_PurchaseData()
    {
        Inv_Purchase inv_Purchase = new Inv_Purchase();
        inv_Purchase = Inv_PurchaseManager.GetInv_PurchaseByID(Int32.Parse(Request.QueryString["inv_PurchaseID"]));

        txtPurchaseName.Text = inv_Purchase.PurchaseName;
        txtPurchseDate.Text = inv_Purchase.PurchseDate;
        ddlSuppier.SelectedValue = inv_Purchase.SuppierID.ToString();
        txtInvoiceNo.Text = inv_Purchase.InvoiceNo;
        txtParticulars.Text = inv_Purchase.Particulars;
        cbIsPurchase.Checked = inv_Purchase.IsFeature;
        ddlWorkSatation.SelectedValue = inv_Purchase.WorkSatationID.ToString();
        txtExtraField1.Text = inv_Purchase.ExtraField1;
        txtExtraField2.Text = inv_Purchase.ExtraField2;
        txtExtraField3.Text = inv_Purchase.ExtraField3;
        txtExtraField4.Text = inv_Purchase.ExtraField4;
        txtExtraField5.Text = inv_Purchase.ExtraField5;
        txtAddedBy.Text = inv_Purchase.AddedBy.ToString();
        txtUpdatedBy.Text = inv_Purchase.UpdatedBy.ToString();
        txtUpdatedDate.Text = inv_Purchase.UpdatedDate;
        ddlRowStatus.SelectedValue = inv_Purchase.RowStatusID.ToString();
    }
    private void loadSuppier()
    {
        ListItem li = new ListItem("Select Suppier...", "0");
        ddlSuppier.Items.Add(li);

        List<Suppier> suppiers = new List<Suppier>();
        suppiers = SuppierManager.GetAllSuppiers();
        foreach (Suppier suppier in suppiers)
        {
            ListItem item = new ListItem(suppier.SuppierName.ToString(), suppier.SuppierID.ToString());
            ddlSuppier.Items.Add(item);
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
