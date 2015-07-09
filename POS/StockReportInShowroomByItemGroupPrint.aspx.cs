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
        lblStockDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
        int workStationID = 0;
        try
        {
            workStationID = Int32.Parse(Request.QueryString["WorkStationID"]);
            lblStockDate.Text += "<br/>" + Request.QueryString["WorkStationName"];
        }
        catch (Exception ex)
        {
            workStationID = 0;
        }
        //purchase info
        string sql = @"SELECT 
ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text as Product,
SUM(Pos_WorkStationStock.[Stock]) as Stock
      ,SUM(Pos_WorkStationStock.[Stock] * Pos_Product.SalePrice) as Amount
      
  FROM [Pos_WorkStationStock]
  inner join Pos_Product on Pos_Product.Pos_ProductID=Pos_WorkStationStock.ProductID
  inner join ACC_ChartOfAccountLabel4 on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
  =Pos_Product.ProductID
 "+ (workStationID!=0 ?"where WorkStationID="+workStationID.ToString():"")+@"
  group by ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
";

        DataSet ds = CommonManager.SQLExec(sql);
            
        int serialNo = 1;

        string htmlTable = @" <table id='itemList_tbl' style='border:1px solid black;width:100%;' cellpadding='0' cellspacing='0'>
                        <tr id='tableHeader'>
                            <td  style='border-left:0px;'>S/L</td>
                            <td>Product Name</td>
                            <td>Qty</td>
                            <td>Amount</td>
                        </tr>
                            ";

        decimal totalStock = 0;
        decimal totalStockAmount = 0;

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
           if (decimal.Parse(dr["Stock"].ToString()) > 0
                ||
                decimal.Parse(dr["Stock"].ToString()) < 0
                )
           {
                totalStock += decimal.Parse(dr["Stock"].ToString());
                totalStockAmount += decimal.Parse(dr["Amount"].ToString());
                htmlTable += @"<tr  class='itemCss'><td  style='border-left:0px;'>" + (serialNo++).ToString() + @"</td><td>" + dr["Product"].ToString()
                    + @"</td><td style='text-align:right;'>" + decimal.Parse(dr["Stock"].ToString()).ToString("0,0")
                    + @"</td><td style='text-align:right;'>" + decimal.Parse(dr["Amount"].ToString()).ToString("0,0.00") + @"</td></tr>";
            }
        }
            htmlTable += @"<tr class='subtotalRow'><td  style='border-left:0px;'></td><td>
                    Total</td><td style='text-align:right;'>" + totalStock.ToString("0,0") 
                    + @"</td><td style='text-align:right;'>" + totalStockAmount.ToString("0,0.00") + @"</td></tr></table>";
            
            lblItemList.Text = htmlTable;
    }

    private void initialLoad()
    {
        lblPrintDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
    }


}