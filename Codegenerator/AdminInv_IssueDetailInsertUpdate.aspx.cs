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

public partial class AdminInv_IssueDetailInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadItem();
            loadProduct();
            loadAdditionalWithIssueDetail();
            loadRowStatus();
            if (Request.QueryString["inv_IssueDetailID"] != null)
            {
                int inv_IssueDetailID = Int32.Parse(Request.QueryString["inv_IssueDetailID"]);
                if (inv_IssueDetailID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showInv_IssueDetailData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Inv_IssueDetail inv_IssueDetail = new Inv_IssueDetail();

        inv_IssueDetail.ItemID = Int32.Parse(ddlItem.SelectedValue);
        inv_IssueDetail.Quantity = Decimal.Parse(txtQuantity.Text);
        inv_IssueDetail.ApproximateQuantity = Int32.Parse(txtApproximateQuantity.Text);
        inv_IssueDetail.ProductID = Int32.Parse(ddlProduct.SelectedValue);
        inv_IssueDetail.AdditionalWithIssueDetailID = Int32.Parse(ddlAdditionalWithIssueDetail.SelectedValue);
        inv_IssueDetail.ExtraField1 = txtExtraField1.Text;
        inv_IssueDetail.ExtraField2 = txtExtraField2.Text;
        inv_IssueDetail.ExtraField3 = txtExtraField3.Text;
        inv_IssueDetail.ExtraField4 = txtExtraField4.Text;
        inv_IssueDetail.ExtraField5 = txtExtraField5.Text;
        inv_IssueDetail.AddedBy =getLogin().LoginID;
        inv_IssueDetail.AddedDate = DateTime.Now;
        inv_IssueDetail.UpdatedBy = getLogin().LoginID;
        inv_IssueDetail.UpdatedDate = DateTime.Now;
        inv_IssueDetail.RowStatusID = 1;
        int resutl = Inv_IssueDetailManager.InsertInv_IssueDetail(inv_IssueDetail);
        Response.Redirect("AdminInv_IssueDetailDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Inv_IssueDetail inv_IssueDetail = new Inv_IssueDetail();
        inv_IssueDetail = Inv_IssueDetailManager.GetInv_IssueDetailByID(Int32.Parse(Request.QueryString["inv_IssueDetailID"]));
        Inv_IssueDetail tempInv_IssueDetail = new Inv_IssueDetail();
        tempInv_IssueDetail.Inv_IssueDetailID = inv_IssueDetail.Inv_IssueDetailID;

        tempInv_IssueDetail.ItemID = Int32.Parse(ddlItem.SelectedValue);
        tempInv_IssueDetail.Quantity = Decimal.Parse(txtQuantity.Text);
        tempInv_IssueDetail.ApproximateQuantity = Int32.Parse(txtApproximateQuantity.Text);
        tempInv_IssueDetail.ProductID = Int32.Parse(ddlProduct.SelectedValue);
        tempInv_IssueDetail.AdditionalWithIssueDetailID = Int32.Parse(ddlAdditionalWithIssueDetail.SelectedValue);
        tempInv_IssueDetail.ExtraField1 = txtExtraField1.Text;
        tempInv_IssueDetail.ExtraField2 = txtExtraField2.Text;
        tempInv_IssueDetail.ExtraField3 = txtExtraField3.Text;
        tempInv_IssueDetail.ExtraField4 = txtExtraField4.Text;
        tempInv_IssueDetail.ExtraField5 = txtExtraField5.Text;
        tempInv_IssueDetail.AddedBy =getLogin().LoginID;
        tempInv_IssueDetail.AddedDate = DateTime.Now;
        tempInv_IssueDetail.UpdatedBy = getLogin().LoginID;
        tempInv_IssueDetail.UpdatedDate = DateTime.Now;
        tempInv_IssueDetail.RowStatusID = 1;
        bool result = Inv_IssueDetailManager.UpdateInv_IssueDetail(tempInv_IssueDetail);
        Response.Redirect("AdminInv_IssueDetailDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlItem.SelectedIndex = 0;
        txtQuantity.Text = "";
        txtApproximateQuantity.Text = "";
        ddlProduct.SelectedIndex = 0;
        ddlAdditionalWithIssueDetail.SelectedIndex = 0;
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
    private void showInv_IssueDetailData()
    {
        Inv_IssueDetail inv_IssueDetail = new Inv_IssueDetail();
        inv_IssueDetail = Inv_IssueDetailManager.GetInv_IssueDetailByID(Int32.Parse(Request.QueryString["inv_IssueDetailID"]));

        ddlItem.SelectedValue = inv_IssueDetail.ItemID.ToString();
        txtQuantity.Text = inv_IssueDetail.Quantity.ToString();
        txtApproximateQuantity.Text = inv_IssueDetail.ApproximateQuantity.ToString();
        ddlProduct.SelectedValue = inv_IssueDetail.ProductID.ToString();
        ddlAdditionalWithIssueDetail.SelectedValue = inv_IssueDetail.AdditionalWithIssueDetailID.ToString();
        txtExtraField1.Text = inv_IssueDetail.ExtraField1;
        txtExtraField2.Text = inv_IssueDetail.ExtraField2;
        txtExtraField3.Text = inv_IssueDetail.ExtraField3;
        txtExtraField4.Text = inv_IssueDetail.ExtraField4;
        txtExtraField5.Text = inv_IssueDetail.ExtraField5;
        txtAddedBy.Text = inv_IssueDetail.AddedBy.ToString();
        txtUpdatedBy.Text = inv_IssueDetail.UpdatedBy.ToString();
        txtUpdatedDate.Text = inv_IssueDetail.UpdatedDate;
        ddlRowStatus.SelectedValue = inv_IssueDetail.RowStatusID.ToString();
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
    private void loadAdditionalWithIssueDetail()
    {
        ListItem li = new ListItem("Select AdditionalWithIssueDetail...", "0");
        ddlAdditionalWithIssueDetail.Items.Add(li);

        List<AdditionalWithIssueDetail> additionalWithIssueDetails = new List<AdditionalWithIssueDetail>();
        additionalWithIssueDetails = AdditionalWithIssueDetailManager.GetAllAdditionalWithIssueDetails();
        foreach (AdditionalWithIssueDetail additionalWithIssueDetail in additionalWithIssueDetails)
        {
            ListItem item = new ListItem(additionalWithIssueDetail.AdditionalWithIssueDetailName.ToString(), additionalWithIssueDetail.AdditionalWithIssueDetailID.ToString());
            ddlAdditionalWithIssueDetail.Items.Add(item);
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
