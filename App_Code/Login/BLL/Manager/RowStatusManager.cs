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

public class RowStatusManager
{
	public RowStatusManager()
	{
	}

    public static List<RowStatus> GetAllRowStatuss()
    {
        List<RowStatus> rowStatuss = new List<RowStatus>();
        SqlRowStatusProvider sqlRowStatusProvider = new SqlRowStatusProvider();
        rowStatuss = sqlRowStatusProvider.GetAllRowStatuss();
        return rowStatuss;
    }


    public static RowStatus GetRowStatusByID(int id)
    {
        RowStatus rowStatus = new RowStatus();
        SqlRowStatusProvider sqlRowStatusProvider = new SqlRowStatusProvider();
        rowStatus = sqlRowStatusProvider.GetRowStatusByID(id);
        return rowStatus;
    }


    public static int InsertRowStatus(RowStatus rowStatus)
    {
        SqlRowStatusProvider sqlRowStatusProvider = new SqlRowStatusProvider();
        return sqlRowStatusProvider.InsertRowStatus(rowStatus);
    }


    public static bool UpdateRowStatus(RowStatus rowStatus)
    {
        SqlRowStatusProvider sqlRowStatusProvider = new SqlRowStatusProvider();
        return sqlRowStatusProvider.UpdateRowStatus(rowStatus);
    }

    public static bool DeleteRowStatus(int rowStatusID)
    {
        SqlRowStatusProvider sqlRowStatusProvider = new SqlRowStatusProvider();
        return sqlRowStatusProvider.DeleteRowStatus(rowStatusID);
    }
}
