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

public class AssessmentnCareChildnDateManager
{
	public AssessmentnCareChildnDateManager()
	{
	}

    public static List<AssessmentnCareChildnDate> GetAllAssessmentnCareChildnDates()
    {
        List<AssessmentnCareChildnDate> assessmentnCareChildnDates = new List<AssessmentnCareChildnDate>();
        SqlAssessmentnCareChildnDateProvider sqlAssessmentnCareChildnDateProvider = new SqlAssessmentnCareChildnDateProvider();
        assessmentnCareChildnDates = sqlAssessmentnCareChildnDateProvider.GetAllAssessmentnCareChildnDates();
        return assessmentnCareChildnDates;
    }

    public static List<AssessmentnCareChildnDate> GetAllAssessmentnCareChildnDatesByAssessmentnCareDateID(int AssessmentnCareDateID)
    {
        List<AssessmentnCareChildnDate> assessmentnCareChildnDates = new List<AssessmentnCareChildnDate>();
        SqlAssessmentnCareChildnDateProvider sqlAssessmentnCareChildnDateProvider = new SqlAssessmentnCareChildnDateProvider();
        assessmentnCareChildnDates = sqlAssessmentnCareChildnDateProvider.GetAllAssessmentnCareChildnDatesByAssessmentnCareDateID(AssessmentnCareDateID);
        return assessmentnCareChildnDates;
    }


    public static AssessmentnCareChildnDate GetAssessmentnCareChildnDateByID(int id)
    {
        AssessmentnCareChildnDate assessmentnCareChildnDate = new AssessmentnCareChildnDate();
        SqlAssessmentnCareChildnDateProvider sqlAssessmentnCareChildnDateProvider = new SqlAssessmentnCareChildnDateProvider();
        assessmentnCareChildnDate = sqlAssessmentnCareChildnDateProvider.GetAssessmentnCareChildnDateByID(id);
        return assessmentnCareChildnDate;
    }


    public static int InsertAssessmentnCareChildnDate(AssessmentnCareChildnDate assessmentnCareChildnDate)
    {
        SqlAssessmentnCareChildnDateProvider sqlAssessmentnCareChildnDateProvider = new SqlAssessmentnCareChildnDateProvider();
        return sqlAssessmentnCareChildnDateProvider.InsertAssessmentnCareChildnDate(assessmentnCareChildnDate);
    }


    public static bool UpdateAssessmentnCareChildnDate(AssessmentnCareChildnDate assessmentnCareChildnDate)
    {
        SqlAssessmentnCareChildnDateProvider sqlAssessmentnCareChildnDateProvider = new SqlAssessmentnCareChildnDateProvider();
        return sqlAssessmentnCareChildnDateProvider.UpdateAssessmentnCareChildnDate(assessmentnCareChildnDate);
    }

    public static bool DeleteAssessmentnCareChildnDate(int assessmentnCareChildnDateID)
    {
        SqlAssessmentnCareChildnDateProvider sqlAssessmentnCareChildnDateProvider = new SqlAssessmentnCareChildnDateProvider();
        return sqlAssessmentnCareChildnDateProvider.DeleteAssessmentnCareChildnDate(assessmentnCareChildnDateID);
    }
}
