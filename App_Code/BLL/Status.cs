using System;
using System.Data;
using System.Configuration;
using System.Linq;

public class Status
{
    public Status()
    {
    }


    public Status
        (
int  stastusID,
string  statusPrefix,
int  pageid,
string  statDesc

        )

    {
this.StastusID = stastusID;
this.StatusPrefix = statusPrefix;
this.pageid = pageid;
this.StatDesc = statDesc;

    }

    public int  StastusID
    {
        get ; 
        set  ;
    }

    public string  StatusPrefix
    {
        get ; 
        set  ;
    }

    public int  pageid
    {
        get ; 
        set  ;
    }

    public string  StatDesc
    {
        get ; 
        set  ;
    }

}

