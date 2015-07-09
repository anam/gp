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

public class ObservationNoteManager
{
	public ObservationNoteManager()
	{
	}

    public static List<ObservationNote> GetAllObservationNotes()
    {
        List<ObservationNote> observationNotes = new List<ObservationNote>();
        SqlObservationNoteProvider sqlObservationNoteProvider = new SqlObservationNoteProvider();
        observationNotes = sqlObservationNoteProvider.GetAllObservationNotes();
        return observationNotes;
    }

    public static List<ObservationNote> GetAllObservationNotesByResidentID(int ResidentID)
    {
        List<ObservationNote> observationNotes = new List<ObservationNote>();
        SqlObservationNoteProvider sqlObservationNoteProvider = new SqlObservationNoteProvider();
        observationNotes = sqlObservationNoteProvider.GetAllObservationNotesByResidentID(ResidentID);
        return observationNotes;
    }

    public static List<ObservationNote> GetAllObservationNotesByResidentIDWithType(int ResidentID,int typeID)
    {
        List<ObservationNote> observationNotes = new List<ObservationNote>();
        SqlObservationNoteProvider sqlObservationNoteProvider = new SqlObservationNoteProvider();
        observationNotes = sqlObservationNoteProvider.GetAllObservationNotesByResidentIDWithType(ResidentID,typeID);
        return observationNotes;
    }


    public static ObservationNote GetObservationNoteByID(int id)
    {
        ObservationNote observationNote = new ObservationNote();
        SqlObservationNoteProvider sqlObservationNoteProvider = new SqlObservationNoteProvider();
        observationNote = sqlObservationNoteProvider.GetObservationNoteByID(id);
        return observationNote;
    }


    public static int InsertObservationNote(ObservationNote observationNote)
    {
        SqlObservationNoteProvider sqlObservationNoteProvider = new SqlObservationNoteProvider();
        return sqlObservationNoteProvider.InsertObservationNote(observationNote);
    }


    public static bool UpdateObservationNote(ObservationNote observationNote)
    {
        SqlObservationNoteProvider sqlObservationNoteProvider = new SqlObservationNoteProvider();
        return sqlObservationNoteProvider.UpdateObservationNote(observationNote);
    }

    public static bool DeleteObservationNote(int observationNoteID)
    {
        SqlObservationNoteProvider sqlObservationNoteProvider = new SqlObservationNoteProvider();
        return sqlObservationNoteProvider.DeleteObservationNote(observationNoteID);
    }
}
