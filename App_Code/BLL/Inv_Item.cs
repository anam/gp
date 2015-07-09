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

public class Inv_Item
{
    public Inv_Item()
    {
    }

    public Inv_Item
        (
        int inv_ItemID, 
        string itemName, 
        int purchaseID, 
        string itemCode, 
        int rawMaterialID, 
        int storeID, 
        int qualityUnitID, 
        decimal qualityValue, 
        int quantityUnitID, 
        decimal pricePerUnit, 
        decimal purchasedQuantity, 
        decimal issueReturedQuantity, 
        decimal utilizedQuantity, 
        decimal lostQuantity, 
        decimal extraFieldQuantity1, 
        decimal extraFieldQuantity2, 
        decimal extraFieldQuantity3, 
        decimal extraFieldQuantity4, 
        decimal extraFieldQuantity5, 
        string extraField1, 
        string extraField2, 
        string extraField3, 
        string extraField4, 
        string extraField5, 
        string extraField6, 
        string extraField7, 
        string extraField8, 
        string extraField9, 
        string extraField10, 
        int addedBy, 
        DateTime addedDate, 
        int updatedBy, 
        DateTime updatedDate, 
        int rowStatusID
        )
    {
        this.Inv_ItemID = inv_ItemID;
        this.ItemName = itemName;
        this.PurchaseID = purchaseID;
        this.ItemCode = itemCode;
        this.RawMaterialID = rawMaterialID;
        this.StoreID = storeID;
        this.QualityUnitID = qualityUnitID;
        this.QualityValue = qualityValue;
        this.QuantityUnitID = quantityUnitID;
        this.PricePerUnit = pricePerUnit;
        this.PurchasedQuantity = purchasedQuantity;
        this.IssueReturedQuantity = issueReturedQuantity;
        this.UtilizedQuantity = utilizedQuantity;
        this.LostQuantity = lostQuantity;
        this.ExtraFieldQuantity1 = extraFieldQuantity1;
        this.ExtraFieldQuantity2 = extraFieldQuantity2;
        this.ExtraFieldQuantity3 = extraFieldQuantity3;
        this.ExtraFieldQuantity4 = extraFieldQuantity4;
        this.ExtraFieldQuantity5 = extraFieldQuantity5;
        this.ExtraField1 = extraField1;
        this.ExtraField2 = extraField2;
        this.ExtraField3 = extraField3;
        this.ExtraField4 = extraField4;
        this.ExtraField5 = extraField5;
        this.ExtraField6 = extraField6;
        this.ExtraField7 = extraField7;
        this.ExtraField8 = extraField8;
        this.ExtraField9 = extraField9;
        this.ExtraField10 = extraField10;
        this.AddedBy = addedBy;
        this.AddedDate = addedDate;
        this.UpdatedBy = updatedBy;
        this.UpdatedDate = updatedDate;
        this.RowStatusID = rowStatusID;
    }


    private int _inv_ItemID;
    public int Inv_ItemID
    {
        get { return _inv_ItemID; }
        set { _inv_ItemID = value; }
    }

    private string _itemName;
    public string ItemName
    {
        get { return _itemName; }
        set { _itemName = value; }
    }

    private int _purchaseID;
    public int PurchaseID
    {
        get { return _purchaseID; }
        set { _purchaseID = value; }
    }

    private string _itemCode;
    public string ItemCode
    {
        get { return _itemCode; }
        set { _itemCode = value; }
    }

    private int _rawMaterialID;
    public int RawMaterialID
    {
        get { return _rawMaterialID; }
        set { _rawMaterialID = value; }
    }

    private string _rawMaterialTypeName;

    public string RawMaterialTypeName
    {
        get { return _rawMaterialTypeName; }
        set { _rawMaterialTypeName = value; }
    }

    private string _rawMaterialName;

    public string RawMaterialName
    {
        get { return _rawMaterialName; }
        set { _rawMaterialName = value; }
    }

    private int _rawMaterialTypeID;
    public int RawMaterialTypeID
    {
        get { return _rawMaterialTypeID; }
        set { _rawMaterialTypeID = value; }
    }


    private int _storeID;
    public int StoreID
    {
        get { return _storeID; }
        set { _storeID = value; }
    }

    private int _qualityUnitID;
    public int QualityUnitID
    {
        get { return _qualityUnitID; }
        set { _qualityUnitID = value; }
    }

    private string _qualityUnitName;

    public string QualityUnitName
    {
        get { return _qualityUnitName; }
        set { _qualityUnitName = value; }
    }


    private decimal _qualityValue;
    public decimal QualityValue
    {
        get { return _qualityValue; }
        set { _qualityValue = value; }
    }

    private int _quantityUnitID;
    public int QuantityUnitID
    {
        get { return _quantityUnitID; }
        set { _quantityUnitID = value; }
    }

    private string _quantityUnitName;

    public string QuantityUnitName
    {
        get { return _quantityUnitName; }
        set { _quantityUnitName = value; }
    }


    private decimal _pricePerUnit;
    public decimal PricePerUnit
    {
        get { return _pricePerUnit; }
        set { _pricePerUnit = value; }
    }

    private decimal _purchasedQuantity;
    public decimal PurchasedQuantity
    {
        get { return _purchasedQuantity; }
        set { _purchasedQuantity = value; }
    }

    private decimal _purchasedQuantityPrice;
    public decimal PurchasedQuantityPrice
    {
        get { return _purchasedQuantityPrice; }
        set { _purchasedQuantityPrice = value; }
    }

    private decimal _issueReturedQuantity;
    public decimal IssueReturedQuantity
    {
        get { return _issueReturedQuantity; }
        set { _issueReturedQuantity = value; }
    }

    private decimal _utilizedQuantity;
    public decimal UtilizedQuantity
    {
        get { return _utilizedQuantity; }
        set { _utilizedQuantity = value; }
    }

    private decimal _lostQuantity;
    public decimal LostQuantity
    {
        get { return _lostQuantity; }
        set { _lostQuantity = value; }
    }

    private decimal _extraFieldQuantity1;
    /// <summary>
    /// Store quantity
    /// </summary>
    public decimal ExtraFieldQuantity1
    {
        get { return _extraFieldQuantity1; }
        set { _extraFieldQuantity1 = value; }
    }

    private decimal _extraFieldQuantity2;
    /// <summary>
    /// Purchased Adjustment
    /// </summary>
    public decimal ExtraFieldQuantity2
    {
        get { return _extraFieldQuantity2; }
        set { _extraFieldQuantity2 = value; }
    }

    private decimal _extraFieldQuantity3;
    /// <summary>
    /// Purchased return Quantity
    /// </summary>
    public decimal ExtraFieldQuantity3
    {
        get { return _extraFieldQuantity3; }
        set { _extraFieldQuantity3 = value; }
    }

    private decimal _extraFieldQuantity4;
    /// <summary>
    /// Wastage Quantity
    /// </summary>
    public decimal ExtraFieldQuantity4
    {
        get { return _extraFieldQuantity4; }
        set { _extraFieldQuantity4 = value; }
    }

    private decimal _extraFieldQuantity5;
    /// <summary>
    /// Issued Quantity
    /// </summary>
    public decimal ExtraFieldQuantity5
    {
        get { return _extraFieldQuantity5; }
        set { _extraFieldQuantity5 = value; }
    }

    private string _extraField1;
    /// <summary>
    /// Quality Unit
    /// </summary>
    public string ExtraField1
    {
        get { return _extraField1; }
        set { _extraField1 = value; }
    }

    private string _extraField2;
    /// <summary>
    /// Quantity Unit
    /// </summary>
    public string ExtraField2
    {
        get { return _extraField2; }
        set { _extraField2 = value; }
    }

    private string _extraField3;
    /// <summary>
    /// Fabrick for which product
    /// </summary>
    public string ExtraField3
    {
        get { return _extraField3; }
        set { _extraField3 = value; }
    }

    private string _extraField4;
    /// <summary>
    /// Fabrick type ID
    /// </summary>
    public string ExtraField4
    {
        get { return _extraField4; }
        set { _extraField4 = value; }
    }

    private string _extraField5;
    /// <summary>
    /// Color
    /// </summary>
    public string ExtraField5
    {
        get { return _extraField5; }
        set { _extraField5 = value; }
    }

    private string _extraField6;
    /// <summary>
    /// Purchase time payment Mode 'Credit'/'Cash'
    /// </summary>
    public string ExtraField6
    {
        get { return _extraField6; }
        set { _extraField6 = value; }
    }

    private string _extraField7;
    /// <summary>
    /// Note at update time
    /// </summary>
    public string ExtraField7
    {
        get { return _extraField7; }
        set { _extraField7 = value; }
    }

    private string _extraField8;
    public string ExtraField8
    {
        get { return _extraField8; }
        set { _extraField8 = value; }
    }

    private string _extraField9;
    public string ExtraField9
    {
        get { return _extraField9; }
        set { _extraField9 = value; }
    }

    private string _extraField10;
    public string ExtraField10
    {
        get { return _extraField10; }
        set { _extraField10 = value; }
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
