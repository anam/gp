using System;
using System.Data;
using System.Configuration;
using System.Linq;

public class States
{
    public States()
    {
    }


    public States
        (
int  stateID,
string  stateNameFullName,
string  stateNameShortName

        )

    {
this.StateID = stateID;
this.StateNameFullName = stateNameFullName;
this.StateNameShortName = stateNameShortName;

    }

    public int  StateID
    {
        get ; 
        set  ;
    }

    public string  StateNameFullName
    {
        get ; 
        set  ;
    }

    public string  StateNameShortName
    {
        get ; 
        set  ;
    }

}

