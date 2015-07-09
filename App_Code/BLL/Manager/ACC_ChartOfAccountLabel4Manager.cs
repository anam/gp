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

public class ACC_ChartOfAccountLabel4Manager
{
	public ACC_ChartOfAccountLabel4Manager()
	{
	}

    public static List<ACC_ChartOfAccountLabel4> GetAllACC_ChartOfAccountLabel4s()
    {
        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        SqlACC_ChartOfAccountLabel4Provider sqlACC_ChartOfAccountLabel4Provider = new SqlACC_ChartOfAccountLabel4Provider();
        aCC_ChartOfAccountLabel4s = sqlACC_ChartOfAccountLabel4Provider.GetAllACC_ChartOfAccountLabel4s();
        return aCC_ChartOfAccountLabel4s;
    }

    public static List<ACC_ChartOfAccountLabel4> GetAllACC_ChartOfAccountLabel4sForJournalEntry()
    {
        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        SqlACC_ChartOfAccountLabel4Provider sqlACC_ChartOfAccountLabel4Provider = new SqlACC_ChartOfAccountLabel4Provider();
        aCC_ChartOfAccountLabel4s = sqlACC_ChartOfAccountLabel4Provider.GetAllACC_ChartOfAccountLabel4sForJournalEntry();
        return aCC_ChartOfAccountLabel4s;
    }

    public static List<ACC_ChartOfAccountLabel4> GetAllACC_ChartOfAccountLabel4sForJournalEntryVisibleOnly()
    {
        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        SqlACC_ChartOfAccountLabel4Provider sqlACC_ChartOfAccountLabel4Provider = new SqlACC_ChartOfAccountLabel4Provider();
        aCC_ChartOfAccountLabel4s = sqlACC_ChartOfAccountLabel4Provider.GetAllACC_ChartOfAccountLabel4sForJournalEntryVisibleOnly();
        return aCC_ChartOfAccountLabel4s;
    }

    public static ACC_ChartOfAccountLabel4 GetACC_ChartOfAccountLabel4ByID(int id)
    {
        ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 = new ACC_ChartOfAccountLabel4();
        SqlACC_ChartOfAccountLabel4Provider sqlACC_ChartOfAccountLabel4Provider = new SqlACC_ChartOfAccountLabel4Provider();
        aCC_ChartOfAccountLabel4 = sqlACC_ChartOfAccountLabel4Provider.GetACC_ChartOfAccountLabel4ByID(id);
        return aCC_ChartOfAccountLabel4;
    }


    public static int InsertACC_ChartOfAccountLabel4(ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4)
    {
        SqlACC_ChartOfAccountLabel4Provider sqlACC_ChartOfAccountLabel4Provider = new SqlACC_ChartOfAccountLabel4Provider();
        return sqlACC_ChartOfAccountLabel4Provider.InsertACC_ChartOfAccountLabel4(aCC_ChartOfAccountLabel4);
    }


    public static bool UpdateACC_ChartOfAccountLabel4(ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4)
    {
        SqlACC_ChartOfAccountLabel4Provider sqlACC_ChartOfAccountLabel4Provider = new SqlACC_ChartOfAccountLabel4Provider();
        return sqlACC_ChartOfAccountLabel4Provider.UpdateACC_ChartOfAccountLabel4(aCC_ChartOfAccountLabel4);
    }

    public static bool DeleteACC_ChartOfAccountLabel4(int aCC_ChartOfAccountLabel4ID)
    {
        SqlACC_ChartOfAccountLabel4Provider sqlACC_ChartOfAccountLabel4Provider = new SqlACC_ChartOfAccountLabel4Provider();
        return sqlACC_ChartOfAccountLabel4Provider.DeleteACC_ChartOfAccountLabel4(aCC_ChartOfAccountLabel4ID);
    }
}
