using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

public class SqlAppointmnetProvider:DataAccessObject
{
	public SqlAppointmnetProvider()
    {
    }


    public DataSet  GetAllAppointmnets()
    {
        DataSet Appointmnets = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllAppointmnets", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Appointmnets);
            myadapter.Dispose();
            connection.Close();

            return Appointmnets;
        }
    }
	public DataSet GetAppointmnetPageWise(int pageIndex, int PageSize, out int recordCount)
    {
    DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("GetAppointmnetPageWise", connection))
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


    public Appointmnet  GetAppointmnetByClientID(int  clientID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAppointmnetByClientID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ClientID", SqlDbType.NVarChar).Value = clientID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetAppointmnetFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public Appointmnet  GetAppointmnetByMetroLocationID(int  metroLocationID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAppointmnetByMetroLocationID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MetroLocationID", SqlDbType.NVarChar).Value = metroLocationID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetAppointmnetFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public Appointmnet  GetAppointmnetByStastusID(int  stastusID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAppointmnetByStastusID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@StastusID", SqlDbType.NVarChar).Value = stastusID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetAppointmnetFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public DataSet  GetDropDownLisAllAppointmnet()
    {
        DataSet Appointmnets = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetDropDownListAllAppointmnet", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Appointmnets);
            myadapter.Dispose();
            connection.Close();

            return Appointmnets;
        }
    }
    public DataSet GetAllAppointmnetByClientID(int clientID)
    {
        DataSet Appointmnets = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllAppointmnetByClientID", connection);
            command.Parameters.Add("@ClientID", SqlDbType.NVarChar).Value = clientID;
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Appointmnets);
            myadapter.Dispose();
            connection.Close();

            return Appointmnets;
        }
    }
    public DataSet GetAllAppointmnetByMetroLocationID(int MetroLocationID)
    {
        DataSet Appointmnets = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllAppointmnetByMetroLocationID", connection);
            command.Parameters.Add("@MetroLocationID", SqlDbType.NVarChar).Value = MetroLocationID;
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Appointmnets);
            myadapter.Dispose();
            connection.Close();

            return Appointmnets;
        }
    }

    //SearchAppointMent
    public DataSet SearchAppointMent(string MetroLocationID,string dateFrom,string DateTo)
    {
        DataSet Appointmnets = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("SearchAppointMent", connection);
            command.Parameters.Add("@MetroLocationID", SqlDbType.NVarChar).Value = MetroLocationID;
            command.Parameters.Add("@dateFrom", SqlDbType.NVarChar).Value = dateFrom;
            command.Parameters.Add("@DateTo", SqlDbType.NVarChar).Value = DateTo;
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Appointmnets);
            myadapter.Dispose();
            connection.Close();

            return Appointmnets;
        }
    }
    
    public List<Appointmnet> GetAppointmnetsFromReader(IDataReader reader)
    {
        List<Appointmnet> appointmnets = new List<Appointmnet>();

        while (reader.Read())
        {
            appointmnets.Add(GetAppointmnetFromReader(reader));
        }
        return appointmnets;
    }

    public Appointmnet GetAppointmnetFromReader(IDataReader reader)
    {
        try
        {
            Appointmnet appointmnet = new Appointmnet
                (

                     DataAccessObject.IsNULL<int>(reader["AppointmnetID"]),
                     DataAccessObject.IsNULL<int>(reader["ClientID"]),
                     DataAccessObject.IsNULL<DateTime>(reader["Date"]),
                     DataAccessObject.IsNULL<string>(reader["Time"]),
                     DataAccessObject.IsNULL<string>(reader["CreateBy"].ToString()),
                     DataAccessObject.IsNULL<string>(reader["City"]),
                     DataAccessObject.IsNULL<int>(reader["MetroLocationID"]),
                     DataAccessObject.IsNULL<int>(reader["StastusID"]),
                     DataAccessObject.IsNULL<DateTime>(reader["CreatedDate"])
                );
             return appointmnet;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Appointmnet  GetAppointmnetByAppointmnetID(int  appointmnetID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAppointmnetByAppointmnetID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@AppointmnetID", SqlDbType.Int).Value = appointmnetID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetAppointmnetFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public int InsertAppointmnet(Appointmnet appointmnet)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("InsertAppointmnet", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AppointmnetID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = appointmnet.ClientID;
            cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = appointmnet.Date;
            cmd.Parameters.Add("@Time", SqlDbType.NVarChar).Value = appointmnet.Time;
            cmd.Parameters.Add("@CreateBy", SqlDbType.NVarChar).Value = appointmnet.CreateBy;
           // cmd.Parameters.Add("@City", SqlDbType.Int).Value = appointmnet.City;
            cmd.Parameters.Add("@MetroLocationID", SqlDbType.Int).Value = appointmnet.MetroLocationID;
            cmd.Parameters.Add("@StastusID", SqlDbType.Int).Value = appointmnet.StastusID;
            cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = appointmnet.CreatedDate;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@AppointmnetID"].Value;
        }
    }

    public bool UpdateAppointmnet(Appointmnet appointmnet)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateAppointmnet", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AppointmnetID", SqlDbType.Int).Value = appointmnet.AppointmnetID;
            cmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = appointmnet.ClientID;
            cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = appointmnet.Date;
            cmd.Parameters.Add("@Time", SqlDbType.NVarChar).Value = appointmnet.Time;
          //  cmd.Parameters.Add("@CreateBy", SqlDbType.NVarChar).Value = appointmnet.CreateBy;
           // cmd.Parameters.Add("@City", SqlDbType.Int).Value = appointmnet.City;
            cmd.Parameters.Add("@MetroLocationID", SqlDbType.Int).Value = appointmnet.MetroLocationID;
            cmd.Parameters.Add("@StastusID", SqlDbType.Int).Value = appointmnet.StastusID;
           // cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = appointmnet.CreatedDate;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }

    public bool UpdateAppointmnetStatus(int AppointmnetID, int StastusID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateAppointmnetStatus", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AppointmnetID", SqlDbType.Int).Value =  AppointmnetID;
          
          
            
            cmd.Parameters.Add("@StastusID", SqlDbType.Int).Value = StastusID;
           // cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = appointmnet.CreatedDate;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
    
    public bool DeleteAppointmnet(int appointmnetID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteAppointmnet", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AppointmnetID", SqlDbType.Int).Value = appointmnetID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }
}

