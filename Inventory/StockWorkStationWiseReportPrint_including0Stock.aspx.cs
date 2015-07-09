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
            initialLoad();
            loadData();
        }
    }

    private void loadData()
    {

        int WorkStationID = 0;
        try
        {
            WorkStationID = Int32.Parse(Request.QueryString["WorkStationID"]);
        }
        catch (Exception ex)
        {
            WorkStationID = 0;
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


        string fromDate = (Request.QueryString["FromDate"] != null ? Request.QueryString["FromDate"].ToString() : "");
        string toDate = (Request.QueryString["ToDate"] != null ? Request.QueryString["ToDate"].ToString() : "");

        //purchase info
        string sql = @"select 
SUM(Inv_IssueDetail.Quantity) as InitialIssed
,SUM(cast(Inv_IssueDetail.[ExtraField1] as decimal(10,2)) * Inv_Item.PricePerUnit) as StockTotalAmount
,Sum(cast(Inv_IssueDetail.[ExtraField1] as decimal(10,2)))  as StockQuantity
,Sum(cast(Inv_IssueDetail.[ExtraField2] as decimal(10,2)))  as IssueReturn
,Sum(cast(Inv_IssueDetail.[ExtraField3] as decimal(10,2)))  as Utlization
,Sum(cast(Inv_IssueDetail.[ExtraField4] as decimal(10,2)))  as Wasted
,Inv_QuantityUnit.QuantityUnitName
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
,Inv_Item.ItemCode,Inv_Item.RawMaterialID
from
Inv_Item 
inner join Inv_IssueDetail on Inv_IssueDetail.ItemID = Inv_Item.Inv_ItemID
inner join Inv_IssueMaster on Inv_IssueMaster.Inv_IssueMasterID = Inv_IssueDetail.ExtraField5
inner join ACC_ChartOfAccountLabel4 on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID= Inv_Item.RawMaterialID
inner join ACC_ChartOfAccountLabel4 as WorkStation on WorkStation.ACC_ChartOfAccountLabel4ID= Inv_IssueMaster.WorkSatationID
inner join Inv_QuantityUnit on Inv_QuantityUnit.Inv_QuantityUnitID=Inv_Item.QuantityUnitID
 where Inv_Item.ExtraField1<>'0' and  Inv_Item.RowStatusID=1 ";

        if (WorkStationID != 0)
        {
            sql += @" and Inv_IssueMaster.WorkSatationID=" + WorkStationID.ToString();
        }
        if (RawmaterialsTypeID != 0)
        {
            sql += " and ACC_ChartOfAccountLabel4.ACC_HeadTypeID =" + RawmaterialsTypeID;
        }


        sql += @"  group by Inv_QuantityUnit.QuantityUnitName
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
,Inv_Item.ItemCode,Inv_Item.RawMaterialID

order by Inv_Item.RawMaterialID,ItemCode;";


        if (WorkStationID != 0)
        {
            sql += "select ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4ID,ExtraField2 from ACC_ChartOfAccountLabel4 where ACC_ChartOfAccountLabel4ID =" + WorkStationID.ToString();
        }

        DataSet ds = CommonManager.SQLExec(sql);


        decimal InitialIssed = 0;
        decimal StockTotalAmount = 0;
        decimal StockQuantity = 0;
        decimal IssueReturn = 0;
        decimal Utlization = 0;
        decimal Wasted = 0;

        decimal InitialIssedTotal = 0;
        decimal StockTotalAmountTotal = 0;
        decimal StockQuantityTotal = 0;
        decimal IssueReturnTotal = 0;
        decimal UtlizationTotal = 0;
        decimal WastedTotal = 0;

        string RawMaterialID = "0";
           
            int serialNo = 1;

            string htmlTable = @" <table id='itemList_tbl' style='border:1px solid black;width:100%;' cellpadding='0' cellspacing='0'>
                        ";

            if (WorkStationID != 0)
            {
                htmlTable += @"<tr>
                            <td colspan='10' style='padding-left:50px;border-top:1px solid black;'>
                                <b>WorkStation name:</b> " + ds.Tables[1].Rows[0]["ChartOfAccountLabel4Text"].ToString() + @"
                            </td>
                        </tr>";
            }
            else
            {
                htmlTable += @"<tr>
                            <td colspan='10' style='padding-left:50px; border-top:1px solid black;border-bottom:1px solid black;'>
                                All Work Station
                            </td>
                        </tr>";
            }

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (RawMaterialID != "0" && RawMaterialID != dr["RawMaterialID"].ToString())
                {
                    htmlTable += @"<tr class='subtotalRow'>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>Sub Total</td>
                            <td>" + InitialIssed.ToString("0,0.00") + @"</td>
                            <td>" + IssueReturn.ToString("0,0.00") + @"</td>
                            <td>" + Wasted.ToString("0,0.00") + @"</td>
                            <td>" + Utlization.ToString("0,0.00") + @"</td>
                            <td>" + StockQuantityTotal.ToString("0,0.00") + @"</td>
                            <td>" + StockTotalAmountTotal.ToString("0,0.00") + @"</td>
                        </tr>";

                    InitialIssed = 0;
                    StockTotalAmount = 0;
                    StockQuantity = 0;
                    IssueReturn = 0;
                    Utlization = 0;
                    Wasted = 0;
                }

                if (RawMaterialID != dr["RawMaterialID"].ToString())
                {
                    RawMaterialID = dr["RawMaterialID"].ToString();
                    htmlTable += @"<tr>
                            <td colspan='10' style='padding-left:50px; padding-top:20px;border-top:1px solid black;'>
                                <b>Item:</b> " + dr["ChartOfAccountLabel4Text"].ToString() + @"
                            </td>
                        </tr>
                        <tr id='tableHeader'>
                            <td  style='border-left:0px;'>S/L</td>
                            <td>Item Code</td>
                            <td>Item Name</td>
                            <td>Unit</td>
                            <td>Initial<br/>Issed<br/>Quantity</td>
                            <td>Issue<br/>Return<br/>Quantity</td>
                            <td>Wastege<br/>Quantity</td>
                            <td>Utlized<br/>Quantity</td>
                            <td>Stock<br/>Quantity</td>
                            <td>Stock<br/>Amount</td>
                        </tr>";

                    InitialIssed = 0;
                    StockTotalAmount = 0;
                    StockQuantity = 0;
                    IssueReturn = 0;
                    Utlization = 0;
                    Wasted = 0;
                }

                htmlTable += @"<tr class='itemCss'>
                            <td  style='border-left:0px;'>" + (serialNo++).ToString() + @"</td>
                            <td>" + dr["ItemCode"].ToString() + @"</td>
                            <td>" + dr["ChartOfAccountLabel4Text"].ToString() + @"</td>
                            <td >" + dr["QuantityUnitName"].ToString() + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["InitialIssed"].ToString()).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["IssueReturn"].ToString()).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["Wasted"].ToString()).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["Utlization"].ToString()).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["StockQuantity"].ToString()).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["StockTotalAmount"].ToString()).ToString("0,0.00") + @"</td>
                        </tr>";
                InitialIssed += decimal.Parse(dr["InitialIssed"].ToString());
                StockTotalAmount += decimal.Parse(dr["StockTotalAmount"].ToString());
                StockQuantity += decimal.Parse(dr["StockQuantity"].ToString());
                IssueReturn += decimal.Parse(dr["IssueReturn"].ToString());
                Utlization += decimal.Parse(dr["Utlization"].ToString());
                Wasted  += decimal.Parse(dr["Wasted"].ToString());

                InitialIssedTotal += decimal.Parse(dr["InitialIssed"].ToString());
                StockTotalAmountTotal += decimal.Parse(dr["StockTotalAmount"].ToString());
                StockQuantityTotal += decimal.Parse(dr["StockQuantity"].ToString());
                IssueReturnTotal += decimal.Parse(dr["IssueReturn"].ToString());
                UtlizationTotal += decimal.Parse(dr["Utlization"].ToString());
                WastedTotal += decimal.Parse(dr["Wasted"].ToString());
            }

            htmlTable += @"<tr class='subtotalRow'>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>Sub Total</td>
                            <td style='text-align:right;'>" + InitialIssed.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + IssueReturn.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + Wasted.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + Utlization.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + StockQuantity.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + StockTotalAmount.ToString("0,0.00") + @"</td>
                        </tr>";

            htmlTable += @"<tr id='lastRow'>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>Grand Total</td>
                        <td style='text-align:right;'>" + InitialIssedTotal.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + IssueReturnTotal.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + WastedTotal.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + UtlizationTotal.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + StockQuantityTotal.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + StockTotalAmountTotal.ToString("0,0.00") + @"</td>
                        </tr></table>";

       

        lblItemList.Text = htmlTable;
    }

    private void initialLoad()
    {
        lblPrintDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
    }
}