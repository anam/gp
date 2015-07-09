using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public class Inv_ProductDetailsManager
{
	public Inv_ProductDetailsManager()
	{
	}

    public static List<Inv_ProductDetails> GetAllInv_ProductDetailss()
    {
        List<Inv_ProductDetails> inv_ProductDetailss = new List<Inv_ProductDetails>();
        SqlInv_ProductDetailsProvider sqlInv_ProductDetailsProvider = new SqlInv_ProductDetailsProvider();
        inv_ProductDetailss = sqlInv_ProductDetailsProvider.GetAllInv_ProductDetailss();
        return inv_ProductDetailss;
    }


    public static Inv_ProductDetails GetInv_ProductDetailsByID(int id)
    {
        Inv_ProductDetails inv_ProductDetails = new Inv_ProductDetails();
        SqlInv_ProductDetailsProvider sqlInv_ProductDetailsProvider = new SqlInv_ProductDetailsProvider();
        inv_ProductDetails = sqlInv_ProductDetailsProvider.GetInv_ProductDetailsByID(id);
        return inv_ProductDetails;
    }


    public static int InsertInv_ProductDetails(Inv_ProductDetails inv_ProductDetails)
    {
        SqlInv_ProductDetailsProvider sqlInv_ProductDetailsProvider = new SqlInv_ProductDetailsProvider();
        return sqlInv_ProductDetailsProvider.InsertInv_ProductDetails(inv_ProductDetails);
    }


    public static bool UpdateInv_ProductDetails(Inv_ProductDetails inv_ProductDetails)
    {
        SqlInv_ProductDetailsProvider sqlInv_ProductDetailsProvider = new SqlInv_ProductDetailsProvider();
        return sqlInv_ProductDetailsProvider.UpdateInv_ProductDetails(inv_ProductDetails);
    }

    public static bool DeleteInv_ProductDetails(int inv_ProductDetailsID)
    {
        SqlInv_ProductDetailsProvider sqlInv_ProductDetailsProvider = new SqlInv_ProductDetailsProvider();
        return sqlInv_ProductDetailsProvider.DeleteInv_ProductDetails(inv_ProductDetailsID);
    }
}
