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

public class AssessmentnCareCommentnDate
{
    public AssessmentnCareCommentnDate()
    {
    }

    public AssessmentnCareCommentnDate
        (
        int assessmentnCareCommentnDateID, 
        int assessmentnCareDateID, 
        int assessmentnCareParentID, 
        string comment
        )
    {
        this.AssessmentnCareCommentnDateID = assessmentnCareCommentnDateID;
        this.AssessmentnCareDateID = assessmentnCareDateID;
        this.AssessmentnCareParentID = assessmentnCareParentID;
        this.Comment = comment;
    }


    private int _assessmentnCareCommentnDateID;
    public int AssessmentnCareCommentnDateID
    {
        get { return _assessmentnCareCommentnDateID; }
        set { _assessmentnCareCommentnDateID = value; }
    }

    private int _assessmentnCareDateID;
    public int AssessmentnCareDateID
    {
        get { return _assessmentnCareDateID; }
        set { _assessmentnCareDateID = value; }
    }

    private int _assessmentnCareParentID;
    public int AssessmentnCareParentID
    {
        get { return _assessmentnCareParentID; }
        set { _assessmentnCareParentID = value; }
    }

    private string _comment;
    public string Comment
    {
        get { return _comment; }
        set { _comment = value; }
    }
}
