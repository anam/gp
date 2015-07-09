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

public class Pos_ProductTransactionTypeManager
{
	public Pos_ProductTransactionTypeManager()
	{
	}

    public static List<Pos_ProductTransactionType> GetAllPos_ProductTransactionTypes()
    {
        List<Pos_ProductTransactionType> pos_ProductTransactionTypes = new List<Pos_ProductTransactionType>();
        SqlPos_ProductTransactionTypeProvider sqlPos_ProductTransactionTypeProvider = new SqlPos_ProductTransactionTypeProvider();
        pos_ProductTransactionTypes = sqlPos_ProductTransactionTypeProvider.GetAllPos_ProductTransactionTypes();
        return pos_ProductTransactionTypes;
    }


    public static Pos_ProductTransactionType GetPos_ProductTransactionTypeByID(int id)
    {
        Pos_ProductTransactionType pos_ProductTransactionType = new Pos_ProductTransactionType();
        SqlPos_ProductTransactionTypeProvider sqlPos_ProductTransactionTypeProvider = new SqlPos_ProductTransactionTypeProvider();
        pos_ProductTransactionType = sqlPos_ProductTransactionTypeProvider.GetPos_ProductTransactionTypeByID(id);
        return pos_ProductTransactionType;
    }


    public static int InsertPos_ProductTransactionType(Pos_ProductTransactionType pos_ProductTransactionType)
    {
        SqlPos_ProductTransactionTypeProvider sqlPos_ProductTransactionTypeProvider = new SqlPos_ProductTransactionTypeProvider();
        return sqlPos_ProductTransactionTypeProvider.InsertPos_ProductTransactionType(pos_ProductTransactionType);
    }


    public static bool UpdatePos_ProductTransactionType(Pos_ProductTransactionType pos_ProductTransactionType)
    {
        SqlPos_ProductTransactionTypeProvider sqlPos_ProductTransactionTypeProvider = new SqlPos_ProductTransactionTypeProvider();
        return sqlPos_ProductTransactionTypeProvider.UpdatePos_ProductTransactionType(pos_ProductTransactionType);
    }

    public static bool DeletePos_ProductTransactionType(int pos_ProductTransactionTypeID)
    {
        SqlPos_ProductTransactionTypeProvider sqlPos_ProductTransactionTypeProvider = new SqlPos_ProductTransactionTypeProvider();
        return sqlPos_ProductTransactionTypeProvider.DeletePos_ProductTransactionType(pos_ProductTransactionTypeID);
    }
}
