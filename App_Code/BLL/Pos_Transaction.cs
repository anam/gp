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

public class Pos_Transaction
{
    public Pos_Transaction()
    {
    }

    public Pos_Transaction
        (
        int pos_TransactionID, 
        int pos_ProductID, 
        decimal quantity, 
        int pos_ProductTrasactionTypeID, 
        int pos_ProductTransactionMasterID, 
        int workStationID, 
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
        this.Pos_TransactionID = pos_TransactionID;
        this.Pos_ProductID = pos_ProductID;
        this.Quantity = quantity;
        this.Pos_ProductTrasactionTypeID = pos_ProductTrasactionTypeID;
        this.Pos_ProductTransactionMasterID = pos_ProductTransactionMasterID;
        this.WorkStationID = workStationID;
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


    private int _pos_TransactionID;
    public int Pos_TransactionID
    {
        get { return _pos_TransactionID; }
        set { _pos_TransactionID = value; }
    }

    private int _pos_ProductID;
    public int Pos_ProductID
    {
        get { return _pos_ProductID; }
        set { _pos_ProductID = value; }
    }

    private decimal _quantity;
    public decimal Quantity
    {
        get { return _quantity; }
        set { _quantity = value; }
    }

    private int _pos_ProductTrasactionTypeID;
    public int Pos_ProductTrasactionTypeID
    {
        get { return _pos_ProductTrasactionTypeID; }
        set { _pos_ProductTrasactionTypeID = value; }
    }

    private int _pos_ProductTransactionMasterID;
    public int Pos_ProductTransactionMasterID
    {
        get { return _pos_ProductTransactionMasterID; }
        set { _pos_ProductTransactionMasterID = value; }
    }

    private int _workStationID;
    public int WorkStationID
    {
        get { return _workStationID; }
        set { _workStationID = value; }
    }

    private string _extraField1;
    /// <summary>
    /// Discount Amount
    /// </summary>
    public string ExtraField1
    {
        get { return _extraField1; }
        set { _extraField1 = value; }
    }

    private string _extraField2;
    /// <summary>
    /// Vat Amount
    /// </summary>
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
    /// <summary>
    /// Sales Return Qty
    /// </summary>
    public string ExtraField4
    {
        get { return _extraField4; }
        set { _extraField4 = value; }
    }

    private string _extraField5;
    /// <summary>
    /// For Sales it is the refference
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
