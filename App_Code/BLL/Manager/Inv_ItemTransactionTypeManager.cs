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

public class Inv_ItemTransactionTypeManager
{
	public Inv_ItemTransactionTypeManager()
	{
	}

    public static List<Inv_ItemTransactionType> GetAllInv_ItemTransactionTypes()
    {
        List<Inv_ItemTransactionType> inv_ItemTransactionTypes = new List<Inv_ItemTransactionType>();
        SqlInv_ItemTransactionTypeProvider sqlInv_ItemTransactionTypeProvider = new SqlInv_ItemTransactionTypeProvider();
        inv_ItemTransactionTypes = sqlInv_ItemTransactionTypeProvider.GetAllInv_ItemTransactionTypes();
        return inv_ItemTransactionTypes;
    }


    public static Inv_ItemTransactionType GetInv_ItemTransactionTypeByID(int id)
    {
        Inv_ItemTransactionType inv_ItemTransactionType = new Inv_ItemTransactionType();
        SqlInv_ItemTransactionTypeProvider sqlInv_ItemTransactionTypeProvider = new SqlInv_ItemTransactionTypeProvider();
        inv_ItemTransactionType = sqlInv_ItemTransactionTypeProvider.GetInv_ItemTransactionTypeByID(id);
        return inv_ItemTransactionType;
    }


    public static int InsertInv_ItemTransactionType(Inv_ItemTransactionType inv_ItemTransactionType)
    {
        SqlInv_ItemTransactionTypeProvider sqlInv_ItemTransactionTypeProvider = new SqlInv_ItemTransactionTypeProvider();
        return sqlInv_ItemTransactionTypeProvider.InsertInv_ItemTransactionType(inv_ItemTransactionType);
    }


    public static bool UpdateInv_ItemTransactionType(Inv_ItemTransactionType inv_ItemTransactionType)
    {
        SqlInv_ItemTransactionTypeProvider sqlInv_ItemTransactionTypeProvider = new SqlInv_ItemTransactionTypeProvider();
        return sqlInv_ItemTransactionTypeProvider.UpdateInv_ItemTransactionType(inv_ItemTransactionType);
    }

    public static bool DeleteInv_ItemTransactionType(int inv_ItemTransactionTypeID)
    {
        SqlInv_ItemTransactionTypeProvider sqlInv_ItemTransactionTypeProvider = new SqlInv_ItemTransactionTypeProvider();
        return sqlInv_ItemTransactionTypeProvider.DeleteInv_ItemTransactionType(inv_ItemTransactionTypeID);
    }
}
