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

        int workStationID = 0;
        try
        {
            workStationID = Int32.Parse(Request.QueryString["WorkStationID"]);
        }
        catch (Exception ex)
        {
            workStationID = 0;
        }



        string fromDate = (Request.QueryString["FromDate"] != null ? Request.QueryString["FromDate"].ToString() : "");
        string toDate = (Request.QueryString["ToDate"] != null ? Request.QueryString["ToDate"].ToString() : "");

        //purchase info
        string sql = @"SELECT [Inv_UtilizationDetails].[ProductID],Inv_Utilization.WorkSatationID
	,Prod.ChartOfAccountLabel4Text as ProductName
	,WorkStation.ChartOfAccountLabel4Text as WorkStationName
      ,sum([FabricsCost]) as FabricsCost
      ,sum([AccesoriesCost]) as AccesoriesCost
      ,sum([Overhead]) as Overhead
      ,sum([OthersCost]) as OthersCost
      ,Sum([ProductionQuantity]) as Qty
      ,sum(([FabricsCost]+[AccesoriesCost]+[Overhead]+[OthersCost])*[ProductionQuantity]) as TotalCost
      ,sum(([FabricsCost]+[AccesoriesCost]+[Overhead]+[OthersCost])*[ProductionQuantity])/Sum([ProductionQuantity]) as UnitCost
  FROM [Inv_UtilizationDetails]
  inner join Inv_Utilization on Inv_Utilization.Inv_UtilizationID=[Inv_UtilizationDetails].Inv_UtilizationID
inner join ACC_ChartOfAccountLabel4 as Prod on Prod.ACC_ChartOfAccountLabel4ID = [Inv_UtilizationDetails].[ProductID]
inner join ACC_ChartOfAccountLabel4 as WorkStation on WorkStation.ACC_ChartOfAccountLabel4ID = Inv_Utilization.WorkSatationID
 where (Inv_Utilization.UtilizationDate  between '" + fromDate + "' and '" + toDate + "')";
        if (workStationID != 0)
        {
            sql += " and Inv_Utilization.WorkSatationID =" + workStationID;
        }


        sql += @" Group by [Inv_UtilizationDetails].[ProductID],Inv_Utilization.WorkSatationID
  ,Prod.ChartOfAccountLabel4Text
	,WorkStation.ChartOfAccountLabel4Text
	order by WorkStation.ChartOfAccountLabel4Text,Prod.ChartOfAccountLabel4Text;";



        DataSet ds= CommonManager.SQLExec(sql);


        string WorkStationID = "0";
            decimal Total = 0;
            decimal Subtotal = 0;
            decimal TotalAmount = 0;
            decimal SubtotalAmount = 0;
            int serialNo = 1;

            string htmlTable = @" <table id='itemList_tbl' style='border:1px solid black;width:100%;' cellpadding='0' cellspacing='0'>
                        ";

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (WorkStationID != "0" && WorkStationID != dr["WorkSatationID"].ToString())
                {
                    htmlTable += @"<tr class='subtotalRow'>
                            <td>&nbsp;</td>
                            <td>Sub Total</td>
                            <td>" + Subtotal.ToString("0,0") + @"</td>
                            <td>&nbsp;</td>
                            <td>" + SubtotalAmount.ToString("0,0.00") + @"</td>
                        </tr>";

                    Subtotal = 0;
                    SubtotalAmount = 0;
                }

                if (WorkStationID != dr["WorkSatationID"].ToString())
                {
                    WorkStationID = dr["WorkSatationID"].ToString();
                    htmlTable += @"<tr>
                            <td colspan='6' style='padding-left:50px; border-top:1px solid black;border-bottom:1px solid black;'>
                                <b>Product :</b> " + dr["WorkStationName"].ToString() + @"
                            </td>
                        </tr>
                        <tr id='tableHeader'>
                            <td  style='border-left:0px;'>S/L</td>
                            <td>Item</td>
                            <td>Qty</td>
                            <td>Unit Cost(Avg)</td>
                            <td>Total Cost</td>
                        </tr>
                      
                        ";
                }

                htmlTable += @"<tr class='itemCss'>
                            <td  style='border-left:0px;'>" + (serialNo++).ToString() + @"</td>
                            <td>" + dr["ProductName"].ToString() + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["Qty"].ToString()).ToString("0,0") + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["UnitCost"].ToString()).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["TotalCost"].ToString()).ToString("0,0.00") + @"</td>
                        </tr>";

                Subtotal += decimal.Parse(dr["Qty"].ToString());
                SubtotalAmount += decimal.Parse(dr["TotalCost"].ToString());

                Total += decimal.Parse(dr["Qty"].ToString());
                TotalAmount += decimal.Parse(dr["TotalCost"].ToString());
            }

            htmlTable += @"<tr class='subtotalRow'>
                            <td>&nbsp;</td>
                            <td>Sub Total</td>
                            <td>" + Subtotal.ToString("0,0") + @"</td>
                            <td>&nbsp;</td>
                            <td>" + SubtotalAmount.ToString("0,0.00") + @"</td>
                        </tr>";

            htmlTable += @"<tr id='lastRow'>
                        <td>&nbsp;</td>
                        <td>Grand Total</td>
                        <td>" + Total.ToString("0,0") + @"</td>
                        <td>&nbsp;</td>
                        <td>" + TotalAmount.ToString("0,0.00") + @"</td>
                    </tr></table>";

       

        lblItemList.Text = htmlTable;
    }

    private void initialLoad()
    {
        lblPrintDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
    }
}