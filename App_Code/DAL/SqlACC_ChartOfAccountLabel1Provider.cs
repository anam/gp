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

public class SqlACC_ChartOfAccountLabel1Provider:DataAccessObject
{
	public SqlACC_ChartOfAccountLabel1Provider()
    {
    }


    public bool DeleteACC_ChartOfAccountLabel1(int aCC_ChartOfAccountLabel1ID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeleteACC_ChartOfAccountLabel1", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACC_ChartOfAccountLabel1ID", SqlDbType.Int).Value = aCC_ChartOfAccountLabel1ID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<ACC_ChartOfAccountLabel1> GetAllACC_ChartOfAccountLabel1s()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_ChartOfAccountLabel1s", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_ChartOfAccountLabel1sFromReader(reader);
        }
    }
    public List<ACC_ChartOfAccountLabel1> GetACC_ChartOfAccountLabel1sFromReader(IDataReader reader)
    {
        List<ACC_ChartOfAccountLabel1> aCC_ChartOfAccountLabel1s = new List<ACC_ChartOfAccountLabel1>();

        while (reader.Read())
        {
            aCC_ChartOfAccountLabel1s.Add(GetACC_ChartOfAccountLabel1FromReader(reader));
        }
        return aCC_ChartOfAccountLabel1s;
    }

    public ACC_ChartOfAccountLabel1 GetACC_ChartOfAccountLabel1FromReader(IDataReader reader)
    {
        try
        {
            ACC_ChartOfAccountLabel1 aCC_ChartOfAccountLabel1 = new ACC_ChartOfAccountLabel1
                (
                    (int)reader["ACC_ChartOfAccountLabel1ID"],
                    reader["Code"].ToString(),
                    reader["ChartOfAccountLabel1Text"].ToString(),
                    reader["ExtraField1"].ToString(),
                    reader["ExtraField2"].ToString(),
                    reader["ExtraField3"].ToString(),
                    (int)reader["AddedBy"],
                    (DateTime)reader["AddedDate"],
                    (int)reader["UpdatedBy"],
                    (DateTime)reader["UpdatedDate"],
                    (int)reader["RowStatusID"]
                );
             return aCC_ChartOfAccountLabel1;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public ACC_ChartOfAccountLabel1 GetACC_ChartOfAccountLabel1ByID(int aCC_ChartOfAccountLabel1ID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetACC_ChartOfAccountLabel1ByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ACC_ChartOfAccountLabel1ID", SqlDbType.Int).Value = aCC_ChartOfAccountLabel1ID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetACC_ChartOfAccountLabel1FromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertACC_ChartOfAccountLabel1(ACC_ChartOfAccountLabel1 aCC_ChartOfAccountLabel1)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertACC_ChartOfAccountLabel1", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACC_ChartOfAccountLabel1ID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Code", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel1.Code;
            cmd.Parameters.Add("@ChartOfAccountLabel1Text", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel1.ChartOfAccountLabel1Text;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel1.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel1.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel1.ExtraField3;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = aCC_ChartOfAccountLabel1.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = aCC_ChartOfAccountLabel1.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = aCC_ChartOfAccountLabel1.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = aCC_ChartOfAccountLabel1.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = aCC_ChartOfAccountLabel1.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@ACC_ChartOfAccountLabel1ID"].Value;
        }
    }

    public bool UpdateACC_ChartOfAccountLabel1(ACC_ChartOfAccountLabel1 aCC_ChartOfAccountLabel1)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdateACC_ChartOfAccountLabel1", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACC_ChartOfAccountLabel1ID", SqlDbType.Int).Value = aCC_ChartOfAccountLabel1.ACC_ChartOfAccountLabel1ID;
            cmd.Parameters.Add("@Code", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel1.Code;
            cmd.Parameters.Add("@ChartOfAccountLabel1Text", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel1.ChartOfAccountLabel1Text;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel1.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel1.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = aCC_ChartOfAccountLabel1.ExtraField3;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = aCC_ChartOfAccountLabel1.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = aCC_ChartOfAccountLabel1.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = aCC_ChartOfAccountLabel1.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = aCC_ChartOfAccountLabel1.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = aCC_ChartOfAccountLabel1.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
