using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

public class SqlFormRightsProvider:DataAccessObject
{
	public SqlFormRightsProvider()
    {
    }


    public DataSet  GetAllFormRightss()
    {
        DataSet FormRightss = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllFormRightss", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(FormRightss);
            myadapter.Dispose();
            connection.Close();

            return FormRightss;
        }
    }
	public DataSet GetFormRightsPageWise(int pageIndex, int PageSize, out int recordCount)
    {
    DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("GetFormRightsPageWise", connection))
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


    public FormRights  GetFormRightsByFormID(int  formID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetFormRightsByFormID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@FormID", SqlDbType.NVarChar).Value = formID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetFormRightsFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public DataSet GetFormRightsByUserID(string userID)
    {
        DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetFormRightsByUserID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = userID;
            connection.Open();
            
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(ds);
            myadapter.Dispose();
            connection.Close();
           // recordCount = Convert.ToInt32(command.Parameters["@RecordCount"].Value);
            return ds;
        }
    }

    public DataSet  GetDropDownLisAllFormRights()
    {
        DataSet FormRightss = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetDropDownListAllFormRights", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(FormRightss);
            myadapter.Dispose();
            connection.Close();

            return FormRightss;
        }
    }
    public List<FormRights> GetFormRightssFromReader(IDataReader reader)
    {
        List<FormRights> formRightss = new List<FormRights>();

        while (reader.Read())
        {
            formRightss.Add(GetFormRightsFromReader(reader));
        }
        return formRightss;
    }

    public FormRights GetFormRightsFromReader(IDataReader reader)
    {
        try
        {
            FormRights formRights = new FormRights
                (

                     DataAccessObject.IsNULL<int>(reader["FormRightsID"]),
                     DataAccessObject.IsNULL<int>(reader["FormID"]),
                     DataAccessObject.IsNULL<string>(reader["UserID"].ToString()),
                     DataAccessObject.IsNULL<bool>(reader["InsertRight"]),
                     DataAccessObject.IsNULL<bool>(reader["UpdateRight"]),
                     DataAccessObject.IsNULL<bool>(reader["DeleteRight"]),
                     DataAccessObject.IsNULL<bool>(reader["SelectRight"])
                );
             return formRights;
        }
        catch(Exception ex)
        {
            return null;
        }
    }
    
    public FormRights  GetFormRightsByFormRightsID(int  formRightsID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetFormRightsByFormRightsID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@FormRightsID", SqlDbType.Int).Value = formRightsID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetFormRightsFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }
    public FormRights GetFormRightsByUserIDFormID(string FormID,string UserID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetFormRightsByUserIDFormID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@PageName", SqlDbType.NVarChar).Value = FormID;
            command.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = UserID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetFormRightsFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }
    public int InsertFormRights(FormRights formRights)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("InsertFormRights", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FormRightsID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@FormID", SqlDbType.Int).Value = formRights.FormID;
            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = formRights.UserID;
            cmd.Parameters.Add("@InsertRight", SqlDbType.Bit).Value = formRights.InsertRight;
            cmd.Parameters.Add("@UpdateRight", SqlDbType.Bit).Value = formRights.UpdateRight;
            cmd.Parameters.Add("@DeleteRight", SqlDbType.Bit).Value = formRights.DeleteRight;
            cmd.Parameters.Add("@SelectRight", SqlDbType.Bit).Value = formRights.SelectRight;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@FormRightsID"].Value;
        }
    }

    public bool UpdateFormRights(FormRights formRights)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateFormRights", connection);
            cmd.CommandType = CommandType.StoredProcedure;
          //  cmd.Parameters.Add("@FormRightsID", SqlDbType.Int).Value = formRights.FormRightsID;
            cmd.Parameters.Add("@FormID", SqlDbType.Int).Value = formRights.FormID;
            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = formRights.UserID;
            cmd.Parameters.Add("@InsertRight", SqlDbType.Bit).Value = formRights.InsertRight;
            cmd.Parameters.Add("@UpdateRight", SqlDbType.Bit).Value = formRights.UpdateRight;
            cmd.Parameters.Add("@DeleteRight", SqlDbType.Bit).Value = formRights.DeleteRight;
            cmd.Parameters.Add("@SelectRight", SqlDbType.Bit).Value = formRights.SelectRight;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }

    public bool DeleteFormRights(int formRightsID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteFormRights", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FormRightsID", SqlDbType.Int).Value = formRightsID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }
}

