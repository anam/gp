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

public class RowStatus
{
    public RowStatus()
    {
    }

    public RowStatus
        (
        int rowStatusID, 
        string rowStatusName
        )
    {
        this.RowStatusID = rowStatusID;
        this.RowStatusName = rowStatusName;
    }


    private int _rowStatusID;
    public int RowStatusID
    {
        get { return _rowStatusID; }
        set { _rowStatusID = value; }
    }

    private string _rowStatusName;
    public string RowStatusName
    {
        get { return _rowStatusName; }
        set { _rowStatusName = value; }
    }
}
