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

public class SqlADLHeaderProvider:DataAccessObject
{
	public SqlADLHeaderProvider()
    {
    }


    public bool DeleteADLHeader(int aDLHeaderID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteADLHeader", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLHeaderID", SqlDbType.Int).Value = aDLHeaderID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<ADLHeader> GetAllADLHeaders()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllADLHeaders", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetADLHeadersFromReader(reader);
        }
    }
    public List<ADLHeader> GetADLHeadersFromReader(IDataReader reader)
    {
        List<ADLHeader> aDLHeaders = new List<ADLHeader>();

        while (reader.Read())
        {
            aDLHeaders.Add(GetADLHeaderFromReader(reader));
        }
        return aDLHeaders;
    }

    public ADLHeader GetADLHeaderFromReader(IDataReader reader)
    {
        try
        {
            ADLHeader aDLHeader = new ADLHeader
                (
                    (int)reader["ADLHeaderID"],
                    reader["ADLHeaderName"].ToString(),
                    reader["ExtraField1"].ToString(),
                    reader["ExtraField2"].ToString(),
                    reader["ExtraField3"].ToString(),
                    reader["ExtraField4"].ToString(),
                    reader["ExtraField5"].ToString()
                );
             return aDLHeader;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public ADLHeader GetADLHeaderByID(int aDLHeaderID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetADLHeaderByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ADLHeaderID", SqlDbType.Int).Value = aDLHeaderID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetADLHeaderFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertADLHeader(ADLHeader aDLHeader)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertADLHeader", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLHeaderID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ADLHeaderName", SqlDbType.NVarChar).Value = aDLHeader.ADLHeaderName;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = aDLHeader.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = aDLHeader.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = aDLHeader.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = aDLHeader.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = aDLHeader.ExtraField5;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@ADLHeaderID"].Value;
        }
    }

    public bool UpdateADLHeader(ADLHeader aDLHeader)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateADLHeader", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLHeaderID", SqlDbType.Int).Value = aDLHeader.ADLHeaderID;
            cmd.Parameters.Add("@ADLHeaderName", SqlDbType.NVarChar).Value = aDLHeader.ADLHeaderName;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = aDLHeader.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = aDLHeader.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = aDLHeader.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = aDLHeader.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = aDLHeader.ExtraField5;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
