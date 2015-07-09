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

public class Inv_IssueMasterManager
{
	public Inv_IssueMasterManager()
	{
	}

    public static List<Inv_IssueMaster> GetAllInv_IssueMasters()
    {
        List<Inv_IssueMaster> inv_IssueMasters = new List<Inv_IssueMaster>();
        SqlInv_IssueMasterProvider sqlInv_IssueMasterProvider = new SqlInv_IssueMasterProvider();
        inv_IssueMasters = sqlInv_IssueMasterProvider.GetAllInv_IssueMasters();
        return inv_IssueMasters;
    }


    public static Inv_IssueMaster GetInv_IssueMasterByID(int id)
    {
        Inv_IssueMaster inv_IssueMaster = new Inv_IssueMaster();
        SqlInv_IssueMasterProvider sqlInv_IssueMasterProvider = new SqlInv_IssueMasterProvider();
        inv_IssueMaster = sqlInv_IssueMasterProvider.GetInv_IssueMasterByID(id);
        return inv_IssueMaster;
    }


    public static int InsertInv_IssueMaster(Inv_IssueMaster inv_IssueMaster)
    {
        SqlInv_IssueMasterProvider sqlInv_IssueMasterProvider = new SqlInv_IssueMasterProvider();
        return sqlInv_IssueMasterProvider.InsertInv_IssueMaster(inv_IssueMaster);
    }


    public static bool UpdateInv_IssueMaster(Inv_IssueMaster inv_IssueMaster)
    {
        SqlInv_IssueMasterProvider sqlInv_IssueMasterProvider = new SqlInv_IssueMasterProvider();
        return sqlInv_IssueMasterProvider.UpdateInv_IssueMaster(inv_IssueMaster);
    }

    public static bool DeleteInv_IssueMaster(int inv_IssueMasterID)
    {
        SqlInv_IssueMasterProvider sqlInv_IssueMasterProvider = new SqlInv_IssueMasterProvider();
        return sqlInv_IssueMasterProvider.DeleteInv_IssueMaster(inv_IssueMasterID);
    }

    public static bool DeleteInv_IssueMasterAll(int inv_IssueMasterID,int login)
    {
        SqlInv_IssueMasterProvider sqlInv_IssueMasterProvider = new SqlInv_IssueMasterProvider();
        return sqlInv_IssueMasterProvider.DeleteInv_IssueMasterAll(inv_IssueMasterID,login);
    }
}
