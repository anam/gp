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

public class SqlInv_IssueMasterReturnProvider:DataAccessObject
{
	public SqlInv_IssueMasterReturnProvider()
    {
    }


    public bool DeleteInv_IssueMasterReturn(int inv_IssueMasterReturnID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeleteInv_IssueMasterReturn", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_IssueMasterReturnID", SqlDbType.Int).Value = inv_IssueMasterReturnID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Inv_IssueMasterReturn> GetAllInv_IssueMasterReturns()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_IssueMasterReturns", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_IssueMasterReturnsFromReader(reader);
        }
    }
    public List<Inv_IssueMasterReturn> GetInv_IssueMasterReturnsFromReader(IDataReader reader)
    {
        List<Inv_IssueMasterReturn> inv_IssueMasterReturns = new List<Inv_IssueMasterReturn>();

        while (reader.Read())
        {
            inv_IssueMasterReturns.Add(GetInv_IssueMasterReturnFromReader(reader));
        }
        return inv_IssueMasterReturns;
    }

    public Inv_IssueMasterReturn GetInv_IssueMasterReturnFromReader(IDataReader reader)
    {
        try
        {
            Inv_IssueMasterReturn inv_IssueMasterReturn = new Inv_IssueMasterReturn
                (
                    (int)reader["Inv_IssueMasterReturnID"],
                    reader["IssueReturnName"].ToString(),
                    (DateTime)reader["IssueReturnDate"],
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
             return inv_IssueMasterReturn;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Inv_IssueMasterReturn GetInv_IssueMasterReturnByID(int inv_IssueMasterReturnID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetInv_IssueMasterReturnByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Inv_IssueMasterReturnID", SqlDbType.Int).Value = inv_IssueMasterReturnID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetInv_IssueMasterReturnFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertInv_IssueMasterReturn(Inv_IssueMasterReturn inv_IssueMasterReturn)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertInv_IssueMasterReturn", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_IssueMasterReturnID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@IssueReturnName", SqlDbType.NVarChar).Value = inv_IssueMasterReturn.IssueReturnName;
            cmd.Parameters.Add("@IssueReturnDate", SqlDbType.DateTime).Value = inv_IssueMasterReturn.IssueReturnDate;
            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = inv_IssueMasterReturn.EmployeeID;
            cmd.Parameters.Add("@WorkSatationID", SqlDbType.Int).Value = inv_IssueMasterReturn.WorkSatationID;
            cmd.Parameters.Add("@Particulars", SqlDbType.NText).Value = inv_IssueMasterReturn.Particulars;
            cmd.Parameters.Add("@IsIssue", SqlDbType.Bit).Value = inv_IssueMasterReturn.IsIssue;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = inv_IssueMasterReturn.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_IssueMasterReturn.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_IssueMasterReturn.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_IssueMasterReturn.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_IssueMasterReturn.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_IssueMasterReturn.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_IssueMasterReturn.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_IssueMasterReturn.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_IssueMasterReturn.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_IssueMasterReturn.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Inv_IssueMasterReturnID"].Value;
        }
    }

    public bool UpdateInv_IssueMasterReturn(Inv_IssueMasterReturn inv_IssueMasterReturn)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdateInv_IssueMasterReturn", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_IssueMasterReturnID", SqlDbType.Int).Value = inv_IssueMasterReturn.Inv_IssueMasterReturnID;
            cmd.Parameters.Add("@IssueReturnName", SqlDbType.NVarChar).Value = inv_IssueMasterReturn.IssueReturnName;
            cmd.Parameters.Add("@IssueReturnDate", SqlDbType.DateTime).Value = inv_IssueMasterReturn.IssueReturnDate;
            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = inv_IssueMasterReturn.EmployeeID;
            cmd.Parameters.Add("@WorkSatationID", SqlDbType.Int).Value = inv_IssueMasterReturn.WorkSatationID;
            cmd.Parameters.Add("@Particulars", SqlDbType.NText).Value = inv_IssueMasterReturn.Particulars;
            cmd.Parameters.Add("@IsIssue", SqlDbType.Bit).Value = inv_IssueMasterReturn.IsIssue;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = inv_IssueMasterReturn.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_IssueMasterReturn.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_IssueMasterReturn.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_IssueMasterReturn.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_IssueMasterReturn.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_IssueMasterReturn.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_IssueMasterReturn.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_IssueMasterReturn.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_IssueMasterReturn.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_IssueMasterReturn.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
