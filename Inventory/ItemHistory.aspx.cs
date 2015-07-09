using System;
using System.Collections;
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
using System.Collections.Generic;

public partial class AdminInv_ItemTransactionDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadQualityUnit();
            loadQuantityUnit();
            showInv_ItemTransactionGrid();
        }
    }

    private void loadQualityUnit()
    {
        ListItem li = new ListItem("Select QualityUnit...", "0");
        ddlQualityUnit.Items.Add(li);

        List<Inv_QualityUnit> qualityUnits = new List<Inv_QualityUnit>();
        qualityUnits = Inv_QualityUnitManager.GetAllInv_QualityUnits();
        foreach (Inv_QualityUnit qualityUnit in qualityUnits)
        {
            ListItem item = new ListItem(qualityUnit.QualityUnitName.ToString(), qualityUnit.Inv_QualityUnitID.ToString());
            ddlQualityUnit.Items.Add(item);
        }
    }
    private void loadQuantityUnit()
    {
        ListItem li = new ListItem("Select QuantityUnit...", "0");
        ddlQuantityUnit.Items.Add(li);

        List<Inv_QuantityUnit> quantityUnits = new List<Inv_QuantityUnit>();
        quantityUnits = Inv_QuantityUnitManager.GetAllInv_QuantityUnits();
        foreach (Inv_QuantityUnit quantityUnit in quantityUnits)
        {
            ListItem item = new ListItem(quantityUnit.QuantityUnitName.ToString(), quantityUnit.Inv_QuantityUnitID.ToString());
            ddlQuantityUnit.Items.Add(item);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminInv_ItemTransactionInsertUpdate.aspx?inv_ItemTransactionID=0");
    }
    protected void lbSelect_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id;
        id = Convert.ToInt32(linkButton.CommandArgument);
        Response.Redirect("AdminInv_ItemTransactionInsertUpdate.aspx?inv_ItemTransactionID=" + id);
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = Inv_ItemTransactionManager.DeleteInv_ItemTransaction(Convert.ToInt32(linkButton.CommandArgument));
        showInv_ItemTransactionGrid();
    }

    private void showInv_ItemTransactionGrid()
    {
        int itemID=0;

        List<Inv_ItemTransaction> Inv_ItemTransactionHostory = new List<Inv_ItemTransaction>();
        if (Request.QueryString["ItemID"] != null)
        {
            itemID = int.Parse(Request.QueryString["ItemID"] );
            Inv_ItemTransactionHostory = Inv_ItemTransactionManager.GetAllInv_ItemTransactionsByItemID(int.Parse(Request.QueryString["ItemID"]));
            //gvInv_ItemTransaction.DataSource = Inv_ItemTransactionHostory;
        }
        else if (Request.QueryString["ItemCode"] != null)
        {
            Inv_ItemTransactionHostory=Inv_ItemTransactionManager.GetAllInv_ItemTransactionsByItemCode(Request.QueryString["ItemCode"]);
            //gvInv_ItemTransaction.DataSource = Inv_ItemTransactionHostory;
            itemID=Inv_ItemTransactionHostory[0].ItemID;
        }
        Inv_ItemTransactionHostory = assignWorkStationName(Inv_ItemTransactionHostory);
        gvInv_ItemTransaction.DataSource = Inv_ItemTransactionHostory;
        gvInv_ItemTransaction.DataBind();


        //item details
        List<Inv_Item> items = new List<Inv_Item>();
        items.Add(Inv_ItemManager.GetInv_ItemByID(itemID));

        gvInv_Item.DataSource = items;
        gvInv_Item.DataBind();

        hfItemID.Value = itemID.ToString();
    }

    private List<Inv_ItemTransaction> assignWorkStationName(List<Inv_ItemTransaction> Inv_ItemTransactionHostory)
    {
        DataSet ds = CommonManager.SQLExec("Select ACC_ChartOfAccountLabel4ID,ChartOfAccountLabel4Text from ACC_ChartOfAccountLabel4 where ACC_HeadTypeID=1");

        foreach (Inv_ItemTransaction item in Inv_ItemTransactionHostory)
        {
            try
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["ACC_ChartOfAccountLabel4ID"].ToString() == item.ExtraField4)
                    {
                        item.ExtraField4 = dr["ChartOfAccountLabel4Text"].ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
              
            }
            
        }

        return Inv_ItemTransactionHostory; 
    }

    private void showAlartMessage(string message)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
             "err_msg",
             "alert('" + message + "');",
             true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        decimal PurchasedQuantity = 0;
        decimal IssuedQty = 0;
        decimal PurchaseID = 0;
        decimal pricePerUnit = 0;
        string addedDate = "";
        foreach (GridViewRow gvr in gvInv_Item.Rows)
        {
            Label lblPurchasedQuantity = (Label)gvr.FindControl("lblPurchasedQuantity");
            Label lblPricePerUnit = (Label)gvr.FindControl("lblPricePerUnit");
            Label lblIssuedQty = (Label)gvr.FindControl("lblExtraFieldQuantity5");
            HiddenField hfPurchaseID=(HiddenField)gvr.FindControl("hfPurchaseID");
            
            PurchaseID = decimal.Parse(hfPurchaseID.Value);
            addedDate = ((Label)gvr.FindControl("lblAddedDate")).Text;
            PurchasedQuantity = decimal.Parse(lblPurchasedQuantity.Text);
            pricePerUnit = decimal.Parse(lblPricePerUnit.Text);
            IssuedQty = decimal.Parse(lblIssuedQty.Text);
        }

        if (txtQuantity.Text != "")
        {
            if ((decimal.Parse(txtQuantity.Text) + PurchasedQuantity) < IssuedQty)
            {
                showAlartMessage("Please delete the issue then update the QTY");
                return;
            }
        }

        string SQL = "";
        string SQLJournal = "";
        if ((txtUnitePrice.Text != "")
            || txtQualityValue.Text != ""
            || txtQuantity.Text != ""
            || ddlQualityUnit.SelectedValue != "0"
            || ddlQuantityUnit.SelectedValue != "0"
            )
        {
           // SQL += "update Inv_Item set UpdatedDate=GETDATE(),ExtraField7+='<br/>(" + DateTime.Now.ToString("dd-MMM-yyyy-hh:mm-tt") + ")-" + txtNote.Text + "' ";
            SQL += "update Inv_Item set UpdatedDate=GETDATE() ";
        }

        if (txtQualityValue.Text != "")
        {
            SQL += " , QualityValue="+txtQualityValue.Text;
        }

        

        if (txtUnitePrice.Text != "" && txtQuantity.Text == "")
        {
            SQL += " , PricePerUnit=" + txtUnitePrice.Text;
            
            SQLJournal += @"declare @CashID int;set @CashID = (select ACC_JournalDetail.ACC_JournalDetailID from ACC_JournalDetail where Debit=0 and ExtraField1='" + hfItemID.Value + @"' and JournalMasterID= ( select ACC_JournalMasterID from ACC_JournalMaster where Note ='Inventory Purchase-" + PurchaseID + @"'))
                        declare @NotCashID int;set @NotCashID = (select ACC_JournalDetail.ACC_JournalDetailID from ACC_JournalDetail where Credit=0 and ExtraField1='" + hfItemID.Value + @"' and JournalMasterID= ( select ACC_JournalMasterID from ACC_JournalMaster where Note ='Inventory Purchase-" + PurchaseID + @"'))";

            SQLJournal += "Update ACC_JournalDetail set UpdatedDate=GETDATE(), Credit=" + (decimal.Parse(txtUnitePrice.Text) * PurchasedQuantity).ToString("0.00") + " where ACC_JournalDetailID=@CashID;";
            SQLJournal += "Update ACC_JournalDetail set UpdatedDate=GETDATE(), Debit=" + (decimal.Parse(txtUnitePrice.Text) * PurchasedQuantity).ToString("0.00") + " where   ACC_JournalDetailID=@NotCashID;";
        }
        else if (txtUnitePrice.Text != "" && txtQuantity.Text != "")
        {
            SQL += " , PricePerUnit=" + txtUnitePrice.Text + " , PurchasedQuantity +=" + txtQuantity.Text + " , ExtraFieldQuantity1 +=" + txtQuantity.Text;

            SQLJournal += @"declare @CashID int;set @CashID = (select ACC_JournalDetail.ACC_JournalDetailID from ACC_JournalDetail where Debit=0 and ExtraField1='" + hfItemID.Value + @"' and JournalMasterID= ( select ACC_JournalMasterID from ACC_JournalMaster where Note ='Inventory Purchase-" + PurchaseID + @"'))
                        declare @NotCashID int;set @NotCashID = (select ACC_JournalDetail.ACC_JournalDetailID from ACC_JournalDetail where Credit=0 and ExtraField1='" + hfItemID.Value + @"' and JournalMasterID= ( select ACC_JournalMasterID from ACC_JournalMaster where Note ='Inventory Purchase-" + PurchaseID + @"'))";


            SQLJournal += "Update ACC_JournalDetail set UpdatedDate=GETDATE(), Credit=" + (decimal.Parse(txtUnitePrice.Text) * (decimal.Parse(txtQuantity.Text) + PurchasedQuantity)).ToString("0.00") + " where ACC_JournalDetailID=@CashID;";
            SQLJournal += "Update ACC_JournalDetail set UpdatedDate=GETDATE(), Debit=" + (decimal.Parse(txtUnitePrice.Text) * (decimal.Parse(txtQuantity.Text) + PurchasedQuantity)).ToString("0.00") + " where   ACC_JournalDetailID=@NotCashID;";

            SQLJournal += "update Inv_ItemTransaction set Quantity+=" + txtQuantity.Text + " where ItemID=" + hfItemID.Value + " and ReferenceID=" + PurchaseID.ToString("0.00");
        }
        else if (txtUnitePrice.Text == "" && txtQuantity.Text != "")
        {
            SQL += " , PurchasedQuantity +=" + txtQuantity.Text + " , ExtraFieldQuantity1 +=" + txtQuantity.Text;

            SQLJournal += @"declare @CashID int;set @CashID = (select ACC_JournalDetail.ACC_JournalDetailID from ACC_JournalDetail where Debit=0 and ExtraField1='" + hfItemID.Value + @"' and JournalMasterID= ( select ACC_JournalMasterID from ACC_JournalMaster where Note ='Inventory Purchase-" + PurchaseID + @"'))
                        declare @NotCashID int;set @NotCashID = (select ACC_JournalDetail.ACC_JournalDetailID from ACC_JournalDetail where Credit=0 and ExtraField1='" + hfItemID.Value + @"' and JournalMasterID= ( select ACC_JournalMasterID from ACC_JournalMaster where Note ='Inventory Purchase-" + PurchaseID + @"'))";

            SQLJournal += "Update ACC_JournalDetail set UpdatedDate=GETDATE(), Credit+=" + (pricePerUnit * decimal.Parse(txtQuantity.Text)).ToString("0.00") + " where ACC_JournalDetailID=@CashID;";
            SQLJournal += "Update ACC_JournalDetail set UpdatedDate=GETDATE(), Debit+=" + (pricePerUnit * decimal.Parse(txtQuantity.Text)).ToString("0.00") + " where  ACC_JournalDetailID=@NotCashID;";

            SQLJournal += "update Inv_ItemTransaction set Quantity+=" + txtQuantity.Text + " where ItemID=" + hfItemID.Value + " and ReferenceID=" + PurchaseID.ToString("0.00") + ";";

        }


        if (ddlQualityUnit.SelectedValue != "0")
        {
            SQL += " , QualityUnitID=" + ddlQualityUnit.SelectedValue;
        }

        if (ddlQuantityUnit.SelectedValue != "0")
        {
            SQL += " , QuantityUnitID=" + ddlQuantityUnit.SelectedValue;
        }

        if (SQL != "")
        {
            CommonManager.SQLExec(SQLJournal+SQL + " where Inv_ItemID=" + hfItemID.Value);
        }

        txtUnitePrice.Text = "";
        txtQualityValue.Text = "";
        ddlQualityUnit.SelectedValue = "0";
        ddlQuantityUnit.SelectedValue = "0";
        txtNote.Text = "";
        showInv_ItemTransactionGrid();
    }
}
