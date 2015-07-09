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

public class SqlPropertyProvider:DataAccessObject
{
	public SqlPropertyProvider()
    {
    }


    public bool DeleteProperty(int propertyID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteProperty", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PropertyID", SqlDbType.Int).Value = propertyID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Property> GetAllProperties()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllProperties", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetPropertiesFromReader(reader);
        }
    }

    public List<Property> GetAllPropertiesSearch(string SearchString)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllPropertiesSearch", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@SearchString", SqlDbType.NVarChar).Value = SearchString;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetPropertiesFromReader(reader);
        }
    }
    public List<Property> GetPropertiesFromReader(IDataReader reader)
    {
        List<Property> properties = new List<Property>();

        while (reader.Read())
        {
            properties.Add(GetPropertyFromReader(reader));
        }
        return properties;
    }

    public Property GetPropertyFromReader(IDataReader reader)
    {
        try
        {
            Property property = new Property
                (
                    (int)reader["PropertyID"],
                    reader["PropertyName"].ToString(),
                    reader["Address"].ToString(),
                    (int)reader["CountryID"],
                    (int)reader["StateID"],
                    (int)reader["CityID"],
                    reader["ExtraField1"].ToString(),
                    reader["ExtraField2"].ToString(),
                    reader["ExtraField3"].ToString(),
                    reader["ExtraField4"].ToString(),
                    reader["ExtraField5"].ToString(),
                    reader["ExtraField6"].ToString(),
                    reader["ExtraField7"].ToString(),
                    reader["ExtraField8"].ToString(),
                    reader["ExtraField9"].ToString(),
                    reader["ExtraField10"].ToString()
                );
            
            try { property.ExtraField3 = reader["StateName"].ToString(); }
            catch (Exception ex) { }

            try { property.ExtraField6 = reader["CompanyName"].ToString(); }
            catch (Exception ex) { }


            try { property.ExtraField4 = decimal.Parse(property.PropertyID.ToString()).ToString("0000"); }
            catch (Exception ex) { }
            
             return property;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Property GetPropertyByID(int propertyID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetPropertyByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@PropertyID", SqlDbType.Int).Value = propertyID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetPropertyFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertProperty(Property property)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertProperty", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PropertyID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@PropertyName", SqlDbType.NVarChar).Value = property.PropertyName;
            cmd.Parameters.Add("@Address", SqlDbType.Text).Value = property.Address;
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = property.CountryID;
            cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = property.StateID;
            cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = property.CityID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = property.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = property.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = property.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = property.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = property.ExtraField5;
            cmd.Parameters.Add("@ExtraField6", SqlDbType.NVarChar).Value = property.ExtraField6;
            cmd.Parameters.Add("@ExtraField7", SqlDbType.NVarChar).Value = property.ExtraField7;
            cmd.Parameters.Add("@ExtraField8", SqlDbType.NVarChar).Value = property.ExtraField8;
            cmd.Parameters.Add("@ExtraField9", SqlDbType.NVarChar).Value = property.ExtraField9;
            cmd.Parameters.Add("@ExtraField10", SqlDbType.NVarChar).Value = property.ExtraField10;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@PropertyID"].Value;
        }
    }

    public bool UpdateProperty(Property property)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateProperty", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PropertyID", SqlDbType.Int).Value = property.PropertyID;
            cmd.Parameters.Add("@PropertyName", SqlDbType.NVarChar).Value = property.PropertyName;
            cmd.Parameters.Add("@Address", SqlDbType.Text).Value = property.Address;
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = property.CountryID;
            cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = property.StateID;
            cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = property.CityID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = property.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = property.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = property.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = property.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = property.ExtraField5;
            cmd.Parameters.Add("@ExtraField6", SqlDbType.NVarChar).Value = property.ExtraField6;
            cmd.Parameters.Add("@ExtraField7", SqlDbType.NVarChar).Value = property.ExtraField7;
            cmd.Parameters.Add("@ExtraField8", SqlDbType.NVarChar).Value = property.ExtraField8;
            cmd.Parameters.Add("@ExtraField9", SqlDbType.NVarChar).Value = property.ExtraField9;
            cmd.Parameters.Add("@ExtraField10", SqlDbType.NVarChar).Value = property.ExtraField10;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
