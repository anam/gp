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

public class ObservationTypeManager
{
	public ObservationTypeManager()
	{
	}

    public static List<ObservationType> GetAllObservationTypes()
    {
        List<ObservationType> observationTypes = new List<ObservationType>();
        SqlObservationTypeProvider sqlObservationTypeProvider = new SqlObservationTypeProvider();
        observationTypes = sqlObservationTypeProvider.GetAllObservationTypes();
        return observationTypes;
    }


    public static ObservationType GetObservationTypeByID(int id)
    {
        ObservationType observationType = new ObservationType();
        SqlObservationTypeProvider sqlObservationTypeProvider = new SqlObservationTypeProvider();
        observationType = sqlObservationTypeProvider.GetObservationTypeByID(id);
        return observationType;
    }


    public static int InsertObservationType(ObservationType observationType)
    {
        SqlObservationTypeProvider sqlObservationTypeProvider = new SqlObservationTypeProvider();
        return sqlObservationTypeProvider.InsertObservationType(observationType);
    }


    public static bool UpdateObservationType(ObservationType observationType)
    {
        SqlObservationTypeProvider sqlObservationTypeProvider = new SqlObservationTypeProvider();
        return sqlObservationTypeProvider.UpdateObservationType(observationType);
    }

    public static bool DeleteObservationType(int observationTypeID)
    {
        SqlObservationTypeProvider sqlObservationTypeProvider = new SqlObservationTypeProvider();
        return sqlObservationTypeProvider.DeleteObservationType(observationTypeID);
    }
}
