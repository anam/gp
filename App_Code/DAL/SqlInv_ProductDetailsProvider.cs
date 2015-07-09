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

public class SqlInv_ProductDetailsProvider:DataAccessObject
{
	public SqlInv_ProductDetailsProvider()
    {
    }


    public bool DeleteInv_ProductDetails(int inv_ProductDetailsID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeleteInv_ProductDetails", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_ProductDetailsID", SqlDbType.Int).Value = inv_ProductDetailsID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Inv_ProductDetails> GetAllInv_ProductDetailss()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_ProductDetailss", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_ProductDetailssFromReader(reader);
        }
    }
    public List<Inv_ProductDetails> GetInv_ProductDetailssFromReader(IDataReader reader)
    {
        List<Inv_ProductDetails> inv_ProductDetailss = new List<Inv_ProductDetails>();

        while (reader.Read())
        {
            inv_ProductDetailss.Add(GetInv_ProductDetailsFromReader(reader));
        }
        return inv_ProductDetailss;
    }

    public Inv_ProductDetails GetInv_ProductDetailsFromReader(IDataReader reader)
    {
        try
        {
            Inv_ProductDetails inv_ProductDetails = new Inv_ProductDetails
                (
                    (int)reader["Inv_ProductDetailsID"],
                    (int)reader["ProductID"],
                    (int)reader["ItemID"],
                    (decimal)reader["Costing"],
                    (decimal)reader["QuantityProduced"],
                    (decimal)reader["QuantityUtilized"],
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
             return inv_ProductDetails;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Inv_ProductDetails GetInv_ProductDetailsByID(int inv_ProductDetailsID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetInv_ProductDetailsByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Inv_ProductDetailsID", SqlDbType.Int).Value = inv_ProductDetailsID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetInv_ProductDetailsFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertInv_ProductDetails(Inv_ProductDetails inv_ProductDetails)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertInv_ProductDetails", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_ProductDetailsID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = inv_ProductDetails.ProductID;
            cmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = inv_ProductDetails.ItemID;
            cmd.Parameters.Add("@Costing", SqlDbType.Decimal).Value = inv_ProductDetails.Costing;
            cmd.Parameters.Add("@QuantityProduced", SqlDbType.Decimal).Value = inv_ProductDetails.QuantityProduced;
            cmd.Parameters.Add("@QuantityUtilized", SqlDbType.Decimal).Value = inv_ProductDetails.QuantityUtilized;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_ProductDetails.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_ProductDetails.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_ProductDetails.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_ProductDetails.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_ProductDetails.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_ProductDetails.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_ProductDetails.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_ProductDetails.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_ProductDetails.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Inv_ProductDetailsID"].Value;
        }
    }

    public bool UpdateInv_ProductDetails(Inv_ProductDetails inv_ProductDetails)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdateInv_ProductDetails", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_ProductDetailsID", SqlDbType.Int).Value = inv_ProductDetails.Inv_ProductDetailsID;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = inv_ProductDetails.ProductID;
            cmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = inv_ProductDetails.ItemID;
            cmd.Parameters.Add("@Costing", SqlDbType.Decimal).Value = inv_ProductDetails.Costing;
            cmd.Parameters.Add("@QuantityProduced", SqlDbType.Decimal).Value = inv_ProductDetails.QuantityProduced;
            cmd.Parameters.Add("@QuantityUtilized", SqlDbType.Decimal).Value = inv_ProductDetails.QuantityUtilized;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_ProductDetails.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_ProductDetails.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_ProductDetails.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_ProductDetails.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_ProductDetails.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_ProductDetails.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_ProductDetails.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_ProductDetails.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_ProductDetails.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
