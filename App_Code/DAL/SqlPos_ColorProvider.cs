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

public class SqlPos_ColorProvider:DataAccessObject
{
	public SqlPos_ColorProvider()
    {
    }


    public bool DeletePos_Color(int pos_ColorID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeletePos_Color", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_ColorID", SqlDbType.Int).Value = pos_ColorID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Pos_Color> GetAllPos_Colors()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllPos_Colors", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetPos_ColorsFromReader(reader);
        }
    }
    public List<Pos_Color> GetPos_ColorsFromReader(IDataReader reader)
    {
        List<Pos_Color> pos_Colors = new List<Pos_Color>();

        while (reader.Read())
        {
            pos_Colors.Add(GetPos_ColorFromReader(reader));
        }
        return pos_Colors;
    }

    public Pos_Color GetPos_ColorFromReader(IDataReader reader)
    {
        try
        {
            Pos_Color pos_Color = new Pos_Color
                (
                    (int)reader["Pos_ColorID"],
                    reader["ColorName"].ToString()
                );
             return pos_Color;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Pos_Color GetPos_ColorByID(int pos_ColorID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetPos_ColorByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Pos_ColorID", SqlDbType.Int).Value = pos_ColorID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetPos_ColorFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertPos_Color(Pos_Color pos_Color)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertPos_Color", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_ColorID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ColorName", SqlDbType.NVarChar).Value = pos_Color.ColorName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Pos_ColorID"].Value;
        }
    }

    public bool UpdatePos_Color(Pos_Color pos_Color)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdatePos_Color", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_ColorID", SqlDbType.Int).Value = pos_Color.Pos_ColorID;
            cmd.Parameters.Add("@ColorName", SqlDbType.NVarChar).Value = pos_Color.ColorName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
