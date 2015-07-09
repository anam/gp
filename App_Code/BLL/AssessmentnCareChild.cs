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

public class AssessmentnCareChild
{
    public AssessmentnCareChild()
    {
    }

    public AssessmentnCareChild
        (
        int assessmentnCareChildID, 
        int assessmentnCareParentID, 
        string assessmentnCareChildName
        )
    {
        this.AssessmentnCareChildID = assessmentnCareChildID;
        this.AssessmentnCareParentID = assessmentnCareParentID;
        this.AssessmentnCareChildName = assessmentnCareChildName;
    }


    private int _assessmentnCareChildID;
    public int AssessmentnCareChildID
    {
        get { return _assessmentnCareChildID; }
        set { _assessmentnCareChildID = value; }
    }

    private int _assessmentnCareParentID;
    public int AssessmentnCareParentID
    {
        get { return _assessmentnCareParentID; }
        set { _assessmentnCareParentID = value; }
    }

    public string AssessmentnCareParentName
    { get; set; }

    private string _assessmentnCareChildName;
    public string AssessmentnCareChildName
    {
        get { return _assessmentnCareChildName; }
        set { _assessmentnCareChildName = value; }
    }
}
