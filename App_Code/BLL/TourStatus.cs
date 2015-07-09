using System;
using System.Data;
using System.Configuration;
using System.Linq;

public class TourStatus
{
    public TourStatus()
    {
    }


    public TourStatus
        (
int  tourStatusID,
string  tourStatusName

        )

    {
this.TourStatusID = tourStatusID;
this.TourStatusName = tourStatusName;

    }

    public int  TourStatusID
    {
        get ; 
        set  ;
    }

    public string  TourStatusName
    {
        get ; 
        set  ;
    }

}

