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

public class SqlPos_SizeProvider:DataAccessObject
{
	public SqlPos_SizeProvider()
    {
    }


    public bool DeletePos_Size(int pos_SizeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeletePos_Size", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_SizeID", SqlDbType.Int).Value = pos_SizeID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Pos_Size> GetAllPos_Sizes()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllPos_Sizes", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetPos_SizesFromReader(reader);
        }
    }
    public List<Pos_Size> GetPos_SizesFromReader(IDataReader reader)
    {
        List<Pos_Size> pos_Sizes = new List<Pos_Size>();

        while (reader.Read())
        {
            pos_Sizes.Add(GetPos_SizeFromReader(reader));
        }
        return pos_Sizes;
    }

    public Pos_Size GetPos_SizeFromReader(IDataReader reader)
    {
        try
        {
            Pos_Size pos_Size = new Pos_Size
                (
                    (int)reader["Pos_SizeID"],
                    reader["SizeName"].ToString(),
                    reader["Code"].ToString()
                );
             return pos_Size;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Pos_Size GetPos_SizeByID(int pos_SizeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetPos_SizeByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Pos_SizeID", SqlDbType.Int).Value = pos_SizeID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetPos_SizeFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertPos_Size(Pos_Size pos_Size)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertPos_Size", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_SizeID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@SizeName", SqlDbType.NVarChar).Value = pos_Size.SizeName;
            cmd.Parameters.Add("@Code", SqlDbType.NChar).Value = pos_Size.Code;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Pos_SizeID"].Value;
        }
    }

    public bool UpdatePos_Size(Pos_Size pos_Size)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdatePos_Size", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_SizeID", SqlDbType.Int).Value = pos_Size.Pos_SizeID;
            cmd.Parameters.Add("@SizeName", SqlDbType.NVarChar).Value = pos_Size.SizeName;
            cmd.Parameters.Add("@Code", SqlDbType.NChar).Value = pos_Size.Code;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
