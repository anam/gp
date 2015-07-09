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

public class Pos_ProductType
{
    public Pos_ProductType()
    {
    }

    public Pos_ProductType
        (
        int pos_ProductTypeID, 
        string productTypeName
        )
    {
        this.Pos_ProductTypeID = pos_ProductTypeID;
        this.ProductTypeName = productTypeName;
    }


    private int _pos_ProductTypeID;
    public int Pos_ProductTypeID
    {
        get { return _pos_ProductTypeID; }
        set { _pos_ProductTypeID = value; }
    }

    private string _productTypeName;
    public string ProductTypeName
    {
        get { return _productTypeName; }
        set { _productTypeName = value; }
    }
}
