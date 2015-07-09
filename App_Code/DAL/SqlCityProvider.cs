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

public class SqlCityProvider:DataAccessObject
{
	public SqlCityProvider()
    {
    }


    public bool DeleteCity(int cityID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteCity", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = cityID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<City> GetAllCities()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllCities", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetCitiesFromReader(reader);
        }
    }
    public List<City> GetCitiesFromReader(IDataReader reader)
    {
        List<City> cities = new List<City>();

        while (reader.Read())
        {
            cities.Add(GetCityFromReader(reader));
        }
        return cities;
    }

    public City GetCityFromReader(IDataReader reader)
    {
        try
        {
            City city = new City
                (
                    (int)reader["CityID"],
                    reader["CityName"].ToString(),
                    (int)reader["StateID"]
                );
             return city;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public City GetCityByID(int cityID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetCityByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CityID", SqlDbType.Int).Value = cityID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetCityFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertCity(City city)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertCity", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CityID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@CityName", SqlDbType.NVarChar).Value = city.CityName;
            cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = city.StateID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@CityID"].Value;
        }
    }

    public bool UpdateCity(City city)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateCity", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = city.CityID;
            cmd.Parameters.Add("@CityName", SqlDbType.NVarChar).Value = city.CityName;
            cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = city.StateID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
