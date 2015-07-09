using System;
using System.Data;
using System.Configuration;
using System.Linq;

public class Customer
{
    public Customer()
    {
    }


    public Customer
        (
            int  customerID,
            string  firstName,
            string  lastName,
            int  age,
            string  guestName,
            int  guestAge,
            int  relationshipID,
            int  incomeID,
            string  primaryPhone,
            string  secondaryPhone,
            string  address1,
            string  address2,
            string  city,
            int  state,
            string zipcode,
            int  country,
            string  business,
            string  email,
            string guestAccompany,
     string   guestFirstname,
	string guestLastname,
	string guestPhone,
	string guestEmail,
	string guestAddress,
	string guestCity,
	string gueststate,
	string guestZip,
    string guestNotes

        )

    {
        this.CustomerID = customerID;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Age = age;
        this.GuestName = guestName;
        this.GuestAge = guestAge;
        this.RelationshipID = relationshipID;
        this.IncomeID = incomeID;
        this.PrimaryPhone = primaryPhone;
        this.SecondaryPhone = secondaryPhone;
        this.Address1 = address1;
        this.Address2 = address2;
        this.City = city;
        this.State = state;
        this.Zipcode = zipcode;
        this.Country = country;
        this.Business = business;
        this.Email = email;
        this.GuestAccompany = guestAccompany;
    this.GuestFirstname = guestFirstname;
	this.GuestLastname = guestLastname ;
	this.GuestPhone = guestPhone;
	this.GuestEmail = guestEmail ;
	this.GuestAddress = guestAddress ;
	this.GuestCity = guestCity;
	this.Gueststate = gueststate;
	this.GuestZip = guestZip;
    this.GuestNotes = guestNotes;

    }

    public int  CustomerID
    {
        get ; 
        set  ;
    }
    public string Zipcode
    {
        get ; 
        set  ;
    }
    
    public string  FirstName
    {
        get ; 
        set  ;
    }
    public string GuestAccompany
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

    public string  GuestName
    {
        get ; 
        set  ;
    }

    public int  GuestAge
    {
        get ; 
        set  ;
    }

    public int  RelationshipID
    {
        get ; 
        set  ;
    }

    public int  IncomeID
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

    public string  Address1
    {
        get ; 
        set  ;
    }

    public string  Address2
    {
        get ; 
        set  ;
    }

    public string  City
    {
        get ; 
        set  ;
    }

    public int  State
    {
        get ; 
        set  ;
    }

    public int  Country
    {
        get ; 
        set  ;
    }

    public string  Business
    {
        get ; 
        set  ;
    }

    public string  Email
    {
        get ; 
        set  ;
    }

    public string  GuestZip
    {
        get ; 
        set  ;
    }
    public string  Gueststate
    {
        get ; 
        set  ;
    }
    public string  GuestCity
    {
        get ; 
        set  ;
    }
    public string  GuestAddress
    {
        get ; 
        set  ;
    }
    public string  GuestEmail
    {
        get ; 
        set  ;
    }
    public string  GuestPhone
    {
        get ; 
        set  ;
    }
    public string  GuestLastname
    {
        get ; 
        set  ;
    }
    public string  GuestFirstname
    {
        get ; 
        set  ;
    }
    public string GuestNotes
    {
        get ; 
        set  ;
    }
	
	
	
	
	
	
	
	

}

