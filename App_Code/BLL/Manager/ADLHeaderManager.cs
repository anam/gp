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

public class ADLHeaderManager
{
	public ADLHeaderManager()
	{
	}

    public static List<ADLHeader> GetAllADLHeaders()
    {
        List<ADLHeader> aDLHeaders = new List<ADLHeader>();
        SqlADLHeaderProvider sqlADLHeaderProvider = new SqlADLHeaderProvider();
        aDLHeaders = sqlADLHeaderProvider.GetAllADLHeaders();
        return aDLHeaders;
    }


    public static ADLHeader GetADLHeaderByID(int id)
    {
        ADLHeader aDLHeader = new ADLHeader();
        SqlADLHeaderProvider sqlADLHeaderProvider = new SqlADLHeaderProvider();
        aDLHeader = sqlADLHeaderProvider.GetADLHeaderByID(id);
        return aDLHeader;
    }


    public static int InsertADLHeader(ADLHeader aDLHeader)
    {
        SqlADLHeaderProvider sqlADLHeaderProvider = new SqlADLHeaderProvider();
        return sqlADLHeaderProvider.InsertADLHeader(aDLHeader);
    }


    public static bool UpdateADLHeader(ADLHeader aDLHeader)
    {
        SqlADLHeaderProvider sqlADLHeaderProvider = new SqlADLHeaderProvider();
        return sqlADLHeaderProvider.UpdateADLHeader(aDLHeader);
    }

    public static bool DeleteADLHeader(int aDLHeaderID)
    {
        SqlADLHeaderProvider sqlADLHeaderProvider = new SqlADLHeaderProvider();
        return sqlADLHeaderProvider.DeleteADLHeader(aDLHeaderID);
    }
}
