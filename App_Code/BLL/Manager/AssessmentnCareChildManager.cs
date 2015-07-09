using System;
using System.Collections.Generic;
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

public class AssessmentnCareChildManager
{
	public AssessmentnCareChildManager()
	{
	}

    public static List<AssessmentnCareChild> GetAllAssessmentnCareChilds()
    {
        List<AssessmentnCareChild> assessmentnCareChilds = new List<AssessmentnCareChild>();
        SqlAssessmentnCareChildProvider sqlAssessmentnCareChildProvider = new SqlAssessmentnCareChildProvider();
        assessmentnCareChilds = sqlAssessmentnCareChildProvider.GetAllAssessmentnCareChilds();
        return assessmentnCareChilds;
    }


    public static List<AssessmentnCareChild> GetAllAssessmentnCareChildsByAssessmentnCareDateIDID(int AssessmentnCareDateIDID)
    {
        List<AssessmentnCareChild> assessmentnCareChilds = new List<AssessmentnCareChild>();
        SqlAssessmentnCareChildProvider sqlAssessmentnCareChildProvider = new SqlAssessmentnCareChildProvider();
        assessmentnCareChilds = sqlAssessmentnCareChildProvider.GetAllAssessmentnCareChildsByAssessmentnCareDateIDID(AssessmentnCareDateIDID);
        return assessmentnCareChilds;
    }


    public static AssessmentnCareChild GetAssessmentnCareChildByID(int id)
    {
        AssessmentnCareChild assessmentnCareChild = new AssessmentnCareChild();
        SqlAssessmentnCareChildProvider sqlAssessmentnCareChildProvider = new SqlAssessmentnCareChildProvider();
        assessmentnCareChild = sqlAssessmentnCareChildProvider.GetAssessmentnCareChildByID(id);
        return assessmentnCareChild;
    }


    public static int InsertAssessmentnCareChild(AssessmentnCareChild assessmentnCareChild)
    {
        SqlAssessmentnCareChildProvider sqlAssessmentnCareChildProvider = new SqlAssessmentnCareChildProvider();
        return sqlAssessmentnCareChildProvider.InsertAssessmentnCareChild(assessmentnCareChild);
    }


    public static bool UpdateAssessmentnCareChild(AssessmentnCareChild assessmentnCareChild)
    {
        SqlAssessmentnCareChildProvider sqlAssessmentnCareChildProvider = new SqlAssessmentnCareChildProvider();
        return sqlAssessmentnCareChildProvider.UpdateAssessmentnCareChild(assessmentnCareChild);
    }

    public static bool DeleteAssessmentnCareChild(int assessmentnCareChildID)
    {
        SqlAssessmentnCareChildProvider sqlAssessmentnCareChildProvider = new SqlAssessmentnCareChildProvider();
        return sqlAssessmentnCareChildProvider.DeleteAssessmentnCareChild(assessmentnCareChildID);
    }
}
