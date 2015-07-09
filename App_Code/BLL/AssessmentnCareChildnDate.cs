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

public class AssessmentnCareChildnDate
{
    public AssessmentnCareChildnDate()
    {
    }

    public AssessmentnCareChildnDate
        (
        int assessmentnCareChildnDateID, 
        int assessmentnCareChildID, 
        int assessmentnCareDateID
        )
    {
        this.AssessmentnCareChildnDateID = assessmentnCareChildnDateID;
        this.AssessmentnCareChildID = assessmentnCareChildID;
        this.AssessmentnCareDateID = assessmentnCareDateID;
    }


    private int _assessmentnCareChildnDateID;
    public int AssessmentnCareChildnDateID
    {
        get { return _assessmentnCareChildnDateID; }
        set { _assessmentnCareChildnDateID = value; }
    }

    private int _assessmentnCareChildID;
    public int AssessmentnCareChildID
    {
        get { return _assessmentnCareChildID; }
        set { _assessmentnCareChildID = value; }
    }

    private int _assessmentnCareDateID;
    public int AssessmentnCareDateID
    {
        get { return _assessmentnCareDateID; }
        set { _assessmentnCareDateID = value; }
    }
}
