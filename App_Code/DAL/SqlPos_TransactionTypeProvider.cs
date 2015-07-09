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

public class SqlPos_TransactionTypeProvider:DataAccessObject
{
	public SqlPos_TransactionTypeProvider()
    {
    }


    public bool DeletePos_TransactionType(int pos_TransactionTypeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeletePos_TransactionType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_TransactionTypeID", SqlDbType.Int).Value = pos_TransactionTypeID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Pos_TransactionType> GetAllPos_TransactionTypes()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllPos_TransactionTypes", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetPos_TransactionTypesFromReader(reader);
        }
    }
    public List<Pos_TransactionType> GetPos_TransactionTypesFromReader(IDataReader reader)
    {
        List<Pos_TransactionType> pos_TransactionTypes = new List<Pos_TransactionType>();

        while (reader.Read())
        {
            pos_TransactionTypes.Add(GetPos_TransactionTypeFromReader(reader));
        }
        return pos_TransactionTypes;
    }

    public Pos_TransactionType GetPos_TransactionTypeFromReader(IDataReader reader)
    {
        try
        {
            Pos_TransactionType pos_TransactionType = new Pos_TransactionType
                (
                    (int)reader["Pos_TransactionTypeID"],
                    reader["TransactionTypeName"].ToString(),
                    reader["CentralStockFormula"].ToString(),
                    reader["ShowRoomFormula"].ToString(),
                    reader["ExtraField1"].ToString(),
                    reader["ExtraField2"].ToString(),
                    reader["ExtraField3"].ToString()
                );
            try
            {
                pos_TransactionType.ExtraField4 = reader["ExtraField4"].ToString();
                pos_TransactionType.ExtraField5 = reader["ExtraField5"].ToString();
                pos_TransactionType.ExtraField6 = reader["ExtraField6"].ToString();
                pos_TransactionType.Sorting = int.Parse(pos_TransactionType.ExtraField5);
            }
            catch (Exception ex)
            {
                pos_TransactionType.ExtraField4 ="";
                pos_TransactionType.ExtraField5 = "";
                pos_TransactionType.ExtraField6 = "";
            }
            return pos_TransactionType;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Pos_TransactionType GetPos_TransactionTypeByID(int pos_TransactionTypeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetPos_TransactionTypeByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Pos_TransactionTypeID", SqlDbType.Int).Value = pos_TransactionTypeID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetPos_TransactionTypeFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertPos_TransactionType(Pos_TransactionType pos_TransactionType)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertPos_TransactionType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_TransactionTypeID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@TransactionTypeName", SqlDbType.NVarChar).Value = pos_TransactionType.TransactionTypeName;
            cmd.Parameters.Add("@CentralStockFormula", SqlDbType.NChar).Value = pos_TransactionType.CentralStockFormula;
            cmd.Parameters.Add("@ShowRoomFormula", SqlDbType.NChar).Value = pos_TransactionType.ShowRoomFormula;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = pos_TransactionType.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = pos_TransactionType.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = pos_TransactionType.ExtraField3;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Pos_TransactionTypeID"].Value;
        }
    }

    public bool UpdatePos_TransactionType(Pos_TransactionType pos_TransactionType)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdatePos_TransactionType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_TransactionTypeID", SqlDbType.Int).Value = pos_TransactionType.Pos_TransactionTypeID;
            cmd.Parameters.Add("@TransactionTypeName", SqlDbType.NVarChar).Value = pos_TransactionType.TransactionTypeName;
            cmd.Parameters.Add("@CentralStockFormula", SqlDbType.NChar).Value = pos_TransactionType.CentralStockFormula;
            cmd.Parameters.Add("@ShowRoomFormula", SqlDbType.NChar).Value = pos_TransactionType.ShowRoomFormula;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = pos_TransactionType.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = pos_TransactionType.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = pos_TransactionType.ExtraField3;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
