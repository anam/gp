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

public class SqlLoginRoleProvider:DataAccessObject
{
	public SqlLoginRoleProvider()
    {
    }


    public bool DeleteLoginRole(int loginRoleID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_DeleteLoginRole", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LoginRoleID", SqlDbType.Int).Value = loginRoleID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<LoginRole> GetAllLoginRoles()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetAllLoginRoles", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetLoginRolesFromReader(reader);
        }
    }

    public List<LoginRole> GetAllLoginRolesByLoginID(int loginID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetAllLoginRoles", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@LoginID", SqlDbType.Int).Value = loginID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetLoginRolesFromReader(reader);
        }
    }

    public List<LoginRole> GetLoginRolesFromReader(IDataReader reader)
    {
        List<LoginRole> loginRoles = new List<LoginRole>();

        while (reader.Read())
        {
            loginRoles.Add(GetLoginRoleFromReader(reader));
        }
        return loginRoles;
    }

    public LoginRole GetLoginRoleFromReader(IDataReader reader)
    {
        try
        {
            LoginRole loginRole = new LoginRole
                (
                    (int)reader["LoginRoleID"],
                    (int)reader["RoleID"],
                    (int)reader["LoginID"],
                    (int)reader["RowStatusID"],
                    (DateTime)reader["AddedDate"],
                    reader["AddedBy"].ToString(),
                    (DateTime)reader["ModifyDate"],
                    reader["ModifyBy"].ToString()
                );
             return loginRole;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public LoginRole GetLoginRoleByID(int loginRoleID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetLoginRoleByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@LoginRoleID", SqlDbType.Int).Value = loginRoleID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetLoginRoleFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertLoginRole(LoginRole loginRole)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_InsertLoginRole", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LoginRoleID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = loginRole.RoleID;
            cmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = loginRole.LoginID;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = loginRole.RowStatusID;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = loginRole.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = loginRole.AddedBy;
            cmd.Parameters.Add("@ModifyDate", SqlDbType.DateTime).Value = loginRole.ModifyDate;
            cmd.Parameters.Add("@ModifyBy", SqlDbType.NVarChar).Value = loginRole.ModifyBy;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@LoginRoleID"].Value;
        }
    }

    public bool UpdateLoginRole(LoginRole loginRole)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_UpdateLoginRole", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LoginRoleID", SqlDbType.Int).Value = loginRole.LoginRoleID;
            cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = loginRole.RoleID;
            cmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = loginRole.LoginID;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = loginRole.RowStatusID;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = loginRole.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = loginRole.AddedBy;
            cmd.Parameters.Add("@ModifyDate", SqlDbType.DateTime).Value = loginRole.ModifyDate;
            cmd.Parameters.Add("@ModifyBy", SqlDbType.NVarChar).Value = loginRole.ModifyBy;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }

    public bool UpdateLoginRoleByIDs(LoginRole loginRole,String RoleIDs)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_UpdateLoginRoleByIDs", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@RoleIDs", SqlDbType.NVarChar).Value = RoleIDs;
            cmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = loginRole.LoginID;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = loginRole.RowStatusID;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = loginRole.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = loginRole.AddedBy;
            cmd.Parameters.Add("@ModifyDate", SqlDbType.DateTime).Value = loginRole.ModifyDate;
            cmd.Parameters.Add("@ModifyBy", SqlDbType.NVarChar).Value = loginRole.ModifyBy;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
