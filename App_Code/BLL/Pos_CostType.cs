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

public class Pos_CostType
{
    public Pos_CostType()
    {
    }

    public Pos_CostType
        (
        int pos_CostTypeID, 
        string costTypeName
        )
    {
        this.Pos_CostTypeID = pos_CostTypeID;
        this.CostTypeName = costTypeName;
    }


    private int _pos_CostTypeID;
    public int Pos_CostTypeID
    {
        get { return _pos_CostTypeID; }
        set { _pos_CostTypeID = value; }
    }

    private string _costTypeName;
    public string CostTypeName
    {
        get { return _costTypeName; }
        set { _costTypeName = value; }
    }
}
