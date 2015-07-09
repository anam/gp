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

public class Inv_UtilizationManager
{
	public Inv_UtilizationManager()
	{
	}

    public static List<Inv_Utilization> GetAllInv_Utilizations()
    {
        List<Inv_Utilization> inv_Utilizations = new List<Inv_Utilization>();
        SqlInv_UtilizationProvider sqlInv_UtilizationProvider = new SqlInv_UtilizationProvider();
        inv_Utilizations = sqlInv_UtilizationProvider.GetAllInv_Utilizations();
        return inv_Utilizations;
    }


    public static Inv_Utilization GetInv_UtilizationByID(int id)
    {
        Inv_Utilization inv_Utilization = new Inv_Utilization();
        SqlInv_UtilizationProvider sqlInv_UtilizationProvider = new SqlInv_UtilizationProvider();
        inv_Utilization = sqlInv_UtilizationProvider.GetInv_UtilizationByID(id);
        return inv_Utilization;
    }


    public static int InsertInv_Utilization(Inv_Utilization inv_Utilization)
    {
        SqlInv_UtilizationProvider sqlInv_UtilizationProvider = new SqlInv_UtilizationProvider();
        return sqlInv_UtilizationProvider.InsertInv_Utilization(inv_Utilization);
    }


    public static bool UpdateInv_Utilization(Inv_Utilization inv_Utilization)
    {
        SqlInv_UtilizationProvider sqlInv_UtilizationProvider = new SqlInv_UtilizationProvider();
        return sqlInv_UtilizationProvider.UpdateInv_Utilization(inv_Utilization);
    }

    public static bool DeleteInv_Utilization(int inv_UtilizationID)
    {
        SqlInv_UtilizationProvider sqlInv_UtilizationProvider = new SqlInv_UtilizationProvider();
        return sqlInv_UtilizationProvider.DeleteInv_Utilization(inv_UtilizationID);
    }
}
