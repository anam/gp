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

public class SqlADLStatusProvider:DataAccessObject
{
	public SqlADLStatusProvider()
    {
    }


    public bool DeleteADLStatus(int aDLStatusID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteADLStatus", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLStatusID", SqlDbType.Int).Value = aDLStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<ADLStatus> GetAllADLStatuss()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllADLStatuss", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetADLStatussFromReader(reader);
        }
    }
    public List<ADLStatus> GetADLStatussFromReader(IDataReader reader)
    {
        List<ADLStatus> aDLStatuss = new List<ADLStatus>();

        while (reader.Read())
        {
            aDLStatuss.Add(GetADLStatusFromReader(reader));
        }
        return aDLStatuss;
    }

    public ADLStatus GetADLStatusFromReader(IDataReader reader)
    {
        try
        {
            ADLStatus aDLStatus = new ADLStatus
                (
                    (int)reader["ADLStatusID"],
                    reader["ADLStatusName"].ToString()
                );
             return aDLStatus;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public ADLStatus GetADLStatusByID(int aDLStatusID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetADLStatusByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ADLStatusID", SqlDbType.Int).Value = aDLStatusID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetADLStatusFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertADLStatus(ADLStatus aDLStatus)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertADLStatus", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLStatusID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ADLStatusName", SqlDbType.NVarChar).Value = aDLStatus.ADLStatusName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@ADLStatusID"].Value;
        }
    }

    public bool UpdateADLStatus(ADLStatus aDLStatus)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateADLStatus", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLStatusID", SqlDbType.Int).Value = aDLStatus.ADLStatusID;
            cmd.Parameters.Add("@ADLStatusName", SqlDbType.NVarChar).Value = aDLStatus.ADLStatusName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
