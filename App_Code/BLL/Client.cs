using System;
using System.Data;
using System.Configuration;
using System.Linq;

public class Client
{
    public Client()
    {
    }


    public Client
        (
int  clientID,
string  firstName,
string  lastName,
int  age,
string  primaryPhone,
string  secondaryPhone,
string  email,
string  address,
string  city,
string state,
string  zipCode,
string  sex,
int  metroLocationID,
int ReferralSourceID,
string  notes,
        Boolean IsSignedUp,
        string SignUpMessage,
string  createdBy,
DateTime  createdDate

        )

    {
this.ClientID = clientID;
this.FirstName = firstName;
this.LastName = lastName;
this.Age = age;
this.PrimaryPhone = primaryPhone;
this.SecondaryPhone = secondaryPhone;
this.Email = email;
this.Address = address;
this.City = city;
this.State = state;
this.ZipCode = zipCode;
this.Sex = sex;
this.MetroLocationID = metroLocationID;
this.ReferralSourceID = ReferralSourceID;
this.Notes = notes;
this.IsSignedUp = IsSignedUp;
this.SignUpMessage = SignUpMessage;
this.CreatedBy = createdBy;
this.CreatedDate = createdDate;

    }

    public int  ClientID
    {
        get ; 
        set  ;
    }

    public string  FirstName
    {
        get ; 
        set  ;
    }

    public string  LastName
    {
        get ; 
        set  ;
    }

    public int  Age
    {
        get ; 
        set  ;
    }

    public string  PrimaryPhone
    {
        get ; 
        set  ;
    }

    public string  SecondaryPhone
    {
        get ; 
        set  ;
    }

    public string  Email
    {
        get ; 
        set  ;
    }

    public string  Address
    {
        get ; 
        set  ;
    }

    public string  City
    {
        get ; 
        set  ;
    }

    public string State
    {
        get ; 
        set  ;
    }

    public string  ZipCode
    {
        get ; 
        set  ;
    }

    public string  Sex
    {
        get ; 
        set  ;
    }

    public int  MetroLocationID
    {
        get ; 
        set  ;
    }

    public int ReferralSourceID
    {
        get ; 
        set  ;
    }

    public string  Notes
    {
        get ; 
        set  ;
    }
    public string SignUpMessage
    {
        get ; 
        set  ;
    }


    
    public Boolean IsSignedUp
    {
        get ; 
        set  ;
    }
    public string  CreatedBy
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

