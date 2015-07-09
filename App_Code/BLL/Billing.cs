using System;
using System.Data;
using System.Configuration;
using System.Linq;

public class Billing
{
    public Billing()
    {
    }


    public Billing
        (
int  billingID,
int  clientID,
DateTime  paymnetDate,
int  paymnetType,
decimal  amountCharge,
decimal  amountPaid,
        int TreatmentServiceID,
string  entryBy,
DateTime  entryDate

        )

    {
this.BillingID = billingID;
this.ClientID = clientID;
this.PaymnetDate = paymnetDate;
this.PaymnetType = paymnetType;
this.AmountCharge = amountCharge;
this.AmountPaid = amountPaid;
this.TreatmentServiceID = TreatmentServiceID;
this.EntryBy = entryBy;
this.EntryDate = entryDate;

    }

    public int  BillingID
    {
        get ; 
        set  ;
    }

    public int  ClientID
    {
        get ; 
        set  ;
    }

    public DateTime  PaymnetDate
    {
        get ; 
        set  ;
    }

    public int  PaymnetType
    {
        get ; 
        set  ;
    }

    public decimal  AmountCharge
    {
        get ; 
        set  ;
    }

    public decimal  AmountPaid
    {
        get ; 
        set  ;
    }

    public int TreatmentServiceID
    {
        get ; 
        set  ;
    }
    
    public string  EntryBy
    {
        get ; 
        set  ;
    }

    public DateTime  EntryDate
    {
        get ; 
        set  ;
    }

}

