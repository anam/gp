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

public class ACC_HeadTypeManager
{
	public ACC_HeadTypeManager()
	{
	}

    public static List<ACC_HeadType> GetAllACC_HeadTypes()
    {
        List<ACC_HeadType> aCC_HeadTypes = new List<ACC_HeadType>();
        SqlACC_HeadTypeProvider sqlACC_HeadTypeProvider = new SqlACC_HeadTypeProvider();
        aCC_HeadTypes = sqlACC_HeadTypeProvider.GetAllACC_HeadTypes();
        return aCC_HeadTypes;
    }


    public static ACC_HeadType GetACC_HeadTypeByID(int id)
    {
        ACC_HeadType aCC_HeadType = new ACC_HeadType();
        SqlACC_HeadTypeProvider sqlACC_HeadTypeProvider = new SqlACC_HeadTypeProvider();
        aCC_HeadType = sqlACC_HeadTypeProvider.GetACC_HeadTypeByID(id);
        return aCC_HeadType;
    }


    public static int InsertACC_HeadType(ACC_HeadType aCC_HeadType)
    {
        SqlACC_HeadTypeProvider sqlACC_HeadTypeProvider = new SqlACC_HeadTypeProvider();
        return sqlACC_HeadTypeProvider.InsertACC_HeadType(aCC_HeadType);
    }


    public static bool UpdateACC_HeadType(ACC_HeadType aCC_HeadType)
    {
        SqlACC_HeadTypeProvider sqlACC_HeadTypeProvider = new SqlACC_HeadTypeProvider();
        return sqlACC_HeadTypeProvider.UpdateACC_HeadType(aCC_HeadType);
    }

    public static bool DeleteACC_HeadType(int aCC_HeadTypeID)
    {
        SqlACC_HeadTypeProvider sqlACC_HeadTypeProvider = new SqlACC_HeadTypeProvider();
        return sqlACC_HeadTypeProvider.DeleteACC_HeadType(aCC_HeadTypeID);
    }
}
