using System;
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

public class Pos_CardBank
{
    public Pos_CardBank()
    {
    }

    public Pos_CardBank
        (
        int pos_CardBankID, 
        string bankName, 
        string details, 
        int charOfAccountLabel4ID
        )
    {
        this.Pos_CardBankID = pos_CardBankID;
        this.BankName = bankName;
        this.Details = details;
        this.CharOfAccountLabel4ID = charOfAccountLabel4ID;
    }


    private int _pos_CardBankID;
    public int Pos_CardBankID
    {
        get { return _pos_CardBankID; }
        set { _pos_CardBankID = value; }
    }

    private string _bankName;
    public string BankName
    {
        get { return _bankName; }
        set { _bankName = value; }
    }

    private string _details;
    public string Details
    {
        get { return _details; }
        set { _details = value; }
    }

    private int _charOfAccountLabel4ID;
    public int CharOfAccountLabel4ID
    {
        get { return _charOfAccountLabel4ID; }
        set { _charOfAccountLabel4ID = value; }
    }
}
