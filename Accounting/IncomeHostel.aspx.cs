using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Accounting_DayendSummaryEntry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadLoginInHiddenField();
            //loadACC_ChartOfAccountLabel4();
            loadACC_ChartOfAccountLabel3();
            loadBalanceDDL();
            txtJournalDate.Text = DateTime.Today.ToString("dd MMM yyyy");

            ddlBalance.SelectedValue = "1@1";
            ddlBalance_SelectedIndexChanged(this,new EventArgs());
        }
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

    private void loadACC_ChartOfAccountLabel3()
    {
        List<ACC_ChartOfAccountLabel3> aCC_ChartOfAccountLabel3s = new List<ACC_ChartOfAccountLabel3>();
        List<ACC_ChartOfAccountLabel3> aCC_ChartOfAccountLabel3sExpences = new List<ACC_ChartOfAccountLabel3>();
        aCC_ChartOfAccountLabel3s = ACC_ChartOfAccountLabel3Manager.GetAllACC_ChartOfAccountLabel3sForJournalEntryForDropDownList();
        foreach (ACC_ChartOfAccountLabel3 aCC_ChartOfAccountLabel3 in aCC_ChartOfAccountLabel3s)
        {
            ListItem item = new ListItem(aCC_ChartOfAccountLabel3.ChartOfAccountLabel3Text.ToString(), aCC_ChartOfAccountLabel3.ExtraField1.ToString() + "@" + aCC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel3ID.ToString());
            ddlAllACC_ChartOfAccountLabel3.Items.Add(item);

            //load Expence
            if (aCC_ChartOfAccountLabel3.ExtraField2 == "3")
            {
                aCC_ChartOfAccountLabel3.ChartOfAccountLabel3Text = aCC_ChartOfAccountLabel3.ChartOfAccountLabel3Text.Split('>')[1].Replace(")","-->")+ aCC_ChartOfAccountLabel3.ChartOfAccountLabel3Text.Split('(')[0];
                aCC_ChartOfAccountLabel3sExpences.Add(aCC_ChartOfAccountLabel3);
            }
        }

        gvACC_ChartOfAccountLabel3.DataSource = aCC_ChartOfAccountLabel3sExpences;
        gvACC_ChartOfAccountLabel3.DataBind();

        foreach (GridViewRow gvr in gvACC_ChartOfAccountLabel3.Rows)
        {
            DropDownList ddlACC_ChartOfAccountLabel4 =(DropDownList)gvr.FindControl("ddlACC_ChartOfAccountLabel4");

            foreach (ListItem item in ddlAllACC_ChartOfAccountLabel4.Items)
            {
                ddlACC_ChartOfAccountLabel4.Items.Add(item);
            }

            //((TextBox)gvr.FindControl("txtJournalDate")).Text = DateTime.Today.ToString("dd MMM yyyy");
            ((TextBox)gvr.FindControl("txtCheckDate")).Text = DateTime.Today.ToString("dd MMM yyyy");
        }
    }

    private void loadACC_ChartOfAccountLabel4()
    {
        ddlAllACC_ChartOfAccountLabel4.Items.Add(new ListItem("Select", "0"));
        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        aCC_ChartOfAccountLabel4s = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4sForJournalEntry();

        List<ACC_HeadType> allACC_HeadType= ACC_HeadTypeManager.GetAllACC_HeadTypes();
        foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
        {
            /*
             ACC_HeadTypeID	HeadTypeName
1	Work Station
2	Raw Materials
3	Products
4	Employee
5	Bank Account
6	Supplier
7	ShareHolder
8	Others
             */
            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 1 ||
                aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 4 ||
                aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 5 ||
                aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 6 ||
                aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 8
                )
            {
                foreach (ACC_HeadType aCC_HeadType in allACC_HeadType)
                {
                    if (aCC_HeadType.ACC_HeadTypeID == aCC_ChartOfAccountLabel4.ACC_HeadTypeID)
                    {
                        aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text = aCC_HeadType.HeadTypeName+" -> "+ aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text; 
                    }
                }

                ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlAllACC_ChartOfAccountLabel4.Items.Add(item);

                
            }

            
        }
    }

    protected void btnJournalSubmit_Click(object sender, EventArgs e)
    {
        List<ACC_HeadType> allACC_HeadType= ACC_HeadTypeManager.GetAllACC_HeadTypes();

        foreach (GridViewRow gvr in gvACC_ChartOfAccountLabel3.Rows)
        {
            
                HiddenField hfChartOfAccountLabel3ID = (HiddenField)gvr.FindControl("hfChartOfAccountLabel3ID");
                //DropDownList ddlACC_ChartOfAccountLabel4 = (DropDownList)gvr.FindControl("ddlACC_ChartOfAccountLabel4");
                Label lblChartOfAccountLabel3Text = (Label)gvr.FindControl("lblChartOfAccountLabel3Text");

                Label lblJournalMasterID = (Label)gvr.FindControl("lblJournalMasterID");
                
                TextBox txtAmount = (TextBox)gvr.FindControl("txtAmount");
                TextBox txtPayto = (TextBox)gvr.FindControl("txtPayto");
                TextBox txtAddress = (TextBox)gvr.FindControl("txtAddress");
                TextBox txtNote = (TextBox)gvr.FindControl("txtNote");
                TextBox txtCheckDate = (TextBox)gvr.FindControl("txtCheckDate");
                TextBox txtCheckNo = (TextBox)gvr.FindControl("txtCheckNo");
                TextBox txtBank = (TextBox)gvr.FindControl("txtBank");

                if (txtAmount.Text == "0")
                {
                    continue;
                }
                try
                {
                ACC_JournalMaster aCC_JournalMaster = new ACC_JournalMaster();

                aCC_JournalMaster.JournalMasterName = "1";//Voucher Type
                aCC_JournalMaster.ExtraField1 = txtPayto.Text;
                aCC_JournalMaster.ExtraField2 = txtAddress.Text;
                aCC_JournalMaster.ExtraField3 = "";
                aCC_JournalMaster.Note = txtNote.Text;
                aCC_JournalMaster.JournalDate = DateTime.Parse(txtJournalDate.Text);
                aCC_JournalMaster.AddedBy = getLogin().LoginID;
                aCC_JournalMaster.AddedDate = DateTime.Now;
                aCC_JournalMaster.UpdatedBy = getLogin().LoginID;
                aCC_JournalMaster.UpdatedDate = DateTime.Now;
                aCC_JournalMaster.RowStatusID = 1;
                int JournalMasterID = 0;
                JournalMasterID = ACC_JournalMasterManager.InsertACC_JournalMaster(aCC_JournalMaster);
                
                ACC_JournalDetail aCC_JournalDetail = new ACC_JournalDetail();
                aCC_JournalDetail.JournalMasterID = JournalMasterID;

                //if(ddlACC_ChartOfAccountLabel4.SelectedValue != "0")
                //{
                //    aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = Int32.Parse(ddlACC_ChartOfAccountLabel4.SelectedValue);
                //    aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = ddlACC_ChartOfAccountLabel4.SelectedItem.Text;

                //}
                //else
                //{
                    aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = 0;
                    aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = "N/A";
                //}

                aCC_JournalDetail.ACC_ChartOfAccountLabel3Text = lblChartOfAccountLabel3Text.Text;
                aCC_JournalDetail.ACC_ChartOfAccountLabel3ID = Int32.Parse(hfChartOfAccountLabel3ID.Value);

                string workStationText = "";
                foreach (ACC_HeadType item in allACC_HeadType)
                {
                    if (item.ACC_HeadTypeID == 1)
                    {
                        workStationText = item.HeadTypeName;
                        break;
                    }
                }

                //if (ddlACC_ChartOfAccountLabel4.SelectedItem.Text.Contains(workStationText))
                //{
                //    aCC_JournalDetail.WorkStation = aCC_JournalDetail.ACC_ChartOfAccountLabel4ID;
                //    aCC_JournalDetail.WorkStationName = aCC_JournalDetail.ACC_ChartOfAccountLabel4Text;
                //}
                //else
                //{
                    aCC_JournalDetail.WorkStation = 1;
                    aCC_JournalDetail.WorkStationName = "Head Office";
                //}


                    aCC_JournalDetail.Credit = Decimal.Parse(txtAmount.Text);
                    aCC_JournalDetail.Debit = Decimal.Parse("0");

                aCC_JournalDetail.ExtraField3 = "";
                aCC_JournalDetail.ExtraField2 = "";
                aCC_JournalDetail.ExtraField1 = "";
                aCC_JournalDetail.AddedBy = getLogin().LoginID;
                aCC_JournalDetail.AddedDate = DateTime.Now;
                aCC_JournalDetail.UpdatedBy = getLogin().LoginID;
                aCC_JournalDetail.UpdatedDate = DateTime.Now;
                aCC_JournalDetail.RowStatusID = 1;

                //For Debit part
                ACC_JournalDetailManager.InsertACC_JournalDetail(aCC_JournalDetail);

                //For Money Entry
                aCC_JournalDetail.ACC_ChartOfAccountLabel3Text ="";
                aCC_JournalDetail.ACC_ChartOfAccountLabel3ID = int.Parse(ddlBalance.SelectedValue.Split('@')[0]);
                aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = int.Parse(ddlBalance.SelectedValue.Split('@')[1]);
                aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = "";
                aCC_JournalDetail.Credit = 0;
                aCC_JournalDetail.Debit = Decimal.Parse(txtAmount.Text);
                aCC_JournalDetail.WorkStation = 1;
                aCC_JournalDetail.WorkStationName = "Head Office";

                aCC_JournalDetail.ExtraField3 = txtCheckNo.Text;
                aCC_JournalDetail.ExtraField2 = txtBank.Text;
                aCC_JournalDetail.ExtraField1 = txtCheckDate.Text;

                ACC_JournalDetailManager.InsertACC_JournalDetail(aCC_JournalDetail);

                lblJournalMasterID.Text = "<a href='Voucherprint.aspx?JournalMasterID=" + JournalMasterID.ToString() + "' target='_blank'>PV # " + JournalMasterID.ToString() + "</a>";
                //showAlartMessage("Successfully Done.");
            }
            catch (Exception ex)
            {
                lblJournalMasterID.Text = "<span style='color:red;'>Error !!</span>";
                //showAlartMessage("Error occured");
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

    protected void ddlBalance_SelectedIndexChanged(object sender, EventArgs e)
    {
//        lblGL.Text = @"<a class='buttonCss' href='GeneralLedgerPrint.aspx?ACC_ChartOfAccountLabel4ID=" + ddlBalance.SelectedValue.Split('@')[1] + @"&ACC_ChartOfAccountLabel3ID=" + ddlBalance.SelectedValue.Split('@')[0] + @"&WorkStationID=0&FromDate=2013-01-01 12:00:00 AM&ToDate=" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + @"' target='_blank' 
//            >Print GL</a>";
    }

    private void loadBalanceDDL()
    {
        
        string sql = @"select SUM(Debit)- SUM(Credit) as Balance
                        ,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
                        ,ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
                        ,ACC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel3ID
                        ,ACC_ChartOfAccountLabel3.ChartOfAccountLabel3Text
                        from ACC_JournalDetail 
                        inner join ACC_ChartOfAccountLabel4 on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
                        =ACC_JournalDetail.ACC_ChartOfAccountLabel4ID
                        inner join ACC_ChartOfAccountLabel3 on ACC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel3ID
                        = ACC_JournalDetail.ACC_ChartOfAccountLabel3ID
                        inner join ACC_JournalMaster 
                        on ACC_JournalDetail.JournalMasterID =ACC_JournalMaster.ACC_JournalMasterID
                        where ACC_JournalDetail.ACC_ChartOfAccountLabel3ID in (1,3,2)
                        and ACC_JournalMaster.RowStatusID=1
                        group by 
                        ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
                        ,ACC_ChartOfAccountLabel3.ChartOfAccountLabel3Text
                        ,ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
                        ,ACC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel3ID
                        order by ACC_ChartOfAccountLabel3ID,ChartOfAccountLabel4Text";

        DataSet ds = CommonManager.SQLExec(sql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            ddlBalance.Items.Add(new ListItem(
                dr["ChartOfAccountLabel3Text"].ToString() + "-->" + dr["ChartOfAccountLabel4Text"].ToString() + "------->(" + dr["Balance"].ToString() + ")"
                , dr["ACC_ChartOfAccountLabel3ID"].ToString() + "@" + dr["ACC_ChartOfAccountLabel4ID"].ToString()
                ));
        }

    }
}