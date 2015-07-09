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

public class Inv_IssueMasterReturnManager
{
	public Inv_IssueMasterReturnManager()
	{
	}

    public static List<Inv_IssueMasterReturn> GetAllInv_IssueMasterReturns()
    {
        List<Inv_IssueMasterReturn> inv_IssueMasterReturns = new List<Inv_IssueMasterReturn>();
        SqlInv_IssueMasterReturnProvider sqlInv_IssueMasterReturnProvider = new SqlInv_IssueMasterReturnProvider();
        inv_IssueMasterReturns = sqlInv_IssueMasterReturnProvider.GetAllInv_IssueMasterReturns();
        return inv_IssueMasterReturns;
    }


    public static Inv_IssueMasterReturn GetInv_IssueMasterReturnByID(int id)
    {
        Inv_IssueMasterReturn inv_IssueMasterReturn = new Inv_IssueMasterReturn();
        SqlInv_IssueMasterReturnProvider sqlInv_IssueMasterReturnProvider = new SqlInv_IssueMasterReturnProvider();
        inv_IssueMasterReturn = sqlInv_IssueMasterReturnProvider.GetInv_IssueMasterReturnByID(id);
        return inv_IssueMasterReturn;
    }


    public static int InsertInv_IssueMasterReturn(Inv_IssueMasterReturn inv_IssueMasterReturn)
    {
        SqlInv_IssueMasterReturnProvider sqlInv_IssueMasterReturnProvider = new SqlInv_IssueMasterReturnProvider();
        return sqlInv_IssueMasterReturnProvider.InsertInv_IssueMasterReturn(inv_IssueMasterReturn);
    }


    public static bool UpdateInv_IssueMasterReturn(Inv_IssueMasterReturn inv_IssueMasterReturn)
    {
        SqlInv_IssueMasterReturnProvider sqlInv_IssueMasterReturnProvider = new SqlInv_IssueMasterReturnProvider();
        return sqlInv_IssueMasterReturnProvider.UpdateInv_IssueMasterReturn(inv_IssueMasterReturn);
    }

    public static bool DeleteInv_IssueMasterReturn(int inv_IssueMasterReturnID)
    {
        SqlInv_IssueMasterReturnProvider sqlInv_IssueMasterReturnProvider = new SqlInv_IssueMasterReturnProvider();
        return sqlInv_IssueMasterReturnProvider.DeleteInv_IssueMasterReturn(inv_IssueMasterReturnID);
    }
}
