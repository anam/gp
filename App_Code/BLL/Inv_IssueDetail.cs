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

public class Inv_IssueDetail
{
    public Inv_IssueDetail()
    {
    }

    public Inv_IssueDetail
        (
        int inv_IssueDetailID, 
        int itemID, 
        decimal quantity, 
        int approximateQuantity, 
        int productID, 
        int additionalWithIssueDetailID, 
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
        this.Inv_IssueDetailID = inv_IssueDetailID;
        this.ItemID = itemID;
        this.Quantity = quantity;
        this.ApproximateQuantity = approximateQuantity;
        this.ProductID = productID;
        this.AdditionalWithIssueDetailID = additionalWithIssueDetailID;
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


    private int _inv_IssueDetailID;
    public int Inv_IssueDetailID
    {
        get { return _inv_IssueDetailID; }
        set { _inv_IssueDetailID = value; }
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

    private int _approximateQuantity;
    public int ApproximateQuantity
    {
        get { return _approximateQuantity; }
        set { _approximateQuantity = value; }
    }

    private int _productID;
    public int ProductID
    {
        get { return _productID; }
        set { _productID = value; }
    }

    private int _additionalWithIssueDetailID;
    /// <summary>
    /// acctual Produced Qty
    /// </summary>
    public int AdditionalWithIssueDetailID
    {
        get { return _additionalWithIssueDetailID; }
        set { _additionalWithIssueDetailID = value; }
    }

    private string _extraField1;
    /// <summary>
    /// Issue Stock quantity
    /// </summary>
    public string ExtraField1
    {
        get { return _extraField1; }
        set { _extraField1 = value; }
    }

    private string _extraField2;
    /// <summary>
    /// Issue return quantity
    /// </summary>
    public string ExtraField2
    {
        get { return _extraField2; }
        set { _extraField2 = value; }
    }

    private string _extraField3;
    /// <summary>
    /// Issue utlized quantity
    /// </summary>
    public string ExtraField3
    {
        get { return _extraField3; }
        set { _extraField3 = value; }
    }

    private string _extraField4;
    /// <summary>
    /// Issue wasted quantity
    /// </summary>
    public string ExtraField4
    {
        get { return _extraField4; }
        set { _extraField4 = value; }
    }

    private string _extraField5;
    /// <summary>
    /// Issue Master ID
    /// </summary>
    public string ExtraField5
    {
        get { return _extraField5; }
        set { _extraField5 = value; }
    }

    private string _parentChildGap;

    public string ParentChildGap
    {
        get { return _parentChildGap; }
        set { _parentChildGap = value; }
    }

    private bool _isProcessed;

    public bool IsProcessed
    {
        get { return _isProcessed; }
        set { _isProcessed = value; }
    }

    private string _itemCode;

    public string ItemCode
    {
        get { return _itemCode; }
        set { _itemCode = value; }
    }

    private int _RawMaterialID;

    public int RawMaterialID
    {
        get { return _RawMaterialID; }
        set { _RawMaterialID = value; }
    }

    private string _itemName;

    public string ItemName
    {
        get { return _itemName; }
        set { _itemName = value; }
    }
    private decimal _pricePerUnit;

    public decimal PricePerUnit
    {
        get { return _pricePerUnit; }
        set { _pricePerUnit = value; }
    }

    private decimal _totalPrice;

    public decimal TotalPrice
    {
        get { return _totalPrice; }
        set { _totalPrice = value; }
    }


    private string _quantityUnitName;

    public string QuantityUnitName
    {
        get { return _quantityUnitName; }
        set { _quantityUnitName = value; }
    }
    private string _qualityUnitName;

    public string QualityUnitName
    {
        get { return _qualityUnitName; }
        set { _qualityUnitName = value; }
    }
    private decimal _qualityUnitValue;

    public decimal QualityUnitValue
    {
        get { return _qualityUnitValue; }
        set { _qualityUnitValue = value; }
    }
    private string _productName;

    public string ProductName
    {
        get { return _productName; }
        set { _productName = value; }
    }

    private int _ACC_HeadTypeID;
    public int ACC_HeadTypeID
    {
        get { return _ACC_HeadTypeID; }
        set { _ACC_HeadTypeID = value; }
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
