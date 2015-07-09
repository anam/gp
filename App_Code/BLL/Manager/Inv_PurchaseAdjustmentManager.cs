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

public class Inv_PurchaseAdjustmentManager
{
	public Inv_PurchaseAdjustmentManager()
	{
	}

    public static List<Inv_PurchaseAdjustment> GetAllInv_PurchaseAdjustments()
    {
        List<Inv_PurchaseAdjustment> inv_PurchaseAdjustments = new List<Inv_PurchaseAdjustment>();
        SqlInv_PurchaseAdjustmentProvider sqlInv_PurchaseAdjustmentProvider = new SqlInv_PurchaseAdjustmentProvider();
        inv_PurchaseAdjustments = sqlInv_PurchaseAdjustmentProvider.GetAllInv_PurchaseAdjustments();
        return inv_PurchaseAdjustments;
    }


    public static Inv_PurchaseAdjustment GetInv_PurchaseAdjustmentByID(int id)
    {
        Inv_PurchaseAdjustment inv_PurchaseAdjustment = new Inv_PurchaseAdjustment();
        SqlInv_PurchaseAdjustmentProvider sqlInv_PurchaseAdjustmentProvider = new SqlInv_PurchaseAdjustmentProvider();
        inv_PurchaseAdjustment = sqlInv_PurchaseAdjustmentProvider.GetInv_PurchaseAdjustmentByID(id);
        return inv_PurchaseAdjustment;
    }


    public static int InsertInv_PurchaseAdjustment(Inv_PurchaseAdjustment inv_PurchaseAdjustment)
    {
        SqlInv_PurchaseAdjustmentProvider sqlInv_PurchaseAdjustmentProvider = new SqlInv_PurchaseAdjustmentProvider();
        return sqlInv_PurchaseAdjustmentProvider.InsertInv_PurchaseAdjustment(inv_PurchaseAdjustment);
    }


    public static bool UpdateInv_PurchaseAdjustment(Inv_PurchaseAdjustment inv_PurchaseAdjustment)
    {
        SqlInv_PurchaseAdjustmentProvider sqlInv_PurchaseAdjustmentProvider = new SqlInv_PurchaseAdjustmentProvider();
        return sqlInv_PurchaseAdjustmentProvider.UpdateInv_PurchaseAdjustment(inv_PurchaseAdjustment);
    }

    public static bool DeleteInv_PurchaseAdjustment(int inv_PurchaseAdjustmentID)
    {
        SqlInv_PurchaseAdjustmentProvider sqlInv_PurchaseAdjustmentProvider = new SqlInv_PurchaseAdjustmentProvider();
        return sqlInv_PurchaseAdjustmentProvider.DeleteInv_PurchaseAdjustment(inv_PurchaseAdjustmentID);
    }
}
