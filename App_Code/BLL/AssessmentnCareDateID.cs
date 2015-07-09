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

public class AssessmentnCareDate
{
    public AssessmentnCareDate()
    {
    }

    public AssessmentnCareDate
        (
        int assessmentnCareDateIDID, 
        DateTime assessmentnCareDate, 
        int residentID, 
        int addedBy, 
        DateTime addedDate, 
        int updatedBy, 
        DateTime updatedDate
        )
    {
        this.AssessmentnCareDateIDID = assessmentnCareDateIDID;
        this.AssessmentnCareDateName = assessmentnCareDate;
        this.ResidentID = residentID;
        this.AddedBy = addedBy;
        this.AddedDate = addedDate;
        this.UpdatedBy = updatedBy;
        this.UpdatedDate = updatedDate;
    }


    private int _assessmentnCareDateIDID;
    public int AssessmentnCareDateIDID
    {
        get { return _assessmentnCareDateIDID; }
        set { _assessmentnCareDateIDID = value; }
    }

    public DateTime AssessmentnCareDateName
    {
        get;
        set;
    }

    private int _residentID;
    public int ResidentID
    {
        get { return _residentID; }
        set { _residentID = value; }
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
