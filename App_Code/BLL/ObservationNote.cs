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

public class ObservationNote
{
    public ObservationNote()
    {
    }

    public ObservationNote
        (
        int observationNoteID, 
        string note, 
        int residentID, 
        int addeBy, 
        DateTime addedDate, 
        string extraField1, 
        string extraField2, 
        string extraField3, 
        string extraField4, 
        string extraField5
        )
    {
        this.ObservationNoteID = observationNoteID;
        this.Note = note;
        this.ResidentID = residentID;
        this.AddeBy = addeBy;
        this.AddedDate = addedDate;
        this.ExtraField1 = extraField1;
        this.ExtraField2 = extraField2;
        this.ExtraField3 = extraField3;
        this.ExtraField4 = extraField4;
        this.ExtraField5 = extraField5;
    }


    private int _observationNoteID;
    public int ObservationNoteID
    {
        get { return _observationNoteID; }
        set { _observationNoteID = value; }
    }

    private string _note;
    public string Note
    {
        get { return _note; }
        set { _note = value; }
    }

    private int _residentID;
    public int ResidentID
    {
        get { return _residentID; }
        set { _residentID = value; }
    }

    private int _addeBy;
    public int AddeBy
    {
        get { return _addeBy; }
        set { _addeBy = value; }
    }

    private DateTime _addedDate;
    public DateTime AddedDate
    {
        get { return _addedDate; }
        set { _addedDate = value; }
    }

    private string _extraField1;
    //Made By
    public string ExtraField1
    {
        get { return _extraField1; }
        set { _extraField1 = value; }
    }

    private string _extraField2;
    /// <summary>
    /// Observation Type
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
