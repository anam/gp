using System;
using System.Data;
using System.Configuration;
using System.Linq;

public class FormRights
{
    public FormRights()
    {
    }


    public FormRights
        (
int  formRightsID,
int  formID,
string  userID,
bool  insertRight,
bool  updateRight,
bool  deleteRight,
bool  selectRight

        )

    {
this.FormRightsID = formRightsID;
this.FormID = formID;
this.UserID = userID;
this.InsertRight = insertRight;
this.UpdateRight = updateRight;
this.DeleteRight = deleteRight;
this.SelectRight = selectRight;

    }

    public int  FormRightsID
    {
        get ; 
        set  ;
    }

    public int  FormID
    {
        get ; 
        set  ;
    }

    public string  UserID
    {
        get ; 
        set  ;
    }

    public bool  InsertRight
    {
        get ; 
        set  ;
    }

    public bool  UpdateRight
    {
        get ; 
        set  ;
    }

    public bool  DeleteRight
    {
        get ; 
        set  ;
    }

    public bool  SelectRight
    {
        get ; 
        set  ;
    }

}

