using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class AdminRowStatusInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnUpdate.Visible = false;
            btnAdd.Visible = true;
            if (Request.QueryString["rowStatusID"] != null)
            {
                int rowStatusID = Int32.Parse(Request.QueryString["rowStatusID"]);
                if (rowStatusID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showRowStatusData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        RowStatus rowStatus = new RowStatus();

        rowStatus.RowStatusName = txtRowStatusName.Text;
        int resutl = RowStatusManager.InsertRowStatus(rowStatus);
        Response.Redirect("AdminRowStatusDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        RowStatus rowStatus = new RowStatus();
        rowStatus = RowStatusManager.GetRowStatusByID(Int32.Parse(Request.QueryString["rowStatusID"]));
        RowStatus tempRowStatus = new RowStatus();
        tempRowStatus.RowStatusID = rowStatus.RowStatusID;

        tempRowStatus.RowStatusName = txtRowStatusName.Text;
        bool result = RowStatusManager.UpdateRowStatus(tempRowStatus);
        Response.Redirect("AdminRowStatusDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtRowStatusName.Text = "";
    }
    private void showRowStatusData()
    {
        RowStatus rowStatus = new RowStatus();
        rowStatus = RowStatusManager.GetRowStatusByID(Int32.Parse(Request.QueryString["rowStatusID"]));

        txtRowStatusName.Text = rowStatus.RowStatusName;
    }
}
