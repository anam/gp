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

public class ADLHeaderDetailnDateTimeManager
{
	public ADLHeaderDetailnDateTimeManager()
	{
	}

    public static List<ADLHeaderDetailnDateTime> GetAllADLHeaderDetailnDateTimes()
    {
        List<ADLHeaderDetailnDateTime> aDLHeaderDetailnDateTimes = new List<ADLHeaderDetailnDateTime>();
        SqlADLHeaderDetailnDateTimeProvider sqlADLHeaderDetailnDateTimeProvider = new SqlADLHeaderDetailnDateTimeProvider();
        aDLHeaderDetailnDateTimes = sqlADLHeaderDetailnDateTimeProvider.GetAllADLHeaderDetailnDateTimes();
        return aDLHeaderDetailnDateTimes;
    }

    public static DataSet GetAllADLHeaderDetailnDateTimeByADLDateTimeID(int ADLDateTimeID)
    {
        SqlADLHeaderDetailnDateTimeProvider sqlADLHeaderDetailnDateTimeProvider = new SqlADLHeaderDetailnDateTimeProvider();
        return sqlADLHeaderDetailnDateTimeProvider.GetAllADLHeaderDetailnDateTimeByADLDateTimeID(ADLDateTimeID);
    }
    public static DataSet GetAllADLHeaderDetailnDateTimeByResidentID(int ResidentID)
    {
        SqlADLHeaderDetailnDateTimeProvider sqlADLHeaderDetailnDateTimeProvider = new SqlADLHeaderDetailnDateTimeProvider();
        return sqlADLHeaderDetailnDateTimeProvider.GetAllADLHeaderDetailnDateTimeByResidentID(ResidentID);
    }

    public static DataSet GetAllADLHeaderDetailnDateTimeByResidentIDWithDateRange(int ResidentID,DateTime StartDate,DateTime EndDate)
    {
        SqlADLHeaderDetailnDateTimeProvider sqlADLHeaderDetailnDateTimeProvider = new SqlADLHeaderDetailnDateTimeProvider();
        return sqlADLHeaderDetailnDateTimeProvider.GetAllADLHeaderDetailnDateTimeByResidentIDWithDateRange(ResidentID, StartDate,EndDate);
    }

    public static ADLHeaderDetailnDateTime GetADLHeaderDetailnDateTimeByID(int id)
    {
        ADLHeaderDetailnDateTime aDLHeaderDetailnDateTime = new ADLHeaderDetailnDateTime();
        SqlADLHeaderDetailnDateTimeProvider sqlADLHeaderDetailnDateTimeProvider = new SqlADLHeaderDetailnDateTimeProvider();
        aDLHeaderDetailnDateTime = sqlADLHeaderDetailnDateTimeProvider.GetADLHeaderDetailnDateTimeByID(id);
        return aDLHeaderDetailnDateTime;
    }


    public static int InsertADLHeaderDetailnDateTime(ADLHeaderDetailnDateTime aDLHeaderDetailnDateTime)
    {
        SqlADLHeaderDetailnDateTimeProvider sqlADLHeaderDetailnDateTimeProvider = new SqlADLHeaderDetailnDateTimeProvider();
        return sqlADLHeaderDetailnDateTimeProvider.InsertADLHeaderDetailnDateTime(aDLHeaderDetailnDateTime);
    }


    public static bool UpdateADLHeaderDetailnDateTime(ADLHeaderDetailnDateTime aDLHeaderDetailnDateTime)
    {
        SqlADLHeaderDetailnDateTimeProvider sqlADLHeaderDetailnDateTimeProvider = new SqlADLHeaderDetailnDateTimeProvider();
        return sqlADLHeaderDetailnDateTimeProvider.UpdateADLHeaderDetailnDateTime(aDLHeaderDetailnDateTime);
    }

    public static bool DeleteADLHeaderDetailnDateTime(int aDLHeaderDetailnDateTimeID)
    {
        SqlADLHeaderDetailnDateTimeProvider sqlADLHeaderDetailnDateTimeProvider = new SqlADLHeaderDetailnDateTimeProvider();
        return sqlADLHeaderDetailnDateTimeProvider.DeleteADLHeaderDetailnDateTime(aDLHeaderDetailnDateTimeID);
    }

    public static bool DeleteADLHeaderDetailnDateTimeByADLDateTimeID(int aDLDateTimeID)
    {
        SqlADLHeaderDetailnDateTimeProvider sqlADLHeaderDetailnDateTimeProvider = new SqlADLHeaderDetailnDateTimeProvider();
        return sqlADLHeaderDetailnDateTimeProvider.DeleteADLHeaderDetailnDateTimeByADLDateTimeID(aDLDateTimeID);
    }
}
