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

public class SqlRoleProvider:DataAccessObject
{
	public SqlRoleProvider()
    {
    }


    public bool DeleteRole(int roleID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_DeleteRole", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public bool DeleteModuleMenuPageButtonRole(int roleID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_DeleteModuleMenuPageButtonRole", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }


    public List<Role> GetAllRoles()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetAllRoles", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetRolesFromReader(reader);
        }
    }
    public List<Role> GetRolesFromReader(IDataReader reader)
    {
        List<Role> roles = new List<Role>();

        while (reader.Read())
        {
            roles.Add(GetRoleFromReader(reader));
        }
        return roles;
    }

    public DataSet GetModuleNMenuNPageNButtonByRoleID(int RoleID)
    {
        DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("Login_GetModuleNMenuNPageNButtonByRoleID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RoleID", RoleID);
                connection.Open();
                SqlDataAdapter myadapter = new SqlDataAdapter(command);
                myadapter.Fill(ds);
                myadapter.Dispose();
                connection.Close();
                return ds;
            }
        }
    }

    public Role GetRoleFromReader(IDataReader reader)
    {
        try
        {
            Role role = new Role
                (
                    (int)reader["RoleID"],
                    reader["RoleName"].ToString(),
                    reader["RoleDescription"].ToString(),
                    (DateTime)reader["AddedDate"],
                    reader["AddedBy"].ToString(),
                    (DateTime)reader["ModifyDate"],
                    reader["ModifyBy"].ToString(),
                    (int)reader["RowStatusID"]
                );
             return role;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Role GetRoleByID(int roleID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetRoleByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetRoleFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertRole(Role role)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_InsertRole", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@RoleID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@RoleName", SqlDbType.NVarChar).Value = role.RoleName;
            cmd.Parameters.Add("@RoleDescription", SqlDbType.NText).Value = role.RoleDescription;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = role.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = role.AddedBy;
            cmd.Parameters.Add("@ModifyDate", SqlDbType.DateTime).Value = role.ModifyDate;
            cmd.Parameters.Add("@ModifyBy", SqlDbType.NVarChar).Value = role.ModifyBy;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = role.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@RoleID"].Value;
        }
    }

    public bool UpdateRole(Role role)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_UpdateRole", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = role.RoleID;
            cmd.Parameters.Add("@RoleName", SqlDbType.NVarChar).Value = role.RoleName;
            cmd.Parameters.Add("@RoleDescription", SqlDbType.NText).Value = role.RoleDescription;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = role.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = role.AddedBy;
            cmd.Parameters.Add("@ModifyDate", SqlDbType.DateTime).Value = role.ModifyDate;
            cmd.Parameters.Add("@ModifyBy", SqlDbType.NVarChar).Value = role.ModifyBy;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = role.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
