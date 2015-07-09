using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

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
        int workstationID = 0;
        try
        {
            workstationID = Int32.Parse(Request.QueryString["WorkStationID"]);
        }
        catch (Exception ex)
        {
            workstationID = 0;
        }


        int RawmaterialsTypeID = 0;
        try
        {
            RawmaterialsTypeID = Int32.Parse(Request.QueryString["RawmaterialsTypeID"]);
        }
        catch (Exception ex)
        {
            RawmaterialsTypeID = 0;
        }


        int itemID = 0;
        try
        {
            itemID = Int32.Parse(Request.QueryString["ItemID"]);
        }
        catch (Exception ex)
        {
            itemID = 0;
        }

        string fromDate = (Request.QueryString["FromDate"] != null ? Request.QueryString["FromDate"].ToString() : "");
        string toDate = (Request.QueryString["ToDate"] != null ? Request.QueryString["ToDate"].ToString() : "");
        lblStockDate.Text = DateTime.Parse(fromDate).ToString("dd/MM/yyyy") + " to " + DateTime.Parse(toDate).ToString("dd/MM/yyyy");
        
        string sql = @"select SUM(Inv_ItemTransaction.Quantity) as Stock,Inv_QuantityUnit.QuantityUnitName
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text as Product
,ACC_ChartOfAccountLabel4.ExtraField1
,SUM(Inv_ItemTransaction.Quantity * Inv_Item.PricePerUnit) as Amount  
,WorkStation.ChartOfAccountLabel4Text as WorkStationName
,Inv_IssueMaster.WorkSatationID,Inv_IssueMaster.Inv_IssueMasterID as Style
from
Inv_Item 
inner join ACC_ChartOfAccountLabel4 on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID= Inv_Item.RawMaterialID
inner join Inv_QuantityUnit on Inv_QuantityUnit.Inv_QuantityUnitID=Inv_Item.QuantityUnitID
inner join Inv_ItemTransaction on Inv_ItemTransaction.ItemID =Inv_Item.Inv_ItemID
inner join Inv_IssueMaster on Inv_IssueMaster.Inv_IssueMasterID =Inv_ItemTransaction.ExtraField5
inner join ACC_ChartOfAccountLabel4 as WorkStation on WorkStation.ACC_ChartOfAccountLabel4ID= Inv_IssueMaster.WorkSatationID
where Inv_Item.RowStatusID=1 and Inv_IssueMaster.RowStatusID=1 and Inv_ItemTransaction.RowStatusID=1 and Inv_ItemTransaction.ItemTrasactionTypeID =2 and  (Inv_IssueMaster.IssueDate >= '" + fromDate + "' and Inv_IssueMaster.IssueDate <= '" + toDate + "')";
        if (workstationID != 0)
        {
            sql += " and Inv_IssueMaster.WorkSatationID =" + workstationID;
        }

        if (RawmaterialsTypeID != 0)
        {
            sql += " and ACC_ChartOfAccountLabel4.ACC_HeadTypeID =" + RawmaterialsTypeID;
        }
        if (itemID != 0)
        {
            sql += " and Inv_Item.RawMaterialID =" + itemID;
        }


        sql += @" group by Inv_QuantityUnit.QuantityUnitName
                ,Inv_IssueMaster.Inv_IssueMasterID
                ,Inv_IssueMaster.WorkSatationID
                ,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
                ,ACC_ChartOfAccountLabel4.ExtraField1
                ,WorkStation.ChartOfAccountLabel4Text
                order by WorkStation.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,Inv_IssueMaster.Inv_IssueMasterID;";


   
        DataSet ds = CommonManager.SQLExec(sql);
            
        int serialNo = 1;

        string htmlTable = @" <table id='itemList_tbl' style='border:1px solid black;width:100%;' cellpadding='0' cellspacing='0'>
                        <tr id='tableHeader'>
                            <td  style='border-left:0px;'>S/L</td>
                            <td>Issue ID</td>
                            <td>Code</td>
                            <td>Item</td>
                            <td>Quantity</td>
                            <td>Unit</td>
                            <td>Amount</td>
                        </tr>
                            ";

        decimal subtotalQty = 0;
        decimal subtotalStockAmount = 0;

        decimal totalQty = 0;
        decimal totalStockAmount = 0;

        decimal GrandtotalQty = 0;
        decimal GrandtotalStockAmount = 0;
        
        string lastProductName = "";
        string lastStyle = "";

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (decimal.Parse(dr["Stock"].ToString()) > 0)
            {
                if (lastProductName == "")
                {
                    htmlTable += @" <tr  id='tableHeader'>
                            <td  style='border-left:0px;'></td>
                            <td colspan='6' style='text-align:left;'>Item Name :" + dr["Product"].ToString() + @"</td>
                        </tr>
                            ";
//                    htmlTable += @" <tr  id='tableHeader'>
//                            <td  style='border-left:0px;'></td>
//                            <td colspan='6' style='text-align:left;'>Issue ID :<a href='IssuePrint.aspx?IssueID=" + dr["Style"].ToString() + @"' target='_blank'>" + dr["Style"].ToString() + @"</a></td>
//                        </tr>
//                            ";
                    lastProductName = dr["Product"].ToString();

                    lastStyle = dr["Style"].ToString();
                }
                else
                    if (lastProductName != dr["Product"].ToString())
                    {
                        htmlTable += @" <tr class='subtotalRow' style='display:none;'>
                            <td  style='border-left:0px;'></td>
                            <td style='text-align:right;'  colspan='3'>Total</td>
                            <td>" + totalQty.ToString("0,0") + @"</td>
                            <td></td>
                            <td>" + totalStockAmount.ToString("0,0.00") + @"</td>
                        </tr>
                             <tr class='subtotalRow'>
                            <td  style='border-left:0px;'></td>
                            <td style='text-align:right;'  colspan='3'>Sub Total</td>
                            <td>" + subtotalQty.ToString("0,0") + @"</td>
                            <td> </td>
                            <td>" + subtotalStockAmount.ToString("0,0.00") + @"</td>
                        </tr>
                            ";

                        htmlTable += @" <tr  id='tableHeader'>
                            <td  style='border-left:0px;'></td>
                            <td colspan='6' style='text-align:left;'>Item Name :" + dr["Product"].ToString() + @"</td>
                        </tr>
                            ";
//                        htmlTable += @" <tr  id='tableHeader'>
//                            <td  style='border-left:0px;'></td>
//                            <td colspan='6' style='text-align:left;'>Issue ID :<a href='IssuePrint.aspx?IssueID=" + dr["Style"].ToString() + @"' target='_blank'>" + dr["Style"].ToString() + @"</a></td>
//                        </tr>
//                            ";

                        lastProductName = dr["Product"].ToString();
                        lastStyle = dr["Style"].ToString();

                        subtotalQty = 0;
                        subtotalStockAmount = 0;

                        totalQty = 0;
                        totalStockAmount = 0;
                    }
                    else
                        if (lastStyle != dr["Style"].ToString())
                        {
                            htmlTable += @" <tr class='subtotalRow' style='display:none;'>
                            <td  style='border-left:0px;'></td>
                            <td style='text-align:right;'  colspan='3'>Total</td>
                            <td>" + totalQty.ToString("0,0") + @"</td>
                            <td> </td>
                            <td>" + totalStockAmount.ToString("0,0.00") + @"</td>
                        </tr>
                            ";

//                            htmlTable += @" <tr  id='tableHeader'>
//                            <td  style='border-left:0px;'></td>
//                            <td colspan='6' style='text-align:left;'>Issue ID :<a href='IssuePrint.aspx?IssueID=" + dr["Style"].ToString() + @"' target='_blank'>" + dr["Style"].ToString() + @"</a></td>
//                        </tr>
//                            ";

                            lastStyle = dr["Style"].ToString();

                            totalQty = 0;
                            totalStockAmount = 0;
                        }

                totalQty += decimal.Parse(dr["Stock"].ToString());
                totalStockAmount += decimal.Parse(dr["Amount"].ToString());

                subtotalQty += decimal.Parse(dr["Stock"].ToString());
                subtotalStockAmount += decimal.Parse(dr["Amount"].ToString());

                GrandtotalQty += decimal.Parse(dr["Stock"].ToString());
                GrandtotalStockAmount += decimal.Parse(dr["Amount"].ToString());



                htmlTable += @" <tr class='itemCss'>
                            <td  style='border-left:0px;'>" + (serialNo++).ToString() + @"</td>
                            <td><a href='IssuePrint.aspx?IssueID=" + dr["Style"].ToString() + @"' target='_blank'>" + dr["Style"].ToString() + @"</a></td>                            
                            <td>" + dr["ExtraField1"].ToString() + @"</td>
                            <td>" + dr["Product"].ToString() + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["Stock"].ToString()).ToString("0,0") + @"</td>
                            <td style='text-align:center;'>" + dr["QuantityUnitName"].ToString() + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["Amount"].ToString()).ToString("0,0.00") + @"</td>
                        </tr>
                            ";
            }
        }
        htmlTable += @" <tr id='lastRow' style='display:none;'>
                            <td  style='border-left:0px;'></td>
                            <td style='text-align:right;'  colspan='3'>Total</td>
                            <td>" + totalQty.ToString("0,0") + @"</td>
                            <td> </td>
                            <td>" + totalStockAmount.ToString("0,0.00") + @"</td>
                        </tr>
                             <tr id='lastRow'>
                            <td  style='border-left:0px;'></td>
                            <td style='text-align:right;'  colspan='3'>Sub Total</td>
                            <td>" + subtotalQty.ToString("0,0") + @"</td>
                            <td> </td>
                            <td>" + subtotalStockAmount.ToString("0,0.00") + @"</td>
                        </tr>
                            <tr id='lastRow'>
                            <td  style='border-left:0px;'></td>
                            <td style='text-align:right;'  colspan='3'>Grand Total</td>
                            <td>" + GrandtotalQty.ToString("0,0") + @"</td>
                            <td> </td>
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
        foreach (Pos_Product pos_Transaction in items)
        {


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
            CommonManager.SQLExec(sql);
        }

        Response.Redirect("DelivaryChalanPrint.aspx?Pos_TransactionMasterID=" + Pos_TransactionMasterID);
    }


}