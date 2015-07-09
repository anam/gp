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

public class SqlInv_PurchaseReturenProvider:DataAccessObject
{
	public SqlInv_PurchaseReturenProvider()
    {
    }


    public bool DeleteInv_PurchaseReturen(int inv_PurchaseReturenID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeleteInv_PurchaseReturen", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_PurchaseReturenID", SqlDbType.Int).Value = inv_PurchaseReturenID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Inv_PurchaseReturen> GetAllInv_PurchaseReturens()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_PurchaseReturens", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_PurchaseReturensFromReader(reader);
        }
    }

    public List<Inv_PurchaseReturen> GetAllInv_PurchaseReturensByDateNSupplierID( string sql)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_PurchaseReturensByDateNSupplierID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@SQL", SqlDbType.NVarChar).Value = sql;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_PurchaseReturensFromReader(reader);
        }
    }

    public List<Inv_PurchaseReturen> GetInv_PurchaseReturensFromReader(IDataReader reader)
    {
        List<Inv_PurchaseReturen> inv_PurchaseReturens = new List<Inv_PurchaseReturen>();

        while (reader.Read())
        {
            inv_PurchaseReturens.Add(GetInv_PurchaseReturenFromReader(reader));
        }
        return inv_PurchaseReturens;
    }

    public Inv_PurchaseReturen GetInv_PurchaseReturenFromReader(IDataReader reader)
    {
        try
        {
            Inv_PurchaseReturen inv_PurchaseReturen = new Inv_PurchaseReturen
                (
                    (int)reader["Inv_PurchaseReturenID"],
                    (DateTime)reader["PurchseReturenDate"],
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
             return inv_PurchaseReturen;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Inv_PurchaseReturen GetInv_PurchaseReturenByID(int inv_PurchaseReturenID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetInv_PurchaseReturenByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Inv_PurchaseReturenID", SqlDbType.Int).Value = inv_PurchaseReturenID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetInv_PurchaseReturenFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertInv_PurchaseReturen(Inv_PurchaseReturen inv_PurchaseReturen)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertInv_PurchaseReturen", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_PurchaseReturenID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@PurchseReturenDate", SqlDbType.DateTime).Value = inv_PurchaseReturen.PurchseReturenDate;
            cmd.Parameters.Add("@PurchaseIDs", SqlDbType.NVarChar).Value = inv_PurchaseReturen.PurchaseIDs;
            cmd.Parameters.Add("@WorkSatationID", SqlDbType.Int).Value = inv_PurchaseReturen.WorkSatationID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = inv_PurchaseReturen.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_PurchaseReturen.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_PurchaseReturen.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_PurchaseReturen.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_PurchaseReturen.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_PurchaseReturen.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_PurchaseReturen.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_PurchaseReturen.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_PurchaseReturen.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_PurchaseReturen.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Inv_PurchaseReturenID"].Value;
        }
    }

    public bool UpdateInv_PurchaseReturen(Inv_PurchaseReturen inv_PurchaseReturen)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdateInv_PurchaseReturen", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_PurchaseReturenID", SqlDbType.Int).Value = inv_PurchaseReturen.Inv_PurchaseReturenID;
            cmd.Parameters.Add("@PurchseReturenDate", SqlDbType.DateTime).Value = inv_PurchaseReturen.PurchseReturenDate;
            cmd.Parameters.Add("@PurchaseIDs", SqlDbType.NVarChar).Value = inv_PurchaseReturen.PurchaseIDs;
            cmd.Parameters.Add("@WorkSatationID", SqlDbType.Int).Value = inv_PurchaseReturen.WorkSatationID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = inv_PurchaseReturen.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_PurchaseReturen.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_PurchaseReturen.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_PurchaseReturen.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_PurchaseReturen.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_PurchaseReturen.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_PurchaseReturen.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_PurchaseReturen.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_PurchaseReturen.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_PurchaseReturen.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
