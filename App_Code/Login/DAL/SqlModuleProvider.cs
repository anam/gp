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

public class SqlModuleProvider:DataAccessObject
{
	public SqlModuleProvider()
    {
    }


    public bool DeleteModule(int moduleID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_DeleteModule", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ModuleID", SqlDbType.Int).Value = moduleID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Module> GetAllModules()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetAllModules", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetModulesFromReader(reader);
        }
    }

    public List<Module> GetAllModulesSearch(string SearchString)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetAllModulesSearch", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@SearchString", SqlDbType.Int).Value = SearchString;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetModulesFromReader(reader);
        }
    }

    public List<Module> GetModulesFromReader(IDataReader reader)
    {
        List<Module> modules = new List<Module>();

        while (reader.Read())
        {
            modules.Add(GetModuleFromReader(reader));
        }
        return modules;
    }

    public Module GetModuleFromReader(IDataReader reader)
    {
        try
        {
            Module module = new Module
                (
                    (int)reader["ModuleID"],
                    reader["ModuleName"].ToString(),
                    reader["DefaultURL"].ToString(),
                    reader["AddedBy"].ToString(),
                    (DateTime)reader["AddedDate"],
                    reader["UpdatedBy"].ToString(),
                    (DateTime)reader["UpdatedDate"],
                    (int)reader["RowStatusID"]
                );

            try
            {
                module.FolderName = reader["FolderName"].ToString();
                module.MenuOrder = (decimal)reader["MenuOrder"];
            }
            catch (Exception ex) { }
             return module;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Module GetModuleByID(int moduleID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetModuleByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ModuleID", SqlDbType.Int).Value = moduleID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetModuleFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertModule(Module module)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_InsertModule", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ModuleID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ModuleName", SqlDbType.NVarChar).Value = module.ModuleName;
            cmd.Parameters.Add("@FolderName", SqlDbType.NVarChar).Value = module.FolderName;
            cmd.Parameters.Add("@DefaultURL", SqlDbType.NVarChar).Value = module.DefaultURL;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = module.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = module.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar).Value = module.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = module.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = module.RowStatusID;
            cmd.Parameters.Add("@MenuOrder", SqlDbType.Decimal).Value = module.MenuOrder;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@ModuleID"].Value;
        }
    }

    public bool UpdateModule(Module module)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_UpdateModule", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ModuleID", SqlDbType.Int).Value = module.ModuleID;
            cmd.Parameters.Add("@ModuleName", SqlDbType.NVarChar).Value = module.ModuleName;
            cmd.Parameters.Add("@FolderName", SqlDbType.NVarChar).Value = module.FolderName;
            cmd.Parameters.Add("@DefaultURL", SqlDbType.NVarChar).Value = module.DefaultURL;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = module.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = module.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar).Value = module.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = module.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = module.RowStatusID;
            cmd.Parameters.Add("@MenuOrder", SqlDbType.Decimal).Value = module.MenuOrder;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
