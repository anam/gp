using System;
using System.Collections;
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
using System.Collections.Generic;

public partial class AdminACC_ChartOfAccountLabel4Display : System.Web.UI.Page
{
    
    protected void lbSelect_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id;
        id = Convert.ToInt32(linkButton.CommandArgument);
        
        hfaCC_ChartOfAccountLabel4ID.Value = id.ToString();
        showACC_ChartOfAccountLabel4Data();
        btnAdd.Visible = false;
        btnUpdate.Visible = true;
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = ACC_ChartOfAccountLabel4Manager.DeleteACC_ChartOfAccountLabel4(Convert.ToInt32(linkButton.CommandArgument));
        showACC_ChartOfAccountLabel4Grid();
    }

    protected void loadL3()
    {
        ddlCOAL3.Items.Clear();
        ddlCOAL3.Items.Add(new ListItem("Select Label-3", "0"));
        

        List<ACC_ChartOfAccountLabel3> aCC_ChartOfAccountLabel3s = new List<ACC_ChartOfAccountLabel3>();
        aCC_ChartOfAccountLabel3s = ACC_ChartOfAccountLabel3Manager.GetAllACC_ChartOfAccountLabel3sForJournalEntryForDropDownList();
        foreach (ACC_ChartOfAccountLabel3 aCC_ChartOfAccountLabel3 in aCC_ChartOfAccountLabel3s)
        {
            if (aCC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel2ID == 35
                || (aCC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel2ID >= 13
                && aCC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel2ID <= 18)
                || aCC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel2ID == 53
                
                )
            {
                ListItem item = new ListItem(aCC_ChartOfAccountLabel3.ChartOfAccountLabel3Text.ToString(),aCC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel3ID.ToString());
                ddlCOAL3.Items.Add(item);
            }
        }

    }

    private void showACC_ChartOfAccountLabel4Grid()
    {
        string sql = @"
SELECT ACC_ChartOfAccountLabel4.[ACC_ChartOfAccountLabel4ID]
      ,ACC_ChartOfAccountLabel4.[Code]
      ,ACC_ChartOfAccountLabel4.[ACC_HeadTypeID]
      ,ACC_HeadType.HeadTypeName +' -> '+ACC_ChartOfAccountLabel4.[ChartOfAccountLabel4Text] as ChartOfAccountLabel4Text
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
	
";

        if (ddlStatusSearch.SelectedValue != "0")
        //{
        //    gvACC_ChartOfAccountLabel4.DataSource = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4s();
        //}
        //else
        {
            sql += @"
            where ACC_ChartOfAccountLabel4.[RowStatusID] ="+ddlRowStatus.SelectedValue+@"
            ";
            //gvACC_ChartOfAccountLabel4.DataSource = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4s().FindAll(x => (x.RowStatusID.ToString() == ddlStatusSearch.SelectedValue));
        }
        sql += @"
            order by ACC_HeadType.ACC_HeadTypeID,ACC_ChartOfAccountLabel4.[ChartOfAccountLabel4Text]
            ";
        DataSet ds = CommonManager.SQLExec(sql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            try
            {
                dr["IsVisible"] = bool.Parse( dr["IsVisible"].ToString());
            }
            catch (Exception ex)
            {
                dr["IsVisible"] = false;
            }
        }
        gvACC_ChartOfAccountLabel4.DataSource = ds.Tables[0];
        gvACC_ChartOfAccountLabel4.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadL3();
            loadACC_HeadType();
            loadRowStatus();

            initialLoad();
            
            showACC_ChartOfAccountLabel4Grid();
        }
    }

    private void initialLoad()
    {
        btnUpdate.Visible = false;
        btnAdd.Visible = true;

        btnLoadItemCode.Visible = ButtonManager.GetAllButtonsByPageURLnUserIDnButtonName("btnLoadItemCode", HttpContext.Current.Request.Url.AbsoluteUri, getLogin().LoginID.ToString());
        ddlCOAL3.Visible = ButtonManager.GetAllButtonsByPageURLnUserIDnButtonName("ddlCOAL3", HttpContext.Current.Request.Url.AbsoluteUri, getLogin().LoginID.ToString());
    }

    private Login getLogin()
    {
        Login login = new Login();
        try
        {
            if (Session["Login"] == null) { Session["PreviousPage"] = HttpContext.Current.Request.Url.AbsoluteUri; Response.Redirect("../LoginPage.aspx"); }

            login = (Login)Session["Login"];
        }
        catch (Exception ex)
        { }

        return login;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 = new ACC_ChartOfAccountLabel4();

        aCC_ChartOfAccountLabel4.Code = txtCode.Text;
        aCC_ChartOfAccountLabel4.ACC_HeadTypeID = Int32.Parse(ddlACC_HeadType.SelectedValue);
        aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text = txtChartOfAccountLabel4Text.Text;
        aCC_ChartOfAccountLabel4.ExtraField1 = txtExtraField1.Text;
        aCC_ChartOfAccountLabel4.ExtraField2 = txtExtraField2.Text;
        aCC_ChartOfAccountLabel4.ExtraField3 = ddlACC_HeadType.SelectedValue == "10" ? (ddlCOAL3.SelectedValue == "0" ? "203" : ddlCOAL3.SelectedValue) : txtExtraField3.Text;
        aCC_ChartOfAccountLabel4.AddedBy = getLogin().LoginID;
        aCC_ChartOfAccountLabel4.AddedDate = DateTime.Now;
        aCC_ChartOfAccountLabel4.UpdatedBy = getLogin().LoginID;
        aCC_ChartOfAccountLabel4.UpdatedDate = DateTime.Now;
        aCC_ChartOfAccountLabel4.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = ACC_ChartOfAccountLabel4Manager.InsertACC_ChartOfAccountLabel4(aCC_ChartOfAccountLabel4);
        showACC_ChartOfAccountLabel4Grid();
        btnClear_Click(this, new EventArgs());
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 = new ACC_ChartOfAccountLabel4();
        aCC_ChartOfAccountLabel4 = ACC_ChartOfAccountLabel4Manager.GetACC_ChartOfAccountLabel4ByID(Int32.Parse(hfaCC_ChartOfAccountLabel4ID.Value));
        ACC_ChartOfAccountLabel4 tempACC_ChartOfAccountLabel4 = new ACC_ChartOfAccountLabel4();
        tempACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID = aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID;

        tempACC_ChartOfAccountLabel4.Code = txtCode.Text;
        tempACC_ChartOfAccountLabel4.ACC_HeadTypeID = Int32.Parse(ddlACC_HeadType.SelectedValue);
        tempACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text = txtChartOfAccountLabel4Text.Text;
        tempACC_ChartOfAccountLabel4.ExtraField1 = txtExtraField1.Text;
        tempACC_ChartOfAccountLabel4.ExtraField2 = txtExtraField2.Text;
        tempACC_ChartOfAccountLabel4.ExtraField3 = ddlACC_HeadType.SelectedValue == "10" ? (ddlCOAL3.SelectedValue == "0" ? "203" : ddlCOAL3.SelectedValue) : txtExtraField3.Text;
        tempACC_ChartOfAccountLabel4.AddedBy = getLogin().LoginID;
        tempACC_ChartOfAccountLabel4.AddedDate = DateTime.Now;
        tempACC_ChartOfAccountLabel4.UpdatedBy = getLogin().LoginID;
        tempACC_ChartOfAccountLabel4.UpdatedDate = DateTime.Now;
        tempACC_ChartOfAccountLabel4.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = ACC_ChartOfAccountLabel4Manager.UpdateACC_ChartOfAccountLabel4(tempACC_ChartOfAccountLabel4);
        showACC_ChartOfAccountLabel4Grid();
        btnUpdate.Visible = false;
        btnAdd.Visible = true;
        btnClear_Click(this, new EventArgs());
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtCode.Text = "";
        //ddlACC_ChartOfAccountLabel2.SelectedIndex = 0;
        txtChartOfAccountLabel4Text.Text = "";
        txtExtraField1.Text = "";
        txtExtraField2.Text = "";
        txtExtraField3.Text = "";
        txtAddedBy.Text = "";
        txtUpdatedBy.Text = "";
        txtUpdatedDate.Text = "";
        //ddlRowStatus.SelectedIndex = 0;
    }
    private void showACC_ChartOfAccountLabel4Data()
    {
        ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 = new ACC_ChartOfAccountLabel4();
        aCC_ChartOfAccountLabel4 = ACC_ChartOfAccountLabel4Manager.GetACC_ChartOfAccountLabel4ByID(Int32.Parse(hfaCC_ChartOfAccountLabel4ID.Value));

        txtCode.Text = aCC_ChartOfAccountLabel4.Code;
        ddlACC_HeadType.SelectedValue = aCC_ChartOfAccountLabel4.ACC_HeadTypeID.ToString();
        txtChartOfAccountLabel4Text.Text = aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text;
        txtExtraField1.Text = aCC_ChartOfAccountLabel4.ExtraField1;
        txtExtraField2.Text = aCC_ChartOfAccountLabel4.ExtraField2;
        txtExtraField3.Text = aCC_ChartOfAccountLabel4.ExtraField3;
        txtAddedBy.Text = aCC_ChartOfAccountLabel4.AddedBy.ToString();
        txtUpdatedBy.Text = aCC_ChartOfAccountLabel4.UpdatedBy.ToString();
        //txtUpdatedDate.Text = aCC_ChartOfAccountLabel4.UpdatedDate;
        ddlRowStatus.SelectedValue = aCC_ChartOfAccountLabel4.RowStatusID.ToString();
        if (ddlACC_HeadType.SelectedValue == "10")
        {
            ddlCOAL3.Visible = true;

            try
            {
                ddlCOAL3.SelectedValue = aCC_ChartOfAccountLabel4.ExtraField3;
            }
            catch (Exception ex)
            { }

            txtExtraField3.Enabled = false;
        }
        else
        {
            txtExtraField3.Enabled = true;
            ddlCOAL3.Visible = false;
        }
    }
    private void loadACC_HeadType()
    {
        ListItem li = new ListItem("Select Type...", "0");
        ddlACC_HeadType.Items.Add(li);

        List<ACC_HeadType> aCC_HeadTypes = new List<ACC_HeadType>();
        aCC_HeadTypes = ACC_HeadTypeManager.GetAllACC_HeadTypes();
        foreach (ACC_HeadType aCC_HeadType in aCC_HeadTypes)
        {
            ListItem item = new ListItem(aCC_HeadType.HeadTypeName.ToString(), aCC_HeadType.ACC_HeadTypeID.ToString());
            ddlACC_HeadType.Items.Add(item);
        }
    }
    private void loadRowStatus()
    {
        ListItem li = new ListItem("Select Status...", "0");
        ddlRowStatus.Items.Add(li);
        ddlStatusSearch.Items.Add(new ListItem("All", "0"));

        List<RowStatus> rowStatuss = new List<RowStatus>();
        rowStatuss = RowStatusManager.GetAllRowStatuss();
        foreach (RowStatus rowStatus in rowStatuss)
        {
            ListItem item = new ListItem(rowStatus.RowStatusName.ToString(), rowStatus.RowStatusID.ToString());
            ddlRowStatus.Items.Add(item);
            ddlStatusSearch.Items.Add(item);
        }

        ddlStatusSearch.SelectedValue = "1";
    }

    protected void ddlStatusSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        showACC_ChartOfAccountLabel4Grid();
    }
    protected void btnLoadItemCode_Click(object sender, EventArgs e)
    {
        string itemCategory="";
        if (ddlACC_HeadType.SelectedValue == "3")//product
        {
            itemCategory = "01";
        }
        else
            if (ddlACC_HeadType.SelectedValue == "2")//Fabrics
            {
                itemCategory = "02";
            }
            else
            if (ddlACC_HeadType.SelectedValue == "9")//Productive Accessories
            {
                itemCategory = "05";
            }
            else
                if (ddlACC_HeadType.SelectedValue == "10")//Non-Productive Accessories
                {
                    itemCategory = "06";
                }

        if (itemCategory != "")
        {
            DataSet ds = CommonManager.SQLExec(@"select (MAX( CAST( Substring(ExtraField1,3,3) as int)) +1) as ItemCode
		                            from ACC_ChartOfAccountLabel4
		                            where ACC_HeadTypeID=" + ddlACC_HeadType.SelectedValue);
            try
            {
                txtExtraField1.Text = itemCategory + decimal.Parse(ds.Tables[0].Rows[0][0].ToString()).ToString("000");
            }
            catch (Exception ex)
            { txtExtraField1.Text = ""; }
        }
    }
    protected void btnVisibilitySave_Click(object sender, EventArgs e)
    {
        string sql = "Delete ACC_ChartOfAccountLabel4Visibility";
        foreach (GridViewRow gvr in gvACC_ChartOfAccountLabel4.Rows)
        {
            HiddenField hfChartOfAccountLabel4ID = (HiddenField)gvr.FindControl("hfChartOfAccountLabel4ID");
            CheckBox chkVisibility = (CheckBox)gvr.FindControl("chkVisibility");

            sql += @"
insert into ACC_ChartOfAccountLabel4Visibility values(" + hfChartOfAccountLabel4ID.Value + @"," + (chkVisibility.Checked ? "1" : "0") + @");
";
        }

        CommonManager.SQLExec(sql);
    }
}
