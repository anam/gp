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

public class SqlPos_CustomerProvider:DataAccessObject
{
	public SqlPos_CustomerProvider()
    {
    }


    public bool DeletePos_Customer(int pos_CustomerID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeletePos_Customer", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_CustomerID", SqlDbType.Int).Value = pos_CustomerID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Pos_Customer> GetAllPos_Customers()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllPos_Customers", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetPos_CustomersFromReader(reader);
        }
    }
    public List<Pos_Customer> GetPos_CustomersFromReader(IDataReader reader)
    {
        List<Pos_Customer> pos_Customers = new List<Pos_Customer>();

        while (reader.Read())
        {
            pos_Customers.Add(GetPos_CustomerFromReader(reader));
        }
        return pos_Customers;
    }

    public Pos_Customer GetPos_CustomerFromReader(IDataReader reader)
    {
        try
        {
            Pos_Customer pos_Customer = new Pos_Customer
                (
                    (int)reader["Pos_CustomerID"],
                    reader["CardNo"].ToString(),
                    reader["Signature"].ToString(),
                    reader["CustomerName"].ToString(),
                    (int)reader["ReferenceID"],
                    reader["Address"].ToString(),
                    reader["Mobile"].ToString(),
                    reader["Phone"].ToString(),
                    (decimal)reader["DiscountPersent"],
                    reader["Note"].ToString(),
                    reader["ExtraFiled1"].ToString(),
                    reader["ExtraFiled2"].ToString(),
                    reader["ExtraFiled3"].ToString(),
                    reader["ExtraFiled4"].ToString(),
                    reader["ExtraFiled5"].ToString(),
                    (int)reader["AddedBy"],
                    (DateTime)reader["AddedDate"],
                    (int)reader["UpdatedBy"],
                    (DateTime)reader["UpdatedDate"],
                    (int)reader["RowSatatusID"],
                    (DateTime)reader["DateofBirth"],
                    (DateTime)reader["ApplicationDate"],
                    (DateTime)reader["CardIssueDate"],
                    (DateTime)reader["ExpireDate"],
                    reader["CardType"].ToString(),
                    reader["ApprovedBy"].ToString()
                );
             return pos_Customer;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Pos_Customer GetPos_CustomerByID(int pos_CustomerID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetPos_CustomerByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Pos_CustomerID", SqlDbType.Int).Value = pos_CustomerID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetPos_CustomerFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertPos_Customer(Pos_Customer pos_Customer)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertPos_Customer", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_CustomerID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@CardNo", SqlDbType.VarChar).Value = pos_Customer.CardNo;
            cmd.Parameters.Add("@Signature", SqlDbType.NVarChar).Value = pos_Customer.Signature;
            cmd.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = pos_Customer.CustomerName;
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = pos_Customer.ReferenceID;
            cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = pos_Customer.Address;
            cmd.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = pos_Customer.Mobile;
            cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = pos_Customer.Phone;
            cmd.Parameters.Add("@DiscountPersent", SqlDbType.Decimal).Value = pos_Customer.DiscountPersent;
            cmd.Parameters.Add("@Note", SqlDbType.VarChar).Value = pos_Customer.Note;
            cmd.Parameters.Add("@ExtraFiled1", SqlDbType.NVarChar).Value = pos_Customer.ExtraFiled1;
            cmd.Parameters.Add("@ExtraFiled2", SqlDbType.NVarChar).Value = pos_Customer.ExtraFiled2;
            cmd.Parameters.Add("@ExtraFiled3", SqlDbType.NVarChar).Value = pos_Customer.ExtraFiled3;
            cmd.Parameters.Add("@ExtraFiled4", SqlDbType.NVarChar).Value = pos_Customer.ExtraFiled4;
            cmd.Parameters.Add("@ExtraFiled5", SqlDbType.NVarChar).Value = pos_Customer.ExtraFiled5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = pos_Customer.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = pos_Customer.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = pos_Customer.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = pos_Customer.UpdatedDate;
            cmd.Parameters.Add("@RowSatatusID", SqlDbType.Int).Value = pos_Customer.RowSatatusID;
            cmd.Parameters.Add("@DateofBirth", SqlDbType.DateTime).Value = pos_Customer.DateofBirth;
            cmd.Parameters.Add("@ApplicationDate", SqlDbType.DateTime).Value = pos_Customer.ApplicationDate;
            cmd.Parameters.Add("@CardIssueDate", SqlDbType.DateTime).Value = pos_Customer.CardIssueDate;
            cmd.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = pos_Customer.ExpireDate;
            cmd.Parameters.Add("@CardType", SqlDbType.NVarChar).Value = pos_Customer.CardType;
            cmd.Parameters.Add("@ApprovedBy", SqlDbType.NVarChar).Value = pos_Customer.ApprovedBy;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Pos_CustomerID"].Value;
        }
    }

    public bool UpdatePos_Customer(Pos_Customer pos_Customer)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdatePos_Customer", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_CustomerID", SqlDbType.Int).Value = pos_Customer.Pos_CustomerID;
            cmd.Parameters.Add("@CardNo", SqlDbType.VarChar).Value = pos_Customer.CardNo;
            cmd.Parameters.Add("@Signature", SqlDbType.NVarChar).Value = pos_Customer.Signature;
            cmd.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = pos_Customer.CustomerName;
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = pos_Customer.ReferenceID;
            cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = pos_Customer.Address;
            cmd.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = pos_Customer.Mobile;
            cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = pos_Customer.Phone;
            cmd.Parameters.Add("@DiscountPersent", SqlDbType.Decimal).Value = pos_Customer.DiscountPersent;
            cmd.Parameters.Add("@Note", SqlDbType.VarChar).Value = pos_Customer.Note;
            cmd.Parameters.Add("@ExtraFiled1", SqlDbType.NVarChar).Value = pos_Customer.ExtraFiled1;
            cmd.Parameters.Add("@ExtraFiled2", SqlDbType.NVarChar).Value = pos_Customer.ExtraFiled2;
            cmd.Parameters.Add("@ExtraFiled3", SqlDbType.NVarChar).Value = pos_Customer.ExtraFiled3;
            cmd.Parameters.Add("@ExtraFiled4", SqlDbType.NVarChar).Value = pos_Customer.ExtraFiled4;
            cmd.Parameters.Add("@ExtraFiled5", SqlDbType.NVarChar).Value = pos_Customer.ExtraFiled5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = pos_Customer.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = pos_Customer.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = pos_Customer.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = pos_Customer.UpdatedDate;
            cmd.Parameters.Add("@RowSatatusID", SqlDbType.Int).Value = pos_Customer.RowSatatusID;
            cmd.Parameters.Add("@DateofBirth", SqlDbType.DateTime).Value = pos_Customer.DateofBirth;
            cmd.Parameters.Add("@ApplicationDate", SqlDbType.DateTime).Value = pos_Customer.ApplicationDate;
            cmd.Parameters.Add("@CardIssueDate", SqlDbType.DateTime).Value = pos_Customer.CardIssueDate;
            cmd.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = pos_Customer.ExpireDate;
            cmd.Parameters.Add("@CardType", SqlDbType.NVarChar).Value = pos_Customer.CardType;
            cmd.Parameters.Add("@ApprovedBy", SqlDbType.NVarChar).Value = pos_Customer.ApprovedBy;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }

    public List<Pos_Customer> GetAllPos_customersBySearchArg(string cardNo, string mobileNo)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllPos_customersBySearchArg", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CardNo", SqlDbType.VarChar).Value = cardNo;
            command.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = mobileNo;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);
            return GetPos_CustomersFromReader(reader);
        }
    }
}
