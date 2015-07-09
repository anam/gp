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

        int suppliyerID = 0;
        try
        {
            suppliyerID = Int32.Parse(Request.QueryString["SuppliyerID"]);
        }
        catch (Exception ex)
        {
            suppliyerID = 0;
        }

        int productID = 0;
        try
        {
            productID = Int32.Parse(Request.QueryString["ProductID"]);
        }
        catch (Exception ex)
        {
            productID = 0;
        }

        int WorkStationID = 0;
        try
        {
            WorkStationID = Int32.Parse(Request.QueryString["WorkStationID"]);
        }
        catch (Exception ex)
        {
            WorkStationID = 0;
        }

        int TransactionTypeID = 0;
        try
        {
            TransactionTypeID = Int32.Parse(Request.QueryString["TransactionTypeID"]);
        }
        catch (Exception ex)
        {
            TransactionTypeID = 0;
        }

        string fromDate = (Request.QueryString["FromDate"] != null ? Request.QueryString["FromDate"].ToString() : "");
        string toDate = (Request.QueryString["ToDate"] != null ? Request.QueryString["ToDate"].ToString() : "");
        lblStockDate.Text = DateTime.Parse(fromDate).ToString("dd/MM/yyyy") + " to " + DateTime.Parse(toDate).ToString("dd/MM/yyyy");

        //purchase info
        string sql = @"select SUM(Pos_Transaction.Quantity) as Quantity
,Inv_QuantityUnit.QuantityUnitName
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
,ACC_ChartOfAccountLabel4.ExtraField1
,SUM(Pos_Transaction.Quantity * Pos_Product.SalePrice) as TotalAmount 
,(SUM(Pos_Transaction.Quantity * Pos_Product.SalePrice)/SUM(Pos_Transaction.Quantity)) as UnitPrice 
,Pos_Product.BarCode,Pos_Product.ProductID,Pos_Size.SizeName,Pos_Product.Pic2
from
Pos_Product 
inner join Pos_Size on Pos_Size.Pos_SizeID= Pos_Product.Pos_SizeID
inner join ACC_ChartOfAccountLabel4 on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID= Pos_Product.ProductID
inner join Inv_QuantityUnit on Inv_QuantityUnit.Inv_QuantityUnitID=Pos_Product.Inv_QuantityUnitID
inner join Pos_Transaction on Pos_Transaction.Pos_ProductID =Pos_Product.Pos_ProductID
inner join Pos_TransactionMaster on Pos_TransactionMaster.Pos_TransactionMasterID = Pos_Transaction.Pos_ProductTransactionMasterID
where   Pos_TransactionMaster.Pos_TransactionTypeID =" + TransactionTypeID + @" and  (Pos_TransactionMaster.TransactionDate between '" + fromDate + "' and '" + toDate + "')";
        if (suppliyerID != 0)
        {
            sql += " and Pos_TransactionMaster.ToOrFromID =" + suppliyerID;
        }

        if (productID != 0)
        {
            sql += " and Pos_Product.ProductID =" + productID;
        }

        if (WorkStationID != 0)
        {
            if (
                TransactionTypeID == 9
                //||
                //TransactionTypeID == 12
                )
                sql += " and Pos_TransactionMaster.ToOrFromID =" + WorkStationID;
            else
                sql += " and Pos_TransactionMaster.WorkSatationID =" + WorkStationID;
        }

        sql += @"
group by Inv_QuantityUnit.QuantityUnitName
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
,ACC_ChartOfAccountLabel4.ExtraField1
,Pos_Product.BarCode,Pos_Product.ProductID,Pos_Size.SizeName,Pos_Product.Pic2
order by Pos_Product.ProductID,Pos_Product.BarCode;";


        sql += @"
select distinct 0 as colNo, Pos_Product.Pos_SizeID,Pos_Size.SizeName, 0 as Counting, 0 as GrandTotal,0 as Subtotal
from
Pos_Product 
inner join Pos_Size on Pos_Size.Pos_SizeID= Pos_Product.Pos_SizeID
inner join ACC_ChartOfAccountLabel4 on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID= Pos_Product.ProductID
inner join Inv_QuantityUnit on Inv_QuantityUnit.Inv_QuantityUnitID=Pos_Product.Inv_QuantityUnitID
inner join Pos_Transaction on Pos_Transaction.Pos_ProductID =Pos_Product.Pos_ProductID
inner join Pos_TransactionMaster on Pos_TransactionMaster.Pos_TransactionMasterID = Pos_Transaction.Pos_ProductTransactionMasterID
where   Pos_TransactionMaster.Pos_TransactionTypeID ="+TransactionTypeID+@" and  (Pos_TransactionMaster.TransactionDate between '" + fromDate + "' and '" + toDate + "')";
        if (suppliyerID != 0)
        {
            sql += " and Pos_TransactionMaster.ToOrFromID =" + suppliyerID;
        }
        if (productID != 0)
        {
            sql += " and Pos_Product.ProductID =" + productID;
        }

        if (WorkStationID != 0)
        {
            if (
                TransactionTypeID == 9
                //||
                //TransactionTypeID == 12
                )
                sql += " and Pos_TransactionMaster.ToOrFromID =" + WorkStationID;
            else
                sql += " and Pos_TransactionMaster.WorkSatationID =" + WorkStationID;
        }

        sql += @"
order by Pos_Product.Pos_SizeID;
";

        if (suppliyerID != 0)
        {
            sql += "select ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4ID,ExtraField2 from ACC_ChartOfAccountLabel4 where ACC_ChartOfAccountLabel4ID =" + suppliyerID;
        }
        else
        if (WorkStationID != 0)
        {
            sql += "select ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4ID,ExtraField2 from ACC_ChartOfAccountLabel4 where ACC_ChartOfAccountLabel4ID =" + WorkStationID;
        }

        lblVoucherType.Text = Pos_TransactionTypeManager.GetPos_TransactionTypeByID(TransactionTypeID).TransactionTypeName;
        

        DataSet ds= CommonManager.SQLExec(sql);
        int i=0;
        string SizeHeader = "<table cellspacing='0'><tr>";
        foreach (DataRow dr in ds.Tables[1].Rows)
        {
            dr["colNo"] = ++i;
            SizeHeader += "<td style='border:1px solid black;min-width:26px;'>" + dr["SizeName"].ToString() + "</td>";
        }
        SizeHeader += "</tr></table>";

        string ProductID = "0";
            decimal Total = 0;
            decimal Subtotal = 0;
            decimal TotalAmount = 0;
            decimal SubtotalAmount = 0;
            int serialNo = 1;

            string htmlTable = @" <table id='itemList_tbl' style='border:1px solid black;width:100%;' cellpadding='0' cellspacing='0'>
                        ";

            if (suppliyerID != 0)
            {
                htmlTable += @"<tr>
                            <td colspan='2' style='padding-left:50px;border-top:1px solid black;'>
                                <b>Supplier name:</b> " + ds.Tables[2].Rows[0]["ChartOfAccountLabel4Text"].ToString() + @"
                            </td>
                            <td colspan='2' style=' border-top:1px solid black;'>
                                <b>Supplier ID:</b> " + ds.Tables[2].Rows[0]["ACC_ChartOfAccountLabel4ID"].ToString() + @"
                            </td>
                        </tr>
                        <tr>
                            <td colspan='4' style='padding-left:50px;'>
                                <b>Supplier Address:</b> " + ds.Tables[2].Rows[0]["ExtraField2"].ToString() + @"
                            </td>
                        </tr>
                        <tr>
                            <td colspan='4' style='padding-left:50px; border-bottom:1px solid black;'>
                                <b>Transaction Date:</b> " + DateTime.Parse(fromDate).ToString("dd MMM yyyy") + " To " + DateTime.Parse(toDate).ToString("dd MMM yyyy") + @"
                            </td>
                        </tr>";
            }
            if (WorkStationID != 0)
            {
                htmlTable += @"<tr>
                            <td colspan='2' style='padding-left:50px;border-top:1px solid black;'>
                                <b>WorkStation name:</b> " + ds.Tables[2].Rows[0]["ChartOfAccountLabel4Text"].ToString() + @"
                            </td>
                            <td colspan='2' style=' border-top:1px solid black;'>
                                <b>ID:</b> " + ds.Tables[2].Rows[0]["ACC_ChartOfAccountLabel4ID"].ToString() + @"
                            </td>
                        </tr>
                        <tr>
                            <td colspan='4' style='padding-left:50px;'>
                                <b>Address:</b> " + ds.Tables[2].Rows[0]["ExtraField2"].ToString() + @"
                            </td>
                        </tr>
                        <tr>
                            <td colspan='4' style='padding-left:50px; border-bottom:1px solid black;'>
                                <b>Transaction Date:</b> " + DateTime.Parse(fromDate).ToString("dd MMM yyyy") + " To " + DateTime.Parse(toDate).ToString("dd MMM yyyy") + @"
                            </td>
                        </tr>";
            }
            else
            {
                htmlTable += @"<tr>
                            <td colspan='4' style='padding-left:50px; border-top:1px solid black;border-bottom:1px solid black;'>
                                All Workstation
                            </td>
                        </tr>";
            }

            string lastBarCode = "";
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (lastBarCode != dr["Pic2"].ToString())
                {
                    if (lastBarCode != "")
                    {
                        foreach (DataRow drsize in ds.Tables[1].Rows)
                        {
                            htmlTable += "<td style='border:1px solid black;min-width:26px;'>";
                            if (int.Parse(drsize["Counting"].ToString()) != 0)
                            {
                                htmlTable += drsize["Counting"].ToString();
                            }
                            else
                            {
                                htmlTable += "&nbsp;";
                            }
                            htmlTable += "</td>";
                            drsize["Counting"] = 0;
                        }
                        htmlTable += @"</tr></table></td>
                            </tr>";
                    }
                }
                if (ProductID != "0" && ProductID != dr["ProductID"].ToString())
                {
                    htmlTable += @"<tr class='subtotalRow'>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>Sub Total</td>
                            <td><table><tr>";
                    foreach (DataRow drsize in ds.Tables[1].Rows)
                    {
                        htmlTable += "<td style='border:1px solid black;min-width:26px;'>";
                        if (int.Parse(drsize["Subtotal"].ToString()) != 0)
                        {
                            htmlTable += drsize["Subtotal"].ToString();
                        }
                        else
                        {
                            htmlTable += "&nbsp;";
                        }
                        htmlTable += "</td>";
                        drsize["Subtotal"] = 0;
                    }

                    htmlTable+= @"</tr></table></td>
                            </tr>";

                    Subtotal = 0;
                    SubtotalAmount = 0;
                }

                if (ProductID != dr["ProductID"].ToString())
                {
                    ProductID = dr["ProductID"].ToString();
                    htmlTable += @"<tr>
                            <td colspan='4' style='padding-left:50px; border-top:1px solid black;border-bottom:1px solid black;'>
                                <b>Item:</b> " + dr["ChartOfAccountLabel4Text"].ToString() + @"
                            </td>
                        </tr>
                        <tr id='tableHeader'>
                            <td  style='border-left:0px;'>S/L</td>
                            <td>Item Code</td>
                            <td>Item Name</td>
                            <td>"+SizeHeader+@"
                            </td>
                        </tr>";
                }

                if (lastBarCode != dr["Pic2"].ToString())
                {
                    
                    htmlTable += @"<tr class='itemCss'>
                            <td  style='border-left:0px;'>" + (serialNo++).ToString() + @"</td>
                            <td>" + dr["Pic2"].ToString() + @"</td>
                            <td>" + dr["ChartOfAccountLabel4Text"].ToString() + @"</td>
                            <td style='padding-left:50px;padding:5px;'><table cellspacing='0'><tr>";
                                  
                   lastBarCode = dr["Pic2"].ToString();
                   
                }

                if (lastBarCode == dr["Pic2"].ToString())
                {
                    foreach (DataRow drsize in ds.Tables[1].Rows)
                    {
                        if (dr["SizeName"].ToString() == drsize["SizeName"].ToString())
                        {
                            drsize["Counting"] = int.Parse(dr["Quantity"].ToString().Split('.')[0]);
                            drsize["GrandTotal"] = int.Parse(drsize["GrandTotal"].ToString()) + int.Parse(dr["Quantity"].ToString().Split('.')[0]);
                            drsize["Subtotal"] = int.Parse(drsize["Subtotal"].ToString()) + int.Parse(dr["Quantity"].ToString().Split('.')[0]);
                        }
                    }
                }
                /*
                htmlTable += @"<tr class='itemCss'>
                            <td  style='border-left:0px;'>" + (serialNo++).ToString() + @"</td>
                            <td>" + dr["BarCode"].ToString() + @"</td>
                            <td>" + dr["ChartOfAccountLabel4Text"].ToString() + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["Quantity"].ToString()).ToString("0,0.00") + @"</td>
                            </tr>";
                */

                Subtotal += decimal.Parse(dr["Quantity"].ToString());
                SubtotalAmount += decimal.Parse(dr["TotalAmount"].ToString());

                Total += decimal.Parse(dr["Quantity"].ToString());
                TotalAmount += decimal.Parse(dr["TotalAmount"].ToString());
            }

        //last item
            foreach (DataRow drsize in ds.Tables[1].Rows)
            {
                htmlTable += "<td style='border:1px solid black;min-width:26px;'>";
                if (int.Parse(drsize["Counting"].ToString()) != 0)
                {
                    htmlTable += drsize["Counting"].ToString();
                }
                else
                {
                    htmlTable += "&nbsp;";
                }
                htmlTable += "</td>";
                drsize["Counting"] = 0;
            }
            htmlTable += @"</tr></table></td>
                            </tr>";
           

        //last item subtotal
            htmlTable += @"<tr class='subtotalRow'>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>Sub Total</td>
                            <td><table><tr>";
            foreach (DataRow drsize in ds.Tables[1].Rows)
            {
                htmlTable += "<td style='border:1px solid black;min-width:26px;'>";
                if (int.Parse(drsize["Subtotal"].ToString()) != 0)
                {
                    htmlTable += drsize["Subtotal"].ToString();
                }
                else
                {
                    htmlTable += "&nbsp;";
                }
                htmlTable += "</td>";
                drsize["Subtotal"] = 0;
            }

            htmlTable += @"</tr></table></td>
                            </tr>";

            //grand total
            htmlTable += @"<tr id='lastRow'>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>Sub Total</td>
                            <td><table><tr>";
            foreach (DataRow drsize in ds.Tables[1].Rows)
            {
                htmlTable += "<td style='border:1px solid black;min-width:26px;'>";
                if (int.Parse(drsize["GrandTotal"].ToString()) != 0)
                {
                    htmlTable += drsize["GrandTotal"].ToString();
                }
                else
                {
                    htmlTable += "&nbsp;";
                }
                htmlTable += "</td>";
                drsize["GrandTotal"] = 0;
            }

            htmlTable += @"</tr></table></td>
                            </tr></table>";
       

        lblItemList.Text += htmlTable;
    }

    private void initialLoad()
    {
        lblPrintDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
    }
}