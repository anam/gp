using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

public class SqlMarketingProvider:DataAccessObject
{
	public SqlMarketingProvider()
    {
    }


    public DataSet  GetAllMarketings()
    {
        DataSet Marketings = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllMarketings", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Marketings);
            myadapter.Dispose();
            connection.Close();

            return Marketings;
        }
    }
	public DataSet GetMarketingPageWise(int pageIndex, int PageSize, out int recordCount)
    {
    DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("GetMarketingPageWise", connection))
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


    public Marketing  GetMarketingByCustomerID(int  customerID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetMarketingByCustomerID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CustomerID", SqlDbType.NVarChar).Value = customerID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetMarketingFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public Marketing  GetMarketingByMarketingAgentID(int  marketingAgentID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetMarketingByMarketingAgentID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MarketingAgentID", SqlDbType.NVarChar).Value = marketingAgentID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetMarketingFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public Marketing  GetMarketingByMarketingCloserID(int  marketingCloserID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetMarketingByMarketingCloserID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MarketingCloserID", SqlDbType.NVarChar).Value = marketingCloserID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetMarketingFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public Marketing  GetMarketingByMarketingVanueID(int  marketingVanueID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetMarketingByMarketingVanueID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MarketingVanueID", SqlDbType.NVarChar).Value = marketingVanueID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetMarketingFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public Marketing  GetMarketingByLeadSourceID(int  leadSourceID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetMarketingByLeadSourceID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@LeadSourceID", SqlDbType.NVarChar).Value = leadSourceID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetMarketingFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public Marketing  GetMarketingByGiftID(int  giftID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetMarketingByGiftID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@GiftID", SqlDbType.NVarChar).Value = giftID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetMarketingFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public DataSet  GetDropDownLisAllMarketing()
    {
        DataSet Marketings = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetDropDownListAllMarketing", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Marketings);
            myadapter.Dispose();
            connection.Close();

            return Marketings;
        }
    }

    public DataSet   GetAllMarketingsWithRelation()
    {
       DataSet ds = new DataSet();
       using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllMarketingsWithRelation", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(ds);
            myadapter.Dispose();
            connection.Close();

            return ds;
        }
    }
    public List<Marketing> GetMarketingsFromReader(IDataReader reader)
    {
        List<Marketing> marketings = new List<Marketing>();

        while (reader.Read())
        {
            marketings.Add(GetMarketingFromReader(reader));
        }
        return marketings;
    }

    public Marketing GetMarketingFromReader(IDataReader reader)
    {
        try
        {
            Marketing marketing = new Marketing
                (

                     DataAccessObject.IsNULL<int>(reader["MarketingID"]),
                     DataAccessObject.IsNULL<int>(reader["CustomerID"]),
                     DataAccessObject.IsNULL<int>(reader["MarketingAgentID"]),
                     DataAccessObject.IsNULL<int>(reader["MarketingCloserID"]),
                     DataAccessObject.IsNULL<int>(reader["MarketingVanueID"]),
                     DataAccessObject.IsNULL<int>(reader["LeadSourceID"]),
                     DataAccessObject.IsNULL<int>(reader["GiftID"]),
                     DataAccessObject.IsNULL<decimal>(reader["DepositAmount"]),
                     DataAccessObject.IsNULL<bool>(reader["Refundable"]),
                     DataAccessObject.IsNULL<string>(reader["Notes"])
                );
             return marketing;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Marketing  GetMarketingByMarketingID(int  marketingID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetMarketingByMarketingID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MarketingID", SqlDbType.Int).Value = marketingID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetMarketingFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public int InsertMarketing(Marketing marketing)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("InsertMarketing", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MarketingID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = marketing.CustomerID;
            cmd.Parameters.Add("@MarketingAgentID", SqlDbType.Int).Value = marketing.MarketingAgentID;
            cmd.Parameters.Add("@MarketingCloserID", SqlDbType.Int).Value = marketing.MarketingCloserID;
            cmd.Parameters.Add("@MarketingVanueID", SqlDbType.Int).Value = marketing.MarketingVanueID;
            cmd.Parameters.Add("@LeadSourceID", SqlDbType.Int).Value = marketing.LeadSourceID;
            cmd.Parameters.Add("@GiftID", SqlDbType.Int).Value = marketing.GiftID;
            cmd.Parameters.Add("@DepositAmount", SqlDbType.Decimal).Value = marketing.DepositAmount;
            cmd.Parameters.Add("@Refundable", SqlDbType.Bit).Value = marketing.Refundable;
            cmd.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = marketing.Notes;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@MarketingID"].Value;
        }
    }

    public bool UpdateMarketing(Marketing marketing)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateMarketing", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MarketingID", SqlDbType.Int).Value = marketing.MarketingID;
            cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = marketing.CustomerID;
            cmd.Parameters.Add("@MarketingAgentID", SqlDbType.Int).Value = marketing.MarketingAgentID;
            cmd.Parameters.Add("@MarketingCloserID", SqlDbType.Int).Value = marketing.MarketingCloserID;
            cmd.Parameters.Add("@MarketingVanueID", SqlDbType.Int).Value = marketing.MarketingVanueID;
            cmd.Parameters.Add("@LeadSourceID", SqlDbType.Int).Value = marketing.LeadSourceID;
            cmd.Parameters.Add("@GiftID", SqlDbType.Int).Value = marketing.GiftID;
            cmd.Parameters.Add("@DepositAmount", SqlDbType.Decimal).Value = marketing.DepositAmount;
            cmd.Parameters.Add("@Refundable", SqlDbType.Bit).Value = marketing.Refundable;
            cmd.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = marketing.Notes;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }

    public bool DeleteMarketing(int marketingID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteMarketing", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MarketingID", SqlDbType.Int).Value = marketingID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }
}

