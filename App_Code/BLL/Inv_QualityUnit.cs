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

public class Inv_QualityUnit
{
    public Inv_QualityUnit()
    {
    }

    public Inv_QualityUnit
        (
        int inv_QualityUnitID, 
        string qualityUnitName, 
        int rowStatusID
        )
    {
        this.Inv_QualityUnitID = inv_QualityUnitID;
        this.QualityUnitName = qualityUnitName;
        this.RowStatusID = rowStatusID;
    }


    private int _inv_QualityUnitID;
    public int Inv_QualityUnitID
    {
        get { return _inv_QualityUnitID; }
        set { _inv_QualityUnitID = value; }
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
