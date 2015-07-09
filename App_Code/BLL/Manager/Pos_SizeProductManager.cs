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

public class Pos_SizeProductManager
{
	public Pos_SizeProductManager()
	{
	}

    public static List<Pos_SizeProduct> GetAllPos_SizeProducts()
    {
        List<Pos_SizeProduct> pos_SizeProducts = new List<Pos_SizeProduct>();
        SqlPos_SizeProductProvider sqlPos_SizeProductProvider = new SqlPos_SizeProductProvider();
        pos_SizeProducts = sqlPos_SizeProductProvider.GetAllPos_SizeProducts();
        return pos_SizeProducts;
    }


    public static Pos_SizeProduct GetPos_SizeProductByID(int id)
    {
        Pos_SizeProduct pos_SizeProduct = new Pos_SizeProduct();
        SqlPos_SizeProductProvider sqlPos_SizeProductProvider = new SqlPos_SizeProductProvider();
        pos_SizeProduct = sqlPos_SizeProductProvider.GetPos_SizeProductByID(id);
        return pos_SizeProduct;
    }


    public static int InsertPos_SizeProduct(Pos_SizeProduct pos_SizeProduct)
    {
        SqlPos_SizeProductProvider sqlPos_SizeProductProvider = new SqlPos_SizeProductProvider();
        return sqlPos_SizeProductProvider.InsertPos_SizeProduct(pos_SizeProduct);
    }


    public static bool UpdatePos_SizeProduct(Pos_SizeProduct pos_SizeProduct)
    {
        SqlPos_SizeProductProvider sqlPos_SizeProductProvider = new SqlPos_SizeProductProvider();
        return sqlPos_SizeProductProvider.UpdatePos_SizeProduct(pos_SizeProduct);
    }

    public static bool DeletePos_SizeProduct(int pos_SizeProductID)
    {
        SqlPos_SizeProductProvider sqlPos_SizeProductProvider = new SqlPos_SizeProductProvider();
        return sqlPos_SizeProductProvider.DeletePos_SizeProduct(pos_SizeProductID);
    }
}
