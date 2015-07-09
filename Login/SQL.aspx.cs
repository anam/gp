using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login_SQL : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnMSQL_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            gvResult.DataSource = MSSQL.SQLExec(txtSQL.Text+";Select 'Excecuted Successfully' as Message;").Tables[0];
            gvResult.DataBind();
            
        }
        catch (Exception ex)
        {
            gvResult.DataSource = null;
            gvResult.DataBind();

            lblMsg.Text = ex.Message;
        }
    }

    protected void btnMSQLSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            gvResult.DataSource = MSSQL.SQLExec(txtSQL.Text + ";Select 'Excecuted Successfully' as Message;").Tables[0];
            gvResult.DataBind();
            string sql = @"
                            INSERT INTO [Auto_SQL_New]
                                        ([SQLString]
                                        ,[Status]
                                        ,[WorkStationID]
                                        ,[Status1])
                                    VALUES
                                        ('" + txtSQL.Text.Replace("'", "''") + @"'
                                        ,1
                                        ,1
                                        ,1)
                            ";
            MSSQL.SQLExec(sql);
        }
        catch (Exception ex)
        {
            gvResult.DataSource = null;
            gvResult.DataBind();

            lblMsg.Text = ex.Message;
        }
    }

    protected void btnMyQL_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            gvResult.DataSource = MySQL.SQLExec(txtSQL.Text + ";Select 'Excecuted Successfully' as Message");
            gvResult.DataBind();

        }
        catch (Exception ex)
        {
            gvResult.DataSource = null;
            gvResult.DataBind();
            lblMsg.Text = ex.Message;
        }
    }
}