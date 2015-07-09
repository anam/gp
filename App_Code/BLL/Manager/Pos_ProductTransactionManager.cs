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

public class Pos_ProductTransactionManager
{
	public Pos_ProductTransactionManager()
	{
	}

    public static List<Pos_ProductTransaction> GetAllPos_ProductTransactions()
    {
        List<Pos_ProductTransaction> pos_ProductTransactions = new List<Pos_ProductTransaction>();
        SqlPos_ProductTransactionProvider sqlPos_ProductTransactionProvider = new SqlPos_ProductTransactionProvider();
        pos_ProductTransactions = sqlPos_ProductTransactionProvider.GetAllPos_ProductTransactions();
        return pos_ProductTransactions;
    }


    public static Pos_ProductTransaction GetPos_ProductTransactionByID(int id)
    {
        Pos_ProductTransaction pos_ProductTransaction = new Pos_ProductTransaction();
        SqlPos_ProductTransactionProvider sqlPos_ProductTransactionProvider = new SqlPos_ProductTransactionProvider();
        pos_ProductTransaction = sqlPos_ProductTransactionProvider.GetPos_ProductTransactionByID(id);
        return pos_ProductTransaction;
    }


    public static int InsertPos_ProductTransaction(Pos_ProductTransaction pos_ProductTransaction)
    {
        SqlPos_ProductTransactionProvider sqlPos_ProductTransactionProvider = new SqlPos_ProductTransactionProvider();
        return sqlPos_ProductTransactionProvider.InsertPos_ProductTransaction(pos_ProductTransaction);
    }


    public static bool UpdatePos_ProductTransaction(Pos_ProductTransaction pos_ProductTransaction)
    {
        SqlPos_ProductTransactionProvider sqlPos_ProductTransactionProvider = new SqlPos_ProductTransactionProvider();
        return sqlPos_ProductTransactionProvider.UpdatePos_ProductTransaction(pos_ProductTransaction);
    }

    public static bool DeletePos_ProductTransaction(int pos_ProductTransactionID)
    {
        SqlPos_ProductTransactionProvider sqlPos_ProductTransactionProvider = new SqlPos_ProductTransactionProvider();
        return sqlPos_ProductTransactionProvider.DeletePos_ProductTransaction(pos_ProductTransactionID);
    }
}
