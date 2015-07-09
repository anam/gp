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

public class SqlInv_UtilizationDetailsProvider:DataAccessObject
{
	public SqlInv_UtilizationDetailsProvider()
    {
    }


    public bool DeleteInv_UtilizationDetails(int inv_UtilizationDetailsID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeleteInv_UtilizationDetails", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_UtilizationDetailsID", SqlDbType.Int).Value = inv_UtilizationDetailsID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Inv_UtilizationDetails> GetAllInv_UtilizationDetailss()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_UtilizationDetailss", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_UtilizationDetailssFromReader(reader);
        }
    }
    public List<Inv_UtilizationDetails> GetInv_UtilizationDetailssFromReader(IDataReader reader)
    {
        List<Inv_UtilizationDetails> inv_UtilizationDetailss = new List<Inv_UtilizationDetails>();

        while (reader.Read())
        {
            inv_UtilizationDetailss.Add(GetInv_UtilizationDetailsFromReader(reader));
        }
        return inv_UtilizationDetailss;
    }

    public Inv_UtilizationDetails GetInv_UtilizationDetailsFromReader(IDataReader reader)
    {
        try
        {
            Inv_UtilizationDetails inv_UtilizationDetails = new Inv_UtilizationDetails
                (
                    (int)reader["Inv_UtilizationDetailsID"],
                    (int)reader["Pos_SizeID"],
                    (int)reader["ProductID"],
                    (int)reader["Inv_ItemID"],
                    (int)reader["Inv_UtilizationID"],
                    (int)reader["Inv_ItemTransactionID"],
                    (decimal)reader["FabricsCost"],
                    (decimal)reader["AccesoriesCost"],
                    (decimal)reader["Overhead"],
                    (decimal)reader["OthersCost"],
                    (decimal)reader["ProductionQuantity"],
                    (decimal)reader["ProcessedQuantity"],
                    (bool)reader["IsReject"],
                    reader["ExtraField1"].ToString(),
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
             return inv_UtilizationDetails;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Inv_UtilizationDetails GetInv_UtilizationDetailsByID(int inv_UtilizationDetailsID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetInv_UtilizationDetailsByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Inv_UtilizationDetailsID", SqlDbType.Int).Value = inv_UtilizationDetailsID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetInv_UtilizationDetailsFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertInv_UtilizationDetails(Inv_UtilizationDetails inv_UtilizationDetails)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertInv_UtilizationDetails", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_UtilizationDetailsID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Pos_SizeID", SqlDbType.Int).Value = inv_UtilizationDetails.Pos_SizeID;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = inv_UtilizationDetails.ProductID;
            cmd.Parameters.Add("@Inv_ItemID", SqlDbType.Int).Value = inv_UtilizationDetails.Inv_ItemID;
            cmd.Parameters.Add("@Inv_UtilizationID", SqlDbType.Int).Value = inv_UtilizationDetails.Inv_UtilizationID;
            cmd.Parameters.Add("@Inv_ItemTransactionID", SqlDbType.Int).Value = inv_UtilizationDetails.Inv_ItemTransactionID;
            cmd.Parameters.Add("@FabricsCost", SqlDbType.Decimal).Value = inv_UtilizationDetails.FabricsCost;
            cmd.Parameters.Add("@AccesoriesCost", SqlDbType.Decimal).Value = inv_UtilizationDetails.AccesoriesCost;
            cmd.Parameters.Add("@Overhead", SqlDbType.Decimal).Value = inv_UtilizationDetails.Overhead;
            cmd.Parameters.Add("@OthersCost", SqlDbType.Decimal).Value = inv_UtilizationDetails.OthersCost;
            cmd.Parameters.Add("@ProductionQuantity", SqlDbType.Decimal).Value = inv_UtilizationDetails.ProductionQuantity;
            cmd.Parameters.Add("@ProcessedQuantity", SqlDbType.Decimal).Value = inv_UtilizationDetails.ProcessedQuantity;
            cmd.Parameters.Add("@IsReject", SqlDbType.Bit).Value = inv_UtilizationDetails.IsReject;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = inv_UtilizationDetails.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_UtilizationDetails.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_UtilizationDetails.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_UtilizationDetails.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_UtilizationDetails.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_UtilizationDetails.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_UtilizationDetails.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_UtilizationDetails.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_UtilizationDetails.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_UtilizationDetails.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Inv_UtilizationDetailsID"].Value;
        }
    }

    public bool UpdateInv_UtilizationDetails(Inv_UtilizationDetails inv_UtilizationDetails)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdateInv_UtilizationDetails", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_UtilizationDetailsID", SqlDbType.Int).Value = inv_UtilizationDetails.Inv_UtilizationDetailsID;
            cmd.Parameters.Add("@Pos_SizeID", SqlDbType.Int).Value = inv_UtilizationDetails.Pos_SizeID;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = inv_UtilizationDetails.ProductID;
            cmd.Parameters.Add("@Inv_ItemID", SqlDbType.Int).Value = inv_UtilizationDetails.Inv_ItemID;
            cmd.Parameters.Add("@Inv_UtilizationID", SqlDbType.Int).Value = inv_UtilizationDetails.Inv_UtilizationID;
            cmd.Parameters.Add("@Inv_ItemTransactionID", SqlDbType.Int).Value = inv_UtilizationDetails.Inv_ItemTransactionID;
            cmd.Parameters.Add("@FabricsCost", SqlDbType.Decimal).Value = inv_UtilizationDetails.FabricsCost;
            cmd.Parameters.Add("@AccesoriesCost", SqlDbType.Decimal).Value = inv_UtilizationDetails.AccesoriesCost;
            cmd.Parameters.Add("@Overhead", SqlDbType.Decimal).Value = inv_UtilizationDetails.Overhead;
            cmd.Parameters.Add("@OthersCost", SqlDbType.Decimal).Value = inv_UtilizationDetails.OthersCost;
            cmd.Parameters.Add("@ProductionQuantity", SqlDbType.Decimal).Value = inv_UtilizationDetails.ProductionQuantity;
            cmd.Parameters.Add("@ProcessedQuantity", SqlDbType.Decimal).Value = inv_UtilizationDetails.ProcessedQuantity;
            cmd.Parameters.Add("@IsReject", SqlDbType.Bit).Value = inv_UtilizationDetails.IsReject;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = inv_UtilizationDetails.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_UtilizationDetails.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_UtilizationDetails.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_UtilizationDetails.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_UtilizationDetails.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_UtilizationDetails.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_UtilizationDetails.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_UtilizationDetails.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_UtilizationDetails.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_UtilizationDetails.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
