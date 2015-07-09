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

public class SqlADLDateTimeProvider:DataAccessObject
{
	public SqlADLDateTimeProvider()
    {
    }


    public bool DeleteADLDateTime(int aDLDateTimeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteADLDateTime", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLDateTimeID", SqlDbType.Int).Value = aDLDateTimeID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<ADLDateTime> GetAllADLDateTimes()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllADLDateTimes", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetADLDateTimesFromReader(reader);
        }
    }
    public List<ADLDateTime> GetADLDateTimesFromReader(IDataReader reader)
    {
        List<ADLDateTime> aDLDateTimes = new List<ADLDateTime>();

        while (reader.Read())
        {
            aDLDateTimes.Add(GetADLDateTimeFromReader(reader));
        }
        return aDLDateTimes;
    }

    public ADLDateTime GetADLDateTimeFromReader(IDataReader reader)
    {
        try
        {
            ADLDateTime aDLDateTime = new ADLDateTime
                (
                    (int)reader["ADLDateTimeID"],
                    (DateTime)reader["ADLDateTime"]
                );
             return aDLDateTime;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public ADLDateTime GetADLDateTimeByID(int aDLDateTimeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetADLDateTimeByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ADLDateTimeID", SqlDbType.Int).Value = aDLDateTimeID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetADLDateTimeFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertADLDateTime(ADLDateTime aDLDateTime)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertADLDateTime", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLDateTimeID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ADLDateTime", SqlDbType.DateTime).Value = aDLDateTime.ADLDateTimeValue;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@ADLDateTimeID"].Value;
        }
    }

    public bool UpdateADLDateTime(ADLDateTime aDLDateTime)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateADLDateTime", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLDateTimeID", SqlDbType.Int).Value = aDLDateTime.ADLDateTimeID;
            cmd.Parameters.Add("@ADLDateTime", SqlDbType.DateTime).Value = aDLDateTime.ADLDateTimeValue;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
