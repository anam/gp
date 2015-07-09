using System;
using System.Data;
using System.Configuration;
using System.Linq;

public class ReferralSource
{
    public ReferralSource()
    {
    }


    public ReferralSource
        (
int  referralSourceID,
string  referralSourceName,
string  referralType,
string  referralStyle,
string  referralStation,
int  metroLocationID

        )

    {
this.ReferralSourceID = referralSourceID;
this.ReferralSourceName = referralSourceName;
this.ReferralType = referralType;
this.ReferralStyle = referralStyle;
this.ReferralStation = referralStation;
this.MetroLocationID = metroLocationID;

    }

    public int  ReferralSourceID
    {
        get ; 
        set  ;
    }

    public string  ReferralSourceName
    {
        get ; 
        set  ;
    }

    public string  ReferralType
    {
        get ; 
        set  ;
    }

    public string  ReferralStyle
    {
        get ; 
        set  ;
    }

    public string  ReferralStation
    {
        get ; 
        set  ;
    }

    public int  MetroLocationID
    {
        get ; 
        set  ;
    }

}

