using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

public class SqlCustomerProvider:DataAccessObject
{
	public SqlCustomerProvider()
    {
    }


    public DataSet  GetAllCustomers()
    {
        DataSet Customers = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllCustomers", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Customers);
            myadapter.Dispose();
            connection.Close();

            return Customers;
        }
    }
	public DataSet GetCustomerPageWise(int pageIndex, int PageSize, out int recordCount)
    {
    DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("GetCustomerPageWise", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PageIndex", pageIndex);
                command.Parameters.AddWithValue("@PageSize",  PageSize );
                command.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                command.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                connection.Open();
                SqlDataAdapter myadapter = new SqlDataAdapter(command);
                myadapter.Fill(ds);
                myadapter.Dispose();
                connection.Close();
                recordCount = Convert.ToInt32(command.Parameters["@RecordCount"].Value);
                return ds;
            }
        }
    }


    public Customer  GetCustomerByRelationshipID(int  relationshipID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetCustomerByRelationshipID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@RelationshipID", SqlDbType.NVarChar).Value = relationshipID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetCustomerFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public Customer  GetCustomerByIncomeID(int  incomeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetCustomerByIncomeID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@IncomeID", SqlDbType.NVarChar).Value = incomeID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetCustomerFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public DataSet  GetDropDownLisAllCustomer()
    {
        DataSet Customers = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetDropDownListAllCustomer", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Customers);
            myadapter.Dispose();
            connection.Close();

            return Customers;
        }
    }

    public DataSet   GetAllCustomersWithRelation()
    {
       DataSet ds = new DataSet();
       using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllCustomersWithRelation", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(ds);
            myadapter.Dispose();
            connection.Close();

            return ds;
        }
    }
    public List<Customer> GetCustomersFromReader(IDataReader reader)
    {
        List<Customer> customers = new List<Customer>();

        while (reader.Read())
        {
            customers.Add(GetCustomerFromReader(reader));
        }
        return customers;
    }

    public Customer GetCustomerFromReader(IDataReader reader)
    {
        try
        {
            Customer customer = new Customer
                (

                     DataAccessObject.IsNULL<int>(reader["CustomerID"]),
                     DataAccessObject.IsNULL<string>(reader["FirstName"]),
                     DataAccessObject.IsNULL<string>(reader["LastName"]),
                     DataAccessObject.IsNULL<int>(reader["Age"]),
                     DataAccessObject.IsNULL<string>(reader["GuestName"]),
                     DataAccessObject.IsNULL<int>(reader["GuestAge"]),
                     DataAccessObject.IsNULL<int>(reader["RelationshipID"]),
                     DataAccessObject.IsNULL<int>(reader["IncomeID"]),
                     DataAccessObject.IsNULL<string>(reader["PrimaryPhone"]),
                     DataAccessObject.IsNULL<string>(reader["SecondaryPhone"]),
                     DataAccessObject.IsNULL<string>(reader["Address1"]),
                     DataAccessObject.IsNULL<string>(reader["Address2"]),
                     DataAccessObject.IsNULL<string>(reader["City"]),
                     DataAccessObject.IsNULL<int>(reader["State"]),
                      DataAccessObject.IsNULL<string>(reader["Zipcode"]), 
                     DataAccessObject.IsNULL<int>(reader["Country"]),
                     DataAccessObject.IsNULL<string>(reader["Business"]),
                     DataAccessObject.IsNULL<string>(reader["Email"]),
                     DataAccessObject.IsNULL<string>(reader["GuestAccompany"]),
                     DataAccessObject.IsNULL<string>(reader["GuestFirstname"]),
                     DataAccessObject.IsNULL<string>(reader["GuestLastname"]),
                     DataAccessObject.IsNULL<string>(reader["GuestPhone"]),
                     DataAccessObject.IsNULL<string>(reader["GuestEmail"]),
                     DataAccessObject.IsNULL<string>(reader["GuestAddress"]),
                     DataAccessObject.IsNULL<string>(reader["GuestCity"]),
                     DataAccessObject.IsNULL<string>(reader["Gueststate"]),
                     DataAccessObject.IsNULL<string>(reader["GuestZip"]),
                     DataAccessObject.IsNULL<string>(reader["GuestNotes"])

                );
             return customer;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Customer  GetCustomerByCustomerID(int  customerID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetCustomerByCustomerID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = customerID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetCustomerFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public int InsertCustomer(Customer customer)
    {

        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("InsertCustomer", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = customer.FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = customer.LastName;
            cmd.Parameters.Add("@Age", SqlDbType.Int).Value = customer.Age;
            cmd.Parameters.Add("@GuestName", SqlDbType.NVarChar).Value = customer.GuestName;
            cmd.Parameters.Add("@GuestAge", SqlDbType.Int).Value = customer.GuestAge;
            cmd.Parameters.Add("@RelationshipID", SqlDbType.Int).Value = customer.RelationshipID;
            cmd.Parameters.Add("@IncomeID", SqlDbType.Int).Value = customer.IncomeID;
            cmd.Parameters.Add("@PrimaryPhone", SqlDbType.NVarChar).Value = customer.PrimaryPhone;
            cmd.Parameters.Add("@SecondaryPhone", SqlDbType.NVarChar).Value = customer.SecondaryPhone;
            cmd.Parameters.Add("@Address1", SqlDbType.NVarChar).Value = customer.Address1;
            cmd.Parameters.Add("@Address2", SqlDbType.NVarChar).Value = customer.Address2;
            cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = customer.City;
            cmd.Parameters.Add("@State", SqlDbType.Int).Value = customer.State;
            cmd.Parameters.Add("@Zipcode", SqlDbType.NVarChar).Value = customer.Zipcode;
            cmd.Parameters.Add("@Country", SqlDbType.Int).Value = customer.Country;
            cmd.Parameters.Add("@Business", SqlDbType.NVarChar).Value = customer.Business;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = customer.Email;
            cmd.Parameters.Add("@GuestAccompany", SqlDbType.NVarChar).Value = customer.GuestAccompany;
            cmd.Parameters.Add("@GuestFirstname", SqlDbType.NVarChar).Value = customer.GuestFirstname;
            cmd.Parameters.Add("@GuestLastname", SqlDbType.NVarChar).Value = customer.GuestLastname;
            cmd.Parameters.Add("@GuestPhone", SqlDbType.NVarChar).Value = customer.GuestPhone;
            cmd.Parameters.Add("@GuestEmail", SqlDbType.NVarChar).Value = customer.GuestEmail;
            cmd.Parameters.Add("@GuestAddress", SqlDbType.NVarChar).Value = customer.GuestAddress;
            cmd.Parameters.Add("@GuestCity", SqlDbType.NVarChar).Value = customer.GuestCity;
            cmd.Parameters.Add("@Gueststate", SqlDbType.NVarChar).Value = customer.Gueststate;
            cmd.Parameters.Add("@GuestZip", SqlDbType.NVarChar).Value = customer.GuestZip;
            cmd.Parameters.Add("@GuestNotes", SqlDbType.NVarChar).Value = customer.GuestNotes;
            
            connection.Open();
            
            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@CustomerID"].Value;
        }
    }

    public bool UpdateCustomer(Customer customer)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateCustomer", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = customer.CustomerID;
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = customer.FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = customer.LastName;
            cmd.Parameters.Add("@Age", SqlDbType.Int).Value = customer.Age;
            cmd.Parameters.Add("@GuestName", SqlDbType.NVarChar).Value = customer.GuestName;
            cmd.Parameters.Add("@GuestAge", SqlDbType.Int).Value = customer.GuestAge;
            cmd.Parameters.Add("@RelationshipID", SqlDbType.Int).Value = customer.RelationshipID;
            cmd.Parameters.Add("@IncomeID", SqlDbType.Int).Value = customer.IncomeID;
            cmd.Parameters.Add("@PrimaryPhone", SqlDbType.NVarChar).Value = customer.PrimaryPhone;
            cmd.Parameters.Add("@SecondaryPhone", SqlDbType.NVarChar).Value = customer.SecondaryPhone;
            cmd.Parameters.Add("@Address1", SqlDbType.NVarChar).Value = customer.Address1;
            cmd.Parameters.Add("@Address2", SqlDbType.NVarChar).Value = customer.Address2;
            cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = customer.City;
            cmd.Parameters.Add("@State", SqlDbType.Int).Value = customer.State;
            cmd.Parameters.Add("@Zipcode", SqlDbType.NVarChar).Value = customer.Zipcode;
            cmd.Parameters.Add("@Country", SqlDbType.Int).Value = customer.Country;
            cmd.Parameters.Add("@Business", SqlDbType.NVarChar).Value = customer.Business;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = customer.Email;
            cmd.Parameters.Add("@GuestAccompany", SqlDbType.NVarChar).Value = customer.GuestAccompany;
            cmd.Parameters.Add("@GuestFirstname", SqlDbType.NVarChar).Value = customer.GuestFirstname;
            cmd.Parameters.Add("@GuestLastname", SqlDbType.NVarChar).Value = customer.GuestLastname;
            cmd.Parameters.Add("@GuestPhone", SqlDbType.NVarChar).Value = customer.GuestPhone;
            cmd.Parameters.Add("@GuestEmail", SqlDbType.NVarChar).Value = customer.GuestEmail;
            cmd.Parameters.Add("@GuestAddress", SqlDbType.NVarChar).Value = customer.GuestAddress;
            cmd.Parameters.Add("@GuestCity", SqlDbType.NVarChar).Value = customer.GuestCity;
            cmd.Parameters.Add("@Gueststate", SqlDbType.NVarChar).Value = customer.Gueststate;
            cmd.Parameters.Add("@GuestZip", SqlDbType.NVarChar).Value = customer.GuestZip;
            cmd.Parameters.Add("@GuestNotes", SqlDbType.NVarChar).Value = customer.GuestNotes;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }

    public bool DeleteCustomer(int customerID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteCustomer", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = customerID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }
}

