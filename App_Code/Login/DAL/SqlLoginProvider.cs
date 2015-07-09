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

public class SqlLoginProvider:DataAccessObject
{
	public SqlLoginProvider()
    {
    }


    public bool DeleteLogin(int loginID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_DeleteLogin", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = loginID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Login> GetAllLogins()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetAllLogins", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetLoginsFromReader(reader);
        }
    }

    public DataSet GetAllLoginForMarketingUser()
    {
        DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("Login_GetAllLoginForMarketingUser", connection))
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

    public DataSet GetAllCloserWhoHasCustomer()
    {
        DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("Login_GetAllCloserWhoHasCustomer", connection))
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

    public List<Login> GetAllLoginsByLoginName(string loginName)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetLoginByLoginName", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@LoginName", SqlDbType.NVarChar).Value = loginName;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetLoginsFromReader(reader);
        }
    }

    public List<Login> GetLoginsFromReader(IDataReader reader)
    {
        List<Login> logins = new List<Login>();

        while (reader.Read())
        {
            logins.Add(GetLoginFromReader(reader));
        }
        return logins;
    }

    public Login GetLoginFromReader(IDataReader reader)
    {
        try
        {
            Login login = new Login
                (
                    (int)reader["LoginID"],
                    reader["LoginName"].ToString(),
                    reader["Password"].ToString(),
                    reader["Email"].ToString(),
                    reader["FirstName"].ToString(),
                    reader["MiddleName"].ToString(),
                    reader["LastName"].ToString(),
                    reader["CellPhone"].ToString(),
                    reader["HomePhone"].ToString(),
                    reader["WorkPhone"].ToString(),
                    (int)reader["RowStatusID"],
                    reader["AddedBy"].ToString(),
                    (DateTime)reader["AddedDate"],
                    reader["UpdatedBy"].ToString(),
                    (DateTime)reader["UpdatedDate"],
                    reader["Details"].ToString(),
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
             return login;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Login GetLoginByID(int loginID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetLoginByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@LoginID", SqlDbType.Int).Value = loginID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetLoginFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public Login GetLoginByLoginName(string LoginName)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("Login_GetLoginByLoginName", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@LoginName", SqlDbType.NVarChar).Value = LoginName;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetLoginFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }


    public int InsertLogin(Login login)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_InsertLogin", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LoginID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@LoginName", SqlDbType.NVarChar).Value = login.LoginName;
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = login.Password;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = login.Email;
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = login.FirstName;
            cmd.Parameters.Add("@MiddleName", SqlDbType.NVarChar).Value = login.MiddleName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = login.LastName;
            cmd.Parameters.Add("@CellPhone", SqlDbType.NVarChar).Value = login.CellPhone;
            cmd.Parameters.Add("@HomePhone", SqlDbType.NVarChar).Value = login.HomePhone;
            cmd.Parameters.Add("@WorkPhone", SqlDbType.NVarChar).Value = login.WorkPhone;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = login.RowStatusID;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = login.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = login.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar).Value = login.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = login.UpdatedDate;
            cmd.Parameters.Add("@Details", SqlDbType.NText).Value = login.Details;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = login.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = login.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = login.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = login.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = login.ExtraField5;
            cmd.Parameters.Add("@ExtraField6", SqlDbType.NVarChar).Value = login.ExtraField6;
            cmd.Parameters.Add("@ExtraField7", SqlDbType.NVarChar).Value = login.ExtraField7;
            cmd.Parameters.Add("@ExtraField8", SqlDbType.NVarChar).Value = login.ExtraField8;
            cmd.Parameters.Add("@ExtraField9", SqlDbType.NVarChar).Value = login.ExtraField9;
            cmd.Parameters.Add("@ExtraField10", SqlDbType.NVarChar).Value = login.ExtraField10;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@LoginID"].Value;
        }
    }

    public bool UpdateLogin(Login login)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Login_UpdateLogin", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LoginID", SqlDbType.Int).Value = login.LoginID;
            cmd.Parameters.Add("@LoginName", SqlDbType.NVarChar).Value = login.LoginName;
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = login.Password;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = login.Email;
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = login.FirstName;
            cmd.Parameters.Add("@MiddleName", SqlDbType.NVarChar).Value = login.MiddleName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = login.LastName;
            cmd.Parameters.Add("@CellPhone", SqlDbType.NVarChar).Value = login.CellPhone;
            cmd.Parameters.Add("@HomePhone", SqlDbType.NVarChar).Value = login.HomePhone;
            cmd.Parameters.Add("@WorkPhone", SqlDbType.NVarChar).Value = login.WorkPhone;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = login.RowStatusID;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = login.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = login.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar).Value = login.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = login.UpdatedDate;
            cmd.Parameters.Add("@Details", SqlDbType.NText).Value = login.Details;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = login.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = login.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = login.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = login.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = login.ExtraField5;
            cmd.Parameters.Add("@ExtraField6", SqlDbType.NVarChar).Value = login.ExtraField6;
            cmd.Parameters.Add("@ExtraField7", SqlDbType.NVarChar).Value = login.ExtraField7;
            cmd.Parameters.Add("@ExtraField8", SqlDbType.NVarChar).Value = login.ExtraField8;
            cmd.Parameters.Add("@ExtraField9", SqlDbType.NVarChar).Value = login.ExtraField9;
            cmd.Parameters.Add("@ExtraField10", SqlDbType.NVarChar).Value = login.ExtraField10;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
