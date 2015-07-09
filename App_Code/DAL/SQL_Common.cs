using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

public class SQL_Common:DataAccessObject
{
    public SQL_Common()
    {
    }


    public DataSet COMN_ExecSQL(string sql)
    {
        DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("GP_COMN_ExecSQL", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SQL", sql);
                connection.Open();
                SqlDataAdapter myadapter = new SqlDataAdapter(command);
                myadapter.Fill(ds);
                myadapter.Dispose();
                connection.Close();
                return ds;
            }
        }
    }


    public string ProcessDataBackup()
    {
        DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("Create_DB_Backup", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataAdapter myadapter = new SqlDataAdapter(command);
                myadapter.Fill(ds);
                myadapter.Dispose();
                connection.Close();
                return ds.Tables[0].Rows[0][0].ToString();
            }
        }
    }

    public void ProcessDataBackupAuto()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Create_Auto_DB_Backup", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return;
        }
    }
}

