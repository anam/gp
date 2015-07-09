using System;
using System.Data;
using System.Configuration;
using System.Linq;

public class Document
{
    public Document()
    {
    }


    public Document
        (
int  documentID,
int  clientID,
string  details,
string  fileName

        )

    {
this.DocumentID = documentID;
this.ClientID = clientID;
this.Details = details;
this.FileName = fileName;

    }

    public int  DocumentID
    {
        get ; 
        set  ;
    }

    public int  ClientID
    {
        get ; 
        set  ;
    }

    public string  Details
    {
        get ; 
        set  ;
    }

    public string  FileName
    {
        get ; 
        set  ;
    }

}

