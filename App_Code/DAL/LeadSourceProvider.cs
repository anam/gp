using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

public class SqlLeadSourceProvider:DataAccessObject
{
	public SqlLeadSourceProvider()
    {
    }


    public DataSet  GetAllLeadSources()
    {
        DataSet LeadSources = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllLeadSources", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(LeadSources);
            myadapter.Dispose();
            connection.Close();

            return LeadSources;
        }
    }
	public DataSet GetLeadSourcePageWise(int pageIndex, int PageSize, out int recordCount)
    {
    DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("GetLeadSourcePageWise", connection))
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


    public DataSet  GetDropDownLisAllLeadSource()
    {
        DataSet LeadSources = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetDropDownListAllLeadSource", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(LeadSources);
            myadapter.Dispose();
            connection.Close();

            return LeadSources;
        }
    }
    public List<LeadSource> GetLeadSourcesFromReader(IDataReader reader)
    {
        List<LeadSource> leadSources = new List<LeadSource>();

        while (reader.Read())
        {
            leadSources.Add(GetLeadSourceFromReader(reader));
        }
        return leadSources;
    }

    public LeadSource GetLeadSourceFromReader(IDataReader reader)
    {
        try
        {
            LeadSource leadSource = new LeadSource
                (

                     DataAccessObject.IsNULL<int>(reader["LeadSourceID"]),
                     DataAccessObject.IsNULL<string>(reader["LeadSourceName"])
                );
             return leadSource;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public LeadSource  GetLeadSourceByLeadSourceID(int  leadSourceID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetLeadSourceByLeadSourceID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@LeadSourceID", SqlDbType.Int).Value = leadSourceID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetLeadSourceFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public int InsertLeadSource(LeadSource leadSource)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("InsertLeadSource", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LeadSourceID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@LeadSourceName", SqlDbType.NVarChar).Value = leadSource.LeadSourceName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@LeadSourceID"].Value;
        }
    }

    public bool UpdateLeadSource(LeadSource leadSource)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateLeadSource", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LeadSourceID", SqlDbType.Int).Value = leadSource.LeadSourceID;
            cmd.Parameters.Add("@LeadSourceName", SqlDbType.NVarChar).Value = leadSource.LeadSourceName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }

    public bool DeleteLeadSource(int leadSourceID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteLeadSource", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LeadSourceID", SqlDbType.Int).Value = leadSourceID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }
}

