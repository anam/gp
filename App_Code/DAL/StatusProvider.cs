using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

public class SqlStatusProvider:DataAccessObject
{
	public SqlStatusProvider()
    {
    }


    public DataSet  GetAllStatuss()
    {
        DataSet Statuss = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllStatuss", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Statuss);
            myadapter.Dispose();
            connection.Close();

            return Statuss;
        }
    }
	public DataSet GetStatusPageWise(int pageIndex, int PageSize, out int recordCount)
    {
    DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("GetStatusPageWise", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PageIndex", pageIndex);
                command.Parameters.AddWithValue("@PageSize",  PageSize );
                command.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                command.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                connection.Open();
                SqlDataAdapter myadapter = new SqlDataAdapter(command);
                myadapter.Fill(ds);
                myadapter.Dispose();
                connection.Close();
                recordCount = Convert.ToInt32(command.Parameters["@RecordCount"].Value);
                return ds;
            }
        }
    }


    public DataSet  GetDropDownLisAllStatus()
    {
        DataSet Statuss = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetDropDownListAllStatus", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Statuss);
            myadapter.Dispose();
            connection.Close();

            return Statuss;
        }
    }
    public List<Status> GetStatussFromReader(IDataReader reader)
    {
        List<Status> statuss = new List<Status>();

        while (reader.Read())
        {
            statuss.Add(GetStatusFromReader(reader));
        }
        return statuss;
    }

    public Status GetStatusFromReader(IDataReader reader)
    {
        try
        {
            Status status = new Status
                (

                     DataAccessObject.IsNULL<int>(reader["StastusID"]),
                     DataAccessObject.IsNULL<string>(reader["StatusPrefix"]),
                     DataAccessObject.IsNULL<int>(reader["pageid"]),
                     DataAccessObject.IsNULL<string>(reader["StatDesc"])
                );
             return status;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Status  GetStatusByStastusID(int  stastusID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetStatusByStastusID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@StastusID", SqlDbType.Int).Value = stastusID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetStatusFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public int InsertStatus(Status status)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("InsertStatus", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StastusID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@StatusPrefix", SqlDbType.NVarChar).Value = status.StatusPrefix;
            cmd.Parameters.Add("@pageid", SqlDbType.Int).Value = status.pageid;
            cmd.Parameters.Add("@StatDesc", SqlDbType.NVarChar).Value = status.StatDesc;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@StastusID"].Value;
        }
    }

    public bool UpdateStatus(Status status)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateStatus", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StastusID", SqlDbType.Int).Value = status.StastusID;
            cmd.Parameters.Add("@StatusPrefix", SqlDbType.NVarChar).Value = status.StatusPrefix;
            cmd.Parameters.Add("@pageid", SqlDbType.Int).Value = status.pageid;
            cmd.Parameters.Add("@StatDesc", SqlDbType.NVarChar).Value = status.StatDesc;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }

    public bool DeleteStatus(int stastusID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteStatus", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StastusID", SqlDbType.Int).Value = stastusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }
}

