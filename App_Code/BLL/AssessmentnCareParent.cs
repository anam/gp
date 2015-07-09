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

public class AssessmentnCareParent
{
    public AssessmentnCareParent()
    {
    }

    public AssessmentnCareParent
        (
        int assessmentnCareParentID, 
        string assessmentnCareParentName
        )
    {
        this.AssessmentnCareParentID = assessmentnCareParentID;
        this.AssessmentnCareParentName = assessmentnCareParentName;
    }


    private int _assessmentnCareParentID;
    public int AssessmentnCareParentID
    {
        get { return _assessmentnCareParentID; }
        set { _assessmentnCareParentID = value; }
    }

    private string _assessmentnCareParentName;
    public string AssessmentnCareParentName
    {
        get { return _assessmentnCareParentName; }
        set { _assessmentnCareParentName = value; }
    }
}
