using System;
using System.Data;
using System.Configuration;
using System.Linq;

public class MetroLocation
{
    public MetroLocation()
    {
    }


    public MetroLocation
        (
int  metroLocationID,
string  metroName,
string  state

        )

    {
this.MetroLocationID = metroLocationID;
this.MetroLocationName = metroName;
this.State = state;

    }

    public int  MetroLocationID
    {
        get ; 
        set  ;
    }

    public string  MetroLocationName
    {
        get ; 
        set  ;
    }

    public string  State
    {
        get ; 
        set  ;
    }

}

