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

public class ADLDetailnStatusManager
{
	public ADLDetailnStatusManager()
	{
	}

    public static List<ADLDetailnStatus> GetAllADLDetailnStatuss()
    {
        List<ADLDetailnStatus> aDLDetailnStatuss = new List<ADLDetailnStatus>();
        SqlADLDetailnStatusProvider sqlADLDetailnStatusProvider = new SqlADLDetailnStatusProvider();
        aDLDetailnStatuss = sqlADLDetailnStatusProvider.GetAllADLDetailnStatuss();
        return aDLDetailnStatuss;
    }


    public static List<ADLDetailnStatus> GetAllADLDetailnStatussByADLDetailID(int adlDetailID)
    {
        List<ADLDetailnStatus> aDLDetailnStatuss = new List<ADLDetailnStatus>();
        SqlADLDetailnStatusProvider sqlADLDetailnStatusProvider = new SqlADLDetailnStatusProvider();
        aDLDetailnStatuss = sqlADLDetailnStatusProvider.GetAllADLDetailnStatussByADLDetailID(adlDetailID);
        return aDLDetailnStatuss;
    }


    public static ADLDetailnStatus GetADLDetailnStatusByID(int id)
    {
        ADLDetailnStatus aDLDetailnStatus = new ADLDetailnStatus();
        SqlADLDetailnStatusProvider sqlADLDetailnStatusProvider = new SqlADLDetailnStatusProvider();
        aDLDetailnStatus = sqlADLDetailnStatusProvider.GetADLDetailnStatusByID(id);
        return aDLDetailnStatus;
    }


    public static int InsertADLDetailnStatus(ADLDetailnStatus aDLDetailnStatus)
    {
        SqlADLDetailnStatusProvider sqlADLDetailnStatusProvider = new SqlADLDetailnStatusProvider();
        return sqlADLDetailnStatusProvider.InsertADLDetailnStatus(aDLDetailnStatus);
    }


    public static bool UpdateADLDetailnStatus(ADLDetailnStatus aDLDetailnStatus)
    {
        SqlADLDetailnStatusProvider sqlADLDetailnStatusProvider = new SqlADLDetailnStatusProvider();
        return sqlADLDetailnStatusProvider.UpdateADLDetailnStatus(aDLDetailnStatus);
    }

    public static bool DeleteADLDetailnStatus(int aDLDetailnStatusID)
    {
        SqlADLDetailnStatusProvider sqlADLDetailnStatusProvider = new SqlADLDetailnStatusProvider();
        return sqlADLDetailnStatusProvider.DeleteADLDetailnStatus(aDLDetailnStatusID);
    }
}
