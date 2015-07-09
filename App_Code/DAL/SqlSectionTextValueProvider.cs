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

public class SqlSectionTextValueProvider:DataAccessObject
{
	public SqlSectionTextValueProvider()
    {
    }


    public bool DeleteSectionTextValue(int sectionTextValueID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteSectionTextValue", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SectionTextValueID", SqlDbType.Int).Value = sectionTextValueID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<SectionTextValue> GetAllSectionTextValues()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllSectionTextValues", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetSectionTextValuesFromReader(reader);
        }
    }
    public List<SectionTextValue> GetSectionTextValuesFromReader(IDataReader reader)
    {
        List<SectionTextValue> sectionTextValues = new List<SectionTextValue>();

        while (reader.Read())
        {
            sectionTextValues.Add(GetSectionTextValueFromReader(reader));
        }
        return sectionTextValues;
    }

    public SectionTextValue GetSectionTextValueFromReader(IDataReader reader)
    {
        try
        {
            SectionTextValue sectionTextValue = new SectionTextValue
                (
                    (int)reader["SectionTextValueID"],
                    (int)reader["CarePlanDateTimeID"],
                    reader["Section_2"].ToString(),
                    reader["Section_3"].ToString(),
                    reader["Section_6"].ToString(),
                    reader["Section_7"].ToString(),
                    (int)reader["AddedBy"],
                    (DateTime)reader["AddedDate"],
                    (int)reader["UpdatedBy"],
                    (DateTime)reader["UpdatedDate"]
                );
             return sectionTextValue;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public SectionTextValue GetSectionTextValueByID(int sectionTextValueID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetSectionTextValueByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@SectionTextValueID", SqlDbType.Int).Value = sectionTextValueID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetSectionTextValueFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public SectionTextValue GetSectionTextValueByCarePlanDateTimeID(int CarePlanDateTimeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetSectionTextValueByCarePlanDateTimeID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CarePlanDateTimeID", SqlDbType.Int).Value = CarePlanDateTimeID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetSectionTextValueFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertSectionTextValue(SectionTextValue sectionTextValue)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertSectionTextValue", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SectionTextValueID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@CarePlanDateTimeID", SqlDbType.Int).Value = sectionTextValue.CarePlanDateTimeID;
            cmd.Parameters.Add("@Section_2", SqlDbType.NText).Value = sectionTextValue.Section_2;
            cmd.Parameters.Add("@Section_3", SqlDbType.NText).Value = sectionTextValue.Section_3;
            cmd.Parameters.Add("@Section_6", SqlDbType.NText).Value = sectionTextValue.Section_6;
            cmd.Parameters.Add("@Section_7", SqlDbType.NText).Value = sectionTextValue.Section_7;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = sectionTextValue.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = sectionTextValue.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = sectionTextValue.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = sectionTextValue.UpdatedDate;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@SectionTextValueID"].Value;
        }
    }

    public bool UpdateSectionTextValue(SectionTextValue sectionTextValue)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateSectionTextValue", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SectionTextValueID", SqlDbType.Int).Value = sectionTextValue.SectionTextValueID;
            cmd.Parameters.Add("@CarePlanDateTimeID", SqlDbType.Int).Value = sectionTextValue.CarePlanDateTimeID;
            cmd.Parameters.Add("@Section_2", SqlDbType.NText).Value = sectionTextValue.Section_2;
            cmd.Parameters.Add("@Section_3", SqlDbType.NText).Value = sectionTextValue.Section_3;
            cmd.Parameters.Add("@Section_6", SqlDbType.NText).Value = sectionTextValue.Section_6;
            cmd.Parameters.Add("@Section_7", SqlDbType.NText).Value = sectionTextValue.Section_7;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = sectionTextValue.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = sectionTextValue.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = sectionTextValue.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = sectionTextValue.UpdatedDate;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
