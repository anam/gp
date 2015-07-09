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

public class SqlInv_UtilizationProvider:DataAccessObject
{
	public SqlInv_UtilizationProvider()
    {
    }


    public bool DeleteInv_Utilization(int inv_UtilizationID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeleteInv_Utilization", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_UtilizationID", SqlDbType.Int).Value = inv_UtilizationID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Inv_Utilization> GetAllInv_Utilizations()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_Utilizations", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_UtilizationsFromReader(reader);
        }
    }
    public List<Inv_Utilization> GetInv_UtilizationsFromReader(IDataReader reader)
    {
        List<Inv_Utilization> inv_Utilizations = new List<Inv_Utilization>();

        while (reader.Read())
        {
            inv_Utilizations.Add(GetInv_UtilizationFromReader(reader));
        }
        return inv_Utilizations;
    }

    public Inv_Utilization GetInv_UtilizationFromReader(IDataReader reader)
    {
        try
        {
            Inv_Utilization inv_Utilization = new Inv_Utilization
                (
                    (int)reader["Inv_UtilizationID"],
                    (DateTime)reader["UtilizationDate"],
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
             return inv_Utilization;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Inv_Utilization GetInv_UtilizationByID(int inv_UtilizationID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetInv_UtilizationByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Inv_UtilizationID", SqlDbType.Int).Value = inv_UtilizationID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetInv_UtilizationFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertInv_Utilization(Inv_Utilization inv_Utilization)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertInv_Utilization", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_UtilizationID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@UtilizationDate", SqlDbType.DateTime).Value = inv_Utilization.UtilizationDate;
            cmd.Parameters.Add("@IssueIDs", SqlDbType.NVarChar).Value = inv_Utilization.IssueIDs;
            cmd.Parameters.Add("@WorkSatationID", SqlDbType.Int).Value = inv_Utilization.WorkSatationID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = inv_Utilization.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_Utilization.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_Utilization.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_Utilization.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_Utilization.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_Utilization.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_Utilization.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_Utilization.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_Utilization.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_Utilization.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Inv_UtilizationID"].Value;
        }
    }

    public bool UpdateInv_Utilization(Inv_Utilization inv_Utilization)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdateInv_Utilization", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_UtilizationID", SqlDbType.Int).Value = inv_Utilization.Inv_UtilizationID;
            cmd.Parameters.Add("@UtilizationDate", SqlDbType.DateTime).Value = inv_Utilization.UtilizationDate;
            cmd.Parameters.Add("@IssueIDs", SqlDbType.NVarChar).Value = inv_Utilization.IssueIDs;
            cmd.Parameters.Add("@WorkSatationID", SqlDbType.Int).Value = inv_Utilization.WorkSatationID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = inv_Utilization.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_Utilization.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_Utilization.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_Utilization.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_Utilization.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_Utilization.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_Utilization.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_Utilization.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_Utilization.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_Utilization.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
