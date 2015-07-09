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

public class ResidentManager
{
	public ResidentManager()
	{
	}

    public static List<Resident> GetAllResidents()
    {
        List<Resident> residents = new List<Resident>();
        SqlResidentProvider sqlResidentProvider = new SqlResidentProvider();
        residents = sqlResidentProvider.GetAllResidents();
        return residents;
    }
    public static List<Resident> SearchResidents(string SearchString)
    {
        List<Resident> residents = new List<Resident>();
        SqlResidentProvider sqlResidentProvider = new SqlResidentProvider();
        residents = sqlResidentProvider.SearchResidents(SearchString);
        return residents;
    }


    public static Resident GetResidentByID(int id)
    {
        Resident resident = new Resident();
        SqlResidentProvider sqlResidentProvider = new SqlResidentProvider();
        resident = sqlResidentProvider.GetResidentByID(id);
        return resident;
    }


    public static int InsertResident(Resident resident)
    {
        SqlResidentProvider sqlResidentProvider = new SqlResidentProvider();
        return sqlResidentProvider.InsertResident(resident);
    }


    public static bool UpdateResident(Resident resident)
    {
        SqlResidentProvider sqlResidentProvider = new SqlResidentProvider();
        return sqlResidentProvider.UpdateResident(resident);
    }

    public static bool DeleteResident(int residentID)
    {
        SqlResidentProvider sqlResidentProvider = new SqlResidentProvider();
        return sqlResidentProvider.DeleteResident(residentID);
    }
}
