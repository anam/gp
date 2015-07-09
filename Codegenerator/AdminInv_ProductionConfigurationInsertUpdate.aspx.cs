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

public partial class AdminInv_ProductionConfigurationInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadProduct();
            loadQualityUnit();
            loadQuantityUnit();
            loadRawMaterial();
            loadRowStatus();
            if (Request.QueryString["inv_ProductionConfigurationID"] != null)
            {
                int inv_ProductionConfigurationID = Int32.Parse(Request.QueryString["inv_ProductionConfigurationID"]);
                if (inv_ProductionConfigurationID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showInv_ProductionConfigurationData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Inv_ProductionConfiguration inv_ProductionConfiguration = new Inv_ProductionConfiguration();

        inv_ProductionConfiguration.ProductID = Int32.Parse(ddlProduct.SelectedValue);
        inv_ProductionConfiguration.QualityValue = Decimal.Parse(txtQualityValue.Text);
        inv_ProductionConfiguration.QualityUnitID = Int32.Parse(ddlQualityUnit.SelectedValue);
        inv_ProductionConfiguration.QuantityValue = Decimal.Parse(txtQuantityValue.Text);
        inv_ProductionConfiguration.QuantityUnitID = Int32.Parse(ddlQuantityUnit.SelectedValue);
        inv_ProductionConfiguration.RawMaterialID = Int32.Parse(ddlRawMaterial.SelectedValue);
        inv_ProductionConfiguration.ExtraField1 = txtExtraField1.Text;
        inv_ProductionConfiguration.ExtraField2 = txtExtraField2.Text;
        inv_ProductionConfiguration.ExtraField3 = txtExtraField3.Text;
        inv_ProductionConfiguration.ExtraField4 = txtExtraField4.Text;
        inv_ProductionConfiguration.ExtraField5 = txtExtraField5.Text;
        inv_ProductionConfiguration.AddedBy =getLogin().LoginID;
        inv_ProductionConfiguration.AddedDate = DateTime.Now;
        inv_ProductionConfiguration.UpdatedBy = getLogin().LoginID;
        inv_ProductionConfiguration.UpdatedDate = DateTime.Now;
        inv_ProductionConfiguration.RowStatusID = 1;
        int resutl = Inv_ProductionConfigurationManager.InsertInv_ProductionConfiguration(inv_ProductionConfiguration);
        Response.Redirect("AdminInv_ProductionConfigurationDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Inv_ProductionConfiguration inv_ProductionConfiguration = new Inv_ProductionConfiguration();
        inv_ProductionConfiguration = Inv_ProductionConfigurationManager.GetInv_ProductionConfigurationByID(Int32.Parse(Request.QueryString["inv_ProductionConfigurationID"]));
        Inv_ProductionConfiguration tempInv_ProductionConfiguration = new Inv_ProductionConfiguration();
        tempInv_ProductionConfiguration.Inv_ProductionConfigurationID = inv_ProductionConfiguration.Inv_ProductionConfigurationID;

        tempInv_ProductionConfiguration.ProductID = Int32.Parse(ddlProduct.SelectedValue);
        tempInv_ProductionConfiguration.QualityValue = Decimal.Parse(txtQualityValue.Text);
        tempInv_ProductionConfiguration.QualityUnitID = Int32.Parse(ddlQualityUnit.SelectedValue);
        tempInv_ProductionConfiguration.QuantityValue = Decimal.Parse(txtQuantityValue.Text);
        tempInv_ProductionConfiguration.QuantityUnitID = Int32.Parse(ddlQuantityUnit.SelectedValue);
        tempInv_ProductionConfiguration.RawMaterialID = Int32.Parse(ddlRawMaterial.SelectedValue);
        tempInv_ProductionConfiguration.ExtraField1 = txtExtraField1.Text;
        tempInv_ProductionConfiguration.ExtraField2 = txtExtraField2.Text;
        tempInv_ProductionConfiguration.ExtraField3 = txtExtraField3.Text;
        tempInv_ProductionConfiguration.ExtraField4 = txtExtraField4.Text;
        tempInv_ProductionConfiguration.ExtraField5 = txtExtraField5.Text;
        tempInv_ProductionConfiguration.AddedBy =getLogin().LoginID;
        tempInv_ProductionConfiguration.AddedDate = DateTime.Now;
        tempInv_ProductionConfiguration.UpdatedBy = getLogin().LoginID;
        tempInv_ProductionConfiguration.UpdatedDate = DateTime.Now;
        tempInv_ProductionConfiguration.RowStatusID = 1;
        bool result = Inv_ProductionConfigurationManager.UpdateInv_ProductionConfiguration(tempInv_ProductionConfiguration);
        Response.Redirect("AdminInv_ProductionConfigurationDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlProduct.SelectedIndex = 0;
        txtQualityValue.Text = "";
        ddlQualityUnit.SelectedIndex = 0;
        txtQuantityValue.Text = "";
        ddlQuantityUnit.SelectedIndex = 0;
        ddlRawMaterial.SelectedIndex = 0;
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
    private void showInv_ProductionConfigurationData()
    {
        Inv_ProductionConfiguration inv_ProductionConfiguration = new Inv_ProductionConfiguration();
        inv_ProductionConfiguration = Inv_ProductionConfigurationManager.GetInv_ProductionConfigurationByID(Int32.Parse(Request.QueryString["inv_ProductionConfigurationID"]));

        ddlProduct.SelectedValue = inv_ProductionConfiguration.ProductID.ToString();
        txtQualityValue.Text = inv_ProductionConfiguration.QualityValue.ToString();
        ddlQualityUnit.SelectedValue = inv_ProductionConfiguration.QualityUnitID.ToString();
        txtQuantityValue.Text = inv_ProductionConfiguration.QuantityValue.ToString();
        ddlQuantityUnit.SelectedValue = inv_ProductionConfiguration.QuantityUnitID.ToString();
        ddlRawMaterial.SelectedValue = inv_ProductionConfiguration.RawMaterialID.ToString();
        txtExtraField1.Text = inv_ProductionConfiguration.ExtraField1;
        txtExtraField2.Text = inv_ProductionConfiguration.ExtraField2;
        txtExtraField3.Text = inv_ProductionConfiguration.ExtraField3;
        txtExtraField4.Text = inv_ProductionConfiguration.ExtraField4;
        txtExtraField5.Text = inv_ProductionConfiguration.ExtraField5;
        txtAddedBy.Text = inv_ProductionConfiguration.AddedBy.ToString();
        txtUpdatedBy.Text = inv_ProductionConfiguration.UpdatedBy.ToString();
        txtUpdatedDate.Text = inv_ProductionConfiguration.UpdatedDate;
        ddlRowStatus.SelectedValue = inv_ProductionConfiguration.RowStatusID.ToString();
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
