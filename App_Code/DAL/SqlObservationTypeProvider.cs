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

public class SqlObservationTypeProvider:DataAccessObject
{
	public SqlObservationTypeProvider()
    {
    }


    public bool DeleteObservationType(int observationTypeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteObservationType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ObservationTypeID", SqlDbType.Int).Value = observationTypeID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<ObservationType> GetAllObservationTypes()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllObservationTypes", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetObservationTypesFromReader(reader);
        }
    }
    public List<ObservationType> GetObservationTypesFromReader(IDataReader reader)
    {
        List<ObservationType> observationTypes = new List<ObservationType>();

        while (reader.Read())
        {
            observationTypes.Add(GetObservationTypeFromReader(reader));
        }
        return observationTypes;
    }

    public ObservationType GetObservationTypeFromReader(IDataReader reader)
    {
        try
        {
            ObservationType observationType = new ObservationType
                (
                    (int)reader["ObservationTypeID"],
                    reader["ObservationTypeName"].ToString()
                );
             return observationType;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public ObservationType GetObservationTypeByID(int observationTypeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetObservationTypeByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ObservationTypeID", SqlDbType.Int).Value = observationTypeID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetObservationTypeFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertObservationType(ObservationType observationType)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertObservationType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ObservationTypeID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ObservationTypeName", SqlDbType.NVarChar).Value = observationType.ObservationTypeName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@ObservationTypeID"].Value;
        }
    }

    public bool UpdateObservationType(ObservationType observationType)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateObservationType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ObservationTypeID", SqlDbType.Int).Value = observationType.ObservationTypeID;
            cmd.Parameters.Add("@ObservationTypeName", SqlDbType.NVarChar).Value = observationType.ObservationTypeName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
