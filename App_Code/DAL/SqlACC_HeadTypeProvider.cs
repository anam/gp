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

public class SqlACC_HeadTypeProvider:DataAccessObject
{
	public SqlACC_HeadTypeProvider()
    {
    }


    public bool DeleteACC_HeadType(int aCC_HeadTypeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeleteACC_HeadType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACC_HeadTypeID", SqlDbType.Int).Value = aCC_HeadTypeID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<ACC_HeadType> GetAllACC_HeadTypes()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_HeadTypes", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_HeadTypesFromReader(reader);
        }
    }
    public List<ACC_HeadType> GetACC_HeadTypesFromReader(IDataReader reader)
    {
        List<ACC_HeadType> aCC_HeadTypes = new List<ACC_HeadType>();

        while (reader.Read())
        {
            aCC_HeadTypes.Add(GetACC_HeadTypeFromReader(reader));
        }
        return aCC_HeadTypes;
    }

    public ACC_HeadType GetACC_HeadTypeFromReader(IDataReader reader)
    {
        try
        {
            ACC_HeadType aCC_HeadType = new ACC_HeadType
                (
                    (int)reader["ACC_HeadTypeID"],
                    reader["HeadTypeName"].ToString(),
                    reader["ExtraField1"].ToString(),
                    reader["ExtraField2"].ToString(),
                    reader["ExtraField3"].ToString()
                );
             return aCC_HeadType;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public ACC_HeadType GetACC_HeadTypeByID(int aCC_HeadTypeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetACC_HeadTypeByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ACC_HeadTypeID", SqlDbType.Int).Value = aCC_HeadTypeID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetACC_HeadTypeFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertACC_HeadType(ACC_HeadType aCC_HeadType)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertACC_HeadType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACC_HeadTypeID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@HeadTypeName", SqlDbType.NVarChar).Value = aCC_HeadType.HeadTypeName;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = aCC_HeadType.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = aCC_HeadType.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = aCC_HeadType.ExtraField3;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@ACC_HeadTypeID"].Value;
        }
    }

    public bool UpdateACC_HeadType(ACC_HeadType aCC_HeadType)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdateACC_HeadType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACC_HeadTypeID", SqlDbType.Int).Value = aCC_HeadType.ACC_HeadTypeID;
            cmd.Parameters.Add("@HeadTypeName", SqlDbType.NVarChar).Value = aCC_HeadType.HeadTypeName;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = aCC_HeadType.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = aCC_HeadType.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = aCC_HeadType.ExtraField3;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
