using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

public class SqlTourProvider:DataAccessObject
{
	public SqlTourProvider()
    {
    }


    public DataSet  GetAllTours()
    {
        DataSet Tours = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllTours", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Tours);
            myadapter.Dispose();
            connection.Close();

            return Tours;
        }
    }
	public DataSet GetTourPageWise(int pageIndex, int PageSize, out int recordCount)
    {
    DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("GetTourPageWise", connection))
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


    public Tour  GetTourByCustomerID(int  customerID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetTourByCustomerID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CustomerID", SqlDbType.NVarChar).Value = customerID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetTourFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public Tour  GetTourByTourStatusID(int  tourStatusID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetTourByTourStatusID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@TourStatusID", SqlDbType.NVarChar).Value = tourStatusID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetTourFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public DataSet  GetDropDownLisAllTour()
    {
        DataSet Tours = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetDropDownListAllTour", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Tours);
            myadapter.Dispose();
            connection.Close();

            return Tours;
        }
    }

    public DataSet   GetAllToursWithRelation()
    {
       DataSet ds = new DataSet();
       using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllToursWithRelation", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(ds);
            myadapter.Dispose();
            connection.Close();

            return ds;
        }
    }
    public List<Tour> GetToursFromReader(IDataReader reader)
    {
        List<Tour> tours = new List<Tour>();

        while (reader.Read())
        {
            tours.Add(GetTourFromReader(reader));
        }
        return tours;
    }

    public Tour GetTourFromReader(IDataReader reader)
    {
        try
        {
            Tour tour = new Tour
                (

                     DataAccessObject.IsNULL<int>(reader["TourID"]),
                     DataAccessObject.IsNULL<int>(reader["CustomerID"]),
                     DataAccessObject.IsNULL<string>(reader["TourName"]),
                     DataAccessObject.IsNULL<int>(reader["TourStatusID"]),
                     DataAccessObject.IsNULL<int>(reader["SalesCenterID"]),
                     
                     DataAccessObject.IsNULL<DateTime>(reader["TourDate"]),
                     DataAccessObject.IsNULL<string>(reader["TourTime"])
                );
             return tour;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Tour  GetTourByTourID(int  tourID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetTourByTourID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@TourID", SqlDbType.Int).Value = tourID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetTourFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public int InsertTour(Tour tour)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("InsertTour", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TourID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = tour.CustomerID;
            cmd.Parameters.Add("@TourName", SqlDbType.NVarChar).Value = tour.TourName;
            cmd.Parameters.Add("@TourStatusID", SqlDbType.Int).Value = tour.TourStatusID;
            cmd.Parameters.Add("@SalesCenterID", SqlDbType.Int).Value = tour.SalesCenterID;
            
            cmd.Parameters.Add("@TourDate", SqlDbType.DateTime).Value = tour.TourDate;
            cmd.Parameters.Add("@TourTime", SqlDbType.NVarChar).Value = tour.TourTime;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@TourID"].Value;
        }
    }

    public bool UpdateTour(Tour tour)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateTour", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TourID", SqlDbType.Int).Value = tour.TourID;
            cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = tour.CustomerID;
            cmd.Parameters.Add("@TourName", SqlDbType.NVarChar).Value = tour.TourName;
            cmd.Parameters.Add("@TourStatusID", SqlDbType.Int).Value = tour.TourStatusID;
            cmd.Parameters.Add("@SalesCenterID", SqlDbType.Int).Value = tour.SalesCenterID;
            cmd.Parameters.Add("@TourDate", SqlDbType.DateTime).Value = tour.TourDate;
            cmd.Parameters.Add("@TourTime", SqlDbType.NVarChar).Value = tour.TourTime;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }

    public bool DeleteTour(int tourID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteTour", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TourID", SqlDbType.Int).Value = tourID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }
}

