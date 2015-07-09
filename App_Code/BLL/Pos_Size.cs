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

public class Pos_Size
{
    public Pos_Size()
    {
    }

    public Pos_Size
        (
        int pos_SizeID, 
        string sizeName, 
        string code
        )
    {
        this.Pos_SizeID = pos_SizeID;
        this.SizeName = sizeName;
        this.Code = code;
    }


    private int _pos_SizeID;
    public int Pos_SizeID
    {
        get { return _pos_SizeID; }
        set { _pos_SizeID = value; }
    }

    private string _sizeName;
    public string SizeName
    {
        get { return _sizeName; }
        set { _sizeName = value; }
    }

    private string _code;
    public string Code
    {
        get { return _code; }
        set { _code = value; }
    }
}
