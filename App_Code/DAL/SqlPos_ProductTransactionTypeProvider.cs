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

public class SqlPos_ProductTransactionTypeProvider:DataAccessObject
{
	public SqlPos_ProductTransactionTypeProvider()
    {
    }


    public bool DeletePos_ProductTransactionType(int pos_ProductTransactionTypeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeletePos_ProductTransactionType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_ProductTransactionTypeID", SqlDbType.Int).Value = pos_ProductTransactionTypeID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Pos_ProductTransactionType> GetAllPos_ProductTransactionTypes()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllPos_ProductTransactionTypes", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetPos_ProductTransactionTypesFromReader(reader);
        }
    }
    public List<Pos_ProductTransactionType> GetPos_ProductTransactionTypesFromReader(IDataReader reader)
    {
        List<Pos_ProductTransactionType> pos_ProductTransactionTypes = new List<Pos_ProductTransactionType>();

        while (reader.Read())
        {
            pos_ProductTransactionTypes.Add(GetPos_ProductTransactionTypeFromReader(reader));
        }
        return pos_ProductTransactionTypes;
    }

    public Pos_ProductTransactionType GetPos_ProductTransactionTypeFromReader(IDataReader reader)
    {
        try
        {
            Pos_ProductTransactionType pos_ProductTransactionType = new Pos_ProductTransactionType
                (
                    (int)reader["Pos_ProductTransactionTypeID"],
                    reader["ProductTransactionTypeName"].ToString(),
                    reader["CentralStockFormula"].ToString(),
                    reader["ShowRoomFormula"].ToString(),
                    reader["ExtraField1"].ToString(),
                    reader["ExtraField2"].ToString(),
                    reader["ExtraField3"].ToString()
                );
             return pos_ProductTransactionType;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Pos_ProductTransactionType GetPos_ProductTransactionTypeByID(int pos_ProductTransactionTypeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetPos_ProductTransactionTypeByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Pos_ProductTransactionTypeID", SqlDbType.Int).Value = pos_ProductTransactionTypeID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetPos_ProductTransactionTypeFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertPos_ProductTransactionType(Pos_ProductTransactionType pos_ProductTransactionType)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertPos_ProductTransactionType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_ProductTransactionTypeID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ProductTransactionTypeName", SqlDbType.NVarChar).Value = pos_ProductTransactionType.ProductTransactionTypeName;
            cmd.Parameters.Add("@CentralStockFormula", SqlDbType.NChar).Value = pos_ProductTransactionType.CentralStockFormula;
            cmd.Parameters.Add("@ShowRoomFormula", SqlDbType.NChar).Value = pos_ProductTransactionType.ShowRoomFormula;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = pos_ProductTransactionType.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = pos_ProductTransactionType.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = pos_ProductTransactionType.ExtraField3;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Pos_ProductTransactionTypeID"].Value;
        }
    }

    public bool UpdatePos_ProductTransactionType(Pos_ProductTransactionType pos_ProductTransactionType)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdatePos_ProductTransactionType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_ProductTransactionTypeID", SqlDbType.Int).Value = pos_ProductTransactionType.Pos_ProductTransactionTypeID;
            cmd.Parameters.Add("@ProductTransactionTypeName", SqlDbType.NVarChar).Value = pos_ProductTransactionType.ProductTransactionTypeName;
            cmd.Parameters.Add("@CentralStockFormula", SqlDbType.NChar).Value = pos_ProductTransactionType.CentralStockFormula;
            cmd.Parameters.Add("@ShowRoomFormula", SqlDbType.NChar).Value = pos_ProductTransactionType.ShowRoomFormula;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = pos_ProductTransactionType.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = pos_ProductTransactionType.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = pos_ProductTransactionType.ExtraField3;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
