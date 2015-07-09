using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

public class SqlMetroLocationProvider:DataAccessObject
{
	public SqlMetroLocationProvider()
    {
    }


    public DataSet  GetAllMetroLocations()
    {
        DataSet MetroLocations = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllMetroLocations", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(MetroLocations);
            myadapter.Dispose();
            connection.Close();

            return MetroLocations;
        }
    }
	public DataSet GetMetroLocationPageWise(int pageIndex, int PageSize, out int recordCount)
    {
    DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("GetMetroLocationPageWise", connection))
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


    public DataSet  GetDropDownLisAllMetroLocation()
    {
        DataSet MetroLocations = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetDropDownListAllMetroLocation", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(MetroLocations);
            myadapter.Dispose();
            connection.Close();

            return MetroLocations;
        }
    }
    public List<MetroLocation> GetMetroLocationsFromReader(IDataReader reader)
    {
        List<MetroLocation> metroLocations = new List<MetroLocation>();

        while (reader.Read())
        {
            metroLocations.Add(GetMetroLocationFromReader(reader));
        }
        return metroLocations;
    }

    public MetroLocation GetMetroLocationFromReader(IDataReader reader)
    {
        try
        {
            MetroLocation metroLocation = new MetroLocation
                (

                     DataAccessObject.IsNULL<int>(reader["MetroLocationID"]),
                     DataAccessObject.IsNULL<string>(reader["MetroLocationName"]),
                     DataAccessObject.IsNULL<string>(reader["State"])
                );
             return metroLocation;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public MetroLocation  GetMetroLocationByMetroLocationID(int  metroLocationID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetMetroLocationByMetroLocationID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MetroLocationID", SqlDbType.Int).Value = metroLocationID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetMetroLocationFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public int InsertMetroLocation(MetroLocation metroLocation)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("InsertMetroLocation", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MetroLocationID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@MetroLocationName", SqlDbType.NVarChar).Value = metroLocation.MetroLocationName;
            cmd.Parameters.Add("@State", SqlDbType.NVarChar).Value = metroLocation.State;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@MetroLocationID"].Value;
        }
    }

    public bool UpdateMetroLocation(MetroLocation metroLocation)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateMetroLocation", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MetroLocationID", SqlDbType.Int).Value = metroLocation.MetroLocationID;
            cmd.Parameters.Add("@MetroLocationName", SqlDbType.NVarChar).Value = metroLocation.MetroLocationName;
            cmd.Parameters.Add("@State", SqlDbType.NVarChar).Value = metroLocation.State;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }

    public bool DeleteMetroLocation(int metroLocationID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteMetroLocation", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MetroLocationID", SqlDbType.Int).Value = metroLocationID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }
}

