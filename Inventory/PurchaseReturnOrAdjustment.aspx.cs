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
            loadACC_ChartOfAccountLabel4();
            txtPurchaseReturnDate.Text = DateTime.Today.ToString("dd MMM yyyy");
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

    private void loadACC_ChartOfAccountLabel4()
    {

        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        aCC_ChartOfAccountLabel4s = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4sForJournalEntry();

        ddlSuppier.Items.Add(new ListItem("Select Supplier", "0"));

        foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
        {
            ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_HeadTypeID.ToString() + "@" + aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 6)
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlSuppier.Items.Add(item);
            }

        }


    }

   
    protected void btnbtnSearch_Click(object sender, EventArgs e)
    {
        showInv_IssueDetailGrid();
    }

    protected void lbSelect_Click(object sender, EventArgs e)
    {
        
    }

    private void showInv_IssueDetailGrid()
    {
        List<Inv_Item> items = new List<Inv_Item>();
        items = Inv_ItemManager.GetAllInv_ItemsInStockBySupplierID(int.Parse(ddlSuppier.SelectedValue));

        foreach (Inv_Item item in items)
        {
            item.PurchasedQuantityPrice = item.PricePerUnit * item.ExtraFieldQuantity1;
        }

        gvInv_Item.DataSource = items;
        gvInv_Item.DataBind();
    }


    private Login getLogin()
    {
        Login login = new Login();
        try
        {
            if (Session["Login"] == null) { Session["PreviousPage"] = HttpContext.Current.Request.Url.AbsoluteUri; Response.Redirect("../LoginPage.aspx"); }

            login = (Login)Session["Login"];
        }
        catch (Exception ex)
        { }

        return login;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        int purchaseReturnID = 0;
        Inv_PurchaseReturen inv_PurchaseReturen = new Inv_PurchaseReturen();

        inv_PurchaseReturen.PurchseReturenDate = DateTime.Parse(txtPurchaseReturnDate.Text);
        inv_PurchaseReturen.PurchaseIDs = getPurchaseIDs(true);
        inv_PurchaseReturen.WorkSatationID = 2;
        inv_PurchaseReturen.ExtraField1 = ddlSuppier.SelectedValue;
        inv_PurchaseReturen.ExtraField2 = txtNote.Text;
        inv_PurchaseReturen.ExtraField3 = "";
        inv_PurchaseReturen.ExtraField4 = "";
        inv_PurchaseReturen.ExtraField5 = "";
        inv_PurchaseReturen.AddedBy = getLogin().LoginID;
        inv_PurchaseReturen.AddedDate = DateTime.Now;
        inv_PurchaseReturen.UpdatedBy = getLogin().LoginID;
        inv_PurchaseReturen.UpdatedDate = DateTime.Now;
        inv_PurchaseReturen.RowStatusID = 1;
                
        int purchaseAdjustmentID = 0;
        Inv_PurchaseAdjustment inv_PurchaseAdjustment = new Inv_PurchaseAdjustment();

        inv_PurchaseAdjustment.PurchseAdjustmentDate = DateTime.Parse(txtPurchaseReturnDate.Text);
        inv_PurchaseAdjustment.PurchaseIDs = getPurchaseIDs(false);
        inv_PurchaseAdjustment.WorkSatationID =2;
        inv_PurchaseAdjustment.ExtraField1 = ddlSuppier.SelectedValue;
        inv_PurchaseAdjustment.ExtraField2 = txtNote.Text;
        inv_PurchaseAdjustment.ExtraField3 = "";
        inv_PurchaseAdjustment.ExtraField4 = "";
        inv_PurchaseAdjustment.ExtraField5 = "";
        inv_PurchaseAdjustment.AddedBy = getLogin().LoginID; 
        inv_PurchaseAdjustment.AddedDate = DateTime.Now;
        inv_PurchaseAdjustment.UpdatedBy = getLogin().LoginID;
        inv_PurchaseAdjustment.UpdatedDate = DateTime.Now;
        inv_PurchaseAdjustment.RowStatusID = 1;


        foreach (GridViewRow gvr in gvInv_Item.Rows)
        {
            HiddenField hfInv_ItemID = (HiddenField)gvr.FindControl("hfInv_ItemID");
            Label lblPurchaseID = (Label)gvr.FindControl("lblPurchaseID");
            TextBox txtReturnQuantity = (TextBox)gvr.FindControl("txtReturnQuantity");
            TextBox txtAdjustmentQuantity = (TextBox)gvr.FindControl("txtAdjustmentQuantity");

            if (txtReturnQuantity.Text != "0")
            {
                

                if (purchaseReturnID == 0)
                {
                    purchaseReturnID = Inv_PurchaseReturenManager.InsertInv_PurchaseReturen(inv_PurchaseReturen);
                }

                Inv_ItemTransaction inv_ItemTransaction = new Inv_ItemTransaction();

                inv_ItemTransaction.ItemID = Int32.Parse(hfInv_ItemID.Value);
                inv_ItemTransaction.Quantity = Decimal.Parse(txtReturnQuantity.Text);
                inv_ItemTransaction.ItemTrasactionTypeID = 1;//purchase return
                inv_ItemTransaction.ReferenceID = purchaseReturnID;
                inv_ItemTransaction.ExtraField1 ="";
                inv_ItemTransaction.ExtraField2 = "";
                inv_ItemTransaction.ExtraField3 = "";
                inv_ItemTransaction.ExtraField4 = "";
                inv_ItemTransaction.ExtraField5 = "";
                inv_ItemTransaction.AddedBy = getLogin().LoginID;
                inv_ItemTransaction.AddedDate = DateTime.Now;
                inv_ItemTransaction.UpdatedBy = getLogin().LoginID;
                inv_ItemTransaction.UpdatedDate = DateTime.Parse(txtPurchaseReturnDate.Text);
                inv_ItemTransaction.RowStatusID = 1;
                int resutl = Inv_ItemTransactionManager.InsertInv_ItemTransaction(inv_ItemTransaction);
            }

            if (txtAdjustmentQuantity.Text != "0")
            {
                if (purchaseAdjustmentID == 0)
                {
                    purchaseAdjustmentID = Inv_PurchaseAdjustmentManager.InsertInv_PurchaseAdjustment(inv_PurchaseAdjustment);
                }

                Inv_ItemTransaction inv_ItemTransaction = new Inv_ItemTransaction();

                inv_ItemTransaction.ItemID = Int32.Parse(hfInv_ItemID.Value);
                inv_ItemTransaction.Quantity = Decimal.Parse(txtAdjustmentQuantity.Text);
                inv_ItemTransaction.ItemTrasactionTypeID =7;//Adjustment
                inv_ItemTransaction.ReferenceID = purchaseAdjustmentID;
                inv_ItemTransaction.ExtraField1 = "";
                inv_ItemTransaction.ExtraField2 = "";
                inv_ItemTransaction.ExtraField3 = "";
                inv_ItemTransaction.ExtraField4 = "";
                inv_ItemTransaction.ExtraField5 = "";
                inv_ItemTransaction.AddedBy = getLogin().LoginID;
                inv_ItemTransaction.AddedDate = DateTime.Now;
                inv_ItemTransaction.UpdatedBy = getLogin().LoginID;
                inv_ItemTransaction.UpdatedDate = DateTime.Parse(txtPurchaseReturnDate.Text);
                inv_ItemTransaction.RowStatusID = 1;
                int resutl = Inv_ItemTransactionManager.InsertInv_ItemTransaction(inv_ItemTransaction);
            }
        }

        if (purchaseReturnID != 0)
        {
            hlnkPurchasePrint.NavigateUrl = "PurchaseReturnPrint.aspx?ReturnID=" + purchaseReturnID.ToString();
            hlnkPurchasePrint.Visible = true;
        }

        if (purchaseAdjustmentID != 0)
        {
            hlnkAdjustmentPrint.NavigateUrl = "AdjustmentPrint.aspx?AdjustmentID=" + purchaseAdjustmentID.ToString();
            hlnkAdjustmentPrint.Visible = true;
        }


        showInv_IssueDetailGrid();
    }

    private string getPurchaseIDs(bool isPurchaseReturn)
    {
        string purchaseIDs = "";
        foreach (GridViewRow gvr in gvInv_Item.Rows)
        {
            Label lblPurchaseID = (Label)gvr.FindControl("lblPurchaseID");
            TextBox txtReturnQuantity=(TextBox)gvr.FindControl("txtReturnQuantity");
            TextBox txtAdjustmentQuantity = (TextBox)gvr.FindControl("txtAdjustmentQuantity");

            if (txtReturnQuantity.Text != "0" && isPurchaseReturn)
            {
                if (!("," + purchaseIDs + ",").Contains("," + lblPurchaseID.Text + ","))
                {
                    purchaseIDs += (purchaseIDs == "" ? "" : ",") + lblPurchaseID.Text;
                }
            }
            else if (txtAdjustmentQuantity.Text != "0" && !isPurchaseReturn)
            {
                if (!("," + purchaseIDs + ",").Contains("," + lblPurchaseID.Text + ","))
                {
                    purchaseIDs += (purchaseIDs == "" ? "" : ",") + lblPurchaseID.Text;
                }
            }
        }

        return purchaseIDs;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        
    }
    private void showInv_ProductData()
    {
        
    }
   
}
