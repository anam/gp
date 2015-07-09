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

public class SqlPos_TransactionProvider:DataAccessObject
{
	public SqlPos_TransactionProvider()
    {
    }


    public bool DeletePos_Transaction(int pos_TransactionID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeletePos_Transaction", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_TransactionID", SqlDbType.Int).Value = pos_TransactionID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Pos_Transaction> GetAllPos_Transactions()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllPos_Transactions", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetPos_TransactionsFromReader(reader);
        }
    }
    public List<Pos_Transaction> GetPos_TransactionsFromReader(IDataReader reader)
    {
        List<Pos_Transaction> pos_Transactions = new List<Pos_Transaction>();

        while (reader.Read())
        {
            pos_Transactions.Add(GetPos_TransactionFromReader(reader));
        }
        return pos_Transactions;
    }

    public Pos_Transaction GetPos_TransactionFromReader(IDataReader reader)
    {
        try
        {
            Pos_Transaction pos_Transaction = new Pos_Transaction
                (
                    (int)reader["Pos_TransactionID"],
                    (int)reader["Pos_ProductID"],
                    (decimal)reader["Quantity"],
                    (int)reader["Pos_ProductTrasactionTypeID"],
                    (int)reader["Pos_ProductTransactionMasterID"],
                    (int)reader["WorkStationID"],
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
             return pos_Transaction;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Pos_Transaction GetPos_TransactionByID(int pos_TransactionID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetPos_TransactionByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Pos_TransactionID", SqlDbType.Int).Value = pos_TransactionID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetPos_TransactionFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertPos_Transaction(Pos_Transaction pos_Transaction)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertPos_Transaction", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_TransactionID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Pos_ProductID", SqlDbType.Int).Value = pos_Transaction.Pos_ProductID;
            cmd.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = pos_Transaction.Quantity;
            cmd.Parameters.Add("@Pos_ProductTrasactionTypeID", SqlDbType.Int).Value = pos_Transaction.Pos_ProductTrasactionTypeID;
            cmd.Parameters.Add("@Pos_ProductTransactionMasterID", SqlDbType.Int).Value = pos_Transaction.Pos_ProductTransactionMasterID;
            cmd.Parameters.Add("@WorkStationID", SqlDbType.Int).Value = pos_Transaction.WorkStationID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = pos_Transaction.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = pos_Transaction.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = pos_Transaction.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = pos_Transaction.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = pos_Transaction.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = pos_Transaction.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = pos_Transaction.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = pos_Transaction.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = pos_Transaction.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = pos_Transaction.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Pos_TransactionID"].Value;
        }
    }

    public int InsertPos_TransactionWithOpositeEntry(Pos_Transaction pos_Transaction)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertPos_TransactionWithOpositeEntry", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_TransactionID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Pos_ProductID", SqlDbType.Int).Value = pos_Transaction.Pos_ProductID;
            cmd.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = pos_Transaction.Quantity;
            cmd.Parameters.Add("@Pos_ProductTrasactionTypeID", SqlDbType.Int).Value = pos_Transaction.Pos_ProductTrasactionTypeID;
            cmd.Parameters.Add("@Pos_ProductTransactionMasterID", SqlDbType.Int).Value = pos_Transaction.Pos_ProductTransactionMasterID;
            cmd.Parameters.Add("@WorkStationID", SqlDbType.Int).Value = pos_Transaction.WorkStationID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = pos_Transaction.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = pos_Transaction.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = pos_Transaction.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = pos_Transaction.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = pos_Transaction.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = pos_Transaction.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = pos_Transaction.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = pos_Transaction.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = pos_Transaction.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = pos_Transaction.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Pos_TransactionID"].Value;
        }
    }

    public bool UpdatePos_Transaction(Pos_Transaction pos_Transaction)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdatePos_Transaction", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_TransactionID", SqlDbType.Int).Value = pos_Transaction.Pos_TransactionID;
            cmd.Parameters.Add("@Pos_ProductID", SqlDbType.Int).Value = pos_Transaction.Pos_ProductID;
            cmd.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = pos_Transaction.Quantity;
            cmd.Parameters.Add("@Pos_ProductTrasactionTypeID", SqlDbType.Int).Value = pos_Transaction.Pos_ProductTrasactionTypeID;
            cmd.Parameters.Add("@Pos_ProductTransactionMasterID", SqlDbType.Int).Value = pos_Transaction.Pos_ProductTransactionMasterID;
            cmd.Parameters.Add("@WorkStationID", SqlDbType.Int).Value = pos_Transaction.WorkStationID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = pos_Transaction.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = pos_Transaction.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = pos_Transaction.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = pos_Transaction.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = pos_Transaction.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = pos_Transaction.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = pos_Transaction.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = pos_Transaction.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = pos_Transaction.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = pos_Transaction.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
