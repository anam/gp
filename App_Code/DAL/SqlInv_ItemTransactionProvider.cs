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

public class SqlInv_ItemTransactionProvider:DataAccessObject
{
	public SqlInv_ItemTransactionProvider()
    {
    }


    public bool DeleteInv_ItemTransaction(int inv_ItemTransactionID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeleteInv_ItemTransaction", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_ItemTransactionID", SqlDbType.Int).Value = inv_ItemTransactionID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Inv_ItemTransaction> GetAllInv_ItemTransactions()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_ItemTransactions", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_ItemTransactionsFromReader(reader);
        }
    }


    public List<Inv_ItemTransaction> GetAllInv_ItemTransactionsByItemID(int itemID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_ItemTransactionsByItemID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ItemID", SqlDbType.Int).Value = itemID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_ItemTransactionsFromReader(reader);
        }
    }



    public List<Inv_ItemTransaction> GetAllInv_ItemTransactionsByItemCode(string itemCode)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_ItemTransactionsByItemCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ItemCode", SqlDbType.NVarChar).Value = itemCode;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_ItemTransactionsFromReader(reader);
        }
    }

    public List<Inv_ItemTransaction> GetInv_ItemTransactionsFromReader(IDataReader reader)
    {
        List<Inv_ItemTransaction> inv_ItemTransactions = new List<Inv_ItemTransaction>();

        while (reader.Read())
        {
            inv_ItemTransactions.Add(GetInv_ItemTransactionFromReader(reader));
        }
        return inv_ItemTransactions;
    }

    public Inv_ItemTransaction GetInv_ItemTransactionFromReader(IDataReader reader)
    {
        try
        {
            Inv_ItemTransaction inv_ItemTransaction = new Inv_ItemTransaction
                (
                    (int)reader["Inv_ItemTransactionID"],
                    (int)reader["ItemID"],
                    (decimal)reader["Quantity"],
                    (int)reader["ItemTrasactionTypeID"],
                    (decimal)reader["ReferenceID"],
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
             return inv_ItemTransaction;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Inv_ItemTransaction GetInv_ItemTransactionByID(int inv_ItemTransactionID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetInv_ItemTransactionByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Inv_ItemTransactionID", SqlDbType.Int).Value = inv_ItemTransactionID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetInv_ItemTransactionFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertInv_ItemTransaction(Inv_ItemTransaction inv_ItemTransaction)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertInv_ItemTransaction", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_ItemTransactionID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = inv_ItemTransaction.ItemID;
            cmd.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = inv_ItemTransaction.Quantity;
            cmd.Parameters.Add("@ItemTrasactionTypeID", SqlDbType.Int).Value = inv_ItemTransaction.ItemTrasactionTypeID;
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Decimal).Value = inv_ItemTransaction.ReferenceID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = inv_ItemTransaction.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_ItemTransaction.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_ItemTransaction.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_ItemTransaction.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_ItemTransaction.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_ItemTransaction.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_ItemTransaction.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_ItemTransaction.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_ItemTransaction.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_ItemTransaction.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Inv_ItemTransactionID"].Value;
        }
    }

    public bool UpdateInv_ItemTransaction(Inv_ItemTransaction inv_ItemTransaction)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdateInv_ItemTransaction", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_ItemTransactionID", SqlDbType.Int).Value = inv_ItemTransaction.Inv_ItemTransactionID;
            cmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = inv_ItemTransaction.ItemID;
            cmd.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = inv_ItemTransaction.Quantity;
            cmd.Parameters.Add("@ItemTrasactionTypeID", SqlDbType.Int).Value = inv_ItemTransaction.ItemTrasactionTypeID;
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Decimal).Value = inv_ItemTransaction.ReferenceID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = inv_ItemTransaction.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_ItemTransaction.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_ItemTransaction.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_ItemTransaction.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_ItemTransaction.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_ItemTransaction.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_ItemTransaction.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_ItemTransaction.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_ItemTransaction.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_ItemTransaction.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
