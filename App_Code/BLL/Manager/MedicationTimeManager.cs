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

public class MedicationTimeManager
{
	public MedicationTimeManager()
	{
	}

    public static List<MedicationTime> GetAllMedicationTimes()
    {
        List<MedicationTime> medicationTimes = new List<MedicationTime>();
        SqlMedicationTimeProvider sqlMedicationTimeProvider = new SqlMedicationTimeProvider();
        medicationTimes = sqlMedicationTimeProvider.GetAllMedicationTimes();
        return medicationTimes;
    }


    public static DataSet GetAllMedicationTimesByResident(int residentID)
    {
        SqlMedicationTimeProvider sqlMedicationTimeProvider = new SqlMedicationTimeProvider();
        return sqlMedicationTimeProvider.GetAllMedicationTimesByResident(residentID);
    }

    public static DataSet GetAllMedicationTimesByResidentByStatus(int residentID, string status)
    {
        SqlMedicationTimeProvider sqlMedicationTimeProvider = new SqlMedicationTimeProvider();
        return sqlMedicationTimeProvider.GetAllMedicationTimesByResidentByStatus(residentID,status);
    }

    public static DataSet GetAllMedicationTimesByResidentForPrint(int residentID,int printOption)
    {
        SqlMedicationTimeProvider sqlMedicationTimeProvider = new SqlMedicationTimeProvider();
        return sqlMedicationTimeProvider.GetAllMedicationTimesByResidentForPrint(residentID,printOption);
    }

    public static MedicationTime GetMedicationTimeByID(int id)
    {
        MedicationTime medicationTime = new MedicationTime();
        SqlMedicationTimeProvider sqlMedicationTimeProvider = new SqlMedicationTimeProvider();
        medicationTime = sqlMedicationTimeProvider.GetMedicationTimeByID(id);
        return medicationTime;
    }


    public static int InsertMedicationTime(MedicationTime medicationTime)
    {
        SqlMedicationTimeProvider sqlMedicationTimeProvider = new SqlMedicationTimeProvider();
        return sqlMedicationTimeProvider.InsertMedicationTime(medicationTime);
    }


    public static bool UpdateMedicationTime(MedicationTime medicationTime)
    {
        SqlMedicationTimeProvider sqlMedicationTimeProvider = new SqlMedicationTimeProvider();
        return sqlMedicationTimeProvider.UpdateMedicationTime(medicationTime);
    }

    public static bool DeleteMedicationTime(int medicationTimeID)
    {
        SqlMedicationTimeProvider sqlMedicationTimeProvider = new SqlMedicationTimeProvider();
        return sqlMedicationTimeProvider.DeleteMedicationTime(medicationTimeID);
    }
}
