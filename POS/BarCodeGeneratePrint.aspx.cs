using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Inventory_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadBarCode();
        }
    }

    private void loadBarCode()
    {
        string barCodewithQty = Request.QueryString["barcodes"];
        if (!barCodewithQty.Trim().Contains(","))
        {
            barCodewithQty = barCodewithQty + ",";
        }
        
        string txtIDs_Text = "";
        foreach (string item in barCodewithQty.Split(','))
        {
            if (item.Contains('-'))
            {
                for (int i = 0; i < int.Parse(item.Split('-')[1]); i++)
                {
                    txtIDs_Text += (txtIDs_Text == "" ? "" : ",") + item.Split('-')[0];
                }
            }
        }
        string barcodes = "<table border='0' cellpadding='0' cellspacing='0'><tr>";
        int remaining = 5;
        
        if (!txtIDs_Text.Trim().Contains(","))
        {
            txtIDs_Text = txtIDs_Text + ",";
        }

        string sql = @"Select Pos_Product.ProductName,Pos_Product.StyleCode,
        (Pos_Product.SalePrice +(Pos_Product.SalePrice * Pos_Product.VatPercentage / 100)) as SalePrice,        
        Pos_Size.SizeName,Pos_Product.BarCode,Pos_Product.IsVatExclusive,'' as VatText,Pos_Product.DiscountPercentage from Pos_Product
        inner join Pos_Size on Pos_Size.Pos_SizeID =Pos_Product.Pos_SizeID
        where Pos_Product.BarCode in ('" + txtIDs_Text.Replace(",","','")+"')";

        DataSet ds = CommonManager.SQLExec(sql);
        bool is1stRow = true;
        int rowNo = 1;
        string tr_StyleWithOutDiscount = "withOutDiscount";
        string tr_StyleWithDiscount = "withDiscount";
        for (int i = 0; i < txtIDs_Text.Trim().Split(',').Length; i++)
        {
            try
            {
                string test = txtIDs_Text.Trim().Split(',')[i];
                if (test != "")
                {
                    //tr_StyleWithOutDiscount = "withOutDiscount";
                    //tr_StyleWithDiscount = "withDiscount";
                    //if (is1stRow)
                    //{
                    //    tr_StyleWithOutDiscount = "withOutDiscount1stRow";
                    //    tr_StyleWithDiscount = "withDiscount1stRow";
                        
                    //}

                    if (i % 5 == 0)
                    {
                        if (i != 0)
                        {
                            rowNo += 1;
                            is1stRow = false;
                            barcodes += "</tr><tr>";
                        }

                        if (rowNo == 12)
                        {
                            rowNo = 1;
                            is1stRow = true;
                            tr_StyleWithOutDiscount = "withOutDiscount";
                            tr_StyleWithDiscount = "withDiscount";
                            if (is1stRow)
                            {
                                tr_StyleWithOutDiscount = "withOutDiscount1stRow";
                                tr_StyleWithDiscount = "withDiscount1stRow";

                            }
                        }
                        else
                        {
                            tr_StyleWithOutDiscount = "withOutDiscount";
                            tr_StyleWithDiscount = "withDiscount";
                            if (is1stRow)
                            {
                                tr_StyleWithOutDiscount = "withOutDiscount1stRow";
                                tr_StyleWithDiscount = "withDiscount1stRow";

                            }
                        }
                            remaining = 4;
                    }
                    else
                    {
                        remaining--;
                    }
                    

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr["BarCode"].ToString().Trim() == txtIDs_Text.Trim().Split(',')[i])
                        {
                            if (decimal.Parse(dr["DiscountPercentage"].ToString()) == 0)
                            {
                                barcodes += "<td class='" + tr_StyleWithOutDiscount + "'>" + dr["ProductName"].ToString() + "<br/>Size: " + dr["SizeName"].ToString() + "&nbsp;&nbsp;<span style='font-family:Arial;font-weight:bold;'>GENTLE PARK</span><div style='overflow: hidden; margin-left: -12px; width: 157px; height: 39px; margin-top: 0px;'><img style='margin-top: -19px;' src='http://www.bcgen.com/demo/linear-dbgs.aspx?D=";
                                barcodes += txtIDs_Text.Trim().Split(',')[i] + "'/></div>"+ "&nbsp;&nbsp;" + dr["StyleCode"].ToString() +"<br/>TK. " + decimal.Parse(dr["SalePrice"].ToString()).ToString("0,0.00") + " (Inclusive)</td>";
                            }
                            else
                            {
                                barcodes += "<td class='" + tr_StyleWithDiscount + "'>" + dr["ProductName"].ToString() + "&nbsp;&nbsp;(Size: " + dr["SizeName"].ToString() + ")<br/>" + dr["StyleCode"].ToString() + "&nbsp;&nbsp;<span style='font-family:Arial;font-weight:bold;'>GENTLE PARK</span><div style='overflow: hidden; margin-left: -12px; width: 157px; height: 39px; margin-top: 0px;'><img style='margin-top: -19px;' src='http://www.bcgen.com/demo/linear-dbgs.aspx?D=";
                                barcodes += txtIDs_Text.Trim().Split(',')[i] + "'/></div><span style='font-size:10px;'>" + decimal.Parse(dr["DiscountPercentage"].ToString()).ToString("0.00") + " % Discount on TK. " + decimal.Parse(dr["SalePrice"].ToString()).ToString("0,0.00") + "</span><br/><span style='text-align:center; width:100%;font-weight:bold;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; TK." + (decimal.Parse(dr["SalePrice"].ToString()) * (100 - decimal.Parse(dr["DiscountPercentage"].ToString())) / 100).ToString("0,0.00") + "  (Inclusive)</span></td>";
                            }
                        }
                    }                    
                }
            }
            catch (Exception ex)
            { }
        }

        for (int i = 0; i < remaining; i++)
        {
            barcodes += "<td></td>";
        }
        barcodes += "</tr></table>";

        lblbarcode.Text = barcodes;
    }
}