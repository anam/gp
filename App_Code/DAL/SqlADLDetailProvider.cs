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

public class SqlADLDetailProvider:DataAccessObject
{
	public SqlADLDetailProvider()
    {
    }


    public bool DeleteADLDetail(int aDLDetailID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteADLDetail", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLDetailID", SqlDbType.Int).Value = aDLDetailID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<ADLDetail> GetAllADLDetails()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllADLDetails", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetADLDetailsFromReader(reader);
        }
    }
    public List<ADLDetail> GetADLDetailsFromReader(IDataReader reader)
    {
        List<ADLDetail> aDLDetails = new List<ADLDetail>();

        while (reader.Read())
        {
            aDLDetails.Add(GetADLDetailFromReader(reader));
        }
        return aDLDetails;
    }

    public ADLDetail GetADLDetailFromReader(IDataReader reader)
    {
        try
        {
            ADLDetail aDLDetail = new ADLDetail
                (
                    (int)reader["ADLDetailID"],
                    reader["ADLDetailName"].ToString(),
                    reader["ExtraField1"].ToString(),
                    reader["ExtraField2"].ToString(),
                    reader["ExtraField3"].ToString(),
                    reader["ExtraField4"].ToString(),
                    reader["ExtraField5"].ToString()
                );
             return aDLDetail;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public ADLDetail GetADLDetailByID(int aDLDetailID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetADLDetailByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ADLDetailID", SqlDbType.Int).Value = aDLDetailID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetADLDetailFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertADLDetail(ADLDetail aDLDetail)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertADLDetail", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLDetailID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ADLDetailName", SqlDbType.NVarChar).Value = aDLDetail.ADLDetailName;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = aDLDetail.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = aDLDetail.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = aDLDetail.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = aDLDetail.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = aDLDetail.ExtraField5;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@ADLDetailID"].Value;
        }
    }

    public bool UpdateADLDetail(ADLDetail aDLDetail)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateADLDetail", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLDetailID", SqlDbType.Int).Value = aDLDetail.ADLDetailID;
            cmd.Parameters.Add("@ADLDetailName", SqlDbType.NVarChar).Value = aDLDetail.ADLDetailName;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = aDLDetail.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = aDLDetail.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = aDLDetail.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = aDLDetail.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = aDLDetail.ExtraField5;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
