using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

public class SqlMarketingVanueProvider:DataAccessObject
{
	public SqlMarketingVanueProvider()
    {
    }


    public DataSet  GetAllMarketingVanues()
    {
        DataSet MarketingVanues = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllMarketingVanues", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(MarketingVanues);
            myadapter.Dispose();
            connection.Close();

            return MarketingVanues;
        }
    }
	public DataSet GetMarketingVanuePageWise(int pageIndex, int PageSize, out int recordCount)
    {
    DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("GetMarketingVanuePageWise", connection))
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


    public DataSet  GetDropDownLisAllMarketingVanue()
    {
        DataSet MarketingVanues = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetDropDownListAllMarketingVanue", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(MarketingVanues);
            myadapter.Dispose();
            connection.Close();

            return MarketingVanues;
        }
    }
    public List<MarketingVanue> GetMarketingVanuesFromReader(IDataReader reader)
    {
        List<MarketingVanue> marketingVanues = new List<MarketingVanue>();

        while (reader.Read())
        {
            marketingVanues.Add(GetMarketingVanueFromReader(reader));
        }
        return marketingVanues;
    }

    public MarketingVanue GetMarketingVanueFromReader(IDataReader reader)
    {
        try
        {
            MarketingVanue marketingVanue = new MarketingVanue
                (

                     DataAccessObject.IsNULL<int>(reader["MarketingVanueID"]),
                     DataAccessObject.IsNULL<string>(reader["MarketingVanueName"])
                );
             return marketingVanue;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public MarketingVanue  GetMarketingVanueByMarketingVanueID(int  marketingVanueID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetMarketingVanueByMarketingVanueID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MarketingVanueID", SqlDbType.Int).Value = marketingVanueID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetMarketingVanueFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public int InsertMarketingVanue(MarketingVanue marketingVanue)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("InsertMarketingVanue", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MarketingVanueID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@MarketingVanueName", SqlDbType.NVarChar).Value = marketingVanue.MarketingVanueName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@MarketingVanueID"].Value;
        }
    }

    public bool UpdateMarketingVanue(MarketingVanue marketingVanue)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateMarketingVanue", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MarketingVanueID", SqlDbType.Int).Value = marketingVanue.MarketingVanueID;
            cmd.Parameters.Add("@MarketingVanueName", SqlDbType.NVarChar).Value = marketingVanue.MarketingVanueName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }

    public bool DeleteMarketingVanue(int marketingVanueID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteMarketingVanue", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MarketingVanueID", SqlDbType.Int).Value = marketingVanueID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }
}

