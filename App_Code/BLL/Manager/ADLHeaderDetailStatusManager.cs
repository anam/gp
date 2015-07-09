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

public class ADLHeaderDetailStatusManager
{
	public ADLHeaderDetailStatusManager()
	{
	}

    public static List<ADLHeaderDetailStatus> GetAllADLHeaderDetailStatuss()
    {
        List<ADLHeaderDetailStatus> aDLHeaderDetailStatuss = new List<ADLHeaderDetailStatus>();
        SqlADLHeaderDetailStatusProvider sqlADLHeaderDetailStatusProvider = new SqlADLHeaderDetailStatusProvider();
        aDLHeaderDetailStatuss = sqlADLHeaderDetailStatusProvider.GetAllADLHeaderDetailStatuss();
        return aDLHeaderDetailStatuss;
    }


    public static ADLHeaderDetailStatus GetADLHeaderDetailStatusByID(int id)
    {
        ADLHeaderDetailStatus aDLHeaderDetailStatus = new ADLHeaderDetailStatus();
        SqlADLHeaderDetailStatusProvider sqlADLHeaderDetailStatusProvider = new SqlADLHeaderDetailStatusProvider();
        aDLHeaderDetailStatus = sqlADLHeaderDetailStatusProvider.GetADLHeaderDetailStatusByID(id);
        return aDLHeaderDetailStatus;
    }


    public static int InsertADLHeaderDetailStatus(ADLHeaderDetailStatus aDLHeaderDetailStatus)
    {
        SqlADLHeaderDetailStatusProvider sqlADLHeaderDetailStatusProvider = new SqlADLHeaderDetailStatusProvider();
        return sqlADLHeaderDetailStatusProvider.InsertADLHeaderDetailStatus(aDLHeaderDetailStatus);
    }


    public static bool UpdateADLHeaderDetailStatus(ADLHeaderDetailStatus aDLHeaderDetailStatus)
    {
        SqlADLHeaderDetailStatusProvider sqlADLHeaderDetailStatusProvider = new SqlADLHeaderDetailStatusProvider();
        return sqlADLHeaderDetailStatusProvider.UpdateADLHeaderDetailStatus(aDLHeaderDetailStatus);
    }

    public static bool DeleteADLHeaderDetailStatus(int aDLHeaderDetailStatusID)
    {
        SqlADLHeaderDetailStatusProvider sqlADLHeaderDetailStatusProvider = new SqlADLHeaderDetailStatusProvider();
        return sqlADLHeaderDetailStatusProvider.DeleteADLHeaderDetailStatus(aDLHeaderDetailStatusID);
    }
}
