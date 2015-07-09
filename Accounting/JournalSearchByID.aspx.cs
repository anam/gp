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

public partial class AdminACC_JournalDetailInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initialLoad();
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

    private void initialLoad()
    {
        if (hfLoginID.Value == "")
        {
            hfLoginID.Value = getLogin().LoginID.ToString();
        }

        btnDeleteJournal.Visible = ButtonManager.GetAllButtonsByPageURLnUserIDnButtonName("btnDelete", HttpContext.Current.Request.Url.AbsoluteUri, getLogin().LoginID.ToString());
    }

   
  

    protected void btnDeleteJournal_Click(object sender, EventArgs e)
    {
        ACC_JournalMasterManager.DeleteACC_JournalMaster(int.Parse(txtJournalMasterID.Text));
        txtJournalMasterID.Text = "";
        btnTransactionSummary_Click(this, new EventArgs());
    }

    protected void btnTransactionSummary_Click(object sender, EventArgs e)
    {
         
        if (txtJournalMasterID.Text != "")
        {
            ltrlJournalPreview.Text = "<iframe height='800px' frameborder='0' width='900px' src='Voucherprint.aspx?JournalMasterID=" + txtJournalMasterID.Text + "' allowfullscreen=''></iframe><hr/>";
        }
        else
        {
            ltrlJournalPreview.Text = "";
        }
    }

    protected void btnSearchByPurchaseID_Click(object sender, EventArgs e)
    {
        if (txtPurchaseID.Text != "")
        {
            string SQL = "select top 1 ACC_JournalMasterID from ACC_JournalMaster where Note='Inventory Purchase-" + txtPurchaseID.Text + "' ";


            ltrlJournalPreview.Text = "<iframe height='800px' frameborder='0' width='900px' src='Voucherprint.aspx?JournalMasterID=" + CommonManager.SQLExec(SQL).Tables[0].Rows[0][0].ToString() + "' allowfullscreen=''></iframe><hr/>";
        }
        else
        {
            ltrlJournalPreview.Text = "";
        }
    }

    protected void btnSearchByPosPurchaseID_Click(object sender, EventArgs e)
    {
        if (txtPosPurchaseID.Text != "")
        {
            string SQL = "select top 1 ACC_JournalMasterID from ACC_JournalMaster where Note='Product Purchase-" + txtPosPurchaseID.Text + "' ";


            ltrlJournalPreview.Text = "<iframe height='800px' frameborder='0' width='900px' src='Voucherprint.aspx?JournalMasterID=" + CommonManager.SQLExec(SQL).Tables[0].Rows[0][0].ToString() + "' allowfullscreen=''></iframe><hr/>";
        }
        else
        {
            ltrlJournalPreview.Text = "";
        }
    }

    protected void btnSearchByIssueID_Click(object sender, EventArgs e)
    {
        if (txtIssueID.Text != "")
        {
            string SQL = "select top 1 ACC_JournalMasterID from ACC_JournalMaster where Note='Inventory Issue-" + txtIssueID.Text + "' ";


            ltrlJournalPreview.Text = "<iframe height='800px' frameborder='0' width='900px' src='Voucherprint.aspx?JournalMasterID=" + CommonManager.SQLExec(SQL).Tables[0].Rows[0][0].ToString() + "' allowfullscreen=''></iframe><hr/>";
        }
        else
        {
            ltrlJournalPreview.Text = "";
        }
    }
    protected void btnSearchByUtilizationID_Click(object sender, EventArgs e)
    {
        if (txtUtilizationID.Text != "")
        {
            string SQL = "select top 1 ACC_JournalMasterID from ACC_JournalMaster where Note='Utilization ID = " + txtUtilizationID.Text + "' ";


            ltrlJournalPreview.Text = "<iframe height='800px' frameborder='0' width='900px' src='Voucherprint.aspx?JournalMasterID=" + CommonManager.SQLExec(SQL).Tables[0].Rows[0][0].ToString() + "' allowfullscreen=''></iframe><hr/>";
        }
        else
        {
            ltrlJournalPreview.Text = "";
        }
    }
    protected void btnJournalEdit_Click(object sender, EventArgs e)
    {
        lblEditLink.Text = "<a target='_blank' href='JournalModification.aspx?JournalMasterID=" + txtJournalMasterID.Text + "' >Edit Link</a>";

    }
    protected void btnReGenerateJournal_Click(object sender, EventArgs e)
    {
        if (txtPurchaseID.Text != "")
        {
            string SQL = "select top 1 ACC_JournalMasterID from ACC_JournalMaster where Note='Inventory Purchase-" + txtPurchaseID.Text + "' ";
            string JournalMasterID= CommonManager.SQLExec(SQL).Tables[0].Rows[0][0].ToString() ;
            CommonManager.SQLExec("update ACC_JournalDetail set RowStatusID=3,UpdatedDate=GETDATE() where [JournalMasterID]=" + JournalMasterID);
            int PurchaseID = int.Parse(txtPurchaseID.Text);

            Inv_Purchase purchase = Inv_PurchaseManager.GetInv_PurchaseByID(PurchaseID);
            /*
            lblPurchaseDate.Text = purchase.PurchseDate.ToString("dd-MMM-yyyy");
            lblPurchaseID.Text = purchase.Inv_PurchaseID.ToString() + (purchase.RowStatusID == 3 ? "<b style='color:red;'>(Deleted)</b>" : "");
            lblPaymentType.Text = purchase.ExtraField3;
            lblInvoiceNo.Text = purchase.InvoiceNo;
            lblSupplierName.Text = purchase.SupplierName;
            lblParticulars.Text = purchase.Particulars;
            */
            //Item Info
            List<Inv_Item> items = new List<Inv_Item>();
            items = Inv_ItemManager.GetAllInv_ItemsByPurchaseID(PurchaseID);
            foreach (Inv_Item item in items)
            {
                string sql = @"
Declare @PurchaseID int
set @PurchaseID=" + PurchaseID + @";
Declare @JournalMasterID int
set @JournalMasterID=" + JournalMasterID + @";
Declare @AddedBy int
set @AddedBy=" + 1 + @";
Declare @UpdatedBy int
set @UpdatedBy=" + 1 + @";
Declare @AddedDate DATETIME
set @AddedDate=GETDATE();
Declare @UpdatedDate DATETIME
set @UpdatedDate=GETDATE();
Declare @RowStatusID int
set @RowStatusID=" + 1 + @";

Declare @PurchasedQuantity DECIMAL(10, 2)
set @PurchasedQuantity=" + item.PurchasedQuantity + @";
Declare @PricePerUnit DECIMAL(18, 6)
set @PricePerUnit=" + item.PricePerUnit + @";
Declare @Inv_ItemID int
set @Inv_ItemID=" + item.Inv_ItemID + @";

--supplyer payable entry
INSERT INTO [ACC_JournalDetail]
           ([JournalMasterID]
           ,[ACC_ChartOfAccountLabel4ID]
           ,[ACC_ChartOfAccountLabel3ID]
           ,[WorkStation]
           ,[Debit]
           ,[Credit]
           ,[ExtraField3]
           ,[ExtraField2]
           ,[ExtraField1]
           ,[AddedBy]
           ,[AddedDate]
           ,[UpdatedBy]
           ,[UpdatedDate]
           ,[RowStatusID])
     VALUES
           (@JournalMasterID
           ,(select Inv_Purchase.SuppierID from Inv_Purchase where Inv_PurchaseID=@PurchaseID)
           ,43
           ,1
           ,0
           ,(@PurchasedQuantity * @PricePerUnit)
           ,''
           ,''
           ,CAST(@Inv_ItemID as nvarchar(256)),
    @AddedBy,
    @AddedDate,
    @AddedBy,
    @UpdatedDate,
    @RowStatusID);

Declare @RawMaterialID int
set @RawMaterialID=(select Inv_Item.RawMaterialID from Inv_Item where Inv_ItemID=@Inv_ItemID)
Declare @TypeID int
set @TypeID = (select ACC_HeadTypeID from  ACC_ChartOfAccountLabel4 where ACC_ChartOfAccountLabel4ID=@RawMaterialID)
IF @TypeID = 2 --Fabric Entry
BEGIN
INSERT INTO [ACC_JournalDetail]
           ([JournalMasterID]
           ,[ACC_ChartOfAccountLabel4ID]
           ,[ACC_ChartOfAccountLabel3ID]
           ,[WorkStation]
           ,[Debit]
           ,[Credit]
           ,[ExtraField3]
           ,[ExtraField2]
           ,[ExtraField1]
           ,[AddedBy]
           ,[AddedDate]
           ,[UpdatedBy]
           ,[UpdatedDate]
           ,[RowStatusID])
     VALUES
           (@JournalMasterID
           ,@RawMaterialID
           ,5
           ,1
           ,(@PurchasedQuantity * @PricePerUnit)
           ,0
           ,''
           ,''
           ,CAST(@Inv_ItemID as nvarchar(256)),
			@AddedBy,
			@AddedDate,
			@AddedBy,
			@UpdatedDate,
			@RowStatusID);
END
Else IF @TypeID = 9 --Factory accessories
BEGIN
INSERT INTO [ACC_JournalDetail]
           ([JournalMasterID]
           ,[ACC_ChartOfAccountLabel4ID]
           ,[ACC_ChartOfAccountLabel3ID]
           ,[WorkStation]
           ,[Debit]
           ,[Credit]
           ,[ExtraField3]
           ,[ExtraField2]
           ,[ExtraField1]
           ,[AddedBy]
           ,[AddedDate]
           ,[UpdatedBy]
           ,[UpdatedDate]
           ,[RowStatusID])
     VALUES
           (@JournalMasterID
           ,@RawMaterialID
           ,7
           ,1
           ,(@PurchasedQuantity * @PricePerUnit)
           ,0
           ,''
           ,''
           ,CAST(@Inv_ItemID as nvarchar(256)),
			@AddedBy,
			@AddedDate,
			@AddedBy,
			@UpdatedDate,
			@RowStatusID);
END
Else IF @TypeID = 10 --Non-productive accessories
BEGIN
INSERT INTO [ACC_JournalDetail]
           ([JournalMasterID]
           ,[ACC_ChartOfAccountLabel4ID]
           ,[ACC_ChartOfAccountLabel3ID]
           ,[WorkStation]
           ,[Debit]
           ,[Credit]
           ,[ExtraField3]
           ,[ExtraField2]
           ,[ExtraField1]
           ,[AddedBy]
           ,[AddedDate]
           ,[UpdatedBy]
           ,[UpdatedDate]
           ,[RowStatusID])
     VALUES
           (@JournalMasterID
           ,@RawMaterialID
           ,(select top 1 CAST(ExtraField3 as int) from ACC_ChartOfAccountLabel4 where ACC_ChartOfAccountLabel4ID=@RawMaterialID)
           ,1
           ,(@PurchasedQuantity * @PricePerUnit)
           ,0
           ,''
           ,''
           ,CAST(@Inv_ItemID as nvarchar(256)),
			@AddedBy,
			@AddedDate,
			@AddedBy,
			@UpdatedDate,
			@RowStatusID);
END
";
                CommonManager.SQLExec(sql);
            }
        }        
    }
}
