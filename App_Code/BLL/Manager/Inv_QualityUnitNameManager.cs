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

public class Inv_QualityUnitNameManager
{
	public Inv_QualityUnitNameManager()
	{
	}

    public static List<Inv_QualityUnitName> GetAllInv_QualityUnitNames()
    {
        List<Inv_QualityUnitName> inv_QualityUnitNames = new List<Inv_QualityUnitName>();
        SqlInv_QualityUnitNameProvider sqlInv_QualityUnitNameProvider = new SqlInv_QualityUnitNameProvider();
        inv_QualityUnitNames = sqlInv_QualityUnitNameProvider.GetAllInv_QualityUnitNames();
        return inv_QualityUnitNames;
    }


    public static Inv_QualityUnitName GetInv_QualityUnitNameByID(int id)
    {
        Inv_QualityUnitName inv_QualityUnitName = new Inv_QualityUnitName();
        SqlInv_QualityUnitNameProvider sqlInv_QualityUnitNameProvider = new SqlInv_QualityUnitNameProvider();
        inv_QualityUnitName = sqlInv_QualityUnitNameProvider.GetInv_QualityUnitNameByID(id);
        return inv_QualityUnitName;
    }


    public static int InsertInv_QualityUnitName(Inv_QualityUnitName inv_QualityUnitName)
    {
        SqlInv_QualityUnitNameProvider sqlInv_QualityUnitNameProvider = new SqlInv_QualityUnitNameProvider();
        return sqlInv_QualityUnitNameProvider.InsertInv_QualityUnitName(inv_QualityUnitName);
    }


    public static bool UpdateInv_QualityUnitName(Inv_QualityUnitName inv_QualityUnitName)
    {
        SqlInv_QualityUnitNameProvider sqlInv_QualityUnitNameProvider = new SqlInv_QualityUnitNameProvider();
        return sqlInv_QualityUnitNameProvider.UpdateInv_QualityUnitName(inv_QualityUnitName);
    }

    public static bool DeleteInv_QualityUnitName(int inv_QualityUnitNameID)
    {
        SqlInv_QualityUnitNameProvider sqlInv_QualityUnitNameProvider = new SqlInv_QualityUnitNameProvider();
        return sqlInv_QualityUnitNameProvider.DeleteInv_QualityUnitName(inv_QualityUnitNameID);
    }
}
