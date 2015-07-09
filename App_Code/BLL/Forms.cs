using System;
using System.Data;
using System.Configuration;
using System.Linq;

public class Forms
{
    public Forms()
    {
    }


    public Forms
        (
int  formsID,
string  name,
string  formPrefix,
string  description

        )

    {
this.FormsID = formsID;
this.Name = name;
this.FormPrefix = formPrefix;
this.Description = description;

    }

    public int  FormsID
    {
        get ; 
        set  ;
    }

    public string  Name
    {
        get ; 
        set  ;
    }

    public string  FormPrefix
    {
        get ; 
        set  ;
    }

    public string  Description
    {
        get ; 
        set  ;
    }

}

