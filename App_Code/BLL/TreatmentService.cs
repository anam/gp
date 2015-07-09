using System;
using System.Data;
using System.Configuration;
using System.Linq;

public class TreatmentService
{
    public TreatmentService()
    {
    }


    public TreatmentService
        (
int  treatmentServiceID,
string  treatmentServiceName

        )

    {
this.TreatmentServiceID = treatmentServiceID;
this.TreatmentServiceName = treatmentServiceName;

    }

    public int  TreatmentServiceID
    {
        get ; 
        set  ;
    }

    public string  TreatmentServiceName
    {
        get ; 
        set  ;
    }

}

