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

public class ObservationType
{
    public ObservationType()
    {
    }

    public ObservationType
        (
        int observationTypeID, 
        string observationTypeName
        )
    {
        this.ObservationTypeID = observationTypeID;
        this.ObservationTypeName = observationTypeName;
    }


    private int _observationTypeID;
    public int ObservationTypeID
    {
        get { return _observationTypeID; }
        set { _observationTypeID = value; }
    }

    private string _observationTypeName;
    public string ObservationTypeName
    {
        get { return _observationTypeName; }
        set { _observationTypeName = value; }
    }
}
