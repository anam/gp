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

public class Inv_ProductManager
{
	public Inv_ProductManager()
	{
	}

    public static List<Inv_Product> GetAllInv_Products()
    {
        List<Inv_Product> inv_Products = new List<Inv_Product>();
        SqlInv_ProductProvider sqlInv_ProductProvider = new SqlInv_ProductProvider();
        inv_Products = sqlInv_ProductProvider.GetAllInv_Products();
        return inv_Products;
    }


    public static Inv_Product GetInv_ProductByID(int id)
    {
        Inv_Product inv_Product = new Inv_Product();
        SqlInv_ProductProvider sqlInv_ProductProvider = new SqlInv_ProductProvider();
        inv_Product = sqlInv_ProductProvider.GetInv_ProductByID(id);
        return inv_Product;
    }


    public static int InsertInv_Product(Inv_Product inv_Product)
    {
        SqlInv_ProductProvider sqlInv_ProductProvider = new SqlInv_ProductProvider();
        return sqlInv_ProductProvider.InsertInv_Product(inv_Product);
    }


    public static bool UpdateInv_Product(Inv_Product inv_Product)
    {
        SqlInv_ProductProvider sqlInv_ProductProvider = new SqlInv_ProductProvider();
        return sqlInv_ProductProvider.UpdateInv_Product(inv_Product);
    }

    public static bool DeleteInv_Product(int inv_ProductID)
    {
        SqlInv_ProductProvider sqlInv_ProductProvider = new SqlInv_ProductProvider();
        return sqlInv_ProductProvider.DeleteInv_Product(inv_ProductID);
    }
}
