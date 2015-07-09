using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ControlPanel_HeadTransfer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadL();
        }
    }

    private void loadL()
    {
        string sql = @"
SELECT     ACC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel3ID as ID, ACC_ChartOfAccountLabel3.ChartOfAccountLabel3Text as L3, 
                      ACC_ChartOfAccountLabel2.ChartOfAccountLabel2Text as L2, ACC_ChartOfAccountLabel1.ChartOfAccountLabel1Text as L1
FROM         ACC_ChartOfAccountLabel1 INNER JOIN
                      ACC_ChartOfAccountLabel2 ON 
                      ACC_ChartOfAccountLabel1.ACC_ChartOfAccountLabel1ID = ACC_ChartOfAccountLabel2.ACC_ChartOfAccountLabel1ID INNER JOIN
                      ACC_ChartOfAccountLabel3 ON ACC_ChartOfAccountLabel2.ACC_ChartOfAccountLabel2ID = ACC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel2ID
where ACC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel3ID in (select Distinct ACC_ChartOfAccountLabel3ID from ACC_JournalDetail)
order by ACC_ChartOfAccountLabel1.ChartOfAccountLabel1Text,ACC_ChartOfAccountLabel2.ChartOfAccountLabel2Text,ACC_ChartOfAccountLabel3.ChartOfAccountLabel3Text
";

        sql += @"
SELECT     ACC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel3ID as ID, ACC_ChartOfAccountLabel3.ChartOfAccountLabel3Text as L3, 
                      ACC_ChartOfAccountLabel2.ChartOfAccountLabel2Text as L2, ACC_ChartOfAccountLabel1.ChartOfAccountLabel1Text as L1
FROM         ACC_ChartOfAccountLabel1 INNER JOIN
                      ACC_ChartOfAccountLabel2 ON 
                      ACC_ChartOfAccountLabel1.ACC_ChartOfAccountLabel1ID = ACC_ChartOfAccountLabel2.ACC_ChartOfAccountLabel1ID INNER JOIN
                      ACC_ChartOfAccountLabel3 ON ACC_ChartOfAccountLabel2.ACC_ChartOfAccountLabel2ID = ACC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel2ID
where ACC_ChartOfAccountLabel3.RowStatusID=1
order by ACC_ChartOfAccountLabel1.ChartOfAccountLabel1Text,ACC_ChartOfAccountLabel2.ChartOfAccountLabel2Text,ACC_ChartOfAccountLabel3.ChartOfAccountLabel3Text
";
        sql += @"
SELECT     ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID as ID, ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text as L4, ACC_HeadType.HeadTypeName as [Type]
FROM         ACC_ChartOfAccountLabel4 INNER JOIN
                      ACC_HeadType ON ACC_ChartOfAccountLabel4.ACC_HeadTypeID = ACC_HeadType.ACC_HeadTypeID
where ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID in (select Distinct ACC_ChartOfAccountLabel4ID from ACC_JournalDetail)
order by ACC_HeadType.HeadTypeName,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
";

        sql += @"
SELECT     ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID as ID, ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text as L4, ACC_HeadType.HeadTypeName as [Type]
FROM         ACC_ChartOfAccountLabel4 INNER JOIN
                      ACC_HeadType ON ACC_ChartOfAccountLabel4.ACC_HeadTypeID = ACC_HeadType.ACC_HeadTypeID
where ACC_ChartOfAccountLabel4.RowStatusID=1
order by ACC_HeadType.HeadTypeName,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
";

        DataSet ds = CommonManager.SQLExec(sql);

        chkL3.Items.Clear();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            chkL3.Items.Add(new ListItem(
                dr["L3"].ToString()+" --- " +
                dr["L2"].ToString()+" --- " +
                dr["L1"].ToString()
                , dr["ID"].ToString()
                ));
        }

        ddlL3.Items.Clear();
        foreach (DataRow dr in ds.Tables[1].Rows)
        {
            ddlL3.Items.Add(new ListItem(
                dr["L3"].ToString() + " --- " +
                dr["L2"].ToString() + " --- " +
                dr["L1"].ToString()
                , dr["ID"].ToString()
                ));
        }


        chkL4.Items.Clear();
        foreach (DataRow dr in ds.Tables[2].Rows)
        {
            chkL4.Items.Add(new ListItem(
                dr["L4"].ToString() + " --- " +
                dr["Type"].ToString()
                , dr["ID"].ToString()
                ));
        }

        ddlL4.Items.Clear();
        foreach (DataRow dr in ds.Tables[3].Rows)
        {
            ddlL4.Items.Add(new ListItem(
                dr["L4"].ToString() + " --- " +
                dr["Type"].ToString()
                , dr["ID"].ToString()
                ));
        }

    }
    protected void btnL4Transfer_Click(object sender, EventArgs e)
    {
        string L4 = "";
        foreach (ListItem item in chkL4.Items)
        {
            if (item.Selected)
            {
                L4 += (L4 == "" ? "" : ",") + item.Value;
            }
        }

        string sql = @"update ACC_JournalDetail set ACC_ChartOfAccountLabel4ID="+ddlL4.SelectedValue+" where ACC_ChartOfAccountLabel4ID in ("+L4+@");";
        sql += @"update ACC_ChartOfAccountLabel4 set RowStatusID=2 where ACC_ChartOfAccountLabel4ID<>" + ddlL4.SelectedValue + " and  ACC_ChartOfAccountLabel4ID in (" + L4 + @")";

        if (L4 != "")
        {
            CommonManager.SQLExec(sql);
        }

        loadL();
    }
    protected void btnL3Transfer_Click(object sender, EventArgs e)
    {
        string L3 = "";
        foreach (ListItem item in chkL3.Items)
        {
            if (item.Selected)
            {
                L3 += (L3 == "" ? "" : ",") + item.Value;
            }
        }

        string sql = @"update ACC_JournalDetail set ACC_ChartOfAccountLabel3ID=" + ddlL3.SelectedValue + " where ACC_ChartOfAccountLabel3ID in (" + L3 + @")";
        sql += @"update ACC_ChartOfAccountLabel3 set RowStatusID=2 where ACC_ChartOfAccountLabel3ID<> " + ddlL3.SelectedValue + " and  ACC_ChartOfAccountLabel3ID in (" + L3 + @")";

        if (L3 != "")
        {
            CommonManager.SQLExec(sql);
        }
        loadL();
    }
}