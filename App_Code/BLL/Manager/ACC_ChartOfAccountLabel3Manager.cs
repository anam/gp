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

public class ACC_ChartOfAccountLabel3Manager
{
	public ACC_ChartOfAccountLabel3Manager()
	{
	}

    public static List<ACC_ChartOfAccountLabel3> GetAllACC_ChartOfAccountLabel3s()
    {
        List<ACC_ChartOfAccountLabel3> aCC_ChartOfAccountLabel3s = new List<ACC_ChartOfAccountLabel3>();
        SqlACC_ChartOfAccountLabel3Provider sqlACC_ChartOfAccountLabel3Provider = new SqlACC_ChartOfAccountLabel3Provider();
        aCC_ChartOfAccountLabel3s = sqlACC_ChartOfAccountLabel3Provider.GetAllACC_ChartOfAccountLabel3s();
        return aCC_ChartOfAccountLabel3s;
    }

    public static List<ACC_ChartOfAccountLabel3> GetAllACC_ChartOfAccountLabel3sForJournalEntry()
    {
        List<ACC_ChartOfAccountLabel3> aCC_ChartOfAccountLabel3s = new List<ACC_ChartOfAccountLabel3>();
        SqlACC_ChartOfAccountLabel3Provider sqlACC_ChartOfAccountLabel3Provider = new SqlACC_ChartOfAccountLabel3Provider();
        aCC_ChartOfAccountLabel3s = sqlACC_ChartOfAccountLabel3Provider.GetAllACC_ChartOfAccountLabel3sForJournalEntry();
        return aCC_ChartOfAccountLabel3s;
    }


    public static List<ACC_ChartOfAccountLabel3> GetAllACC_ChartOfAccountLabel3sForJournalEntryForDropDownList()
    {
        List<ACC_ChartOfAccountLabel3> aCC_ChartOfAccountLabel3s = new List<ACC_ChartOfAccountLabel3>();
        SqlACC_ChartOfAccountLabel3Provider sqlACC_ChartOfAccountLabel3Provider = new SqlACC_ChartOfAccountLabel3Provider();
        aCC_ChartOfAccountLabel3s = sqlACC_ChartOfAccountLabel3Provider.GetAllACC_ChartOfAccountLabel3sForJournalEntryForDropDownList();
        return aCC_ChartOfAccountLabel3s;
    }
    public static ACC_ChartOfAccountLabel3 GetACC_ChartOfAccountLabel3ByID(int id)
    {
        ACC_ChartOfAccountLabel3 aCC_ChartOfAccountLabel3 = new ACC_ChartOfAccountLabel3();
        SqlACC_ChartOfAccountLabel3Provider sqlACC_ChartOfAccountLabel3Provider = new SqlACC_ChartOfAccountLabel3Provider();
        aCC_ChartOfAccountLabel3 = sqlACC_ChartOfAccountLabel3Provider.GetACC_ChartOfAccountLabel3ByID(id);
        return aCC_ChartOfAccountLabel3;
    }


    public static int InsertACC_ChartOfAccountLabel3(ACC_ChartOfAccountLabel3 aCC_ChartOfAccountLabel3)
    {
        SqlACC_ChartOfAccountLabel3Provider sqlACC_ChartOfAccountLabel3Provider = new SqlACC_ChartOfAccountLabel3Provider();
        return sqlACC_ChartOfAccountLabel3Provider.InsertACC_ChartOfAccountLabel3(aCC_ChartOfAccountLabel3);
    }


    public static bool UpdateACC_ChartOfAccountLabel3(ACC_ChartOfAccountLabel3 aCC_ChartOfAccountLabel3)
    {
        SqlACC_ChartOfAccountLabel3Provider sqlACC_ChartOfAccountLabel3Provider = new SqlACC_ChartOfAccountLabel3Provider();
        return sqlACC_ChartOfAccountLabel3Provider.UpdateACC_ChartOfAccountLabel3(aCC_ChartOfAccountLabel3);
    }

    public static bool DeleteACC_ChartOfAccountLabel3(int aCC_ChartOfAccountLabel3ID)
    {
        SqlACC_ChartOfAccountLabel3Provider sqlACC_ChartOfAccountLabel3Provider = new SqlACC_ChartOfAccountLabel3Provider();
        return sqlACC_ChartOfAccountLabel3Provider.DeleteACC_ChartOfAccountLabel3(aCC_ChartOfAccountLabel3ID);
    }
}
