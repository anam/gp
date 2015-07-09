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

public class Pos_TransactionManager
{
	public Pos_TransactionManager()
	{
	}

    public static List<Pos_Transaction> GetAllPos_Transactions()
    {
        List<Pos_Transaction> pos_Transactions = new List<Pos_Transaction>();
        SqlPos_TransactionProvider sqlPos_TransactionProvider = new SqlPos_TransactionProvider();
        pos_Transactions = sqlPos_TransactionProvider.GetAllPos_Transactions();
        return pos_Transactions;
    }


    public static Pos_Transaction GetPos_TransactionByID(int id)
    {
        Pos_Transaction pos_Transaction = new Pos_Transaction();
        SqlPos_TransactionProvider sqlPos_TransactionProvider = new SqlPos_TransactionProvider();
        pos_Transaction = sqlPos_TransactionProvider.GetPos_TransactionByID(id);
        return pos_Transaction;
    }


    public static int InsertPos_Transaction(Pos_Transaction pos_Transaction)
    {
        SqlPos_TransactionProvider sqlPos_TransactionProvider = new SqlPos_TransactionProvider();
        return sqlPos_TransactionProvider.InsertPos_Transaction(pos_Transaction);
    }

    public static int InsertPos_TransactionWithOpositeEntry(Pos_Transaction pos_Transaction)
    {
        SqlPos_TransactionProvider sqlPos_TransactionProvider = new SqlPos_TransactionProvider();
        return sqlPos_TransactionProvider.InsertPos_TransactionWithOpositeEntry(pos_Transaction);
    }


    public static bool UpdatePos_Transaction(Pos_Transaction pos_Transaction)
    {
        SqlPos_TransactionProvider sqlPos_TransactionProvider = new SqlPos_TransactionProvider();
        return sqlPos_TransactionProvider.UpdatePos_Transaction(pos_Transaction);
    }

    public static bool DeletePos_Transaction(int pos_TransactionID)
    {
        SqlPos_TransactionProvider sqlPos_TransactionProvider = new SqlPos_TransactionProvider();
        return sqlPos_TransactionProvider.DeletePos_Transaction(pos_TransactionID);
    }
}
