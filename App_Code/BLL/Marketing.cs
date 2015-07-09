using System;
using System.Data;
using System.Configuration;
using System.Linq;

public class Marketing
{
    public Marketing()
    {
    }


    public Marketing
        (
int  marketingID,
int  customerID,
int  marketingAgentID,
int  marketingCloserID,
int  marketingVanueID,
int  leadSourceID,
int  giftID,
decimal  depositAmount,
bool  refundable,
string  notes

        )

    {
this.MarketingID = marketingID;
this.CustomerID = customerID;
this.MarketingAgentID = marketingAgentID;
this.MarketingCloserID = marketingCloserID;
this.MarketingVanueID = marketingVanueID;
this.LeadSourceID = leadSourceID;
this.GiftID = giftID;
this.DepositAmount = depositAmount;
this.Refundable = refundable;
this.Notes = notes;

    }

    public int  MarketingID
    {
        get ; 
        set  ;
    }

    public int  CustomerID
    {
        get ; 
        set  ;
    }

    public int  MarketingAgentID
    {
        get ; 
        set  ;
    }

    public int  MarketingCloserID
    {
        get ; 
        set  ;
    }

    public int  MarketingVanueID
    {
        get ; 
        set  ;
    }

    public int  LeadSourceID
    {
        get ; 
        set  ;
    }

    public int  GiftID
    {
        get ; 
        set  ;
    }

    public decimal  DepositAmount
    {
        get ; 
        set  ;
    }

    public bool  Refundable
    {
        get ; 
        set  ;
    }

    public string  Notes
    {
        get ; 
        set  ;
    }

}

