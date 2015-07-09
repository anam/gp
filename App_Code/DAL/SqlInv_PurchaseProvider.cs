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

public class SqlInv_PurchaseProvider:DataAccessObject
{
	public SqlInv_PurchaseProvider()
    {
    }


    public bool DeleteInv_Purchase(int inv_PurchaseID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeleteInv_Purchase", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_PurchaseID", SqlDbType.Int).Value = inv_PurchaseID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Inv_Purchase> GetAllInv_Purchases()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_Purchases", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_PurchasesFromReader(reader);
        }
    }


    public List<Inv_Purchase> GetAllInv_PurchasesByDateNSupplierID(string SQL)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_PurchasesByDateNSupplierID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@SQL", SqlDbType.NVarChar).Value = SQL;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_PurchasesFromReader(reader);
        }
    }
    public List<Inv_Purchase> GetInv_PurchasesFromReader(IDataReader reader)
    {
        List<Inv_Purchase> inv_Purchases = new List<Inv_Purchase>();

        while (reader.Read())
        {
            inv_Purchases.Add(GetInv_PurchaseFromReader(reader));
        }
        return inv_Purchases;
    }

    public Inv_Purchase GetInv_PurchaseFromReader(IDataReader reader)
    {
        try
        {
            Inv_Purchase inv_Purchase = new Inv_Purchase
                (
                    (int)reader["Inv_PurchaseID"],
                    reader["PurchaseName"].ToString(),
                    (DateTime)reader["PurchseDate"],
                    (int)reader["SuppierID"],
                    reader["InvoiceNo"].ToString(),
                    reader["Particulars"].ToString(),
                    (bool)reader["IsPurchase"],
                    (int)reader["WorkSatationID"],
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

            try
            { inv_Purchase.SupplierName = reader["SupplierName"].ToString(); }
            catch (Exception ex) { }
             return inv_Purchase;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Inv_Purchase GetInv_PurchaseByID(int inv_PurchaseID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetInv_PurchaseByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Inv_PurchaseID", SqlDbType.Int).Value = inv_PurchaseID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetInv_PurchaseFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertInv_Purchase(Inv_Purchase inv_Purchase)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertInv_Purchase", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_PurchaseID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@PurchaseName", SqlDbType.NVarChar).Value = inv_Purchase.PurchaseName;
            cmd.Parameters.Add("@PurchseDate", SqlDbType.DateTime).Value = inv_Purchase.PurchseDate;
            cmd.Parameters.Add("@SuppierID", SqlDbType.Int).Value = inv_Purchase.SuppierID;
            cmd.Parameters.Add("@InvoiceNo", SqlDbType.NVarChar).Value = inv_Purchase.InvoiceNo;
            cmd.Parameters.Add("@Particulars", SqlDbType.NText).Value = inv_Purchase.Particulars;
            cmd.Parameters.Add("@IsPurchase", SqlDbType.Bit).Value = inv_Purchase.IsPurchase;
            cmd.Parameters.Add("@WorkSatationID", SqlDbType.Int).Value = inv_Purchase.WorkSatationID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = inv_Purchase.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_Purchase.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_Purchase.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_Purchase.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_Purchase.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_Purchase.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_Purchase.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_Purchase.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_Purchase.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_Purchase.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Inv_PurchaseID"].Value;
        }
    }

    public bool UpdateInv_Purchase(Inv_Purchase inv_Purchase)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdateInv_Purchase", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_PurchaseID", SqlDbType.Int).Value = inv_Purchase.Inv_PurchaseID;
            cmd.Parameters.Add("@PurchaseName", SqlDbType.NVarChar).Value = inv_Purchase.PurchaseName;
            cmd.Parameters.Add("@PurchseDate", SqlDbType.DateTime).Value = inv_Purchase.PurchseDate;
            cmd.Parameters.Add("@SuppierID", SqlDbType.Int).Value = inv_Purchase.SuppierID;
            cmd.Parameters.Add("@InvoiceNo", SqlDbType.NVarChar).Value = inv_Purchase.InvoiceNo;
            cmd.Parameters.Add("@Particulars", SqlDbType.NText).Value = inv_Purchase.Particulars;
            cmd.Parameters.Add("@IsPurchase", SqlDbType.Bit).Value = inv_Purchase.IsPurchase;
            cmd.Parameters.Add("@WorkSatationID", SqlDbType.Int).Value = inv_Purchase.WorkSatationID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = inv_Purchase.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_Purchase.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_Purchase.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_Purchase.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_Purchase.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_Purchase.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_Purchase.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_Purchase.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_Purchase.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_Purchase.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
