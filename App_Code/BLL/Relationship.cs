using System;
using System.Data;
using System.Configuration;
using System.Linq;

public class Relationship
{
    public Relationship()
    {
    }


    public Relationship
        (
int  relationshipID,
string  relationshipName

        )

    {
this.RelationshipID = relationshipID;
this.RelationshipName = relationshipName;

    }

    public int  RelationshipID
    {
        get ; 
        set  ;
    }

    public string  RelationshipName
    {
        get ; 
        set  ;
    }

}

