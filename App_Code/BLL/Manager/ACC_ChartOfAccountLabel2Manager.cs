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

public class ACC_ChartOfAccountLabel2Manager
{
	public ACC_ChartOfAccountLabel2Manager()
	{
	}

    public static List<ACC_ChartOfAccountLabel2> GetAllACC_ChartOfAccountLabel2s()
    {
        List<ACC_ChartOfAccountLabel2> aCC_ChartOfAccountLabel2s = new List<ACC_ChartOfAccountLabel2>();
        SqlACC_ChartOfAccountLabel2Provider sqlACC_ChartOfAccountLabel2Provider = new SqlACC_ChartOfAccountLabel2Provider();
        aCC_ChartOfAccountLabel2s = sqlACC_ChartOfAccountLabel2Provider.GetAllACC_ChartOfAccountLabel2s();
        return aCC_ChartOfAccountLabel2s;
    }

   


    public static ACC_ChartOfAccountLabel2 GetACC_ChartOfAccountLabel2ByID(int id)
    {
        ACC_ChartOfAccountLabel2 aCC_ChartOfAccountLabel2 = new ACC_ChartOfAccountLabel2();
        SqlACC_ChartOfAccountLabel2Provider sqlACC_ChartOfAccountLabel2Provider = new SqlACC_ChartOfAccountLabel2Provider();
        aCC_ChartOfAccountLabel2 = sqlACC_ChartOfAccountLabel2Provider.GetACC_ChartOfAccountLabel2ByID(id);
        return aCC_ChartOfAccountLabel2;
    }


    public static int InsertACC_ChartOfAccountLabel2(ACC_ChartOfAccountLabel2 aCC_ChartOfAccountLabel2)
    {
        SqlACC_ChartOfAccountLabel2Provider sqlACC_ChartOfAccountLabel2Provider = new SqlACC_ChartOfAccountLabel2Provider();
        return sqlACC_ChartOfAccountLabel2Provider.InsertACC_ChartOfAccountLabel2(aCC_ChartOfAccountLabel2);
    }


    public static bool UpdateACC_ChartOfAccountLabel2(ACC_ChartOfAccountLabel2 aCC_ChartOfAccountLabel2)
    {
        SqlACC_ChartOfAccountLabel2Provider sqlACC_ChartOfAccountLabel2Provider = new SqlACC_ChartOfAccountLabel2Provider();
        return sqlACC_ChartOfAccountLabel2Provider.UpdateACC_ChartOfAccountLabel2(aCC_ChartOfAccountLabel2);
    }

    public static bool DeleteACC_ChartOfAccountLabel2(int aCC_ChartOfAccountLabel2ID)
    {
        SqlACC_ChartOfAccountLabel2Provider sqlACC_ChartOfAccountLabel2Provider = new SqlACC_ChartOfAccountLabel2Provider();
        return sqlACC_ChartOfAccountLabel2Provider.DeleteACC_ChartOfAccountLabel2(aCC_ChartOfAccountLabel2ID);
    }
}
