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

public partial class AdminInv_UtilizationDetailsInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadPos_Size();
            loadProduct();
            loadInv_Item();
            loadInv_Utilization();
            loadInv_ItemTransaction();
            loadRowStatus();
            if (Request.QueryString["inv_UtilizationDetailsID"] != null)
            {
                int inv_UtilizationDetailsID = Int32.Parse(Request.QueryString["inv_UtilizationDetailsID"]);
                if (inv_UtilizationDetailsID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showInv_UtilizationDetailsData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Inv_UtilizationDetails inv_UtilizationDetails = new Inv_UtilizationDetails();

        inv_UtilizationDetails.Pos_SizeID = Int32.Parse(ddlPos_Size.SelectedValue);
        inv_UtilizationDetails.ProductID = Int32.Parse(ddlProduct.SelectedValue);
        inv_UtilizationDetails.Inv_ItemID = Int32.Parse(ddlInv_Item.SelectedValue);
        inv_UtilizationDetails.Inv_UtilizationID = Int32.Parse(ddlInv_Utilization.SelectedValue);
        inv_UtilizationDetails.Inv_ItemTransactionID = Int32.Parse(ddlInv_ItemTransaction.SelectedValue);
        inv_UtilizationDetails.FabricsCost = Decimal.Parse(txtFabricsCost.Text);
        inv_UtilizationDetails.AccesoriesCost = Decimal.Parse(txtAccesoriesCost.Text);
        inv_UtilizationDetails.Overhead = Decimal.Parse(txtOverhead.Text);
        inv_UtilizationDetails.OthersCost = Decimal.Parse(txtOthersCost.Text);
        inv_UtilizationDetails.ProductionQuantity = Decimal.Parse(txtProductionQuantity.Text);
        inv_UtilizationDetails.ProcessedQuantity = Decimal.Parse(txtProcessedQuantity.Text);
        inv_UtilizationDetails.IsReject = cbIsReject.Checked;
        inv_UtilizationDetails.ExtraField1 = txtExtraField1.Text;
        inv_UtilizationDetails.ExtraField2 = txtExtraField2.Text;
        inv_UtilizationDetails.ExtraField3 = txtExtraField3.Text;
        inv_UtilizationDetails.ExtraField4 = txtExtraField4.Text;
        inv_UtilizationDetails.ExtraField5 = txtExtraField5.Text;
        inv_UtilizationDetails.AddedBy = Int32.Parse(txtAddedBy.Text);
        inv_UtilizationDetails.AddedDate = DateTime.Now;
        inv_UtilizationDetails.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        inv_UtilizationDetails.UpdatedDate = txtUpdatedDate.Text;
        inv_UtilizationDetails.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = Inv_UtilizationDetailsManager.InsertInv_UtilizationDetails(inv_UtilizationDetails);
        Response.Redirect("AdminInv_UtilizationDetailsDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Inv_UtilizationDetails inv_UtilizationDetails = new Inv_UtilizationDetails();
        inv_UtilizationDetails = Inv_UtilizationDetailsManager.GetInv_UtilizationDetailsByID(Int32.Parse(Request.QueryString["inv_UtilizationDetailsID"]));
        Inv_UtilizationDetails tempInv_UtilizationDetails = new Inv_UtilizationDetails();
        tempInv_UtilizationDetails.Inv_UtilizationDetailsID = inv_UtilizationDetails.Inv_UtilizationDetailsID;

        tempInv_UtilizationDetails.Pos_SizeID = Int32.Parse(ddlPos_Size.SelectedValue);
        tempInv_UtilizationDetails.ProductID = Int32.Parse(ddlProduct.SelectedValue);
        tempInv_UtilizationDetails.Inv_ItemID = Int32.Parse(ddlInv_Item.SelectedValue);
        tempInv_UtilizationDetails.Inv_UtilizationID = Int32.Parse(ddlInv_Utilization.SelectedValue);
        tempInv_UtilizationDetails.Inv_ItemTransactionID = Int32.Parse(ddlInv_ItemTransaction.SelectedValue);
        tempInv_UtilizationDetails.FabricsCost = Decimal.Parse(txtFabricsCost.Text);
        tempInv_UtilizationDetails.AccesoriesCost = Decimal.Parse(txtAccesoriesCost.Text);
        tempInv_UtilizationDetails.Overhead = Decimal.Parse(txtOverhead.Text);
        tempInv_UtilizationDetails.OthersCost = Decimal.Parse(txtOthersCost.Text);
        tempInv_UtilizationDetails.ProductionQuantity = Decimal.Parse(txtProductionQuantity.Text);
        tempInv_UtilizationDetails.ProcessedQuantity = Decimal.Parse(txtProcessedQuantity.Text);
        tempInv_UtilizationDetails.IsReject = cbIsReject.Checked;
        tempInv_UtilizationDetails.ExtraField1 = txtExtraField1.Text;
        tempInv_UtilizationDetails.ExtraField2 = txtExtraField2.Text;
        tempInv_UtilizationDetails.ExtraField3 = txtExtraField3.Text;
        tempInv_UtilizationDetails.ExtraField4 = txtExtraField4.Text;
        tempInv_UtilizationDetails.ExtraField5 = txtExtraField5.Text;
        tempInv_UtilizationDetails.AddedBy = Int32.Parse(txtAddedBy.Text);
        tempInv_UtilizationDetails.AddedDate = DateTime.Now;
        tempInv_UtilizationDetails.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        tempInv_UtilizationDetails.UpdatedDate = txtUpdatedDate.Text;
        tempInv_UtilizationDetails.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = Inv_UtilizationDetailsManager.UpdateInv_UtilizationDetails(tempInv_UtilizationDetails);
        Response.Redirect("AdminInv_UtilizationDetailsDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlPos_Size.SelectedIndex = 0;
        ddlProduct.SelectedIndex = 0;
        ddlInv_Item.SelectedIndex = 0;
        ddlInv_Utilization.SelectedIndex = 0;
        ddlInv_ItemTransaction.SelectedIndex = 0;
        txtFabricsCost.Text = "";
        txtAccesoriesCost.Text = "";
        txtOverhead.Text = "";
        txtOthersCost.Text = "";
        txtProductionQuantity.Text = "";
        txtProcessedQuantity.Text = "";
        cbIsReject.Checked = false;
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
    private void showInv_UtilizationDetailsData()
    {
        Inv_UtilizationDetails inv_UtilizationDetails = new Inv_UtilizationDetails();
        inv_UtilizationDetails = Inv_UtilizationDetailsManager.GetInv_UtilizationDetailsByID(Int32.Parse(Request.QueryString["inv_UtilizationDetailsID"]));

        ddlPos_Size.SelectedValue = inv_UtilizationDetails.Pos_SizeID.ToString();
        ddlProduct.SelectedValue = inv_UtilizationDetails.ProductID.ToString();
        ddlInv_Item.SelectedValue = inv_UtilizationDetails.Inv_ItemID.ToString();
        ddlInv_Utilization.SelectedValue = inv_UtilizationDetails.Inv_UtilizationID.ToString();
        ddlInv_ItemTransaction.SelectedValue = inv_UtilizationDetails.Inv_ItemTransactionID.ToString();
        txtFabricsCost.Text = inv_UtilizationDetails.FabricsCost.ToString();
        txtAccesoriesCost.Text = inv_UtilizationDetails.AccesoriesCost.ToString();
        txtOverhead.Text = inv_UtilizationDetails.Overhead.ToString();
        txtOthersCost.Text = inv_UtilizationDetails.OthersCost.ToString();
        txtProductionQuantity.Text = inv_UtilizationDetails.ProductionQuantity.ToString();
        txtProcessedQuantity.Text = inv_UtilizationDetails.ProcessedQuantity.ToString();
        cbIsReject.Checked = inv_UtilizationDetails.IsFeature;
        txtExtraField1.Text = inv_UtilizationDetails.ExtraField1;
        txtExtraField2.Text = inv_UtilizationDetails.ExtraField2;
        txtExtraField3.Text = inv_UtilizationDetails.ExtraField3;
        txtExtraField4.Text = inv_UtilizationDetails.ExtraField4;
        txtExtraField5.Text = inv_UtilizationDetails.ExtraField5;
        txtAddedBy.Text = inv_UtilizationDetails.AddedBy.ToString();
        txtUpdatedBy.Text = inv_UtilizationDetails.UpdatedBy.ToString();
        txtUpdatedDate.Text = inv_UtilizationDetails.UpdatedDate;
        ddlRowStatus.SelectedValue = inv_UtilizationDetails.RowStatusID.ToString();
    }
    private void loadPos_Size()
    {
        ListItem li = new ListItem("Select Pos_Size...", "0");
        ddlPos_Size.Items.Add(li);

        List<Pos_Size> pos_Sizes = new List<Pos_Size>();
        pos_Sizes = Pos_SizeManager.GetAllPos_Sizes();
        foreach (Pos_Size pos_Size in pos_Sizes)
        {
            ListItem item = new ListItem(pos_Size.Pos_SizeName.ToString(), pos_Size.Pos_SizeID.ToString());
            ddlPos_Size.Items.Add(item);
        }
    }
    private void loadProduct()
    {
        ListItem li = new ListItem("Select Product...", "0");
        ddlProduct.Items.Add(li);

        List<Product> products = new List<Product>();
        products = ProductManager.GetAllProducts();
        foreach (Product product in products)
        {
            ListItem item = new ListItem(product.ProductName.ToString(), product.ProductID.ToString());
            ddlProduct.Items.Add(item);
        }
    }
    private void loadInv_Item()
    {
        ListItem li = new ListItem("Select Inv_Item...", "0");
        ddlInv_Item.Items.Add(li);

        List<Inv_Item> inv_Items = new List<Inv_Item>();
        inv_Items = Inv_ItemManager.GetAllInv_Items();
        foreach (Inv_Item inv_Item in inv_Items)
        {
            ListItem item = new ListItem(inv_Item.Inv_ItemName.ToString(), inv_Item.Inv_ItemID.ToString());
            ddlInv_Item.Items.Add(item);
        }
    }
    private void loadInv_Utilization()
    {
        ListItem li = new ListItem("Select Inv_Utilization...", "0");
        ddlInv_Utilization.Items.Add(li);

        List<Inv_Utilization> inv_Utilizations = new List<Inv_Utilization>();
        inv_Utilizations = Inv_UtilizationManager.GetAllInv_Utilizations();
        foreach (Inv_Utilization inv_Utilization in inv_Utilizations)
        {
            ListItem item = new ListItem(inv_Utilization.Inv_UtilizationName.ToString(), inv_Utilization.Inv_UtilizationID.ToString());
            ddlInv_Utilization.Items.Add(item);
        }
    }
    private void loadInv_ItemTransaction()
    {
        ListItem li = new ListItem("Select Inv_ItemTransaction...", "0");
        ddlInv_ItemTransaction.Items.Add(li);

        List<Inv_ItemTransaction> inv_ItemTransactions = new List<Inv_ItemTransaction>();
        inv_ItemTransactions = Inv_ItemTransactionManager.GetAllInv_ItemTransactions();
        foreach (Inv_ItemTransaction inv_ItemTransaction in inv_ItemTransactions)
        {
            ListItem item = new ListItem(inv_ItemTransaction.Inv_ItemTransactionName.ToString(), inv_ItemTransaction.Inv_ItemTransactionID.ToString());
            ddlInv_ItemTransaction.Items.Add(item);
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
