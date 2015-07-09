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
        string txtIDs_Text = Request.QueryString["barcodes"];

        string barcodes = "<table><tr>";
        int remaining = 4;

        string barcode = txtIDs_Text.Trim().Split('-')[0].Trim();
        string sql = @"Select Pos_Product.ProductName,Pos_Product.StyleCode,
        Pos_Product.SalePrice,        
        Pos_Size.SizeName,Pos_Product.BarCode,Pos_Product.IsVatExclusive,'' as VatText,Pos_Product.DiscountPercentage from Pos_Product
        inner join Pos_Size on Pos_Size.Pos_SizeID =Pos_Product.Pos_SizeID
        where Pos_Product.BarCode in (" + barcode + ")";

        DataSet ds = CommonManager.SQLExec(sql);


        for (int i = 0; i < int.Parse(txtIDs_Text.Trim().Split('-')[1]); i++)
        {
            try
            {

                if (barcode != "")
                {
                    if (i % 4 == 0)
                    {
                        barcodes += "</tr><tr>";
                        remaining = 3;
                    }
                    else
                    {
                        remaining--;
                    }

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr["BarCode"].ToString().Trim() == barcode)
                        {
                            if (decimal.Parse(dr["DiscountPercentage"].ToString()) == 0)
                            {
                                barcodes += "<td style='padding:15px 15px;font-size:12px;font-familly:tahoma;'>" + dr["ProductName"].ToString() + "&nbsp;&nbsp;(Size: " + dr["SizeName"].ToString() + ")<br/>" + dr["StyleCode"].ToString() + "&nbsp;&nbsp;<span style='font-family:Arial;font-weight:bold;'>GENTLE PARK</span><div style='overflow: hidden; margin-left: -12px; width: 157px; height: 39px; margin-top: 0px;'><img style='margin-top: -19px;' src='http://www.bcgen.com/demo/linear-dbgs.aspx?D=";
                                barcodes += barcode + "'/></div>TK. " + decimal.Parse(dr["SalePrice"].ToString()).ToString("0,0.00") + " (+ VAT)</td>";
                            }
                            else
                            {
                                barcodes += "<td style='padding:15px 15px;font-size:12px;font-familly:tahoma;'>" + dr["ProductName"].ToString() + "&nbsp;&nbsp;(Size: " + dr["SizeName"].ToString() + ")<br/>" + dr["StyleCode"].ToString() + "&nbsp;&nbsp;<span style='font-family:Arial;font-weight:bold;'>GENTLE PARK</span><div style='overflow: hidden; margin-left: -12px; width: 157px; height: 39px; margin-top: 0px;'><img style='margin-top: -19px;' src='http://www.bcgen.com/demo/linear-dbgs.aspx?D=";
                                barcodes += barcode + "'/></div><span style='font-size:10px;'>" + decimal.Parse(dr["DiscountPercentage"].ToString()).ToString("0.00") + " % Discount on TK. " + decimal.Parse(dr["SalePrice"].ToString()).ToString("0,0.00") + "</span><br/><span style='text-align:center; width:100%;font-weight:bold;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; TK." + (decimal.Parse(dr["SalePrice"].ToString()) * (100 - decimal.Parse(dr["DiscountPercentage"].ToString())) / 100).ToString("0,0.00") + "</span></td>";
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