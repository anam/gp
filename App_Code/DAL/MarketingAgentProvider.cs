using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

public class SqlMarketingAgentProvider:DataAccessObject
{
	public SqlMarketingAgentProvider()
    {
    }


    public DataSet  GetAllMarketingAgents()
    {
        DataSet MarketingAgents = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllMarketingAgents", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(MarketingAgents);
            myadapter.Dispose();
            connection.Close();

            return MarketingAgents;
        }
    }
	public DataSet GetMarketingAgentPageWise(int pageIndex, int PageSize, out int recordCount)
    {
    DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("GetMarketingAgentPageWise", connection))
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


    public DataSet  GetDropDownLisAllMarketingAgent()
    {
        DataSet MarketingAgents = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetDropDownListAllMarketingAgent", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(MarketingAgents);
            myadapter.Dispose();
            connection.Close();

            return MarketingAgents;
        }
    }
    public List<MarketingAgent> GetMarketingAgentsFromReader(IDataReader reader)
    {
        List<MarketingAgent> marketingAgents = new List<MarketingAgent>();

        while (reader.Read())
        {
            marketingAgents.Add(GetMarketingAgentFromReader(reader));
        }
        return marketingAgents;
    }

    public MarketingAgent GetMarketingAgentFromReader(IDataReader reader)
    {
        try
        {
            MarketingAgent marketingAgent = new MarketingAgent
                (

                     DataAccessObject.IsNULL<int>(reader["MarketingAgentID"]),
                     DataAccessObject.IsNULL<string>(reader["MarketingAgenName"])
                );
             return marketingAgent;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public MarketingAgent  GetMarketingAgentByMarketingAgentID(int  marketingAgentID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetMarketingAgentByMarketingAgentID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MarketingAgentID", SqlDbType.Int).Value = marketingAgentID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetMarketingAgentFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public int InsertMarketingAgent(MarketingAgent marketingAgent)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("InsertMarketingAgent", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MarketingAgentID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@MarketingAgenName", SqlDbType.NVarChar).Value = marketingAgent.MarketingAgenName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@MarketingAgentID"].Value;
        }
    }

    public bool UpdateMarketingAgent(MarketingAgent marketingAgent)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateMarketingAgent", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MarketingAgentID", SqlDbType.Int).Value = marketingAgent.MarketingAgentID;
            cmd.Parameters.Add("@MarketingAgenName", SqlDbType.NVarChar).Value = marketingAgent.MarketingAgenName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }

    public bool DeleteMarketingAgent(int marketingAgentID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteMarketingAgent", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MarketingAgentID", SqlDbType.Int).Value = marketingAgentID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }
}

