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

public class Inv_WastageManager
{
	public Inv_WastageManager()
	{
	}

    public static List<Inv_Wastage> GetAllInv_Wastages()
    {
        List<Inv_Wastage> inv_Wastages = new List<Inv_Wastage>();
        SqlInv_WastageProvider sqlInv_WastageProvider = new SqlInv_WastageProvider();
        inv_Wastages = sqlInv_WastageProvider.GetAllInv_Wastages();
        return inv_Wastages;
    }


    public static Inv_Wastage GetInv_WastageByID(int id)
    {
        Inv_Wastage inv_Wastage = new Inv_Wastage();
        SqlInv_WastageProvider sqlInv_WastageProvider = new SqlInv_WastageProvider();
        inv_Wastage = sqlInv_WastageProvider.GetInv_WastageByID(id);
        return inv_Wastage;
    }


    public static int InsertInv_Wastage(Inv_Wastage inv_Wastage)
    {
        SqlInv_WastageProvider sqlInv_WastageProvider = new SqlInv_WastageProvider();
        return sqlInv_WastageProvider.InsertInv_Wastage(inv_Wastage);
    }


    public static bool UpdateInv_Wastage(Inv_Wastage inv_Wastage)
    {
        SqlInv_WastageProvider sqlInv_WastageProvider = new SqlInv_WastageProvider();
        return sqlInv_WastageProvider.UpdateInv_Wastage(inv_Wastage);
    }

    public static bool DeleteInv_Wastage(int inv_WastageID)
    {
        SqlInv_WastageProvider sqlInv_WastageProvider = new SqlInv_WastageProvider();
        return sqlInv_WastageProvider.DeleteInv_Wastage(inv_WastageID);
    }
}
