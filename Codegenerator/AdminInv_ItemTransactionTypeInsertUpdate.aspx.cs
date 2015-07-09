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

public partial class AdminInv_ItemTransactionTypeInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadRowStatus();
            if (Request.QueryString["inv_ItemTransactionTypeID"] != null)
            {
                int inv_ItemTransactionTypeID = Int32.Parse(Request.QueryString["inv_ItemTransactionTypeID"]);
                if (inv_ItemTransactionTypeID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showInv_ItemTransactionTypeData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Inv_ItemTransactionType inv_ItemTransactionType = new Inv_ItemTransactionType();

        inv_ItemTransactionType.ItemTransactionTypeName = txtItemTransactionTypeName.Text;
        inv_ItemTransactionType.RowStatusID = 1;
        int resutl = Inv_ItemTransactionTypeManager.InsertInv_ItemTransactionType(inv_ItemTransactionType);
        Response.Redirect("AdminInv_ItemTransactionTypeDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Inv_ItemTransactionType inv_ItemTransactionType = new Inv_ItemTransactionType();
        inv_ItemTransactionType = Inv_ItemTransactionTypeManager.GetInv_ItemTransactionTypeByID(Int32.Parse(Request.QueryString["inv_ItemTransactionTypeID"]));
        Inv_ItemTransactionType tempInv_ItemTransactionType = new Inv_ItemTransactionType();
        tempInv_ItemTransactionType.Inv_ItemTransactionTypeID = inv_ItemTransactionType.Inv_ItemTransactionTypeID;

        tempInv_ItemTransactionType.ItemTransactionTypeName = txtItemTransactionTypeName.Text;
        tempInv_ItemTransactionType.RowStatusID = 1;
        bool result = Inv_ItemTransactionTypeManager.UpdateInv_ItemTransactionType(tempInv_ItemTransactionType);
        Response.Redirect("AdminInv_ItemTransactionTypeDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtItemTransactionTypeName.Text = "";
        ddlRowStatus.SelectedIndex = 0;
    }
    private void showInv_ItemTransactionTypeData()
    {
        Inv_ItemTransactionType inv_ItemTransactionType = new Inv_ItemTransactionType();
        inv_ItemTransactionType = Inv_ItemTransactionTypeManager.GetInv_ItemTransactionTypeByID(Int32.Parse(Request.QueryString["inv_ItemTransactionTypeID"]));

        txtItemTransactionTypeName.Text = inv_ItemTransactionType.ItemTransactionTypeName;
        ddlRowStatus.SelectedValue = inv_ItemTransactionType.RowStatusID.ToString();
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
