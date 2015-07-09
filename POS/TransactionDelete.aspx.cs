using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Inventory_PurchasePrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadLoginInHiddenField();
            initialLoad();
            if (Request.QueryString["Pos_TransactionID"] != null
                &&
                Request.QueryString["Pos_TransactionMasterID"] != null
                )
            {
                DeleteSingleTransactionMaster();
                Response.Redirect("TransactionPrint.aspx?Pos_TransactionMasterID=" + Request.QueryString["Pos_TransactionMasterID"]);
            }
            else if (Request.QueryString["Pos_TransactionMasterID"] != null)
            {
                DeleteWholeTransactionMaster();
            }
            Response.Redirect("TransactionPrint.aspx?Pos_TransactionMasterID=" + Request.QueryString["Pos_TransactionMasterID"]);
        }
    }

    private void DeleteSingleTransactionMaster()
    {
        int Pos_TransactionMasterID = int.Parse(Request.QueryString["Pos_TransactionMasterID"]);

        Pos_TransactionMaster pos_TransactionMaster = Pos_TransactionMasterManager.GetPos_TransactionMasterByID(Pos_TransactionMasterID);
        //Item Info
        List<Pos_Product> items = new List<Pos_Product>();
        items = Pos_ProductManager.GetAllPos_ProductsByTrasactionMasterID(Pos_TransactionMasterID);

        string sql = "";
        sql = "Update Pos_TransactionMaster set  UpdatedDate=GetDate(),UpdatedBy=" + getLogin().LoginID.ToString() + @" where Pos_TransactionMasterID=" + Request.QueryString["Pos_TransactionMasterID"] + ";";
        sql += "Update Pos_Transaction set  UpdatedDate=GetDate(),UpdatedBy=" + getLogin().LoginID.ToString() + @",RowStatusID=3 where Pos_ProductTransactionMasterID=" + Request.QueryString["Pos_TransactionMasterID"] + " and  Pos_TransactionID=" + Request.QueryString["Pos_TransactionID"] + ";";
        bool executionProgramed = false;

        switch (pos_TransactionMaster.Pos_TransactionTypeID)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;

            case 4:
                break;

            case 5:
                break;

            case 6:
                break;

            case 7:
                break;

            case 8:
                break;

            case 9: //issue to show room
                executionProgramed = true;
                if (pos_TransactionMaster.ExtraField5 != "Pending")
                {
                    foreach (Pos_Product item in items)
                    {
                        sql += "Update Pos_WorkStationStock set   Stock -= " + item.ExtraField1 + @" where ProductID=" + item.Pos_ProductID.ToString() + @" and WorkStationID=" + pos_TransactionMaster.WorkSatationID.ToString() + ";";
                        sql += "Update Pos_Product set ExtraField1 =(cast(ExtraField1 as decimal(10,2)) + ((" + "+1" + ")*" + item.ExtraField1 + ")) where Pos_ProductID=" + item.Pos_ProductID.ToString() + ";";

                        sql += "Update ACC_JournalMaster set RowStatusID=3 where ExtraField2='POS ISSUE' and ExtraField3='" + Request.QueryString["Pos_TransactionMasterID"] + @"';";
                        sql += "Update ACC_JournalDetail set RowStatusID=3 where JournalMasterID=(select ACC_JournalMasterID from ACC_JournalMaster where ExtraField2='POS ISSUE' and ExtraField3='" + Request.QueryString["Pos_TransactionMasterID"] + @"');";
                    }
                }
                else
                {
                    foreach (Pos_Product item in items)
                    {
                        sql += "Update Pos_Product set ExtraField1 =(cast(ExtraField1 as decimal(10,2)) + ((" + "+1" + ")*" + item.ExtraField1 + ")) where Pos_ProductID=" + item.Pos_ProductID.ToString() + ";";
                    }
                }

                break;

            case 10://issue return to show room
                executionProgramed = true;
                if (pos_TransactionMaster.ExtraField5 != "Pending")
                {
                    foreach (Pos_Product item in items)
                    {
                        sql += "Update Pos_WorkStationStock set   Stock += " + item.ExtraField1 + @" where ProductID=" + item.Pos_ProductID.ToString() + @" and WorkStationID=" + pos_TransactionMaster.WorkSatationID.ToString() + ";";
                        sql += "Update Pos_Product set ExtraField1 =(cast(ExtraField1 as decimal(10,2)) + ((" + "-1" + ")*" + item.ExtraField1 + ")) where Pos_ProductID=" + item.Pos_ProductID.ToString() + ";";
                    }
                }
                else
                {
                    foreach (Pos_Product item in items)
                    {
                        sql += "Update Pos_WorkStationStock set   Stock += " + item.ExtraField1 + @" where ProductID=" + item.Pos_ProductID.ToString() + @" and WorkStationID=" + pos_TransactionMaster.WorkSatationID.ToString() + ";";
                    }
                }
                break;

            case 11: //Send to another Branch 
                {
                    executionProgramed = true;
                    //Sender Delete
                    foreach (Pos_Product item in items)
                    {
                        sql += "Update Pos_WorkStationStock set   Stock += " + item.ExtraField1 + @" where ProductID=" + item.Pos_ProductID.ToString() + @" and WorkStationID=" + pos_TransactionMaster.WorkSatationID.ToString() + ";";
                    }

                    //Receiver Delete
                    Pos_TransactionMasterID++;
                    pos_TransactionMaster = Pos_TransactionMasterManager.GetPos_TransactionMasterByID(Pos_TransactionMasterID);
                    sql = "Update Pos_TransactionMaster set  UpdatedDate=GetDate(),UpdatedBy=" + getLogin().LoginID.ToString() + @",RowStatusID=3 where Pos_TransactionMasterID=" + Pos_TransactionMasterID.ToString() + ";";
                    sql += "Update Pos_Transaction set  UpdatedDate=GetDate(),UpdatedBy=" + getLogin().LoginID.ToString() + @",RowStatusID=3 where Pos_ProductTransactionMasterID=" + Pos_TransactionMasterID.ToString() + ";";

                    //Item Info
                    if (pos_TransactionMaster.ExtraField5 != "Pending")
                    {
                        items = new List<Pos_Product>();
                        items = Pos_ProductManager.GetAllPos_ProductsByTrasactionMasterID(Pos_TransactionMasterID);

                        foreach (Pos_Product item in items)
                        {
                            sql += "Update Pos_WorkStationStock set   Stock -= " + item.ExtraField1 + @" where ProductID=" + item.Pos_ProductID.ToString() + @" and WorkStationID=" + pos_TransactionMaster.WorkSatationID.ToString() + ";";
                        }
                    }
                    break;
                }
            case 12: //Received from another branch
                showAlartMessage("Please ask the sender branch to delete.");
                break;

            case 13: //Sales
                {
                    executionProgramed = true;

                    if (pos_TransactionMaster.UpdatedBy < 0)//when sales return
                    {
                        //cencel the sales return
                        sql += "Update Pos_TransactionMaster set  UpdatedDate=GetDate(),UpdatedBy=" + getLogin().LoginID.ToString() + @",RowStatusID=3 where Pos_TransactionMasterID=" + (-1 * pos_TransactionMaster.UpdatedBy) + ";";
                        sql += "Update Pos_Transaction set  UpdatedDate=GetDate(),UpdatedBy=" + getLogin().LoginID.ToString() + @",RowStatusID=3 where Pos_ProductTransactionMasterID=" + (-1 * pos_TransactionMaster.UpdatedBy) + ";";

                        string returnSales = "select * from Pos_Transaction where Pos_ProductTransactionMasterID=" + (-1 * pos_TransactionMaster.UpdatedBy) + ";";
                        DataSet dsReturnSales = CommonManager.SQLExec(returnSales);
                        bool masterNotUpdated = true;
                        foreach (DataRow dr in dsReturnSales.Tables[0].Rows)
                        {
                            if (masterNotUpdated)
                            {
                                //update the sales of which the sales was returned
                                sql += "Update Pos_TransactionMaster set UpdatedDate=GetDate(), Particulars= cast(Particulars as nvarchar(max))+'<hr/>Sales Return Cancelled' where Pos_TransactionMasterID=(Select Pos_ProductTransactionMasterID from Pos_Transaction where  Pos_TransactionID=" + dr["ExtraField4"] + ");";

                                masterNotUpdated = false;
                            }

                            //revercing the sales return
                            sql += "Update Pos_Transaction set  UpdatedDate=GetDate(),UpdatedBy=" + getLogin().LoginID.ToString() + @",ExtraField4= (cast(ExtraField4 as int)-" + decimal.Parse(dr["Quantity"].ToString()).ToString("0") + ") where Pos_TransactionID=" + dr["ExtraField4"] + ";";
                            sql += "update Pos_WorkStationStock set Stock-=" + decimal.Parse(dr["Quantity"].ToString()).ToString("0") + " where WorkStationID=" + pos_TransactionMaster.WorkSatationID.ToString() + " and ProductID=" + dr["Pos_ProductID"] + ";";

                        }
                    }

                    foreach (Pos_Product item in items)
                    {
                        sql += "Update Pos_WorkStationStock set   Stock += " + item.ExtraField1 + @" where ProductID=" + item.Pos_ProductID.ToString() + @" and WorkStationID=" + pos_TransactionMaster.WorkSatationID.ToString() + ";";
                    }

                    sql += "Update ACC_JournalMaster set RowStatusID=3 where ExtraField2='POS SALE' and ExtraField3='" + Request.QueryString["Pos_TransactionMasterID"] + @"';";
                    sql += "Update ACC_JournalDetail set RowStatusID=3 where JournalMasterID=(select ACC_JournalMasterID from ACC_JournalMaster where ExtraField2='POS SALE' and ExtraField3='" + Request.QueryString["Pos_TransactionMasterID"] + @"');";
                }
                break;
            case 25://Purchase
                { 
                executionProgramed = true;
                string sql_tmp = @"Select * from Pos_Transaction where Pos_TransactionID=" + Request.QueryString["Pos_TransactionID"]
                    + "; Select * from ACC_JournalMaster where Note='" + "Product Purchase-'+(Select cast(TransactionID as nvarchar) from Pos_TransactionMaster where Pos_TransactionMasterID=" + Request.QueryString["Pos_TransactionMasterID"] + ")";

                DataSet ds = CommonManager.SQLExec(sql_tmp);
                if (pos_TransactionMaster.ExtraField5 != "Pending")
                {
                    foreach (Pos_Product item in items)
                    {
                        if(ds.Tables[0].Rows[0]["Pos_ProductID"].ToString() == item.Pos_ProductID.ToString())
                        {
                            sql+="; update Pos_Product set pos_Product.RowStatusID = 3 where Pos_ProductID="+item.Pos_ProductID;
                            sql += "; Update [Pos_Transaction] set [RowStatusID]=3 where Pos_TransactionID=" + Request.QueryString["Pos_TransactionID"];
                            sql += "; Update [ACC_JournalDetail] set [RowStatusID]=3 where [JournalMasterID]=" + ds.Tables[1].Rows[0]["ACC_JournalMasterID"].ToString() + " and ExtraField1='" + item.Pos_ProductID.ToString() + "'";
                            break;
                        }
                    
                     }
                }
                else
                {
                    foreach (Pos_Product item in items)
                    {
                        if (ds.Tables[0].Rows[0]["Pos_ProductID"].ToString() == item.Pos_ProductID.ToString())
                        {
                            sql += "; update Pos_Product set pos_Product.RowStatusID = 3 where Pos_ProductID=" + item.Pos_ProductID;
                            sql += "; Update [Pos_Transaction] set [RowStatusID]=3 where Pos_TransactionID=" + Request.QueryString["Pos_TransactionID"];
                            sql += "; Update [ACC_JournalDetail] set [RowStatusID]=3 where [JournalMasterID]=" + ds.Tables[1].Rows[0]["ACC_JournalMasterID"].ToString() + " and ExtraField1='" + item.Pos_ProductID.ToString()+"'";
                            break;
                        }
                    }
                }

                }
                break;
            default:
                break;
        }

        if (executionProgramed)
        {
            try
            {
                CommonManager.SQLExec(sql);
                showAlartMessage("Delete Successfully");
            }
            catch (Exception ex)
            {
                showAlartMessage("Delete Error");
            }
        }
    }

    private void showAlartMessage(string message)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
             "err_msg",
             "alert('" + message + "');",
             true);
    }

    private void loadLoginInHiddenField()
    {
        if (hfLoginID.Value == "")
        {
            hfLoginID.Value = getLogin().LoginID.ToString();
        }
    }

    private Login getLogin()
    {
        Login login = new Login();

        try
        {
            if (Session["Login"] != null)
            {
                login = (Login)Session["Login"];
            }
            else if (hfLoginID.Value != "")
            {
                login = LoginManager.GetLoginByID(int.Parse(hfLoginID.Value));
            }
            else
            { Session["PreviousPage"] = HttpContext.Current.Request.Url.AbsoluteUri; Response.Redirect("../LoginPage.aspx"); }

        }
        catch (Exception ex)
        { }

        return login;
    }
    private void DeleteWholeTransactionMaster()
    {
        int Pos_TransactionMasterID = int.Parse(Request.QueryString["Pos_TransactionMasterID"]);

        Pos_TransactionMaster pos_TransactionMaster = Pos_TransactionMasterManager.GetPos_TransactionMasterByID(Pos_TransactionMasterID);
        //Item Info
        List<Pos_Product> items = new List<Pos_Product>();
        items = Pos_ProductManager.GetAllPos_ProductsByTrasactionMasterID(Pos_TransactionMasterID);

        string sql = "";
        sql = "Update Pos_TransactionMaster set  UpdatedDate=GetDate(),UpdatedBy=" + getLogin().LoginID.ToString() + @",RowStatusID=3 where Pos_TransactionMasterID=" + Request.QueryString["Pos_TransactionMasterID"] + ";";
        sql += "Update Pos_Transaction set  UpdatedDate=GetDate(),UpdatedBy=" + getLogin().LoginID.ToString() + @",RowStatusID=3 where Pos_ProductTransactionMasterID=" + Request.QueryString["Pos_TransactionMasterID"] + ";";
        
        
        bool executionProgramed = false;

        switch (pos_TransactionMaster.Pos_TransactionTypeID)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;

            case 4:
                break;

            case 5:
                break;

            case 6:
                break;

            case 7:
                break;

            case 8:
                break;

            case 9: //issue to show room
                executionProgramed = true;
                if (pos_TransactionMaster.ExtraField5 != "Pending")
                {
                    foreach (Pos_Product item in items)
                    {
                        sql += "Update Pos_WorkStationStock set   Stock -= " + item.ExtraField1 + @" where ProductID=" + item.Pos_ProductID.ToString() + @" and WorkStationID=" + pos_TransactionMaster.WorkSatationID.ToString() + ";";
                        sql += "Update Pos_Product set ExtraField1 =(cast(ExtraField1 as decimal(10,2)) + ((" + "+1" + ")*" + item.ExtraField1 + ")) where Pos_ProductID=" + item.Pos_ProductID.ToString() + ";";
                        
                        sql += "Update ACC_JournalMaster set RowStatusID=3 where ExtraField2='POS ISSUE' and ExtraField3='" + Request.QueryString["Pos_TransactionMasterID"] + @"';";
                        sql += "Update ACC_JournalDetail set RowStatusID=3 where JournalMasterID=(select ACC_JournalMasterID from ACC_JournalMaster where ExtraField2='POS ISSUE' and ExtraField3='" + Request.QueryString["Pos_TransactionMasterID"] + @"');";
                    }
                }
                else
                {
                    foreach (Pos_Product item in items)
                    {
                        sql += "Update Pos_Product set ExtraField1 =(cast(ExtraField1 as decimal(10,2)) + ((" + "+1" + ")*" + item.ExtraField1 + ")) where Pos_ProductID=" + item.Pos_ProductID.ToString() + ";";
                    }
                }

                break;

            case 10://issue return to show room
                executionProgramed = true;
                if (pos_TransactionMaster.ExtraField5 != "Pending")
                {
                    foreach (Pos_Product item in items)
                    {
                        sql += "Update Pos_WorkStationStock set   Stock += " + item.ExtraField1 + @" where ProductID=" + item.Pos_ProductID.ToString() + @" and WorkStationID=" + pos_TransactionMaster.WorkSatationID.ToString() + ";";
                        sql += "Update Pos_Product set ExtraField1 =(cast(ExtraField1 as decimal(10,2)) + ((" + "-1" + ")*" + item.ExtraField1 + ")) where Pos_ProductID=" + item.Pos_ProductID.ToString() + ";";
                    }
                }
                else
                {
                    foreach (Pos_Product item in items)
                    {
                        sql += "Update Pos_WorkStationStock set   Stock += " + item.ExtraField1 + @" where ProductID=" + item.Pos_ProductID.ToString() + @" and WorkStationID=" + pos_TransactionMaster.WorkSatationID.ToString() + ";";
                    }
                }
                break;

            case 11: //Send to another Branch 
                {
                    executionProgramed = true;
                    //Sender Delete
                    foreach (Pos_Product item in items)
                    {
                        sql += "Update Pos_WorkStationStock set   Stock += " + item.ExtraField1 + @" where ProductID=" + item.Pos_ProductID.ToString() + @" and WorkStationID=" + pos_TransactionMaster.WorkSatationID.ToString() + ";";
                    }

                    //Receiver Delete
                    Pos_TransactionMasterID++;
                    pos_TransactionMaster = Pos_TransactionMasterManager.GetPos_TransactionMasterByID(Pos_TransactionMasterID);
                    sql = "Update Pos_TransactionMaster set  UpdatedDate=GetDate(),UpdatedBy=" + getLogin().LoginID.ToString() + @",RowStatusID=3 where Pos_TransactionMasterID=" + Pos_TransactionMasterID.ToString() + ";";
                    sql += "Update Pos_Transaction set  UpdatedDate=GetDate(),UpdatedBy=" + getLogin().LoginID.ToString() + @",RowStatusID=3 where Pos_ProductTransactionMasterID=" + Pos_TransactionMasterID.ToString() + ";";

                    //Item Info
                    if (pos_TransactionMaster.ExtraField5 != "Pending")
                    {
                        items = new List<Pos_Product>();
                        items = Pos_ProductManager.GetAllPos_ProductsByTrasactionMasterID(Pos_TransactionMasterID);
                    
                        foreach (Pos_Product item in items)
                        {
                            sql += "Update Pos_WorkStationStock set   Stock -= " + item.ExtraField1 + @" where ProductID=" + item.Pos_ProductID.ToString() + @" and WorkStationID=" + pos_TransactionMaster.WorkSatationID.ToString() + ";";
                        }
                    }
                    break;
                }
            case 12: //Received from another branch
                showAlartMessage("Please ask the sender branch to delete.");
                break;

            case 13: //Sales
                {
                    executionProgramed = true;

                    if (pos_TransactionMaster.UpdatedBy < 0)//when sales return
                    {
                        //cencel the sales return
                        sql += "Update Pos_TransactionMaster set  UpdatedDate=GetDate(),UpdatedBy=" + getLogin().LoginID.ToString() + @",RowStatusID=3 where Pos_TransactionMasterID=" + (-1 * pos_TransactionMaster.UpdatedBy) + ";";
                        sql += "Update Pos_Transaction set  UpdatedDate=GetDate(),UpdatedBy=" + getLogin().LoginID.ToString() + @",RowStatusID=3 where Pos_ProductTransactionMasterID=" + (-1 * pos_TransactionMaster.UpdatedBy) + ";";

                        string returnSales = "select * from Pos_Transaction where Pos_ProductTransactionMasterID=" + (-1 * pos_TransactionMaster.UpdatedBy) + ";";
                        DataSet dsReturnSales = CommonManager.SQLExec(returnSales);
                        bool masterNotUpdated = true;
                        foreach (DataRow dr in dsReturnSales.Tables[0].Rows)
                        {
                            if (masterNotUpdated)
                            {
                                //update the sales of which the sales was returned
                                sql += "Update Pos_TransactionMaster set UpdatedDate=GetDate(), Particulars= cast(Particulars as nvarchar(max))+'<hr/>Sales Return Cancelled' where Pos_TransactionMasterID=(Select Pos_ProductTransactionMasterID from Pos_Transaction where  Pos_TransactionID=" + dr["ExtraField4"] + ");";

                                masterNotUpdated = false;
                            }

                            //revercing the sales return
                            sql += "Update Pos_Transaction set  UpdatedDate=GetDate(),UpdatedBy=" + getLogin().LoginID.ToString() + @",ExtraField4= (cast(ExtraField4 as int)-" + decimal.Parse(dr["Quantity"].ToString()).ToString("0") + ") where Pos_TransactionID=" + dr["ExtraField4"] + ";";
                            sql += "update Pos_WorkStationStock set Stock-=" + decimal.Parse(dr["Quantity"].ToString()).ToString("0") + " where WorkStationID=" + pos_TransactionMaster.WorkSatationID.ToString() + " and ProductID=" + dr["Pos_ProductID"] + ";";

                        }
                    }

                    foreach (Pos_Product item in items)
                    {
                        sql += "Update Pos_WorkStationStock set   Stock += " + item.ExtraField1 + @" where ProductID=" + item.Pos_ProductID.ToString() + @" and WorkStationID=" + pos_TransactionMaster.WorkSatationID.ToString() + ";";
                    }

                    sql += "Update ACC_JournalMaster set RowStatusID=3 where ExtraField2='POS SALE' and ExtraField3='" + Request.QueryString["Pos_TransactionMasterID"] + @"';";
                    sql += "Update ACC_JournalDetail set RowStatusID=3 where JournalMasterID=(select ACC_JournalMasterID from ACC_JournalMaster where ExtraField2='POS SALE' and ExtraField3='" + Request.QueryString["Pos_TransactionMasterID"] + @"');";
                }
                break;
            case 25:
                executionProgramed = true;
                sql += "Update ACC_JournalMaster set RowStatusID=3  where Note='" + "Product Purchase-'+(Select cast(TransactionID as nvarchar) from Pos_TransactionMaster where Pos_TransactionMasterID=" + Request.QueryString["Pos_TransactionMasterID"] + ")";
                break;
            default:
                break;
        }

        if (executionProgramed)
        {
            try
            {
                CommonManager.SQLExec(sql);
                showAlartMessage("Delete Successfully");
            }
            catch (Exception ex)
            {
                showAlartMessage("Delete Error");
            }
        }
    }

    private void initialLoad()
    {
       
    }
    protected void btnReceived_Click(object sender, EventArgs e)
    {
        int Pos_TransactionMasterID = int.Parse(Request.QueryString["Pos_TransactionMasterID"]);

        Pos_TransactionMaster pos_TransactionMaster = Pos_TransactionMasterManager.GetPos_TransactionMasterByID(Pos_TransactionMasterID);

        Pos_TransactionType transactionType = Pos_TransactionTypeManager.GetPos_TransactionTypeByID(pos_TransactionMaster.Pos_TransactionTypeID);
        string sql = "Declare @Count int; ";
        sql += "update Pos_TransactionMaster set  ExtraField5='' where Pos_TransactionMasterID=" + Pos_TransactionMasterID + ";";


        List<Pos_Product> items = new List<Pos_Product>();
        items = Pos_ProductManager.GetAllPos_ProductsByTrasactionMasterID(Pos_TransactionMasterID);
        foreach (Pos_Product pos_Transaction in items)
        {


            if (pos_TransactionMaster.WorkSatationID == 1 && transactionType.CentralStockFormula != "0")
            {
                //For head office Central Stock
                sql += "Update Pos_Product set ExtraField1 =(cast(ExtraField1 as decimal(10,2)) + ((" + transactionType.CentralStockFormula + ")*" + pos_Transaction.ExtraField1 + ")) where Pos_ProductID=" + pos_Transaction.Pos_ProductID.ToString() + ";";
            }
            else
                if (pos_TransactionMaster.WorkSatationID != 1 && transactionType.ShowRoomFormula != "0")
                {
                    sql += @"
                            set @Count=
                            (
                            select COUNT(*) from Pos_WorkStationStock
                            where ProductID=" + pos_Transaction.Pos_ProductID.ToString() + @" and WorkStationID=" + pos_TransactionMaster.WorkSatationID + @"
                            )

                            if @Count = 0
                            BEGIN
	                            INSERT INTO [Pos_WorkStationStock]
                                        ([WorkStationID]
                                        ,[ProductID]
                                        ,[Stock])
                                        VALUES(" + pos_TransactionMaster.WorkSatationID + @"," + pos_Transaction.Pos_ProductID.ToString() + @"," + pos_Transaction.ExtraField1 + @");
                            END
                            ELSE
                            BEGIN
                                Update Pos_WorkStationStock set Stock += ((+1)*" + pos_Transaction.ExtraField1 + @") where  WorkStationID=" + pos_TransactionMaster.WorkSatationID + @" and  ProductID=" + pos_Transaction.Pos_ProductID.ToString() + @";
                            END;
                            
                                ";
                }
            CommonManager.SQLExec(sql);
        }

        Response.Redirect("TransactionPrint.aspx?Pos_TransactionMasterID=" + Pos_TransactionMasterID);
    }

}