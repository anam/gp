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

public class SqlPos_ProductTransactionMasterProvider:DataAccessObject
{
	public SqlPos_ProductTransactionMasterProvider()
    {
    }


    public bool DeletePos_ProductTransactionMaster(int pos_ProductTransactionMasterID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeletePos_ProductTransactionMaster", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_ProductTransactionMasterID", SqlDbType.Int).Value = pos_ProductTransactionMasterID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Pos_ProductTransactionMaster> GetAllPos_ProductTransactionMasters()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllPos_ProductTransactionMasters", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetPos_ProductTransactionMastersFromReader(reader);
        }
    }
    public List<Pos_ProductTransactionMaster> GetPos_ProductTransactionMastersFromReader(IDataReader reader)
    {
        List<Pos_ProductTransactionMaster> pos_ProductTransactionMasters = new List<Pos_ProductTransactionMaster>();

        while (reader.Read())
        {
            pos_ProductTransactionMasters.Add(GetPos_ProductTransactionMasterFromReader(reader));
        }
        return pos_ProductTransactionMasters;
    }

    public Pos_ProductTransactionMaster GetPos_ProductTransactionMasterFromReader(IDataReader reader)
    {
        try
        {
            Pos_ProductTransactionMaster pos_ProductTransactionMaster = new Pos_ProductTransactionMaster
                (
                    (int)reader["Pos_ProductTransactionMasterID"],
                    (DateTime)reader["TransactionDate"],
                    (int)reader["TransactionID"],
                    (int)reader["ToOrFromID"],
                    reader["Record"].ToString(),
                    reader["Particulars"].ToString(),
                    (int)reader["WorkSatationID"],
                    reader["ExtraField1"].ToString(),
                    reader["ExtraField2"].ToString(),
                    reader["ExtraField3"].ToString(),
                    reader["ExtraField4"].ToString(),
                    reader["ExtraField5"].ToString(),
                    (int)reader["AddedBy"],
                    (DateTime)reader["AddedDate"],
                    (int)reader["UpdatedBy"],
                    (DateTime)reader["UpdatedDate"],
                    (int)reader["RowStatusID"]
                );
             return pos_ProductTransactionMaster;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Pos_ProductTransactionMaster GetPos_ProductTransactionMasterByID(int pos_ProductTransactionMasterID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetPos_ProductTransactionMasterByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Pos_ProductTransactionMasterID", SqlDbType.Int).Value = pos_ProductTransactionMasterID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetPos_ProductTransactionMasterFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertPos_ProductTransactionMaster(Pos_ProductTransactionMaster pos_ProductTransactionMaster)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertPos_ProductTransactionMaster", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_ProductTransactionMasterID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@TransactionDate", SqlDbType.DateTime).Value = pos_ProductTransactionMaster.TransactionDate;
            cmd.Parameters.Add("@TransactionID", SqlDbType.Int).Value = pos_ProductTransactionMaster.TransactionID;
            cmd.Parameters.Add("@ToOrFromID", SqlDbType.Int).Value = pos_ProductTransactionMaster.ToOrFromID;
            cmd.Parameters.Add("@Record", SqlDbType.NVarChar).Value = pos_ProductTransactionMaster.Record;
            cmd.Parameters.Add("@Particulars", SqlDbType.NText).Value = pos_ProductTransactionMaster.Particulars;
            cmd.Parameters.Add("@WorkSatationID", SqlDbType.Int).Value = pos_ProductTransactionMaster.WorkSatationID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = pos_ProductTransactionMaster.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = pos_ProductTransactionMaster.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = pos_ProductTransactionMaster.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = pos_ProductTransactionMaster.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = pos_ProductTransactionMaster.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = pos_ProductTransactionMaster.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = pos_ProductTransactionMaster.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = pos_ProductTransactionMaster.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = pos_ProductTransactionMaster.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = pos_ProductTransactionMaster.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Pos_ProductTransactionMasterID"].Value;
        }
    }

    public bool UpdatePos_ProductTransactionMaster(Pos_ProductTransactionMaster pos_ProductTransactionMaster)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdatePos_ProductTransactionMaster", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_ProductTransactionMasterID", SqlDbType.Int).Value = pos_ProductTransactionMaster.Pos_ProductTransactionMasterID;
            cmd.Parameters.Add("@TransactionDate", SqlDbType.DateTime).Value = pos_ProductTransactionMaster.TransactionDate;
            cmd.Parameters.Add("@TransactionID", SqlDbType.Int).Value = pos_ProductTransactionMaster.TransactionID;
            cmd.Parameters.Add("@ToOrFromID", SqlDbType.Int).Value = pos_ProductTransactionMaster.ToOrFromID;
            cmd.Parameters.Add("@Record", SqlDbType.NVarChar).Value = pos_ProductTransactionMaster.Record;
            cmd.Parameters.Add("@Particulars", SqlDbType.NText).Value = pos_ProductTransactionMaster.Particulars;
            cmd.Parameters.Add("@WorkSatationID", SqlDbType.Int).Value = pos_ProductTransactionMaster.WorkSatationID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = pos_ProductTransactionMaster.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = pos_ProductTransactionMaster.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = pos_ProductTransactionMaster.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = pos_ProductTransactionMaster.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = pos_ProductTransactionMaster.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = pos_ProductTransactionMaster.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = pos_ProductTransactionMaster.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = pos_ProductTransactionMaster.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = pos_ProductTransactionMaster.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = pos_ProductTransactionMaster.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
