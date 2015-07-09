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

public class SqlPos_TransactionMasterProvider:DataAccessObject
{
	public SqlPos_TransactionMasterProvider()
    {
    }


    public bool DeletePos_TransactionMaster(int pos_TransactionMasterID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeletePos_TransactionMaster", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_TransactionMasterID", SqlDbType.Int).Value = pos_TransactionMasterID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Pos_TransactionMaster> GetAllPos_TransactionMasters()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllPos_TransactionMasters", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetPos_TransactionMastersFromReader(reader);
        }
    }
    public List<Pos_TransactionMaster> GetPos_TransactionMastersFromReader(IDataReader reader)
    {
        List<Pos_TransactionMaster> pos_TransactionMasters = new List<Pos_TransactionMaster>();

        while (reader.Read())
        {
            pos_TransactionMasters.Add(GetPos_TransactionMasterFromReader(reader));
        }
        return pos_TransactionMasters;
    }

    public Pos_TransactionMaster GetPos_TransactionMasterFromReader(IDataReader reader)
    {
        try
        {
            Pos_TransactionMaster pos_TransactionMaster = new Pos_TransactionMaster
                (
                    (int)reader["Pos_TransactionMasterID"],
                    (DateTime)reader["TransactionDate"],
                    (int)reader["Pos_TransactionTypeID"],
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

            try {
                pos_TransactionMaster.ToOrFromName = reader["ToOrFromName"].ToString();
            }
            catch (Exception ex)
            {
                pos_TransactionMaster.ToOrFromName = "N/A";
            }

             return pos_TransactionMaster;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Pos_TransactionMaster GetPos_TransactionMasterByID(int pos_TransactionMasterID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetPos_TransactionMasterByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Pos_TransactionMasterID", SqlDbType.Int).Value = pos_TransactionMasterID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetPos_TransactionMasterFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertPos_TransactionMaster(Pos_TransactionMaster pos_TransactionMaster)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertPos_TransactionMaster", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_TransactionMasterID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@TransactionDate", SqlDbType.DateTime).Value = pos_TransactionMaster.TransactionDate;
            cmd.Parameters.Add("@Pos_TransactionTypeID", SqlDbType.Int).Value = pos_TransactionMaster.Pos_TransactionTypeID;
            cmd.Parameters.Add("@TransactionID", SqlDbType.Int).Value = pos_TransactionMaster.TransactionID;
            cmd.Parameters.Add("@ToOrFromID", SqlDbType.Int).Value = pos_TransactionMaster.ToOrFromID;
            cmd.Parameters.Add("@Record", SqlDbType.NVarChar).Value = pos_TransactionMaster.Record;
            cmd.Parameters.Add("@Particulars", SqlDbType.NText).Value = pos_TransactionMaster.Particulars;
            cmd.Parameters.Add("@WorkSatationID", SqlDbType.Int).Value = pos_TransactionMaster.WorkSatationID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = pos_TransactionMaster.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = pos_TransactionMaster.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = pos_TransactionMaster.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = pos_TransactionMaster.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = pos_TransactionMaster.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = pos_TransactionMaster.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = pos_TransactionMaster.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = pos_TransactionMaster.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = pos_TransactionMaster.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = pos_TransactionMaster.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Pos_TransactionMasterID"].Value;
        }
    }

    public int InsertPos_TransactionMaster(Pos_TransactionMaster pos_TransactionMaster,bool isDoubleEntry)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertPos_TransactionMasterDoubleEntry", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_TransactionMasterID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@TransactionDate", SqlDbType.DateTime).Value = pos_TransactionMaster.TransactionDate;
            cmd.Parameters.Add("@Pos_TransactionTypeID", SqlDbType.Int).Value = pos_TransactionMaster.Pos_TransactionTypeID;
            cmd.Parameters.Add("@TransactionID", SqlDbType.Int).Value = pos_TransactionMaster.TransactionID;
            cmd.Parameters.Add("@ToOrFromID", SqlDbType.Int).Value = pos_TransactionMaster.ToOrFromID;
            cmd.Parameters.Add("@Record", SqlDbType.NVarChar).Value = pos_TransactionMaster.Record;
            cmd.Parameters.Add("@Particulars", SqlDbType.NText).Value = pos_TransactionMaster.Particulars;
            cmd.Parameters.Add("@WorkSatationID", SqlDbType.Int).Value = pos_TransactionMaster.WorkSatationID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = pos_TransactionMaster.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = pos_TransactionMaster.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = pos_TransactionMaster.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = pos_TransactionMaster.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = pos_TransactionMaster.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = pos_TransactionMaster.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = pos_TransactionMaster.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = pos_TransactionMaster.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = pos_TransactionMaster.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = pos_TransactionMaster.RowStatusID;
            cmd.Parameters.Add("@IsDouble", SqlDbType.Bit).Value = isDoubleEntry;
            
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Pos_TransactionMasterID"].Value;
        }
    }

    public bool UpdatePos_TransactionMaster(Pos_TransactionMaster pos_TransactionMaster)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdatePos_TransactionMaster", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_TransactionMasterID", SqlDbType.Int).Value = pos_TransactionMaster.Pos_TransactionMasterID;
            cmd.Parameters.Add("@TransactionDate", SqlDbType.DateTime).Value = pos_TransactionMaster.TransactionDate;
            cmd.Parameters.Add("@Pos_TransactionTypeID", SqlDbType.Int).Value = pos_TransactionMaster.Pos_TransactionTypeID;
            cmd.Parameters.Add("@TransactionID", SqlDbType.Int).Value = pos_TransactionMaster.TransactionID;
            cmd.Parameters.Add("@ToOrFromID", SqlDbType.Int).Value = pos_TransactionMaster.ToOrFromID;
            cmd.Parameters.Add("@Record", SqlDbType.NVarChar).Value = pos_TransactionMaster.Record;
            cmd.Parameters.Add("@Particulars", SqlDbType.NText).Value = pos_TransactionMaster.Particulars;
            cmd.Parameters.Add("@WorkSatationID", SqlDbType.Int).Value = pos_TransactionMaster.WorkSatationID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = pos_TransactionMaster.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = pos_TransactionMaster.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = pos_TransactionMaster.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = pos_TransactionMaster.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = pos_TransactionMaster.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = pos_TransactionMaster.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = pos_TransactionMaster.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = pos_TransactionMaster.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = pos_TransactionMaster.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = pos_TransactionMaster.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
