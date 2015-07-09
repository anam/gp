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

public class ADLStatusManager
{
	public ADLStatusManager()
	{
	}

    public static List<ADLStatus> GetAllADLStatuss()
    {
        List<ADLStatus> aDLStatuss = new List<ADLStatus>();
        SqlADLStatusProvider sqlADLStatusProvider = new SqlADLStatusProvider();
        aDLStatuss = sqlADLStatusProvider.GetAllADLStatuss();
        return aDLStatuss;
    }


    public static ADLStatus GetADLStatusByID(int id)
    {
        ADLStatus aDLStatus = new ADLStatus();
        SqlADLStatusProvider sqlADLStatusProvider = new SqlADLStatusProvider();
        aDLStatus = sqlADLStatusProvider.GetADLStatusByID(id);
        return aDLStatus;
    }


    public static int InsertADLStatus(ADLStatus aDLStatus)
    {
        SqlADLStatusProvider sqlADLStatusProvider = new SqlADLStatusProvider();
        return sqlADLStatusProvider.InsertADLStatus(aDLStatus);
    }


    public static bool UpdateADLStatus(ADLStatus aDLStatus)
    {
        SqlADLStatusProvider sqlADLStatusProvider = new SqlADLStatusProvider();
        return sqlADLStatusProvider.UpdateADLStatus(aDLStatus);
    }

    public static bool DeleteADLStatus(int aDLStatusID)
    {
        SqlADLStatusProvider sqlADLStatusProvider = new SqlADLStatusProvider();
        return sqlADLStatusProvider.DeleteADLStatus(aDLStatusID);
    }
}
