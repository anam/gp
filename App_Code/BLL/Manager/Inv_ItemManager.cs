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

public class Inv_ItemManager
{
	public Inv_ItemManager()
	{
	}

    public static List<Inv_Item> GetAllInv_Items()
    {
        List<Inv_Item> inv_Items = new List<Inv_Item>();
        SqlInv_ItemProvider sqlInv_ItemProvider = new SqlInv_ItemProvider();
        inv_Items = sqlInv_ItemProvider.GetAllInv_Items();
        return inv_Items;
    }

    public static List<Inv_Item> GetAllInv_ItemsInStock()
    {
        List<Inv_Item> inv_Items = new List<Inv_Item>();
        SqlInv_ItemProvider sqlInv_ItemProvider = new SqlInv_ItemProvider();
        inv_Items = sqlInv_ItemProvider.GetAllInv_ItemsInStock();
        return inv_Items;
    }

    public static List<Inv_Item> GetAllInv_ItemsByIDs(string ids)
    {
        List<Inv_Item> inv_Items = new List<Inv_Item>();
        SqlInv_ItemProvider sqlInv_ItemProvider = new SqlInv_ItemProvider();
        inv_Items = sqlInv_ItemProvider.GetAllInv_ItemsByIDs(ids);
        return inv_Items;
    }

    public static List<Inv_Item> GetAllInv_ItemsInStockBySupplierID(int supplierID)
    {
        List<Inv_Item> inv_Items = new List<Inv_Item>();
        SqlInv_ItemProvider sqlInv_ItemProvider = new SqlInv_ItemProvider();
        inv_Items = sqlInv_ItemProvider.GetAllInv_ItemsInStockSupplierID(supplierID);
        return inv_Items;
    }


    public static List<Inv_Item> GetAllInv_ItemsByPurchaseID(int purchaseID)
    {
        List<Inv_Item> inv_Items = new List<Inv_Item>();
        SqlInv_ItemProvider sqlInv_ItemProvider = new SqlInv_ItemProvider();
        inv_Items = sqlInv_ItemProvider.GetAllInv_ItemsByPurchaseID(purchaseID);
        return inv_Items;
    }


    public static List<Inv_Item> GetAllInv_ItemsByPurchaseReturnID(int returnID)
    {
        List<Inv_Item> inv_Items = new List<Inv_Item>();
        SqlInv_ItemProvider sqlInv_ItemProvider = new SqlInv_ItemProvider();
        inv_Items = sqlInv_ItemProvider.GetAllInv_ItemsByPurchaseReturnID(returnID);
        return inv_Items;
    }


    public static List<Inv_Item> GetAllInv_ItemsByAdjustmentID(int returnID)
    {
        List<Inv_Item> inv_Items = new List<Inv_Item>();
        SqlInv_ItemProvider sqlInv_ItemProvider = new SqlInv_ItemProvider();
        inv_Items = sqlInv_ItemProvider.GetAllInv_ItemsByAdjustmentID(returnID);
        return inv_Items;
    }


    public static List<Inv_Item> GetAllInv_ItemsByUtilizationID(int returnID)
    {
        List<Inv_Item> inv_Items = new List<Inv_Item>();
        SqlInv_ItemProvider sqlInv_ItemProvider = new SqlInv_ItemProvider();
        inv_Items = sqlInv_ItemProvider.GetAllInv_ItemsByUtilizationID(returnID);
        return inv_Items;
    }

    public static List<Inv_Item> GetAllInv_ItemsByWastageID(int returnID)
    {
        List<Inv_Item> inv_Items = new List<Inv_Item>();
        SqlInv_ItemProvider sqlInv_ItemProvider = new SqlInv_ItemProvider();
        inv_Items = sqlInv_ItemProvider.GetAllInv_ItemsByWastageID(returnID);
        return inv_Items;
    }



    public static Inv_Item GetInv_ItemByID(int id)
    {
        Inv_Item inv_Item = new Inv_Item();
        SqlInv_ItemProvider sqlInv_ItemProvider = new SqlInv_ItemProvider();
        inv_Item = sqlInv_ItemProvider.GetInv_ItemByID(id);
        return inv_Item;
    }


    public static int InsertInv_Item(Inv_Item inv_Item)
    {
        SqlInv_ItemProvider sqlInv_ItemProvider = new SqlInv_ItemProvider();
        return sqlInv_ItemProvider.InsertInv_Item(inv_Item);
    }


    public static bool UpdateInv_Item(Inv_Item inv_Item)
    {
        SqlInv_ItemProvider sqlInv_ItemProvider = new SqlInv_ItemProvider();
        return sqlInv_ItemProvider.UpdateInv_Item(inv_Item);
    }

    public static bool DeleteInv_Item(int inv_ItemID)
    {
        SqlInv_ItemProvider sqlInv_ItemProvider = new SqlInv_ItemProvider();
        return sqlInv_ItemProvider.DeleteInv_Item(inv_ItemID);
    }
}
