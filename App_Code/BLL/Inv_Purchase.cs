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

public class Inv_Purchase
{
    public Inv_Purchase()
    {
    }

    public Inv_Purchase
        (
        int inv_PurchaseID, 
        string purchaseName, 
        DateTime purchseDate, 
        int suppierID, 
        string invoiceNo, 
        string particulars, 
        bool isPurchase, 
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
        this.Inv_PurchaseID = inv_PurchaseID;
        this.PurchaseName = purchaseName;
        this.PurchseDate = purchseDate;
        this.SuppierID = suppierID;
        this.InvoiceNo = invoiceNo;
        this.Particulars = particulars;
        this.IsPurchase = isPurchase;
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


    private int _inv_PurchaseID;
    public int Inv_PurchaseID
    {
        get { return _inv_PurchaseID; }
        set { _inv_PurchaseID = value; }
    }

    private string _purchaseName;
    public string PurchaseName
    {
        get { return _purchaseName; }
        set { _purchaseName = value; }
    }

    private DateTime _purchseDate;
    public DateTime PurchseDate
    {
        get { return _purchseDate; }
        set { _purchseDate = value; }
    }

    private int _suppierID;
    public int SuppierID
    {
        get { return _suppierID; }
        set { _suppierID = value; }
    }

    private string _supplierName;

    public string SupplierName
    {
        get { return _supplierName; }
        set { _supplierName = value; }
    }

    private string _invoiceNo;
    public string InvoiceNo
    {
        get { return _invoiceNo; }
        set { _invoiceNo = value; }
    }

    private string _particulars;
    public string Particulars
    {
        get { return _particulars; }
        set { _particulars = value; }
    }

    private bool _isPurchase;
    public bool IsPurchase
    {
        get { return _isPurchase; }
        set { _isPurchase = value; }
    }

    private int _workSatationID;
    public int WorkSatationID
    {
        get { return _workSatationID; }
        set { _workSatationID = value; }
    }

    private string _extraField1;
    /// <summary>
    /// Purchase Item
    /// </summary>
    public string ExtraField1
    {
        get { return _extraField1; }
        set { _extraField1 = value; }
    }

    private string _extraField2;
    /// <summary>
    /// JournalMasterID
    /// </summary>
    public string ExtraField2
    {
        get { return _extraField2; }
        set { _extraField2 = value; }
    }

    private string _extraField3;
    /// <summary>
    /// Payment Type
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
