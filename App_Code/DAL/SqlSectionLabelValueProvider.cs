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

public class SqlSectionLabelValueProvider:DataAccessObject
{
	public SqlSectionLabelValueProvider()
    {
    }


    public bool DeleteSectionLabelValue(int sectionLabelValueID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteSectionLabelValue", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SectionLabelValueID", SqlDbType.Int).Value = sectionLabelValueID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public bool DeleteSectionLabelValueByCarePlanDateTimeID(string CarePlanDateTimeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteSectionLabelValueByCarePlanDateTimeID", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = CarePlanDateTimeID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<SectionLabelValue> GetAllSectionLabelValues()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllSectionLabelValues", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetSectionLabelValuesFromReader(reader);
        }
    }

    public List<SectionLabelValue> GetAllSectionLabelValuesByCarePlanDateTimeID(int CarePlanDateTimeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllSectionLabelValuesByCarePlanDateTimeID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = CarePlanDateTimeID.ToString();
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetSectionLabelValuesFromReader(reader);
        }
    }

    public List<SectionLabelValue> GetSectionLabelValuesFromReader(IDataReader reader)
    {
        List<SectionLabelValue> sectionLabelValues = new List<SectionLabelValue>();

        while (reader.Read())
        {
            sectionLabelValues.Add(GetSectionLabelValueFromReader(reader));
        }
        return sectionLabelValues;
    }

    public SectionLabelValue GetSectionLabelValueFromReader(IDataReader reader)
    {
        try
        {
            SectionLabelValue sectionLabelValue = new SectionLabelValue
                (
                    (int)reader["SectionLabelValueID"],
                    (int)reader["SectionLabelID"],
                    (int)reader["AddedBy"],
                    (DateTime)reader["AddedDate"],
                    reader["Value"].ToString(),
                    reader["ExtraField1"].ToString(),
                    reader["ExtraField2"].ToString()
                );
             return sectionLabelValue;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public SectionLabelValue GetSectionLabelValueByID(int sectionLabelValueID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetSectionLabelValueByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@SectionLabelValueID", SqlDbType.Int).Value = sectionLabelValueID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetSectionLabelValueFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertSectionLabelValue(SectionLabelValue sectionLabelValue)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertSectionLabelValue", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SectionLabelValueID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@SectionLabelID", SqlDbType.Int).Value = sectionLabelValue.SectionLabelID;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = sectionLabelValue.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = sectionLabelValue.AddedDate;
            cmd.Parameters.Add("@Value", SqlDbType.NVarChar).Value = sectionLabelValue.Value;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = sectionLabelValue.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = sectionLabelValue.ExtraField2;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@SectionLabelValueID"].Value;
        }
    }

    public bool UpdateSectionLabelValue(SectionLabelValue sectionLabelValue)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateSectionLabelValue", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SectionLabelValueID", SqlDbType.Int).Value = sectionLabelValue.SectionLabelValueID;
            cmd.Parameters.Add("@SectionLabelID", SqlDbType.Int).Value = sectionLabelValue.SectionLabelID;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = sectionLabelValue.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = sectionLabelValue.AddedDate;
            cmd.Parameters.Add("@Value", SqlDbType.NVarChar).Value = sectionLabelValue.Value;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = sectionLabelValue.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = sectionLabelValue.ExtraField2;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
