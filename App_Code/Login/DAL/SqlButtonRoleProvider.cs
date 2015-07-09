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

public class SqlButtonRoleProvider:DataAccessObject
{
	public SqlButtonRoleProvider()
    {
    }


    public bool DeleteButtonRole(int buttonRoleID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_DeleteButtonRole", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ButtonRoleID", SqlDbType.Int).Value = buttonRoleID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<ButtonRole> GetAllButtonRoles()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetAllButtonRoles", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetButtonRolesFromReader(reader);
        }
    }
    public List<ButtonRole> GetButtonRolesFromReader(IDataReader reader)
    {
        List<ButtonRole> buttonRoles = new List<ButtonRole>();

        while (reader.Read())
        {
            buttonRoles.Add(GetButtonRoleFromReader(reader));
        }
        return buttonRoles;
    }

    public ButtonRole GetButtonRoleFromReader(IDataReader reader)
    {
        try
        {
            ButtonRole buttonRole = new ButtonRole
                (
                    (int)reader["ButtonRoleID"],
                    (int)reader["RoleID"],
                    (int)reader["ButtonID"],
                    (DateTime)reader["AddedDate"],
                    reader["AddedBy"].ToString(),
                    (DateTime)reader["ModifyDate"],
                    reader["ModifyBy"].ToString(),
                    (int)reader["RowStatusID"]
                );
             return buttonRole;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public ButtonRole GetButtonRoleByID(int buttonRoleID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetButtonRoleByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ButtonRoleID", SqlDbType.Int).Value = buttonRoleID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetButtonRoleFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertButtonRole(ButtonRole buttonRole)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_InsertButtonRole", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ButtonRoleID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = buttonRole.RoleID;
            cmd.Parameters.Add("@ButtonID", SqlDbType.Int).Value = buttonRole.ButtonID;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = buttonRole.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = buttonRole.AddedBy;
            cmd.Parameters.Add("@ModifyDate", SqlDbType.DateTime).Value = buttonRole.ModifyDate;
            cmd.Parameters.Add("@ModifyBy", SqlDbType.NVarChar).Value = buttonRole.ModifyBy;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = buttonRole.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@ButtonRoleID"].Value;
        }
    }

    public bool UpdateButtonRole(ButtonRole buttonRole)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_UpdateButtonRole", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ButtonRoleID", SqlDbType.Int).Value = buttonRole.ButtonRoleID;
            cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = buttonRole.RoleID;
            cmd.Parameters.Add("@ButtonID", SqlDbType.Int).Value = buttonRole.ButtonID;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = buttonRole.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = buttonRole.AddedBy;
            cmd.Parameters.Add("@ModifyDate", SqlDbType.DateTime).Value = buttonRole.ModifyDate;
            cmd.Parameters.Add("@ModifyBy", SqlDbType.NVarChar).Value = buttonRole.ModifyBy;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = buttonRole.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
