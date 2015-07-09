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

public class Inv_UtilizationDetails
{
    public Inv_UtilizationDetails()
    {
    }

    public Inv_UtilizationDetails
        (
        int inv_UtilizationDetailsID, 
        int pos_SizeID, 
        int productID, 
        int inv_ItemID, 
        int inv_UtilizationID, 
        int inv_ItemTransactionID, 
        decimal fabricsCost, 
        decimal accesoriesCost, 
        decimal overhead, 
        decimal othersCost, 
        decimal productionQuantity, 
        decimal processedQuantity, 
        bool isReject, 
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
        this.Inv_UtilizationDetailsID = inv_UtilizationDetailsID;
        this.Pos_SizeID = pos_SizeID;
        this.ProductID = productID;
        this.Inv_ItemID = inv_ItemID;
        this.Inv_UtilizationID = inv_UtilizationID;
        this.Inv_ItemTransactionID = inv_ItemTransactionID;
        this.FabricsCost = fabricsCost;
        this.AccesoriesCost = accesoriesCost;
        this.Overhead = overhead;
        this.OthersCost = othersCost;
        this.ProductionQuantity = productionQuantity;
        this.ProcessedQuantity = processedQuantity;
        this.IsReject = isReject;
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


    private int _inv_UtilizationDetailsID;
    public int Inv_UtilizationDetailsID
    {
        get { return _inv_UtilizationDetailsID; }
        set { _inv_UtilizationDetailsID = value; }
    }

    private int _pos_SizeID;
    public int Pos_SizeID
    {
        get { return _pos_SizeID; }
        set { _pos_SizeID = value; }
    }

    private int _productID;
    public int ProductID
    {
        get { return _productID; }
        set { _productID = value; }
    }

    private int _inv_ItemID;
    public int Inv_ItemID
    {
        get { return _inv_ItemID; }
        set { _inv_ItemID = value; }
    }

    private int _inv_UtilizationID;
    public int Inv_UtilizationID
    {
        get { return _inv_UtilizationID; }
        set { _inv_UtilizationID = value; }
    }

    private int _inv_ItemTransactionID;
    public int Inv_ItemTransactionID
    {
        get { return _inv_ItemTransactionID; }
        set { _inv_ItemTransactionID = value; }
    }

    private decimal _fabricsCost;
    public decimal FabricsCost
    {
        get { return _fabricsCost; }
        set { _fabricsCost = value; }
    }

    private decimal _accesoriesCost;
    public decimal AccesoriesCost
    {
        get { return _accesoriesCost; }
        set { _accesoriesCost = value; }
    }

    private decimal _overhead;
    public decimal Overhead
    {
        get { return _overhead; }
        set { _overhead = value; }
    }

    private decimal _othersCost;
    public decimal OthersCost
    {
        get { return _othersCost; }
        set { _othersCost = value; }
    }

    private decimal _productionQuantity;
    public decimal ProductionQuantity
    {
        get { return _productionQuantity; }
        set { _productionQuantity = value; }
    }

    private decimal _processedQuantity;
    public decimal ProcessedQuantity
    {
        get { return _processedQuantity; }
        set { _processedQuantity = value; }
    }

    private bool _isReject;
    public bool IsReject
    {
        get { return _isReject; }
        set { _isReject = value; }
    }

    private string _extraField1;
    /// <summary>
    /// Color
    /// </summary>
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
