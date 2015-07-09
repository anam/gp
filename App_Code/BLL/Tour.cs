using System;
using System.Data;
using System.Configuration;
using System.Linq;

public class Tour
{
    public Tour()
    {
    }


    public Tour
        (
int  tourID,
int  customerID,
string  tourName,
int  tourStatusID,
        int SalesCenterID,
DateTime  tourDate,
string  tourTime

        )

    {
this.TourID = tourID;
this.CustomerID = customerID;
this.TourName = tourName;
this.TourStatusID = tourStatusID;
this.SalesCenterID = SalesCenterID;
this.TourDate = tourDate;
this.TourTime = tourTime;

    }

    public int  TourID
    {
        get ; 
        set  ;
    }

    public int  CustomerID
    {
        get ; 
        set  ;
    }

    public string  TourName
    {
        get ; 
        set  ;
    }

    public int  TourStatusID
    {
        get ; 
        set  ;
    }
    public int SalesCenterID
    {
        get ; 
        set  ;
    }
    
    public DateTime  TourDate
    {
        get ; 
        set  ;
    }

    public string  TourTime
    {
        get ; 
        set  ;
    }

}

