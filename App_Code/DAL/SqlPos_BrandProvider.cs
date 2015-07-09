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

public class SqlPos_BrandProvider:DataAccessObject
{
	public SqlPos_BrandProvider()
    {
    }


    public bool DeletePos_Brand(int pos_BrandID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeletePos_Brand", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_BrandID", SqlDbType.Int).Value = pos_BrandID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Pos_Brand> GetAllPos_Brands()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllPos_Brands", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetPos_BrandsFromReader(reader);
        }
    }
    public List<Pos_Brand> GetPos_BrandsFromReader(IDataReader reader)
    {
        List<Pos_Brand> pos_Brands = new List<Pos_Brand>();

        while (reader.Read())
        {
            pos_Brands.Add(GetPos_BrandFromReader(reader));
        }
        return pos_Brands;
    }

    public Pos_Brand GetPos_BrandFromReader(IDataReader reader)
    {
        try
        {
            Pos_Brand pos_Brand = new Pos_Brand
                (
                    (int)reader["Pos_BrandID"],
                    reader["BrandName"].ToString(),
                    reader["Details"].ToString()
                );
             return pos_Brand;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Pos_Brand GetPos_BrandByID(int pos_BrandID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetPos_BrandByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Pos_BrandID", SqlDbType.Int).Value = pos_BrandID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetPos_BrandFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertPos_Brand(Pos_Brand pos_Brand)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertPos_Brand", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_BrandID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@BrandName", SqlDbType.NVarChar).Value = pos_Brand.BrandName;
            cmd.Parameters.Add("@Details", SqlDbType.NText).Value = pos_Brand.Details;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Pos_BrandID"].Value;
        }
    }

    public bool UpdatePos_Brand(Pos_Brand pos_Brand)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdatePos_Brand", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_BrandID", SqlDbType.Int).Value = pos_Brand.Pos_BrandID;
            cmd.Parameters.Add("@BrandName", SqlDbType.NVarChar).Value = pos_Brand.BrandName;
            cmd.Parameters.Add("@Details", SqlDbType.NText).Value = pos_Brand.Details;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
