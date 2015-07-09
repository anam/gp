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

public class SqlPos_ProductTransactionProvider:DataAccessObject
{
	public SqlPos_ProductTransactionProvider()
    {
    }


    public bool DeletePos_ProductTransaction(int pos_ProductTransactionID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeletePos_ProductTransaction", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_ProductTransactionID", SqlDbType.Int).Value = pos_ProductTransactionID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Pos_ProductTransaction> GetAllPos_ProductTransactions()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllPos_ProductTransactions", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetPos_ProductTransactionsFromReader(reader);
        }
    }
    public List<Pos_ProductTransaction> GetPos_ProductTransactionsFromReader(IDataReader reader)
    {
        List<Pos_ProductTransaction> pos_ProductTransactions = new List<Pos_ProductTransaction>();

        while (reader.Read())
        {
            pos_ProductTransactions.Add(GetPos_ProductTransactionFromReader(reader));
        }
        return pos_ProductTransactions;
    }

    public Pos_ProductTransaction GetPos_ProductTransactionFromReader(IDataReader reader)
    {
        try
        {
            Pos_ProductTransaction pos_ProductTransaction = new Pos_ProductTransaction
                (
                    (int)reader["Pos_ProductTransactionID"],
                    (int)reader["Pos_ProductID"],
                    (decimal)reader["Quantity"],
                    (int)reader["Pos_ProductTrasactionTypeID"],
                    (int)reader["Pos_ProductTransactionMasterID"],
                    reader["ExtraField1"].ToString(),
                    reader["ExtraField2"].ToString(),
                    reader["ExtraField3"].ToString(),
                    reader["ExtraField4"].ToString(),
                    reader["ExtraField5"].ToString(),
                    (int)reader["AddedBy"],
                    (DateTime)reader["AddedDate"],
                    (int)reader["UpdatedBy"],
                    (DateTime)reader["UpdatedDate"],
                    (int)reader["RowStatusID"]
                );
             return pos_ProductTransaction;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Pos_ProductTransaction GetPos_ProductTransactionByID(int pos_ProductTransactionID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetPos_ProductTransactionByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Pos_ProductTransactionID", SqlDbType.Int).Value = pos_ProductTransactionID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetPos_ProductTransactionFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertPos_ProductTransaction(Pos_ProductTransaction pos_ProductTransaction)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertPos_ProductTransaction", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_ProductTransactionID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Pos_ProductID", SqlDbType.Int).Value = pos_ProductTransaction.Pos_ProductID;
            cmd.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = pos_ProductTransaction.Quantity;
            cmd.Parameters.Add("@Pos_ProductTrasactionTypeID", SqlDbType.Int).Value = pos_ProductTransaction.Pos_ProductTrasactionTypeID;
            cmd.Parameters.Add("@Pos_ProductTransactionMasterID", SqlDbType.Int).Value = pos_ProductTransaction.Pos_ProductTransactionMasterID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = pos_ProductTransaction.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = pos_ProductTransaction.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = pos_ProductTransaction.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = pos_ProductTransaction.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = pos_ProductTransaction.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = pos_ProductTransaction.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = pos_ProductTransaction.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = pos_ProductTransaction.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = pos_ProductTransaction.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = pos_ProductTransaction.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Pos_ProductTransactionID"].Value;
        }
    }

    public bool UpdatePos_ProductTransaction(Pos_ProductTransaction pos_ProductTransaction)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdatePos_ProductTransaction", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_ProductTransactionID", SqlDbType.Int).Value = pos_ProductTransaction.Pos_ProductTransactionID;
            cmd.Parameters.Add("@Pos_ProductID", SqlDbType.Int).Value = pos_ProductTransaction.Pos_ProductID;
            cmd.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = pos_ProductTransaction.Quantity;
            cmd.Parameters.Add("@Pos_ProductTrasactionTypeID", SqlDbType.Int).Value = pos_ProductTransaction.Pos_ProductTrasactionTypeID;
            cmd.Parameters.Add("@Pos_ProductTransactionMasterID", SqlDbType.Int).Value = pos_ProductTransaction.Pos_ProductTransactionMasterID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = pos_ProductTransaction.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = pos_ProductTransaction.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = pos_ProductTransaction.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = pos_ProductTransaction.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = pos_ProductTransaction.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = pos_ProductTransaction.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = pos_ProductTransaction.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = pos_ProductTransaction.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = pos_ProductTransaction.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = pos_ProductTransaction.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
