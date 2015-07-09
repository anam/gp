using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Auto_SQL
/// </summary>
public class Auto_SQL
{
	public Auto_SQL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string add(
        string SQLString
        ,string Status
        ,string ForWorkStationID
        ,string ToWorkStationID
        ,string FromID
        ,string FromTime
        ,string UploadTime
        ,string ExecuteTime
        ,string ExtraField1
        ,string ExtraField2
        ,string ExtraField3
        ,string ExtraField4
        ,string ExtraField5
        )
    {
      string  sql = @"INSERT INTO Auto_SQL
           (SQLString
           ,Status
           ,ForWorkStationID
           ,ToWorkStationID
           ,FromID
           ,FromTime
           ,UploadTime
           ,ExecuteTime
           ,ExtraField1
           ,ExtraField2
           ,ExtraField3
           ,ExtraField4
           ,ExtraField5)
     VALUES
           ('" + SQLString.Replace("'", "''") + @"'
           ," +Status + @"
           ," +ForWorkStationID + @"
           ," + ToWorkStationID + @"
           ,"+FromID+@"
           ,'" + FromTime + @"'
           ,'" + UploadTime + @"'
           ,'" + ExecuteTime + @"'
           ,'" + ExtraField1 + @"'
           ,'" + ExtraField2 + @"'
           ,'" + ExtraField3 + @"'
           ,'" + ExtraField4 + @"'
           ,'" + ExtraField5 + @"'
            );
            ";

      return sql;
    }
}