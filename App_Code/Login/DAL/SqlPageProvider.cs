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

public class SqlPageProvider:DataAccessObject
{
	public SqlPageProvider()
    {
    }


    public bool DeletePage(int pageID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_DeletePage", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PageID", SqlDbType.Int).Value = pageID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public DataSet GetAllPagenMenuByModuleID(int ModuleID)
    {
        DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("Login_GetAllPagenMenuByModuleID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ModuleID", ModuleID);
                connection.Open();
                SqlDataAdapter myadapter = new SqlDataAdapter(command);
                myadapter.Fill(ds);
                myadapter.Dispose();
                connection.Close();
                return ds;
            }
        }
    }


    public List<Page> GetAllPages()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetAllPages", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetPagesFromReader(reader);
        }
    }

    public List<Page> GetAllPagesByModuleID(int moduleID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetAllPagesByModuleID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ModuleID", SqlDbType.Int).Value = moduleID;

            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetPagesFromReader(reader);
        }
    }

    public List<Page> GetPagesFromReader(IDataReader reader)
    {
        List<Page> pages = new List<Page>();

        while (reader.Read())
        {
            pages.Add(GetPageFromReader(reader));
        }
        return pages;
    }

    public Page GetPageFromReader(IDataReader reader)
    {
        try
        {
            Page page = new Page
                (
                    (int)reader["PageID"],
                    reader["PageTitle"].ToString(),
                    reader["PageURL"].ToString(),
                    (int)reader["ModuleID"],
                    reader["AddedBy"].ToString(),
                    (DateTime)reader["AddedDate"],
                    reader["UpdatedBy"].ToString(),
                    (DateTime)reader["UpdatedDate"],
                    (int)reader["RowStatusID"]
                );
             return page;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Page GetPageByID(int pageID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetPageByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@PageID", SqlDbType.Int).Value = pageID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetPageFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertPage(Page page)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_InsertPage", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PageID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@PageTitle", SqlDbType.NVarChar).Value = page.PageTitle;
            cmd.Parameters.Add("@PageURL", SqlDbType.NVarChar).Value = page.PageURL;
            cmd.Parameters.Add("@ModuleID", SqlDbType.Int).Value = page.ModuleID;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = page.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = page.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar).Value = page.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = page.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = page.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@PageID"].Value;
        }
    }

    public bool UpdatePage(Page page)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_UpdatePage", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PageID", SqlDbType.Int).Value = page.PageID;
            cmd.Parameters.Add("@PageTitle", SqlDbType.NVarChar).Value = page.PageTitle;
            cmd.Parameters.Add("@PageURL", SqlDbType.NVarChar).Value = page.PageURL;
            cmd.Parameters.Add("@ModuleID", SqlDbType.Int).Value = page.ModuleID;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = page.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = page.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar).Value = page.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = page.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = page.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
