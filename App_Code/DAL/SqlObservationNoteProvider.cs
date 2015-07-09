using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public class SqlObservationNoteProvider:DataAccessObject
{
	public SqlObservationNoteProvider()
    {
    }


    public bool DeleteObservationNote(int observationNoteID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteObservationNote", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ObservationNoteID", SqlDbType.Int).Value = observationNoteID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<ObservationNote> GetAllObservationNotes()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllObservationNotes", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetObservationNotesFromReader(reader);
        }
    }

    public List<ObservationNote> GetAllObservationNotesByResidentID(int ResidentID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllObservationNotesByResidentID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = ResidentID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetObservationNotesFromReader(reader);
        }
    }


    public List<ObservationNote> GetAllObservationNotesByResidentIDWithType(int ResidentID,int typeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllObservationNotesByResidentIDWithType", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = ResidentID;
            command.Parameters.Add("@TypeID", SqlDbType.NVarChar).Value = typeID.ToString();
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetObservationNotesFromReader(reader);
        }
    }


    public List<ObservationNote> GetObservationNotesFromReader(IDataReader reader)
    {
        List<ObservationNote> observationNotes = new List<ObservationNote>();

        while (reader.Read())
        {
            observationNotes.Add(GetObservationNoteFromReader(reader));
        }
        return observationNotes;
    }

    public ObservationNote GetObservationNoteFromReader(IDataReader reader)
    {
        try
        {
            ObservationNote observationNote = new ObservationNote
                (
                    (int)reader["ObservationNoteID"],
                    reader["Note"].ToString(),
                    (int)reader["ResidentID"],
                    (int)reader["AddeBy"],
                    (DateTime)reader["AddedDate"],
                    reader["ExtraField1"].ToString(),
                    reader["ExtraField2"].ToString(),
                    reader["ExtraField3"].ToString(),
                    reader["ExtraField4"].ToString(),
                    reader["ExtraField5"].ToString()
                );
             return observationNote;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public ObservationNote GetObservationNoteByID(int observationNoteID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetObservationNoteByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ObservationNoteID", SqlDbType.Int).Value = observationNoteID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetObservationNoteFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertObservationNote(ObservationNote observationNote)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertObservationNote", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ObservationNoteID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Note", SqlDbType.NText).Value = observationNote.Note;
            cmd.Parameters.Add("@ResidentID", SqlDbType.Int).Value = observationNote.ResidentID;
            cmd.Parameters.Add("@AddeBy", SqlDbType.Int).Value = observationNote.AddeBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = observationNote.AddedDate;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = observationNote.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = observationNote.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = observationNote.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = observationNote.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = observationNote.ExtraField5;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@ObservationNoteID"].Value;
        }
    }

    public bool UpdateObservationNote(ObservationNote observationNote)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateObservationNote", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ObservationNoteID", SqlDbType.Int).Value = observationNote.ObservationNoteID;
            cmd.Parameters.Add("@Note", SqlDbType.NText).Value = observationNote.Note;
            cmd.Parameters.Add("@ResidentID", SqlDbType.Int).Value = observationNote.ResidentID;
            cmd.Parameters.Add("@AddeBy", SqlDbType.Int).Value = observationNote.AddeBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = observationNote.AddedDate;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = observationNote.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = observationNote.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = observationNote.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = observationNote.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = observationNote.ExtraField5;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
