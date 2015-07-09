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

public class SqlCarePlanDateTimeProvider:DataAccessObject
{
	public SqlCarePlanDateTimeProvider()
    {
    }


    public bool DeleteCarePlanDateTime(int carePlanDateTimeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteCarePlanDateTime", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CarePlanDateTimeID", SqlDbType.Int).Value = carePlanDateTimeID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<CarePlanDateTime> GetAllCarePlanDateTimes()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllCarePlanDateTimes", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetCarePlanDateTimesFromReader(reader);
        }
    }

    public List<CarePlanDateTime> GetAllCarePlanDateTimesByResidentID(int ResidentID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllCarePlanDateTimesByResidentID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = ResidentID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetCarePlanDateTimesFromReader(reader);
        }
    }

    public List<CarePlanDateTime> GetCarePlanDateTimesFromReader(IDataReader reader)
    {
        List<CarePlanDateTime> carePlanDateTimes = new List<CarePlanDateTime>();

        while (reader.Read())
        {
            carePlanDateTimes.Add(GetCarePlanDateTimeFromReader(reader));
        }
        return carePlanDateTimes;
    }

    public CarePlanDateTime GetCarePlanDateTimeFromReader(IDataReader reader)
    {
        try
        {
            CarePlanDateTime carePlanDateTime = new CarePlanDateTime
                (
                    (int)reader["CarePlanDateTimeID"],
                    (DateTime)reader["CarePlanDateTimeValue"]
                );
             return carePlanDateTime;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public CarePlanDateTime GetCarePlanDateTimeByID(int carePlanDateTimeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetCarePlanDateTimeByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CarePlanDateTimeID", SqlDbType.Int).Value = carePlanDateTimeID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetCarePlanDateTimeFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertCarePlanDateTime(CarePlanDateTime carePlanDateTime)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertCarePlanDateTime", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CarePlanDateTimeID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@CarePlanDateTimeValue", SqlDbType.NVarChar).Value = carePlanDateTime.CarePlanDateTimeValue;
            cmd.Parameters.Add("@ResidentID", SqlDbType.Int).Value = carePlanDateTime.ResidentID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@CarePlanDateTimeID"].Value;
        }
    }

    public bool UpdateCarePlanDateTime(CarePlanDateTime carePlanDateTime)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateCarePlanDateTime", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CarePlanDateTimeID", SqlDbType.Int).Value = carePlanDateTime.CarePlanDateTimeID;
            cmd.Parameters.Add("@CarePlanDateTimeValue", SqlDbType.NVarChar).Value = carePlanDateTime.CarePlanDateTimeValue;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
