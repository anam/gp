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

public class SqlMenuRoleProvider:DataAccessObject
{
	public SqlMenuRoleProvider()
    {
    }


    public bool DeleteMenuRole(int menuRoleID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_DeleteMenuRole", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MenuRoleID", SqlDbType.Int).Value = menuRoleID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<MenuRole> GetAllMenuRoles()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetAllMenuRoles", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetMenuRolesFromReader(reader);
        }
    }
    public List<MenuRole> GetMenuRolesFromReader(IDataReader reader)
    {
        List<MenuRole> menuRoles = new List<MenuRole>();

        while (reader.Read())
        {
            menuRoles.Add(GetMenuRoleFromReader(reader));
        }
        return menuRoles;
    }

    public MenuRole GetMenuRoleFromReader(IDataReader reader)
    {
        try
        {
            MenuRole menuRole = new MenuRole
                (
                    (int)reader["MenuRoleID"],
                    (int)reader["MenuID"],
                    (int)reader["RoleID"],
                    (DateTime)reader["AddedDate"],
                    reader["AddedBy"].ToString(),
                    (DateTime)reader["ModifyDate"],
                    reader["ModifyBy"].ToString(),
                    (int)reader["RowStatusID"]
                );
             return menuRole;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public MenuRole GetMenuRoleByID(int menuRoleID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetMenuRoleByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MenuRoleID", SqlDbType.Int).Value = menuRoleID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetMenuRoleFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertMenuRole(MenuRole menuRole)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_InsertMenuRole", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MenuRoleID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@MenuID", SqlDbType.Int).Value = menuRole.MenuID;
            cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = menuRole.RoleID;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = menuRole.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = menuRole.AddedBy;
            cmd.Parameters.Add("@ModifyDate", SqlDbType.DateTime).Value = menuRole.ModifyDate;
            cmd.Parameters.Add("@ModifyBy", SqlDbType.NVarChar).Value = menuRole.ModifyBy;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = menuRole.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@MenuRoleID"].Value;
        }
    }

    public bool UpdateMenuRole(MenuRole menuRole)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_UpdateMenuRole", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MenuRoleID", SqlDbType.Int).Value = menuRole.MenuRoleID;
            cmd.Parameters.Add("@MenuID", SqlDbType.Int).Value = menuRole.MenuID;
            cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = menuRole.RoleID;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = menuRole.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = menuRole.AddedBy;
            cmd.Parameters.Add("@ModifyDate", SqlDbType.DateTime).Value = menuRole.ModifyDate;
            cmd.Parameters.Add("@ModifyBy", SqlDbType.NVarChar).Value = menuRole.ModifyBy;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = menuRole.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
