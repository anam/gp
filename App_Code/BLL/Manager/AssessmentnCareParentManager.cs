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

public class AssessmentnCareParentManager
{
	public AssessmentnCareParentManager()
	{
	}

    public static List<AssessmentnCareParent> GetAllAssessmentnCareParents()
    {
        List<AssessmentnCareParent> assessmentnCareParents = new List<AssessmentnCareParent>();
        SqlAssessmentnCareParentProvider sqlAssessmentnCareParentProvider = new SqlAssessmentnCareParentProvider();
        assessmentnCareParents = sqlAssessmentnCareParentProvider.GetAllAssessmentnCareParents();
        return assessmentnCareParents;
    }

    public static List<AssessmentnCareParent> GetAllAssessmentnCareParentsByAssessmentnCareDateIDID(int AssessmentnCareDateIDID)
    {
        List<AssessmentnCareParent> assessmentnCareParents = new List<AssessmentnCareParent>();
        SqlAssessmentnCareParentProvider sqlAssessmentnCareParentProvider = new SqlAssessmentnCareParentProvider();
        assessmentnCareParents = sqlAssessmentnCareParentProvider.GetAllAssessmentnCareParentsByAssessmentnCareDateIDID(AssessmentnCareDateIDID);
        return assessmentnCareParents;
    }

    public static AssessmentnCareParent GetAssessmentnCareParentByID(int id)
    {
        AssessmentnCareParent assessmentnCareParent = new AssessmentnCareParent();
        SqlAssessmentnCareParentProvider sqlAssessmentnCareParentProvider = new SqlAssessmentnCareParentProvider();
        assessmentnCareParent = sqlAssessmentnCareParentProvider.GetAssessmentnCareParentByID(id);
        return assessmentnCareParent;
    }


    public static int InsertAssessmentnCareParent(AssessmentnCareParent assessmentnCareParent)
    {
        SqlAssessmentnCareParentProvider sqlAssessmentnCareParentProvider = new SqlAssessmentnCareParentProvider();
        return sqlAssessmentnCareParentProvider.InsertAssessmentnCareParent(assessmentnCareParent);
    }


    public static bool UpdateAssessmentnCareParent(AssessmentnCareParent assessmentnCareParent)
    {
        SqlAssessmentnCareParentProvider sqlAssessmentnCareParentProvider = new SqlAssessmentnCareParentProvider();
        return sqlAssessmentnCareParentProvider.UpdateAssessmentnCareParent(assessmentnCareParent);
    }

    public static bool DeleteAssessmentnCareParent(int assessmentnCareParentID)
    {
        SqlAssessmentnCareParentProvider sqlAssessmentnCareParentProvider = new SqlAssessmentnCareParentProvider();
        return sqlAssessmentnCareParentProvider.DeleteAssessmentnCareParent(assessmentnCareParentID);
    }
}
