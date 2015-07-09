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

public class AssessmentnCareCommentnDateManager
{
	public AssessmentnCareCommentnDateManager()
	{
	}

    public static List<AssessmentnCareCommentnDate> GetAllAssessmentnCareCommentnDates()
    {
        List<AssessmentnCareCommentnDate> assessmentnCareCommentnDates = new List<AssessmentnCareCommentnDate>();
        SqlAssessmentnCareCommentnDateProvider sqlAssessmentnCareCommentnDateProvider = new SqlAssessmentnCareCommentnDateProvider();
        assessmentnCareCommentnDates = sqlAssessmentnCareCommentnDateProvider.GetAllAssessmentnCareCommentnDates();
        return assessmentnCareCommentnDates;
    }


    public static List<AssessmentnCareCommentnDate> GetAllAssessmentnCareCommentnDatesByAssessmentnCareDateID(int AssessmentnCareDateID)
    {
        List<AssessmentnCareCommentnDate> assessmentnCareCommentnDates = new List<AssessmentnCareCommentnDate>();
        SqlAssessmentnCareCommentnDateProvider sqlAssessmentnCareCommentnDateProvider = new SqlAssessmentnCareCommentnDateProvider();
        assessmentnCareCommentnDates = sqlAssessmentnCareCommentnDateProvider.GetAllAssessmentnCareCommentnDatesByAssessmentnCareDateID(AssessmentnCareDateID);
        return assessmentnCareCommentnDates;
    }

    public static AssessmentnCareCommentnDate GetAssessmentnCareCommentnDateByID(int id)
    {
        AssessmentnCareCommentnDate assessmentnCareCommentnDate = new AssessmentnCareCommentnDate();
        SqlAssessmentnCareCommentnDateProvider sqlAssessmentnCareCommentnDateProvider = new SqlAssessmentnCareCommentnDateProvider();
        assessmentnCareCommentnDate = sqlAssessmentnCareCommentnDateProvider.GetAssessmentnCareCommentnDateByID(id);
        return assessmentnCareCommentnDate;
    }


    public static int InsertAssessmentnCareCommentnDate(AssessmentnCareCommentnDate assessmentnCareCommentnDate)
    {
        SqlAssessmentnCareCommentnDateProvider sqlAssessmentnCareCommentnDateProvider = new SqlAssessmentnCareCommentnDateProvider();
        return sqlAssessmentnCareCommentnDateProvider.InsertAssessmentnCareCommentnDate(assessmentnCareCommentnDate);
    }


    public static bool UpdateAssessmentnCareCommentnDate(AssessmentnCareCommentnDate assessmentnCareCommentnDate)
    {
        SqlAssessmentnCareCommentnDateProvider sqlAssessmentnCareCommentnDateProvider = new SqlAssessmentnCareCommentnDateProvider();
        return sqlAssessmentnCareCommentnDateProvider.UpdateAssessmentnCareCommentnDate(assessmentnCareCommentnDate);
    }

    public static bool DeleteAssessmentnCareCommentnDate(int assessmentnCareCommentnDateID)
    {
        SqlAssessmentnCareCommentnDateProvider sqlAssessmentnCareCommentnDateProvider = new SqlAssessmentnCareCommentnDateProvider();
        return sqlAssessmentnCareCommentnDateProvider.DeleteAssessmentnCareCommentnDate(assessmentnCareCommentnDateID);
    }
}
