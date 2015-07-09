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

public class Pos_ProductCost
{
    public Pos_ProductCost()
    {
    }

    public Pos_ProductCost
        (
        int pos_ProductCostID, 
        int pos_CostTypeID, 
        int productID, 
        decimal amount, 
        string extraField1, 
        string extraField2, 
        string extraField3
        )
    {
        this.Pos_ProductCostID = pos_ProductCostID;
        this.Pos_CostTypeID = pos_CostTypeID;
        this.ProductID = productID;
        this.Amount = amount;
        this.ExtraField1 = extraField1;
        this.ExtraField2 = extraField2;
        this.ExtraField3 = extraField3;
    }


    private int _pos_ProductCostID;
    public int Pos_ProductCostID
    {
        get { return _pos_ProductCostID; }
        set { _pos_ProductCostID = value; }
    }

    private int _pos_CostTypeID;
    public int Pos_CostTypeID
    {
        get { return _pos_CostTypeID; }
        set { _pos_CostTypeID = value; }
    }

    private int _productID;
    public int ProductID
    {
        get { return _productID; }
        set { _productID = value; }
    }

    private decimal _amount;
    public decimal Amount
    {
        get { return _amount; }
        set { _amount = value; }
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
}
