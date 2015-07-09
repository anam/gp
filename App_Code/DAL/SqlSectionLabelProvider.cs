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

public class SqlSectionLabelProvider:DataAccessObject
{
	public SqlSectionLabelProvider()
    {
    }


    public bool DeleteSectionLabel(int sectionLabelID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteSectionLabel", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SectionLabelID", SqlDbType.Int).Value = sectionLabelID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<SectionLabel> GetAllSectionLabels()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllSectionLabels", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetSectionLabelsFromReader(reader);
        }
    }
    public List<SectionLabel> GetSectionLabelsFromReader(IDataReader reader)
    {
        List<SectionLabel> sectionLabels = new List<SectionLabel>();

        while (reader.Read())
        {
            sectionLabels.Add(GetSectionLabelFromReader(reader));
        }
        return sectionLabels;
    }

    public SectionLabel GetSectionLabelFromReader(IDataReader reader)
    {
        try
        {
            SectionLabel sectionLabel = new SectionLabel
                (
                    (int)reader["SectionLabelID"],
                    (int)reader["SectionNo"],
                    reader["LabelText"].ToString(),
                    reader["ExtraField1"].ToString(),
                    reader["ExtraField2"].ToString(),
                    reader["ExtraField3"].ToString(),
                    reader["ExtraField4"].ToString(),
                    reader["ExtraField5"].ToString()
                );
             return sectionLabel;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public SectionLabel GetSectionLabelByID(int sectionLabelID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetSectionLabelByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@SectionLabelID", SqlDbType.Int).Value = sectionLabelID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetSectionLabelFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertSectionLabel(SectionLabel sectionLabel)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertSectionLabel", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SectionLabelID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@SectionNo", SqlDbType.Int).Value = sectionLabel.SectionNo;
            cmd.Parameters.Add("@LabelText", SqlDbType.NVarChar).Value = sectionLabel.LabelText;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = sectionLabel.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = sectionLabel.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = sectionLabel.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = sectionLabel.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = sectionLabel.ExtraField5;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@SectionLabelID"].Value;
        }
    }

    public bool UpdateSectionLabel(SectionLabel sectionLabel)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateSectionLabel", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SectionLabelID", SqlDbType.Int).Value = sectionLabel.SectionLabelID;
            cmd.Parameters.Add("@SectionNo", SqlDbType.Int).Value = sectionLabel.SectionNo;
            cmd.Parameters.Add("@LabelText", SqlDbType.NVarChar).Value = sectionLabel.LabelText;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = sectionLabel.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = sectionLabel.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = sectionLabel.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = sectionLabel.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = sectionLabel.ExtraField5;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
