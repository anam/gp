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

public class AssessmentnCareDateIDManager
{
	public AssessmentnCareDateIDManager()
	{
	}

    public static List<AssessmentnCareDate> GetAllAssessmentnCareDateIDs()
    {
        List<AssessmentnCareDate> assessmentnCareDateIDs = new List<AssessmentnCareDate>();
        SqlAssessmentnCareDateIDProvider sqlAssessmentnCareDateIDProvider = new SqlAssessmentnCareDateIDProvider();
        assessmentnCareDateIDs = sqlAssessmentnCareDateIDProvider.GetAllAssessmentnCareDateIDs();
        return assessmentnCareDateIDs;
    }
    public static List<AssessmentnCareDate> GetAllAssessmentnCareDates()
    {
        List<AssessmentnCareDate> assessmentnCareDateIDs = new List<AssessmentnCareDate>();
        SqlAssessmentnCareDateIDProvider sqlAssessmentnCareDateIDProvider = new SqlAssessmentnCareDateIDProvider();
        assessmentnCareDateIDs = sqlAssessmentnCareDateIDProvider.GetAllAssessmentnCareDateIDs();
        return assessmentnCareDateIDs;
    }

    public static List<AssessmentnCareDate> GetAllAssessmentnCareDatesByResidentID(int residentID)
    {
        List<AssessmentnCareDate> assessmentnCareDateIDs = new List<AssessmentnCareDate>();
        SqlAssessmentnCareDateIDProvider sqlAssessmentnCareDateIDProvider = new SqlAssessmentnCareDateIDProvider();
        assessmentnCareDateIDs = sqlAssessmentnCareDateIDProvider.GetAllAssessmentnCareDateIDsByResidentID(residentID);
        return assessmentnCareDateIDs;
    }

    public static AssessmentnCareDate GetAssessmentnCareDateIDByID(int id)
    {
        AssessmentnCareDate assessmentnCareDateID = new AssessmentnCareDate();
        SqlAssessmentnCareDateIDProvider sqlAssessmentnCareDateIDProvider = new SqlAssessmentnCareDateIDProvider();
        assessmentnCareDateID = sqlAssessmentnCareDateIDProvider.GetAssessmentnCareDateIDByID(id);
        return assessmentnCareDateID;
    }


    public static int InsertAssessmentnCareDateID(AssessmentnCareDate assessmentnCareDateID)
    {
        SqlAssessmentnCareDateIDProvider sqlAssessmentnCareDateIDProvider = new SqlAssessmentnCareDateIDProvider();
        return sqlAssessmentnCareDateIDProvider.InsertAssessmentnCareDateID(assessmentnCareDateID);
    }


    public static bool UpdateAssessmentnCareDateID(AssessmentnCareDate assessmentnCareDateID)
    {
        SqlAssessmentnCareDateIDProvider sqlAssessmentnCareDateIDProvider = new SqlAssessmentnCareDateIDProvider();
        return sqlAssessmentnCareDateIDProvider.UpdateAssessmentnCareDateID(assessmentnCareDateID);
    }

    public static bool ProcessAssessmentnCareDateID(AssessmentnCareDate assessmentnCareDateID, string chiledIds, string commentsByParentIds)
    {
        SqlAssessmentnCareDateIDProvider sqlAssessmentnCareDateIDProvider = new SqlAssessmentnCareDateIDProvider();
        return sqlAssessmentnCareDateIDProvider.ProcessAssessmentnCareDateID(assessmentnCareDateID, chiledIds, commentsByParentIds);
    }

    public static bool DeleteAssessmentnCareDateID(int assessmentnCareDateIDID)
    {
        SqlAssessmentnCareDateIDProvider sqlAssessmentnCareDateIDProvider = new SqlAssessmentnCareDateIDProvider();
        return sqlAssessmentnCareDateIDProvider.DeleteAssessmentnCareDateID(assessmentnCareDateIDID);
    }
}
