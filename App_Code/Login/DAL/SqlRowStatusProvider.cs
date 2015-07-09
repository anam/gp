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

public class SqlRowStatusProvider:DataAccessObject
{
	public SqlRowStatusProvider()
    {
    }


    public bool DeleteRowStatus(int rowStatusID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_DeleteRowStatus", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = rowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<RowStatus> GetAllRowStatuss()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetAllRowStatuss", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetRowStatussFromReader(reader);
        }
    }
    public List<RowStatus> GetRowStatussFromReader(IDataReader reader)
    {
        List<RowStatus> rowStatuss = new List<RowStatus>();

        while (reader.Read())
        {
            rowStatuss.Add(GetRowStatusFromReader(reader));
        }
        return rowStatuss;
    }

    public RowStatus GetRowStatusFromReader(IDataReader reader)
    {
        try
        {
            RowStatus rowStatus = new RowStatus
                (
                    (int)reader["RowStatusID"],
                    reader["RowStatusName"].ToString()
                );
             return rowStatus;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public RowStatus GetRowStatusByID(int rowStatusID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetRowStatusByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = rowStatusID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetRowStatusFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertRowStatus(RowStatus rowStatus)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_InsertRowStatus", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@RowStatusName", SqlDbType.NVarChar).Value = rowStatus.RowStatusName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@RowStatusID"].Value;
        }
    }

    public bool UpdateRowStatus(RowStatus rowStatus)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_UpdateRowStatus", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = rowStatus.RowStatusID;
            cmd.Parameters.Add("@RowStatusName", SqlDbType.NVarChar).Value = rowStatus.RowStatusName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
