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

public class SqlADLItemDescriptionProvider:DataAccessObject
{
	public SqlADLItemDescriptionProvider()
    {
    }


    public bool DeleteADLItemDescription(int aDLItemDescriptionID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteADLItemDescription", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLItemDescriptionID", SqlDbType.Int).Value = aDLItemDescriptionID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<ADLItemDescription> GetAllADLItemDescriptions()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllADLItemDescriptions", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetADLItemDescriptionsFromReader(reader);
        }
    }
    public List<ADLItemDescription> GetADLItemDescriptionsFromReader(IDataReader reader)
    {
        List<ADLItemDescription> aDLItemDescriptions = new List<ADLItemDescription>();

        while (reader.Read())
        {
            aDLItemDescriptions.Add(GetADLItemDescriptionFromReader(reader));
        }
        return aDLItemDescriptions;
    }

    public ADLItemDescription GetADLItemDescriptionFromReader(IDataReader reader)
    {
        try
        {
            ADLItemDescription aDLItemDescription = new ADLItemDescription
                (
                    (int)reader["ADLItemDescriptionID"],
                    reader["ADLItemDescriptionName"].ToString()
                );
             return aDLItemDescription;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public ADLItemDescription GetADLItemDescriptionByID(int aDLItemDescriptionID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetADLItemDescriptionByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ADLItemDescriptionID", SqlDbType.Int).Value = aDLItemDescriptionID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetADLItemDescriptionFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertADLItemDescription(ADLItemDescription aDLItemDescription)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertADLItemDescription", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLItemDescriptionID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ADLItemDescriptionName", SqlDbType.NVarChar).Value = aDLItemDescription.ADLItemDescriptionName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@ADLItemDescriptionID"].Value;
        }
    }

    public bool UpdateADLItemDescription(ADLItemDescription aDLItemDescription)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateADLItemDescription", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLItemDescriptionID", SqlDbType.Int).Value = aDLItemDescription.ADLItemDescriptionID;
            cmd.Parameters.Add("@ADLItemDescriptionName", SqlDbType.NVarChar).Value = aDLItemDescription.ADLItemDescriptionName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
