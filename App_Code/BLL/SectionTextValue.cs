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

public class SectionTextValue
{
    public SectionTextValue()
    {
    }

    public SectionTextValue
        (
        int sectionTextValueID, 
        int carePlanDateTimeID, 
        string section_2, 
        string section_3, 
        string section_6, 
        string section_7, 
        int addedBy, 
        DateTime addedDate, 
        int updatedBy, 
        DateTime updatedDate
        )
    {
        this.SectionTextValueID = sectionTextValueID;
        this.CarePlanDateTimeID = carePlanDateTimeID;
        this.Section_2 = section_2;
        this.Section_3 = section_3;
        this.Section_6 = section_6;
        this.Section_7 = section_7;
        this.AddedBy = addedBy;
        this.AddedDate = addedDate;
        this.UpdatedBy = updatedBy;
        this.UpdatedDate = updatedDate;
    }


    private int _sectionTextValueID;
    public int SectionTextValueID
    {
        get { return _sectionTextValueID; }
        set { _sectionTextValueID = value; }
    }

    private int _carePlanDateTimeID;
    public int CarePlanDateTimeID
    {
        get { return _carePlanDateTimeID; }
        set { _carePlanDateTimeID = value; }
    }

    private string _section_2;
    public string Section_2
    {
        get { return _section_2; }
        set { _section_2 = value; }
    }

    private string _section_3;
    public string Section_3
    {
        get { return _section_3; }
        set { _section_3 = value; }
    }

    private string _section_6;
    public string Section_6
    {
        get { return _section_6; }
        set { _section_6 = value; }
    }

    private string _section_7;
    public string Section_7
    {
        get { return _section_7; }
        set { _section_7 = value; }
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

    private int _updatedBy;
    public int UpdatedBy
    {
        get { return _updatedBy; }
        set { _updatedBy = value; }
    }

    private DateTime _updatedDate;
    public DateTime UpdatedDate
    {
        get { return _updatedDate; }
        set { _updatedDate = value; }
    }
}
