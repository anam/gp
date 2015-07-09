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

public class Pos_TransactionType
{
    public Pos_TransactionType()
    {
    }

    public Pos_TransactionType
        (
        int pos_TransactionTypeID, 
        string transactionTypeName, 
        string centralStockFormula, 
        string showRoomFormula, 
        string extraField1, 
        string extraField2, 
        string extraField3
        )
    {
        this.Pos_TransactionTypeID = pos_TransactionTypeID;
        this.TransactionTypeName = transactionTypeName;
        this.CentralStockFormula = centralStockFormula;
        this.ShowRoomFormula = showRoomFormula;
        this.ExtraField1 = extraField1;
        this.ExtraField2 = extraField2;
        this.ExtraField3 = extraField3;
    }


    private int _pos_TransactionTypeID;
    public int Pos_TransactionTypeID
    {
        get { return _pos_TransactionTypeID; }
        set { _pos_TransactionTypeID = value; }
    }

    private string _transactionTypeName;
    public string TransactionTypeName
    {
        get { return _transactionTypeName; }
        set { _transactionTypeName = value; }
    }

    private string _centralStockFormula;
    public string CentralStockFormula
    {
        get { return _centralStockFormula; }
        set { _centralStockFormula = value; }
    }

    private string _showRoomFormula;
    public string ShowRoomFormula
    {
        get { return _showRoomFormula; }
        set { _showRoomFormula = value; }
    }

    private string _extraField1;
    public string ExtraField1
    {
        get { return _extraField1; }
        set { _extraField1 = value; }
    }

    private string _extraField2;
    public string ExtraField2
    {
        get { return _extraField2; }
        set { _extraField2 = value; }
    }

    private string _extraField3;
    public string ExtraField3
    {
        get { return _extraField3; }
        set { _extraField3 = value; }
    }

    private string _extraField4;

    public string ExtraField4
    {
        get { return _extraField4; }
        set { _extraField4 = value; }
    }
    private string _extraField5;

    public string ExtraField5
    {
        get { return _extraField5; }
        set { _extraField5 = value; }
    }
    private string _extraField6;

    public string ExtraField6
    {
        get { return _extraField6; }
        set { _extraField6 = value; }
    }

    private int _sorting;

    public int Sorting
    {
        get { return _sorting; }
        set { _sorting = value; }
    }

}
