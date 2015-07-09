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

public class Pos_CardBankManager
{
	public Pos_CardBankManager()
	{
	}

    public static List<Pos_CardBank> GetAllPos_CardBanks()
    {
        List<Pos_CardBank> pos_CardBanks = new List<Pos_CardBank>();
        SqlPos_CardBankProvider sqlPos_CardBankProvider = new SqlPos_CardBankProvider();
        pos_CardBanks = sqlPos_CardBankProvider.GetAllPos_CardBanks();
        return pos_CardBanks;
    }


    public static Pos_CardBank GetPos_CardBankByID(int id)
    {
        Pos_CardBank pos_CardBank = new Pos_CardBank();
        SqlPos_CardBankProvider sqlPos_CardBankProvider = new SqlPos_CardBankProvider();
        pos_CardBank = sqlPos_CardBankProvider.GetPos_CardBankByID(id);
        return pos_CardBank;
    }


    public static int InsertPos_CardBank(Pos_CardBank pos_CardBank)
    {
        SqlPos_CardBankProvider sqlPos_CardBankProvider = new SqlPos_CardBankProvider();
        return sqlPos_CardBankProvider.InsertPos_CardBank(pos_CardBank);
    }


    public static bool UpdatePos_CardBank(Pos_CardBank pos_CardBank)
    {
        SqlPos_CardBankProvider sqlPos_CardBankProvider = new SqlPos_CardBankProvider();
        return sqlPos_CardBankProvider.UpdatePos_CardBank(pos_CardBank);
    }

    public static bool DeletePos_CardBank(int pos_CardBankID)
    {
        SqlPos_CardBankProvider sqlPos_CardBankProvider = new SqlPos_CardBankProvider();
        return sqlPos_CardBankProvider.DeletePos_CardBank(pos_CardBankID);
    }
}
