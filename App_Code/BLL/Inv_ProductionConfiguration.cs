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

public class Inv_ProductionConfiguration
{
    public Inv_ProductionConfiguration()
    {
    }

    public Inv_ProductionConfiguration
        (
        int inv_ProductionConfigurationID, 
        int productID, 
        decimal qualityValue, 
        int qualityUnitID, 
        decimal quantityValue, 
        int quantityUnitID, 
        int rawMaterialID, 
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
        this.Inv_ProductionConfigurationID = inv_ProductionConfigurationID;
        this.ProductID = productID;
        this.QualityValue = qualityValue;
        this.QualityUnitID = qualityUnitID;
        this.QuantityValue = quantityValue;
        this.QuantityUnitID = quantityUnitID;
        this.RawMaterialID = rawMaterialID;
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


    private int _inv_ProductionConfigurationID;
    public int Inv_ProductionConfigurationID
    {
        get { return _inv_ProductionConfigurationID; }
        set { _inv_ProductionConfigurationID = value; }
    }

    private int _productID;
    public int ProductID
    {
        get { return _productID; }
        set { _productID = value; }
    }

    private decimal _qualityValue;
    public decimal QualityValue
    {
        get { return _qualityValue; }
        set { _qualityValue = value; }
    }

    private int _qualityUnitID;
    public int QualityUnitID
    {
        get { return _qualityUnitID; }
        set { _qualityUnitID = value; }
    }

    private decimal _quantityValue;
    public decimal QuantityValue
    {
        get { return _quantityValue; }
        set { _quantityValue = value; }
    }

    private int _quantityUnitID;
    public int QuantityUnitID
    {
        get { return _quantityUnitID; }
        set { _quantityUnitID = value; }
    }

    private int _rawMaterialID;
    public int RawMaterialID
    {
        get { return _rawMaterialID; }
        set { _rawMaterialID = value; }
    }

    private string _extraField1;
    /// <summary>
    /// Product Name
    /// </summary>
    public string ExtraField1
    {
        get { return _extraField1; }
        set { _extraField1 = value; }
    }

    private string _extraField2;
    /// <summary>
    /// RawMaterial Name
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
    /// QualityUnitName
    /// </summary>
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
