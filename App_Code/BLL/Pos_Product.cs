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

public class Pos_Product
{
    public Pos_Product()
    {
    }

    public Pos_Product
        (
        int pos_ProductID, 
        int productID, 
        int referenceID, 
        int pos_ProductTypeID, 
        string inv_UtilizationDetailsIDs, 
        int productStatusID, 
        string productName, 
        string designCode, 
        int pos_SizeID, 
        int pos_BrandID, 
        int inv_QuantityUnitID, 
        decimal fabricsCost, 
        decimal accesoriesCost, 
        decimal overhead, 
        decimal othersCost, 
        decimal purchasePrice, 
        decimal salePrice, 
        decimal oldSalePrice, 
        string note, 
        string barCode, 
        int pos_ColorID, 
        int pos_FabricsTypeID, 
        string styleCode, 
        string pic1, 
        string pic2, 
        string pic3, 
        decimal vatPercentage, 
        bool isVatExclusive, 
        decimal discountPercentage, 
        decimal discountAmount, 
        string fabricsNo, 
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
        this.Pos_ProductID = pos_ProductID;
        this.ProductID = productID;
        this.ReferenceID = referenceID;
        this.Pos_ProductTypeID = pos_ProductTypeID;
        this.Inv_UtilizationDetailsIDs = inv_UtilizationDetailsIDs;
        this.ProductStatusID = productStatusID;
        this.ProductName = productName;
        this.DesignCode = designCode;
        this.Pos_SizeID = pos_SizeID;
        this.Pos_BrandID = pos_BrandID;
        this.Inv_QuantityUnitID = inv_QuantityUnitID;
        this.FabricsCost = fabricsCost;
        this.AccesoriesCost = accesoriesCost;
        this.Overhead = overhead;
        this.OthersCost = othersCost;
        this.PurchasePrice = purchasePrice;
        this.SalePrice = salePrice;
        this.OldSalePrice = oldSalePrice;
        this.Note = note;
        this.BarCode = barCode;
        this.Pos_ColorID = pos_ColorID;
        this.Pos_FabricsTypeID = pos_FabricsTypeID;
        this.StyleCode = styleCode;
        this.Pic1 = pic1;
        this.Pic2 = pic2;
        this.Pic3 = pic3;
        this.VatPercentage = vatPercentage;
        this.IsVatExclusive = isVatExclusive;
        this.DiscountPercentage = discountPercentage;
        this.DiscountAmount = discountAmount;
        this.FabricsNo = fabricsNo;
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


    private int _pos_ProductID;
    public int Pos_ProductID
    {
        get { return _pos_ProductID; }
        set { _pos_ProductID = value; }
    }

    private int _productID;
    public int ProductID
    {
        get { return _productID; }
        set { _productID = value; }
    }

    private int _referenceID;
    public int ReferenceID
    {
        get { return _referenceID; }
        set { _referenceID = value; }
    }

    private int _pos_ProductTypeID;
    public int Pos_ProductTypeID
    {
        get { return _pos_ProductTypeID; }
        set { _pos_ProductTypeID = value; }
    }

    private string _inv_UtilizationDetailsIDs;
    public string Inv_UtilizationDetailsIDs
    {
        get { return _inv_UtilizationDetailsIDs; }
        set { _inv_UtilizationDetailsIDs = value; }
    }

    private int _productStatusID;
    public int ProductStatusID
    {
        get { return _productStatusID; }
        set { _productStatusID = value; }
    }

    private string _productName;
    public string ProductName
    {
        get { return _productName; }
        set { _productName = value; }
    }

    private string _designCode;
    public string DesignCode
    {
        get { return _designCode; }
        set { _designCode = value; }
    }

    private int _pos_SizeID;
    public int Pos_SizeID
    {
        get { return _pos_SizeID; }
        set { _pos_SizeID = value; }
    }

    private int _pos_BrandID;
    public int Pos_BrandID
    {
        get { return _pos_BrandID; }
        set { _pos_BrandID = value; }
    }

    private int _inv_QuantityUnitID;
    public int Inv_QuantityUnitID
    {
        get { return _inv_QuantityUnitID; }
        set { _inv_QuantityUnitID = value; }
    }

    private string _Inv_QuantityUniteName;

    public string Inv_QuantityUniteName
    {
        get { return _Inv_QuantityUniteName; }
        set { _Inv_QuantityUniteName = value; }
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
    /// <summary>
    /// Labour Cost
    /// </summary>
    public decimal OthersCost
    {
        get { return _othersCost; }
        set { _othersCost = value; }
    }

    private decimal _purchasePrice;
    public decimal PurchasePrice
    {
        get { return _purchasePrice; }
        set { _purchasePrice = value; }
    }

    private decimal _salePrice;
    public decimal SalePrice
    {
        get { return _salePrice; }
        set { _salePrice = value; }
    }

    private decimal _oldSalePrice;
    public decimal OldSalePrice
    {
        get { return _oldSalePrice; }
        set { _oldSalePrice = value; }
    }

    private string _note;
    public string Note
    {
        get { return _note; }
        set { _note = value; }
    }

    private string _barCode;
    public string BarCode
    {
        get { return _barCode; }
        set { _barCode = value; }
    }

    private int _pos_ColorID;
    public int Pos_ColorID
    {
        get { return _pos_ColorID; }
        set { _pos_ColorID = value; }
    }

    private int _pos_FabricsTypeID;
    public int Pos_FabricsTypeID
    {
        get { return _pos_FabricsTypeID; }
        set { _pos_FabricsTypeID = value; }
    }

    private string _styleCode;
    public string StyleCode
    {
        get { return _styleCode; }
        set { _styleCode = value; }
    }

    private string _pic1;
    public string Pic1
    {
        get { return _pic1; }
        set { _pic1 = value; }
    }

    private string _pic2;
    /// <summary>
    /// For 10 DIGIT
    /// </summary>
    public string Pic2
    {
        get { return _pic2; }
        set { _pic2 = value; }
    }

    private string _pic3;
    public string Pic3
    {
        get { return _pic3; }
        set { _pic3 = value; }
    }

    private decimal _vatPercentage;
    public decimal VatPercentage
    {
        get { return _vatPercentage; }
        set { _vatPercentage = value; }
    }

    private bool _isVatExclusive;
    public bool IsVatExclusive
    {
        get { return _isVatExclusive; }
        set { _isVatExclusive = value; }
    }

    private decimal _discountPercentage;
    public decimal DiscountPercentage
    {
        get { return _discountPercentage; }
        set { _discountPercentage = value; }
    }

    private decimal _discountAmount;
    public decimal DiscountAmount
    {
        get { return _discountAmount; }
        set { _discountAmount = value; }
    }

    private string _fabricsNo;
    /// <summary>
    /// Production Unit
    /// </summary>
    public string FabricsNo
    {
        get { return _fabricsNo; }
        set { _fabricsNo = value; }
    }

    private string _extraField1;
    /// <summary>
    /// Stock  / Select time e Qty
    /// </summary>
    public string ExtraField1
    {
        get { return _extraField1; }
        set { _extraField1 = value; }
    }

    private string _extraField2;
    /// <summary>
    /// Select time e ChartOfAccountLabel4Text
    /// </summary>
    public string ExtraField2
    {
        get { return _extraField2; }
        set { _extraField2 = value; }
    }

    private string _extraField3;
    /// <summary>
    /// QuantityUnitName
    /// </summary>
    public string ExtraField3
    {
        get { return _extraField3; }
        set { _extraField3 = value; }
    }

    private string _extraField4;
    /// <summary>
    /// Avg Cost
    /// </summary>
    public string ExtraField4
    {
        get { return _extraField4; }
        set { _extraField4 = value; }
    }

    private string _extraField5;
    /// <summary>
    /// ColorName
    /// </summary>
    public string ExtraField5
    {
        get { return _extraField5; }
        set { _extraField5 = value; }
    }

    private string _extraField6;
    /// <summary>
    /// SizeName
    /// </summary>
    public string ExtraField6
    {
        get { return _extraField6; }
        set { _extraField6 = value; }
    }

    private string _extraField7;
    /// <summary>
    /// ProductTypeName
    /// </summary>
    public string ExtraField7
    {
        get { return _extraField7; }
        set { _extraField7 = value; }
    }

    private string _extraField8;
    /// <summary>
    /// ProductStatusName
    /// </summary>
    public string ExtraField8
    {
        get { return _extraField8; }
        set { _extraField8 = value; }
    }

    private string _extraField9;
    /// <summary>
    /// BrandName / Purchase time Supplier ID
    /// </summary>
    public string ExtraField9
    {
        get { return _extraField9; }
        set { _extraField9 = value; }
    }

    private string _extraField10;
    /// <summary>
    /// For purchase time tmp JournalMasterID
    /// </summary>
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

    private decimal _production_Stock;
    public decimal Production_Stock
    {
        get { return _production_Stock; }
        set { _production_Stock = value; }
    }

    private decimal _production_Current;
    public decimal Production_Current
    {
        get { return _production_Current; }
        set { _production_Current = value; }
    }

    private decimal _productionAdjustment_Stock;
    public decimal ProductionAdjustment_Stock
    {
        get { return _productionAdjustment_Stock; }
        set { _productionAdjustment_Stock = value; }
    }

    private decimal _productionAdjustment_Current;
    public decimal ProductionAdjustment_Current
    {
        get { return _productionAdjustment_Current; }
        set { _productionAdjustment_Current = value; }
    }

    private decimal _purchase_Stock;
    public decimal Purchase_Stock
    {
        get { return _purchase_Stock; }
        set { _purchase_Stock = value; }
    }

    private decimal _purchase_Current;
    public decimal Purchase_Current
    {
        get { return _purchase_Current; }
        set { _purchase_Current = value; }
    }

    private decimal _purchaseAdjustment_Stock;
    public decimal PurchaseAdjustment_Stock
    {
        get { return _purchaseAdjustment_Stock; }
        set { _purchaseAdjustment_Stock = value; }
    }

    private decimal _purchaseAdjustment_Current;
    public decimal PurchaseAdjustment_Current
    {
        get { return _purchaseAdjustment_Current; }
        set { _purchaseAdjustment_Current = value; }
    }

    private decimal _purchaseReturn_Stock;
    public decimal PurchaseReturn_Stock
    {
        get { return _purchaseReturn_Stock; }
        set { _purchaseReturn_Stock = value; }
    }

    private decimal _purchaseReturn_Current;
    public decimal PurchaseReturn_Current
    {
        get { return _purchaseReturn_Current; }
        set { _purchaseReturn_Current = value; }
    }

    private decimal _lost_HO_Stock;
    public decimal Lost_HO_Stock
    {
        get { return _lost_HO_Stock; }
        set { _lost_HO_Stock = value; }
    }

    private decimal _lost_HO_Current;
    public decimal Lost_HO_Current
    {
        get { return _lost_HO_Current; }
        set { _lost_HO_Current = value; }
    }

    private decimal _wastage_HO_Stock;
    public decimal Wastage_HO_Stock
    {
        get { return _wastage_HO_Stock; }
        set { _wastage_HO_Stock = value; }
    }

    private decimal _wastage_HO_Current;
    public decimal Wastage_HO_Current
    {
        get { return _wastage_HO_Current; }
        set { _wastage_HO_Current = value; }
    }

    private decimal _issue_Wash_HO_Stock;
    public decimal Issue_Wash_HO_Stock
    {
        get { return _issue_Wash_HO_Stock; }
        set { _issue_Wash_HO_Stock = value; }
    }

    private decimal _issue_Wash_HO_Current;
    public decimal Issue_Wash_HO_Current
    {
        get { return _issue_Wash_HO_Current; }
        set { _issue_Wash_HO_Current = value; }
    }

    private decimal _back_After_Wash_Stock;
    public decimal Back_After_Wash_Stock
    {
        get { return _back_After_Wash_Stock; }
        set { _back_After_Wash_Stock = value; }
    }

    private decimal _back_After_Wash_Current;
    public decimal Back_After_Wash_Current
    {
        get { return _back_After_Wash_Current; }
        set { _back_After_Wash_Current = value; }
    }

    private decimal _issue_for_Repair_Stock;
    public decimal Issue_for_Repair_Stock
    {
        get { return _issue_for_Repair_Stock; }
        set { _issue_for_Repair_Stock = value; }
    }

    private decimal _issue_for_Repair_Current;
    public decimal Issue_for_Repair_Current
    {
        get { return _issue_for_Repair_Current; }
        set { _issue_for_Repair_Current = value; }
    }

    private decimal _back_after_repair_Stock;
    public decimal Back_after_repair_Stock
    {
        get { return _back_after_repair_Stock; }
        set { _back_after_repair_Stock = value; }
    }

    private decimal _back_after_repair_Current;
    public decimal Back_after_repair_Current
    {
        get { return _back_after_repair_Current; }
        set { _back_after_repair_Current = value; }
    }

    private decimal _issue_to_Showroom_Stock;
    public decimal Issue_to_Showroom_Stock
    {
        get { return _issue_to_Showroom_Stock; }
        set { _issue_to_Showroom_Stock = value; }
    }

    private decimal _issue_to_Showroom_Current;
    public decimal Issue_to_Showroom_Current
    {
        get { return _issue_to_Showroom_Current; }
        set { _issue_to_Showroom_Current = value; }
    }

    private decimal _issue_Return_HO_Stock;
    public decimal Issue_Return_HO_Stock
    {
        get { return _issue_Return_HO_Stock; }
        set { _issue_Return_HO_Stock = value; }
    }

    private decimal _issue_Return_HO_Current;
    public decimal Issue_Return_HO_Current
    {
        get { return _issue_Return_HO_Current; }
        set { _issue_Return_HO_Current = value; }
    }

    private decimal _send_another_Showroom_Stock;
    public decimal Send_another_Showroom_Stock
    {
        get { return _send_another_Showroom_Stock; }
        set { _send_another_Showroom_Stock = value; }
    }

    private decimal _send_another_Showroom_Current;
    public decimal Send_another_Showroom_Current
    {
        get { return _send_another_Showroom_Current; }
        set { _send_another_Showroom_Current = value; }
    }

    private decimal _received_another_Showroom_Stock;
    public decimal Received_another_Showroom_Stock
    {
        get { return _received_another_Showroom_Stock; }
        set { _received_another_Showroom_Stock = value; }
    }

    private decimal _received_another_Showroom_Current;
    public decimal Received_another_Showroom_Current
    {
        get { return _received_another_Showroom_Current; }
        set { _received_another_Showroom_Current = value; }
    }

    private decimal _sales_Stock;
    public decimal Sales_Stock
    {
        get { return _sales_Stock; }
        set { _sales_Stock = value; }
    }

    private decimal _sales_Current;
    public decimal Sales_Current
    {
        get { return _sales_Current; }
        set { _sales_Current = value; }
    }

    private decimal _sales_return_Stock;
    public decimal Sales_return_Stock
    {
        get { return _sales_return_Stock; }
        set { _sales_return_Stock = value; }
    }

    private decimal _sales_return_Current;
    public decimal Sales_return_Current
    {
        get { return _sales_return_Current; }
        set { _sales_return_Current = value; }
    }

    private decimal _gift_HO_Stock;
    public decimal Gift_HO_Stock
    {
        get { return _gift_HO_Stock; }
        set { _gift_HO_Stock = value; }
    }

    private decimal _gift_HO_Current;
    public decimal Gift_HO_Current
    {
        get { return _gift_HO_Current; }
        set { _gift_HO_Current = value; }
    }

    private decimal _gift_Showroom_Stock;
    public decimal Gift_Showroom_Stock
    {
        get { return _gift_Showroom_Stock; }
        set { _gift_Showroom_Stock = value; }
    }

    private decimal _gift_Showroom_Current;
    public decimal Gift_Showroom_Current
    {
        get { return _gift_Showroom_Current; }
        set { _gift_Showroom_Current = value; }
    }

    private decimal _send_wash_Showroom_Stock;
    public decimal Send_wash_Showroom_Stock
    {
        get { return _send_wash_Showroom_Stock; }
        set { _send_wash_Showroom_Stock = value; }
    }

    private decimal _send_wash_Showroom_Current;
    public decimal Send_wash_Showroom_Current
    {
        get { return _send_wash_Showroom_Current; }
        set { _send_wash_Showroom_Current = value; }
    }

    private decimal _received_wash_Showroom_Stock;
    public decimal Received_wash_Showroom_Stock
    {
        get { return _received_wash_Showroom_Stock; }
        set { _received_wash_Showroom_Stock = value; }
    }

    private decimal _received_wash_Showroom_Current;
    public decimal Received_wash_Showroom_Current
    {
        get { return _received_wash_Showroom_Current; }
        set { _received_wash_Showroom_Current = value; }
    }

    private decimal _send_repair_Showroom_Stock;
    public decimal Send_repair_Showroom_Stock
    {
        get { return _send_repair_Showroom_Stock; }
        set { _send_repair_Showroom_Stock = value; }
    }

    private decimal _send_repair_Showroom_Current;
    public decimal Send_repair_Showroom_Current
    {
        get { return _send_repair_Showroom_Current; }
        set { _send_repair_Showroom_Current = value; }
    }

    private decimal _received_repair_Showroom_Stock;
    public decimal Received_repair_Showroom_Stock
    {
        get { return _received_repair_Showroom_Stock; }
        set { _received_repair_Showroom_Stock = value; }
    }

    private decimal _received_repair_Showroom_Current;
    public decimal Received_repair_Showroom_Current
    {
        get { return _received_repair_Showroom_Current; }
        set { _received_repair_Showroom_Current = value; }
    }

    private decimal _lost_Showroom_Stock;
    public decimal Lost_Showroom_Stock
    {
        get { return _lost_Showroom_Stock; }
        set { _lost_Showroom_Stock = value; }
    }

    private decimal _lost_Showroom_Current;
    public decimal Lost_Showroom_Current
    {
        get { return _lost_Showroom_Current; }
        set { _lost_Showroom_Current = value; }
    }

    private decimal _wastage_Showroom_Stock;
    public decimal Wastage_Showroom_Stock
    {
        get { return _wastage_Showroom_Stock; }
        set { _wastage_Showroom_Stock = value; }
    }

    private decimal _wastage_Showroom_Current;
    public decimal Wastage_Showroom_Current
    {
        get { return _wastage_Showroom_Current; }
        set { _wastage_Showroom_Current = value; }
    }

    private decimal _adjustment_Add_Showroom_Stock;
    public decimal Adjustment_Add_Showroom_Stock
    {
        get { return _adjustment_Add_Showroom_Stock; }
        set { _adjustment_Add_Showroom_Stock = value; }
    }

    private decimal _adjustment_Add_Showroom_Current;
    public decimal Adjustment_Add_Showroom_Current
    {
        get { return _adjustment_Add_Showroom_Current; }
        set { _adjustment_Add_Showroom_Current = value; }
    }

    private decimal _adjustment_Sub_Showroom_Stock;
    public decimal Adjustment_Sub_Showroom_Stock
    {
        get { return _adjustment_Sub_Showroom_Stock; }
        set { _adjustment_Sub_Showroom_Stock = value; }
    }

    private decimal _adjustment_Sub_Showroom_Current;
    public decimal Adjustment_Sub_Showroom_Current
    {
        get { return _adjustment_Sub_Showroom_Current; }
        set { _adjustment_Sub_Showroom_Current = value; }
    }




    private decimal _production_Stock_price;
    public decimal Production_Stock_price
    {
        get { return _production_Stock_price; }
        set { _production_Stock_price = value; }
    }

    private decimal _production_Current_price;
    public decimal Production_Current_price
    {
        get { return _production_Current_price; }
        set { _production_Current_price = value; }
    }

    private decimal _productionAdjustment_Stock_price;
    public decimal ProductionAdjustment_Stock_price
    {
        get { return _productionAdjustment_Stock_price; }
        set { _productionAdjustment_Stock_price = value; }
    }

    private decimal _productionAdjustment_Current_price;
    public decimal ProductionAdjustment_Current_price
    {
        get { return _productionAdjustment_Current_price; }
        set { _productionAdjustment_Current_price = value; }
    }

    private decimal _purchase_Stock_price;
    public decimal Purchase_Stock_price
    {
        get { return _purchase_Stock_price; }
        set { _purchase_Stock_price = value; }
    }

    private decimal _purchase_Current_price;
    public decimal Purchase_Current_price
    {
        get { return _purchase_Current_price; }
        set { _purchase_Current_price = value; }
    }

    private decimal _purchaseAdjustment_Stock_price;
    public decimal PurchaseAdjustment_Stock_price
    {
        get { return _purchaseAdjustment_Stock_price; }
        set { _purchaseAdjustment_Stock_price = value; }
    }

    private decimal _purchaseAdjustment_Current_price;
    public decimal PurchaseAdjustment_Current_price
    {
        get { return _purchaseAdjustment_Current_price; }
        set { _purchaseAdjustment_Current_price = value; }
    }

    private decimal _purchaseReturn_Stock_price;
    public decimal PurchaseReturn_Stock_price
    {
        get { return _purchaseReturn_Stock_price; }
        set { _purchaseReturn_Stock_price = value; }
    }

    private decimal _purchaseReturn_Current_price;
    public decimal PurchaseReturn_Current_price
    {
        get { return _purchaseReturn_Current_price; }
        set { _purchaseReturn_Current_price = value; }
    }

    private decimal _lost_HO_Stock_price;
    public decimal Lost_HO_Stock_price
    {
        get { return _lost_HO_Stock_price; }
        set { _lost_HO_Stock_price = value; }
    }

    private decimal _lost_HO_Current_price;
    public decimal Lost_HO_Current_price
    {
        get { return _lost_HO_Current_price; }
        set { _lost_HO_Current_price = value; }
    }

    private decimal _wastage_HO_Stock_price;
    public decimal Wastage_HO_Stock_price
    {
        get { return _wastage_HO_Stock_price; }
        set { _wastage_HO_Stock_price = value; }
    }

    private decimal _wastage_HO_Current_price;
    public decimal Wastage_HO_Current_price
    {
        get { return _wastage_HO_Current_price; }
        set { _wastage_HO_Current_price = value; }
    }

    private decimal _issue_Wash_HO_Stock_price;
    public decimal Issue_Wash_HO_Stock_price
    {
        get { return _issue_Wash_HO_Stock_price; }
        set { _issue_Wash_HO_Stock_price = value; }
    }

    private decimal _issue_Wash_HO_Current_price;
    public decimal Issue_Wash_HO_Current_price
    {
        get { return _issue_Wash_HO_Current_price; }
        set { _issue_Wash_HO_Current_price = value; }
    }

    private decimal _back_After_Wash_Stock_price;
    public decimal Back_After_Wash_Stock_price
    {
        get { return _back_After_Wash_Stock_price; }
        set { _back_After_Wash_Stock_price = value; }
    }

    private decimal _back_After_Wash_Current_price;
    public decimal Back_After_Wash_Current_price
    {
        get { return _back_After_Wash_Current_price; }
        set { _back_After_Wash_Current_price = value; }
    }

    private decimal _issue_for_Repair_Stock_price;
    public decimal Issue_for_Repair_Stock_price
    {
        get { return _issue_for_Repair_Stock_price; }
        set { _issue_for_Repair_Stock_price = value; }
    }

    private decimal _issue_for_Repair_Current_price;
    public decimal Issue_for_Repair_Current_price
    {
        get { return _issue_for_Repair_Current_price; }
        set { _issue_for_Repair_Current_price = value; }
    }

    private decimal _back_after_repair_Stock_price;
    public decimal Back_after_repair_Stock_price
    {
        get { return _back_after_repair_Stock_price; }
        set { _back_after_repair_Stock_price = value; }
    }

    private decimal _back_after_repair_Current_price;
    public decimal Back_after_repair_Current_price
    {
        get { return _back_after_repair_Current_price; }
        set { _back_after_repair_Current_price = value; }
    }

    private decimal _issue_to_Showroom_Stock_price;
    public decimal Issue_to_Showroom_Stock_price
    {
        get { return _issue_to_Showroom_Stock_price; }
        set { _issue_to_Showroom_Stock_price = value; }
    }

    private decimal _issue_to_Showroom_Current_price;
    public decimal Issue_to_Showroom_Current_price
    {
        get { return _issue_to_Showroom_Current_price; }
        set { _issue_to_Showroom_Current_price = value; }
    }

    private decimal _issue_Return_HO_Stock_price;
    public decimal Issue_Return_HO_Stock_price
    {
        get { return _issue_Return_HO_Stock_price; }
        set { _issue_Return_HO_Stock_price = value; }
    }

    private decimal _issue_Return_HO_Current_price;
    public decimal Issue_Return_HO_Current_price
    {
        get { return _issue_Return_HO_Current_price; }
        set { _issue_Return_HO_Current_price = value; }
    }

    private decimal _send_another_Showroom_Stock_price;
    public decimal Send_another_Showroom_Stock_price
    {
        get { return _send_another_Showroom_Stock_price; }
        set { _send_another_Showroom_Stock_price = value; }
    }

    private decimal _send_another_Showroom_Current_price;
    public decimal Send_another_Showroom_Current_price
    {
        get { return _send_another_Showroom_Current_price; }
        set { _send_another_Showroom_Current_price = value; }
    }

    private decimal _received_another_Showroom_Stock_price;
    public decimal Received_another_Showroom_Stock_price
    {
        get { return _received_another_Showroom_Stock_price; }
        set { _received_another_Showroom_Stock_price = value; }
    }

    private decimal _received_another_Showroom_Current_price;
    public decimal Received_another_Showroom_Current_price
    {
        get { return _received_another_Showroom_Current_price; }
        set { _received_another_Showroom_Current_price = value; }
    }

    private decimal _sales_Stock_price;
    public decimal Sales_Stock_price
    {
        get { return _sales_Stock_price; }
        set { _sales_Stock_price = value; }
    }

    private decimal _sales_Current_price;
    public decimal Sales_Current_price
    {
        get { return _sales_Current_price; }
        set { _sales_Current_price = value; }
    }

    private decimal _sales_return_Stock_price;
    public decimal Sales_return_Stock_price
    {
        get { return _sales_return_Stock_price; }
        set { _sales_return_Stock_price = value; }
    }

    private decimal _sales_return_Current_price;
    public decimal Sales_return_Current_price
    {
        get { return _sales_return_Current_price; }
        set { _sales_return_Current_price = value; }
    }

    private decimal _gift_HO_Stock_price;
    public decimal Gift_HO_Stock_price
    {
        get { return _gift_HO_Stock_price; }
        set { _gift_HO_Stock_price = value; }
    }

    private decimal _gift_HO_Current_price;
    public decimal Gift_HO_Current_price
    {
        get { return _gift_HO_Current_price; }
        set { _gift_HO_Current_price = value; }
    }

    private decimal _gift_Showroom_Stock_price;
    public decimal Gift_Showroom_Stock_price
    {
        get { return _gift_Showroom_Stock_price; }
        set { _gift_Showroom_Stock_price = value; }
    }

    private decimal _gift_Showroom_Current_price;
    public decimal Gift_Showroom_Current_price
    {
        get { return _gift_Showroom_Current_price; }
        set { _gift_Showroom_Current_price = value; }
    }

    private decimal _send_wash_Showroom_Stock_price;
    public decimal Send_wash_Showroom_Stock_price
    {
        get { return _send_wash_Showroom_Stock_price; }
        set { _send_wash_Showroom_Stock_price = value; }
    }

    private decimal _send_wash_Showroom_Current_price;
    public decimal Send_wash_Showroom_Current_price
    {
        get { return _send_wash_Showroom_Current_price; }
        set { _send_wash_Showroom_Current_price = value; }
    }

    private decimal _received_wash_Showroom_Stock_price;
    public decimal Received_wash_Showroom_Stock_price
    {
        get { return _received_wash_Showroom_Stock_price; }
        set { _received_wash_Showroom_Stock_price = value; }
    }

    private decimal _received_wash_Showroom_Current_price;
    public decimal Received_wash_Showroom_Current_price
    {
        get { return _received_wash_Showroom_Current_price; }
        set { _received_wash_Showroom_Current_price = value; }
    }

    private decimal _send_repair_Showroom_Stock_price;
    public decimal Send_repair_Showroom_Stock_price
    {
        get { return _send_repair_Showroom_Stock_price; }
        set { _send_repair_Showroom_Stock_price = value; }
    }

    private decimal _send_repair_Showroom_Current_price;
    public decimal Send_repair_Showroom_Current_price
    {
        get { return _send_repair_Showroom_Current_price; }
        set { _send_repair_Showroom_Current_price = value; }
    }

    private decimal _received_repair_Showroom_Stock_price;
    public decimal Received_repair_Showroom_Stock_price
    {
        get { return _received_repair_Showroom_Stock_price; }
        set { _received_repair_Showroom_Stock_price = value; }
    }

    private decimal _received_repair_Showroom_Current_price;
    public decimal Received_repair_Showroom_Current_price
    {
        get { return _received_repair_Showroom_Current_price; }
        set { _received_repair_Showroom_Current_price = value; }
    }

    private decimal _lost_Showroom_Stock_price;
    public decimal Lost_Showroom_Stock_price
    {
        get { return _lost_Showroom_Stock_price; }
        set { _lost_Showroom_Stock_price = value; }
    }

    private decimal _lost_Showroom_Current_price;
    public decimal Lost_Showroom_Current_price
    {
        get { return _lost_Showroom_Current_price; }
        set { _lost_Showroom_Current_price = value; }
    }

    private decimal _wastage_Showroom_Stock_price;
    public decimal Wastage_Showroom_Stock_price
    {
        get { return _wastage_Showroom_Stock_price; }
        set { _wastage_Showroom_Stock_price = value; }
    }

    private decimal _wastage_Showroom_Current_price;
    public decimal Wastage_Showroom_Current_price
    {
        get { return _wastage_Showroom_Current_price; }
        set { _wastage_Showroom_Current_price = value; }
    }

    private decimal _adjustment_Add_Showroom_Stock_price;
    public decimal Adjustment_Add_Showroom_Stock_price
    {
        get { return _adjustment_Add_Showroom_Stock_price; }
        set { _adjustment_Add_Showroom_Stock_price = value; }
    }

    private decimal _adjustment_Add_Showroom_Current_price;
    public decimal Adjustment_Add_Showroom_Current_price
    {
        get { return _adjustment_Add_Showroom_Current_price; }
        set { _adjustment_Add_Showroom_Current_price = value; }
    }

    private decimal _adjustment_Sub_Showroom_Stock_price;
    public decimal Adjustment_Sub_Showroom_Stock_price
    {
        get { return _adjustment_Sub_Showroom_Stock_price; }
        set { _adjustment_Sub_Showroom_Stock_price = value; }
    }

    private decimal _adjustment_Sub_Showroom_Current_price;
    public decimal Adjustment_Sub_Showroom_Current_price
    {
        get { return _adjustment_Sub_Showroom_Current_price; }
        set { _adjustment_Sub_Showroom_Current_price = value; }
    }


    private decimal _opiningStock;

    public decimal OpiningStock
    {
        get { return _opiningStock; }
        set { _opiningStock = value; }
    }
    private decimal _stockInHand;

    public decimal StockInHand
    {
        get { return _stockInHand; }
        set { _stockInHand = value; }
    }

    private decimal _opiningStockPrice;

    public decimal OpiningStockPrice
    {
        get { return _opiningStockPrice; }
        set { _opiningStockPrice = value; }
    }
    private decimal _stockInHandPrice;

    public decimal StockInHandPrice
    {
        get { return _stockInHandPrice; }
        set { _stockInHandPrice = value; }
    }



}
