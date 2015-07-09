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

public class ACC_ChartOfAccountLabel1Manager
{
	public ACC_ChartOfAccountLabel1Manager()
	{
	}

    public static List<ACC_ChartOfAccountLabel1> GetAllACC_ChartOfAccountLabel1s()
    {
        List<ACC_ChartOfAccountLabel1> aCC_ChartOfAccountLabel1s = new List<ACC_ChartOfAccountLabel1>();
        SqlACC_ChartOfAccountLabel1Provider sqlACC_ChartOfAccountLabel1Provider = new SqlACC_ChartOfAccountLabel1Provider();
        aCC_ChartOfAccountLabel1s = sqlACC_ChartOfAccountLabel1Provider.GetAllACC_ChartOfAccountLabel1s();
        return aCC_ChartOfAccountLabel1s;
    }


    public static ACC_ChartOfAccountLabel1 GetACC_ChartOfAccountLabel1ByID(int id)
    {
        ACC_ChartOfAccountLabel1 aCC_ChartOfAccountLabel1 = new ACC_ChartOfAccountLabel1();
        SqlACC_ChartOfAccountLabel1Provider sqlACC_ChartOfAccountLabel1Provider = new SqlACC_ChartOfAccountLabel1Provider();
        aCC_ChartOfAccountLabel1 = sqlACC_ChartOfAccountLabel1Provider.GetACC_ChartOfAccountLabel1ByID(id);
        return aCC_ChartOfAccountLabel1;
    }


    public static int InsertACC_ChartOfAccountLabel1(ACC_ChartOfAccountLabel1 aCC_ChartOfAccountLabel1)
    {
        SqlACC_ChartOfAccountLabel1Provider sqlACC_ChartOfAccountLabel1Provider = new SqlACC_ChartOfAccountLabel1Provider();
        return sqlACC_ChartOfAccountLabel1Provider.InsertACC_ChartOfAccountLabel1(aCC_ChartOfAccountLabel1);
    }


    public static bool UpdateACC_ChartOfAccountLabel1(ACC_ChartOfAccountLabel1 aCC_ChartOfAccountLabel1)
    {
        SqlACC_ChartOfAccountLabel1Provider sqlACC_ChartOfAccountLabel1Provider = new SqlACC_ChartOfAccountLabel1Provider();
        return sqlACC_ChartOfAccountLabel1Provider.UpdateACC_ChartOfAccountLabel1(aCC_ChartOfAccountLabel1);
    }

    public static bool DeleteACC_ChartOfAccountLabel1(int aCC_ChartOfAccountLabel1ID)
    {
        SqlACC_ChartOfAccountLabel1Provider sqlACC_ChartOfAccountLabel1Provider = new SqlACC_ChartOfAccountLabel1Provider();
        return sqlACC_ChartOfAccountLabel1Provider.DeleteACC_ChartOfAccountLabel1(aCC_ChartOfAccountLabel1ID);
    }
}
