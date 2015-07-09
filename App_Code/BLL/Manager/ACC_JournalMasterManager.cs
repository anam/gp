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

public class ACC_JournalMasterManager
{
	public ACC_JournalMasterManager()
	{
	}

    public static List<ACC_JournalMaster> GetAllACC_JournalMasters()
    {
        List<ACC_JournalMaster> aCC_JournalMasters = new List<ACC_JournalMaster>();
        SqlACC_JournalMasterProvider sqlACC_JournalMasterProvider = new SqlACC_JournalMasterProvider();
        aCC_JournalMasters = sqlACC_JournalMasterProvider.GetAllACC_JournalMasters();
        return aCC_JournalMasters;
    }


    public static List<ACC_JournalMaster> GetAllACC_JournalMastersByDateRange(string searchString)
    {
        List<ACC_JournalMaster> aCC_JournalMasters = new List<ACC_JournalMaster>();
        SqlACC_JournalMasterProvider sqlACC_JournalMasterProvider = new SqlACC_JournalMasterProvider();
        aCC_JournalMasters = sqlACC_JournalMasterProvider.GetAllACC_JournalMastersByDateRange(searchString);
        return aCC_JournalMasters;
    }


    public static ACC_JournalMaster GetACC_JournalMasterByID(int id)
    {
        ACC_JournalMaster aCC_JournalMaster = new ACC_JournalMaster();
        SqlACC_JournalMasterProvider sqlACC_JournalMasterProvider = new SqlACC_JournalMasterProvider();
        aCC_JournalMaster = sqlACC_JournalMasterProvider.GetACC_JournalMasterByID(id);
        return aCC_JournalMaster;
    }


    public static int InsertACC_JournalMaster(ACC_JournalMaster aCC_JournalMaster)
    {
        SqlACC_JournalMasterProvider sqlACC_JournalMasterProvider = new SqlACC_JournalMasterProvider();
        return sqlACC_JournalMasterProvider.InsertACC_JournalMaster(aCC_JournalMaster);
    }

    public static int InsertACC_JournalMasterTmp(ACC_JournalMaster aCC_JournalMaster)
    {
        SqlACC_JournalMasterProvider sqlACC_JournalMasterProvider = new SqlACC_JournalMasterProvider();
        return sqlACC_JournalMasterProvider.InsertACC_JournalMasterTmp(aCC_JournalMaster);
    }


    public static bool UpdateACC_JournalMaster(ACC_JournalMaster aCC_JournalMaster)
    {
        SqlACC_JournalMasterProvider sqlACC_JournalMasterProvider = new SqlACC_JournalMasterProvider();
        return sqlACC_JournalMasterProvider.UpdateACC_JournalMaster(aCC_JournalMaster);
    }

    public static bool DeleteACC_JournalMaster(int aCC_JournalMasterID)
    {
        SqlACC_JournalMasterProvider sqlACC_JournalMasterProvider = new SqlACC_JournalMasterProvider();
        return sqlACC_JournalMasterProvider.DeleteACC_JournalMaster(aCC_JournalMasterID);
    }
}
