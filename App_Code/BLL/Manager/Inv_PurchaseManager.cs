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

public class Inv_PurchaseManager
{
	public Inv_PurchaseManager()
	{
	}

    public static List<Inv_Purchase> GetAllInv_Purchases()
    {
        List<Inv_Purchase> inv_Purchases = new List<Inv_Purchase>();
        SqlInv_PurchaseProvider sqlInv_PurchaseProvider = new SqlInv_PurchaseProvider();
        inv_Purchases = sqlInv_PurchaseProvider.GetAllInv_Purchases();
        return inv_Purchases;
    }


    public static List<Inv_Purchase> GetAllInv_PurchasesByDateNSupplierID(string SQL)
    {
        List<Inv_Purchase> inv_Purchases = new List<Inv_Purchase>();
        SqlInv_PurchaseProvider sqlInv_PurchaseProvider = new SqlInv_PurchaseProvider();
        inv_Purchases = sqlInv_PurchaseProvider.GetAllInv_PurchasesByDateNSupplierID(SQL);
        return inv_Purchases;
    }


    public static Inv_Purchase GetInv_PurchaseByID(int id)
    {
        Inv_Purchase inv_Purchase = new Inv_Purchase();
        SqlInv_PurchaseProvider sqlInv_PurchaseProvider = new SqlInv_PurchaseProvider();
        inv_Purchase = sqlInv_PurchaseProvider.GetInv_PurchaseByID(id);
        return inv_Purchase;
    }


    public static int InsertInv_Purchase(Inv_Purchase inv_Purchase)
    {
        SqlInv_PurchaseProvider sqlInv_PurchaseProvider = new SqlInv_PurchaseProvider();
        return sqlInv_PurchaseProvider.InsertInv_Purchase(inv_Purchase);
    }


    public static bool UpdateInv_Purchase(Inv_Purchase inv_Purchase)
    {
        SqlInv_PurchaseProvider sqlInv_PurchaseProvider = new SqlInv_PurchaseProvider();
        return sqlInv_PurchaseProvider.UpdateInv_Purchase(inv_Purchase);
    }

    public static bool DeleteInv_Purchase(int inv_PurchaseID)
    {
        SqlInv_PurchaseProvider sqlInv_PurchaseProvider = new SqlInv_PurchaseProvider();
        return sqlInv_PurchaseProvider.DeleteInv_Purchase(inv_PurchaseID);
    }
}
