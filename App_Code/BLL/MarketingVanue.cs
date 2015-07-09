using System;
using System.Data;
using System.Configuration;
using System.Linq;

public class MarketingVanue
{
    public MarketingVanue()
    {
    }


    public MarketingVanue
        (
int  marketingVanueID,
string  marketingVanueName

        )

    {
this.MarketingVanueID = marketingVanueID;
this.MarketingVanueName = marketingVanueName;

    }

    public int  MarketingVanueID
    {
        get ; 
        set  ;
    }

    public string  MarketingVanueName
    {
        get ; 
        set  ;
    }

}

