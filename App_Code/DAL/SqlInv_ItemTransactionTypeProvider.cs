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

public class SqlInv_ItemTransactionTypeProvider:DataAccessObject
{
	public SqlInv_ItemTransactionTypeProvider()
    {
    }


    public bool DeleteInv_ItemTransactionType(int inv_ItemTransactionTypeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeleteInv_ItemTransactionType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_ItemTransactionTypeID", SqlDbType.Int).Value = inv_ItemTransactionTypeID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Inv_ItemTransactionType> GetAllInv_ItemTransactionTypes()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_ItemTransactionTypes", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_ItemTransactionTypesFromReader(reader);
        }
    }
    public List<Inv_ItemTransactionType> GetInv_ItemTransactionTypesFromReader(IDataReader reader)
    {
        List<Inv_ItemTransactionType> inv_ItemTransactionTypes = new List<Inv_ItemTransactionType>();

        while (reader.Read())
        {
            inv_ItemTransactionTypes.Add(GetInv_ItemTransactionTypeFromReader(reader));
        }
        return inv_ItemTransactionTypes;
    }

    public Inv_ItemTransactionType GetInv_ItemTransactionTypeFromReader(IDataReader reader)
    {
        try
        {
            Inv_ItemTransactionType inv_ItemTransactionType = new Inv_ItemTransactionType
                (
                    (int)reader["Inv_ItemTransactionTypeID"],
                    reader["ItemTransactionTypeName"].ToString(),
                    (int)reader["RowStatusID"]
                );
             return inv_ItemTransactionType;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Inv_ItemTransactionType GetInv_ItemTransactionTypeByID(int inv_ItemTransactionTypeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetInv_ItemTransactionTypeByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Inv_ItemTransactionTypeID", SqlDbType.Int).Value = inv_ItemTransactionTypeID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetInv_ItemTransactionTypeFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertInv_ItemTransactionType(Inv_ItemTransactionType inv_ItemTransactionType)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertInv_ItemTransactionType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_ItemTransactionTypeID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ItemTransactionTypeName", SqlDbType.NVarChar).Value = inv_ItemTransactionType.ItemTransactionTypeName;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_ItemTransactionType.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Inv_ItemTransactionTypeID"].Value;
        }
    }

    public bool UpdateInv_ItemTransactionType(Inv_ItemTransactionType inv_ItemTransactionType)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdateInv_ItemTransactionType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_ItemTransactionTypeID", SqlDbType.Int).Value = inv_ItemTransactionType.Inv_ItemTransactionTypeID;
            cmd.Parameters.Add("@ItemTransactionTypeName", SqlDbType.NVarChar).Value = inv_ItemTransactionType.ItemTransactionTypeName;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_ItemTransactionType.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
