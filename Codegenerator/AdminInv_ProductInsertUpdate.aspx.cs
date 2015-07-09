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

public partial class AdminInv_ProductInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadProduct();
            loadRowStatus();
            if (Request.QueryString["inv_ProductID"] != null)
            {
                int inv_ProductID = Int32.Parse(Request.QueryString["inv_ProductID"]);
                if (inv_ProductID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showInv_ProductData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Inv_Product inv_Product = new Inv_Product();

        inv_Product.ProductID = Int32.Parse(ddlProduct.SelectedValue);
        inv_Product.ProductCode = Int32.Parse(txtProductCode.Text);
        inv_Product.AvgCosting = Decimal.Parse(txtAvgCosting.Text);
        inv_Product.SalePrice = Decimal.Parse(txtSalePrice.Text);
        inv_Product.ExtraField2 = txtExtraField2.Text;
        inv_Product.ExtraField3 = txtExtraField3.Text;
        inv_Product.ExtraField4 = txtExtraField4.Text;
        inv_Product.ExtraField5 = txtExtraField5.Text;
        inv_Product.AddedBy =getLogin().LoginID;
        inv_Product.AddedDate = DateTime.Now;
        inv_Product.UpdatedBy = getLogin().LoginID;
        inv_Product.UpdatedDate = DateTime.Now;
        inv_Product.RowStatusID = 1;
        int resutl = Inv_ProductManager.InsertInv_Product(inv_Product);
        Response.Redirect("AdminInv_ProductDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Inv_Product inv_Product = new Inv_Product();
        inv_Product = Inv_ProductManager.GetInv_ProductByID(Int32.Parse(Request.QueryString["inv_ProductID"]));
        Inv_Product tempInv_Product = new Inv_Product();
        tempInv_Product.Inv_ProductID = inv_Product.Inv_ProductID;

        tempInv_Product.ProductID = Int32.Parse(ddlProduct.SelectedValue);
        tempInv_Product.ProductCode = Int32.Parse(txtProductCode.Text);
        tempInv_Product.AvgCosting = Decimal.Parse(txtAvgCosting.Text);
        tempInv_Product.SalePrice = Decimal.Parse(txtSalePrice.Text);
        tempInv_Product.ExtraField2 = txtExtraField2.Text;
        tempInv_Product.ExtraField3 = txtExtraField3.Text;
        tempInv_Product.ExtraField4 = txtExtraField4.Text;
        tempInv_Product.ExtraField5 = txtExtraField5.Text;
        tempInv_Product.AddedBy =getLogin().LoginID;
        tempInv_Product.AddedDate = DateTime.Now;
        tempInv_Product.UpdatedBy = getLogin().LoginID;
        tempInv_Product.UpdatedDate = DateTime.Now;
        tempInv_Product.RowStatusID = 1;
        bool result = Inv_ProductManager.UpdateInv_Product(tempInv_Product);
        Response.Redirect("AdminInv_ProductDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlProduct.SelectedIndex = 0;
        txtProductCode.Text = "";
        txtAvgCosting.Text = "";
        txtSalePrice.Text = "";
        txtExtraField2.Text = "";
        txtExtraField3.Text = "";
        txtExtraField4.Text = "";
        txtExtraField5.Text = "";
        txtAddedBy.Text = "";
        txtUpdatedBy.Text = "";
        txtUpdatedDate.Text = "";
        ddlRowStatus.SelectedIndex = 0;
    }
    private void showInv_ProductData()
    {
        Inv_Product inv_Product = new Inv_Product();
        inv_Product = Inv_ProductManager.GetInv_ProductByID(Int32.Parse(Request.QueryString["inv_ProductID"]));

        ddlProduct.SelectedValue = inv_Product.ProductID.ToString();
        txtProductCode.Text = inv_Product.ProductCode.ToString();
        txtAvgCosting.Text = inv_Product.AvgCosting.ToString();
        txtSalePrice.Text = inv_Product.SalePrice.ToString();
        txtExtraField2.Text = inv_Product.ExtraField2;
        txtExtraField3.Text = inv_Product.ExtraField3;
        txtExtraField4.Text = inv_Product.ExtraField4;
        txtExtraField5.Text = inv_Product.ExtraField5;
        txtAddedBy.Text = inv_Product.AddedBy.ToString();
        txtUpdatedBy.Text = inv_Product.UpdatedBy.ToString();
        txtUpdatedDate.Text = inv_Product.UpdatedDate;
        ddlRowStatus.SelectedValue = inv_Product.RowStatusID.ToString();
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
