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

public class SectionLabelValue
{
    public SectionLabelValue()
    {
    }

    public SectionLabelValue
        (
        int sectionLabelValueID, 
        int sectionLabelID, 
        int addedBy, 
        DateTime addedDate, 
        string value, 
        string extraField1, 
        string extraField2
        )
    {
        this.SectionLabelValueID = sectionLabelValueID;
        this.SectionLabelID = sectionLabelID;
        this.AddedBy = addedBy;
        this.AddedDate = addedDate;
        this.Value = value;
        this.ExtraField1 = extraField1;
        this.ExtraField2 = extraField2;
    }


    private int _sectionLabelValueID;
    public int SectionLabelValueID
    {
        get { return _sectionLabelValueID; }
        set { _sectionLabelValueID = value; }
    }

    private int _sectionLabelID;
    public int SectionLabelID
    {
        get { return _sectionLabelID; }
        set { _sectionLabelID = value; }
    }

    private int _addedBy;
    public int AddedBy
    {
        get { return _addedBy; }
        set { _addedBy = value; }
    }

    private DateTime _addedDate;
    public DateTime AddedDate
    {
        get { return _addedDate; }
        set { _addedDate = value; }
    }

    private string _value;
    public string Value
    {
        get { return _value; }
        set { _value = value; }
    }

    private string _extraField1;
    public string ExtraField1
    {
        get { return _extraField1; }
        set { _extraField1 = value; }
    }

    private string _extraField2;
    public string ExtraField2
    {
        get { return _extraField2; }
        set { _extraField2 = value; }
    }
}
