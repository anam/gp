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

public class Inv_PurchaseAdjustment
{
    public Inv_PurchaseAdjustment()
    {
    }

    public Inv_PurchaseAdjustment
        (
        int inv_PurchaseAdjustmentID, 
        DateTime purchseAdjustmentDate, 
        string purchaseIDs, 
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
        this.Inv_PurchaseAdjustmentID = inv_PurchaseAdjustmentID;
        this.PurchseAdjustmentDate = purchseAdjustmentDate;
        this.PurchaseIDs = purchaseIDs;
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


    private int _inv_PurchaseAdjustmentID;
    public int Inv_PurchaseAdjustmentID
    {
        get { return _inv_PurchaseAdjustmentID; }
        set { _inv_PurchaseAdjustmentID = value; }
    }

    private DateTime _purchseAdjustmentDate;
    public DateTime PurchseAdjustmentDate
    {
        get { return _purchseAdjustmentDate; }
        set { _purchseAdjustmentDate = value; }
    }

    private string _purchaseIDs;
    public string PurchaseIDs
    {
        get { return _purchaseIDs; }
        set { _purchaseIDs = value; }
    }

    private int _workSatationID;
    public int WorkSatationID
    {
        get { return _workSatationID; }
        set { _workSatationID = value; }
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
