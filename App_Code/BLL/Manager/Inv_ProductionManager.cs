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

public class Inv_ProductionManager
{
	public Inv_ProductionManager()
	{
	}

    public static List<Inv_Production> GetAllInv_Productions()
    {
        List<Inv_Production> inv_Productions = new List<Inv_Production>();
        SqlInv_ProductionProvider sqlInv_ProductionProvider = new SqlInv_ProductionProvider();
        inv_Productions = sqlInv_ProductionProvider.GetAllInv_Productions();
        return inv_Productions;
    }


    public static Inv_Production GetInv_ProductionByID(int id)
    {
        Inv_Production inv_Production = new Inv_Production();
        SqlInv_ProductionProvider sqlInv_ProductionProvider = new SqlInv_ProductionProvider();
        inv_Production = sqlInv_ProductionProvider.GetInv_ProductionByID(id);
        return inv_Production;
    }


    public static int InsertInv_Production(Inv_Production inv_Production)
    {
        SqlInv_ProductionProvider sqlInv_ProductionProvider = new SqlInv_ProductionProvider();
        return sqlInv_ProductionProvider.InsertInv_Production(inv_Production);
    }


    public static bool UpdateInv_Production(Inv_Production inv_Production)
    {
        SqlInv_ProductionProvider sqlInv_ProductionProvider = new SqlInv_ProductionProvider();
        return sqlInv_ProductionProvider.UpdateInv_Production(inv_Production);
    }

    public static bool DeleteInv_Production(int inv_ProductionID)
    {
        SqlInv_ProductionProvider sqlInv_ProductionProvider = new SqlInv_ProductionProvider();
        return sqlInv_ProductionProvider.DeleteInv_Production(inv_ProductionID);
    }
}
