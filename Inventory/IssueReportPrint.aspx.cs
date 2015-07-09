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

        string fromDate = (Request.QueryString["FromDate"] != null ? Request.QueryString["FromDate"].ToString() : "");
        string toDate = (Request.QueryString["ToDate"] != null ? Request.QueryString["ToDate"].ToString() : "");
        lblStockDate.Text = DateTime.Parse(fromDate).ToString("dd/MM/yyyy") + " to " + DateTime.Parse(toDate).ToString("dd/MM/yyyy");
        
        //purchase info
        string sql = @"select SUM(Inv_ItemTransaction.Quantity) as Quantity,Inv_QuantityUnit.QuantityUnitName
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
,ACC_ChartOfAccountLabel4.ExtraField1
,SUM(Inv_ItemTransaction.Quantity * Inv_Item.PricePerUnit) as TotalAmount  
from
Inv_Item 
inner join ACC_ChartOfAccountLabel4 on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID= Inv_Item.RawMaterialID
inner join Inv_QuantityUnit on Inv_QuantityUnit.Inv_QuantityUnitID=Inv_Item.QuantityUnitID
inner join Inv_ItemTransaction on Inv_ItemTransaction.ItemID =Inv_Item.Inv_ItemID
inner join Inv_IssueMaster on Inv_IssueMaster.Inv_IssueMasterID =Inv_ItemTransaction.ExtraField5
where Inv_Item.RowStatusID=1 and Inv_IssueMaster.RowStatusID=1 and Inv_ItemTransaction.RowStatusID=1 and Inv_ItemTransaction.ItemTrasactionTypeID =2 and  (Inv_IssueMaster.IssueDate between '" + fromDate + "' and '" + toDate + "')";
        if (workstationID != 0)
        {
            sql += " and Inv_IssueMaster.WorkSatationID =" + workstationID;
        }

        if (RawmaterialsTypeID != 0)
        {
            sql += " and ACC_ChartOfAccountLabel4.ACC_HeadTypeID =" + RawmaterialsTypeID;
        }
        sql += @" group by Inv_QuantityUnit.QuantityUnitName
                ,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
                ,ACC_ChartOfAccountLabel4.ExtraField1
                order by ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text;";


        if (workstationID != 0)
        {
            sql += "select ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4ID,ExtraField2 from ACC_ChartOfAccountLabel4 where ACC_ChartOfAccountLabel4ID =" + workstationID;
        }
        DataSet ds= CommonManager.SQLExec(sql);
        
        
        string lastSupplierID = "0";
            decimal Total = 0;
            decimal Subtotal = 0;
            decimal TotalAmount = 0;
            decimal SubtotalAmount = 0;
            int serialNo = 1;

            string htmlTable = @" <table id='itemList_tbl' style='border:1px solid black;width:100%;' cellpadding='0' cellspacing='0'>";
            if (workstationID != 0)
            {
                htmlTable += @"<tr>
                            <td colspan='4' style='padding-left:50px;border-top:1px solid black;'>
                                <b>WorkStation name:</b> " + ds.Tables[1].Rows[0]["ChartOfAccountLabel4Text"].ToString() + @"
                            </td>
                            <td colspan='2' style=' border-top:1px solid black;'>
                                <b>WorkStation ID:</b> " + ds.Tables[1].Rows[0]["ACC_ChartOfAccountLabel4ID"].ToString() + @"
                            </td>
                        </tr>
                        <tr>
                            <td colspan='7' style='padding-left:50px; border-bottom:1px solid black;'>
                                <b>Transaction Date:</b> " + DateTime.Parse(fromDate).ToString("dd MMM yyyy") + " To " + DateTime.Parse(toDate).ToString("dd MMM yyyy") + @"
                            </td>
                        </tr>";
            }
            else
            {
                htmlTable += @"<tr>
                            <td colspan='6' style='padding-left:50px; border-top:1px solid black;border-bottom:1px solid black;'>
                                All WorkStation
                            </td>
                        </tr>";
            }
            htmlTable += @"<tr id='tableHeader'>
                            <td  style='border-left:0px;'>S/L</td>
                            <td>Item Code</td>
                            <td>Item Name</td>
                            <td>Quantity</td>
                            <td>Unit</td>
                            <td>Amount</td>
                        </tr>
                        ";


            foreach (DataRow dr in ds.Tables[0].Rows)
            {
//                if (lastSupplierID != "0" && lastSupplierID != dr["SupplierID"].ToString())
//                {
//                    htmlTable += @"<tr class='subtotalRow'>
//                            <td>&nbsp;</td>
//                            <td>&nbsp;</td>
//                            <td>Sub Total</td>
//                            <td>" + Subtotal.ToString("0,0.00") + @"</td>
//                            <td>&nbsp;</td>
//                            <td>" + SubtotalAmount.ToString("0,0.00") + @"</td>
//                        </tr>";

//                    Subtotal = 0;
//                    SubtotalAmount = 0;
//                }

//                if (lastSupplierID != dr["SupplierID"].ToString())
//                {
//                    lastSupplierID = dr["SupplierID"].ToString();
//                    htmlTable += @"<tr>
//                            <td colspan='4' style='padding-left:50px; padding-top:20px;border-top:1px solid black;'>
//                                <b>Supplier name:</b> " + dr["SupplierName"].ToString() + @"
//                            </td>
//                            <td colspan='2' style=' border-top:1px solid black;padding-top:20px;'>
//                                <b>Supplier ID:</b> " + dr["SupplierID"].ToString() + @"
//                            </td>
//                        </tr>
//                        <tr>
//                            <td colspan='7' style='padding-left:50px;'>
//                                <b>Supplier Address:</b> " + dr["SupplierAddress"].ToString() + @"
//                            </td>
//                        </tr>
//                        <tr>
//                            <td colspan='7' style='padding-left:50px; border-bottom:1px solid black;'>
//                                <b>Transaction Date:</b> " + DateTime.Parse(fromDate).ToString("dd MMM yyyy") + " To " + DateTime.Parse(toDate).ToString("dd MMM yyyy") + @"
//                            </td>
//                        </tr>
//                        <tr id='tableHeader'>
//                            <td  style='border-left:0px;'>S/L</td>
//                            <td>Item Code</td>
//                            <td>Item Name</td>
//                            <td>Quantity</td>
//                            <td>Unit</td>
//                            <td>Amount</td>
//                        </tr>";
//                }

                htmlTable += @"<tr class='itemCss'>
                            <td  style='border-left:0px;'>" + (serialNo++).ToString() + @"</td>
                            <td>" + dr["ExtraField1"].ToString() + @"</td>
                            <td>" + dr["ChartOfAccountLabel4Text"].ToString() + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["Quantity"].ToString()).ToString("0,0.00") + @"</td>
                            <td >" + dr["QuantityUnitName"].ToString() + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["TotalAmount"].ToString()).ToString("0,0.00") + @"</td>
                        </tr>";

                Subtotal += decimal.Parse(dr["Quantity"].ToString());
                SubtotalAmount += decimal.Parse(dr["TotalAmount"].ToString());

                Total += decimal.Parse(dr["Quantity"].ToString());
                TotalAmount += decimal.Parse(dr["TotalAmount"].ToString());
            }

//            htmlTable += @"<tr class='subtotalRow'>
//                            <td>&nbsp;</td>
//                            <td>&nbsp;</td>
//                            <td>Sub Total</td>
//                            <td>" + Subtotal.ToString("0,0.00") + @"</td>
//                            <td>&nbsp;</td>
//                            <td>" + SubtotalAmount.ToString("0,0.00") + @"</td>
//                        </tr>";

            htmlTable += @"<tr id='lastRow'>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>Total</td>
                        <td>" + Total.ToString("0,0.00") + @"</td>
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