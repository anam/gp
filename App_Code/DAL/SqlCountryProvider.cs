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

public class SqlCountryProvider:DataAccessObject
{
	public SqlCountryProvider()
    {
    }


    public bool DeleteCountry(int countryID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteCountry", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = countryID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Country> GetAllCountries()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllCountries", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetCountriesFromReader(reader);
        }
    }
    public List<Country> GetCountriesFromReader(IDataReader reader)
    {
        List<Country> countries = new List<Country>();

        while (reader.Read())
        {
            countries.Add(GetCountryFromReader(reader));
        }
        return countries;
    }

    public Country GetCountryFromReader(IDataReader reader)
    {
        try
        {
            Country country = new Country
                (
                    (int)reader["CountryID"],
                    reader["CountryName"].ToString()
                );
             return country;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Country GetCountryByID(int countryID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetCountryByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CountryID", SqlDbType.Int).Value = countryID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetCountryFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertCountry(Country country)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertCountry", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@CountryName", SqlDbType.NVarChar).Value = country.CountryName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@CountryID"].Value;
        }
    }

    public bool UpdateCountry(Country country)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateCountry", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = country.CountryID;
            cmd.Parameters.Add("@CountryName", SqlDbType.NVarChar).Value = country.CountryName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
