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

public class MedicineTimenDateManager
{
	public MedicineTimenDateManager()
	{
	}

    public static List<MedicineTimenDate> GetAllMedicineTimenDates()
    {
        List<MedicineTimenDate> medicineTimenDates = new List<MedicineTimenDate>();
        SqlMedicineTimenDateProvider sqlMedicineTimenDateProvider = new SqlMedicineTimenDateProvider();
        medicineTimenDates = sqlMedicineTimenDateProvider.GetAllMedicineTimenDates();
        return medicineTimenDates;
    }

    public static DataSet GetAllMedicineTimenDatesDistinctDateByResidentID(int ResidentID)
    {
        SqlMedicineTimenDateProvider sqlMedicineTimenDateProvider = new SqlMedicineTimenDateProvider();
        return sqlMedicineTimenDateProvider.GetAllMedicineTimenDatesDistinctDateByResidentID(ResidentID);
    }

    public static DataSet GetMedicationTimenDateByResidentID(int ResidentID)
    {
        SqlMedicineTimenDateProvider sqlMedicineTimenDateProvider = new SqlMedicineTimenDateProvider();
        return sqlMedicineTimenDateProvider.GetMedicationTimenDateByResidentID(ResidentID);
    }


    public static DataSet GetMedicationTimenDateByResidentIDWithDateRange(int ResidentID, DateTime startdate, DateTime endDate)
    {
        SqlMedicineTimenDateProvider sqlMedicineTimenDateProvider = new SqlMedicineTimenDateProvider();
        return sqlMedicineTimenDateProvider.GetMedicationTimenDateByResidentIDWithDateRange(ResidentID, startdate, endDate);
    }


    public static List<MedicineTimenDate> GetAllMedicineTimenDatesDistinctDateByResidentIDnDate(int ResidentID,string SearchString)
    {
        List<MedicineTimenDate> medicineTimenDates = new List<MedicineTimenDate>();
        SqlMedicineTimenDateProvider sqlMedicineTimenDateProvider = new SqlMedicineTimenDateProvider();
        medicineTimenDates = sqlMedicineTimenDateProvider.GetAllMedicineTimenDatesDistinctDateByResidentIDnDate(ResidentID,SearchString);
        return medicineTimenDates;
    }

    public static List<MedicineTimenDate> GetAllMedicineTimenDatesByResidentID(int ResidentID)
    {
        List<MedicineTimenDate> medicineTimenDates = new List<MedicineTimenDate>();
        SqlMedicineTimenDateProvider sqlMedicineTimenDateProvider = new SqlMedicineTimenDateProvider();
        medicineTimenDates = sqlMedicineTimenDateProvider.GetAllMedicineTimenDatesByResidentID(ResidentID);
        return medicineTimenDates;
    }


    public static MedicineTimenDate GetMedicineTimenDateByID(int id)
    {
        MedicineTimenDate medicineTimenDate = new MedicineTimenDate();
        SqlMedicineTimenDateProvider sqlMedicineTimenDateProvider = new SqlMedicineTimenDateProvider();
        medicineTimenDate = sqlMedicineTimenDateProvider.GetMedicineTimenDateByID(id);
        return medicineTimenDate;
    }


    public static int InsertMedicineTimenDate(MedicineTimenDate medicineTimenDate)
    {
        SqlMedicineTimenDateProvider sqlMedicineTimenDateProvider = new SqlMedicineTimenDateProvider();
        return sqlMedicineTimenDateProvider.InsertMedicineTimenDate(medicineTimenDate);
    }


    public static bool UpdateMedicineTimenDate(MedicineTimenDate medicineTimenDate)
    {
        SqlMedicineTimenDateProvider sqlMedicineTimenDateProvider = new SqlMedicineTimenDateProvider();
        return sqlMedicineTimenDateProvider.UpdateMedicineTimenDate(medicineTimenDate);
    }

    public static bool DeleteMedicineTimenDate(int medicineTimenDateID)
    {
        SqlMedicineTimenDateProvider sqlMedicineTimenDateProvider = new SqlMedicineTimenDateProvider();
        return sqlMedicineTimenDateProvider.DeleteMedicineTimenDate(medicineTimenDateID);
    }

    public static bool DeleteMedicineTimenDateByResidentID(int residentID,string DateRange)
    {
        SqlMedicineTimenDateProvider sqlMedicineTimenDateProvider = new SqlMedicineTimenDateProvider();
        return sqlMedicineTimenDateProvider.DeleteMedicineTimenDateByResidentID(residentID, DateRange);
    }
}
