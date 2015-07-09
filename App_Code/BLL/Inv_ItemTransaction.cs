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

public class Inv_ItemTransaction
{
    public Inv_ItemTransaction()
    {
    }

    public Inv_ItemTransaction
        (
        int inv_ItemTransactionID, 
        int itemID, 
        decimal quantity, 
        int itemTrasactionTypeID, 
        decimal referenceID, 
        string extraField1, 
        string extraField2, 
        string extraField3, 
        string extraField4, 
        string extraField5, 
        int addedBy, 
        DateTime addedDate, 
        int updatedBy, 
        DateTime updatedDate, 
        int rowStatusID
        )
    {
        this.Inv_ItemTransactionID = inv_ItemTransactionID;
        this.ItemID = itemID;
        this.Quantity = quantity;
        this.ItemTrasactionTypeID = itemTrasactionTypeID;
        this.ReferenceID = referenceID;
        this.ExtraField1 = extraField1;
        this.ExtraField2 = extraField2;
        this.ExtraField3 = extraField3;
        this.ExtraField4 = extraField4;
        this.ExtraField5 = extraField5;
        this.AddedBy = addedBy;
        this.AddedDate = addedDate;
        this.UpdatedBy = updatedBy;
        this.UpdatedDate = updatedDate;
        this.RowStatusID = rowStatusID;
    }


    private int _inv_ItemTransactionID;
    public int Inv_ItemTransactionID
    {
        get { return _inv_ItemTransactionID; }
        set { _inv_ItemTransactionID = value; }
    }

    private int _itemID;
    public int ItemID
    {
        get { return _itemID; }
        set { _itemID = value; }
    }

    private decimal _quantity;
    public decimal Quantity
    {
        get { return _quantity; }
        set { _quantity = value; }
    }

    private int _itemTrasactionTypeID;
    public int ItemTrasactionTypeID
    {
        get { return _itemTrasactionTypeID; }
        set { _itemTrasactionTypeID = value; }
    }

    private decimal _referenceID;
    public decimal ReferenceID
    {
        get { return _referenceID; }
        set { _referenceID = value; }
    }

    private string _extraField1;
    /// <summary>
    /// IssueRetuen Time IssueDetailsID
    /// </summary>
    public string ExtraField1
    {
        get { return _extraField1; }
        set { _extraField1 = value; }
    }

    private string _extraField2;
    /// <summary>
    /// IssueDetialsID at Issue Time
    /// </summary>
    public string ExtraField2
    {
        get { return _extraField2; }
        set { _extraField2 = value; }
    }

    private string _extraField3;
    /// <summary>
    /// Utilization time Inv_IssueDetailsID
    /// </summary>
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
    /// <summary>
    /// Issue Master ID / Issue Return Master ID
    /// </summary>
    public string ExtraField5
    {
        get { return _extraField5; }
        set { _extraField5 = value; }
    }

    private int _addedBy;
    public int AddedBy
    {
        get { return _addedBy; }
        set { _addedBy = value; }
    }

    private DateTime _addedDate;
    public DateTime AddedDate
    {
        get { return _addedDate; }
        set { _addedDate = value; }
    }

    private int _updatedBy;
    public int UpdatedBy
    {
        get { return _updatedBy; }
        set { _updatedBy = value; }
    }

    private DateTime _updatedDate;
    /// <summary>
    /// Transaction Date
    /// </summary>
    public DateTime UpdatedDate
    {
        get { return _updatedDate; }
        set { _updatedDate = value; }
    }

    private int _rowStatusID;
    public int RowStatusID
    {
        get { return _rowStatusID; }
        set { _rowStatusID = value; }
    }
}
