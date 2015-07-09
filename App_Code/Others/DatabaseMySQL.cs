using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using MySql.Data.MySqlClient;


public class MySQL
{
    public MySQL()
    {
    }

    public static DataSet SQLExec(string sql)
    {
        DataSet sql_CommonDS = new DataSet();
        DatabaseMySQL sql_Common = new DatabaseMySQL();
        sql_CommonDS = sql_Common.getDataSet(sql);
        return sql_CommonDS;
    }
}
    public class errorLog
    {
        
    public static void write(string errSrcName, string methodName, string exString, string userID) 
    { //errSrcFile = errSrcFile.Substring(4,errSrcFile.Length-9);
        
        string errMsg = errSrcName + "~" + methodName + "~" + exString + "~" + System.DateTime.Now.ToString() + "~" + userID; 
        string filePath = "C:\\ErrorLog\\CygnusLog.txt"; 
        StreamWriter sw = null;
        try { 
                if (File.Exists(filePath)) 
                {
                    sw = File.AppendText(filePath);
                    sw.WriteLine(errMsg); 
                    sw.Close();
                } 
                else 
                { 
                    sw = File.CreateText(filePath);
                    sw.WriteLine(errMsg);
                    sw.Close();
                    
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }

        
    }
    
    public class DatabaseMySQL
	{
		//public SqlConnection conn;
        public  MySqlConnection conn ;

        public DatabaseMySQL()
		{
		}

		public void Open() 
		{
			if (conn == null) 
			{
                //conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Remote_ConnectionString"].ConnectionString);
				
                conn.Open();
			}				
		}

		public void Close() 
		{
			if (conn != null)
			{
				conn.Close();
				conn.Dispose();
				conn = null;
			}
		}

        public MySqlDataReader getList(string strSQL)
		{
			Open();
            MySqlCommand cmd = new MySqlCommand(strSQL, this.conn);
			MySqlDataReader dr = null;
			try
			{
				dr = cmd.ExecuteReader();
			}
			finally
			{
				cmd.Dispose();
			}
			return dr;
		}

		public DataSet getDataSet(string strSQL)
		{
			Open();

            MySqlCommand cmd = new MySqlCommand(strSQL, conn);
			//SqlCommand cmd = new  MySqlCommand(strSQL,conn);

			MySqlDataAdapter da = new MySqlDataAdapter();
			da.SelectCommand = cmd;

			DataSet ds = new DataSet();
			da.Fill(ds, "dataset");

			Close();
			return ds;
		}

		/// <summary>
		/// you must request for two fields in sql for this function
		/// </summary>
		/// <param name="cmb">Target DropDownList or ComboBox</param>
		/// <param name="tag">Enter Attribute of the DDL ie,Customer,supplier</param>
		/// <param name="strSQL">Executing Query</param>
		public void fillCombo(DropDownList cmb,string tag,string strSQL )
		{
			cmb.Items.Clear();
			if ( tag.Length > 0 )
				cmb.Items.Add(new ListItem("Select " + tag,"0"));

			MySqlDataReader dr = this.getList(strSQL);
			while(dr.Read())
			{
				cmb.Items.Add(new ListItem(dr.GetValue(1).ToString(),dr.GetValue(0).ToString()));
			}

		}

       

		public bool executeNonQry (string strSQL)
		{
			Open();
            MySqlCommand cmd = new MySqlCommand(strSQL, this.conn);
			try 
			{
				if ( cmd.ExecuteNonQuery() > 0 )
					return true;
				else
					return false ;
			}
			catch(Exception ex)
			{
				string err ;
				err = ex.ToString();
				return false;
			}
			finally
			{
				cmd.Dispose() ;
				Close() ;
			}
		}

        public MySqlParameter MakeInParam(string ParamName, MySqlDbType DbType, int Size, object Value)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
        }

        public MySqlParameter MakeOutParam(string ParamName, MySqlDbType DbType, int Size)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Output, null);
        }

        public MySqlParameter MakeParam(string ParamName, MySqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
        {
            MySqlParameter param;

            if (Size > 0)
                param = new MySqlParameter(ParamName, DbType, Size);
            else
                param = new MySqlParameter(ParamName, DbType);

            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null))
                param.Value = Value;


            if ((Direction == ParameterDirection.Input && Value.ToString().Length < 1))
                param.Value = DBNull.Value;



            return param;
        }

        //public int RunProc(string procName, MySqlParameter[] prams)
        //{
        //    MySqlCommand cmd = CreateCommand(procName, prams);
        //    cmd.ExecuteNonQuery();
        //    this.Close();
        //    return (int)cmd.Parameters["ReturnValue"].Value;
        //}

        //private MySqlCommand CreateCommand(string procName, MySqlParameter[] prams)
        //{
        //    // make sure connection is open
        //    Open();

        //    //command = new  MySqlCommand( sprocName, new SqlConnection( ConfigManager.DALConnectionString ) );
        //    MySqlCommand cmd = new  MySqlCommand(procName, conn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    // add proc parameters
        //    if (prams != null)
        //    {
        //        foreach (MySqlParameter parameter in prams)
        //            cmd.Parameters.Add(parameter);
        //    }

        //    // return param
        //    cmd.Parameters.Add(
        //        new MySqlParameter("ReturnValue", MySqlDbType.int, 4,ParameterDirection.ReturnValue, false, 0, 0,string.Empty, DataRowVersion.Default, null));

        //    return cmd;
        //}
		public long maxID (string tbName,string field)
		{
			try
			{
				Open();
				string strSQL="select max(" + field + ") from " + tbName ;
                MySqlCommand cmd = new MySqlCommand(strSQL, this.conn);
				long maxIDField= Convert.ToInt32(cmd.ExecuteScalar());
				cmd=null;
				return maxIDField + 1;
			}
			catch
			{
				return 1;
			}
		}

		public string GetSingleValue (string strSQL)
		{
			Open();
            MySqlCommand cmd = new MySqlCommand(strSQL, this.conn);
			try
			{
				string strValue = Convert.ToString( cmd.ExecuteScalar() ) ;
				return strValue ;
			}
			catch(Exception ex)
			{
				string strEx = ex.Message ;
				return "" ;
			}
			finally
			{
				cmd.Dispose() ;
				Close() ;
			}
		}

		public MySqlDataReader OkLogin(string email,string pass)
		{
			string strSQL = "select CustID,firstName,lastName,email,company,suiteNo,workPhone,street1,city1,zip1,state1 from tblCustomer where email='" + email + "' and pass='" + pass + "'" ;
			MySqlDataReader dr = this.getList(strSQL) ;
			return dr ;
		}

	}

