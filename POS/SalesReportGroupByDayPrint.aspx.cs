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
        string fromDate = (Request.QueryString["FromDate"] != null ? Request.QueryString["FromDate"].ToString() : "");
        string toDate = (Request.QueryString["ToDate"] != null ? Request.QueryString["ToDate"].ToString() : "");
        lblStockDate.Text = DateTime.Parse(fromDate).ToString("dd/MM/yyyy") + " to " + DateTime.Parse(toDate).ToString("dd/MM/yyyy");

        int WorkStationID = 0;
        try
        {
            WorkStationID = Int32.Parse(Request.QueryString["WorkStationID"]);
        }
        catch (Exception ex)
        {
            WorkStationID = 0;
        }


        //purchase info
        string sql = @"Select SUM( cast(Pos_Transaction.ExtraField1 as decimal(10,2))) as discount
,sum( cast(Pos_Transaction.ExtraField2 as decimal(10,2))) as VatAmout
,(cast(Pos_TransactionMaster.ExtraField1 as decimal(10,2))) as CashPayment
,cast(Pos_TransactionMaster.ExtraField5 as decimal(10,2)) as Due
,cast(Pos_TransactionMaster.ExtraField2 as decimal(10,2)) as CardPayment
,cast(Pos_TransactionMaster.ExtraField1 as decimal(10,2))  
+ cast(Pos_TransactionMaster.ExtraField2 as decimal(10,2)) as NetSale
,cast(Pos_TransactionMaster.ExtraField1 as decimal(10,2))  
+ cast(Pos_TransactionMaster.ExtraField2 as decimal(10,2)) 
- sum( cast(Pos_Transaction.ExtraField2 as decimal(10,2)))
- SUM( cast(Pos_Transaction.ExtraField1 as decimal(10,2))) as GrossSale1
,Pos_TransactionMaster.TransactionDate
,Pos_TransactionMaster.TransactionID
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
,Pos_TransactionMaster.Pos_TransactionMasterID
,SUM(SalePrice) as GrossSale
,Pos_TransactionMaster.UpdatedBy
from Pos_Transaction 
inner join Pos_TransactionMaster
on Pos_TransactionMaster.Pos_TransactionMasterID =Pos_Transaction.Pos_ProductTransactionMasterID
inner join Pos_Product
on Pos_Product.Pos_ProductID =Pos_Transaction.Pos_ProductID
inner join ACC_ChartOfAccountLabel4
on Pos_TransactionMaster.WorkSatationID =ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
where Pos_TransactionMaster.RowStatusID=1 and Pos_TransactionMaster.Pos_TransactionTypeID=13 ";

        if (WorkStationID != 0)
        {
            sql += " and Pos_TransactionMaster.WorkSatationID =" + WorkStationID;
        }
        sql+=@"
                        and Pos_TransactionMaster.TransactionDate between '" + DateTime.Parse(fromDate).ToString("yyyy-MM-dd") + @"' and '" + DateTime.Parse(toDate).ToString("yyyy-MM-dd") + @"  11:59 PM'
                        group by Pos_TransactionMaster.ExtraField1,Pos_TransactionMaster.ExtraField5

,Pos_TransactionMaster.UpdatedBy,Pos_TransactionMaster.ExtraField2,
Pos_TransactionMaster.TransactionDate,
ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
,Pos_TransactionMaster.TransactionID
,Pos_TransactionMaster.Pos_TransactionMasterID
order by ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
,Pos_TransactionMaster.TransactionDate
,Pos_TransactionMaster.TransactionID
                        ";

        DataSet ds = CommonManager.SQLExec(sql);
            
        int serialNo = 1;

        string htmlTable = @" <table id='itemList_tbl' style='border:1px solid black;width:805px;' cellpadding='0' cellspacing='0'>
                        <tr id='tableHeader'>
                            <td  style='border-left:0px;'>S/L</td>
                            <td style='width:140px;' colspan='2'>Invoice Date</td>
                            
                            <td>Sales Price</td>
                            <td>Vat Amount</td>
                            <td>Discount</td>
                            <td>Dis %</td>
                            <td>Cash Payment</td>
                            <td>Card Payment</td>
                            <td>Net Sales Amount</td>
                            <td>Note</td>
                        </tr>
                            ";

        decimal subtotalGrossSale = 0;
        decimal subtotalDiscount = 0;
        decimal subtotalVat = 0;
        decimal subtotalNetSale = 0;
        decimal subtotalCashPayment = 0;
        decimal subtotalCardPayment = 0;

        decimal totalGrossSale = 0;
        decimal totalDiscount = 0;
        decimal totalVat = 0;
        decimal totalNetSale = 0;
        decimal totalCashPayment = 0;
        decimal totalCardPayment = 0;

        decimal grandtotalGrossSale = 0;
        decimal grandtotalDiscount = 0;
        decimal grandtotalVat = 0;
        decimal grandtotalNetSale = 0;
        decimal grandtotalCashPayment = 0;
        decimal grandtotalCardPayment = 0;
        
        string lastShowRoom = "";
        string lastDate = "";

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (decimal.Parse(dr["NetSale"].ToString()) > 0)
            {
                if (decimal.Parse(dr["CashPayment"].ToString()) > decimal.Parse(dr["Due"].ToString()))
                {
                    dr["CashPayment"] = (decimal.Parse(dr["CashPayment"].ToString()) - decimal.Parse(dr["Due"].ToString()));
                }
                else
                {
                    dr["CardPayment"] = (decimal.Parse(dr["CardPayment"].ToString()) - decimal.Parse(dr["Due"].ToString()));
                }

                dr["NetSale"] = (decimal.Parse(dr["CardPayment"].ToString()) + decimal.Parse(dr["CashPayment"].ToString()));
               
                if (lastShowRoom == "")
                {
                    htmlTable += @" <tr id='tableHeader'>
                            <td  style='border-left:0px;'></td>
                            <td colspan='10' style='text-align:left;'>Show Room :" + dr["ChartOfAccountLabel4Text"].ToString() + @"</td>
                        </tr>
                            ";
                    lastShowRoom = dr["ChartOfAccountLabel4Text"].ToString();
                    /*
                    htmlTable += @" <tr id='tableHeader'>
                            <td  style='border-left:0px;'></td>
                            <td colspan='10' style='text-align:left;'>Date :" + DateTime.Parse(dr["TransactionDate"].ToString()).ToString("dd MMM yyyy") + @"</td>
                        </tr>
                            ";
                    */
                    lastDate = DateTime.Parse(dr["TransactionDate"].ToString()).ToString("dd MMM yyyy");
                }
                else
                    if (lastShowRoom != dr["ChartOfAccountLabel4Text"].ToString())
                    {
                        htmlTable += @" <tr class='itemCss'>
                            <td  style='border-left:0px;'></td>
                            <td style='text-align:right;' colspan='2'>" + lastDate + @"</td>
                            <td style='text-align:right;' >" + totalGrossSale.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + totalVat.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + totalDiscount.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' ></td>
                            <td style='text-align:right;' >" + totalCashPayment.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + totalCardPayment.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + totalNetSale.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' ></td>
                        </tr>
                        <tr  id='lastRow'>
                            <td  style='border-left:0px;'></td>
                            <td style='text-align:right;' colspan='2'>Sub Total</td>
                            <td style='text-align:right;' >" + subtotalGrossSale.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + subtotalVat.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + subtotalDiscount.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' ></td>
                            <td style='text-align:right;' >" + subtotalCashPayment.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + subtotalCardPayment.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + subtotalNetSale.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' ></td>
                        </tr>
                            ";

                        htmlTable += @" <tr  id='tableHeader'>
                            <td  style='border-left:0px;'></td>
                            <td colspan='10' style='text-align:left;'>Show Room :" + dr["ChartOfAccountLabel4Text"].ToString() + @"</td>
                        </tr>
                            ";
                        /*
                        htmlTable += @" <tr  id='tableHeader'>
                            <td  style='border-left:0px;'></td>
                            <td colspan='10' style='text-align:left;'>Date :" + DateTime.Parse(dr["TransactionDate"].ToString()).ToString("dd MMM yyyy") + @"</td>
                        </tr>
                            ";
                        */
                        lastShowRoom = dr["ChartOfAccountLabel4Text"].ToString();
                        lastDate = DateTime.Parse(dr["TransactionDate"].ToString()).ToString("dd MMM yyyy");

                        totalGrossSale = 0;
                        totalDiscount = 0;
                        totalVat = 0;
                        totalNetSale = 0;
                        totalCashPayment = 0;
                        totalCardPayment = 0;

                        subtotalGrossSale = 0;
                        subtotalDiscount = 0;
                        subtotalVat = 0;
                        subtotalNetSale = 0;
                        subtotalCashPayment = 0;
                        subtotalCardPayment = 0;
                    }
                    else
                        if (lastDate != DateTime.Parse(dr["TransactionDate"].ToString()).ToString("dd MMM yyyy"))
                        {
                            htmlTable += @" <tr class='itemCss'>
                            <td  style='border-left:0px;'></td>
                            <td style='text-align:right;' colspan='2'>" + lastDate + @"</td>
                            <td style='text-align:right;' >" + totalGrossSale.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + totalVat.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + totalDiscount.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' ></td>
                            <td style='text-align:right;' >" + totalCashPayment.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + totalCardPayment.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + totalNetSale.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' ></td>
                        </tr>
                            ";
                            /*
                            htmlTable += @" <tr  id='tableHeader'>
                            <td  style='border-left:0px;'></td>
                            <td colspan='10' style='text-align:left;'>Date :" + DateTime.Parse(dr["TransactionDate"].ToString()).ToString("dd MMM yyyy") + @"</td>
                        </tr>
                            ";
                            */
                            lastDate = DateTime.Parse(dr["TransactionDate"].ToString()).ToString("dd MMM yyyy");


                            totalGrossSale = 0;
                            totalDiscount = 0;
                            totalVat = 0;
                            totalNetSale = 0;
                            totalCashPayment = 0;
                            totalCardPayment = 0;
                        }


                totalGrossSale += decimal.Parse(dr["GrossSale"].ToString());
                totalDiscount += decimal.Parse(dr["discount"].ToString());
                totalVat += decimal.Parse(dr["VatAmout"].ToString());
                totalNetSale += decimal.Parse(dr["NetSale"].ToString());
                totalCashPayment += decimal.Parse(dr["CashPayment"].ToString());
                totalCardPayment += decimal.Parse(dr["CardPayment"].ToString());


                subtotalGrossSale += decimal.Parse(dr["GrossSale"].ToString());
                subtotalDiscount += decimal.Parse(dr["discount"].ToString());
                subtotalVat += decimal.Parse(dr["VatAmout"].ToString());
                subtotalNetSale += decimal.Parse(dr["NetSale"].ToString());
                subtotalCashPayment += decimal.Parse(dr["CashPayment"].ToString());
                subtotalCardPayment += decimal.Parse(dr["CardPayment"].ToString());

                grandtotalGrossSale += decimal.Parse(dr["GrossSale"].ToString());
                grandtotalDiscount += decimal.Parse(dr["discount"].ToString());
                grandtotalVat += decimal.Parse(dr["VatAmout"].ToString());
                grandtotalNetSale += decimal.Parse(dr["NetSale"].ToString());
                grandtotalCashPayment += decimal.Parse(dr["CashPayment"].ToString());
                grandtotalCardPayment += decimal.Parse(dr["CardPayment"].ToString());

                string color = "";
                if (decimal.Parse(dr["UpdatedBy"].ToString())<0)
                    color = "style='color:Red;'";
                /*
                htmlTable += @" <tr class='itemCss'>
                            <td  style='border-left:0px;'>" + (serialNo++).ToString() + @"</td>
                            <td>" + DateTime.Parse(dr["TransactionDate"].ToString()).ToString("dd MMM yyyy") + @"</td>
                            <td><a "+color+@" href='SalesPrint.aspx?Pos_TransactionMasterID="+dr["Pos_TransactionMasterID"].ToString()+"'  target='_target'>" + decimal.Parse(dr["TransactionID"].ToString()).ToString("00000") + @"</a></td>
                            <td style='text-align:right;' >" + decimal.Parse(dr["GrossSale"].ToString()).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + decimal.Parse(dr["VatAmout"].ToString()).ToString("0,0.00") + @"</td>
                            <td style='text-align:right=;' >" + decimal.Parse(dr["discount"].ToString()).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >0.00</td>
                            <td style='text-align:right;' >" + decimal.Parse(dr["CashPayment"].ToString()).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + decimal.Parse(dr["CardPayment"].ToString()).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + decimal.Parse(dr["NetSale"].ToString()).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' ></td>
                        </tr>
                            ";
                 */
            }
        }
        htmlTable += @" <tr  class='itemCss'>
                            <td  style='border-left:0px;'></td>
                            <td style='text-align:right;' colspan='2'>" + lastDate + @"</td>
                            <td style='text-align:right;' >" + totalGrossSale.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + totalVat.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + totalDiscount.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' ></td>
                            <td style='text-align:right;' >" + totalCashPayment.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + totalCardPayment.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + totalNetSale.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' ></td>
                        </tr>
                        <tr id='lastRow'>
                            <td  style='border-left:0px;'></td>
                            <td style='text-align:right;' colspan='2'>Sub Total</td>
                            <td style='text-align:right;' >" + subtotalGrossSale.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + subtotalVat.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + subtotalDiscount.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' ></td>
                            <td style='text-align:right;' >" + subtotalCashPayment.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + subtotalCardPayment.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + subtotalNetSale.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' ></td>
                        </tr>
                         <tr id='lastRow'>
                             <td  style='border-left:0px;'></td>
                            <td style='text-align:right;' colspan='2'>Grand Total</td>
                            <td style='text-align:right;' >" + grandtotalGrossSale.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + grandtotalVat.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + grandtotalDiscount.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' ></td>
                            <td style='text-align:right;' >" + grandtotalCashPayment.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + grandtotalCardPayment.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' >" + grandtotalNetSale.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;' ></td>
                        </tr>
                           </table>  ";

        lblItemList.Text = htmlTable;
    }

    private void initialLoad()
    {
        lblPrintDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
    }


}