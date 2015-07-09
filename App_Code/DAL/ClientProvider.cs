using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

public class SqlClientProvider:DataAccessObject
{
	public SqlClientProvider()
    {
    }


    public DataSet  GetAllClients()
    {
        DataSet Clients = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllClients", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Clients);
            myadapter.Dispose();
            connection.Close();

            return Clients;
        }
    }
    public DataSet GetCallLogPageWise(string search_val, int pageIndex, int PageSize, out int recordCount, int LocationID, DateTime FromDate, DateTime ToDate, String UserID, string SignUp)
    {
        DataSet ds = new DataSet();

        if (UserID != "0")
        {
            var myGuid = new Guid(UserID);
        }


        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("GetCallLogPageWise", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Search", search_val);
                command.Parameters.AddWithValue("@PageIndex", pageIndex);
                command.Parameters.AddWithValue("@PageSize", PageSize);
                command.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                command.Parameters.Add("@LocationID", LocationID);
                if (UserID != "0")
                {
                    var myGuid = new Guid(UserID);
                    command.Parameters.Add("@UserID", myGuid);
                }
                else
                {
                    command.Parameters.Add("@UserID", "00000000-0000-0000-0000-000000000000");
                }

                command.Parameters.Add("@FromDate", FromDate);
                command.Parameters.Add("@SignUp", SignUp);
                command.Parameters.Add("@ToDate", ToDate);
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
    public DataSet GetClientPageWise(string search_val, int pageIndex, int PageSize, out int recordCount, int LocationID, DateTime FromDate, DateTime ToDate)
    {
    DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("GetClientPageWise", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Search", search_val);
                command.Parameters.AddWithValue("@PageIndex", pageIndex);
                command.Parameters.AddWithValue("@PageSize",  PageSize );
                command.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                command.Parameters.Add("@LocationID", LocationID);
                command.Parameters.Add("@FromDate", FromDate);
                command.Parameters.Add("@ToDate", ToDate);
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
    public DataSet LoadClientPaymentHistoryPage(string search_val, int pageIndex, int PageSize, out int recordCount)
    {
    DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("GetClientPaymentPageWise", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Search", search_val);
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

    public string GetClientMaxApptDate(int clientID)
    {
        string Maxdate = "";
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            DataSet ds = new DataSet();
            SqlCommand command = new SqlCommand("GetMaxApptdate", connection);
            command.Parameters.Add("@ClientID", SqlDbType.Int).Value = clientID;
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(ds);
            myadapter.Dispose();
            connection.Close();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Maxdate = ds.Tables[0].Rows[0][0].ToString();
                }
            }

        }
        return Maxdate;
    }

    public Client  GetClientByCityID(int  cityID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetClientByCityID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CityID", SqlDbType.NVarChar).Value = cityID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetClientFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public Client  GetClientByMetroLocationID(int  metroLocationID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetClientByMetroLocationID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MetroLocationID", SqlDbType.NVarChar).Value = metroLocationID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetClientFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public DataSet  GetDropDownLisAllClient()
    {
        DataSet Clients = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetDropDownListAllClient", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Clients);
            myadapter.Dispose();
            connection.Close();

            return Clients;
        }
    }

    public DataSet   GetAllClientsWithRelation()
    {
       DataSet ds = new DataSet();
       using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllClientsWithRelation", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(ds);
            myadapter.Dispose();
            connection.Close();

            return ds;
        }
    }
    public List<Client> GetClientsFromReader(IDataReader reader)
    {
        List<Client> clients = new List<Client>();

        while (reader.Read())
        {
            clients.Add(GetClientFromReader(reader));
        }
        return clients;
    }

    public Client GetClientFromReader(IDataReader reader)
    {
        try
        {
            Client client = new Client
                (

                     DataAccessObject.IsNULL<int>(reader["ClientID"]),
                     DataAccessObject.IsNULL<string>(reader["FirstName"]),
                     DataAccessObject.IsNULL<string>(reader["LastName"]),
                     DataAccessObject.IsNULL<int>(reader["Age"]),
                     DataAccessObject.IsNULL<string>(reader["PrimaryPhone"]),
                     DataAccessObject.IsNULL<string>(reader["SecondaryPhone"]),
                     DataAccessObject.IsNULL<string>(reader["Email"]),
                     DataAccessObject.IsNULL<string>(reader["Address"]),
                     DataAccessObject.IsNULL<string>(reader["City"]),
                     DataAccessObject.IsNULL<string>(reader["State"]),
                     DataAccessObject.IsNULL<string>(reader["ZipCode"]),
                     DataAccessObject.IsNULL<string>(reader["Sex"]),
                     DataAccessObject.IsNULL<int>(reader["MetroLocationID"]),
                     DataAccessObject.IsNULL<int>(reader["ReferralSourceID"]),
                     DataAccessObject.IsNULL<string>(reader["Notes"]),
                     DataAccessObject.IsNULL<Boolean>(reader["IsSignedUp"]),

                           DataAccessObject.IsNULL<string>(reader["SignUpMessage"]),
                     DataAccessObject.IsNULL<string>(reader["CreatedBy"].ToString()),
                     DataAccessObject.IsNULL<DateTime>(reader["CreatedDate"])
                );
             return client;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Client  GetClientByClientID(int  clientID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetClientByClientID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ClientID", SqlDbType.Int).Value = clientID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetClientFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public int InsertClient(Client client)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("InsertClient", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ClientID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = client.FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = client.LastName;
            cmd.Parameters.Add("@Age", SqlDbType.Int).Value = client.Age;
            cmd.Parameters.Add("@PrimaryPhone", SqlDbType.NVarChar).Value = client.PrimaryPhone;
            cmd.Parameters.Add("@SecondaryPhone", SqlDbType.NVarChar).Value = client.SecondaryPhone;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = client.Email;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = client.Address;
            cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = client.City;
            cmd.Parameters.Add("@State", SqlDbType.NVarChar).Value = client.State;
            cmd.Parameters.Add("@ZipCode", SqlDbType.NVarChar).Value = client.ZipCode;
            cmd.Parameters.Add("@Sex", SqlDbType.NVarChar).Value = client.Sex;
            cmd.Parameters.Add("@MetroLocationID", SqlDbType.Int).Value = client.MetroLocationID;
            cmd.Parameters.Add("@ReferralSourceID", SqlDbType.Int).Value = client.ReferralSourceID;
            cmd.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = client.Notes;
            cmd.Parameters.Add("@IsSignedUp", SqlDbType.Bit).Value = client.IsSignedUp;

            cmd.Parameters.Add("@SignUpMessage", SqlDbType.NVarChar).Value = client.SignUpMessage;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = client.CreatedBy;
            cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = client.CreatedDate;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@ClientID"].Value;
        }
    }

    public bool UpdateClient(Client client)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateClient", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = client.ClientID;
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = client.FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = client.LastName;
            cmd.Parameters.Add("@Age", SqlDbType.Int).Value = client.Age;
            cmd.Parameters.Add("@PrimaryPhone", SqlDbType.NVarChar).Value = client.PrimaryPhone;
            cmd.Parameters.Add("@SecondaryPhone", SqlDbType.NVarChar).Value = client.SecondaryPhone;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = client.Email;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = client.Address;
            cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = client.City;
            cmd.Parameters.Add("@State", SqlDbType.NVarChar).Value = client.State;
            cmd.Parameters.Add("@ZipCode", SqlDbType.NVarChar).Value = client.ZipCode;
            cmd.Parameters.Add("@Sex", SqlDbType.NVarChar).Value = client.Sex;
            cmd.Parameters.Add("@MetroLocationID", SqlDbType.Int).Value = client.MetroLocationID;
            cmd.Parameters.Add("@ReferralSourceID", SqlDbType.Int).Value = client.ReferralSourceID;
            cmd.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = client.Notes;
            cmd.Parameters.Add("@IsSignedUp", SqlDbType.Bit).Value = client.IsSignedUp;
            cmd.Parameters.Add("@SignUpMessage", SqlDbType.NVarChar).Value = client.SignUpMessage;
            //cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = client.CreatedBy;
          //  cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = client.CreatedDate;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }

    public bool DeleteClient(int clientID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteClient", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = clientID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }
}

