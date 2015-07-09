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

public partial class AdminInv_ProductDetailsInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadProduct();
            loadItem();
            loadRowStatus();
            if (Request.QueryString["inv_ProductDetailsID"] != null)
            {
                int inv_ProductDetailsID = Int32.Parse(Request.QueryString["inv_ProductDetailsID"]);
                if (inv_ProductDetailsID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showInv_ProductDetailsData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Inv_ProductDetails inv_ProductDetails = new Inv_ProductDetails();

        inv_ProductDetails.ProductID = Int32.Parse(ddlProduct.SelectedValue);
        inv_ProductDetails.ItemID = Int32.Parse(ddlItem.SelectedValue);
        inv_ProductDetails.Costing = Decimal.Parse(txtCosting.Text);
        inv_ProductDetails.QuantityProduced = Decimal.Parse(txtQuantityProduced.Text);
        inv_ProductDetails.QuantityUtilized = Decimal.Parse(txtQuantityUtilized.Text);
        inv_ProductDetails.ExtraField2 = txtExtraField2.Text;
        inv_ProductDetails.ExtraField3 = txtExtraField3.Text;
        inv_ProductDetails.ExtraField4 = txtExtraField4.Text;
        inv_ProductDetails.ExtraField5 = txtExtraField5.Text;
        inv_ProductDetails.AddedBy =getLogin().LoginID;
        inv_ProductDetails.AddedDate = DateTime.Now;
        inv_ProductDetails.UpdatedBy = getLogin().LoginID;
        inv_ProductDetails.UpdatedDate = DateTime.Now;
        inv_ProductDetails.RowStatusID = 1;
        int resutl = Inv_ProductDetailsManager.InsertInv_ProductDetails(inv_ProductDetails);
        Response.Redirect("AdminInv_ProductDetailsDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Inv_ProductDetails inv_ProductDetails = new Inv_ProductDetails();
        inv_ProductDetails = Inv_ProductDetailsManager.GetInv_ProductDetailsByID(Int32.Parse(Request.QueryString["inv_ProductDetailsID"]));
        Inv_ProductDetails tempInv_ProductDetails = new Inv_ProductDetails();
        tempInv_ProductDetails.Inv_ProductDetailsID = inv_ProductDetails.Inv_ProductDetailsID;

        tempInv_ProductDetails.ProductID = Int32.Parse(ddlProduct.SelectedValue);
        tempInv_ProductDetails.ItemID = Int32.Parse(ddlItem.SelectedValue);
        tempInv_ProductDetails.Costing = Decimal.Parse(txtCosting.Text);
        tempInv_ProductDetails.QuantityProduced = Decimal.Parse(txtQuantityProduced.Text);
        tempInv_ProductDetails.QuantityUtilized = Decimal.Parse(txtQuantityUtilized.Text);
        tempInv_ProductDetails.ExtraField2 = txtExtraField2.Text;
        tempInv_ProductDetails.ExtraField3 = txtExtraField3.Text;
        tempInv_ProductDetails.ExtraField4 = txtExtraField4.Text;
        tempInv_ProductDetails.ExtraField5 = txtExtraField5.Text;
        tempInv_ProductDetails.AddedBy =getLogin().LoginID;
        tempInv_ProductDetails.AddedDate = DateTime.Now;
        tempInv_ProductDetails.UpdatedBy = getLogin().LoginID;
        tempInv_ProductDetails.UpdatedDate = DateTime.Now;
        tempInv_ProductDetails.RowStatusID = 1;
        bool result = Inv_ProductDetailsManager.UpdateInv_ProductDetails(tempInv_ProductDetails);
        Response.Redirect("AdminInv_ProductDetailsDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlProduct.SelectedIndex = 0;
        ddlItem.SelectedIndex = 0;
        txtCosting.Text = "";
        txtQuantityProduced.Text = "";
        txtQuantityUtilized.Text = "";
        txtExtraField2.Text = "";
        txtExtraField3.Text = "";
        txtExtraField4.Text = "";
        txtExtraField5.Text = "";
        txtAddedBy.Text = "";
        txtUpdatedBy.Text = "";
        txtUpdatedDate.Text = "";
        ddlRowStatus.SelectedIndex = 0;
    }
    private void showInv_ProductDetailsData()
    {
        Inv_ProductDetails inv_ProductDetails = new Inv_ProductDetails();
        inv_ProductDetails = Inv_ProductDetailsManager.GetInv_ProductDetailsByID(Int32.Parse(Request.QueryString["inv_ProductDetailsID"]));

        ddlProduct.SelectedValue = inv_ProductDetails.ProductID.ToString();
        ddlItem.SelectedValue = inv_ProductDetails.ItemID.ToString();
        txtCosting.Text = inv_ProductDetails.Costing.ToString();
        txtQuantityProduced.Text = inv_ProductDetails.QuantityProduced.ToString();
        txtQuantityUtilized.Text = inv_ProductDetails.QuantityUtilized.ToString();
        txtExtraField2.Text = inv_ProductDetails.ExtraField2;
        txtExtraField3.Text = inv_ProductDetails.ExtraField3;
        txtExtraField4.Text = inv_ProductDetails.ExtraField4;
        txtExtraField5.Text = inv_ProductDetails.ExtraField5;
        txtAddedBy.Text = inv_ProductDetails.AddedBy.ToString();
        txtUpdatedBy.Text = inv_ProductDetails.UpdatedBy.ToString();
        txtUpdatedDate.Text = inv_ProductDetails.UpdatedDate;
        ddlRowStatus.SelectedValue = inv_ProductDetails.RowStatusID.ToString();
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
    private void loadItem()
    {
        ListItem li = new ListItem("Select Item...", "0");
        ddlItem.Items.Add(li);

        List<Item> items = new List<Item>();
        items = ItemManager.GetAllItems();
        foreach (Item item in items)
        {
            ListItem item = new ListItem(item.ItemName.ToString(), item.ItemID.ToString());
            ddlItem.Items.Add(item);
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
