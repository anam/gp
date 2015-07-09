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

public class SqlACC_ChartOfAccountLabel3Provider:DataAccessObject
{
	public SqlACC_ChartOfAccountLabel3Provider()
    {
    }


    public bool DeleteACC_ChartOfAccountLabel3(int aCC_ChartOfAccountLabel3ID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeleteACC_ChartOfAccountLabel3", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACC_ChartOfAccountLabel3ID", SqlDbType.Int).Value = aCC_ChartOfAccountLabel3ID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<ACC_ChartOfAccountLabel3> GetAllACC_ChartOfAccountLabel3s()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_ChartOfAccountLabel3s", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_ChartOfAccountLabel3sFromReader(reader);
        }
    }

    public List<ACC_ChartOfAccountLabel3> GetAllACC_ChartOfAccountLabel3sForJournalEntry()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_ChartOfAccountLabel3sForJournalEntry", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_ChartOfAccountLabel3sFromReader(reader);
        }
    }


    public List<ACC_ChartOfAccountLabel3> GetAllACC_ChartOfAccountLabel3sForJournalEntryForDropDownList()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_ChartOfAccountLabel3sForJournalEntryForDropDownList", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_ChartOfAccountLabel3sFromReader(reader);
        }
    }
    public List<ACC_ChartOfAccountLabel3> GetACC_ChartOfAccountLabel3sFromReader(IDataReader reader)
    {
        List<ACC_ChartOfAccountLabel3> aCC_ChartOfAccountLabel3s = new List<ACC_ChartOfAccountLabel3>();

        while (reader.Read())
        {
            aCC_ChartOfAccountLabel3s.Add(GetACC_ChartOfAccountLabel3FromReader(reader));
        }
        return aCC_ChartOfAccountLabel3s;
    }

    public ACC_ChartOfAccountLabel3 GetACC_ChartOfAccountLabel3FromReader(IDataReader reader)
    {
        try
        {
            ACC_ChartOfAccountLabel3 aCC_ChartOfAccountLabel3 = new ACC_ChartOfAccountLabel3
                (
                    (int)reader["ACC_ChartOfAccountLabel3ID"],
                    reader["Code"].ToString(),
                    (int)reader["ACC_ChartOfAccountLabel2ID"],
                    reader["ChartOfAccountLabel3Text"].ToString(),
                    reader["ExtraField1"].ToString(),
                    reader["ExtraField2"].ToString(),
                    reader["ExtraField3"].ToString(),
                    (int)reader["AddedBy"],
                    (DateTime)reader["AddedDate"],
                    (int)reader["UpdatedBy"],
                    (DateTime)reader["UpdatedDate"],
                    (int)reader["RowStatusID"]
                );
             return aCC_ChartOfAccountLabel3;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public ACC_ChartOfAccountLabel3 GetACC_ChartOfAccountLabel3ByID(int aCC_ChartOfAccountLabel3ID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetACC_ChartOfAccountLabel3ByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ACC_ChartOfAccountLabel3ID", SqlDbType.Int).Value = aCC_ChartOfAccountLabel3ID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetACC_ChartOfAccountLabel3FromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertACC_ChartOfAccountLabel3(ACC_ChartOfAccountLabel3 aCC_ChartOfAccountLabel3)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertACC_ChartOfAccountLabel3", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACC_ChartOfAccountLabel3ID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Code", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel3.Code;
            cmd.Parameters.Add("@ACC_ChartOfAccountLabel2ID", SqlDbType.Int).Value = aCC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel2ID;
            cmd.Parameters.Add("@ChartOfAccountLabel3Text", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel3.ChartOfAccountLabel3Text;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel3.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel3.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel3.ExtraField3;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = aCC_ChartOfAccountLabel3.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = aCC_ChartOfAccountLabel3.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = aCC_ChartOfAccountLabel3.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = aCC_ChartOfAccountLabel3.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = aCC_ChartOfAccountLabel3.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@ACC_ChartOfAccountLabel3ID"].Value;
        }
    }

    public bool UpdateACC_ChartOfAccountLabel3(ACC_ChartOfAccountLabel3 aCC_ChartOfAccountLabel3)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdateACC_ChartOfAccountLabel3", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACC_ChartOfAccountLabel3ID", SqlDbType.Int).Value = aCC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel3ID;
            cmd.Parameters.Add("@Code", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel3.Code;
            cmd.Parameters.Add("@ACC_ChartOfAccountLabel2ID", SqlDbType.Int).Value = aCC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel2ID;
            cmd.Parameters.Add("@ChartOfAccountLabel3Text", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel3.ChartOfAccountLabel3Text;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel3.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel3.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel3.ExtraField3;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = aCC_ChartOfAccountLabel3.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = aCC_ChartOfAccountLabel3.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = aCC_ChartOfAccountLabel3.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = aCC_ChartOfAccountLabel3.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = aCC_ChartOfAccountLabel3.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
