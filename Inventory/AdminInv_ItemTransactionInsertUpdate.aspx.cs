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

public partial class AdminInv_ItemTransactionInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadItem();
            loadItemTrasactionType();
            loadReference();
            loadRowStatus();
            if (Request.QueryString["inv_ItemTransactionID"] != null)
            {
                int inv_ItemTransactionID = Int32.Parse(Request.QueryString["inv_ItemTransactionID"]);
                if (inv_ItemTransactionID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showInv_ItemTransactionData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Inv_ItemTransaction inv_ItemTransaction = new Inv_ItemTransaction();

        inv_ItemTransaction.ItemID = Int32.Parse(ddlItem.SelectedValue);
        inv_ItemTransaction.Quantity = Decimal.Parse(txtQuantity.Text);
        inv_ItemTransaction.ItemTrasactionTypeID = Int32.Parse(ddlItemTrasactionType.SelectedValue);
        inv_ItemTransaction.ReferenceID = Int32.Parse(ddlReference.SelectedValue);
        inv_ItemTransaction.ExtraField1 = txtExtraField1.Text;
        inv_ItemTransaction.ExtraField2 = txtExtraField2.Text;
        inv_ItemTransaction.ExtraField3 = txtExtraField3.Text;
        inv_ItemTransaction.ExtraField4 = txtExtraField4.Text;
        inv_ItemTransaction.ExtraField5 = txtExtraField5.Text;
        inv_ItemTransaction.AddedBy =getLogin().LoginID;
        inv_ItemTransaction.AddedDate = DateTime.Now;
        inv_ItemTransaction.UpdatedBy = getLogin().LoginID;
        inv_ItemTransaction.UpdatedDate = DateTime.Now;
        inv_ItemTransaction.RowStatusID = 1;
        int resutl = Inv_ItemTransactionManager.InsertInv_ItemTransaction(inv_ItemTransaction);
        Response.Redirect("AdminInv_ItemTransactionDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Inv_ItemTransaction inv_ItemTransaction = new Inv_ItemTransaction();
        inv_ItemTransaction = Inv_ItemTransactionManager.GetInv_ItemTransactionByID(Int32.Parse(Request.QueryString["inv_ItemTransactionID"]));
        Inv_ItemTransaction tempInv_ItemTransaction = new Inv_ItemTransaction();
        tempInv_ItemTransaction.Inv_ItemTransactionID = inv_ItemTransaction.Inv_ItemTransactionID;

        tempInv_ItemTransaction.ItemID = Int32.Parse(ddlItem.SelectedValue);
        tempInv_ItemTransaction.Quantity = Decimal.Parse(txtQuantity.Text);
        tempInv_ItemTransaction.ItemTrasactionTypeID = Int32.Parse(ddlItemTrasactionType.SelectedValue);
        tempInv_ItemTransaction.ReferenceID = Int32.Parse(ddlReference.SelectedValue);
        tempInv_ItemTransaction.ExtraField1 = txtExtraField1.Text;
        tempInv_ItemTransaction.ExtraField2 = txtExtraField2.Text;
        tempInv_ItemTransaction.ExtraField3 = txtExtraField3.Text;
        tempInv_ItemTransaction.ExtraField4 = txtExtraField4.Text;
        tempInv_ItemTransaction.ExtraField5 = txtExtraField5.Text;
        tempInv_ItemTransaction.AddedBy =getLogin().LoginID;
        tempInv_ItemTransaction.AddedDate = DateTime.Now;
        tempInv_ItemTransaction.UpdatedBy = getLogin().LoginID;
        tempInv_ItemTransaction.UpdatedDate = DateTime.Now;
        tempInv_ItemTransaction.RowStatusID = 1;
        bool result = Inv_ItemTransactionManager.UpdateInv_ItemTransaction(tempInv_ItemTransaction);
        Response.Redirect("AdminInv_ItemTransactionDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlItem.SelectedIndex = 0;
        txtQuantity.Text = "";
        ddlItemTrasactionType.SelectedIndex = 0;
        ddlReference.SelectedIndex = 0;
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
    private void showInv_ItemTransactionData()
    {
        Inv_ItemTransaction inv_ItemTransaction = new Inv_ItemTransaction();
        inv_ItemTransaction = Inv_ItemTransactionManager.GetInv_ItemTransactionByID(Int32.Parse(Request.QueryString["inv_ItemTransactionID"]));

        ddlItem.SelectedValue = inv_ItemTransaction.ItemID.ToString();
        txtQuantity.Text = inv_ItemTransaction.Quantity.ToString();
        ddlItemTrasactionType.SelectedValue = inv_ItemTransaction.ItemTrasactionTypeID.ToString();
        ddlReference.SelectedValue = inv_ItemTransaction.ReferenceID.ToString();
        txtExtraField1.Text = inv_ItemTransaction.ExtraField1;
        txtExtraField2.Text = inv_ItemTransaction.ExtraField2;
        txtExtraField3.Text = inv_ItemTransaction.ExtraField3;
        txtExtraField4.Text = inv_ItemTransaction.ExtraField4;
        txtExtraField5.Text = inv_ItemTransaction.ExtraField5;
        txtAddedBy.Text = inv_ItemTransaction.AddedBy.ToString();
        txtUpdatedBy.Text = inv_ItemTransaction.UpdatedBy.ToString();
        txtUpdatedDate.Text = inv_ItemTransaction.UpdatedDate;
        ddlRowStatus.SelectedValue = inv_ItemTransaction.RowStatusID.ToString();
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
    private void loadItemTrasactionType()
    {
        ListItem li = new ListItem("Select ItemTrasactionType...", "0");
        ddlItemTrasactionType.Items.Add(li);

        List<ItemTrasactionType> itemTrasactionTypes = new List<ItemTrasactionType>();
        itemTrasactionTypes = ItemTrasactionTypeManager.GetAllItemTrasactionTypes();
        foreach (ItemTrasactionType itemTrasactionType in itemTrasactionTypes)
        {
            ListItem item = new ListItem(itemTrasactionType.ItemTrasactionTypeName.ToString(), itemTrasactionType.ItemTrasactionTypeID.ToString());
            ddlItemTrasactionType.Items.Add(item);
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
