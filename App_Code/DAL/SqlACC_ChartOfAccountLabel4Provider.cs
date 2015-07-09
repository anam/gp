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

public class SqlACC_ChartOfAccountLabel4Provider:DataAccessObject
{
	public SqlACC_ChartOfAccountLabel4Provider()
    {
    }


    public bool DeleteACC_ChartOfAccountLabel4(int aCC_ChartOfAccountLabel4ID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeleteACC_ChartOfAccountLabel4", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACC_ChartOfAccountLabel4ID", SqlDbType.Int).Value = aCC_ChartOfAccountLabel4ID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<ACC_ChartOfAccountLabel4> GetAllACC_ChartOfAccountLabel4s()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_ChartOfAccountLabel4s", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_ChartOfAccountLabel4sFromReader(reader);
        }
    }


    public List<ACC_ChartOfAccountLabel4> GetAllACC_ChartOfAccountLabel4sForJournalEntry()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_ChartOfAccountLabel4sForJournalEntry", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_ChartOfAccountLabel4sFromReader(reader);
        }
    }



    public List<ACC_ChartOfAccountLabel4> GetAllACC_ChartOfAccountLabel4sForJournalEntryVisibleOnly()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_ChartOfAccountLabel4sForJournalEntryVisibleOnly", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_ChartOfAccountLabel4sFromReader(reader);
        }
    }

    public List<ACC_ChartOfAccountLabel4> GetACC_ChartOfAccountLabel4sFromReader(IDataReader reader)
    {
        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();

        while (reader.Read())
        {
            aCC_ChartOfAccountLabel4s.Add(GetACC_ChartOfAccountLabel4FromReader(reader));
        }
        return aCC_ChartOfAccountLabel4s;
    }

    public ACC_ChartOfAccountLabel4 GetACC_ChartOfAccountLabel4FromReader(IDataReader reader)
    {
        try
        {
            ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 = new ACC_ChartOfAccountLabel4
                (
                    (int)reader["ACC_ChartOfAccountLabel4ID"],
                    reader["Code"].ToString(),
                    (int)reader["ACC_HeadTypeID"],
                    reader["ChartOfAccountLabel4Text"].ToString(),
                    reader["ExtraField1"].ToString(),
                    reader["ExtraField2"].ToString(),
                    reader["ExtraField3"].ToString(),
                    (int)reader["AddedBy"],
                    (DateTime)reader["AddedDate"],
                    (int)reader["UpdatedBy"],
                    (DateTime)reader["UpdatedDate"],
                    (int)reader["RowStatusID"]
                );
             return aCC_ChartOfAccountLabel4;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public ACC_ChartOfAccountLabel4 GetACC_ChartOfAccountLabel4ByID(int aCC_ChartOfAccountLabel4ID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetACC_ChartOfAccountLabel4ByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ACC_ChartOfAccountLabel4ID", SqlDbType.Int).Value = aCC_ChartOfAccountLabel4ID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetACC_ChartOfAccountLabel4FromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertACC_ChartOfAccountLabel4(ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertACC_ChartOfAccountLabel4", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACC_ChartOfAccountLabel4ID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Code", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel4.Code;
            cmd.Parameters.Add("@ACC_HeadTypeID", SqlDbType.Int).Value = aCC_ChartOfAccountLabel4.ACC_HeadTypeID;
            cmd.Parameters.Add("@ChartOfAccountLabel4Text", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel4.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel4.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel4.ExtraField3;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = aCC_ChartOfAccountLabel4.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = aCC_ChartOfAccountLabel4.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = aCC_ChartOfAccountLabel4.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = aCC_ChartOfAccountLabel4.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = aCC_ChartOfAccountLabel4.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@ACC_ChartOfAccountLabel4ID"].Value;
        }
    }

    public bool UpdateACC_ChartOfAccountLabel4(ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdateACC_ChartOfAccountLabel4", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACC_ChartOfAccountLabel4ID", SqlDbType.Int).Value = aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID;
            cmd.Parameters.Add("@Code", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel4.Code;
            cmd.Parameters.Add("@ACC_HeadTypeID", SqlDbType.Int).Value = aCC_ChartOfAccountLabel4.ACC_HeadTypeID;
            cmd.Parameters.Add("@ChartOfAccountLabel4Text", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel4.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel4.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel4.ExtraField3;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = aCC_ChartOfAccountLabel4.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = aCC_ChartOfAccountLabel4.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = aCC_ChartOfAccountLabel4.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = aCC_ChartOfAccountLabel4.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = aCC_ChartOfAccountLabel4.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
