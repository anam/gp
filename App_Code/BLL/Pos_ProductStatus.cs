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

public class Pos_ProductStatus
{
    public Pos_ProductStatus()
    {
    }

    public Pos_ProductStatus
        (
        int pos_ProductStatusID, 
        string productStatusName
        )
    {
        this.Pos_ProductStatusID = pos_ProductStatusID;
        this.ProductStatusName = productStatusName;
    }


    private int _pos_ProductStatusID;
    public int Pos_ProductStatusID
    {
        get { return _pos_ProductStatusID; }
        set { _pos_ProductStatusID = value; }
    }

    private string _productStatusName;
    public string ProductStatusName
    {
        get { return _productStatusName; }
        set { _productStatusName = value; }
    }
}
