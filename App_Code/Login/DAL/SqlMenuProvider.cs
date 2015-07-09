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

public class SqlMenuProvider:DataAccessObject
{
	public SqlMenuProvider()
    {
    }


    public bool DeleteMenu(int menuID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_DeleteMenu", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MenuID", SqlDbType.Int).Value = menuID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public DataSet GetMenuByLoginID(int LoginID)
    {
        DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("Login_GetMenuByLoginID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LoginID", LoginID);
                connection.Open();
                SqlDataAdapter myadapter = new SqlDataAdapter(command);
                myadapter.Fill(ds);
                myadapter.Dispose();
                connection.Close();
                return ds;
            }
        }
    }

    public DataSet GetMenuddl()
    {
        DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("Login_GetMenuddl", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataAdapter myadapter = new SqlDataAdapter(command);
                myadapter.Fill(ds);
                myadapter.Dispose();
                connection.Close();
                return ds;
            }
        }
    }


    public List<Menu> GetAllMenus()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetAllMenus", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetMenusFromReader(reader);
        }
    }

    public List<Menu> GetAllMenusByModuleID(int moduleID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetAllMenusByModuleID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ModuleID", SqlDbType.Int).Value = moduleID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetMenusFromReader(reader);
        }
    }

    public List<Menu> GetMenusFromReader(IDataReader reader)
    {
        List<Menu> menus = new List<Menu>();

        while (reader.Read())
        {
            menus.Add(GetMenuFromReader(reader));
        }
        return menus;
    }

    public Menu GetMenuFromReader(IDataReader reader)
    {
        try
        {
            Menu menu = new Menu
                (
                    (int)reader["MenuID"],
                    reader["MenuTitle"].ToString(),
                    (int)reader["ModuleID"],
                    (int)reader["PageID"],
                    (int)reader["ParentMenuID"],
                    reader["AddedBy"].ToString(),
                    (DateTime)reader["AddedDate"],
                    reader["UpdatedBy"].ToString(),
                    (DateTime)reader["UpdatedDate"],
                    (int)reader["RowStatusID"]
                );

            try
            {
                menu.MenuOrder = (decimal)reader["MenuOrder"];
            }
            catch (Exception ex) { }
             return menu;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Menu GetMenuByID(int menuID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetMenuByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MenuID", SqlDbType.Int).Value = menuID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetMenuFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertMenu(Menu menu)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_InsertMenu", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MenuID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@MenuTitle", SqlDbType.NVarChar).Value = menu.MenuTitle;
            cmd.Parameters.Add("@ModuleID", SqlDbType.Int).Value = menu.ModuleID;
            cmd.Parameters.Add("@PageID", SqlDbType.Int).Value = menu.PageID;
            cmd.Parameters.Add("@ParentMenuID", SqlDbType.Int).Value = menu.ParentMenuID;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = menu.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = menu.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar).Value = menu.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = menu.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = menu.RowStatusID;
            cmd.Parameters.Add("@MenuOrder", SqlDbType.Decimal).Value = menu.MenuOrder;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@MenuID"].Value;
        }
    }

    public bool UpdateMenu(Menu menu)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_UpdateMenu", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MenuID", SqlDbType.Int).Value = menu.MenuID;
            cmd.Parameters.Add("@MenuTitle", SqlDbType.NVarChar).Value = menu.MenuTitle;
            cmd.Parameters.Add("@ModuleID", SqlDbType.Int).Value = menu.ModuleID;
            cmd.Parameters.Add("@PageID", SqlDbType.Int).Value = menu.PageID;
            cmd.Parameters.Add("@ParentMenuID", SqlDbType.Int).Value = menu.ParentMenuID;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = menu.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = menu.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar).Value = menu.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = menu.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = menu.RowStatusID;
            cmd.Parameters.Add("@MenuOrder", SqlDbType.Decimal).Value = menu.MenuOrder;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
