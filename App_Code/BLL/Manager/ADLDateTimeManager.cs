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

public class ADLDateTimeManager
{
	public ADLDateTimeManager()
	{
	}

    public static List<ADLDateTime> GetAllADLDateTimes()
    {
        List<ADLDateTime> aDLDateTimes = new List<ADLDateTime>();
        SqlADLDateTimeProvider sqlADLDateTimeProvider = new SqlADLDateTimeProvider();
        aDLDateTimes = sqlADLDateTimeProvider.GetAllADLDateTimes();
        return aDLDateTimes;
    }


    public static ADLDateTime GetADLDateTimeByID(int id)
    {
        ADLDateTime aDLDateTime = new ADLDateTime();
        SqlADLDateTimeProvider sqlADLDateTimeProvider = new SqlADLDateTimeProvider();
        aDLDateTime = sqlADLDateTimeProvider.GetADLDateTimeByID(id);
        return aDLDateTime;
    }


    public static int InsertADLDateTime(ADLDateTime aDLDateTime)
    {
        SqlADLDateTimeProvider sqlADLDateTimeProvider = new SqlADLDateTimeProvider();
        return sqlADLDateTimeProvider.InsertADLDateTime(aDLDateTime);
    }


    public static bool UpdateADLDateTime(ADLDateTime aDLDateTime)
    {
        SqlADLDateTimeProvider sqlADLDateTimeProvider = new SqlADLDateTimeProvider();
        return sqlADLDateTimeProvider.UpdateADLDateTime(aDLDateTime);
    }

    public static bool DeleteADLDateTime(int aDLDateTimeID)
    {
        SqlADLDateTimeProvider sqlADLDateTimeProvider = new SqlADLDateTimeProvider();
        return sqlADLDateTimeProvider.DeleteADLDateTime(aDLDateTimeID);
    }
}
