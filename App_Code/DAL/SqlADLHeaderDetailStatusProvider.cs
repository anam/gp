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

public class SqlADLHeaderDetailStatusProvider:DataAccessObject
{
	public SqlADLHeaderDetailStatusProvider()
    {
    }


    public bool DeleteADLHeaderDetailStatus(int aDLHeaderDetailStatusID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteADLHeaderDetailStatus", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLHeaderDetailStatusID", SqlDbType.Int).Value = aDLHeaderDetailStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<ADLHeaderDetailStatus> GetAllADLHeaderDetailStatuss()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllADLHeaderDetailStatuss", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetADLHeaderDetailStatussFromReader(reader);
        }
    }
    public List<ADLHeaderDetailStatus> GetADLHeaderDetailStatussFromReader(IDataReader reader)
    {
        List<ADLHeaderDetailStatus> aDLHeaderDetailStatuss = new List<ADLHeaderDetailStatus>();

        while (reader.Read())
        {
            aDLHeaderDetailStatuss.Add(GetADLHeaderDetailStatusFromReader(reader));
        }
        return aDLHeaderDetailStatuss;
    }

    public ADLHeaderDetailStatus GetADLHeaderDetailStatusFromReader(IDataReader reader)
    {
        try
        {
            ADLHeaderDetailStatus aDLHeaderDetailStatus = new ADLHeaderDetailStatus
                (
                    (int)reader["ADLHeaderDetailStatusID"],
                    reader["ADLHeaderDetailStatusName"].ToString()
                );
             return aDLHeaderDetailStatus;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public ADLHeaderDetailStatus GetADLHeaderDetailStatusByID(int aDLHeaderDetailStatusID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetADLHeaderDetailStatusByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ADLHeaderDetailStatusID", SqlDbType.Int).Value = aDLHeaderDetailStatusID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetADLHeaderDetailStatusFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertADLHeaderDetailStatus(ADLHeaderDetailStatus aDLHeaderDetailStatus)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertADLHeaderDetailStatus", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLHeaderDetailStatusID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ADLHeaderDetailStatusName", SqlDbType.NVarChar).Value = aDLHeaderDetailStatus.ADLHeaderDetailStatusName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@ADLHeaderDetailStatusID"].Value;
        }
    }

    public bool UpdateADLHeaderDetailStatus(ADLHeaderDetailStatus aDLHeaderDetailStatus)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateADLHeaderDetailStatus", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLHeaderDetailStatusID", SqlDbType.Int).Value = aDLHeaderDetailStatus.ADLHeaderDetailStatusID;
            cmd.Parameters.Add("@ADLHeaderDetailStatusName", SqlDbType.NVarChar).Value = aDLHeaderDetailStatus.ADLHeaderDetailStatusName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
