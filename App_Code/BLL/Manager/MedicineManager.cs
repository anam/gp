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

public class MedicineManager
{
	public MedicineManager()
	{
	}

    public static List<Medicine> GetAllMedicines()
    {
        List<Medicine> medicines = new List<Medicine>();
        SqlMedicineProvider sqlMedicineProvider = new SqlMedicineProvider();
        medicines = sqlMedicineProvider.GetAllMedicines();
        return medicines;
    }


    public static Medicine GetMedicineByID(int id)
    {
        Medicine medicine = new Medicine();
        SqlMedicineProvider sqlMedicineProvider = new SqlMedicineProvider();
        medicine = sqlMedicineProvider.GetMedicineByID(id);
        return medicine;
    }


    public static int InsertMedicine(Medicine medicine)
    {
        SqlMedicineProvider sqlMedicineProvider = new SqlMedicineProvider();
        return sqlMedicineProvider.InsertMedicine(medicine);
    }


    public static bool UpdateMedicine(Medicine medicine)
    {
        SqlMedicineProvider sqlMedicineProvider = new SqlMedicineProvider();
        return sqlMedicineProvider.UpdateMedicine(medicine);
    }

    public static bool DeleteMedicine(int medicineID)
    {
        SqlMedicineProvider sqlMedicineProvider = new SqlMedicineProvider();
        return sqlMedicineProvider.DeleteMedicine(medicineID);
    }
}
