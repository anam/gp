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
using System.IO;

public partial class AdminPos_CustomerInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //loadReference();
            //loadRowSatatus();
            loadACC_ChartOfAccountLabel4();
            if (Request.QueryString["pos_CustomerID"] != null)
            {
                int pos_CustomerID = Int32.Parse(Request.QueryString["pos_CustomerID"]);
                if (pos_CustomerID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showPos_CustomerData();
                }
            }
            initialLoad();
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

    private void initialLoad()
    {
        if (hfLoginID.Value == "")
        {
            hfLoginID.Value = getLogin().LoginID.ToString();
        }

        txtDateofBirth.Text = DateTime.Today.ToString("dd MMM yyyy");
        txtApplicationDate.Text = DateTime.Today.ToString("dd MMM yyyy");
        txtCardIssueDate.Text = DateTime.Today.ToString("dd MMM yyyy");
        txtExpireDate.Text = DateTime.Today.ToString("dd MMM yyyy");
        //txtDiscountPersent.Text = DateTime.Today.ToString("dd MMM yyyy");
    }

    private void loadACC_ChartOfAccountLabel4()
    {
        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        aCC_ChartOfAccountLabel4s = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4sForJournalEntry();

        ddlReference.Items.Add(new ListItem("Select Referrence", "0"));
        ddlBranch.Items.Add(new ListItem("Select Branch", "0"));
        foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
        {
            ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_HeadTypeID.ToString() + "@" + aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 4 )
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlReference.Items.Add(item);
            }

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 1 && aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.Contains("Show"))
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlBranch.Items.Add(item);
            }

        }

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Pos_Customer pos_Customer = new Pos_Customer();

        pos_Customer.ExtraFiled1 = ddlBranch.SelectedValue;
        pos_Customer.CustomerName = txtCustomerName.Text;
        pos_Customer.Address = txtAddress.Text;
        pos_Customer.ExtraFiled2 = txtCompanyName.Text;
        pos_Customer.ExtraFiled3 = txtOfficeAddress.Text;
        pos_Customer.Phone = txtPhone.Text;
        pos_Customer.ExtraFiled4 = txtdesignation.Text;
        pos_Customer.ExtraFiled5 = txtOccupation.Text;
        pos_Customer.DateofBirth = Convert.ToDateTime(txtDateofBirth.Text);
        pos_Customer.Mobile = txtContactNo.Text;
        pos_Customer.ApplicationDate = Convert.ToDateTime(txtApplicationDate.Text);
        pos_Customer.CardNo = txtCardNo.Text;
        pos_Customer.CardIssueDate = Convert.ToDateTime(txtCardIssueDate.Text);
        pos_Customer.ExpireDate = Convert.ToDateTime(txtExpireDate.Text);
        pos_Customer.CardType = ddlCardType.SelectedValue;
        pos_Customer.DiscountPersent = Decimal.Parse(txtDiscountPersent.Text);
        //pos_Customer.Signature = txtSignature.Text;
        if (uplFile.PostedFile != null && uplFile.PostedFile.ContentLength > 0)
        {
            try
            {
                string dirUrl = "~/Upload";
                string dirPath = Server.MapPath(dirUrl);

                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                string fileName = Path.GetFileName(uplFile.PostedFile.FileName);
                string fileUrl = dirUrl + "/" + Path.GetFileName(uplFile.PostedFile.FileName);
                string filePath = Server.MapPath(fileUrl);
                uplFile.PostedFile.SaveAs(filePath);

                pos_Customer.Signature = dirUrl + "/" + fileName;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        else
        {
            pos_Customer.Signature = "~/Upload/NoImage.jpg";
        }
        pos_Customer.ApprovedBy = txtApprovedBy.Text;
        pos_Customer.Note = txtRemark.Text;
        pos_Customer.ReferenceID = Convert.ToInt32(ddlReference);
        pos_Customer.AddedBy = 1;
        pos_Customer.AddedDate = DateTime.Now;
        pos_Customer.UpdatedBy = 1;
        pos_Customer.UpdatedDate = DateTime.Now;
        pos_Customer.RowSatatusID = 1;
        int resutl = Pos_CustomerManager.InsertPos_Customer(pos_Customer);
        Response.Redirect("AdminPos_CustomerDisplay.aspx");

        /*
         INSERT INTO [GentleParkHO].[dbo].[Pos_Customer]
           ([CardNo]
           ,[Signature]
           ,[CustomerName]
           ,[ReferenceID]
           ,[Address]
           ,[Mobile]
           ,[Phone]
           ,[DiscountPersent]
           ,[Note]
           ,[ExtraFiled1]
           ,[ExtraFiled2]
           ,[ExtraFiled3]
           ,[ExtraFiled4]
           ,[ExtraFiled5]
           ,[AddedBy]
           ,[AddedDate]
           ,[UpdatedBy]
           ,[UpdatedDate]
           ,[RowSatatusID]
           ,[DateofBirth]
           ,[ApplicationDate]
           ,[CardIssueDate]
           ,[ExpireDate]
           ,[CardType]
           ,[ApprovedBy])
     Select CardNo
           ,''
           ,CustomerName
           ,0
           ,[Address]
           ,Mobile
           ,Phone
           ,DiscountPersent
           ,''--<Note, varchar(50),>
           ,''--<ExtraFiled1, nvarchar(256),>
           ,''--<ExtraFiled2, nvarchar(256),>
           ,''--<ExtraFiled3, nvarchar(256),>
           ,''--<ExtraFiled4, nvarchar(256),>
           ,''--<ExtraFiled5, nvarchar(256),>
           ,45--<AddedBy, int,>
           ,GETDATE()--<AddedDate, datetime,>
           ,45--<UpdatedBy, int,>
           ,GETDATE()--<UpdatedDate, datetime,>
           ,1--<RowSatatusID, int,>
           ,GETDATE()--<DateofBirth, datetime,>
           ,GETDATE()--<ApplicationDate, datetime,>
           ,GETDATE()--<CardIssueDate, datetime,>
           ,GETDATE()--<ExpireDate, datetime,>
           ,''--<CardType, nvarchar(50),>
           ,''--<ApprovedBy, nvarchar(256),>)
From Pos_Customer_tmp


         */
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Pos_Customer pos_Customer = new Pos_Customer();
        pos_Customer = Pos_CustomerManager.GetPos_CustomerByID(Int32.Parse(Request.QueryString["pos_CustomerID"]));
        Pos_Customer tempPos_Customer = new Pos_Customer();
        tempPos_Customer.Pos_CustomerID = pos_Customer.Pos_CustomerID;
        tempPos_Customer.ExtraFiled1 = ddlBranch.SelectedValue;
        tempPos_Customer.CustomerName = txtCustomerName.Text;
        tempPos_Customer.Address = txtAddress.Text;
        tempPos_Customer.ExtraFiled2 = txtCompanyName.Text;
        tempPos_Customer.ExtraFiled3 = txtOfficeAddress.Text;
        tempPos_Customer.Phone = txtPhone.Text;
        tempPos_Customer.ExtraFiled4 = txtdesignation.Text;
        tempPos_Customer.ExtraFiled5 = txtOccupation.Text;
        tempPos_Customer.DateofBirth = Convert.ToDateTime(txtDateofBirth.Text);
        tempPos_Customer.Mobile = txtContactNo.Text;
        tempPos_Customer.ApplicationDate = Convert.ToDateTime(txtApplicationDate.Text);
        tempPos_Customer.CardNo = txtCardNo.Text;
        tempPos_Customer.CardIssueDate = Convert.ToDateTime(txtCardIssueDate.Text);
        tempPos_Customer.ExpireDate = Convert.ToDateTime(txtExpireDate.Text);
        tempPos_Customer.CardType = ddlCardType.SelectedValue;
        //tempPos_Customer.Signature = txtSignature.Text;
        if (uplFile.PostedFile != null && uplFile.PostedFile.ContentLength > 0)
        {
            try
            {
                string dirUrl = "~/Upload";
                string dirPath = Server.MapPath(dirUrl);

                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                string fileName = Path.GetFileName(uplFile.PostedFile.FileName);
                string fileUrl = dirUrl + "/" + Path.GetFileName(uplFile.PostedFile.FileName);
                string filePath = Server.MapPath(fileUrl);
                uplFile.PostedFile.SaveAs(filePath);

                tempPos_Customer.Signature = dirUrl + "/" + fileName;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        else
        {
            tempPos_Customer.Signature = pos_Customer.Signature;
        }
        tempPos_Customer.ApprovedBy = txtApprovedBy.Text;
        tempPos_Customer.Note = txtRemark.Text;
        tempPos_Customer.ReferenceID = Convert.ToInt32(ddlReference.SelectedValue);
        
        tempPos_Customer.DiscountPersent = Decimal.Parse(txtDiscountPersent.Text);
        tempPos_Customer.AddedBy = 1;
        tempPos_Customer.AddedDate = DateTime.Now;
        tempPos_Customer.UpdatedBy = 1;
        tempPos_Customer.UpdatedDate = DateTime.Now;
        tempPos_Customer.RowSatatusID = 1;
        bool result = Pos_CustomerManager.UpdatePos_Customer(tempPos_Customer);
        Response.Redirect("AdminPos_CustomerDisplay.aspx");
    }
    //protected void btnClear_Click(object sender, EventArgs e)
    //{
    //    txtCardNo.Text = "";
    //    //txtSignature.Text = "";
    //    txtCustomerName.Text = "";
    //    //ddlReference.SelectedIndex = 0;
    //    txtAddress.Text = "";
    //    txtContactNo.Text = "";
    //    txtPhone.Text = "";
    //    txtDiscountPersent.Text = "";
    //    txtRemark.Text = "";
    //    txtBranchNo.Text = "";
    //    txtCompanyName.Text = "";
    //    txtOfficeAddress.Text = "";
    //    txtdesignation.Text = "";
    //    txtOccupation.Text = "";
    //    //txtAddedBy.Text = "";
    //    //txtUpdatedBy.Text = "";
    //    //txtUpdatedDate.Text = "";
    //    //ddlRowSatatus.SelectedIndex = 0;
    //}
    private void showPos_CustomerData()
    {
        Pos_Customer pos_Customer = new Pos_Customer();
        pos_Customer = Pos_CustomerManager.GetPos_CustomerByID(Int32.Parse(Request.QueryString["pos_CustomerID"]));

        ddlBranch.SelectedValue = pos_Customer.ExtraFiled1;
        txtCustomerName.Text = pos_Customer.CustomerName;
        txtAddress.Text = pos_Customer.Address;
        txtCompanyName.Text = pos_Customer.ExtraFiled2;
        txtOfficeAddress.Text = pos_Customer.ExtraFiled3;
        txtPhone.Text = pos_Customer.Phone;
        txtdesignation.Text = pos_Customer.ExtraFiled4;
        txtOccupation.Text = pos_Customer.ExtraFiled5;
        txtDateofBirth.Text = pos_Customer.DateofBirth.ToString("dd MMM yyyy");
        txtContactNo.Text = pos_Customer.Mobile;
        txtApplicationDate.Text = pos_Customer.ApplicationDate.ToString("dd MMM yyyy");
        txtCardNo.Text = pos_Customer.CardNo;
        txtCardIssueDate.Text = pos_Customer.CardIssueDate.ToString("dd MMM yyyy");
        txtExpireDate.Text = pos_Customer.ExpireDate.ToString("dd MMM yyyy");
        ddlCardType.SelectedValue = pos_Customer.CardType;
        txtDiscountPersent.Text = pos_Customer.DiscountPersent.ToString("0.00");
        //txtSignature.Text = pos_Customer.Signature;
        txtApprovedBy.Text = pos_Customer.ApprovedBy;
        txtRemark.Text = pos_Customer.Note;
        ddlReference.SelectedValue = pos_Customer.ReferenceID.ToString();
        //ddlReference.SelectedValue = pos_Customer.ReferenceID.ToString();
        //ddlRowSatatus.SelectedValue = pos_Customer.RowSatatusID.ToString();
    }
    //private void loadReference()
    //{
    //    ListItem li = new ListItem("Select Reference...", "0");
    //    ddlReference.Items.Add(li);

    //    List<Reference> references = new List<Reference>();
    //    references = ReferenceManager.GetAllReferences();
    //    foreach (Reference reference in references)
    //    {
    //        ListItem item = new ListItem(reference.ReferenceName.ToString(), reference.ReferenceID.ToString());
    //        ddlReference.Items.Add(item);
    //    }
    //}
    //private void loadRowSatatus()
    //{
    //    ListItem li = new ListItem("Select RowSatatus...", "0");
    //    ddlRowSatatus.Items.Add(li);

    //    List<RowSatatus> rowSatatuss = new List<RowSatatus>();
    //    rowSatatuss = RowSatatusManager.GetAllRowSatatuss();
    //    foreach (RowSatatus rowSatatus in rowSatatuss)
    //    {
    //        ListItem item = new ListItem(rowSatatus.RowSatatusName.ToString(), rowSatatus.RowSatatusID.ToString());
    //        ddlRowSatatus.Items.Add(item);
    //    }
    //}
}
