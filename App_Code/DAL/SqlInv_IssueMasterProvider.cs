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

public class SqlInv_IssueMasterProvider:DataAccessObject
{
	public SqlInv_IssueMasterProvider()
    {
    }


    public bool DeleteInv_IssueMaster(int inv_IssueMasterID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeleteInv_IssueMaster", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_IssueMasterID", SqlDbType.Int).Value = inv_IssueMasterID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public bool DeleteInv_IssueMasterAll(int inv_IssueMasterID,int login)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeleteInv_IssueMasterAll", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_IssueMasterID", SqlDbType.Int).Value = inv_IssueMasterID;
            cmd.Parameters.Add("@loginID", SqlDbType.Int).Value = login;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result >= 1);
        }
    }

    public List<Inv_IssueMaster> GetAllInv_IssueMasters()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_IssueMasters", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_IssueMastersFromReader(reader);
        }
    }
    public List<Inv_IssueMaster> GetInv_IssueMastersFromReader(IDataReader reader)
    {
        List<Inv_IssueMaster> inv_IssueMasters = new List<Inv_IssueMaster>();

        while (reader.Read())
        {
            inv_IssueMasters.Add(GetInv_IssueMasterFromReader(reader));
        }
        return inv_IssueMasters;
    }

    public Inv_IssueMaster GetInv_IssueMasterFromReader(IDataReader reader)
    {
        try
        {
            Inv_IssueMaster inv_IssueMaster = new Inv_IssueMaster
                (
                    (int)reader["Inv_IssueMasterID"],
                    reader["IssueName"].ToString(),
                    (DateTime)reader["IssueDate"],
                    (int)reader["EmployeeID"],
                    (int)reader["WorkSatationID"],
                    reader["Particulars"].ToString(),
                    (bool)reader["IsIssue"],
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

             return inv_IssueMaster;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Inv_IssueMaster GetInv_IssueMasterByID(int inv_IssueMasterID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetInv_IssueMasterByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Inv_IssueMasterID", SqlDbType.Int).Value = inv_IssueMasterID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetInv_IssueMasterFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertInv_IssueMaster(Inv_IssueMaster inv_IssueMaster)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertInv_IssueMaster", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_IssueMasterID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@IssueName", SqlDbType.NVarChar).Value = inv_IssueMaster.IssueName;
            cmd.Parameters.Add("@IssueDate", SqlDbType.DateTime).Value = inv_IssueMaster.IssueDate;
            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = inv_IssueMaster.EmployeeID;
            cmd.Parameters.Add("@WorkSatationID", SqlDbType.Int).Value = inv_IssueMaster.WorkSatationID;
            cmd.Parameters.Add("@Particulars", SqlDbType.NText).Value = inv_IssueMaster.Particulars;
            cmd.Parameters.Add("@IsIssue", SqlDbType.Bit).Value = inv_IssueMaster.IsIssue;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = inv_IssueMaster.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_IssueMaster.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_IssueMaster.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_IssueMaster.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_IssueMaster.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_IssueMaster.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_IssueMaster.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_IssueMaster.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_IssueMaster.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_IssueMaster.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Inv_IssueMasterID"].Value;
        }
    }

    public bool UpdateInv_IssueMaster(Inv_IssueMaster inv_IssueMaster)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdateInv_IssueMaster", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_IssueMasterID", SqlDbType.Int).Value = inv_IssueMaster.Inv_IssueMasterID;
            cmd.Parameters.Add("@IssueName", SqlDbType.NVarChar).Value = inv_IssueMaster.IssueName;
            cmd.Parameters.Add("@IssueDate", SqlDbType.DateTime).Value = inv_IssueMaster.IssueDate;
            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = inv_IssueMaster.EmployeeID;
            cmd.Parameters.Add("@WorkSatationID", SqlDbType.Int).Value = inv_IssueMaster.WorkSatationID;
            cmd.Parameters.Add("@Particulars", SqlDbType.NText).Value = inv_IssueMaster.Particulars;
            cmd.Parameters.Add("@IsIssue", SqlDbType.Bit).Value = inv_IssueMaster.IsIssue;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = inv_IssueMaster.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_IssueMaster.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_IssueMaster.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_IssueMaster.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_IssueMaster.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_IssueMaster.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_IssueMaster.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_IssueMaster.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_IssueMaster.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_IssueMaster.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
