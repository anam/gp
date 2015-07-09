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

public class SqlACC_JournalMasterProvider:DataAccessObject
{
	public SqlACC_JournalMasterProvider()
    {
    }


    public bool DeleteACC_JournalMaster(int aCC_JournalMasterID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeleteACC_JournalMaster", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACC_JournalMasterID", SqlDbType.Int).Value = aCC_JournalMasterID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<ACC_JournalMaster> GetAllACC_JournalMasters()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_JournalMasters", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_JournalMastersFromReader(reader);
        }
    }

    public List<ACC_JournalMaster> GetAllACC_JournalMastersByDateRange(string searchString)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_JournalMastersByDateRange", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@SearchString", SqlDbType.NVarChar).Value = searchString;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_JournalMastersFromReader(reader);
        }
    }

    public List<ACC_JournalMaster> GetACC_JournalMastersFromReader(IDataReader reader)
    {
        List<ACC_JournalMaster> aCC_JournalMasters = new List<ACC_JournalMaster>();

        while (reader.Read())
        {
            aCC_JournalMasters.Add(GetACC_JournalMasterFromReader(reader));
        }
        return aCC_JournalMasters;
    }

    public ACC_JournalMaster GetACC_JournalMasterFromReader(IDataReader reader)
    {
        try
        {
            ACC_JournalMaster aCC_JournalMaster = new ACC_JournalMaster
                (
                    (int)reader["ACC_JournalMasterID"],
                    reader["JournalMasterName"].ToString(),
                    reader["ExtraField1"].ToString(),
                    reader["ExtraField2"].ToString(),
                    reader["ExtraField3"].ToString(),
                    reader["Note"].ToString(),
                    (DateTime)reader["JournalDate"],
                    (int)reader["AddedBy"],
                    (DateTime)reader["AddedDate"],
                    (int)reader["UpdatedBy"],
                    (DateTime)reader["UpdatedDate"],
                    (int)reader["RowStatusID"]
                );
             return aCC_JournalMaster;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public ACC_JournalMaster GetACC_JournalMasterByID(int aCC_JournalMasterID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetACC_JournalMasterByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ACC_JournalMasterID", SqlDbType.Int).Value = aCC_JournalMasterID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetACC_JournalMasterFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertACC_JournalMaster(ACC_JournalMaster aCC_JournalMaster)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertACC_JournalMaster", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACC_JournalMasterID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@JournalMasterName", SqlDbType.NVarChar).Value = aCC_JournalMaster.JournalMasterName;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = aCC_JournalMaster.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = aCC_JournalMaster.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = aCC_JournalMaster.ExtraField3;
            cmd.Parameters.Add("@Note", SqlDbType.NVarChar).Value = aCC_JournalMaster.Note;
            cmd.Parameters.Add("@JournalDate", SqlDbType.DateTime).Value = aCC_JournalMaster.JournalDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = aCC_JournalMaster.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = aCC_JournalMaster.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = aCC_JournalMaster.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = aCC_JournalMaster.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = aCC_JournalMaster.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@ACC_JournalMasterID"].Value;
        }
    }

    public int InsertACC_JournalMasterTmp(ACC_JournalMaster aCC_JournalMaster)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertACC_JournalMasterTmp", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACC_JournalMasterID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@JournalMasterName", SqlDbType.NVarChar).Value = aCC_JournalMaster.JournalMasterName;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = aCC_JournalMaster.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = aCC_JournalMaster.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = aCC_JournalMaster.ExtraField3;
            cmd.Parameters.Add("@Note", SqlDbType.NVarChar).Value = aCC_JournalMaster.Note;
            cmd.Parameters.Add("@JournalDate", SqlDbType.DateTime).Value = aCC_JournalMaster.JournalDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = aCC_JournalMaster.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = aCC_JournalMaster.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = aCC_JournalMaster.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = aCC_JournalMaster.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = aCC_JournalMaster.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@ACC_JournalMasterID"].Value;
        }
    }
    public bool UpdateACC_JournalMaster(ACC_JournalMaster aCC_JournalMaster)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdateACC_JournalMaster", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACC_JournalMasterID", SqlDbType.Int).Value = aCC_JournalMaster.ACC_JournalMasterID;
            cmd.Parameters.Add("@JournalMasterName", SqlDbType.NVarChar).Value = aCC_JournalMaster.JournalMasterName;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = aCC_JournalMaster.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = aCC_JournalMaster.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = aCC_JournalMaster.ExtraField3;
            cmd.Parameters.Add("@Note", SqlDbType.NVarChar).Value = aCC_JournalMaster.Note;
            cmd.Parameters.Add("@JournalDate", SqlDbType.DateTime).Value = aCC_JournalMaster.JournalDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = aCC_JournalMaster.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = aCC_JournalMaster.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = aCC_JournalMaster.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = aCC_JournalMaster.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = aCC_JournalMaster.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
