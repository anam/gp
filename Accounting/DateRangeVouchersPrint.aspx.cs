using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Accounting_Voucher : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadData();
        }
    }

    private void loadData()
    {
        try
        {
            string html = "";
            foreach (string JournalMasterID in Request.QueryString["JournalMasterIDs"].Split(','))
            {
                html += "<iframe height='800px' frameborder='0' width='900px' src='Voucherprint.aspx?JournalMasterID=" + JournalMasterID + "' allowfullscreen=''></iframe><hr/>";
            }

            lblDateRangeVoucherPrint.Text = html;
        }
        catch (Exception ex)
        { 
        
        }

    }

    
}