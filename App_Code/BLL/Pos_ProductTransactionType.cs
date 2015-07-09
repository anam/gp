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

public class Pos_ProductTransactionType
{
    public Pos_ProductTransactionType()
    {
    }

    public Pos_ProductTransactionType
        (
        int pos_ProductTransactionTypeID, 
        string productTransactionTypeName, 
        string centralStockFormula, 
        string showRoomFormula, 
        string extraField1, 
        string extraField2, 
        string extraField3
        )
    {
        this.Pos_ProductTransactionTypeID = pos_ProductTransactionTypeID;
        this.ProductTransactionTypeName = productTransactionTypeName;
        this.CentralStockFormula = centralStockFormula;
        this.ShowRoomFormula = showRoomFormula;
        this.ExtraField1 = extraField1;
        this.ExtraField2 = extraField2;
        this.ExtraField3 = extraField3;
    }


    private int _pos_ProductTransactionTypeID;
    public int Pos_ProductTransactionTypeID
    {
        get { return _pos_ProductTransactionTypeID; }
        set { _pos_ProductTransactionTypeID = value; }
    }

    private string _productTransactionTypeName;
    public string ProductTransactionTypeName
    {
        get { return _productTransactionTypeName; }
        set { _productTransactionTypeName = value; }
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
}
