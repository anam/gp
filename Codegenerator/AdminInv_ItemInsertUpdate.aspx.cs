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

public partial class AdminInv_ItemInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadPurchase();
            loadRawMaterial();
            loadStore();
            loadQualityUnit();
            loadQuantityUnit();
            loadRowStatus();
            if (Request.QueryString["inv_ItemID"] != null)
            {
                int inv_ItemID = Int32.Parse(Request.QueryString["inv_ItemID"]);
                if (inv_ItemID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showInv_ItemData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Inv_Item inv_Item = new Inv_Item();

        inv_Item.ItemName = txtItemName.Text;
        inv_Item.PurchaseID = Int32.Parse(ddlPurchase.SelectedValue);
        inv_Item.ItemCode = txtItemCode.Text;
        inv_Item.RawMaterialID = Int32.Parse(ddlRawMaterial.SelectedValue);
        inv_Item.StoreID = Int32.Parse(ddlStore.SelectedValue);
        inv_Item.QualityUnitID = Int32.Parse(ddlQualityUnit.SelectedValue);
        inv_Item.QualityValue = Decimal.Parse(txtQualityValue.Text);
        inv_Item.QuantityUnitID = Int32.Parse(ddlQuantityUnit.SelectedValue);
        inv_Item.PricePerUnit = Decimal.Parse(txtPricePerUnit.Text);
        inv_Item.PurchasedQuantity = Decimal.Parse(txtPurchasedQuantity.Text);
        inv_Item.IssueReturedQuantity = Decimal.Parse(txtIssueReturedQuantity.Text);
        inv_Item.UtilizedQuantity = Decimal.Parse(txtUtilizedQuantity.Text);
        inv_Item.LostQuantity = Decimal.Parse(txtLostQuantity.Text);
        inv_Item.ExtraFieldQuantity1 = Decimal.Parse(txtExtraFieldQuantity1.Text);
        inv_Item.ExtraFieldQuantity2 = Decimal.Parse(txtExtraFieldQuantity2.Text);
        inv_Item.ExtraFieldQuantity3 = Decimal.Parse(txtExtraFieldQuantity3.Text);
        inv_Item.ExtraFieldQuantity4 = Decimal.Parse(txtExtraFieldQuantity4.Text);
        inv_Item.ExtraFieldQuantity5 = Decimal.Parse(txtExtraFieldQuantity5.Text);
        inv_Item.ExtraField1 = txtExtraField1.Text;
        inv_Item.ExtraField2 = txtExtraField2.Text;
        inv_Item.ExtraField3 = txtExtraField3.Text;
        inv_Item.ExtraField4 = txtExtraField4.Text;
        inv_Item.ExtraField5 = txtExtraField5.Text;
        inv_Item.ExtraField6 = txtExtraField6.Text;
        inv_Item.ExtraField7 = txtExtraField7.Text;
        inv_Item.ExtraField8 = txtExtraField8.Text;
        inv_Item.ExtraField9 = txtExtraField9.Text;
        inv_Item.ExtraField10 = txtExtraField10.Text;
        inv_Item.AddedBy =getLogin().LoginID;
        inv_Item.AddedDate = DateTime.Now;
        inv_Item.UpdatedBy = getLogin().LoginID;
        inv_Item.UpdatedDate = DateTime.Now;
        inv_Item.RowStatusID = 1;
        int resutl = Inv_ItemManager.InsertInv_Item(inv_Item);
        Response.Redirect("AdminInv_ItemDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Inv_Item inv_Item = new Inv_Item();
        inv_Item = Inv_ItemManager.GetInv_ItemByID(Int32.Parse(Request.QueryString["inv_ItemID"]));
        Inv_Item tempInv_Item = new Inv_Item();
        tempInv_Item.Inv_ItemID = inv_Item.Inv_ItemID;

        tempInv_Item.ItemName = txtItemName.Text;
        tempInv_Item.PurchaseID = Int32.Parse(ddlPurchase.SelectedValue);
        tempInv_Item.ItemCode = txtItemCode.Text;
        tempInv_Item.RawMaterialID = Int32.Parse(ddlRawMaterial.SelectedValue);
        tempInv_Item.StoreID = Int32.Parse(ddlStore.SelectedValue);
        tempInv_Item.QualityUnitID = Int32.Parse(ddlQualityUnit.SelectedValue);
        tempInv_Item.QualityValue = Decimal.Parse(txtQualityValue.Text);
        tempInv_Item.QuantityUnitID = Int32.Parse(ddlQuantityUnit.SelectedValue);
        tempInv_Item.PricePerUnit = Decimal.Parse(txtPricePerUnit.Text);
        tempInv_Item.PurchasedQuantity = Decimal.Parse(txtPurchasedQuantity.Text);
        tempInv_Item.IssueReturedQuantity = Decimal.Parse(txtIssueReturedQuantity.Text);
        tempInv_Item.UtilizedQuantity = Decimal.Parse(txtUtilizedQuantity.Text);
        tempInv_Item.LostQuantity = Decimal.Parse(txtLostQuantity.Text);
        tempInv_Item.ExtraFieldQuantity1 = Decimal.Parse(txtExtraFieldQuantity1.Text);
        tempInv_Item.ExtraFieldQuantity2 = Decimal.Parse(txtExtraFieldQuantity2.Text);
        tempInv_Item.ExtraFieldQuantity3 = Decimal.Parse(txtExtraFieldQuantity3.Text);
        tempInv_Item.ExtraFieldQuantity4 = Decimal.Parse(txtExtraFieldQuantity4.Text);
        tempInv_Item.ExtraFieldQuantity5 = Decimal.Parse(txtExtraFieldQuantity5.Text);
        tempInv_Item.ExtraField1 = txtExtraField1.Text;
        tempInv_Item.ExtraField2 = txtExtraField2.Text;
        tempInv_Item.ExtraField3 = txtExtraField3.Text;
        tempInv_Item.ExtraField4 = txtExtraField4.Text;
        tempInv_Item.ExtraField5 = txtExtraField5.Text;
        tempInv_Item.ExtraField6 = txtExtraField6.Text;
        tempInv_Item.ExtraField7 = txtExtraField7.Text;
        tempInv_Item.ExtraField8 = txtExtraField8.Text;
        tempInv_Item.ExtraField9 = txtExtraField9.Text;
        tempInv_Item.ExtraField10 = txtExtraField10.Text;
        tempInv_Item.AddedBy =getLogin().LoginID;
        tempInv_Item.AddedDate = DateTime.Now;
        tempInv_Item.UpdatedBy = getLogin().LoginID;
        tempInv_Item.UpdatedDate = DateTime.Now;
        tempInv_Item.RowStatusID = 1;
        bool result = Inv_ItemManager.UpdateInv_Item(tempInv_Item);
        Response.Redirect("AdminInv_ItemDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtItemName.Text = "";
        ddlPurchase.SelectedIndex = 0;
        txtItemCode.Text = "";
        ddlRawMaterial.SelectedIndex = 0;
        ddlStore.SelectedIndex = 0;
        ddlQualityUnit.SelectedIndex = 0;
        txtQualityValue.Text = "";
        ddlQuantityUnit.SelectedIndex = 0;
        txtPricePerUnit.Text = "";
        txtPurchasedQuantity.Text = "";
        txtIssueReturedQuantity.Text = "";
        txtUtilizedQuantity.Text = "";
        txtLostQuantity.Text = "";
        txtExtraFieldQuantity1.Text = "";
        txtExtraFieldQuantity2.Text = "";
        txtExtraFieldQuantity3.Text = "";
        txtExtraFieldQuantity4.Text = "";
        txtExtraFieldQuantity5.Text = "";
        txtExtraField1.Text = "";
        txtExtraField2.Text = "";
        txtExtraField3.Text = "";
        txtExtraField4.Text = "";
        txtExtraField5.Text = "";
        txtExtraField6.Text = "";
        txtExtraField7.Text = "";
        txtExtraField8.Text = "";
        txtExtraField9.Text = "";
        txtExtraField10.Text = "";
        txtAddedBy.Text = "";
        txtUpdatedBy.Text = "";
        txtUpdatedDate.Text = "";
        ddlRowStatus.SelectedIndex = 0;
    }
    private void showInv_ItemData()
    {
        Inv_Item inv_Item = new Inv_Item();
        inv_Item = Inv_ItemManager.GetInv_ItemByID(Int32.Parse(Request.QueryString["inv_ItemID"]));

        txtItemName.Text = inv_Item.ItemName;
        ddlPurchase.SelectedValue = inv_Item.PurchaseID.ToString();
        txtItemCode.Text = inv_Item.ItemCode;
        ddlRawMaterial.SelectedValue = inv_Item.RawMaterialID.ToString();
        ddlStore.SelectedValue = inv_Item.StoreID.ToString();
        ddlQualityUnit.SelectedValue = inv_Item.QualityUnitID.ToString();
        txtQualityValue.Text = inv_Item.QualityValue.ToString();
        ddlQuantityUnit.SelectedValue = inv_Item.QuantityUnitID.ToString();
        txtPricePerUnit.Text = inv_Item.PricePerUnit.ToString();
        txtPurchasedQuantity.Text = inv_Item.PurchasedQuantity.ToString();
        txtIssueReturedQuantity.Text = inv_Item.IssueReturedQuantity.ToString();
        txtUtilizedQuantity.Text = inv_Item.UtilizedQuantity.ToString();
        txtLostQuantity.Text = inv_Item.LostQuantity.ToString();
        txtExtraFieldQuantity1.Text = inv_Item.ExtraFieldQuantity1.ToString();
        txtExtraFieldQuantity2.Text = inv_Item.ExtraFieldQuantity2.ToString();
        txtExtraFieldQuantity3.Text = inv_Item.ExtraFieldQuantity3.ToString();
        txtExtraFieldQuantity4.Text = inv_Item.ExtraFieldQuantity4.ToString();
        txtExtraFieldQuantity5.Text = inv_Item.ExtraFieldQuantity5.ToString();
        txtExtraField1.Text = inv_Item.ExtraField1;
        txtExtraField2.Text = inv_Item.ExtraField2;
        txtExtraField3.Text = inv_Item.ExtraField3;
        txtExtraField4.Text = inv_Item.ExtraField4;
        txtExtraField5.Text = inv_Item.ExtraField5;
        txtExtraField6.Text = inv_Item.ExtraField6;
        txtExtraField7.Text = inv_Item.ExtraField7;
        txtExtraField8.Text = inv_Item.ExtraField8;
        txtExtraField9.Text = inv_Item.ExtraField9;
        txtExtraField10.Text = inv_Item.ExtraField10;
        txtAddedBy.Text = inv_Item.AddedBy.ToString();
        txtUpdatedBy.Text = inv_Item.UpdatedBy.ToString();
        txtUpdatedDate.Text = inv_Item.UpdatedDate;
        ddlRowStatus.SelectedValue = inv_Item.RowStatusID.ToString();
    }
    private void loadPurchase()
    {
        ListItem li = new ListItem("Select Purchase...", "0");
        ddlPurchase.Items.Add(li);

        List<Purchase> purchases = new List<Purchase>();
        purchases = PurchaseManager.GetAllPurchases();
        foreach (Purchase purchase in purchases)
        {
            ListItem item = new ListItem(purchase.PurchaseName.ToString(), purchase.PurchaseID.ToString());
            ddlPurchase.Items.Add(item);
        }
    }
    private void loadRawMaterial()
    {
        ListItem li = new ListItem("Select RawMaterial...", "0");
        ddlRawMaterial.Items.Add(li);

        List<RawMaterial> rawMaterials = new List<RawMaterial>();
        rawMaterials = RawMaterialManager.GetAllRawMaterials();
        foreach (RawMaterial rawMaterial in rawMaterials)
        {
            ListItem item = new ListItem(rawMaterial.RawMaterialName.ToString(), rawMaterial.RawMaterialID.ToString());
            ddlRawMaterial.Items.Add(item);
        }
    }
    private void loadStore()
    {
        ListItem li = new ListItem("Select Store...", "0");
        ddlStore.Items.Add(li);

        List<Store> stores = new List<Store>();
        stores = StoreManager.GetAllStores();
        foreach (Store store in stores)
        {
            ListItem item = new ListItem(store.StoreName.ToString(), store.StoreID.ToString());
            ddlStore.Items.Add(item);
        }
    }
    private void loadQualityUnit()
    {
        ListItem li = new ListItem("Select QualityUnit...", "0");
        ddlQualityUnit.Items.Add(li);

        List<QualityUnit> qualityUnits = new List<QualityUnit>();
        qualityUnits = QualityUnitManager.GetAllQualityUnits();
        foreach (QualityUnit qualityUnit in qualityUnits)
        {
            ListItem item = new ListItem(qualityUnit.QualityUnitName.ToString(), qualityUnit.QualityUnitID.ToString());
            ddlQualityUnit.Items.Add(item);
        }
    }
    private void loadQuantityUnit()
    {
        ListItem li = new ListItem("Select QuantityUnit...", "0");
        ddlQuantityUnit.Items.Add(li);

        List<QuantityUnit> quantityUnits = new List<QuantityUnit>();
        quantityUnits = QuantityUnitManager.GetAllQuantityUnits();
        foreach (QuantityUnit quantityUnit in quantityUnits)
        {
            ListItem item = new ListItem(quantityUnit.QuantityUnitName.ToString(), quantityUnit.QuantityUnitID.ToString());
            ddlQuantityUnit.Items.Add(item);
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
