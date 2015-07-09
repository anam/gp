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
        ddlAllACC_ChartOfAccountLabel4.Items.Add(new ListItem("Select", "0"));
        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4sBranches = new List<ACC_ChartOfAccountLabel4>();
        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4sBanks = new List<ACC_ChartOfAccountLabel4>();
        //aCC_ChartOfAccountLabel4s = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4sForJournalEntry();

        string sql = @"
SELECT ACC_ChartOfAccountLabel4.[ACC_ChartOfAccountLabel4ID]
      ,ACC_ChartOfAccountLabel4.[Code]
      ,ACC_ChartOfAccountLabel4.[ACC_HeadTypeID]
      ,ACC_HeadType.HeadTypeName +' -> '+ACC_ChartOfAccountLabel4.[ChartOfAccountLabel4Text] as ChartOfAccountLabel4Text
      --,ACC_HeadType.HeadTypeName +' -> '+ACC_ChartOfAccountLabel4.[ChartOfAccountLabel4Text] as ChartOfAccountLabel4Text
      ,ACC_ChartOfAccountLabel4.[ExtraField1]
      ,ACC_ChartOfAccountLabel4.[ExtraField2]
      ,ACC_ChartOfAccountLabel4.[ExtraField3]
      ,ACC_ChartOfAccountLabel4.[AddedBy]
      ,ACC_ChartOfAccountLabel4.[AddedDate]
      ,ACC_ChartOfAccountLabel4.[UpdatedBy]
      ,ACC_ChartOfAccountLabel4.[UpdatedDate]
      ,ACC_ChartOfAccountLabel4.[RowStatusID] 
        ,ACC_ChartOfAccountLabel4Visibility.IsVisible
FROM ACC_ChartOfAccountLabel4
    inner join ACC_HeadType on ACC_HeadType.ACC_HeadTypeID=ACC_ChartOfAccountLabel4.ACC_HeadTypeID
left outer join ACC_ChartOfAccountLabel4Visibility on ACC_ChartOfAccountLabel4Visibility.ACC_ChartOfAccountLabel4ID
=ACC_ChartOfAccountLabel4.[ACC_ChartOfAccountLabel4ID]
where ACC_ChartOfAccountLabel4.[RowStatusID] =1	
order by ACC_HeadType.ACC_HeadTypeID,ACC_ChartOfAccountLabel4.[ChartOfAccountLabel4Text]
";


        DataSet ds = CommonManager.SQLExec(sql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            try
            {
                dr["IsVisible"] = bool.Parse(dr["IsVisible"].ToString());
            }
            catch (Exception ex)
            {
                dr["IsVisible"] = false;
            }
            if (!bool.Parse(dr["IsVisible"].ToString()))
            {
                ACC_ChartOfAccountLabel4 l4 = new ACC_ChartOfAccountLabel4();
                l4.ACC_ChartOfAccountLabel4ID = int.Parse(dr["ACC_ChartOfAccountLabel4ID"].ToString());
                l4.ACC_HeadTypeID = int.Parse(dr["ACC_HeadTypeID"].ToString());
                l4.ChartOfAccountLabel4Text = dr["ChartOfAccountLabel4Text"].ToString();
                aCC_ChartOfAccountLabel4s.Add(l4); 
            }
        }

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
            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 1 && aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.Contains("Show"))
            {
                aCC_ChartOfAccountLabel4sBranches.Add(aCC_ChartOfAccountLabel4);
            }

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 5)
            {
                aCC_ChartOfAccountLabel4sBanks.Add(aCC_ChartOfAccountLabel4);
            }

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 1 ||
                aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 4 ||
                aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 5 ||
                aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 6 ||
                aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 8
                )
            {
                //foreach (ACC_HeadType aCC_HeadType in allACC_HeadType)
                //{
                //    if (aCC_HeadType.ACC_HeadTypeID == aCC_ChartOfAccountLabel4.ACC_HeadTypeID)
                //    {
                //        aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text = aCC_HeadType.HeadTypeName+" -> "+ aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text; 
                //    }
                //}

                ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlAllACC_ChartOfAccountLabel4.Items.Add(item);

                
            }

            
        }


        gvACC_ChartOfAccountLabel4.DataSource = aCC_ChartOfAccountLabel4sBranches;
        gvACC_ChartOfAccountLabel4.DataBind();

        
        foreach (GridViewRow gvr in gvACC_ChartOfAccountLabel4.Rows)
        {
            DropDownList ddlBanks = (DropDownList)gvr.FindControl("ddlBanks");
            ddlBanks.Items.Add(new ListItem("select bank acc","0"));

            foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4sBanks)
            {
                ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString().Split('>')[1], aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlBanks.Items.Add(item);
            }
        }

    }

    protected void btnJournalSubmit_Click(object sender, EventArgs e)
    {
        List<ACC_HeadType> allACC_HeadType= ACC_HeadTypeManager.GetAllACC_HeadTypes();

        ACC_JournalMaster aCC_JournalMaster = new ACC_JournalMaster();

        aCC_JournalMaster.JournalMasterName = "1";//Voucher Type
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

            DropDownList ddlBank = (DropDownList)gvr.FindControl("ddlBanks");
                
            Label lblChartOfAccountLabel4Text = (Label)gvr.FindControl("lblChartOfAccountLabel4Text");

            TextBox txtCashSales = (TextBox)gvr.FindControl("txtCashSales");
            TextBox txtCardSaleDBBL = (TextBox)gvr.FindControl("txtCardSaleDBBL");
            TextBox txtCardSaleCITY = (TextBox)gvr.FindControl("txtCardSaleCITY");
            TextBox txtbKash = (TextBox)gvr.FindControl("txtbKash");
            TextBox txtBankDepostiedAmount = (TextBox)gvr.FindControl("txtBankDepostiedAmount");
            TextBox txtDiscountAmount = (TextBox)gvr.FindControl("txtDiscountAmount");

            if (txtCashSales.Text == "0" && txtCardSaleDBBL.Text == "0" && txtCardSaleCITY.Text == "0")
            {
                continue;
            }

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

                aCC_JournalDetail.WorkStation = int.Parse(hfChartOfAccountLabel4ID.Value);
                aCC_JournalDetail.WorkStationName = lblChartOfAccountLabel4Text.Text;

                //Discount
                if (txtDiscountAmount.Text != "0")
                {
                    //discount Expence
                    aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = Int32.Parse(hfChartOfAccountLabel4ID.Value);
                    aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = lblChartOfAccountLabel4Text.Text;

                    aCC_JournalDetail.ACC_ChartOfAccountLabel3Text = "Discount";
                    aCC_JournalDetail.ACC_ChartOfAccountLabel3ID = 181;

                    aCC_JournalDetail.Debit = Decimal.Parse(txtDiscountAmount.Text);

                    aCC_JournalDetail.Credit = Decimal.Parse("0");
                    ACC_JournalDetailManager.InsertACC_JournalDetail(aCC_JournalDetail);

                    //Sales revinew discount
                    aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = 829;
                    aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = "All products";

                    aCC_JournalDetail.ACC_ChartOfAccountLabel3Text = "Sales revinew discount";
                    aCC_JournalDetail.ACC_ChartOfAccountLabel3ID = 180;

                    aCC_JournalDetail.Credit = Decimal.Parse(txtDiscountAmount.Text);

                    aCC_JournalDetail.Debit = Decimal.Parse("0");
                    ACC_JournalDetailManager.InsertACC_JournalDetail(aCC_JournalDetail);
                }

                ////Money entry for cash sale
                //aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = Int32.Parse(hfChartOfAccountLabel4ID.Value);
                //aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = lblChartOfAccountLabel4Text.Text;

                //aCC_JournalDetail.ACC_ChartOfAccountLabel3Text = "Cash in Hand";
                //aCC_JournalDetail.ACC_ChartOfAccountLabel3ID = 1;

                //aCC_JournalDetail.WorkStation = aCC_JournalDetail.ACC_ChartOfAccountLabel4ID;
                //aCC_JournalDetail.WorkStationName = aCC_JournalDetail.ACC_ChartOfAccountLabel4Text;

                //aCC_JournalDetail.Debit = Decimal.Parse(txtCashSales.Text);

                //aCC_JournalDetail.Credit = Decimal.Parse("0");
                //if (txtCashSales.Text !="0")
                //ACC_JournalDetailManager.InsertACC_JournalDetail(aCC_JournalDetail);
               
                
                //Money entry for bKash Sale
                //<option value="1803"> Brac Bank (New Account) A/C (1524-2029-8148-6001)</option>
                aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = Int32.Parse(hfChartOfAccountLabel4ID.Value);
                aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = lblChartOfAccountLabel4Text.Text;

                aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = 1803;
                aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = "Brac Bank  A/C (1524-2029-8148-6001)";

                aCC_JournalDetail.ACC_ChartOfAccountLabel3Text = "Cash at Bank";
                aCC_JournalDetail.ACC_ChartOfAccountLabel3ID = 3;


                aCC_JournalDetail.Debit = Decimal.Parse(txtbKash.Text);
                aCC_JournalDetail.Credit = Decimal.Parse("0");
                if (txtbKash.Text != "0")
                    ACC_JournalDetailManager.InsertACC_JournalDetail(aCC_JournalDetail);


                //Money entry for Crard sale CITY
                aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = Int32.Parse(hfChartOfAccountLabel4ID.Value);
                aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = lblChartOfAccountLabel4Text.Text;

                aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = 823;
                aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = "CITY BANK - GentlePark (1401307973001)";

                aCC_JournalDetail.ACC_ChartOfAccountLabel3Text = "Cash at Bank";
                aCC_JournalDetail.ACC_ChartOfAccountLabel3ID = 3;

                
                aCC_JournalDetail.Debit = Decimal.Parse(txtCardSaleCITY.Text);
                aCC_JournalDetail.Credit = Decimal.Parse("0");
                if (txtCardSaleCITY.Text != "0")
                ACC_JournalDetailManager.InsertACC_JournalDetail(aCC_JournalDetail);

                

                //Money entry for Crard sale DBBL
                aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = 828;
                aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = "DBBL-126-110-11033";

                aCC_JournalDetail.ACC_ChartOfAccountLabel3Text = "Cash at Bank";
                aCC_JournalDetail.ACC_ChartOfAccountLabel3ID = 3;

                aCC_JournalDetail.Debit = Decimal.Parse(txtCardSaleDBBL.Text);
                aCC_JournalDetail.Credit = Decimal.Parse("0");
                if (txtCardSaleDBBL.Text != "0")
                ACC_JournalDetailManager.InsertACC_JournalDetail(aCC_JournalDetail);

                //bKash sales 
                //<option value="315">bKash Sales ( (Operating income)-Sales Revenue -> Sales Revenue)</option>
                
                aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = 829;
                aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = "All products";

                aCC_JournalDetail.ACC_ChartOfAccountLabel3Text = "bKash sales";
                aCC_JournalDetail.ACC_ChartOfAccountLabel3ID = 315;

                aCC_JournalDetail.Credit = Decimal.Parse(txtbKash.Text);
                aCC_JournalDetail.Debit = Decimal.Parse("0");
                if (txtbKash.Text != "0")
                    ACC_JournalDetailManager.InsertACC_JournalDetail(aCC_JournalDetail);


                //Cash Sales DBBL
                aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = 829;
                aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = "All products";

                aCC_JournalDetail.ACC_ChartOfAccountLabel3Text = "Card Sales (DBBL)";
                aCC_JournalDetail.ACC_ChartOfAccountLabel3ID = 107;

                aCC_JournalDetail.Credit = Decimal.Parse(txtCardSaleDBBL.Text);
                aCC_JournalDetail.Debit = Decimal.Parse("0");
                if (txtCardSaleDBBL.Text != "0") 
                    ACC_JournalDetailManager.InsertACC_JournalDetail(aCC_JournalDetail);

                //Cash Sales CITY
                aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = 829;
                aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = "All products";

                aCC_JournalDetail.ACC_ChartOfAccountLabel3Text = "Card Sales (CITY)";
                aCC_JournalDetail.ACC_ChartOfAccountLabel3ID = 143;

                aCC_JournalDetail.Credit = Decimal.Parse(txtCardSaleCITY.Text);
                aCC_JournalDetail.Debit = Decimal.Parse("0");
                if (txtCardSaleCITY.Text != "0")
                    ACC_JournalDetailManager.InsertACC_JournalDetail(aCC_JournalDetail);

                if (ddlBank.SelectedValue != "0" && txtCashSales.Text != "0")
                    //if (ddlBank.SelectedValue != "0" && txtBankDepostiedAmount.Text != "0")
                {

                    //Cash Sales
                    aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = 829;
                    aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = "All products";

                    aCC_JournalDetail.ACC_ChartOfAccountLabel3Text = "Cash Sale";
                    aCC_JournalDetail.ACC_ChartOfAccountLabel3ID = 106;

                    aCC_JournalDetail.Credit = Decimal.Parse(txtCashSales.Text);
                    aCC_JournalDetail.Debit = Decimal.Parse("0");
                    ACC_JournalDetailManager.InsertACC_JournalDetail(aCC_JournalDetail);

                    ////Bank Deposit
                    //aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = Int32.Parse(hfChartOfAccountLabel4ID.Value);
                    //aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = lblChartOfAccountLabel4Text.Text;

                    //aCC_JournalDetail.ACC_ChartOfAccountLabel3Text = "Cash in Hand";
                    //aCC_JournalDetail.ACC_ChartOfAccountLabel3ID = 1;

                    //aCC_JournalDetail.WorkStation = aCC_JournalDetail.ACC_ChartOfAccountLabel4ID;
                    //aCC_JournalDetail.WorkStationName = aCC_JournalDetail.ACC_ChartOfAccountLabel4Text;

                    ////aCC_JournalDetail.Credit = Decimal.Parse(txtBankDepostiedAmount.Text);
                    //aCC_JournalDetail.Credit = Decimal.Parse(txtCashSales.Text);
                    //aCC_JournalDetail.Debit = Decimal.Parse("0");
                    //ACC_JournalDetailManager.InsertACC_JournalDetail(aCC_JournalDetail);

                    aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = Int32.Parse(ddlBank.SelectedValue);
                    aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = ddlBank.SelectedItem.Text;

                    aCC_JournalDetail.ACC_ChartOfAccountLabel3Text = "Cash at Bank";
                    aCC_JournalDetail.ACC_ChartOfAccountLabel3ID = 3;

                    //aCC_JournalDetail.Debit = Decimal.Parse(txtBankDepostiedAmount.Text);
                    aCC_JournalDetail.Debit = Decimal.Parse(txtCashSales.Text);
                    aCC_JournalDetail.Credit = Decimal.Parse("0");
                    ACC_JournalDetailManager.InsertACC_JournalDetail(aCC_JournalDetail);
                }
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