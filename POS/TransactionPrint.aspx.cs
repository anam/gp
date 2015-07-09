using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

public partial class Inventory_PurchasePrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadLoginInHiddenField();
            initialLoad();
            loadData();
        }
    }
    private void showAlartMessage(string message)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
             "err_msg",
             "alert('" + message + "');",
             true);
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
    private void loadData()
    {
        int Pos_TransactionMasterID = int.Parse(Request.QueryString["Pos_TransactionMasterID"]);
        
        Pos_TransactionMaster pos_TransactionMaster = Pos_TransactionMasterManager.GetPos_TransactionMasterByID(Pos_TransactionMasterID);

        lblPurchaseDate.Text = pos_TransactionMaster.TransactionDate.ToString("dd-MMM-yyyy");
        lblParticulars.Text = pos_TransactionMaster.Particulars;
        lblTransactionID.Text = pos_TransactionMaster.TransactionID.ToString() + (pos_TransactionMaster.RowStatusID ==1? (Request.QueryString["Delete"] != null ? " (<a href='TransactionDelete.aspx?Pos_TransactionMasterID=" + Request.QueryString["Pos_TransactionMasterID"] + "'>X</a>)" : ""):"<b style='color:red;'>(Deleted)</b>");
        lblRefferenceIDs.Text = pos_TransactionMaster.Record;
        lblToOrFromName.Text = pos_TransactionMaster.ToOrFromName;

        if ((pos_TransactionMaster.ToOrFromID.ToString() != getLogin().ExtraField5
            &&
            pos_TransactionMaster.WorkSatationID.ToString() != getLogin().ExtraField5
            ) && getLogin().ExtraField5 != "1")
        {
            showAlartMessage("Your are not allowed to View this Transaction");
            btnReceived.Visible = false;
            return;

        }
        if (pos_TransactionMaster.ExtraField5 == "Pending")
        {
            lblExtraField5.Text = pos_TransactionMaster.ExtraField5;
            if (pos_TransactionMaster.Pos_TransactionTypeID == 12 
                &&
                getLogin().ExtraField5 == pos_TransactionMaster.WorkSatationID.ToString()
                )
            {
                lblExtraField5.Visible = true;
                btnReceived.Visible = true;
            }
            else
            if (getLogin().ExtraField5 == pos_TransactionMaster.ToOrFromID.ToString())
            {
                lblExtraField5.Visible = true;
                btnReceived.Visible = true;
            }
            else
            {
                lblExtraField5.Visible = false;
                btnReceived.Visible = false;
            }
        }
        else
        {
            lblExtraField5.Visible = false;
            btnReceived.Visible = false;
        }


        string NoOfcollum = "9";

        trRefference.Visible = false;
        trToOrFrom.Visible = false;

        lblVoucherType.Text = Pos_TransactionTypeManager.GetPos_TransactionTypeByID(pos_TransactionMaster.Pos_TransactionTypeID).TransactionTypeName;

        switch (pos_TransactionMaster.Pos_TransactionTypeID)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;

            case 4:
                break;

            case 5:
                break;

            case 6:
                break;

            case 7:
                break;

            case 8:
                break;

            case 9:
                trToOrFrom.Visible = true;
                break;

            case 10:
                trToOrFrom.Visible = true;
                break;

            case 11: //Branch Transfer
                trToOrFrom.Visible = true;
                break;

            case 12: //Branch Transfer
                trToOrFrom.Visible = true;
                break;

            case 25:
                trToOrFrom.Visible = true;
                NoOfcollum = "10";
                break;
            default:
                break;
        }

        //Item Info
        List<Pos_Product> items = new List<Pos_Product>();
        items = Pos_ProductManager.GetAllPos_ProductsByTrasactionMasterID(Pos_TransactionMasterID);

        string htmlTable = @" <table id='itemList_tbl' style='border:1px solid black;width:100%;' cellpadding='0' cellspacing='0'>
                        <tr id='tableHeader'>
                            <td  style='border-left:0px;'>S/L</td>
                            <td>Product Code</td>
                            <td>Product Name</td>
                            <td>Status</td>
                            <td>Quantity</td>
                            <td>Unit</td>
                            " + (NoOfcollum == "10" ? "<td>Cost</td><td>Total Cost</td>" : "")
                             + @"<td>Sale Price</td>
                            <td>VAT %</td>
                            <td>Amount</td>
                        </tr>";
        int lastRawMaterialID = 0;
        decimal Total = 0;
        decimal Subtotal = 0;
        decimal TotalAmount = 0;
        decimal SubtotalAmount = 0;
        decimal TotalAmountCost = 0;
        decimal SubtotalAmountCost = 0;
        int serialNo=1;
        foreach (Pos_Product item in items)
        {
            if (item.RowStatusID != 1) continue;
            if (lastRawMaterialID != 0 && lastRawMaterialID != item.ProductID)
            {
                htmlTable += @"<tr class='subtotalRow'>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>Sub Total</td>
                            <td>" + Subtotal.ToString("0,0.000000") + @"</td>
                            <td>&nbsp;</td>
                            " + (NoOfcollum == "10" ? "<td>&nbsp;</td><td>" + SubtotalAmountCost.ToString("0,0.000000") + @"</td>" : "")
                             + @"<td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>" + SubtotalAmount.ToString("0,0.000000") + @"</td>
                        </tr>";

                Subtotal = 0;
                SubtotalAmount = 0;
                SubtotalAmountCost = 0;
            }

            if (lastRawMaterialID != item.ProductID)
            {
                lastRawMaterialID = item.ProductID;
                htmlTable += @"<tr>
                            <td colspan='" + NoOfcollum + @"' style='padding-left:50px; border-top:1px solid black;border-bottom:1px solid black;font-weight:bold;'>
                                Item: " + item.ExtraField2 +@"
                            </td>
                        </tr>";
            }

            htmlTable += @"<tr class='itemCss'>
                            <td  style='border-left:0px;'>" + (serialNo++).ToString() + (item.RowStatusID == 1 ? (Request.QueryString["Delete"] != null ? (pos_TransactionMaster.Pos_TransactionTypeID==13?"": " (<a href='TransactionDelete.aspx?Pos_TransactionID=" + item.ExtraField10 + "&Pos_TransactionMasterID=" + Request.QueryString["Pos_TransactionMasterID"] + "'>X</a>)") : "") : "<b style='color:red;'>(Deleted)</b>") + @"</td>
                            <td>" +item.BarCode+@"</td>
                            <td>" + item.ExtraField2 + @"</td>
                            <td>" + item.ExtraField8 + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(item.ExtraField1).ToString("0,0.000000")+@"</td>
                            <td >" + item.ExtraField3 + @"</td>
                            " + (NoOfcollum == "10" ? "<td style='text-align:right;'>" + decimal.Parse(item.ExtraField4).ToString("0,0.000000") + @"</td><td>" + (decimal.Parse(item.ExtraField1) * decimal.Parse(item.ExtraField4)).ToString("0,0.000000") + @"</td>" : "")
                             + @"<td style='text-align:right;'>" + item.SalePrice.ToString("0,0.000000") + @"</td>
                            <td style='text-align:right;'>" + item.VatPercentage.ToString("0,0.000000") + @"</td>
                            <td style='text-align:right;'>" + ((decimal.Parse(item.ExtraField1) * item.SalePrice) + ((decimal.Parse(item.ExtraField1) * item.SalePrice)*item.VatPercentage/100)).ToString("0,0.000000") + @"</td>
                        </tr>";

            Subtotal += decimal.Parse(item.ExtraField1);
            SubtotalAmount += ((decimal.Parse(item.ExtraField1) * item.SalePrice) + ((decimal.Parse(item.ExtraField1) * item.SalePrice) * item.VatPercentage / 100));
            SubtotalAmountCost += decimal.Parse(item.ExtraField1) * decimal.Parse(item.ExtraField4);

            Total += decimal.Parse(item.ExtraField1);
            TotalAmount += ((decimal.Parse(item.ExtraField1) * item.SalePrice) + ((decimal.Parse(item.ExtraField1) * item.SalePrice) * item.VatPercentage / 100));
            TotalAmountCost += decimal.Parse(item.ExtraField1) * decimal.Parse(item.ExtraField4);
        }

        htmlTable += @"<tr class='subtotalRow'>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>Sub Total</td>
                        <td>" + Subtotal.ToString("0,0.000000") + @"</td>
                        <td>&nbsp;</td>
                        " + (NoOfcollum == "10" ? "<td>&nbsp;</td><td>" + SubtotalAmountCost.ToString("0,0.000000") + @"</td>" : "")
                             + @"<td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>" + SubtotalAmount.ToString("0,0.000000") + @"</td>
                    </tr>";

        htmlTable += @"<tr id='lastRow'>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>Grand Total</td>
                        <td>" + Total.ToString("0,0.000000") + @"</td>
                        <td>&nbsp;</td>
                        " + (NoOfcollum == "10" ? "<td>&nbsp;</td><td>" + TotalAmountCost.ToString("0,0.000000") + @"</td>" : "")
                             + @"<td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>" + TotalAmount.ToString("0,0.000000") + @"</td>
                    </tr></table>";

        lblItemList.Text = htmlTable;
        
    }

    private void initialLoad()
    {
        lblPrintDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
    }
    protected void btnReceived_Click(object sender, EventArgs e)
    {
        int Pos_TransactionMasterID = int.Parse(Request.QueryString["Pos_TransactionMasterID"]);

        Pos_TransactionMaster pos_TransactionMaster = Pos_TransactionMasterManager.GetPos_TransactionMasterByID(Pos_TransactionMasterID);

        Pos_TransactionType transactionType = Pos_TransactionTypeManager.GetPos_TransactionTypeByID(pos_TransactionMaster.Pos_TransactionTypeID);
        string sql = "Declare @Count int; ";
        sql += "update Pos_TransactionMaster set  ExtraField5='' where Pos_TransactionMasterID=" + Pos_TransactionMasterID + ";";


        List<Pos_Product> items = new List<Pos_Product>();
        items = Pos_ProductManager.GetAllPos_ProductsByTrasactionMasterID(Pos_TransactionMasterID);

        //updated by is the transaction ID
        string accountingEntry = "";
        accountingEntry += @"
                                Declare @JournalMasterID int
                                Declare @ProductID int
                                Declare @FinishedGoodsAmount decimal(18,2)
                                INSERT INTO [ACC_JournalMaster]
                                           ([JournalMasterName]
                                           ,[ExtraField1]
                                           ,[ExtraField2]
                                           ,[ExtraField3]
                                           ,[Note]
                                           ,[JournalDate]
                                           ,[AddedBy]
                                           ,[AddedDate]
                                           ,[UpdatedBy]
                                           ,[UpdatedDate]
                                           ,[RowStatusID])
                                     VALUES
                                           ('1'--<JournalMasterName, nvarchar(50),>
                                           ,''--<ExtraField1, nvarchar(256),>
                                           ,'POS ISSUE RETURN'--<ExtraField2, nvarchar(256),>
                                           ,'" + Pos_TransactionMasterID.ToString() + @"'--<ExtraField3, nvarchar(256),>
                                           ,'" + pos_TransactionMaster.ToOrFromName + " " + "Issue Return Chalan #: " + pos_TransactionMaster.TransactionID + @"'--<Note, nvarchar(256),>
                                           ,GETDATE()--<JournalDate, datetime,>
                                           ," + pos_TransactionMaster.AddedBy.ToString() + @"--<AddedBy, int,>
                                           ,GETDATE()--<AddedDate, datetime,>
                                           ," + pos_TransactionMaster.AddedBy.ToString() + @"--<UpdatedBy, int,>
                                           ,GETDATE()--<UpdatedDate, datetime,>
                                           ,1--<RowStatusID, int,>
                                           );
                                Set @JournalMasterID = SCOPE_IDENTITY();
                                ";

        foreach (Pos_Product pos_Transaction in items)
        {
            //Finished Good(Aseet) -- Head Office
            accountingEntry += @"
                            Set @ProductID=(Select ProductID from Pos_Product where Pos_ProductID=" + pos_Transaction.Pos_ProductID.ToString() + @")
                            set @FinishedGoodsAmount=(select ([Pos_Product].[FabricsCost] + [Pos_Product].[AccesoriesCost] + [Pos_Product].[Overhead]+[Pos_Product].[OthersCost]+[Pos_Product].[PurchasePrice]) from Pos_Product where Pos_ProductID=" + pos_Transaction.Pos_ProductID.ToString() + @")
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
                                   (@JournalMasterID--<JournalMasterID, int,>
                                   ,@ProductID--<ACC_ChartOfAccountLabel4ID, int,>
                                   ,9--<ACC_ChartOfAccountLabel3ID, int,>
                                   ," + pos_TransactionMaster.ToOrFromID + @"--<WorkStation, int,>
                                   ,(@FinishedGoodsAmount *" + decimal.Parse(pos_Transaction.ExtraField1).ToString("0.00") + @")--<Debit, decimal(20,2),>
                                   ,0--<Credit, decimal(20,2),>
                                   ,'" + pos_Transaction.UpdatedBy + @"'--<ExtraField3, nvarchar(256),>
                                   ,'POS ISSUE RETURN'--<ExtraField2, nvarchar(256),>
                                   ,''--<ExtraField1, nvarchar(256),>
                                   ," + pos_TransactionMaster.AddedBy.ToString() + @"--<AddedBy, int,>
                                    ,GETDATE()--<AddedDate, datetime,>
                                    ," + pos_TransactionMaster.AddedBy.ToString() + @"--<UpdatedBy, int,>
                                    ,GETDATE()--<UpdatedDate, datetime,>
                                    ,1--<RowStatusID, int,>
                                   )  ;
                            ";

            //Finished Good(Aseet) -- Branch
            accountingEntry += @"
                            Set @ProductID=(Select ProductID from Pos_Product where Pos_ProductID=" + pos_Transaction.Pos_ProductID.ToString() + @")
                            set @FinishedGoodsAmount=(select ([Pos_Product].[FabricsCost] + [Pos_Product].[AccesoriesCost] + [Pos_Product].[Overhead]+[Pos_Product].[OthersCost]+[Pos_Product].[PurchasePrice]) from Pos_Product where Pos_ProductID=" + pos_Transaction.Pos_ProductID.ToString() + @")
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
                                   (@JournalMasterID--<JournalMasterID, int,>
                                   ,@ProductID--<ACC_ChartOfAccountLabel4ID, int,>
                                   ,9--<ACC_ChartOfAccountLabel3ID, int,>
                                   ," + pos_TransactionMaster.WorkSatationID + @"--<WorkStation, int,>
                                   ,0--<Debit, decimal(20,2),>
                                   ,(@FinishedGoodsAmount *" + decimal.Parse(pos_Transaction.ExtraField1).ToString("0.00") + @")--<Credit, decimal(20,2),>
                                   ,'" + pos_Transaction.UpdatedBy + @"'--<ExtraField3, nvarchar(256),>
                                   ,'POS ISSUE RETURN'--<ExtraField2, nvarchar(256),>
                                   ,''--<ExtraField1, nvarchar(256),>
                                   ," + pos_TransactionMaster.AddedBy.ToString() + @"--<AddedBy, int,>
                                    ,GETDATE()--<AddedDate, datetime,>
                                    ," + pos_TransactionMaster.AddedBy.ToString() + @"--<UpdatedBy, int,>
                                    ,GETDATE()--<UpdatedDate, datetime,>
                                    ,1--<RowStatusID, int,>
                                   )  ;
                            ";

            if (pos_TransactionMaster.Pos_TransactionTypeID == 10)//Issue return to head office
            {
                sql += "Update Pos_Product set ExtraField1 =(cast(ExtraField1 as decimal(10,2)) + (" + pos_Transaction.ExtraField1 + ")) where Pos_ProductID=" + pos_Transaction.Pos_ProductID.ToString() + ";";
            }
            else
            {
                if (pos_TransactionMaster.WorkSatationID == 1 && transactionType.CentralStockFormula != "0")
                {
                    //For head office Central Stock
                    sql += "Update Pos_Product set ExtraField1 =(cast(ExtraField1 as decimal(10,2)) + ((" + transactionType.CentralStockFormula + ")*" + pos_Transaction.ExtraField1 + ")) where Pos_ProductID=" + pos_Transaction.Pos_ProductID.ToString() + ";";
                }
                else
                    if (pos_TransactionMaster.WorkSatationID != 1 && transactionType.ShowRoomFormula != "0")
                    {
                        sql += @"
                            set @Count=
                            (
                            select COUNT(*) from Pos_WorkStationStock
                            where ProductID=" + pos_Transaction.Pos_ProductID.ToString() + @" and WorkStationID=" + pos_TransactionMaster.WorkSatationID + @"
                            )

                            if @Count = 0
                            BEGIN
	                            INSERT INTO [Pos_WorkStationStock]
                                        ([WorkStationID]
                                        ,[ProductID]
                                        ,[Stock])
                                        VALUES(" + pos_TransactionMaster.WorkSatationID + @"," + pos_Transaction.Pos_ProductID.ToString() + @"," + pos_Transaction.ExtraField1 + @");
                            END
                            ELSE
                            BEGIN
                                Update Pos_WorkStationStock set Stock += ((+1)*" + pos_Transaction.ExtraField1 + @") where  WorkStationID=" + pos_TransactionMaster.WorkSatationID + @" and  ProductID=" + pos_Transaction.Pos_ProductID.ToString() + @";
                            END;
                            
                                ";
                    }
            }
            
        }
        CommonManager.SQLExec(sql +accountingEntry);

        sql = "update Pos_TransactionMaster set  ExtraField5='' where WorkSatationID=" + pos_TransactionMaster.WorkSatationID + " and  TransactionID=" + pos_TransactionMaster.TransactionID + " and Pos_TransactionTypeID=" + pos_TransactionMaster.Pos_TransactionTypeID + ";";

        SaveForRemoteDB(sql , pos_TransactionMaster.WorkSatationID);
        Response.Redirect("TransactionPrint.aspx?Pos_TransactionMasterID=" + Pos_TransactionMasterID);
    }
    private void SaveForRemoteDB(string sql,int toWorkStationID)
    {
        sql = Auto_SQL.add(
             sql //SQLString
           , "1"//Status
           , ConfigurationManager.AppSettings["WorkStationID"] //ForWorkStationID
           , toWorkStationID.ToString()  //[ToWorkStationID]
           , "0" //[FromID]
           , DateTime.Now.ToString("yyyy-MM-dd hh:mm tt") //FromTime
           , ""//<UploadTime, nvarchar(256),>
           , ""//<ExecuteTime, nvarchar(256),>
           , ""//<ExtraField1, nvarchar(256),>
           , ""//<ExtraField2, nvarchar(256),>
           , ""//<ExtraField3, nvarchar(256),>
           , ""//<ExtraField4, nvarchar(256),>
           , ""//<ExtraField5, nvarchar(256),>
            );


        CommonManager.SQLExec(sql);
    }

}