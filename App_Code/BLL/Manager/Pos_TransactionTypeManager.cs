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

public class Pos_TransactionTypeManager
{
	public Pos_TransactionTypeManager()
	{
	}

    public static List<Pos_TransactionType> GetAllPos_TransactionTypes()
    {
        List<Pos_TransactionType> pos_TransactionTypes = new List<Pos_TransactionType>();
        SqlPos_TransactionTypeProvider sqlPos_TransactionTypeProvider = new SqlPos_TransactionTypeProvider();
        pos_TransactionTypes = sqlPos_TransactionTypeProvider.GetAllPos_TransactionTypes();
        return pos_TransactionTypes;
    }


    public static Pos_TransactionType GetPos_TransactionTypeByID(int id)
    {
        Pos_TransactionType pos_TransactionType = new Pos_TransactionType();
        SqlPos_TransactionTypeProvider sqlPos_TransactionTypeProvider = new SqlPos_TransactionTypeProvider();
        pos_TransactionType = sqlPos_TransactionTypeProvider.GetPos_TransactionTypeByID(id);
        return pos_TransactionType;
    }


    public static int InsertPos_TransactionType(Pos_TransactionType pos_TransactionType)
    {
        SqlPos_TransactionTypeProvider sqlPos_TransactionTypeProvider = new SqlPos_TransactionTypeProvider();
        return sqlPos_TransactionTypeProvider.InsertPos_TransactionType(pos_TransactionType);
    }


    public static bool UpdatePos_TransactionType(Pos_TransactionType pos_TransactionType)
    {
        SqlPos_TransactionTypeProvider sqlPos_TransactionTypeProvider = new SqlPos_TransactionTypeProvider();
        return sqlPos_TransactionTypeProvider.UpdatePos_TransactionType(pos_TransactionType);
    }

    public static bool DeletePos_TransactionType(int pos_TransactionTypeID)
    {
        SqlPos_TransactionTypeProvider sqlPos_TransactionTypeProvider = new SqlPos_TransactionTypeProvider();
        return sqlPos_TransactionTypeProvider.DeletePos_TransactionType(pos_TransactionTypeID);
    }
}
