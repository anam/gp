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

public class ADLHeaderDetailManager
{
	public ADLHeaderDetailManager()
	{
	}

    public static List<ADLHeaderDetail> GetAllADLHeaderDetails()
    {
        List<ADLHeaderDetail> aDLHeaderDetails = new List<ADLHeaderDetail>();
        SqlADLHeaderDetailProvider sqlADLHeaderDetailProvider = new SqlADLHeaderDetailProvider();
        aDLHeaderDetails = sqlADLHeaderDetailProvider.GetAllADLHeaderDetails();
        return aDLHeaderDetails;
    }

    public static List<ADLHeaderDetail> GetDefaultAfterInsertADLHeaderDetails(int ResidentID)
    {
        List<ADLHeaderDetail> aDLHeaderDetails = new List<ADLHeaderDetail>();
        SqlADLHeaderDetailProvider sqlADLHeaderDetailProvider = new SqlADLHeaderDetailProvider();
        aDLHeaderDetails = sqlADLHeaderDetailProvider.GetDefaultAfterInsertADLHeaderDetails(ResidentID);
        return aDLHeaderDetails;
    }


    public static List<ADLHeaderDetail> GetAllADLHeaderDetailsByResidentID(int ResidentID)
    {
        List<ADLHeaderDetail> aDLHeaderDetails = new List<ADLHeaderDetail>();
        SqlADLHeaderDetailProvider sqlADLHeaderDetailProvider = new SqlADLHeaderDetailProvider();
        aDLHeaderDetails = sqlADLHeaderDetailProvider.GetAllADLHeaderDetailsByResidentID( ResidentID);
        return aDLHeaderDetails;
    }


    public static ADLHeaderDetail GetADLHeaderDetailByID(int id)
    {
        ADLHeaderDetail aDLHeaderDetail = new ADLHeaderDetail();
        SqlADLHeaderDetailProvider sqlADLHeaderDetailProvider = new SqlADLHeaderDetailProvider();
        aDLHeaderDetail = sqlADLHeaderDetailProvider.GetADLHeaderDetailByID(id);
        return aDLHeaderDetail;
    }


    public static int InsertADLHeaderDetail(ADLHeaderDetail aDLHeaderDetail)
    {
        SqlADLHeaderDetailProvider sqlADLHeaderDetailProvider = new SqlADLHeaderDetailProvider();
        return sqlADLHeaderDetailProvider.InsertADLHeaderDetail(aDLHeaderDetail);
    }


    public static bool UpdateADLHeaderDetail(ADLHeaderDetail aDLHeaderDetail)
    {
        SqlADLHeaderDetailProvider sqlADLHeaderDetailProvider = new SqlADLHeaderDetailProvider();
        return sqlADLHeaderDetailProvider.UpdateADLHeaderDetail(aDLHeaderDetail);
    }

    public static bool DeleteADLHeaderDetail(int aDLHeaderDetailID)
    {
        SqlADLHeaderDetailProvider sqlADLHeaderDetailProvider = new SqlADLHeaderDetailProvider();
        return sqlADLHeaderDetailProvider.DeleteADLHeaderDetail(aDLHeaderDetailID);
    }
}
