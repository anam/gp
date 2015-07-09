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

public class Inv_QuantityUnitManager
{
	public Inv_QuantityUnitManager()
	{
	}

    public static List<Inv_QuantityUnit> GetAllInv_QuantityUnits()
    {
        List<Inv_QuantityUnit> inv_QuantityUnits = new List<Inv_QuantityUnit>();
        SqlInv_QuantityUnitProvider sqlInv_QuantityUnitProvider = new SqlInv_QuantityUnitProvider();
        inv_QuantityUnits = sqlInv_QuantityUnitProvider.GetAllInv_QuantityUnits();
        return inv_QuantityUnits;
    }


    public static Inv_QuantityUnit GetInv_QuantityUnitByID(int id)
    {
        Inv_QuantityUnit inv_QuantityUnit = new Inv_QuantityUnit();
        SqlInv_QuantityUnitProvider sqlInv_QuantityUnitProvider = new SqlInv_QuantityUnitProvider();
        inv_QuantityUnit = sqlInv_QuantityUnitProvider.GetInv_QuantityUnitByID(id);
        return inv_QuantityUnit;
    }


    public static int InsertInv_QuantityUnit(Inv_QuantityUnit inv_QuantityUnit)
    {
        SqlInv_QuantityUnitProvider sqlInv_QuantityUnitProvider = new SqlInv_QuantityUnitProvider();
        return sqlInv_QuantityUnitProvider.InsertInv_QuantityUnit(inv_QuantityUnit);
    }


    public static bool UpdateInv_QuantityUnit(Inv_QuantityUnit inv_QuantityUnit)
    {
        SqlInv_QuantityUnitProvider sqlInv_QuantityUnitProvider = new SqlInv_QuantityUnitProvider();
        return sqlInv_QuantityUnitProvider.UpdateInv_QuantityUnit(inv_QuantityUnit);
    }

    public static bool DeleteInv_QuantityUnit(int inv_QuantityUnitID)
    {
        SqlInv_QuantityUnitProvider sqlInv_QuantityUnitProvider = new SqlInv_QuantityUnitProvider();
        return sqlInv_QuantityUnitProvider.DeleteInv_QuantityUnit(inv_QuantityUnitID);
    }
}
