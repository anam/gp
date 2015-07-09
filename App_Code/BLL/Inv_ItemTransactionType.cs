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

public class Inv_ItemTransactionType
{
    public Inv_ItemTransactionType()
    {
    }

    public Inv_ItemTransactionType
        (
        int inv_ItemTransactionTypeID, 
        string itemTransactionTypeName, 
        int rowStatusID
        )
    {
        this.Inv_ItemTransactionTypeID = inv_ItemTransactionTypeID;
        this.ItemTransactionTypeName = itemTransactionTypeName;
        this.RowStatusID = rowStatusID;
    }


    private int _inv_ItemTransactionTypeID;
    public int Inv_ItemTransactionTypeID
    {
        get { return _inv_ItemTransactionTypeID; }
        set { _inv_ItemTransactionTypeID = value; }
    }

    private string _itemTransactionTypeName;
    public string ItemTransactionTypeName
    {
        get { return _itemTransactionTypeName; }
        set { _itemTransactionTypeName = value; }
    }

    private int _rowStatusID;
    public int RowStatusID
    {
        get { return _rowStatusID; }
        set { _rowStatusID = value; }
    }
}
