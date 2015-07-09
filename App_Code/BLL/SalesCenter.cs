using System;
using System.Data;
using System.Configuration;
using System.Linq;

public class SalesCenter
{
    public SalesCenter()
    {
    }


    public SalesCenter
        (
int  salesCenterId,
string  salesCenterName

        )

    {
this.SalesCenterId = salesCenterId;
this.SalesCenterName = salesCenterName;

    }

    public int  SalesCenterId
    {
        get ; 
        set  ;
    }

    public string  SalesCenterName
    {
        get ; 
        set  ;
    }

}

