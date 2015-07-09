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

public class Pos_TransactionMasterManager
{
	public Pos_TransactionMasterManager()
	{
	}

    public static List<Pos_TransactionMaster> GetAllPos_TransactionMasters()
    {
        List<Pos_TransactionMaster> pos_TransactionMasters = new List<Pos_TransactionMaster>();
        SqlPos_TransactionMasterProvider sqlPos_TransactionMasterProvider = new SqlPos_TransactionMasterProvider();
        pos_TransactionMasters = sqlPos_TransactionMasterProvider.GetAllPos_TransactionMasters();
        return pos_TransactionMasters;
    }


    public static Pos_TransactionMaster GetPos_TransactionMasterByID(int id)
    {
        Pos_TransactionMaster pos_TransactionMaster = new Pos_TransactionMaster();
        SqlPos_TransactionMasterProvider sqlPos_TransactionMasterProvider = new SqlPos_TransactionMasterProvider();
        pos_TransactionMaster = sqlPos_TransactionMasterProvider.GetPos_TransactionMasterByID(id);
        return pos_TransactionMaster;
    }


    public static int InsertPos_TransactionMaster(Pos_TransactionMaster pos_TransactionMaster)
    {
        SqlPos_TransactionMasterProvider sqlPos_TransactionMasterProvider = new SqlPos_TransactionMasterProvider();
        return sqlPos_TransactionMasterProvider.InsertPos_TransactionMaster(pos_TransactionMaster);
    }
    public static int InsertPos_TransactionMaster(Pos_TransactionMaster pos_TransactionMaster,bool isdoubleEntry)
    {
        SqlPos_TransactionMasterProvider sqlPos_TransactionMasterProvider = new SqlPos_TransactionMasterProvider();
        return sqlPos_TransactionMasterProvider.InsertPos_TransactionMaster(pos_TransactionMaster, isdoubleEntry);
    }

    public static bool UpdatePos_TransactionMaster(Pos_TransactionMaster pos_TransactionMaster)
    {
        SqlPos_TransactionMasterProvider sqlPos_TransactionMasterProvider = new SqlPos_TransactionMasterProvider();
        return sqlPos_TransactionMasterProvider.UpdatePos_TransactionMaster(pos_TransactionMaster);
    }

    public static bool DeletePos_TransactionMaster(int pos_TransactionMasterID)
    {
        SqlPos_TransactionMasterProvider sqlPos_TransactionMasterProvider = new SqlPos_TransactionMasterProvider();
        return sqlPos_TransactionMasterProvider.DeletePos_TransactionMaster(pos_TransactionMasterID);
    }
}
