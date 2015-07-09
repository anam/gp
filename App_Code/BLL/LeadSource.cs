using System;
using System.Data;
using System.Configuration;
using System.Linq;

public class LeadSource
{
    public LeadSource()
    {
    }


    public LeadSource
        (
int  leadSourceID,
string  leadSourceName

        )

    {
this.LeadSourceID = leadSourceID;
this.LeadSourceName = leadSourceName;

    }

    public int  LeadSourceID
    {
        get ; 
        set  ;
    }

    public string  LeadSourceName
    {
        get ; 
        set  ;
    }

}

