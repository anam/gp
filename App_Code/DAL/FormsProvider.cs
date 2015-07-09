using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

public class SqlFormsProvider:DataAccessObject
{
	public SqlFormsProvider()
    {
    }


    public DataSet  GetAllFormss()
    {
        DataSet Formss = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllFormss", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Formss);
            myadapter.Dispose();
            connection.Close();

            return Formss;
        }
    }
	public DataSet GetFormsPageWise(int pageIndex, int PageSize, out int recordCount)
    {
    DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("GetFormsPageWise", connection))
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


    public DataSet  GetDropDownLisAllForms()
    {
        DataSet Formss = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetDropDownListAllForms", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Formss);
            myadapter.Dispose();
            connection.Close();

            return Formss;
        }
    }
    public List<Forms> GetFormssFromReader(IDataReader reader)
    {
        List<Forms> formss = new List<Forms>();

        while (reader.Read())
        {
            formss.Add(GetFormsFromReader(reader));
        }
        return formss;
    }

    public Forms GetFormsFromReader(IDataReader reader)
    {
        try
        {
            Forms forms = new Forms
                (

                     DataAccessObject.IsNULL<int>(reader["FormsID"]),
                     DataAccessObject.IsNULL<string>(reader["Name"]),
                     DataAccessObject.IsNULL<string>(reader["FormPrefix"]),
                     DataAccessObject.IsNULL<string>(reader["Description"])
                );
             return forms;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Forms  GetFormsByFormsID(int  formsID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetFormsByFormsID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@FormsID", SqlDbType.Int).Value = formsID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetFormsFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public int InsertForms(Forms forms)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("InsertForms", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FormsID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = forms.Name;
            cmd.Parameters.Add("@FormPrefix", SqlDbType.NVarChar).Value = forms.FormPrefix;
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = forms.Description;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@FormsID"].Value;
        }
    }

    public bool UpdateForms(Forms forms)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateForms", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FormsID", SqlDbType.Int).Value = forms.FormsID;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = forms.Name;
            cmd.Parameters.Add("@FormPrefix", SqlDbType.NVarChar).Value = forms.FormPrefix;
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = forms.Description;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }

    public bool DeleteForms(int formsID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteForms", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FormsID", SqlDbType.Int).Value = formsID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }
}

