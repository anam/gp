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

public class Inv_UtilizationDetailsManager
{
	public Inv_UtilizationDetailsManager()
	{
	}

    public static List<Inv_UtilizationDetails> GetAllInv_UtilizationDetailss()
    {
        List<Inv_UtilizationDetails> inv_UtilizationDetailss = new List<Inv_UtilizationDetails>();
        SqlInv_UtilizationDetailsProvider sqlInv_UtilizationDetailsProvider = new SqlInv_UtilizationDetailsProvider();
        inv_UtilizationDetailss = sqlInv_UtilizationDetailsProvider.GetAllInv_UtilizationDetailss();
        return inv_UtilizationDetailss;
    }


    public static Inv_UtilizationDetails GetInv_UtilizationDetailsByID(int id)
    {
        Inv_UtilizationDetails inv_UtilizationDetails = new Inv_UtilizationDetails();
        SqlInv_UtilizationDetailsProvider sqlInv_UtilizationDetailsProvider = new SqlInv_UtilizationDetailsProvider();
        inv_UtilizationDetails = sqlInv_UtilizationDetailsProvider.GetInv_UtilizationDetailsByID(id);
        return inv_UtilizationDetails;
    }


    public static int InsertInv_UtilizationDetails(Inv_UtilizationDetails inv_UtilizationDetails)
    {
        SqlInv_UtilizationDetailsProvider sqlInv_UtilizationDetailsProvider = new SqlInv_UtilizationDetailsProvider();
        return sqlInv_UtilizationDetailsProvider.InsertInv_UtilizationDetails(inv_UtilizationDetails);
    }


    public static bool UpdateInv_UtilizationDetails(Inv_UtilizationDetails inv_UtilizationDetails)
    {
        SqlInv_UtilizationDetailsProvider sqlInv_UtilizationDetailsProvider = new SqlInv_UtilizationDetailsProvider();
        return sqlInv_UtilizationDetailsProvider.UpdateInv_UtilizationDetails(inv_UtilizationDetails);
    }

    public static bool DeleteInv_UtilizationDetails(int inv_UtilizationDetailsID)
    {
        SqlInv_UtilizationDetailsProvider sqlInv_UtilizationDetailsProvider = new SqlInv_UtilizationDetailsProvider();
        return sqlInv_UtilizationDetailsProvider.DeleteInv_UtilizationDetails(inv_UtilizationDetailsID);
    }
}
