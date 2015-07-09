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

public partial class AdminPos_ProductInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadProduct();
            loadReference();
            loadPos_ProductType();
            loadInv_UtilizationDetails();
            loadProductStatus();
            loadPos_Size();
            loadPos_Brand();
            loadInv_QuantityUnit();
            loadPos_Color();
            loadPos_FabricsType();
            loadRowStatus();
            if (Request.QueryString["pos_ProductID"] != null)
            {
                int pos_ProductID = Int32.Parse(Request.QueryString["pos_ProductID"]);
                if (pos_ProductID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showPos_ProductData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Pos_Product pos_Product = new Pos_Product();

        pos_Product.ProductID = Int32.Parse(ddlProduct.SelectedValue);
        pos_Product.ReferenceID = Int32.Parse(ddlReference.SelectedValue);
        pos_Product.Pos_ProductTypeID = Int32.Parse(ddlPos_ProductType.SelectedValue);
        pos_Product.Inv_UtilizationDetailsIDs = Int32.Parse(ddlInv_UtilizationDetails.SelectedValue);
        pos_Product.ProductStatusID = Int32.Parse(ddlProductStatus.SelectedValue);
        pos_Product.ProductName = txtProductName.Text;
        pos_Product.DesignCode = txtDesignCode.Text;
        pos_Product.Pos_SizeID = Int32.Parse(ddlPos_Size.SelectedValue);
        pos_Product.Pos_BrandID = Int32.Parse(ddlPos_Brand.SelectedValue);
        pos_Product.Inv_QuantityUnitID = Int32.Parse(ddlInv_QuantityUnit.SelectedValue);
        pos_Product.FabricsCost = Decimal.Parse(txtFabricsCost.Text);
        pos_Product.AccesoriesCost = Decimal.Parse(txtAccesoriesCost.Text);
        pos_Product.Overhead = Decimal.Parse(txtOverhead.Text);
        pos_Product.OthersCost = Decimal.Parse(txtOthersCost.Text);
        pos_Product.PurchasePrice = Decimal.Parse(txtPurchasePrice.Text);
        pos_Product.SalePrice = Decimal.Parse(txtSalePrice.Text);
        pos_Product.OldSalePrice = Decimal.Parse(txtOldSalePrice.Text);
        pos_Product.Note = txtNote.Text;
        pos_Product.BarCode = txtBarCode.Text;
        pos_Product.Pos_ColorID = Int32.Parse(ddlPos_Color.SelectedValue);
        pos_Product.Pos_FabricsTypeID = Int32.Parse(ddlPos_FabricsType.SelectedValue);
        pos_Product.StyleCode = txtStyleCode.Text;
        pos_Product.Pic1 = txtPic1.Text;
        pos_Product.Pic2 = txtPic2.Text;
        pos_Product.Pic3 = txtPic3.Text;
        pos_Product.VatPercentage = Decimal.Parse(txtVatPercentage.Text);
        pos_Product.IsVatExclusive = cbIsVatExclusive.Checked;
        pos_Product.DiscountPercentage = Decimal.Parse(txtDiscountPercentage.Text);
        pos_Product.DiscountAmount = Decimal.Parse(txtDiscountAmount.Text);
        pos_Product.FabricsNo = txtFabricsNo.Text;
        pos_Product.ExtraField1 = txtExtraField1.Text;
        pos_Product.ExtraField2 = txtExtraField2.Text;
        pos_Product.ExtraField3 = txtExtraField3.Text;
        pos_Product.ExtraField4 = txtExtraField4.Text;
        pos_Product.ExtraField5 = txtExtraField5.Text;
        pos_Product.ExtraField6 = txtExtraField6.Text;
        pos_Product.ExtraField7 = txtExtraField7.Text;
        pos_Product.ExtraField8 = txtExtraField8.Text;
        pos_Product.ExtraField9 = txtExtraField9.Text;
        pos_Product.ExtraField10 = txtExtraField10.Text;
        pos_Product.AddedBy = Int32.Parse(txtAddedBy.Text);
        pos_Product.AddedDate = DateTime.Now;
        pos_Product.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        pos_Product.UpdatedDate = txtUpdatedDate.Text;
        pos_Product.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = Pos_ProductManager.InsertPos_Product(pos_Product);
        Response.Redirect("AdminPos_ProductDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Pos_Product pos_Product = new Pos_Product();
        pos_Product = Pos_ProductManager.GetPos_ProductByID(Int32.Parse(Request.QueryString["pos_ProductID"]));
        Pos_Product tempPos_Product = new Pos_Product();
        tempPos_Product.Pos_ProductID = pos_Product.Pos_ProductID;

        tempPos_Product.ProductID = Int32.Parse(ddlProduct.SelectedValue);
        tempPos_Product.ReferenceID = Int32.Parse(ddlReference.SelectedValue);
        tempPos_Product.Pos_ProductTypeID = Int32.Parse(ddlPos_ProductType.SelectedValue);
        tempPos_Product.Inv_UtilizationDetailsIDs = Int32.Parse(ddlInv_UtilizationDetails.SelectedValue);
        tempPos_Product.ProductStatusID = Int32.Parse(ddlProductStatus.SelectedValue);
        tempPos_Product.ProductName = txtProductName.Text;
        tempPos_Product.DesignCode = txtDesignCode.Text;
        tempPos_Product.Pos_SizeID = Int32.Parse(ddlPos_Size.SelectedValue);
        tempPos_Product.Pos_BrandID = Int32.Parse(ddlPos_Brand.SelectedValue);
        tempPos_Product.Inv_QuantityUnitID = Int32.Parse(ddlInv_QuantityUnit.SelectedValue);
        tempPos_Product.FabricsCost = Decimal.Parse(txtFabricsCost.Text);
        tempPos_Product.AccesoriesCost = Decimal.Parse(txtAccesoriesCost.Text);
        tempPos_Product.Overhead = Decimal.Parse(txtOverhead.Text);
        tempPos_Product.OthersCost = Decimal.Parse(txtOthersCost.Text);
        tempPos_Product.PurchasePrice = Decimal.Parse(txtPurchasePrice.Text);
        tempPos_Product.SalePrice = Decimal.Parse(txtSalePrice.Text);
        tempPos_Product.OldSalePrice = Decimal.Parse(txtOldSalePrice.Text);
        tempPos_Product.Note = txtNote.Text;
        tempPos_Product.BarCode = txtBarCode.Text;
        tempPos_Product.Pos_ColorID = Int32.Parse(ddlPos_Color.SelectedValue);
        tempPos_Product.Pos_FabricsTypeID = Int32.Parse(ddlPos_FabricsType.SelectedValue);
        tempPos_Product.StyleCode = txtStyleCode.Text;
        tempPos_Product.Pic1 = txtPic1.Text;
        tempPos_Product.Pic2 = txtPic2.Text;
        tempPos_Product.Pic3 = txtPic3.Text;
        tempPos_Product.VatPercentage = Decimal.Parse(txtVatPercentage.Text);
        tempPos_Product.IsVatExclusive = cbIsVatExclusive.Checked;
        tempPos_Product.DiscountPercentage = Decimal.Parse(txtDiscountPercentage.Text);
        tempPos_Product.DiscountAmount = Decimal.Parse(txtDiscountAmount.Text);
        tempPos_Product.FabricsNo = txtFabricsNo.Text;
        tempPos_Product.ExtraField1 = txtExtraField1.Text;
        tempPos_Product.ExtraField2 = txtExtraField2.Text;
        tempPos_Product.ExtraField3 = txtExtraField3.Text;
        tempPos_Product.ExtraField4 = txtExtraField4.Text;
        tempPos_Product.ExtraField5 = txtExtraField5.Text;
        tempPos_Product.ExtraField6 = txtExtraField6.Text;
        tempPos_Product.ExtraField7 = txtExtraField7.Text;
        tempPos_Product.ExtraField8 = txtExtraField8.Text;
        tempPos_Product.ExtraField9 = txtExtraField9.Text;
        tempPos_Product.ExtraField10 = txtExtraField10.Text;
        tempPos_Product.AddedBy = Int32.Parse(txtAddedBy.Text);
        tempPos_Product.AddedDate = DateTime.Now;
        tempPos_Product.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        tempPos_Product.UpdatedDate = txtUpdatedDate.Text;
        tempPos_Product.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = Pos_ProductManager.UpdatePos_Product(tempPos_Product);
        Response.Redirect("AdminPos_ProductDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlProduct.SelectedIndex = 0;
        ddlReference.SelectedIndex = 0;
        ddlPos_ProductType.SelectedIndex = 0;
        ddlInv_UtilizationDetails.SelectedIndex = 0;
        ddlProductStatus.SelectedIndex = 0;
        txtProductName.Text = "";
        txtDesignCode.Text = "";
        ddlPos_Size.SelectedIndex = 0;
        ddlPos_Brand.SelectedIndex = 0;
        ddlInv_QuantityUnit.SelectedIndex = 0;
        txtFabricsCost.Text = "";
        txtAccesoriesCost.Text = "";
        txtOverhead.Text = "";
        txtOthersCost.Text = "";
        txtPurchasePrice.Text = "";
        txtSalePrice.Text = "";
        txtOldSalePrice.Text = "";
        txtNote.Text = "";
        txtBarCode.Text = "";
        ddlPos_Color.SelectedIndex = 0;
        ddlPos_FabricsType.SelectedIndex = 0;
        txtStyleCode.Text = "";
        txtPic1.Text = "";
        txtPic2.Text = "";
        txtPic3.Text = "";
        txtVatPercentage.Text = "";
        cbIsVatExclusive.Checked = false;
        txtDiscountPercentage.Text = "";
        txtDiscountAmount.Text = "";
        txtFabricsNo.Text = "";
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
    private void showPos_ProductData()
    {
        Pos_Product pos_Product = new Pos_Product();
        pos_Product = Pos_ProductManager.GetPos_ProductByID(Int32.Parse(Request.QueryString["pos_ProductID"]));

        ddlProduct.SelectedValue = pos_Product.ProductID.ToString();
        ddlReference.SelectedValue = pos_Product.ReferenceID.ToString();
        ddlPos_ProductType.SelectedValue = pos_Product.Pos_ProductTypeID.ToString();
        ddlInv_UtilizationDetails.SelectedValue = pos_Product.Inv_UtilizationDetailsIDs.ToString();
        ddlProductStatus.SelectedValue = pos_Product.ProductStatusID.ToString();
        txtProductName.Text = pos_Product.ProductName;
        txtDesignCode.Text = pos_Product.DesignCode;
        ddlPos_Size.SelectedValue = pos_Product.Pos_SizeID.ToString();
        ddlPos_Brand.SelectedValue = pos_Product.Pos_BrandID.ToString();
        ddlInv_QuantityUnit.SelectedValue = pos_Product.Inv_QuantityUnitID.ToString();
        txtFabricsCost.Text = pos_Product.FabricsCost.ToString();
        txtAccesoriesCost.Text = pos_Product.AccesoriesCost.ToString();
        txtOverhead.Text = pos_Product.Overhead.ToString();
        txtOthersCost.Text = pos_Product.OthersCost.ToString();
        txtPurchasePrice.Text = pos_Product.PurchasePrice.ToString();
        txtSalePrice.Text = pos_Product.SalePrice.ToString();
        txtOldSalePrice.Text = pos_Product.OldSalePrice.ToString();
        txtNote.Text = pos_Product.Note;
        txtBarCode.Text = pos_Product.BarCode;
        ddlPos_Color.SelectedValue = pos_Product.Pos_ColorID.ToString();
        ddlPos_FabricsType.SelectedValue = pos_Product.Pos_FabricsTypeID.ToString();
        txtStyleCode.Text = pos_Product.StyleCode;
        txtPic1.Text = pos_Product.Pic1;
        txtPic2.Text = pos_Product.Pic2;
        txtPic3.Text = pos_Product.Pic3;
        txtVatPercentage.Text = pos_Product.VatPercentage.ToString();
        cbIsVatExclusive.Checked = pos_Product.IsFeature;
        txtDiscountPercentage.Text = pos_Product.DiscountPercentage.ToString();
        txtDiscountAmount.Text = pos_Product.DiscountAmount.ToString();
        txtFabricsNo.Text = pos_Product.FabricsNo;
        txtExtraField1.Text = pos_Product.ExtraField1;
        txtExtraField2.Text = pos_Product.ExtraField2;
        txtExtraField3.Text = pos_Product.ExtraField3;
        txtExtraField4.Text = pos_Product.ExtraField4;
        txtExtraField5.Text = pos_Product.ExtraField5;
        txtExtraField6.Text = pos_Product.ExtraField6;
        txtExtraField7.Text = pos_Product.ExtraField7;
        txtExtraField8.Text = pos_Product.ExtraField8;
        txtExtraField9.Text = pos_Product.ExtraField9;
        txtExtraField10.Text = pos_Product.ExtraField10;
        txtAddedBy.Text = pos_Product.AddedBy.ToString();
        txtUpdatedBy.Text = pos_Product.UpdatedBy.ToString();
        txtUpdatedDate.Text = pos_Product.UpdatedDate;
        ddlRowStatus.SelectedValue = pos_Product.RowStatusID.ToString();
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
    private void loadReference()
    {
        ListItem li = new ListItem("Select Reference...", "0");
        ddlReference.Items.Add(li);

        List<Reference> references = new List<Reference>();
        references = ReferenceManager.GetAllReferences();
        foreach (Reference reference in references)
        {
            ListItem item = new ListItem(reference.ReferenceName.ToString(), reference.ReferenceID.ToString());
            ddlReference.Items.Add(item);
        }
    }
    private void loadPos_ProductType()
    {
        ListItem li = new ListItem("Select Pos_ProductType...", "0");
        ddlPos_ProductType.Items.Add(li);

        List<Pos_ProductType> pos_ProductTypes = new List<Pos_ProductType>();
        pos_ProductTypes = Pos_ProductTypeManager.GetAllPos_ProductTypes();
        foreach (Pos_ProductType pos_ProductType in pos_ProductTypes)
        {
            ListItem item = new ListItem(pos_ProductType.Pos_ProductTypeName.ToString(), pos_ProductType.Pos_ProductTypeID.ToString());
            ddlPos_ProductType.Items.Add(item);
        }
    }
    private void loadInv_UtilizationDetails()
    {
        ListItem li = new ListItem("Select Inv_UtilizationDetails...", "0");
        ddlInv_UtilizationDetails.Items.Add(li);

        List<Inv_UtilizationDetails> inv_UtilizationDetailss = new List<Inv_UtilizationDetails>();
        inv_UtilizationDetailss = Inv_UtilizationDetailsManager.GetAllInv_UtilizationDetailss();
        foreach (Inv_UtilizationDetails inv_UtilizationDetails in inv_UtilizationDetailss)
        {
            ListItem item = new ListItem(inv_UtilizationDetails.Inv_UtilizationDetailsName.ToString(), inv_UtilizationDetails.Inv_UtilizationDetailsIDs.ToString());
            ddlInv_UtilizationDetails.Items.Add(item);
        }
    }
    private void loadProductStatus()
    {
        ListItem li = new ListItem("Select ProductStatus...", "0");
        ddlProductStatus.Items.Add(li);

        List<ProductStatus> productStatuss = new List<ProductStatus>();
        productStatuss = ProductStatusManager.GetAllProductStatuss();
        foreach (ProductStatus productStatus in productStatuss)
        {
            ListItem item = new ListItem(productStatus.ProductStatusName.ToString(), productStatus.ProductStatusID.ToString());
            ddlProductStatus.Items.Add(item);
        }
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
    private void loadPos_Brand()
    {
        ListItem li = new ListItem("Select Pos_Brand...", "0");
        ddlPos_Brand.Items.Add(li);

        List<Pos_Brand> pos_Brands = new List<Pos_Brand>();
        pos_Brands = Pos_BrandManager.GetAllPos_Brands();
        foreach (Pos_Brand pos_Brand in pos_Brands)
        {
            ListItem item = new ListItem(pos_Brand.Pos_BrandName.ToString(), pos_Brand.Pos_BrandID.ToString());
            ddlPos_Brand.Items.Add(item);
        }
    }
    private void loadInv_QuantityUnit()
    {
        ListItem li = new ListItem("Select Inv_QuantityUnit...", "0");
        ddlInv_QuantityUnit.Items.Add(li);

        List<Inv_QuantityUnit> inv_QuantityUnits = new List<Inv_QuantityUnit>();
        inv_QuantityUnits = Inv_QuantityUnitManager.GetAllInv_QuantityUnits();
        foreach (Inv_QuantityUnit inv_QuantityUnit in inv_QuantityUnits)
        {
            ListItem item = new ListItem(inv_QuantityUnit.Inv_QuantityUnitName.ToString(), inv_QuantityUnit.Inv_QuantityUnitID.ToString());
            ddlInv_QuantityUnit.Items.Add(item);
        }
    }
    private void loadPos_Color()
    {
        ListItem li = new ListItem("Select Pos_Color...", "0");
        ddlPos_Color.Items.Add(li);

        List<Pos_Color> pos_Colors = new List<Pos_Color>();
        pos_Colors = Pos_ColorManager.GetAllPos_Colors();
        foreach (Pos_Color pos_Color in pos_Colors)
        {
            ListItem item = new ListItem(pos_Color.Pos_ColorName.ToString(), pos_Color.Pos_ColorID.ToString());
            ddlPos_Color.Items.Add(item);
        }
    }
    private void loadPos_FabricsType()
    {
        ListItem li = new ListItem("Select Pos_FabricsType...", "0");
        ddlPos_FabricsType.Items.Add(li);

        List<Pos_FabricsType> pos_FabricsTypes = new List<Pos_FabricsType>();
        pos_FabricsTypes = Pos_FabricsTypeManager.GetAllPos_FabricsTypes();
        foreach (Pos_FabricsType pos_FabricsType in pos_FabricsTypes)
        {
            ListItem item = new ListItem(pos_FabricsType.Pos_FabricsTypeName.ToString(), pos_FabricsType.Pos_FabricsTypeID.ToString());
            ddlPos_FabricsType.Items.Add(item);
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
