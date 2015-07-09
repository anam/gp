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

public class SqlInv_WastageProvider:DataAccessObject
{
	public SqlInv_WastageProvider()
    {
    }


    public bool DeleteInv_Wastage(int inv_WastageID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeleteInv_Wastage", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_WastageID", SqlDbType.Int).Value = inv_WastageID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Inv_Wastage> GetAllInv_Wastages()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_Wastages", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_WastagesFromReader(reader);
        }
    }
    public List<Inv_Wastage> GetInv_WastagesFromReader(IDataReader reader)
    {
        List<Inv_Wastage> inv_Wastages = new List<Inv_Wastage>();

        while (reader.Read())
        {
            inv_Wastages.Add(GetInv_WastageFromReader(reader));
        }
        return inv_Wastages;
    }

    public Inv_Wastage GetInv_WastageFromReader(IDataReader reader)
    {
        try
        {
            Inv_Wastage inv_Wastage = new Inv_Wastage
                (
                    (int)reader["Inv_WastageID"],
                    (DateTime)reader["WastageDate"],
                    reader["IssueIDs"].ToString(),
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
             return inv_Wastage;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Inv_Wastage GetInv_WastageByID(int inv_WastageID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetInv_WastageByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Inv_WastageID", SqlDbType.Int).Value = inv_WastageID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetInv_WastageFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertInv_Wastage(Inv_Wastage inv_Wastage)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertInv_Wastage", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_WastageID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@WastageDate", SqlDbType.DateTime).Value = inv_Wastage.WastageDate;
            cmd.Parameters.Add("@IssueIDs", SqlDbType.NVarChar).Value = inv_Wastage.IssueIDs;
            cmd.Parameters.Add("@WorkSatationID", SqlDbType.Int).Value = inv_Wastage.WorkSatationID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = inv_Wastage.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_Wastage.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_Wastage.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_Wastage.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_Wastage.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_Wastage.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_Wastage.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_Wastage.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_Wastage.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_Wastage.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Inv_WastageID"].Value;
        }
    }

    public bool UpdateInv_Wastage(Inv_Wastage inv_Wastage)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdateInv_Wastage", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_WastageID", SqlDbType.Int).Value = inv_Wastage.Inv_WastageID;
            cmd.Parameters.Add("@WastageDate", SqlDbType.DateTime).Value = inv_Wastage.WastageDate;
            cmd.Parameters.Add("@IssueIDs", SqlDbType.NVarChar).Value = inv_Wastage.IssueIDs;
            cmd.Parameters.Add("@WorkSatationID", SqlDbType.Int).Value = inv_Wastage.WorkSatationID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = inv_Wastage.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_Wastage.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_Wastage.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_Wastage.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_Wastage.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_Wastage.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_Wastage.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_Wastage.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_Wastage.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_Wastage.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
