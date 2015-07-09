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

public class SqlPageRoleProvider:DataAccessObject
{
	public SqlPageRoleProvider()
    {
    }


    public bool DeletePageRole(int pageRoleID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_DeletePageRole", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PageRoleID", SqlDbType.Int).Value = pageRoleID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<PageRole> GetAllPageRoles()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetAllPageRoles", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetPageRolesFromReader(reader);
        }
    }
    public List<PageRole> GetPageRolesFromReader(IDataReader reader)
    {
        List<PageRole> pageRoles = new List<PageRole>();

        while (reader.Read())
        {
            pageRoles.Add(GetPageRoleFromReader(reader));
        }
        return pageRoles;
    }

    public PageRole GetPageRoleFromReader(IDataReader reader)
    {
        try
        {
            PageRole pageRole = new PageRole
                (
                    (int)reader["PageRoleID"],
                    (int)reader["PageID"],
                    (int)reader["RoleID"],
                    (DateTime)reader["AddedDate"],
                    reader["AddedBy"].ToString(),
                    (DateTime)reader["ModifyDate"],
                    reader["ModifyBy"].ToString(),
                    (int)reader["RowStatusID"]
                );
             return pageRole;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public PageRole GetPageRoleByID(int pageRoleID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetPageRoleByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@PageRoleID", SqlDbType.Int).Value = pageRoleID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetPageRoleFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertPageRole(PageRole pageRole)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_InsertPageRole", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PageRoleID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@PageID", SqlDbType.Int).Value = pageRole.PageID;
            cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = pageRole.RoleID;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = pageRole.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = pageRole.AddedBy;
            cmd.Parameters.Add("@ModifyDate", SqlDbType.DateTime).Value = pageRole.ModifyDate;
            cmd.Parameters.Add("@ModifyBy", SqlDbType.NVarChar).Value = pageRole.ModifyBy;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = pageRole.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@PageRoleID"].Value;
        }
    }

    public bool UpdatePageRole(PageRole pageRole)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_UpdatePageRole", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PageRoleID", SqlDbType.Int).Value = pageRole.PageRoleID;
            cmd.Parameters.Add("@PageID", SqlDbType.Int).Value = pageRole.PageID;
            cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = pageRole.RoleID;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = pageRole.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = pageRole.AddedBy;
            cmd.Parameters.Add("@ModifyDate", SqlDbType.DateTime).Value = pageRole.ModifyDate;
            cmd.Parameters.Add("@ModifyBy", SqlDbType.NVarChar).Value = pageRole.ModifyBy;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = pageRole.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
