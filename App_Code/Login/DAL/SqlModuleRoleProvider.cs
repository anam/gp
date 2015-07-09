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

public class SqlModuleRoleProvider:DataAccessObject
{
	public SqlModuleRoleProvider()
    {
    }


    public bool DeleteModuleRole(int moduleRoleID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_DeleteModuleRole", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ModuleRoleID", SqlDbType.Int).Value = moduleRoleID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<ModuleRole> GetAllModuleRoles()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetAllModuleRoles", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetModuleRolesFromReader(reader);
        }
    }
    public List<ModuleRole> GetModuleRolesFromReader(IDataReader reader)
    {
        List<ModuleRole> moduleRoles = new List<ModuleRole>();

        while (reader.Read())
        {
            moduleRoles.Add(GetModuleRoleFromReader(reader));
        }
        return moduleRoles;
    }

    public ModuleRole GetModuleRoleFromReader(IDataReader reader)
    {
        try
        {
            ModuleRole moduleRole = new ModuleRole
                (
                    (int)reader["ModuleRoleID"],
                    (int)reader["ModuleID"],
                    (int)reader["RoleID"],
                    (DateTime)reader["AddedDate"],
                    reader["AddedBy"].ToString(),
                    (DateTime)reader["ModifyDate"],
                    reader["ModifyBy"].ToString(),
                    (int)reader["RowStatusID"]
                );
             return moduleRole;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public ModuleRole GetModuleRoleByID(int moduleRoleID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetModuleRoleByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ModuleRoleID", SqlDbType.Int).Value = moduleRoleID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetModuleRoleFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertModuleRole(ModuleRole moduleRole)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_InsertModuleRole", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ModuleRoleID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ModuleID", SqlDbType.Int).Value = moduleRole.ModuleID;
            cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = moduleRole.RoleID;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = moduleRole.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = moduleRole.AddedBy;
            cmd.Parameters.Add("@ModifyDate", SqlDbType.DateTime).Value = moduleRole.ModifyDate;
            cmd.Parameters.Add("@ModifyBy", SqlDbType.NVarChar).Value = moduleRole.ModifyBy;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = moduleRole.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@ModuleRoleID"].Value;
        }
    }

    public bool UpdateModuleRole(ModuleRole moduleRole)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_UpdateModuleRole", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ModuleRoleID", SqlDbType.Int).Value = moduleRole.ModuleRoleID;
            cmd.Parameters.Add("@ModuleID", SqlDbType.Int).Value = moduleRole.ModuleID;
            cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = moduleRole.RoleID;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = moduleRole.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = moduleRole.AddedBy;
            cmd.Parameters.Add("@ModifyDate", SqlDbType.DateTime).Value = moduleRole.ModifyDate;
            cmd.Parameters.Add("@ModifyBy", SqlDbType.NVarChar).Value = moduleRole.ModifyBy;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = moduleRole.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
