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

public class ADLHeaderDetailnDateTime
{
    public ADLHeaderDetailnDateTime()
    {
    }

    public ADLHeaderDetailnDateTime
        (
        int aDLHeaderDetailnDateTimeID, 
        int aDLDateTimeID, 
        int aDLHeaderDetailID, 
        string extraField1, 
        string extraField2, 
        string extraField3, 
        string extraField4, 
        string extraField5
        )
    {
        this.ADLHeaderDetailnDateTimeID = aDLHeaderDetailnDateTimeID;
        this.ADLDateTimeID = aDLDateTimeID;
        this.ADLHeaderDetailID = aDLHeaderDetailID;
        this.ExtraField1 = extraField1;
        this.ExtraField2 = extraField2;
        this.ExtraField3 = extraField3;
        this.ExtraField4 = extraField4;
        this.ExtraField5 = extraField5;
    }


    private int _aDLHeaderDetailnDateTimeID;
    public int ADLHeaderDetailnDateTimeID
    {
        get { return _aDLHeaderDetailnDateTimeID; }
        set { _aDLHeaderDetailnDateTimeID = value; }
    }

    private int _aDLDateTimeID;
    public int ADLDateTimeID
    {
        get { return _aDLDateTimeID; }
        set { _aDLDateTimeID = value; }
    }

    private int _aDLHeaderDetailID;
    public int ADLHeaderDetailID
    {
        get { return _aDLHeaderDetailID; }
        set { _aDLHeaderDetailID = value; }
    }

    
    private string _extraField1;
    /// <summary>
    /// Status
    /// </summary>
    public string ExtraField1
    {
        get { return _extraField1; }
        set { _extraField1 = value; }
    }

    private string _extraField2;
    /// <summary>
    /// Saved by Initial
    /// </summary>
    public string ExtraField2
    {
        get { return _extraField2; }
        set { _extraField2 = value; }
    }

    private string _extraField3;
    /// <summary>
    /// Remarks
    /// </summary>
    public string ExtraField3
    {
        get { return _extraField3; }
        set { _extraField3 = value; }
    }

    private string _extraField4;
    /// <summary>
    /// Postedby iD
    /// </summary>
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
