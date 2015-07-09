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

public class SqlDefaultButtonProvider:DataAccessObject
{
	public SqlDefaultButtonProvider()
    {
    }


    public bool DeleteDefaultButton(int defaultButtonID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_DeleteDefaultButton", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DefaultButtonID", SqlDbType.Int).Value = defaultButtonID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<DefaultButton> GetAllDefaultButtons()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetAllDefaultButtons", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetDefaultButtonsFromReader(reader);
        }
    }
    public List<DefaultButton> GetDefaultButtonsFromReader(IDataReader reader)
    {
        List<DefaultButton> defaultButtons = new List<DefaultButton>();

        while (reader.Read())
        {
            defaultButtons.Add(GetDefaultButtonFromReader(reader));
        }
        return defaultButtons;
    }

    public DefaultButton GetDefaultButtonFromReader(IDataReader reader)
    {
        try
        {
            DefaultButton defaultButton = new DefaultButton
                (
                    (int)reader["DefaultButtonID"],
                    reader["DefaultButtonName"].ToString(),
                    reader["DefaultButtonText"].ToString()
                );
             return defaultButton;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public DefaultButton GetDefaultButtonByID(int defaultButtonID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetDefaultButtonByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@DefaultButtonID", SqlDbType.Int).Value = defaultButtonID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetDefaultButtonFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertDefaultButton(DefaultButton defaultButton)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_InsertDefaultButton", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DefaultButtonID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@DefaultButtonName", SqlDbType.NVarChar).Value = defaultButton.DefaultButtonName;
            cmd.Parameters.Add("@DefaultButtonText", SqlDbType.NVarChar).Value = defaultButton.DefaultButtonText;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@DefaultButtonID"].Value;
        }
    }

    public bool UpdateDefaultButton(DefaultButton defaultButton)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_UpdateDefaultButton", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DefaultButtonID", SqlDbType.Int).Value = defaultButton.DefaultButtonID;
            cmd.Parameters.Add("@DefaultButtonName", SqlDbType.NVarChar).Value = defaultButton.DefaultButtonName;
            cmd.Parameters.Add("@DefaultButtonText", SqlDbType.NVarChar).Value = defaultButton.DefaultButtonText;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
