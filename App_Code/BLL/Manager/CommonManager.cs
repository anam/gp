using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public class CommonManager
{
    public CommonManager()
	{
	}

    public static DataSet SQLExec(string sql)
    {
        DataSet sql_CommonDS = new DataSet();
        SQL_Common sql_Common = new SQL_Common();
        sql_CommonDS = sql_Common.COMN_ExecSQL(sql);
        return sql_CommonDS;
    }


}

