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

public class Inv_PurchaseReturenManager
{
	public Inv_PurchaseReturenManager()
	{
	}

    public static List<Inv_PurchaseReturen> GetAllInv_PurchaseReturens()
    {
        List<Inv_PurchaseReturen> inv_PurchaseReturens = new List<Inv_PurchaseReturen>();
        SqlInv_PurchaseReturenProvider sqlInv_PurchaseReturenProvider = new SqlInv_PurchaseReturenProvider();
        inv_PurchaseReturens = sqlInv_PurchaseReturenProvider.GetAllInv_PurchaseReturens();
        return inv_PurchaseReturens;
    }


    public static List<Inv_PurchaseReturen> GetAllInv_PurchaseReturensByDateNSupplierID(string sql)
    {
        List<Inv_PurchaseReturen> inv_PurchaseReturens = new List<Inv_PurchaseReturen>();
        SqlInv_PurchaseReturenProvider sqlInv_PurchaseReturenProvider = new SqlInv_PurchaseReturenProvider();
        inv_PurchaseReturens = sqlInv_PurchaseReturenProvider.GetAllInv_PurchaseReturensByDateNSupplierID(sql);
        return inv_PurchaseReturens;
    }

    public static Inv_PurchaseReturen GetInv_PurchaseReturenByID(int id)
    {
        Inv_PurchaseReturen inv_PurchaseReturen = new Inv_PurchaseReturen();
        SqlInv_PurchaseReturenProvider sqlInv_PurchaseReturenProvider = new SqlInv_PurchaseReturenProvider();
        inv_PurchaseReturen = sqlInv_PurchaseReturenProvider.GetInv_PurchaseReturenByID(id);
        return inv_PurchaseReturen;
    }


    public static int InsertInv_PurchaseReturen(Inv_PurchaseReturen inv_PurchaseReturen)
    {
        SqlInv_PurchaseReturenProvider sqlInv_PurchaseReturenProvider = new SqlInv_PurchaseReturenProvider();
        return sqlInv_PurchaseReturenProvider.InsertInv_PurchaseReturen(inv_PurchaseReturen);
    }


    public static bool UpdateInv_PurchaseReturen(Inv_PurchaseReturen inv_PurchaseReturen)
    {
        SqlInv_PurchaseReturenProvider sqlInv_PurchaseReturenProvider = new SqlInv_PurchaseReturenProvider();
        return sqlInv_PurchaseReturenProvider.UpdateInv_PurchaseReturen(inv_PurchaseReturen);
    }

    public static bool DeleteInv_PurchaseReturen(int inv_PurchaseReturenID)
    {
        SqlInv_PurchaseReturenProvider sqlInv_PurchaseReturenProvider = new SqlInv_PurchaseReturenProvider();
        return sqlInv_PurchaseReturenProvider.DeleteInv_PurchaseReturen(inv_PurchaseReturenID);
    }
}
