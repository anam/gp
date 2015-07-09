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

public class Pos_SizeProduct
{
    public Pos_SizeProduct()
    {
    }

    public Pos_SizeProduct
        (
        int pos_SizeProductID, 
        int pos_SizeID, 
        int productID
        )
    {
        this.Pos_SizeProductID = pos_SizeProductID;
        this.Pos_SizeID = pos_SizeID;
        this.ProductID = productID;
    }


    private int _pos_SizeProductID;
    public int Pos_SizeProductID
    {
        get { return _pos_SizeProductID; }
        set { _pos_SizeProductID = value; }
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
}
