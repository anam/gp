using System;
using System.Data;
using System.Configuration;
using System.Linq;

public class Appointmnet
{
    public Appointmnet()
    {
    }


    public Appointmnet
        (
int  appointmnetID,
int  clientID,
DateTime  date,
string  time,
string  createBy,
string  city,
int  metroLocationID,
int  stastusID,
DateTime  createdDate

        )

    {
this.AppointmnetID = appointmnetID;
this.ClientID = clientID;
this.Date = date;
this.Time = time;
this.CreateBy = createBy;
this.City = city;
this.MetroLocationID = metroLocationID;
this.StastusID = stastusID;
this.CreatedDate = createdDate;

    }

    public int  AppointmnetID
    {
        get ; 
        set  ;
    }

    public int  ClientID
    {
        get ; 
        set  ;
    }

    public DateTime  Date
    {
        get ; 
        set  ;
    }

    public string  Time
    {
        get ; 
        set  ;
    }

    public string  CreateBy
    {
        get ; 
        set  ;
    }

    public string  City
    {
        get ; 
        set  ;
    }

    public int  MetroLocationID
    {
        get ; 
        set  ;
    }

    public int  StastusID
    {
        get ; 
        set  ;
    }

    public DateTime  CreatedDate
    {
        get ; 
        set  ;
    }

}

