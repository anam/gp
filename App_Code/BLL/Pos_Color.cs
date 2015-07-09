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

public class Pos_Color
{
    public Pos_Color()
    {
    }

    public Pos_Color
        (
        int pos_ColorID, 
        string colorName
        )
    {
        this.Pos_ColorID = pos_ColorID;
        this.ColorName = colorName;
    }


    private int _pos_ColorID;
    public int Pos_ColorID
    {
        get { return _pos_ColorID; }
        set { _pos_ColorID = value; }
    }

    private string _colorName;
    public string ColorName
    {
        get { return _colorName; }
        set { _colorName = value; }
    }
}
