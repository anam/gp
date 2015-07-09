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

public class ADLDetailManager
{
	public ADLDetailManager()
	{
	}

    public static List<ADLDetail> GetAllADLDetails()
    {
        List<ADLDetail> aDLDetails = new List<ADLDetail>();
        SqlADLDetailProvider sqlADLDetailProvider = new SqlADLDetailProvider();
        aDLDetails = sqlADLDetailProvider.GetAllADLDetails();
        return aDLDetails;
    }


    public static ADLDetail GetADLDetailByID(int id)
    {
        ADLDetail aDLDetail = new ADLDetail();
        SqlADLDetailProvider sqlADLDetailProvider = new SqlADLDetailProvider();
        aDLDetail = sqlADLDetailProvider.GetADLDetailByID(id);
        return aDLDetail;
    }


    public static int InsertADLDetail(ADLDetail aDLDetail)
    {
        SqlADLDetailProvider sqlADLDetailProvider = new SqlADLDetailProvider();
        return sqlADLDetailProvider.InsertADLDetail(aDLDetail);
    }


    public static bool UpdateADLDetail(ADLDetail aDLDetail)
    {
        SqlADLDetailProvider sqlADLDetailProvider = new SqlADLDetailProvider();
        return sqlADLDetailProvider.UpdateADLDetail(aDLDetail);
    }

    public static bool DeleteADLDetail(int aDLDetailID)
    {
        SqlADLDetailProvider sqlADLDetailProvider = new SqlADLDetailProvider();
        return sqlADLDetailProvider.DeleteADLDetail(aDLDetailID);
    }
}
