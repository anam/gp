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
        lblStockDate.Text = DateTime.Today.ToString("dd/MM/yyyy") ;
        int Pos_TransactionMasterID = int.Parse(Request.QueryString["Pos_TransactionMasterID"]);

        Pos_TransactionMaster pos_TransactionMaster = Pos_TransactionMasterManager.GetPos_TransactionMasterByID(Pos_TransactionMasterID);

        lblPurchaseDate.Text = pos_TransactionMaster.TransactionDate.ToString("dd-MMM-yyyy");
        lblParticulars.Text = pos_TransactionMaster.Particulars;
        lblTransactionID.Text = pos_TransactionMaster.TransactionID.ToString();
        lblRefferenceIDs.Text = pos_TransactionMaster.Record;
        lblToOrFromName.Text = pos_TransactionMaster.ToOrFromName;
        if (pos_TransactionMaster.ToOrFromID.ToString() != getLogin().ExtraField5 && getLogin().ExtraField5!="1")
        {
            showAlartMessage("Your are not allowed to View this Chalan");
            btnReceived.Visible = false;
            return;
            
        }
        if (pos_TransactionMaster.ExtraField5 == "Pending")
        {
            lblExtraField5.Text = pos_TransactionMaster.ExtraField5;
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
        //purchase info
        string sql = @"SELECT 
Pos_Product.StyleCode as Style,Pos_Product.BarCode
,Pos_Size.SizeName as Size,Pos_Color.ColorName as Color
,Pos_Product.ProductName as Product
,Pos_Transaction.Quantity as [Stock]
,Pos_Product.SalePrice 
      ,(Pos_Transaction.Quantity * Pos_Product.SalePrice) as Amount
  FROM Pos_Product
  inner join Pos_Size on Pos_Product.Pos_SizeID=Pos_Size.Pos_SizeID
  inner join Pos_Color on Pos_Product.Pos_ColorID=Pos_Color.Pos_ColorID
inner join Pos_Transaction on Pos_Transaction.Pos_ProductID =Pos_Product.Pos_ProductID
inner join Pos_TransactionMaster on Pos_TransactionMaster.Pos_TransactionMasterID = Pos_Transaction.Pos_ProductTransactionMasterID
 where   Pos_TransactionMaster.Pos_TransactionMasterID =" + Pos_TransactionMasterID.ToString() + @"
order by Pos_Product.StyleCode,Pos_Product.BarCode
";

        DataSet ds = CommonManager.SQLExec(sql);
            
        int serialNo = 1;

        string htmlTable = @" <table id='itemList_tbl' style='border:1px solid black;width:100%;' cellpadding='0' cellspacing='0'>
                        <tr id='tableHeader'>
                            <td  style='border-left:0px;'>S/L</td>
                            <td>Bar Code</td>
                            <td>Description</td>
                            <td>Color</td>
                            <td>Size</td>
                            <td>Quantity</td>
                            <td>Unit Price</td>
                            <td>Amount</td>
                        </tr>
                            ";

        decimal subtotalQty = 0;
        decimal subtotalUnitPrice = 0;
        decimal subtotalStockAmount = 0;

        decimal totalQty = 0;
        decimal totalUnitPrice = 0;
        decimal totalStockAmount = 0;

        decimal GrandtotalQty = 0;
        decimal GrandtotalUnitPrice = 0;
        decimal GrandtotalStockAmount = 0;
        
        string lastProductName = "";
        string lastStyle = "";

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (decimal.Parse(dr["Stock"].ToString()) > 0)
            {
                if (lastProductName == "")
                {
                    htmlTable += @" <tr id='tableHeader'>
                            <td  style='border-left:0px;'></td>
                            <td colspan='7' style='text-align:left;'>Item Name :" + dr["Product"].ToString() + @"</td>
                        </tr>
                            ";
                    lastProductName = dr["Product"].ToString();

                    htmlTable += @" <tr id='tableHeader'>
                            <td  style='border-left:0px;'></td>
                            <td colspan='7' style='text-align:left;'>Style :" + dr["Style"].ToString() + @"</td>
                        </tr>
                            ";
                    lastStyle = dr["Style"].ToString();
                }
                else
                    if (lastProductName != dr["Product"].ToString())
                    {
                        htmlTable += @" <tr class='subtotalRow'>
                            <td  style='border-left:0px;'></td>
                            <td style='text-align:right;' colspan='4'>Total</td>
                            <td>" + totalQty.ToString("0,0") + @"</td>
                            <td>" + totalUnitPrice.ToString("0,0.00") + @"</td>
                            <td>" + totalStockAmount.ToString("0,0.00") + @"</td>
                        </tr>
                             <tr class='subtotalRow'>
                            <td  style='border-left:0px;'></td>
                            <td style='text-align:right;' colspan='4'>Sub Total</td>
                            <td>" + subtotalQty.ToString("0,0") + @"</td>
                            <td>" + subtotalUnitPrice.ToString("0,0.00") + @"</td>
                            <td>" + subtotalStockAmount.ToString("0,0.00") + @"</td>
                        </tr>
                            ";

                        htmlTable += @" <tr  id='tableHeader'>
                            <td  style='border-left:0px;'></td>
                            <td colspan='7' style='text-align:left;'>Item Name :" + dr["Product"].ToString() + @"</td>
                        </tr>
                            ";
                        htmlTable += @" <tr  id='tableHeader'>
                            <td  style='border-left:0px;'></td>
                            <td colspan='7' style='text-align:left;'>Style :" + dr["Style"].ToString() + @"</td>
                        </tr>
                            ";

                        lastProductName = dr["Product"].ToString();
                        lastStyle = dr["Style"].ToString();

                        subtotalQty = 0;
                        subtotalUnitPrice = 0;
                        subtotalStockAmount = 0;

                        totalQty = 0;
                        totalUnitPrice = 0;
                        totalStockAmount = 0;
                    }
                    else
                        if (lastStyle != dr["Style"].ToString())
                        {
                            htmlTable += @" <tr class='subtotalRow'>
                            <td  style='border-left:0px;'></td>
                            <td style='text-align:right;' colspan='4'>Total</td>
                            <td>" + totalQty.ToString("0,0") + @"</td>
                            <td>" + totalUnitPrice.ToString("0,0.00") + @"</td>
                            <td>" + totalStockAmount.ToString("0,0.00") + @"</td>
                        </tr>
                            ";

                            htmlTable += @" <tr id='tableHeader'>
                            <td  style='border-left:0px;'></td>
                            <td colspan='7' style='text-align:left;'>Style :" + dr["Style"].ToString() + @"</td>
                        </tr>
                            ";

                            lastStyle = dr["Style"].ToString();

                            totalQty = 0;
                            totalUnitPrice = 0;
                            totalStockAmount = 0;
                        }

                totalUnitPrice += decimal.Parse(dr["SalePrice"].ToString());
                totalQty += decimal.Parse(dr["Stock"].ToString());
                totalStockAmount += decimal.Parse(dr["Amount"].ToString());

                subtotalUnitPrice += decimal.Parse(dr["SalePrice"].ToString());
                subtotalQty += decimal.Parse(dr["Stock"].ToString());
                subtotalStockAmount += decimal.Parse(dr["Amount"].ToString());

                GrandtotalUnitPrice += decimal.Parse(dr["SalePrice"].ToString());
                GrandtotalQty += decimal.Parse(dr["Stock"].ToString());
                GrandtotalStockAmount += decimal.Parse(dr["Amount"].ToString());



                htmlTable += @" <tr class='itemCss'>
                            <td  style='border-left:0px;'>" + (serialNo++).ToString() + @"</td>
                            <td>" + dr["BarCode"].ToString() + @"</td>
                            <td>" + dr["Product"].ToString() + @"</td>
                            <td>" + dr["Color"].ToString() + @"</td>
                            <td>" + dr["Size"].ToString() + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["Stock"].ToString()).ToString("0,0") + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["SalePrice"].ToString()).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["Amount"].ToString()).ToString("0,0.00") + @"</td>
                        </tr>
                            ";
            }
        }
        htmlTable += @" <tr id='lastRow'>
                            <td  style='border-left:0px;'></td>
                            <td style='text-align:right;' colspan='4'>Total</td>
                            <td>" + totalQty.ToString("0,0") + @"</td>
                            <td>" + totalUnitPrice.ToString("0,0.00") + @"</td>
                            <td>" + totalStockAmount.ToString("0,0.00") + @"</td>
                        </tr>
                             <tr id='lastRow'>
                            <td  style='border-left:0px;'></td>
                            <td style='text-align:right;' colspan='4'>Sub Total</td>
                            <td>" + subtotalQty.ToString("0,0") + @"</td>
                            <td>" + subtotalUnitPrice.ToString("0,0.00") + @"</td>
                            <td>" + subtotalStockAmount.ToString("0,0.00") + @"</td>
                        </tr>
                            <tr id='lastRow'>
                            <td  style='border-left:0px;'></td>
                            <td style='text-align:right;' colspan='4'>Grand Total</td>
                            <td>" + GrandtotalQty.ToString("0,0") + @"</td>
                            <td>" + GrandtotalUnitPrice.ToString("0,0.00") + @"</td>
                            <td>" + GrandtotalStockAmount.ToString("0,0.00") + @"</td>
                        </tr>
                           </table>  ";

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
        sql += "update Pos_TransactionMaster set  ExtraField5='' where Pos_TransactionMasterID=" + Pos_TransactionMasterID+";";


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
                                           ,'POS ISSUE'--<ExtraField2, nvarchar(256),>
                                           ,'" + Pos_TransactionMasterID.ToString() + @"'--<ExtraField3, nvarchar(256),>
                                           ,'"+pos_TransactionMaster.ToOrFromName+" "+"Chalan #: " + pos_TransactionMaster.TransactionID  + @"'--<Note, nvarchar(256),>
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
            //Finished Good(Aseet) -- branch
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
                                   ,'POS ISSUE'--<ExtraField2, nvarchar(256),>
                                   ,''--<ExtraField1, nvarchar(256),>
                                   ," + pos_TransactionMaster.AddedBy.ToString() + @"--<AddedBy, int,>
                                    ,GETDATE()--<AddedDate, datetime,>
                                    ," + pos_TransactionMaster.AddedBy.ToString() + @"--<UpdatedBy, int,>
                                    ,GETDATE()--<UpdatedDate, datetime,>
                                    ,1--<RowStatusID, int,>
                                   )  ;
                            ";

            //Finished Good(Aseet) -- head office
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
                                   ,1--<WorkStation, int,>
                                   ,0--<Debit, decimal(20,2),>
                                   ,(@FinishedGoodsAmount *" + decimal.Parse(pos_Transaction.ExtraField1).ToString("0.00") + @")--<Credit, decimal(20,2),>
                                   ,'" + pos_Transaction.UpdatedBy + @"'--<ExtraField3, nvarchar(256),>
                                   ,'POS ISSUE'--<ExtraField2, nvarchar(256),>
                                   ,''--<ExtraField1, nvarchar(256),>
                                   ," + pos_TransactionMaster.AddedBy.ToString() + @"--<AddedBy, int,>
                                    ,GETDATE()--<AddedDate, datetime,>
                                    ," + pos_TransactionMaster.AddedBy.ToString() + @"--<UpdatedBy, int,>
                                    ,GETDATE()--<UpdatedDate, datetime,>
                                    ,1--<RowStatusID, int,>
                                   )  ;
                            ";

            if (pos_TransactionMaster.ToOrFromID == 1 && transactionType.CentralStockFormula != "0")
            {
                //For head office Central Stock
                sql += "Update Pos_Product set ExtraField1 =(cast(ExtraField1 as decimal(10,2)) + ((" + transactionType.CentralStockFormula + ")*" + pos_Transaction.ExtraField1 + ")) where Pos_ProductID=" + pos_Transaction.Pos_ProductID.ToString() + ";";
            }
            else
                if (pos_TransactionMaster.ToOrFromID != 1 && transactionType.ShowRoomFormula != "0")
                {
                    sql += @"
                            set @Count=
                            (
                            select COUNT(*) from Pos_WorkStationStock
                            where ProductID=" + pos_Transaction.Pos_ProductID.ToString() + @" and WorkStationID=" + pos_TransactionMaster.ToOrFromID + @"
                            )

                            if @Count = 0
                            BEGIN
	                            INSERT INTO [Pos_WorkStationStock]
                                        ([WorkStationID]
                                        ,[ProductID]
                                        ,[Stock])
                                        VALUES(" + pos_TransactionMaster.ToOrFromID + @"," + pos_Transaction.Pos_ProductID.ToString() + @"," + pos_Transaction.ExtraField1 + @");
                            END
                            ELSE
                            BEGIN
                                Update Pos_WorkStationStock set Stock += ((+1)*" + pos_Transaction.ExtraField1 + @") where  WorkStationID=" + pos_TransactionMaster.ToOrFromID + @" and  ProductID=" + pos_Transaction.Pos_ProductID.ToString() + @";
                            END;
                            
                                ";
                }
            
        }

        //CommonManager.SQLExec(sql);
        CommonManager.SQLExec(sql + accountingEntry);
        
        Response.Redirect("DelivaryChalanPrint.aspx?Pos_TransactionMasterID=" + Pos_TransactionMasterID);
    }


}