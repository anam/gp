using System;
using System.Data;
using System.Configuration;
using System.Linq;

public class PaymentType
{
    public PaymentType()
    {
    }


    public PaymentType
        (
int  paymnentTypeID,
string  paymnentTypeName

        )

    {
this.PaymnentTypeID = paymnentTypeID;
this.PaymnentTypeName = paymnentTypeName;

    }

    public int  PaymnentTypeID
    {
        get ; 
        set  ;
    }

    public string  PaymnentTypeName
    {
        get ; 
        set  ;
    }

}

