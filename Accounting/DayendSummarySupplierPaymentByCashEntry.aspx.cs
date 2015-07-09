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
            loadACC_ChartOfAccountLabel4();
            txtJournalDate.Text = DateTime.Today.ToString("dd MMM yyyy");
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

    

    private void loadACC_ChartOfAccountLabel4()
    {
        #region Bank balance
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
                        where ACC_JournalDetail.ACC_ChartOfAccountLabel3ID in (1,3)
                        and ACC_JournalMaster.RowStatusID=1
                        group by 
                        ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
                        ,ACC_ChartOfAccountLabel3.ChartOfAccountLabel3Text
                        ,ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
                        ,ACC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel3ID
                        order by ACC_ChartOfAccountLabel3ID,ChartOfAccountLabel4Text";

        DataSet ds = CommonManager.SQLExec(sql);
        
        #endregion


        ddlAllACC_ChartOfAccountLabel4.Items.Add(new ListItem("Select", "0"));
        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4sSupplier = new List<ACC_ChartOfAccountLabel4>();
        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4sBanks = new List<ACC_ChartOfAccountLabel4>();
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
            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 6)
            {
                aCC_ChartOfAccountLabel4sSupplier.Add(aCC_ChartOfAccountLabel4);
            }

            if (aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID == 1)
            {
                aCC_ChartOfAccountLabel4sBanks.Add(aCC_ChartOfAccountLabel4);
            }
        }


        gvACC_ChartOfAccountLabel4.DataSource = aCC_ChartOfAccountLabel4sBanks;
        gvACC_ChartOfAccountLabel4.DataBind();

        
        foreach (GridViewRow gvr in gvACC_ChartOfAccountLabel4.Rows)
        {
            DropDownList ddlBanks = (DropDownList)gvr.FindControl("ddlBanks");
            DropDownList ddlSupplier = (DropDownList)gvr.FindControl("ddlSupplier");
            Label lblChartOfAccountLabel4Text = (Label)gvr.FindControl("lblChartOfAccountLabel4Text");
            HiddenField hfChartOfAccountLabel4ID=(HiddenField)gvr.FindControl("hfChartOfAccountLabel4ID");
            ddlBanks.Items.Add(new ListItem("select bank acc","0"));
            TextBox txtOpiningBalance = (TextBox)gvr.FindControl("txtOpiningBalance");
            TextBox txtCheckDate = (TextBox)gvr.FindControl("txtCheckDate");
            txtCheckDate.Text = DateTime.Today.ToString("dd MMM yyyy");

            //lblChartOfAccountLabel4Text.Text = lblChartOfAccountLabel4Text.Text.Split('>')[1];
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["ACC_ChartOfAccountLabel4ID"].ToString() == hfChartOfAccountLabel4ID.Value)
                {
                    txtOpiningBalance.Text = dr["Balance"].ToString();
                    break;
                }
            }

            foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4sBanks)
            {
                ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlBanks.Items.Add(item);
            }

            foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4sSupplier)
            {
                ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlSupplier.Items.Add(item);
            }
        }

    }

    protected void btnJournalSubmit_Click(object sender, EventArgs e)
    {
        List<ACC_HeadType> allACC_HeadType= ACC_HeadTypeManager.GetAllACC_HeadTypes();

        ACC_JournalMaster aCC_JournalMaster = new ACC_JournalMaster();

        aCC_JournalMaster.JournalMasterName = "2";//Contra
        aCC_JournalMaster.ExtraField1 = "";
        aCC_JournalMaster.ExtraField2 = "";
        aCC_JournalMaster.ExtraField3 = "";
        aCC_JournalMaster.Note = "";
        aCC_JournalMaster.JournalDate = DateTime.Parse(txtJournalDate.Text);
        aCC_JournalMaster.AddedBy = getLogin().LoginID;
        aCC_JournalMaster.AddedDate = DateTime.Now;
        aCC_JournalMaster.UpdatedBy = getLogin().LoginID;
        aCC_JournalMaster.UpdatedDate = DateTime.Now;
        aCC_JournalMaster.RowStatusID = 1;
        int JournalMasterID = 0;
        JournalMasterID = ACC_JournalMasterManager.InsertACC_JournalMaster(aCC_JournalMaster);

        foreach (GridViewRow gvr in gvACC_ChartOfAccountLabel4.Rows)
        {            
            HiddenField hfChartOfAccountLabel4ID = (HiddenField)gvr.FindControl("hfChartOfAccountLabel4ID");

            DropDownList ddlSupplier = (DropDownList)gvr.FindControl("ddlSupplier");
            Label lblChartOfAccountLabel4Text = (Label)gvr.FindControl("lblChartOfAccountLabel4Text");
            TextBox txtOpiningBalance = (TextBox)gvr.FindControl("txtOpiningBalance");
            TextBox txtSypplyerPayment = (TextBox)gvr.FindControl("txtSypplyerPayment");
            TextBox txtDiscountIncome = (TextBox)gvr.FindControl("txtDiscountIncome");

            try
            {
                ACC_JournalDetail aCC_JournalDetail = new ACC_JournalDetail();
                aCC_JournalDetail.JournalMasterID = JournalMasterID;
                aCC_JournalDetail.ExtraField3 = "";
                aCC_JournalDetail.ExtraField2 = "";
                aCC_JournalDetail.ExtraField1 = "";
                aCC_JournalDetail.AddedBy = getLogin().LoginID;
                aCC_JournalDetail.AddedDate = DateTime.Now;
                aCC_JournalDetail.UpdatedBy = getLogin().LoginID;
                aCC_JournalDetail.UpdatedDate = DateTime.Now;
                aCC_JournalDetail.RowStatusID = 1;

                aCC_JournalDetail.WorkStation = 1;
                aCC_JournalDetail.WorkStationName = "Heand Office";


                //Supplier Payment for Cash in hand
                aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = Int32.Parse(hfChartOfAccountLabel4ID.Value);
                aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = lblChartOfAccountLabel4Text.Text;

                aCC_JournalDetail.ACC_ChartOfAccountLabel3Text = "Cahs in Hand";
                aCC_JournalDetail.ACC_ChartOfAccountLabel3ID = 1;


                aCC_JournalDetail.Credit = Decimal.Parse(txtSypplyerPayment.Text);
                aCC_JournalDetail.Debit = Decimal.Parse("0");
                if (txtSypplyerPayment.Text != "0" && ddlSupplier.SelectedValue!="0")
                    ACC_JournalDetailManager.InsertACC_JournalDetail(aCC_JournalDetail);

                //Supplier Payment for Suppliyer
                if (txtSypplyerPayment.Text != "0" && ddlSupplier.SelectedValue != "0")
                    aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = Int32.Parse(ddlSupplier.SelectedValue);
                aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = ddlSupplier.SelectedItem.Text;

                aCC_JournalDetail.ACC_ChartOfAccountLabel3Text = "Suppliyer payable";
                aCC_JournalDetail.ACC_ChartOfAccountLabel3ID = 43;


                aCC_JournalDetail.Debit = Decimal.Parse(txtSypplyerPayment.Text);
                aCC_JournalDetail.Credit = Decimal.Parse("0");
                if (txtSypplyerPayment.Text != "0" && ddlSupplier.SelectedValue != "0")
                    ACC_JournalDetailManager.InsertACC_JournalDetail(aCC_JournalDetail);

                //Cash discount supplier payable
                aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = Int32.Parse(ddlSupplier.SelectedValue);
                aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = ddlSupplier.SelectedItem.Text;

                aCC_JournalDetail.ACC_ChartOfAccountLabel3Text = "Suppliyer payable";
                aCC_JournalDetail.ACC_ChartOfAccountLabel3ID = 43;


                aCC_JournalDetail.Debit = Decimal.Parse(txtDiscountIncome.Text);
                aCC_JournalDetail.Credit = Decimal.Parse("0");
                if (txtDiscountIncome.Text != "0" && txtDiscountIncome.Text != "" && ddlSupplier.SelectedValue != "0")
                    ACC_JournalDetailManager.InsertACC_JournalDetail(aCC_JournalDetail);

                //Cash discount for Income
                aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = Int32.Parse(ddlSupplier.SelectedValue);
                aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = ddlSupplier.SelectedItem.Text;

                aCC_JournalDetail.ACC_ChartOfAccountLabel3Text = "Cash Discount from Supplier";
                aCC_JournalDetail.ACC_ChartOfAccountLabel3ID = 211;


                aCC_JournalDetail.Credit = Decimal.Parse(txtDiscountIncome.Text);
                aCC_JournalDetail.Debit = Decimal.Parse("0");
                if (txtDiscountIncome.Text != "0" && txtDiscountIncome.Text != "" && ddlSupplier.SelectedValue != "0")
                    ACC_JournalDetailManager.InsertACC_JournalDetail(aCC_JournalDetail);

            }
            catch (Exception ex)
            {
            }
        }

        hlnkPrintVoucher.Visible = true;
        hlnkPrintVoucher.NavigateUrl = "Voucherprint.aspx?JournalMasterID=" + JournalMasterID; 
    }
    private void showAlartMessage(string message)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
             "err_msg",
             "alert('" + message + "');",
             true);
    }

}