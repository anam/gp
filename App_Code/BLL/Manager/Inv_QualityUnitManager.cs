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

public class Inv_QualityUnitManager
{
	public Inv_QualityUnitManager()
	{
	}

    public static List<Inv_QualityUnit> GetAllInv_QualityUnits()
    {
        List<Inv_QualityUnit> inv_QualityUnits = new List<Inv_QualityUnit>();
        SqlInv_QualityUnitProvider sqlInv_QualityUnitProvider = new SqlInv_QualityUnitProvider();
        inv_QualityUnits = sqlInv_QualityUnitProvider.GetAllInv_QualityUnits();
        return inv_QualityUnits;
    }


    public static Inv_QualityUnit GetInv_QualityUnitByID(int id)
    {
        Inv_QualityUnit inv_QualityUnit = new Inv_QualityUnit();
        SqlInv_QualityUnitProvider sqlInv_QualityUnitProvider = new SqlInv_QualityUnitProvider();
        inv_QualityUnit = sqlInv_QualityUnitProvider.GetInv_QualityUnitByID(id);
        return inv_QualityUnit;
    }


    public static int InsertInv_QualityUnit(Inv_QualityUnit inv_QualityUnit)
    {
        SqlInv_QualityUnitProvider sqlInv_QualityUnitProvider = new SqlInv_QualityUnitProvider();
        return sqlInv_QualityUnitProvider.InsertInv_QualityUnit(inv_QualityUnit);
    }


    public static bool UpdateInv_QualityUnit(Inv_QualityUnit inv_QualityUnit)
    {
        SqlInv_QualityUnitProvider sqlInv_QualityUnitProvider = new SqlInv_QualityUnitProvider();
        return sqlInv_QualityUnitProvider.UpdateInv_QualityUnit(inv_QualityUnit);
    }

    public static bool DeleteInv_QualityUnit(int inv_QualityUnitID)
    {
        SqlInv_QualityUnitProvider sqlInv_QualityUnitProvider = new SqlInv_QualityUnitProvider();
        return sqlInv_QualityUnitProvider.DeleteInv_QualityUnit(inv_QualityUnitID);
    }
}
