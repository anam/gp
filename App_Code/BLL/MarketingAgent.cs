using System;
using System.Data;
using System.Configuration;
using System.Linq;

public class MarketingAgent
{
    public MarketingAgent()
    {
    }


    public MarketingAgent
        (
int  marketingAgentID,
string  marketingAgenName

        )

    {
this.MarketingAgentID = marketingAgentID;
this.MarketingAgenName = marketingAgenName;

    }

    public int  MarketingAgentID
    {
        get ; 
        set  ;
    }

    public string  MarketingAgenName
    {
        get ; 
        set  ;
    }

}

