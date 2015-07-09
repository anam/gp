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

public class SqlPos_CardBankProvider:DataAccessObject
{
	public SqlPos_CardBankProvider()
    {
    }


    public bool DeletePos_CardBank(int pos_CardBankID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeletePos_CardBank", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_CardBankID", SqlDbType.Int).Value = pos_CardBankID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Pos_CardBank> GetAllPos_CardBanks()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllPos_CardBanks", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetPos_CardBanksFromReader(reader);
        }
    }
    public List<Pos_CardBank> GetPos_CardBanksFromReader(IDataReader reader)
    {
        List<Pos_CardBank> pos_CardBanks = new List<Pos_CardBank>();

        while (reader.Read())
        {
            pos_CardBanks.Add(GetPos_CardBankFromReader(reader));
        }
        return pos_CardBanks;
    }

    public Pos_CardBank GetPos_CardBankFromReader(IDataReader reader)
    {
        try
        {
            Pos_CardBank pos_CardBank = new Pos_CardBank
                (
                    (int)reader["Pos_CardBankID"],
                    reader["BankName"].ToString(),
                    reader["Details"].ToString(),
                    (int)reader["CharOfAccountLabel4ID"]
                );
             return pos_CardBank;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Pos_CardBank GetPos_CardBankByID(int pos_CardBankID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetPos_CardBankByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Pos_CardBankID", SqlDbType.Int).Value = pos_CardBankID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetPos_CardBankFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertPos_CardBank(Pos_CardBank pos_CardBank)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertPos_CardBank", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_CardBankID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@BankName", SqlDbType.NVarChar).Value = pos_CardBank.BankName;
            cmd.Parameters.Add("@Details", SqlDbType.NText).Value = pos_CardBank.Details;
            cmd.Parameters.Add("@CharOfAccountLabel4ID", SqlDbType.Int).Value = pos_CardBank.CharOfAccountLabel4ID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Pos_CardBankID"].Value;
        }
    }

    public bool UpdatePos_CardBank(Pos_CardBank pos_CardBank)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdatePos_CardBank", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_CardBankID", SqlDbType.Int).Value = pos_CardBank.Pos_CardBankID;
            cmd.Parameters.Add("@BankName", SqlDbType.NVarChar).Value = pos_CardBank.BankName;
            cmd.Parameters.Add("@Details", SqlDbType.NText).Value = pos_CardBank.Details;
            cmd.Parameters.Add("@CharOfAccountLabel4ID", SqlDbType.Int).Value = pos_CardBank.CharOfAccountLabel4ID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
