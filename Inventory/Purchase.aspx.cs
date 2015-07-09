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
            initialLoad();
            loadACC_ChartOfAccountLabel4();
            loadQualityUnit();
            loadQuantityUnit();
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

            if (Request.QueryString["Return"] != null)
            {
                loadItem();
                tr_StockItemList.Visible = true;
            }
            else
            {
                tr_StockItemList.Visible = false;
            }
        }
    }

    private void initialLoad()
    {
        txtPurchseDate.Text = DateTime.Today.ToString("dd MMM yyyy");
        txtItemCode.Text = "1.00";
        loadLoginInHiddenField();
        lblLastPurchaseID.Text = CommonManager.SQLExec("Select Max(Inv_PurchaseID) from Inv_Purchase").Tables[0].Rows[0][0].ToString();

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
        lblMsgWornInput.Visible = false ;
        if (txtPurchasedQuantity.Text == ""
            || txtPricePerUnit.Text == ""
            || txtItemCode.Text ==""
            || ddlQuantityUnit.SelectedValue =="0"
            )
        {
            lblMsgWornInput.Visible = true;
            return; }

        if (ddlRawMaterial.SelectedValue == "0")
        {
            showAlartMessage("Please select the Rawmaterial");
            return;
        }


        List<Inv_Item> PurchasedItem = new List<Inv_Item>();
        PurchasedItem = loadFromTheGrid();

        Inv_Item inv_Item = new Inv_Item();
        inv_Item.ItemName = ddlRawMaterial.SelectedItem.Text;
        inv_Item.PurchaseID = PurchasedItem.Count+1;
        inv_Item.ItemCode = decimal.Parse(txtItemCode.Text).ToString("000.00");
        inv_Item.RawMaterialID = Int32.Parse(ddlRawMaterial.SelectedValue.Split('@')[1]);
        inv_Item.StoreID = 2;
        inv_Item.QualityUnitID = Int32.Parse(ddlQualityUnit.SelectedValue);
        inv_Item.QualityUnitName = ddlQualityUnit.SelectedItem.Text;
        inv_Item.QualityValue = Decimal.Parse(txtQualityValue.Text==""? "0":txtQualityValue.Text);
        inv_Item.QuantityUnitID = Int32.Parse(ddlQuantityUnit.SelectedValue);
        inv_Item.QuantityUnitName = ddlQuantityUnit.SelectedItem.Text;
        inv_Item.PricePerUnit = Decimal.Parse(txtPricePerUnit.Text);
        inv_Item.PurchasedQuantity = Decimal.Parse(txtPurchasedQuantity.Text);
        inv_Item.PurchasedQuantityPrice = inv_Item.PricePerUnit * inv_Item.PurchasedQuantity;
        inv_Item.IssueReturedQuantity = 0;
        inv_Item.UtilizedQuantity = 0;
        inv_Item.LostQuantity =0;
        inv_Item.ExtraFieldQuantity1 = Decimal.Parse(txtPurchasedQuantity.Text);
        inv_Item.ExtraFieldQuantity2 = 0;
        inv_Item.ExtraFieldQuantity3 = 0;
        inv_Item.ExtraFieldQuantity4 = 0;
        inv_Item.ExtraFieldQuantity5 = 0;
        inv_Item.ExtraField1 = "";
        inv_Item.ExtraField2 = "";
        inv_Item.ExtraField3 = ddlProduct.SelectedValue;
        inv_Item.ExtraField4 = ddlFabricsTypeID.SelectedValue;
        inv_Item.ExtraField5 = ddlColor.SelectedValue;
        inv_Item.ExtraField6 = "";
        inv_Item.ExtraField7 = "";
        inv_Item.ExtraField8 = "";
        inv_Item.ExtraField9 = "";
        inv_Item.ExtraField10 = "";
        inv_Item.AddedBy =getLogin().LoginID;
        inv_Item.AddedDate = DateTime.Now;
        inv_Item.UpdatedBy = getLogin().LoginID;
        inv_Item.UpdatedDate = DateTime.Now;
        inv_Item.RowStatusID = 1;
        //int resutl = Inv_ItemManager.InsertInv_Item(inv_Item);
        PurchasedItem.Add(inv_Item);
        showInv_ItemGrid(PurchasedItem);
        btnClear_Click(this, new EventArgs());
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        List<Inv_Item> PurchasedItem = new List<Inv_Item>();
        PurchasedItem = loadFromTheGrid();

        foreach (Inv_Item inv_Item in PurchasedItem)
        {
            if (inv_Item.PurchaseID == int.Parse(hfPurchaseIDEdit.Value))
            {
                inv_Item.ItemName = ddlRawMaterial.SelectedItem.Text;
                inv_Item.ItemCode = decimal.Parse(txtItemCode.Text).ToString("000.00");
                inv_Item.RawMaterialID = Int32.Parse(ddlRawMaterial.SelectedValue.Split('@')[1]);
                inv_Item.StoreID = 2;
                inv_Item.QualityUnitID = Int32.Parse(ddlQualityUnit.SelectedValue);
                inv_Item.QualityUnitName = ddlQualityUnit.SelectedItem.Text;
                inv_Item.QualityValue = Decimal.Parse(txtQualityValue.Text);
                inv_Item.QuantityUnitID = Int32.Parse(ddlQuantityUnit.SelectedValue);
                inv_Item.QuantityUnitName = ddlQuantityUnit.SelectedItem.Text;
                inv_Item.PricePerUnit = Decimal.Parse(txtPricePerUnit.Text);
                inv_Item.PurchasedQuantity = Decimal.Parse(txtPurchasedQuantity.Text);
                inv_Item.PurchasedQuantityPrice = inv_Item.PricePerUnit * inv_Item.PurchasedQuantity;
                inv_Item.IssueReturedQuantity = 0;
                inv_Item.UtilizedQuantity = 0;
                inv_Item.LostQuantity = 0;
                inv_Item.ExtraFieldQuantity1 = Decimal.Parse(txtPurchasedQuantity.Text);
                inv_Item.ExtraFieldQuantity2 = 0;
                inv_Item.ExtraFieldQuantity3 = 0;
                inv_Item.ExtraFieldQuantity4 = 0;
                inv_Item.ExtraFieldQuantity5 = 0;
                inv_Item.ExtraField1 = "";
                inv_Item.ExtraField2 = "";
                inv_Item.ExtraField3 = ddlProduct.SelectedValue;
                inv_Item.ExtraField4 = ddlFabricsTypeID.SelectedValue;
                inv_Item.ExtraField5 = ddlColor.SelectedValue;
                inv_Item.ExtraField6 = "";
                inv_Item.ExtraField7 = "";
                inv_Item.ExtraField8 = "";
                inv_Item.ExtraField9 = "";
                inv_Item.ExtraField10 = "";
                inv_Item.AddedBy = getLogin().LoginID;
                inv_Item.AddedDate = DateTime.Now;
                inv_Item.UpdatedBy = getLogin().LoginID;
                inv_Item.UpdatedDate = DateTime.Now;
                inv_Item.RowStatusID = 1;
                break;
            }
        }

        showInv_ItemGrid(PurchasedItem);
        btnBackToAdd_Click(this,new EventArgs());

        
    }

    private List<Inv_Item> loadFromTheGrid()
    {
        List<Inv_Item> PurchasedItem = new List<Inv_Item>();
        int serial = 1;
        foreach (GridViewRow gvr in gvInv_Item.Rows)
        {
            Label lblPurchaseID = (Label)gvr.FindControl("lblPurchaseID");
            Label lblItemName = (Label)gvr.FindControl("lblItemName");

            HiddenField hfRawMaterialID = (HiddenField)gvr.FindControl("hfRawMaterialID");
            HiddenField hfQualityUnitID = (HiddenField)gvr.FindControl("hfQualityUnitID");
            HiddenField hfQuantityUnitID = (HiddenField)gvr.FindControl("hfQuantityUnitID");
            HiddenField hfExtraField3 = (HiddenField)gvr.FindControl("hfExtraField3");
            HiddenField hfExtraField4 = (HiddenField)gvr.FindControl("hfExtraField4");
            HiddenField hfExtraField5 = (HiddenField)gvr.FindControl("hfExtraField5");

            Label lblItemCode = (Label)gvr.FindControl("lblItemCode");
            Label lblQualityValue = (Label)gvr.FindControl("lblQualityValue");
            Label lblQualityUnitName = (Label)gvr.FindControl("lblQualityUnitName");
            Label lblPurchasedQuantity = (Label)gvr.FindControl("lblPurchasedQuantity");
            Label lblQuantityUnitName = (Label)gvr.FindControl("lblQuantityUnitName");
            Label lblPricePerUnit = (Label)gvr.FindControl("lblPricePerUnit");
            

            Inv_Item inv_Item = new Inv_Item();
            inv_Item.ItemName = lblItemName.Text;
            inv_Item.PurchaseID = serial++;
            inv_Item.ItemCode = lblItemCode.Text;
            inv_Item.RawMaterialID = Int32.Parse(hfRawMaterialID.Value);
            inv_Item.StoreID = 2;
            inv_Item.QualityUnitID = Int32.Parse(hfQualityUnitID.Value);
            inv_Item.QualityUnitName = lblQualityUnitName.Text;
            inv_Item.QualityValue = Decimal.Parse(lblQualityValue.Text);
            inv_Item.QuantityUnitID = Int32.Parse(hfQuantityUnitID.Value);
            inv_Item.QuantityUnitName =lblQuantityUnitName.Text;
            inv_Item.PricePerUnit = Decimal.Parse(lblPricePerUnit.Text);
            inv_Item.PurchasedQuantity = Decimal.Parse(lblPurchasedQuantity.Text);
            inv_Item.PurchasedQuantityPrice = inv_Item.PricePerUnit * inv_Item.PurchasedQuantity;
            inv_Item.IssueReturedQuantity = 0;
            inv_Item.UtilizedQuantity = 0;
            inv_Item.LostQuantity =0;
            inv_Item.ExtraFieldQuantity1 = inv_Item.PurchasedQuantity;
            inv_Item.ExtraFieldQuantity2 = 0;
            inv_Item.ExtraFieldQuantity3 = 0;
            inv_Item.ExtraFieldQuantity4 = 0;
            inv_Item.ExtraFieldQuantity5 = 0;
            inv_Item.ExtraField1 = "";
            inv_Item.ExtraField2 = "";
            inv_Item.ExtraField3 = hfExtraField3.Value;
            inv_Item.ExtraField4 = hfExtraField4.Value;
            inv_Item.ExtraField5 = hfExtraField5.Value;
            inv_Item.ExtraField6 = "";
            inv_Item.ExtraField7 = "";
            inv_Item.ExtraField8 = "";
            inv_Item.ExtraField9 = "";
            inv_Item.ExtraField10 = "";
            inv_Item.AddedBy =getLogin().LoginID;
            inv_Item.AddedDate = DateTime.Now;
            inv_Item.UpdatedBy = getLogin().LoginID;
            inv_Item.UpdatedDate = DateTime.Now;
            inv_Item.RowStatusID = 1;

            PurchasedItem.Add(inv_Item);
        }

        return PurchasedItem;
    }

    private void showInv_ItemGrid(List<Inv_Item> PurchasedItem)
    {
        PurchasedItem.Sort(delegate(Inv_Item p1, Inv_Item p2) { return p1.ItemCode.CompareTo(p2.ItemCode); });

        decimal totalQuantity = 0;
        decimal totalPrice = 0;
        int serial = 1;
        foreach (Inv_Item item in PurchasedItem)
        {
            item.PurchaseID = serial++;
            totalPrice += item.PurchasedQuantityPrice;
            totalQuantity += item.PurchasedQuantity;
        }


        gvInv_Item.DataSource = PurchasedItem;
        gvInv_Item.DataBind();

        
        Label lblPurchasedQuantityFooter= (Label)gvInv_Item.FooterRow.FindControl("lblPurchasedQuantityFooter");
        Label lblPurchasedQuantityPriceFooter= (Label)gvInv_Item.FooterRow.FindControl("lblPurchasedQuantityPriceFooter");

        lblPurchasedQuantityFooter.Text = totalQuantity.ToString("0,0.00");
        lblPurchasedQuantityPriceFooter.Text = totalPrice.ToString("0,0.00");

        decimal maxSerial = 0;
        foreach (Inv_Item inv_Item in PurchasedItem)
        {
            if (decimal.Parse(inv_Item.ItemCode) > maxSerial)
            {
                maxSerial = decimal.Parse(inv_Item.ItemCode);
            }
        }

        txtItemCode.Text = decimal.Parse((maxSerial + 1).ToString("0")).ToString("0.00");
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        //ddlRawMaterial.SelectedIndex = 0;
        //ddlQualityUnit.SelectedIndex = 0;
        //txtQualityValue.Text = "0";
        //ddlQuantityUnit.SelectedIndex = 0;
        //txtPricePerUnit.Text = "0";
        txtPurchasedQuantity.Text = "";
        
    }

    private void loadItem()
    {
        ListItem li = new ListItem("Select Item...", "0");
        ddlItem.Items.Add(li);

        List<Inv_Item> items = new List<Inv_Item>();
        items = Inv_ItemManager.GetAllInv_ItemsInStock();
        foreach (Inv_Item item in items)
        {
            ListItem li_item = new ListItem(item.ItemCode.ToString() + " - " + item.ItemName.ToString() + " {" + item.QualityValue.ToString() + " " + item.ExtraField1.ToString() + "}" + " [" + item.PurchasedQuantity.ToString() + " " + item.ExtraField2.ToString() + "]", item.Inv_ItemID.ToString());
            ddlItem.Items.Add(li_item);
        }
    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id= Convert.ToInt32(linkButton.CommandArgument);

        List<Inv_Item> itemsPurchased = loadFromTheGrid();
        List<Inv_Item> itemsAfterDelete = new List<Inv_Item>();

        int serial = 1;
        foreach (Inv_Item item in itemsPurchased)
        {
            if (item.PurchaseID != id)
            {
                item.PurchaseID = serial++;
                itemsAfterDelete.Add(item);
            }
        }

        showInv_ItemGrid(itemsAfterDelete);
    }

    protected void lbEdit_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id = Convert.ToInt32(linkButton.CommandArgument);

        List<Inv_Item> itemsPurchased = loadFromTheGrid();
        
        foreach (Inv_Item item in itemsPurchased)
        {
            if (item.PurchaseID == id)
            {
                ddlRawMaterial.SelectedValue = rbtnlRawmaterialsType.SelectedValue + "@" + item.RawMaterialID.ToString();
                txtPricePerUnit.Text = item.PricePerUnit.ToString("0.00");
                txtItemCode.Text = item.ItemCode;
                txtPurchasedQuantity.Text = item.PurchasedQuantity.ToString("0.00");
                txtQualityValue.Text = item.QualityValue.ToString("0.00");

                ddlQualityUnit.SelectedValue = item.QualityUnitID.ToString();
                ddlQuantityUnit.SelectedValue = item.QuantityUnitID.ToString();

                hfPurchaseIDEdit.Value = item.PurchaseID.ToString();
                btnUpdate.Visible = true;
                btnBackToAdd.Visible = true;
                btnAdd.Visible = false;
                break;
            }
        }
    }


    protected void btnBackToAdd_Click(object sender, EventArgs e)
    {
        txtPurchasedQuantity.Text = "";
        hfPurchaseIDEdit.Value = "";
        btnAdd.Visible = true;
        btnBackToAdd.Visible = false;
        btnUpdate.Visible = false;
    }

    private void showInv_ItemData()
    {
        
    }
    

    private void loadACC_ChartOfAccountLabel4()
    {

        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        aCC_ChartOfAccountLabel4s = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4sForJournalEntry();

        ddlSuppier.Items.Add(new ListItem("Select Supplier", "0"));
        ddlProduct.Items.Add(new ListItem("Any Product", "0"));

        foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
        {
            

            ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_HeadTypeID.ToString() + "@" + aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 2 //2=fabric
                || aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 9//9=accessories(productive)
                || aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 10)//10=accessories(non productive)
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_HeadTypeID + "@" + aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                if (aCC_ChartOfAccountLabel4.ExtraField1 != "")
                {
                    ddlRawMaterialAll.Items.Add(item);
                }
            }

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 6)
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlSuppier.Items.Add(item);
            }

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 3)
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlProduct.Items.Add(item);
            }


        }

        rbtnlRawmaterialsType_SelectedIndexChanged(this, new EventArgs());
    }

   
    private void loadQualityUnit()
    {
       
        List<Inv_QualityUnit> qualityUnits = new List<Inv_QualityUnit>();
        qualityUnits = Inv_QualityUnitManager.GetAllInv_QualityUnits();
        foreach (Inv_QualityUnit qualityUnit in qualityUnits)
        {
            ListItem item = new ListItem(qualityUnit.QualityUnitName.ToString(), qualityUnit.Inv_QualityUnitID.ToString());
            ddlQualityUnit.Items.Add(item);
        }
    }
    private void showAlartMessage(string message)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
             "err_msg",
             "alert('" + message + "');",
             true);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlSuppier.SelectedValue == "0")
        {
            showAlartMessage("Please select the Supplier");
            return;
        }

        List<Inv_Item> PurchasedItems = new List<Inv_Item>();
        PurchasedItems = loadFromTheGrid();
        if (PurchasedItems.Count == 0)
        {
            showAlartMessage("Please Add Items");
            return;
        }

        int JournalMasterID = 0;
        int pruchseID = 0;
        if (txtOldPurchaseID.Text == "")
        {
            //Insert Purchse
            Inv_Purchase inv_Purchase = new Inv_Purchase();

            inv_Purchase.PurchaseName = "";
            inv_Purchase.PurchseDate = DateTime.Parse(txtPurchseDate.Text);
            inv_Purchase.SuppierID = Int32.Parse(ddlSuppier.SelectedValue);
            inv_Purchase.InvoiceNo = txtInvoiceNo.Text;
            inv_Purchase.Particulars = txtParticulars.Text;
            inv_Purchase.IsPurchase = true;
            inv_Purchase.WorkSatationID = 2;//Int32.Parse(ddlWorkSatation.SelectedValue);
            inv_Purchase.ExtraField1 = rbtnlRawmaterialsType.SelectedValue;
            inv_Purchase.ExtraField2 = "";
            inv_Purchase.ExtraField3 = rbtnlPaymentType.SelectedValue;
            inv_Purchase.ExtraField4 = "";
            inv_Purchase.ExtraField5 = "";
            inv_Purchase.AddedBy = getLogin().LoginID;
            inv_Purchase.AddedDate = DateTime.Now;
            inv_Purchase.UpdatedBy = getLogin().LoginID;
            inv_Purchase.UpdatedDate = DateTime.Now;
            inv_Purchase.RowStatusID = 1;
            pruchseID = Inv_PurchaseManager.InsertInv_Purchase(inv_Purchase);

            //Journal Entry
            ACC_JournalMaster aCC_JournalMaster = new ACC_JournalMaster();

            aCC_JournalMaster.JournalMasterName = (rbtnlPaymentType.SelectedValue == "Cash" ? "2" : "3");//Journal Voucher
            aCC_JournalMaster.ExtraField1 = ddlSuppier.SelectedItem.Text;
            aCC_JournalMaster.ExtraField2 = "";
            aCC_JournalMaster.ExtraField3 = "";
            aCC_JournalMaster.Note = "Inventory Purchase-" + pruchseID.ToString();
            aCC_JournalMaster.JournalDate = DateTime.Parse(txtPurchseDate.Text);
            aCC_JournalMaster.AddedBy = getLogin().LoginID;
            aCC_JournalMaster.AddedDate = DateTime.Now;
            aCC_JournalMaster.UpdatedBy = getLogin().LoginID;
            aCC_JournalMaster.UpdatedDate = DateTime.Now;
            aCC_JournalMaster.RowStatusID = 1;

            JournalMasterID = ACC_JournalMasterManager.InsertACC_JournalMaster(aCC_JournalMaster);
            //insert each Item

            CommonManager.SQLExec("Update Inv_Purchase set ExtraField2='" + JournalMasterID.ToString() + "' where Inv_PurchaseID=" + pruchseID.ToString() + "; select 1");

        }
        else
        {
            string SQL = "select ACC_JournalMasterID from ACC_JournalMaster where Note='Inventory Purchase-"+txtOldPurchaseID.Text+"'";

            try
            {
                JournalMasterID = int.Parse(CommonManager.SQLExec(SQL).Tables[0].Rows[0][0].ToString());
                pruchseID = int.Parse(txtOldPurchaseID.Text);

            }
            catch (Exception ex)
            {
                showAlartMessage("Wrong Old PurchaseID");
                return;
            }
        }


        foreach (Inv_Item item in PurchasedItems)
        {
            item.PurchaseID = pruchseID;
            item.UpdatedBy = JournalMasterID;//emporary we pass the journal master ID
            item.ExtraField6 = rbtnlPaymentType.SelectedValue;
            item.Inv_ItemID= Inv_ItemManager.InsertInv_Item(item);

        }

        PurchasedItems = new List<Inv_Item>();
        showInv_ItemGrid(PurchasedItems);
        hlnkPurchasePrint.NavigateUrl = "PurchasePrint.aspx?PurchaseID=" + pruchseID.ToString();
        hlnkPurchasePrint.Visible = true;
    }

    private void loadQuantityUnit()
    {
        ListItem li = new ListItem("Select Unit...", "0");
        ddlQuantityUnit.Items.Add(li);

        List<Inv_QuantityUnit> quantityUnits = new List<Inv_QuantityUnit>();
        quantityUnits = Inv_QuantityUnitManager.GetAllInv_QuantityUnits();
        foreach (Inv_QuantityUnit quantityUnit in quantityUnits)
        {
            ListItem item = new ListItem(quantityUnit.QuantityUnitName.ToString(), quantityUnit.Inv_QuantityUnitID.ToString());
            ddlQuantityUnit.Items.Add(item);
        }
    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        /*
         inv_IssueDetail.ExtraField1 = ddlItem.SelectedItem.Text.Split(' ')[0]; //Item COde
        inv_IssueDetail.ExtraField2 = ddlItem.SelectedItem.Text.Split('{')[0].Trim().Replace(inv_IssueDetail.ExtraField1 +" - ","");//item Name
        inv_IssueDetail.ExtraField3 = ddlItem.SelectedItem.Text.Split('{')[1].Split('}')[0];//quality
        inv_IssueDetail.ExtraField4 = Request.QueryString["IsNonProduction"] != null ?"":ddlProduct.SelectedItem.Text;//productName
        inv_IssueDetail.ExtraField5 = Inv_ItemManager.GetInv_ItemByID(Int32.Parse(ddlItem.SelectedValue)).PricePerUnit.ToString("0,0.00");//Price Per unit
        
         */
        btnClear_Click(this,new EventArgs());
        txtItemCode.Text = ddlItem.SelectedItem.Text.Split(' ')[0]; //Item COde
        txtQualityValue.Text = ddlItem.SelectedItem.Text.Split('{')[1].Split('}')[0];
        txtPurchasedQuantity.Text = "";
    }
    protected void rbtnlRawmaterialsType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlRawMaterial.Items.Clear();
        ddlRawMaterial.Items.Add(new ListItem("Select "+rbtnlRawmaterialsType.SelectedItem.Text, "0"));

        foreach (ListItem item in ddlRawMaterialAll.Items)
        {
            if (item.Value.Split('@')[0] == rbtnlRawmaterialsType.SelectedValue)
            {
                ddlRawMaterial.Items.Add(item);
            }
        }


        trQuality.Visible = false;
        trProduct.Visible = false;
        trFabricsType.Visible = false;
        trFabricsColor.Visible = false;
        if (rbtnlRawmaterialsType.SelectedValue == "2")
        {
            trQuality.Visible = true;
            trProduct.Visible = true;
            trFabricsType.Visible = true;
            trFabricsColor.Visible = true;
        }
    }

    protected void btnLoadNewlyAddedRawmaterials_Click(object sender, EventArgs e)
    {
        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        aCC_ChartOfAccountLabel4s = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4sForJournalEntry();
        
        foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
        {


            ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_HeadTypeID.ToString() + "@" + aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 2 //2=fabric
                || aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 9//9=accessories(productive)
                || aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 10)//10=accessories(non productive)
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_HeadTypeID + "@" + aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                if (aCC_ChartOfAccountLabel4.ExtraField1 != "")
                {
                    ddlRawMaterialAll.Items.Add(item);
                }
            }

        }

        rbtnlRawmaterialsType_SelectedIndexChanged(this, new EventArgs());
    }
}
