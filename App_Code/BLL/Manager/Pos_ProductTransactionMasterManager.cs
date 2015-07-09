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

public class Pos_ProductTransactionMasterManager
{
	public Pos_ProductTransactionMasterManager()
	{
	}

    public static List<Pos_ProductTransactionMaster> GetAllPos_ProductTransactionMasters()
    {
        List<Pos_ProductTransactionMaster> pos_ProductTransactionMasters = new List<Pos_ProductTransactionMaster>();
        SqlPos_ProductTransactionMasterProvider sqlPos_ProductTransactionMasterProvider = new SqlPos_ProductTransactionMasterProvider();
        pos_ProductTransactionMasters = sqlPos_ProductTransactionMasterProvider.GetAllPos_ProductTransactionMasters();
        return pos_ProductTransactionMasters;
    }


    public static Pos_ProductTransactionMaster GetPos_ProductTransactionMasterByID(int id)
    {
        Pos_ProductTransactionMaster pos_ProductTransactionMaster = new Pos_ProductTransactionMaster();
        SqlPos_ProductTransactionMasterProvider sqlPos_ProductTransactionMasterProvider = new SqlPos_ProductTransactionMasterProvider();
        pos_ProductTransactionMaster = sqlPos_ProductTransactionMasterProvider.GetPos_ProductTransactionMasterByID(id);
        return pos_ProductTransactionMaster;
    }


    public static int InsertPos_ProductTransactionMaster(Pos_ProductTransactionMaster pos_ProductTransactionMaster)
    {
        SqlPos_ProductTransactionMasterProvider sqlPos_ProductTransactionMasterProvider = new SqlPos_ProductTransactionMasterProvider();
        return sqlPos_ProductTransactionMasterProvider.InsertPos_ProductTransactionMaster(pos_ProductTransactionMaster);
    }


    public static bool UpdatePos_ProductTransactionMaster(Pos_ProductTransactionMaster pos_ProductTransactionMaster)
    {
        SqlPos_ProductTransactionMasterProvider sqlPos_ProductTransactionMasterProvider = new SqlPos_ProductTransactionMasterProvider();
        return sqlPos_ProductTransactionMasterProvider.UpdatePos_ProductTransactionMaster(pos_ProductTransactionMaster);
    }

    public static bool DeletePos_ProductTransactionMaster(int pos_ProductTransactionMasterID)
    {
        SqlPos_ProductTransactionMasterProvider sqlPos_ProductTransactionMasterProvider = new SqlPos_ProductTransactionMasterProvider();
        return sqlPos_ProductTransactionMasterProvider.DeletePos_ProductTransactionMaster(pos_ProductTransactionMasterID);
    }
}
