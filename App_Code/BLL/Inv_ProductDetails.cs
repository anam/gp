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

public class Inv_ProductDetails
{
    public Inv_ProductDetails()
    {
    }

    public Inv_ProductDetails
        (
        int inv_ProductDetailsID, 
        int productID, 
        int itemID, 
        decimal costing, 
        decimal quantityProduced, 
        decimal quantityUtilized, 
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
        this.Inv_ProductDetailsID = inv_ProductDetailsID;
        this.ProductID = productID;
        this.ItemID = itemID;
        this.Costing = costing;
        this.QuantityProduced = quantityProduced;
        this.QuantityUtilized = quantityUtilized;
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


    private int _inv_ProductDetailsID;
    public int Inv_ProductDetailsID
    {
        get { return _inv_ProductDetailsID; }
        set { _inv_ProductDetailsID = value; }
    }

    private int _productID;
    public int ProductID
    {
        get { return _productID; }
        set { _productID = value; }
    }

    private int _itemID;
    public int ItemID
    {
        get { return _itemID; }
        set { _itemID = value; }
    }

    private decimal _costing;
    public decimal Costing
    {
        get { return _costing; }
        set { _costing = value; }
    }

    private decimal _quantityProduced;
    public decimal QuantityProduced
    {
        get { return _quantityProduced; }
        set { _quantityProduced = value; }
    }

    private decimal _quantityUtilized;
    public decimal QuantityUtilized
    {
        get { return _quantityUtilized; }
        set { _quantityUtilized = value; }
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
