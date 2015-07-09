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

public class SqlButtonProvider:DataAccessObject
{
	public SqlButtonProvider()
    {
    }


    public bool DeleteButton(int buttonID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_DeleteButton", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ButtonID", SqlDbType.Int).Value = buttonID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Button> GetAllButtons()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetAllButtons", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetButtonsFromReader(reader);
        }
    }

    public List<Button> GetAllButtonsByPageID(int pageID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetAllButtonsByPageID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@PageID", SqlDbType.Int).Value = pageID;

            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetButtonsFromReader(reader);
        }
    }

    public List<Button> GetAllButtonsByPageURLnUserID(string pageURL, int loginID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetAllButtonsByPageURLnUserID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@PageURL", SqlDbType.NVarChar).Value = pageURL;
            command.Parameters.Add("@LoginID", SqlDbType.Int).Value = loginID;

            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetButtonsFromReader(reader);
        }
    }

    public List<Button> GetAllButtonsByPageURLnUserIDnButtonName(string pageURL, int loginID, string buttonName)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetAllButtonsByPageURLnUserIDnButtonName", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@PageURL", SqlDbType.NVarChar).Value = pageURL;
            command.Parameters.Add("@LoginID", SqlDbType.Int).Value = loginID;
            command.Parameters.Add("@ButtonName", SqlDbType.NVarChar).Value = buttonName;

            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetButtonsFromReader(reader);
        }
    }


    public List<Button> GetButtonsFromReader(IDataReader reader)
    {
        List<Button> buttons = new List<Button>();

        while (reader.Read())
        {
            buttons.Add(GetButtonFromReader(reader));
        }
        return buttons;
    }

    public Button GetButtonFromReader(IDataReader reader)
    {
        try
        {
            Button button = new Button
                (
                    (int)reader["ButtonID"],
                    reader["ButtonName"].ToString(),
                    reader["ButtonText"].ToString(),
                    (int)reader["PageID"],
                    reader["AddedBy"].ToString(),
                    (DateTime)reader["AddedDate"],
                    reader["UpdatedBy"].ToString(),
                    (DateTime)reader["UpdatedDate"],
                    (int)reader["RowStatusID"]
                );
             return button;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Button GetButtonByID(int buttonID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetButtonByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ButtonID", SqlDbType.Int).Value = buttonID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetButtonFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertButton(Button button)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_InsertButton", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ButtonID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ButtonName", SqlDbType.NVarChar).Value = button.ButtonName;
            cmd.Parameters.Add("@ButtonText", SqlDbType.NVarChar).Value = button.ButtonText;
            cmd.Parameters.Add("@PageID", SqlDbType.Int).Value = button.PageID;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = button.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = button.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar).Value = button.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = button.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = button.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@ButtonID"].Value;
        }
    }

    public bool UpdateButton(Button button)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_UpdateButton", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ButtonID", SqlDbType.Int).Value = button.ButtonID;
            cmd.Parameters.Add("@ButtonName", SqlDbType.NVarChar).Value = button.ButtonName;
            cmd.Parameters.Add("@ButtonText", SqlDbType.NVarChar).Value = button.ButtonText;
            cmd.Parameters.Add("@PageID", SqlDbType.Int).Value = button.PageID;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = button.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = button.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar).Value = button.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = button.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = button.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
