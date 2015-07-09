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

public class Pos_FabricsType
{
    public Pos_FabricsType()
    {
    }

    public Pos_FabricsType
        (
        int pos_FabricsTypeID, 
        string fabricsTypeName
        )
    {
        this.Pos_FabricsTypeID = pos_FabricsTypeID;
        this.FabricsTypeName = fabricsTypeName;
    }


    private int _pos_FabricsTypeID;
    public int Pos_FabricsTypeID
    {
        get { return _pos_FabricsTypeID; }
        set { _pos_FabricsTypeID = value; }
    }

    private string _fabricsTypeName;
    public string FabricsTypeName
    {
        get { return _fabricsTypeName; }
        set { _fabricsTypeName = value; }
    }
}
