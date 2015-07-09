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

public class Pos_ProductManager
{
	public Pos_ProductManager()
	{
	}

    public static List<Pos_Product> GetAllPos_Products()
    {
        List<Pos_Product> pos_Products = new List<Pos_Product>();
        SqlPos_ProductProvider sqlPos_ProductProvider = new SqlPos_ProductProvider();
        pos_Products = sqlPos_ProductProvider.GetAllPos_Products();
        return pos_Products;
    }

    public static List<Pos_Product> GetAllPos_ProductsByTrasactionMasterID(int transactionMasterID)
    {
        List<Pos_Product> pos_Products = new List<Pos_Product>();
        SqlPos_ProductProvider sqlPos_ProductProvider = new SqlPos_ProductProvider();
        pos_Products = sqlPos_ProductProvider.GetAllPos_ProductsByTrasactionMasterID(transactionMasterID);
        return pos_Products;
    }


    public static List<Pos_Product> GetAllPos_ProductsByInventoryID(int InventoryID)
    {
        List<Pos_Product> pos_Products = new List<Pos_Product>();
        SqlPos_ProductProvider sqlPos_ProductProvider = new SqlPos_ProductProvider();
        pos_Products = sqlPos_ProductProvider.GetAllPos_ProductsByInventoryID(InventoryID);
        return pos_Products;
    }


    public static Pos_Product GetPos_ProductByID(int id)
    {
        Pos_Product pos_Product = new Pos_Product();
        SqlPos_ProductProvider sqlPos_ProductProvider = new SqlPos_ProductProvider();
        pos_Product = sqlPos_ProductProvider.GetPos_ProductByID(id);
        return pos_Product;
    }


    public static int InsertPos_Product(Pos_Product pos_Product)
    {
        SqlPos_ProductProvider sqlPos_ProductProvider = new SqlPos_ProductProvider();
        return sqlPos_ProductProvider.InsertPos_Product(pos_Product);
    }


    public static bool UpdatePos_Product(Pos_Product pos_Product)
    {
        SqlPos_ProductProvider sqlPos_ProductProvider = new SqlPos_ProductProvider();
        return sqlPos_ProductProvider.UpdatePos_Product(pos_Product);
    }

    public static bool DeletePos_Product(int pos_ProductID)
    {
        SqlPos_ProductProvider sqlPos_ProductProvider = new SqlPos_ProductProvider();
        return sqlPos_ProductProvider.DeletePos_Product(pos_ProductID);
    }
}
