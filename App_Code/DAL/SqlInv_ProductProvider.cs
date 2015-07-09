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

public class SqlInv_ProductProvider:DataAccessObject
{
	public SqlInv_ProductProvider()
    {
    }


    public bool DeleteInv_Product(int inv_ProductID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeleteInv_Product", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_ProductID", SqlDbType.Int).Value = inv_ProductID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Inv_Product> GetAllInv_Products()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_Products", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_ProductsFromReader(reader);
        }
    }
    public List<Inv_Product> GetInv_ProductsFromReader(IDataReader reader)
    {
        List<Inv_Product> inv_Products = new List<Inv_Product>();

        while (reader.Read())
        {
            inv_Products.Add(GetInv_ProductFromReader(reader));
        }
        return inv_Products;
    }

    public Inv_Product GetInv_ProductFromReader(IDataReader reader)
    {
        try
        {
            Inv_Product inv_Product = new Inv_Product
                (
                    (int)reader["Inv_ProductID"],
                    (int)reader["ProductID"],
                    (int)reader["ProductCode"],
                    (decimal)reader["AvgCosting"],
                    (decimal)reader["SalePrice"],
                    reader["ExtraField2"].ToString(),
                    reader["ExtraField3"].ToString(),
                    reader["ExtraField4"].ToString(),
                    reader["ExtraField5"].ToString(),
                    (int)reader["AddedBy"],
                    (DateTime)reader["AddedDate"],
                    (int)reader["UpdatedBy"],
                    (DateTime)reader["UpdatedDate"],
                    (int)reader["RowStatusID"]
                );
             return inv_Product;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Inv_Product GetInv_ProductByID(int inv_ProductID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetInv_ProductByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Inv_ProductID", SqlDbType.Int).Value = inv_ProductID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetInv_ProductFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertInv_Product(Inv_Product inv_Product)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertInv_Product", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_ProductID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = inv_Product.ProductID;
            cmd.Parameters.Add("@ProductCode", SqlDbType.Int).Value = inv_Product.ProductCode;
            cmd.Parameters.Add("@AvgCosting", SqlDbType.Decimal).Value = inv_Product.AvgCosting;
            cmd.Parameters.Add("@SalePrice", SqlDbType.Decimal).Value = inv_Product.SalePrice;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_Product.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_Product.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_Product.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_Product.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_Product.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_Product.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_Product.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_Product.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_Product.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Inv_ProductID"].Value;
        }
    }

    public bool UpdateInv_Product(Inv_Product inv_Product)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdateInv_Product", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_ProductID", SqlDbType.Int).Value = inv_Product.Inv_ProductID;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = inv_Product.ProductID;
            cmd.Parameters.Add("@ProductCode", SqlDbType.Int).Value = inv_Product.ProductCode;
            cmd.Parameters.Add("@AvgCosting", SqlDbType.Decimal).Value = inv_Product.AvgCosting;
            cmd.Parameters.Add("@SalePrice", SqlDbType.Decimal).Value = inv_Product.SalePrice;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_Product.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_Product.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_Product.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_Product.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_Product.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_Product.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_Product.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_Product.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_Product.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
