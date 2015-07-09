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

public class Pos_TransactionMaster
{
    public Pos_TransactionMaster()
    {
    }

    public Pos_TransactionMaster
        (
        int pos_TransactionMasterID, 
        DateTime transactionDate, 
        int pos_TransactionTypeID, 
        int transactionID, 
        int toOrFromID, 
        string record, 
        string particulars, 
        int workSatationID, 
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
        this.Pos_TransactionMasterID = pos_TransactionMasterID;
        this.TransactionDate = transactionDate;
        this.Pos_TransactionTypeID = pos_TransactionTypeID;
        this.TransactionID = transactionID;
        this.ToOrFromID = toOrFromID;
        this.Record = record;
        this.Particulars = particulars;
        this.WorkSatationID = workSatationID;
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


    private int _pos_TransactionMasterID;
    public int Pos_TransactionMasterID
    {
        get { return _pos_TransactionMasterID; }
        set { _pos_TransactionMasterID = value; }
    }

    private DateTime _transactionDate;
    public DateTime TransactionDate
    {
        get { return _transactionDate; }
        set { _transactionDate = value; }
    }

    private int _pos_TransactionTypeID;
    public int Pos_TransactionTypeID
    {
        get { return _pos_TransactionTypeID; }
        set { _pos_TransactionTypeID = value; }
    }

    private int _transactionID;
    public int TransactionID
    {
        get { return _transactionID; }
        set { _transactionID = value; }
    }

    private int _toOrFromID;
    public int ToOrFromID
    {
        get { return _toOrFromID; }
        set { _toOrFromID = value; }
    }

    private string _toOrFromName;

    public string ToOrFromName
    {
        get { return _toOrFromName; }
        set { _toOrFromName = value; }
    }

    private string _record;
    /// <summary>
    /// Sales Person
    /// </summary>
    public string Record
    {
        get { return _record; }
        set { _record = value; }
    }

    private string _particulars;
    public string Particulars
    {
        get { return _particulars; }
        set { _particulars = value; }
    }

    private int _workSatationID;
    public int WorkSatationID
    {
        get { return _workSatationID; }
        set { _workSatationID = value; }
    }

    private string _extraField1;
    /// <summary>
    /// Cash Payment Amount
    /// </summary>
    public string ExtraField1
    {
        get { return _extraField1; }
        set { _extraField1 = value; }
    }

    private string _extraField2;
    /// <summary>
    /// Card Payment Amount
    /// For Sales Return The old Invoice #
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
    public string ExtraField4
    {
        get { return _extraField4; }
        set { _extraField4 = value; }
    }

    private string _extraField5;
    /// <summary>
    /// For sending form the Central it is the status pending but then it will be recieved by the receiver it will be blank
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
    /// <summary>
    /// For Sales Return it is the link with the sales return transaction Master ID
    /// </summary>
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
