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

public class SqlADLItemProvider:DataAccessObject
{
	public SqlADLItemProvider()
    {
    }


    public bool DeleteADLItem(int aDLItemID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteADLItem", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLItemID", SqlDbType.Int).Value = aDLItemID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<ADLItem> GetAllADLItems()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllADLItems", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetADLItemsFromReader(reader);
        }
    }

    public List<ADLItem> SearchADLItems(string searchString)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_SearchADLItems", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@SearchString", SqlDbType.NVarChar).Value = searchString;

            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetADLItemsFromReader(reader);
        }
    }

    public List<ADLItem> GetADLItemsFromReader(IDataReader reader)
    {
        List<ADLItem> aDLItems = new List<ADLItem>();

        while (reader.Read())
        {
            aDLItems.Add(GetADLItemFromReader(reader));
        }
        return aDLItems;
    }

    public ADLItem GetADLItemFromReader(IDataReader reader)
    {
        try
        {
            ADLItem aDLItem = new ADLItem
                (
                    (int)reader["ADLItemID"],
                    reader["ADLItemName"].ToString(),
                    (int)reader["ADLItemDescriptionID"],
                    (int)reader["ADLStatusID"],
                    (int)reader["ResidentID"],
                    (DateTime)reader["IteamTime"],
                    reader["Comment"].ToString(),
                    reader["ExtraField1"].ToString(),
                    reader["ExtraField2"].ToString(),
                    reader["ExtraField3"].ToString(),
                    reader["ExtraField4"].ToString(),
                    reader["ExtraField5"].ToString(),
                    reader["ExtraField6"].ToString(),
                    reader["ExtraField7"].ToString(),
                    reader["ExtraField8"].ToString(),
                    reader["ExtraField9"].ToString(),
                    reader["ExtraField10"].ToString()
                );

            try { aDLItem.ExtraField1 = reader["ADLItemDescriptionName"].ToString(); }
            catch (Exception ex) { }
            try { aDLItem.ExtraField2 = reader["ADLStatusName"].ToString(); }
            catch (Exception ex) { }
             return aDLItem;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public ADLItem GetADLItemByID(int aDLItemID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetADLItemByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ADLItemID", SqlDbType.Int).Value = aDLItemID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetADLItemFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertADLItem(ADLItem aDLItem)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertADLItem", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLItemID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ADLItemName", SqlDbType.NVarChar).Value = aDLItem.ADLItemName;
            cmd.Parameters.Add("@ADLItemDescriptionID", SqlDbType.Int).Value = aDLItem.ADLItemDescriptionID;
            cmd.Parameters.Add("@ADLStatusID", SqlDbType.Int).Value = aDLItem.ADLStatusID;
            cmd.Parameters.Add("@ResidentID", SqlDbType.Int).Value = aDLItem.ResidentID;
            cmd.Parameters.Add("@IteamTime", SqlDbType.DateTime).Value = aDLItem.IteamTime;
            cmd.Parameters.Add("@Comment", SqlDbType.Text).Value = aDLItem.Comment;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = aDLItem.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = aDLItem.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = aDLItem.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = aDLItem.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = aDLItem.ExtraField5;
            cmd.Parameters.Add("@ExtraField6", SqlDbType.NVarChar).Value = aDLItem.ExtraField6;
            cmd.Parameters.Add("@ExtraField7", SqlDbType.NVarChar).Value = aDLItem.ExtraField7;
            cmd.Parameters.Add("@ExtraField8", SqlDbType.NVarChar).Value = aDLItem.ExtraField8;
            cmd.Parameters.Add("@ExtraField9", SqlDbType.NVarChar).Value = aDLItem.ExtraField9;
            cmd.Parameters.Add("@ExtraField10", SqlDbType.NVarChar).Value = aDLItem.ExtraField10;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@ADLItemID"].Value;
        }
    }

    public bool UpdateADLItem(ADLItem aDLItem)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateADLItem", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLItemID", SqlDbType.Int).Value = aDLItem.ADLItemID;
            cmd.Parameters.Add("@ADLItemName", SqlDbType.NVarChar).Value = aDLItem.ADLItemName;
            cmd.Parameters.Add("@ADLItemDescriptionID", SqlDbType.Int).Value = aDLItem.ADLItemDescriptionID;
            cmd.Parameters.Add("@ADLStatusID", SqlDbType.Int).Value = aDLItem.ADLStatusID;
            cmd.Parameters.Add("@ResidentID", SqlDbType.Int).Value = aDLItem.ResidentID;
            cmd.Parameters.Add("@IteamTime", SqlDbType.DateTime).Value = aDLItem.IteamTime;
            cmd.Parameters.Add("@Comment", SqlDbType.Text).Value = aDLItem.Comment;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = aDLItem.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = aDLItem.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = aDLItem.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = aDLItem.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = aDLItem.ExtraField5;
            cmd.Parameters.Add("@ExtraField6", SqlDbType.NVarChar).Value = aDLItem.ExtraField6;
            cmd.Parameters.Add("@ExtraField7", SqlDbType.NVarChar).Value = aDLItem.ExtraField7;
            cmd.Parameters.Add("@ExtraField8", SqlDbType.NVarChar).Value = aDLItem.ExtraField8;
            cmd.Parameters.Add("@ExtraField9", SqlDbType.NVarChar).Value = aDLItem.ExtraField9;
            cmd.Parameters.Add("@ExtraField10", SqlDbType.NVarChar).Value = aDLItem.ExtraField10;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
