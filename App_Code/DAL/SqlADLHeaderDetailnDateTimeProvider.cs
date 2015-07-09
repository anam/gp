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

public class SqlADLHeaderDetailnDateTimeProvider:DataAccessObject
{
	public SqlADLHeaderDetailnDateTimeProvider()
    {
    }


    public bool DeleteADLHeaderDetailnDateTime(int aDLHeaderDetailnDateTimeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteADLHeaderDetailnDateTime", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLHeaderDetailnDateTimeID", SqlDbType.Int).Value = aDLHeaderDetailnDateTimeID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public bool DeleteADLHeaderDetailnDateTimeByADLDateTimeID(int aDLDateTimeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteADLHeaderDetailnDateTimeByADLDateTimeID", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLDateTimeID", SqlDbType.Int).Value = aDLDateTimeID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public DataSet GetAllADLHeaderDetailnDateTimeByResidentID(int ResidentID)
    {
        DataSet Appointmnets = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllADLHeaderDetailnDateTimeByResidentID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = ResidentID;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Appointmnets);
            myadapter.Dispose();
            connection.Close();

            return Appointmnets;
        }
    }


    public DataSet GetAllADLHeaderDetailnDateTimeByResidentIDWithDateRange(int ResidentID, DateTime StartDate, DateTime EndDate)
    {
        DataSet Appointmnets = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllADLHeaderDetailnDateTimeByResidentIDWithDateRange", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = ResidentID;
            command.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = StartDate;
            command.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = EndDate;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Appointmnets);
            myadapter.Dispose();
            connection.Close();

            return Appointmnets;
        }
    }


    public DataSet GetAllADLHeaderDetailnDateTimeByADLDateTimeID(int ADLDateTimeID)
    {
        DataSet Appointmnets = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllADLHeaderDetailnDateTimeByADLDateTimeID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ADLDateTimeID", SqlDbType.Int).Value = ADLDateTimeID;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Appointmnets);
            myadapter.Dispose();
            connection.Close();

            return Appointmnets;
        }
    }


    public List<ADLHeaderDetailnDateTime> GetAllADLHeaderDetailnDateTimes()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllADLHeaderDetailnDateTimes", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetADLHeaderDetailnDateTimesFromReader(reader);
        }
    }
    public List<ADLHeaderDetailnDateTime> GetADLHeaderDetailnDateTimesFromReader(IDataReader reader)
    {
        List<ADLHeaderDetailnDateTime> aDLHeaderDetailnDateTimes = new List<ADLHeaderDetailnDateTime>();

        while (reader.Read())
        {
            aDLHeaderDetailnDateTimes.Add(GetADLHeaderDetailnDateTimeFromReader(reader));
        }
        return aDLHeaderDetailnDateTimes;
    }

    public ADLHeaderDetailnDateTime GetADLHeaderDetailnDateTimeFromReader(IDataReader reader)
    {
        try
        {
            ADLHeaderDetailnDateTime aDLHeaderDetailnDateTime = new ADLHeaderDetailnDateTime
                (
                    (int)reader["ADLHeaderDetailnDateTimeID"],
                    (int)reader["ADLDateTimeID"],
                    (int)reader["ADLHeaderDetailID"],
                    reader["ExtraField1"].ToString(),
                    reader["ExtraField2"].ToString(),
                    reader["ExtraField3"].ToString(),
                    reader["ExtraField4"].ToString(),
                    reader["ExtraField5"].ToString()
                );
             return aDLHeaderDetailnDateTime;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public ADLHeaderDetailnDateTime GetADLHeaderDetailnDateTimeByID(int aDLHeaderDetailnDateTimeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetADLHeaderDetailnDateTimeByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ADLHeaderDetailnDateTimeID", SqlDbType.Int).Value = aDLHeaderDetailnDateTimeID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetADLHeaderDetailnDateTimeFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertADLHeaderDetailnDateTime(ADLHeaderDetailnDateTime aDLHeaderDetailnDateTime)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertADLHeaderDetailnDateTime", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLHeaderDetailnDateTimeID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ADLDateTimeID", SqlDbType.Int).Value = aDLHeaderDetailnDateTime.ADLDateTimeID;
            cmd.Parameters.Add("@ADLHeaderDetailID", SqlDbType.Int).Value = aDLHeaderDetailnDateTime.ADLHeaderDetailID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = aDLHeaderDetailnDateTime.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = aDLHeaderDetailnDateTime.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = aDLHeaderDetailnDateTime.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = aDLHeaderDetailnDateTime.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = aDLHeaderDetailnDateTime.ExtraField5;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@ADLHeaderDetailnDateTimeID"].Value;
        }
    }

    public bool UpdateADLHeaderDetailnDateTime(ADLHeaderDetailnDateTime aDLHeaderDetailnDateTime)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateADLHeaderDetailnDateTime", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLHeaderDetailnDateTimeID", SqlDbType.Int).Value = aDLHeaderDetailnDateTime.ADLHeaderDetailnDateTimeID;
            cmd.Parameters.Add("@ADLDateTimeID", SqlDbType.DateTime).Value = aDLHeaderDetailnDateTime.ADLDateTimeID;
            cmd.Parameters.Add("@ADLHeaderDetailID", SqlDbType.Int).Value = aDLHeaderDetailnDateTime.ADLHeaderDetailID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = aDLHeaderDetailnDateTime.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = aDLHeaderDetailnDateTime.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = aDLHeaderDetailnDateTime.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = aDLHeaderDetailnDateTime.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = aDLHeaderDetailnDateTime.ExtraField5;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
