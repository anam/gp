using System;
using System.Data;
using System.Configuration;
using System.Linq;

public class Income
{
    public Income()
    {
    }


    public Income
        (
int  incomeID,
string  incomeName

        )

    {
this.IncomeID = incomeID;
this.IncomeName = incomeName;

    }

    public int  IncomeID
    {
        get ; 
        set  ;
    }

    public string  IncomeName
    {
        get ; 
        set  ;
    }

}

