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

public class SqlPos_SizeProductProvider:DataAccessObject
{
	public SqlPos_SizeProductProvider()
    {
    }


    public bool DeletePos_SizeProduct(int pos_SizeProductID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeletePos_SizeProduct", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_SizeProductID", SqlDbType.Int).Value = pos_SizeProductID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Pos_SizeProduct> GetAllPos_SizeProducts()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllPos_SizeProducts", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetPos_SizeProductsFromReader(reader);
        }
    }
    public List<Pos_SizeProduct> GetPos_SizeProductsFromReader(IDataReader reader)
    {
        List<Pos_SizeProduct> pos_SizeProducts = new List<Pos_SizeProduct>();

        while (reader.Read())
        {
            pos_SizeProducts.Add(GetPos_SizeProductFromReader(reader));
        }
        return pos_SizeProducts;
    }

    public Pos_SizeProduct GetPos_SizeProductFromReader(IDataReader reader)
    {
        try
        {
            Pos_SizeProduct pos_SizeProduct = new Pos_SizeProduct
                (
                    (int)reader["Pos_SizeProductID"],
                    (int)reader["Pos_SizeID"],
                    (int)reader["ProductID"]
                );
             return pos_SizeProduct;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Pos_SizeProduct GetPos_SizeProductByID(int pos_SizeProductID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetPos_SizeProductByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Pos_SizeProductID", SqlDbType.Int).Value = pos_SizeProductID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetPos_SizeProductFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertPos_SizeProduct(Pos_SizeProduct pos_SizeProduct)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertPos_SizeProduct", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_SizeProductID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Pos_SizeID", SqlDbType.Int).Value = pos_SizeProduct.Pos_SizeID;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = pos_SizeProduct.ProductID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Pos_SizeProductID"].Value;
        }
    }

    public bool UpdatePos_SizeProduct(Pos_SizeProduct pos_SizeProduct)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdatePos_SizeProduct", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_SizeProductID", SqlDbType.Int).Value = pos_SizeProduct.Pos_SizeProductID;
            cmd.Parameters.Add("@Pos_SizeID", SqlDbType.Int).Value = pos_SizeProduct.Pos_SizeID;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = pos_SizeProduct.ProductID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
