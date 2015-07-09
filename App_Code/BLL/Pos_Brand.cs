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

public class Pos_Brand
{
    public Pos_Brand()
    {
    }

    public Pos_Brand
        (
        int pos_BrandID, 
        string brandName, 
        string details
        )
    {
        this.Pos_BrandID = pos_BrandID;
        this.BrandName = brandName;
        this.Details = details;
    }


    private int _pos_BrandID;
    public int Pos_BrandID
    {
        get { return _pos_BrandID; }
        set { _pos_BrandID = value; }
    }

    private string _brandName;
    public string BrandName
    {
        get { return _brandName; }
        set { _brandName = value; }
    }

    private string _details;
    public string Details
    {
        get { return _details; }
        set { _details = value; }
    }
}
