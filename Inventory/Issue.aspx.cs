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
            showInv_IssueDetailGrid();
            initialLoad();
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

    private void initialLoad()
    {
        txtIssueDate.Text = DateTime.Today.ToString("dd MMM yyyy");
        ddlEmployee.SelectedValue = "622";
        ddlWorkSatation.SelectedValue = "2";
        loadLoginInHiddenField();

        lblLastIssueID.Text= CommonManager.SQLExec("Select Max(Inv_IssueMasterID) from Inv_IssueMaster").Tables[0].Rows[0][0].ToString();
    }

    private void loadACC_ChartOfAccountLabel4()
    {

        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        aCC_ChartOfAccountLabel4s = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4sForJournalEntry();

        ddlEmployee.Items.Add(new ListItem("Select Employee", "0"));
        ddlWorkSatation.Items.Add(new ListItem("Select WorkStation", "0"));
        ddlProductAll.Items.Add(new ListItem("Select Product", "0"));

        foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
        {
            ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_HeadTypeID.ToString() + "@" + aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 4)
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlEmployee.Items.Add(item);
            }

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 1)
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlWorkSatation.Items.Add(item);
            }

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 3)
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlProductAll.Items.Add(item);
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
        items = Inv_ItemManager.GetAllInv_ItemsInStock();

        foreach (Inv_Item item in items)
        {
            item.PurchasedQuantityPrice = item.PricePerUnit * item.ExtraFieldQuantity1;
        }

        gvInv_Item.DataSource = items;
        gvInv_Item.DataBind();

        foreach (GridViewRow gvr in gvInv_Item.Rows)
        {
            DropDownList ddlProduct = (DropDownList)gvr.FindControl("ddlProduct");
            HiddenField hfRawMaterialTypeID = (HiddenField)gvr.FindControl("hfRawMaterialTypeID");
            TextBox txtApproximateQuantity = (TextBox)gvr.FindControl("txtApproximateQuantity");

            if (hfRawMaterialTypeID.Value == "2")
            {
                ddlProduct.Items.Clear();

                foreach (ListItem item in ddlProductAll.Items)
                {
                    ddlProduct.Items.Add(item);
                }
            }
            else
            {
                ddlProduct.Items.Add(new ListItem("N/A","0"));
                ddlProduct.Enabled = false;
                txtApproximateQuantity.Enabled = false;
                txtApproximateQuantity.Text = "0";
            }

        }
    }


    private void loadLoginInHiddenField()
    {
        if (hfLoginID.Value == "")
        {
            hfLoginID.Value = getLogin().LoginID.ToString();
        }
    }

    private Login getLogin()
    {
        Login login = new Login();

        try
        {
            if (Session["Login"] != null)
            {
                login = (Login)Session["Login"];
            }
            else if (hfLoginID.Value != "")
            {
                login = LoginManager.GetLoginByID(int.Parse(hfLoginID.Value));
            }
            else
            { Session["PreviousPage"] = HttpContext.Current.Request.Url.AbsoluteUri; Response.Redirect("../LoginPage.aspx"); }

        }
        catch (Exception ex)
        { }

        return login;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (!checking())
        {
            return;
        }

        Inv_IssueMaster inv_IssueMaster = new Inv_IssueMaster();

        inv_IssueMaster.IssueName = "";
        inv_IssueMaster.IssueDate = DateTime.Parse(txtIssueDate.Text);
        inv_IssueMaster.EmployeeID = Int32.Parse(ddlEmployee.SelectedValue);
        inv_IssueMaster.WorkSatationID = Int32.Parse(ddlWorkSatation.SelectedValue);
        inv_IssueMaster.Particulars = txtParticulars.Text;
        inv_IssueMaster.IsIssue = (Request.QueryString["IsNonProduction"] != null ? false : true);
        inv_IssueMaster.ExtraField1 = "";
        inv_IssueMaster.ExtraField2 = "";
        inv_IssueMaster.ExtraField3 = "";
        inv_IssueMaster.ExtraField4 = "";
        inv_IssueMaster.ExtraField5 = "";
        inv_IssueMaster.AddedBy = getLogin().LoginID;
        inv_IssueMaster.AddedDate = DateTime.Now;
        inv_IssueMaster.UpdatedBy = getLogin().LoginID;
        inv_IssueMaster.UpdatedDate = DateTime.Now;
        inv_IssueMaster.RowStatusID = 1;

        if (txtOldIssueID.Text == "")
        {
            inv_IssueMaster.Inv_IssueMasterID = Inv_IssueMasterManager.InsertInv_IssueMaster(inv_IssueMaster);
        }
        else
        {
            inv_IssueMaster.Inv_IssueMasterID = int.Parse(txtOldIssueID.Text);
        }
        int JournalMasterID = 0;
        foreach (GridViewRow gvr in gvInv_Item.Rows)
        {
            HiddenField hfInv_ItemID = (HiddenField)gvr.FindControl("hfInv_ItemID");
            Label lblPurchaseID = (Label)gvr.FindControl("lblPurchaseID");
            TextBox txtIssueQuantity = (TextBox)gvr.FindControl("txtIssueQuantity");
            TextBox txtApproximateQuantity = (TextBox)gvr.FindControl("txtApproximateQuantity");
            DropDownList ddlProduct = (DropDownList)gvr.FindControl("ddlProduct");
            HiddenField hfRawMaterialTypeID = (HiddenField)gvr.FindControl("hfRawMaterialTypeID");

            if (hfRawMaterialTypeID.Value == "10" && JournalMasterID == 0)
            {
                if (ddlWorkSatation.SelectedValue != "1")
                {
                    //Journal Entry
                    ACC_JournalMaster aCC_JournalMaster = new ACC_JournalMaster();

                    aCC_JournalMaster.JournalMasterName = "3";//Journal Voucher
                    aCC_JournalMaster.ExtraField1 = ddlWorkSatation.SelectedItem.Text;
                    aCC_JournalMaster.ExtraField2 = "";
                    aCC_JournalMaster.ExtraField3 = "";
                    aCC_JournalMaster.Note = "Inventory Issue-" + inv_IssueMaster.Inv_IssueMasterID.ToString();
                    aCC_JournalMaster.JournalDate = DateTime.Parse(txtIssueDate.Text);
                    aCC_JournalMaster.AddedBy = getLogin().LoginID;
                    aCC_JournalMaster.AddedDate = DateTime.Now;
                    aCC_JournalMaster.UpdatedBy = getLogin().LoginID;
                    aCC_JournalMaster.UpdatedDate = DateTime.Now;
                    aCC_JournalMaster.RowStatusID = 1;

                    JournalMasterID = ACC_JournalMasterManager.InsertACC_JournalMaster(aCC_JournalMaster);
                }
            }

            Label lblAvailableQuantity = (Label)gvr.FindControl("lblExtraFieldQuantity1");

            if (txtIssueQuantity.Text != "0" && txtIssueQuantity.Text != "")
            {
                
                Inv_IssueDetail inv_IssueDetail = new Inv_IssueDetail();
                inv_IssueDetail.ItemID = Int32.Parse(hfInv_ItemID.Value);
                inv_IssueDetail.Quantity = Decimal.Parse(txtIssueQuantity.Text);
                inv_IssueDetail.ProductID = Int32.Parse(ddlProduct.SelectedValue);
                inv_IssueDetail.AdditionalWithIssueDetailID = 0;
                inv_IssueDetail.ApproximateQuantity = Int32.Parse(txtApproximateQuantity.Text);
                
                inv_IssueDetail.ExtraField1 = txtIssueQuantity.Text;
                inv_IssueDetail.ExtraField2 = "0";
                inv_IssueDetail.ExtraField3 = "0";
                inv_IssueDetail.ExtraField4 = (hfRawMaterialTypeID.Value == "10"?JournalMasterID.ToString():"0");
                inv_IssueDetail.ExtraField5 = inv_IssueMaster.Inv_IssueMasterID.ToString();

                inv_IssueDetail.AddedBy = getLogin().LoginID;
                inv_IssueDetail.AddedDate = DateTime.Now;
                inv_IssueDetail.UpdatedBy = getLogin().LoginID;
                inv_IssueDetail.UpdatedDate = DateTime.Now;
                inv_IssueDetail.RowStatusID = 1;
                if (Inv_IssueDetailManager.InsertInv_IssueDetail(inv_IssueDetail) <= 0)
                {
                    showAlartMessage("Double Pressed");
                    return;
                }
            }

            
        }

        hlnkIssuePrint.NavigateUrl = "IssuePrint.aspx?IssueID=" + inv_IssueMaster.Inv_IssueMasterID.ToString();
        hlnkIssuePrint.Visible = true;
       
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

    protected void btnCalculateAppoximateQuantity_Click(object sender, EventArgs e)
    {
        checking();        
    }


    private bool checking()
    {
        bool returnValue = true;

        List<Inv_ProductionConfiguration> inv_ProductionConfigurations = Inv_ProductionConfigurationManager.GetAllInv_ProductionConfigurations();
        
        foreach (GridViewRow gvr in gvInv_Item.Rows)
        {
            HiddenField hfInv_ItemID = (HiddenField)gvr.FindControl("hfInv_ItemID");
            Label lblPurchaseID = (Label)gvr.FindControl("lblPurchaseID");
            Label lblQualityValue = (Label)gvr.FindControl("lblQualityValue");
            HiddenField hfQualityUnitID = (HiddenField)gvr.FindControl("hfQualityUnitID");
            HiddenField hfRawMaterialID = (HiddenField)gvr.FindControl("hfRawMaterialID");

            TextBox txtIssueQuantity = (TextBox)gvr.FindControl("txtIssueQuantity");
            HiddenField hfQuantityUnitID = (HiddenField)gvr.FindControl("hfQuantityUnitID");

            TextBox txtApproximateQuantity = (TextBox)gvr.FindControl("txtApproximateQuantity");
            DropDownList ddlProduct = (DropDownList)gvr.FindControl("ddlProduct");
            HiddenField hfRawMaterialTypeID = (HiddenField)gvr.FindControl("hfRawMaterialTypeID");

            Label lblAvailableInstock = (Label)gvr.FindControl("lblExtraFieldQuantity1");

            Label lblItemCode = (Label)gvr.FindControl("lblItemCode");

           

            if (hfRawMaterialTypeID.Value != "2" 
                || (txtApproximateQuantity.Text !="" && txtApproximateQuantity.Text !="0"))
            {
                continue;
            }

            if (ddlProduct.SelectedValue == "0"
                && txtApproximateQuantity.Text == "0")
            {
                continue;
            }

            if (txtIssueQuantity.Text == "" || txtIssueQuantity.Text == "0")
            {
                continue;
            }

            if (decimal.Parse(txtIssueQuantity.Text) > decimal.Parse(lblAvailableInstock.Text))
            {
                showAlartMessage("Issue is not permited more than stock for " + lblItemCode.Text);
                returnValue = false;
                continue;
            }

            if (ddlProduct.SelectedValue == "0")
            {
                showAlartMessage("Please Select the product for "+lblItemCode.Text);
                returnValue = false;
                continue;
            }
            bool found = false;
            if (txtApproximateQuantity.Text != "0")
            {
                foreach (Inv_ProductionConfiguration confi in inv_ProductionConfigurations)
                {
                    if (confi.ProductID.ToString() == ddlProduct.SelectedValue
                        && confi.QualityUnitID.ToString() == hfQualityUnitID.Value
                        && confi.QuantityUnitID.ToString() == hfQuantityUnitID.Value
                        && confi.QualityValue == decimal.Parse(lblQualityValue.Text)
                        && confi.RawMaterialID.ToString() == hfRawMaterialID.Value
                        )
                    {
                        txtApproximateQuantity.Text = (decimal.Parse(txtIssueQuantity.Text) / confi.QuantityValue).ToString("0");
                        found = true;
                        break;
                    }
                }
            }
            else
            {
                found = true;
            }
            if (!found)
            {
                showAlartMessage("Configuration missing for " + lblItemCode.Text);
                returnValue = false;
            }

        }

        return returnValue;
    }


    private void showAlartMessage(string message)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
             "err_msg",
             "alert('" + message + "');",
             true);
    }

    protected void btnPreviewTotal_Click(object sender, EventArgs e)
    {
        string html = "<table border='1' cellspacing='0' cellpadding='2'>";
        html += "<tr><td>SI</td><td>Item</td><td>Item Code</td><td>Stock</td><td>Issued Qty</td><td>Product</td><td>Appx. Qty</td></tr>";
        int si = 1;
        decimal issuedQty = 0;
        foreach (GridViewRow gvr in gvInv_Item.Rows)
        {
            Label lblItemName = (Label)gvr.FindControl("lblItemName");
            Label lblItemCode = (Label)gvr.FindControl("lblItemCode");
            Label lblPricePerUnit = (Label)gvr.FindControl("lblPricePerUnit");
            Label lblExtraFieldQuantity1 = (Label)gvr.FindControl("lblExtraFieldQuantity1");
            Label lblQuantityUnitID = (Label)gvr.FindControl("lblQuantityUnitID");
            TextBox txtIssueQuantity = (TextBox)gvr.FindControl("txtIssueQuantity");
            TextBox txtApproximateQuantity = (TextBox)gvr.FindControl("txtApproximateQuantity");
            DropDownList ddlProduct = (DropDownList)gvr.FindControl("ddlProduct");

            if (txtIssueQuantity.Text.Trim() != "")
            {
                if (txtIssueQuantity.Text.Trim() != "0")
                {
                    html += "<tr><td>" + (si++)
                        + @"</td><td>" + lblItemName.Text
                        + @"</td><td>" + lblItemCode.Text
                        + @"</td><td>" + lblExtraFieldQuantity1.Text
                        + @"</td><td>" + txtIssueQuantity.Text
                        + @"</td><td>" + ddlProduct.SelectedItem.Text
                        + @"</td><td>" + txtApproximateQuantity.Text
                        + @"</td></tr>";
                    issuedQty += decimal.Parse(txtIssueQuantity.Text);
                }
            }

        }

        html += "<tr><td></td><td>Total</td><td></td><td></td><td>" + issuedQty.ToString("0,0.00")
                        + @"</td><td></td><td></td></tr></table>";
        lblPreview.Text = html;
        
    }
}
