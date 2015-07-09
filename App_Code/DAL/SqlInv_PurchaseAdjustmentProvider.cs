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

public class SqlInv_PurchaseAdjustmentProvider:DataAccessObject
{
	public SqlInv_PurchaseAdjustmentProvider()
    {
    }


    public bool DeleteInv_PurchaseAdjustment(int inv_PurchaseAdjustmentID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeleteInv_PurchaseAdjustment", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_PurchaseAdjustmentID", SqlDbType.Int).Value = inv_PurchaseAdjustmentID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Inv_PurchaseAdjustment> GetAllInv_PurchaseAdjustments()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_PurchaseAdjustments", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_PurchaseAdjustmentsFromReader(reader);
        }
    }
    public List<Inv_PurchaseAdjustment> GetInv_PurchaseAdjustmentsFromReader(IDataReader reader)
    {
        List<Inv_PurchaseAdjustment> inv_PurchaseAdjustments = new List<Inv_PurchaseAdjustment>();

        while (reader.Read())
        {
            inv_PurchaseAdjustments.Add(GetInv_PurchaseAdjustmentFromReader(reader));
        }
        return inv_PurchaseAdjustments;
    }

    public Inv_PurchaseAdjustment GetInv_PurchaseAdjustmentFromReader(IDataReader reader)
    {
        try
        {
            Inv_PurchaseAdjustment inv_PurchaseAdjustment = new Inv_PurchaseAdjustment
                (
                    (int)reader["Inv_PurchaseAdjustmentID"],
                    (DateTime)reader["PurchseAdjustmentDate"],
                    reader["PurchaseIDs"].ToString(),
                    (int)reader["WorkSatationID"],
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
             return inv_PurchaseAdjustment;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Inv_PurchaseAdjustment GetInv_PurchaseAdjustmentByID(int inv_PurchaseAdjustmentID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetInv_PurchaseAdjustmentByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Inv_PurchaseAdjustmentID", SqlDbType.Int).Value = inv_PurchaseAdjustmentID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetInv_PurchaseAdjustmentFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertInv_PurchaseAdjustment(Inv_PurchaseAdjustment inv_PurchaseAdjustment)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertInv_PurchaseAdjustment", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_PurchaseAdjustmentID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@PurchseAdjustmentDate", SqlDbType.DateTime).Value = inv_PurchaseAdjustment.PurchseAdjustmentDate;
            cmd.Parameters.Add("@PurchaseIDs", SqlDbType.NVarChar).Value = inv_PurchaseAdjustment.PurchaseIDs;
            cmd.Parameters.Add("@WorkSatationID", SqlDbType.Int).Value = inv_PurchaseAdjustment.WorkSatationID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = inv_PurchaseAdjustment.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_PurchaseAdjustment.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_PurchaseAdjustment.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_PurchaseAdjustment.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_PurchaseAdjustment.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_PurchaseAdjustment.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_PurchaseAdjustment.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_PurchaseAdjustment.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_PurchaseAdjustment.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_PurchaseAdjustment.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Inv_PurchaseAdjustmentID"].Value;
        }
    }

    public bool UpdateInv_PurchaseAdjustment(Inv_PurchaseAdjustment inv_PurchaseAdjustment)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdateInv_PurchaseAdjustment", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_PurchaseAdjustmentID", SqlDbType.Int).Value = inv_PurchaseAdjustment.Inv_PurchaseAdjustmentID;
            cmd.Parameters.Add("@PurchseAdjustmentDate", SqlDbType.DateTime).Value = inv_PurchaseAdjustment.PurchseAdjustmentDate;
            cmd.Parameters.Add("@PurchaseIDs", SqlDbType.NVarChar).Value = inv_PurchaseAdjustment.PurchaseIDs;
            cmd.Parameters.Add("@WorkSatationID", SqlDbType.Int).Value = inv_PurchaseAdjustment.WorkSatationID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = inv_PurchaseAdjustment.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_PurchaseAdjustment.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_PurchaseAdjustment.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_PurchaseAdjustment.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_PurchaseAdjustment.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_PurchaseAdjustment.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_PurchaseAdjustment.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_PurchaseAdjustment.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_PurchaseAdjustment.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_PurchaseAdjustment.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
