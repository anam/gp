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

public class Inv_ItemTransactionManager
{
	public Inv_ItemTransactionManager()
	{
	}

    public static List<Inv_ItemTransaction> GetAllInv_ItemTransactions()
    {
        List<Inv_ItemTransaction> inv_ItemTransactions = new List<Inv_ItemTransaction>();
        SqlInv_ItemTransactionProvider sqlInv_ItemTransactionProvider = new SqlInv_ItemTransactionProvider();
        inv_ItemTransactions = sqlInv_ItemTransactionProvider.GetAllInv_ItemTransactions();
        return inv_ItemTransactions;
    }

    public static List<Inv_ItemTransaction> GetAllInv_ItemTransactionsByItemID(int itemID)
    {
        List<Inv_ItemTransaction> inv_ItemTransactions = new List<Inv_ItemTransaction>();
        SqlInv_ItemTransactionProvider sqlInv_ItemTransactionProvider = new SqlInv_ItemTransactionProvider();
        inv_ItemTransactions = sqlInv_ItemTransactionProvider.GetAllInv_ItemTransactionsByItemID(itemID);
        return inv_ItemTransactions;
    }

    public static List<Inv_ItemTransaction> GetAllInv_ItemTransactionsByItemCode(string itemCode)
    {
        List<Inv_ItemTransaction> inv_ItemTransactions = new List<Inv_ItemTransaction>();
        SqlInv_ItemTransactionProvider sqlInv_ItemTransactionProvider = new SqlInv_ItemTransactionProvider();
        inv_ItemTransactions = sqlInv_ItemTransactionProvider.GetAllInv_ItemTransactionsByItemCode(itemCode);
        return inv_ItemTransactions;
    }



    public static Inv_ItemTransaction GetInv_ItemTransactionByID(int id)
    {
        Inv_ItemTransaction inv_ItemTransaction = new Inv_ItemTransaction();
        SqlInv_ItemTransactionProvider sqlInv_ItemTransactionProvider = new SqlInv_ItemTransactionProvider();
        inv_ItemTransaction = sqlInv_ItemTransactionProvider.GetInv_ItemTransactionByID(id);
        return inv_ItemTransaction;
    }


    public static int InsertInv_ItemTransaction(Inv_ItemTransaction inv_ItemTransaction)
    {
        SqlInv_ItemTransactionProvider sqlInv_ItemTransactionProvider = new SqlInv_ItemTransactionProvider();
        return sqlInv_ItemTransactionProvider.InsertInv_ItemTransaction(inv_ItemTransaction);
    }


    public static bool UpdateInv_ItemTransaction(Inv_ItemTransaction inv_ItemTransaction)
    {
        SqlInv_ItemTransactionProvider sqlInv_ItemTransactionProvider = new SqlInv_ItemTransactionProvider();
        return sqlInv_ItemTransactionProvider.UpdateInv_ItemTransaction(inv_ItemTransaction);
    }

    public static bool DeleteInv_ItemTransaction(int inv_ItemTransactionID)
    {
        SqlInv_ItemTransactionProvider sqlInv_ItemTransactionProvider = new SqlInv_ItemTransactionProvider();
        return sqlInv_ItemTransactionProvider.DeleteInv_ItemTransaction(inv_ItemTransactionID);
    }
}
