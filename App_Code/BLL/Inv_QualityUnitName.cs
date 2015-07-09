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

public class Inv_QualityUnitName
{
    public Inv_QualityUnitName()
    {
    }

    public Inv_QualityUnitName
        (
        int inv_QualityUnitNameID, 
        string qualityUnitName, 
        int rowStatusID
        )
    {
        this.Inv_QualityUnitNameID = inv_QualityUnitNameID;
        this.QualityUnitName = qualityUnitName;
        this.RowStatusID = rowStatusID;
    }


    private int _inv_QualityUnitNameID;
    public int Inv_QualityUnitNameID
    {
        get { return _inv_QualityUnitNameID; }
        set { _inv_QualityUnitNameID = value; }
    }

    private string _qualityUnitName;
    public string QualityUnitName
    {
        get { return _qualityUnitName; }
        set { _qualityUnitName = value; }
    }

    private int _rowStatusID;
    public int RowStatusID
    {
        get { return _rowStatusID; }
        set { _rowStatusID = value; }
    }
}
