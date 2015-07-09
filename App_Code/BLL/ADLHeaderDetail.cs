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

public class ADLHeaderDetail
{
    public ADLHeaderDetail()
    {
    }

    public ADLHeaderDetail
        (
        int aDLHeaderDetailID, 
        int aDLDetailID, 
        int aDLHeaderID, 
        int residentID, 
        string extraField1, 
        string extraField2, 
        string extraField3, 
        string extraField4, 
        string extraField5
        )
    {
        this.ADLHeaderDetailID = aDLHeaderDetailID;
        this.ADLDetailID = aDLDetailID;
        this.ADLHeaderID = aDLHeaderID;
        this.ResidentID = residentID;
        this.ExtraField1 = extraField1;
        this.ExtraField2 = extraField2;
        this.ExtraField3 = extraField3;
        this.ExtraField4 = extraField4;
        this.ExtraField5 = extraField5;
    }


    private int _aDLHeaderDetailID;
    public int ADLHeaderDetailID
    {
        get { return _aDLHeaderDetailID; }
        set { _aDLHeaderDetailID = value; }
    }

    private int _aDLDetailID;
    public int ADLDetailID
    {
        get { return _aDLDetailID; }
        set { _aDLDetailID = value; }
    }

    private int _aDLHeaderID;
    public int ADLHeaderID
    {
        get { return _aDLHeaderID; }
        set { _aDLHeaderID = value; }
    }

    private int _residentID;
    public int ResidentID
    {
        get { return _residentID; }
        set { _residentID = value; }
    }

    private string _extraField1;
    /// <summary>
    /// HeaderName
    /// </summary>
    public string ExtraField1
    {
        get { return _extraField1; }
        set { _extraField1 = value; }
    }

    private string _extraField2;
    /// <summary>
    /// DetailName
    /// </summary>
    public string ExtraField2
    {
        get { return _extraField2; }
        set { _extraField2 = value; }
    }

    private string _extraField3;
    public string ExtraField3
    {
        get { return _extraField3; }
        set { _extraField3 = value; }
    }

    private string _extraField4;
    public string ExtraField4
    {
        get { return _extraField4; }
        set { _extraField4 = value; }
    }

    private string _extraField5;
    public string ExtraField5
    {
        get { return _extraField5; }
        set { _extraField5 = value; }
    }
}
