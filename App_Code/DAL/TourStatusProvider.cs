using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

public class SqlTourStatusProvider:DataAccessObject
{
	public SqlTourStatusProvider()
    {
    }


    public DataSet  GetAllTourStatuss()
    {
        DataSet TourStatuss = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllTourStatuss", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(TourStatuss);
            myadapter.Dispose();
            connection.Close();

            return TourStatuss;
        }
    }
	public DataSet GetTourStatusPageWise(int pageIndex, int PageSize, out int recordCount)
    {
    DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("GetTourStatusPageWise", connection))
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


    public DataSet  GetDropDownLisAllTourStatus()
    {
        DataSet TourStatuss = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetDropDownListAllTourStatus", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(TourStatuss);
            myadapter.Dispose();
            connection.Close();

            return TourStatuss;
        }
    }
    public List<TourStatus> GetTourStatussFromReader(IDataReader reader)
    {
        List<TourStatus> tourStatuss = new List<TourStatus>();

        while (reader.Read())
        {
            tourStatuss.Add(GetTourStatusFromReader(reader));
        }
        return tourStatuss;
    }

    public TourStatus GetTourStatusFromReader(IDataReader reader)
    {
        try
        {
            TourStatus tourStatus = new TourStatus
                (

                     DataAccessObject.IsNULL<int>(reader["TourStatusID"]),
                     DataAccessObject.IsNULL<string>(reader["TourStatusName"])
                );
             return tourStatus;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public TourStatus  GetTourStatusByTourStatusID(int  tourStatusID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetTourStatusByTourStatusID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@TourStatusID", SqlDbType.Int).Value = tourStatusID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetTourStatusFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public int InsertTourStatus(TourStatus tourStatus)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("InsertTourStatus", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TourStatusID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@TourStatusName", SqlDbType.NVarChar).Value = tourStatus.TourStatusName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@TourStatusID"].Value;
        }
    }

    public bool UpdateTourStatus(TourStatus tourStatus)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateTourStatus", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TourStatusID", SqlDbType.Int).Value = tourStatus.TourStatusID;
            cmd.Parameters.Add("@TourStatusName", SqlDbType.NVarChar).Value = tourStatus.TourStatusName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }

    public bool DeleteTourStatus(int tourStatusID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteTourStatus", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TourStatusID", SqlDbType.Int).Value = tourStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }
}

