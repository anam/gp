using System;
using System.Data;
using System.Configuration;
using System.Linq;

public class Gift
{
    public Gift()
    {
    }


    public Gift
        (
int  giftID,
string  giftName

        )

    {
this.GiftID = giftID;
this.GiftName = giftName;

    }

    public int  GiftID
    {
        get ; 
        set  ;
    }

    public string  GiftName
    {
        get ; 
        set  ;
    }

}

