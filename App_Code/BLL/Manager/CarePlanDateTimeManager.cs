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

public class CarePlanDateTimeManager
{
	public CarePlanDateTimeManager()
	{
	}

    public static List<CarePlanDateTime> GetAllCarePlanDateTimes()
    {
        List<CarePlanDateTime> carePlanDateTimes = new List<CarePlanDateTime>();
        SqlCarePlanDateTimeProvider sqlCarePlanDateTimeProvider = new SqlCarePlanDateTimeProvider();
        carePlanDateTimes = sqlCarePlanDateTimeProvider.GetAllCarePlanDateTimes();
        return carePlanDateTimes;
    }

    public static List<CarePlanDateTime> GetAllCarePlanDateTimesByResidentID(int residentID)
    {
        List<CarePlanDateTime> carePlanDateTimes = new List<CarePlanDateTime>();
        SqlCarePlanDateTimeProvider sqlCarePlanDateTimeProvider = new SqlCarePlanDateTimeProvider();
        carePlanDateTimes = sqlCarePlanDateTimeProvider.GetAllCarePlanDateTimesByResidentID(residentID);
        return carePlanDateTimes;
    }

    public static CarePlanDateTime GetCarePlanDateTimeByID(int id)
    {
        CarePlanDateTime carePlanDateTime = new CarePlanDateTime();
        SqlCarePlanDateTimeProvider sqlCarePlanDateTimeProvider = new SqlCarePlanDateTimeProvider();
        carePlanDateTime = sqlCarePlanDateTimeProvider.GetCarePlanDateTimeByID(id);
        return carePlanDateTime;
    }


    public static int InsertCarePlanDateTime(CarePlanDateTime carePlanDateTime)
    {
        SqlCarePlanDateTimeProvider sqlCarePlanDateTimeProvider = new SqlCarePlanDateTimeProvider();
        return sqlCarePlanDateTimeProvider.InsertCarePlanDateTime(carePlanDateTime);
    }


    public static bool UpdateCarePlanDateTime(CarePlanDateTime carePlanDateTime)
    {
        SqlCarePlanDateTimeProvider sqlCarePlanDateTimeProvider = new SqlCarePlanDateTimeProvider();
        return sqlCarePlanDateTimeProvider.UpdateCarePlanDateTime(carePlanDateTime);
    }

    public static bool DeleteCarePlanDateTime(int carePlanDateTimeID)
    {
        SqlCarePlanDateTimeProvider sqlCarePlanDateTimeProvider = new SqlCarePlanDateTimeProvider();
        return sqlCarePlanDateTimeProvider.DeleteCarePlanDateTime(carePlanDateTimeID);
    }
}
