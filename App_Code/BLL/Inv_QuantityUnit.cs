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

public class Inv_QuantityUnit
{
    public Inv_QuantityUnit()
    {
    }

    public Inv_QuantityUnit
        (
        int inv_QuantityUnitID, 
        string quantityUnitName, 
        int rowStatusID
        )
    {
        this.Inv_QuantityUnitID = inv_QuantityUnitID;
        this.QuantityUnitName = quantityUnitName;
        this.RowStatusID = rowStatusID;
    }


    private int _inv_QuantityUnitID;
    public int Inv_QuantityUnitID
    {
        get { return _inv_QuantityUnitID; }
        set { _inv_QuantityUnitID = value; }
    }

    private string _quantityUnitName;
    public string QuantityUnitName
    {
        get { return _quantityUnitName; }
        set { _quantityUnitName = value; }
    }

    private int _rowStatusID;
    public int RowStatusID
    {
        get { return _rowStatusID; }
        set { _rowStatusID = value; }
    }
}
