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

public class SqlADLHeaderDetailProvider:DataAccessObject
{
	public SqlADLHeaderDetailProvider()
    {
    }


    public bool DeleteADLHeaderDetail(int aDLHeaderDetailID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteADLHeaderDetail", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLHeaderDetailID", SqlDbType.Int).Value = aDLHeaderDetailID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<ADLHeaderDetail> GetAllADLHeaderDetails()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllADLHeaderDetails", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetADLHeaderDetailsFromReader(reader);
        }
    }

    public List<ADLHeaderDetail> GetDefaultAfterInsertADLHeaderDetails(int ResidentID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetDefaultAfterInsertADLHeaderDetails", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = ResidentID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetADLHeaderDetailsFromReader(reader);
        }
    }

    public List<ADLHeaderDetail> GetAllADLHeaderDetailsByResidentID(int ResidentID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllADLHeaderDetailsByResidentID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = ResidentID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetADLHeaderDetailsFromReader(reader);
        }
    }
    public List<ADLHeaderDetail> GetADLHeaderDetailsFromReader(IDataReader reader)
    {
        List<ADLHeaderDetail> aDLHeaderDetails = new List<ADLHeaderDetail>();

        while (reader.Read())
        {
            aDLHeaderDetails.Add(GetADLHeaderDetailFromReader(reader));
        }
        return aDLHeaderDetails;
    }

    public ADLHeaderDetail GetADLHeaderDetailFromReader(IDataReader reader)
    {
        try
        {
            ADLHeaderDetail aDLHeaderDetail = new ADLHeaderDetail
                (
                    (int)reader["ADLHeaderDetailID"],
                    (int)reader["ADLDetailID"],
                    (int)reader["ADLHeaderID"],
                    (int)reader["ResidentID"],
                    reader["ExtraField1"].ToString(),
                    reader["ExtraField2"].ToString(),
                    reader["ExtraField3"].ToString(),
                    reader["ExtraField4"].ToString(),
                    reader["ExtraField5"].ToString()
                );
            try
            {
                aDLHeaderDetail.ExtraField1 = reader["ADLHeaderName"].ToString();
                aDLHeaderDetail.ExtraField2 = reader["ADLDetailName"].ToString();
            }
            catch (Exception ex)
            { }

            return aDLHeaderDetail;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public ADLHeaderDetail GetADLHeaderDetailByID(int aDLHeaderDetailID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetADLHeaderDetailByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ADLHeaderDetailID", SqlDbType.Int).Value = aDLHeaderDetailID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetADLHeaderDetailFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertADLHeaderDetail(ADLHeaderDetail aDLHeaderDetail)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertADLHeaderDetail", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLHeaderDetailID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ADLDetailID", SqlDbType.Int).Value = aDLHeaderDetail.ADLDetailID;
            cmd.Parameters.Add("@ADLHeaderID", SqlDbType.Int).Value = aDLHeaderDetail.ADLHeaderID;
            cmd.Parameters.Add("@ResidentID", SqlDbType.Int).Value = aDLHeaderDetail.ResidentID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = aDLHeaderDetail.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = aDLHeaderDetail.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = aDLHeaderDetail.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = aDLHeaderDetail.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = aDLHeaderDetail.ExtraField5;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@ADLHeaderDetailID"].Value;
        }
    }

    public bool UpdateADLHeaderDetail(ADLHeaderDetail aDLHeaderDetail)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateADLHeaderDetail", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLHeaderDetailID", SqlDbType.Int).Value = aDLHeaderDetail.ADLHeaderDetailID;
            cmd.Parameters.Add("@ADLDetailID", SqlDbType.Int).Value = aDLHeaderDetail.ADLDetailID;
            cmd.Parameters.Add("@ADLHeaderID", SqlDbType.Int).Value = aDLHeaderDetail.ADLHeaderID;
            cmd.Parameters.Add("@ResidentID", SqlDbType.Int).Value = aDLHeaderDetail.ResidentID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = aDLHeaderDetail.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = aDLHeaderDetail.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = aDLHeaderDetail.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = aDLHeaderDetail.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = aDLHeaderDetail.ExtraField5;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
