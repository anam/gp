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

public class SqlACC_ChartOfAccountLabel2Provider:DataAccessObject
{
	public SqlACC_ChartOfAccountLabel2Provider()
    {
    }


    public bool DeleteACC_ChartOfAccountLabel2(int aCC_ChartOfAccountLabel2ID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeleteACC_ChartOfAccountLabel2", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACC_ChartOfAccountLabel2ID", SqlDbType.Int).Value = aCC_ChartOfAccountLabel2ID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<ACC_ChartOfAccountLabel2> GetAllACC_ChartOfAccountLabel2s()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_ChartOfAccountLabel2s", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_ChartOfAccountLabel2sFromReader(reader);
        }
    }
    public List<ACC_ChartOfAccountLabel2> GetACC_ChartOfAccountLabel2sFromReader(IDataReader reader)
    {
        List<ACC_ChartOfAccountLabel2> aCC_ChartOfAccountLabel2s = new List<ACC_ChartOfAccountLabel2>();

        while (reader.Read())
        {
            aCC_ChartOfAccountLabel2s.Add(GetACC_ChartOfAccountLabel2FromReader(reader));
        }
        return aCC_ChartOfAccountLabel2s;
    }

    public ACC_ChartOfAccountLabel2 GetACC_ChartOfAccountLabel2FromReader(IDataReader reader)
    {
        try
        {
            ACC_ChartOfAccountLabel2 aCC_ChartOfAccountLabel2 = new ACC_ChartOfAccountLabel2
                (
                    (int)reader["ACC_ChartOfAccountLabel2ID"],
                    reader["Code"].ToString(),
                    (int)reader["ACC_ChartOfAccountLabel1ID"],
                    reader["ChartOfAccountLabel2Text"].ToString(),
                    reader["ExtraField1"].ToString(),
                    reader["ExtraField2"].ToString(),
                    reader["ExtraField3"].ToString(),
                    (int)reader["AddedBy"],
                    (DateTime)reader["AddedDate"],
                    (int)reader["UpdatedBy"],
                    (DateTime)reader["UpdatedDate"],
                    (int)reader["RowStatusID"]
                );
             return aCC_ChartOfAccountLabel2;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public ACC_ChartOfAccountLabel2 GetACC_ChartOfAccountLabel2ByID(int aCC_ChartOfAccountLabel2ID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetACC_ChartOfAccountLabel2ByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ACC_ChartOfAccountLabel2ID", SqlDbType.Int).Value = aCC_ChartOfAccountLabel2ID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetACC_ChartOfAccountLabel2FromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertACC_ChartOfAccountLabel2(ACC_ChartOfAccountLabel2 aCC_ChartOfAccountLabel2)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertACC_ChartOfAccountLabel2", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACC_ChartOfAccountLabel2ID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Code", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel2.Code;
            cmd.Parameters.Add("@ACC_ChartOfAccountLabel1ID", SqlDbType.Int).Value = aCC_ChartOfAccountLabel2.ACC_ChartOfAccountLabel1ID;
            cmd.Parameters.Add("@ChartOfAccountLabel2Text", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel2.ChartOfAccountLabel2Text;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel2.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel2.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel2.ExtraField3;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = aCC_ChartOfAccountLabel2.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = aCC_ChartOfAccountLabel2.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = aCC_ChartOfAccountLabel2.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = aCC_ChartOfAccountLabel2.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = aCC_ChartOfAccountLabel2.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@ACC_ChartOfAccountLabel2ID"].Value;
        }
    }

    public bool UpdateACC_ChartOfAccountLabel2(ACC_ChartOfAccountLabel2 aCC_ChartOfAccountLabel2)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdateACC_ChartOfAccountLabel2", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACC_ChartOfAccountLabel2ID", SqlDbType.Int).Value = aCC_ChartOfAccountLabel2.ACC_ChartOfAccountLabel2ID;
            cmd.Parameters.Add("@Code", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel2.Code;
            cmd.Parameters.Add("@ACC_ChartOfAccountLabel1ID", SqlDbType.Int).Value = aCC_ChartOfAccountLabel2.ACC_ChartOfAccountLabel1ID;
            cmd.Parameters.Add("@ChartOfAccountLabel2Text", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel2.ChartOfAccountLabel2Text;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel2.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel2.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel2.ExtraField3;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = aCC_ChartOfAccountLabel2.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = aCC_ChartOfAccountLabel2.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = aCC_ChartOfAccountLabel2.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = aCC_ChartOfAccountLabel2.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = aCC_ChartOfAccountLabel2.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
