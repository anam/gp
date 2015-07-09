using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

public class SqlGiftProvider:DataAccessObject
{
	public SqlGiftProvider()
    {
    }


    public DataSet  GetAllGifts()
    {
        DataSet Gifts = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllGifts", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Gifts);
            myadapter.Dispose();
            connection.Close();

            return Gifts;
        }
    }
	public DataSet GetGiftPageWise(int pageIndex, int PageSize, out int recordCount)
    {
    DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("GetGiftPageWise", connection))
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


    public DataSet  GetDropDownLisAllGift()
    {
        DataSet Gifts = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetDropDownListAllGift", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Gifts);
            myadapter.Dispose();
            connection.Close();

            return Gifts;
        }
    }
    public List<Gift> GetGiftsFromReader(IDataReader reader)
    {
        List<Gift> gifts = new List<Gift>();

        while (reader.Read())
        {
            gifts.Add(GetGiftFromReader(reader));
        }
        return gifts;
    }

    public Gift GetGiftFromReader(IDataReader reader)
    {
        try
        {
            Gift gift = new Gift
                (

                     DataAccessObject.IsNULL<int>(reader["GiftID"]),
                     DataAccessObject.IsNULL<string>(reader["GiftName"])
                );
             return gift;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Gift  GetGiftByGiftID(int  giftID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetGiftByGiftID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@GiftID", SqlDbType.Int).Value = giftID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetGiftFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public int InsertGift(Gift gift)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("InsertGift", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@GiftID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@GiftName", SqlDbType.NVarChar).Value = gift.GiftName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@GiftID"].Value;
        }
    }

    public bool UpdateGift(Gift gift)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateGift", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@GiftID", SqlDbType.Int).Value = gift.GiftID;
            cmd.Parameters.Add("@GiftName", SqlDbType.NVarChar).Value = gift.GiftName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }

    public bool DeleteGift(int giftID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteGift", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@GiftID", SqlDbType.Int).Value = giftID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }
}

