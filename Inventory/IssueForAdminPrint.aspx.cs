using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inventory_PurchasePrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initialLoad();
            loadData();
        }
    }

    private void loadData()
    {
        int issueID = int.Parse(Request.QueryString["IssueID"]);

        //purchase info
        Inv_IssueMaster issueMaster = Inv_IssueMasterManager.GetInv_IssueMasterByID(issueID);
        lblPurchaseDate.Text = issueMaster.IssueDate.ToString("dd-MMM-yyyy");
        lblPurchaseID.Text = "<a href='IssueModification.aspx?IssueMasterID=" + issueMaster.Inv_IssueMasterID.ToString() + "' target='_blank'>" + issueMaster.Inv_IssueMasterID.ToString() + "</a>";
        //lblInvoiceNo.Text = purchase.InvoiceNo;
        lblSupplierName.Text = issueMaster.ExtraField1;
        lblParticulars.Text = issueMaster.Particulars;

        if (issueMaster.IsIssue)
        {
            #region Productive issue

            //Item Info
            List<Inv_IssueDetail> inv_IssueDetailsFromDB = Inv_IssueDetailManager.GetAllInv_IssueDetailsByIssueMasterID(issueID.ToString());

            List<Inv_IssueDetail> inv_IssueDetailsArranged = new List<Inv_IssueDetail>();

            foreach (Inv_IssueDetail AddedIssedItem in inv_IssueDetailsFromDB)
            {
                AddedIssedItem.IsProcessed = false;
            }

            foreach (Inv_IssueDetail rootIssedItem in inv_IssueDetailsFromDB)
            {
                if (rootIssedItem.AdditionalWithIssueDetailID == 0)
                {
                    rootIssedItem.ParentChildGap = "";
                    rootIssedItem.IsProcessed = true;
                    inv_IssueDetailsArranged.Add(rootIssedItem);
                    foreach (Inv_IssueDetail AddedIssedItem in inv_IssueDetailsFromDB)
                    {
                        if (AddedIssedItem.AdditionalWithIssueDetailID == rootIssedItem.Inv_IssueDetailID)
                        {
                            AddedIssedItem.IsProcessed = true;
                            AddedIssedItem.ParentChildGap = "----";
                            inv_IssueDetailsArranged.Add(AddedIssedItem);
                        }
                    }
                }
            }

            foreach (Inv_IssueDetail AddedIssedItem in inv_IssueDetailsFromDB)
            {
                if (!AddedIssedItem.IsProcessed)
                {
                    AddedIssedItem.IsProcessed = true;
                    AddedIssedItem.ParentChildGap = "";
                    inv_IssueDetailsArranged.Add(AddedIssedItem);
                }
            }

            string htmlTable = @" <table id='itemList_tbl' style='border:1px solid black;width:100%;' cellpadding='0' cellspacing='0'>
                        <tr id='tableHeader'>
                            <td  style='border-left:0px;'>SL no</td>
                            <td>Code</td>
                            <td>Description</td>
                            <td>Quantity</td>
                            <td>Unit</td>
                            <td  >Unit price</td>
                            <td  >Amount</td>
                            <td>Quality</td>
                            <td>Issue For</td>
                            <td  >Qppx. Qty</td>
                        </tr>";
            int lastProductID = 0;
            
            decimal subTotalQuantity = 0;
            decimal subTotalAmount = 0;
            decimal subTotalAppxQty = 0;
            
            decimal totalQuantity = 0;
            decimal totalAmount = 0;
            decimal totalAppxQty = 0;
            
            int serialNo = 1;
            foreach (Inv_IssueDetail AddedIssedItem in inv_IssueDetailsArranged)
            {
                if (lastProductID != 0 && lastProductID != AddedIssedItem.ProductID)
                {
                    htmlTable += @"<tr class='subtotalRow'>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>Sub Total</td>
                            <td>" + subTotalQuantity.ToString("0,0.00") + @"</td>
                            <td>&nbsp;</td>
                            <td  >&nbsp;</td>
                            <td  >" + subTotalAmount.ToString("0,0.00") + @"</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td >" + subTotalAppxQty.ToString("0,0.00") + @"</td>
                        </tr>";

                    subTotalQuantity = 0;
                    subTotalAmount = 0;
                    subTotalAppxQty = 0;
                }

                if (lastProductID != AddedIssedItem.ProductID)
                {
                    lastProductID = AddedIssedItem.ProductID;
                    htmlTable += @"<tr>
                            <td colspan='10' style='padding-left:50px; border-top:1px solid black;border-bottom:1px solid black;font-weight:bold;'>
                                Issue For: " + AddedIssedItem.ProductName + @"
                            </td>
                        </tr>";
                }

                htmlTable += @"<tr class='itemCss'>
                            <td  style='border-left:0px;'>" + (serialNo++).ToString() +(AddedIssedItem.AddedDate <= DateTime.Parse("19 Apr 2012")?"": ("&nbsp;<a href='IssueDetailDelete.aspx?IssueDetailID="+AddedIssedItem.Inv_IssueDetailID.ToString()+"&IssueID="+Request.QueryString["IssueID"]+"' style='color:red;'>X</a>") )+ @"</td>
                            <td>" + AddedIssedItem.ItemCode + @"</td>
                            <td>" + AddedIssedItem.ItemName + @"</td>
                            <td style='text-align:right;'>" + AddedIssedItem.Quantity.ToString("0,0.00") + @"</td>
                            <td>" + AddedIssedItem.QuantityUnitName + @"</td>
                            <td style='text-align:right;'>" + AddedIssedItem.PricePerUnit.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + AddedIssedItem.TotalPrice.ToString("0,0.00") + @"</td>
                            <td>" + AddedIssedItem.QualityUnitValue + @" - " + AddedIssedItem.QualityUnitName + @"</td>                          
                            <td>" + AddedIssedItem.ProductName + @"</td>
                            <td style='text-align:right;'>" + AddedIssedItem.ApproximateQuantity.ToString("0,0.00") + @"</td>
                        </tr>";

                subTotalQuantity += AddedIssedItem.Quantity;
                subTotalAmount += AddedIssedItem.TotalPrice;
                subTotalAppxQty += AddedIssedItem.ApproximateQuantity;

                totalQuantity += AddedIssedItem.Quantity;
                totalAmount += AddedIssedItem.TotalPrice;
                totalAppxQty += AddedIssedItem.ApproximateQuantity;
            }

            htmlTable += @"<tr class='subtotalRow'>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>Sub Total</td>
                            <td>" + subTotalQuantity.ToString("0,0.00") + @"</td>
                            <td>&nbsp;</td>
                            <td  >&nbsp;</td>
                            <td  >" + subTotalAmount.ToString("0,0.00") + @"</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td  >" + subTotalAppxQty.ToString("0,0.00") + @"</td>
                        </tr>";


            htmlTable += @"<tr  id='lastRow'>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>Grand Total</td>
                            <td>" + totalQuantity.ToString("0,0.00") + @"</td>
                            <td>&nbsp;</td>
                            <td  >&nbsp;</td>
                            <td  >" + totalAmount.ToString("0,0.00") + @"</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td >" + totalAppxQty.ToString("0,0.00") + @"</td>
                        </tr></table>";

            lblItemList.Text = htmlTable;

            #endregion
        }
        else
        {
            #region Non-productive issue
            //Item Info
            List<Inv_IssueDetail> inv_IssueDetailsFromDB = Inv_IssueDetailManager.GetAllInv_IssueDetailsByIssueMasterID(issueID.ToString());

            List<Inv_IssueDetail> inv_IssueDetailsArranged = new List<Inv_IssueDetail>();

            foreach (Inv_IssueDetail AddedIssedItem in inv_IssueDetailsFromDB)
            {
                AddedIssedItem.IsProcessed = false;
            }

            foreach (Inv_IssueDetail rootIssedItem in inv_IssueDetailsFromDB)
            {
                if (rootIssedItem.AdditionalWithIssueDetailID == 0)
                {
                    rootIssedItem.ParentChildGap = "";
                    rootIssedItem.IsProcessed = true;
                    inv_IssueDetailsArranged.Add(rootIssedItem);
                    foreach (Inv_IssueDetail AddedIssedItem in inv_IssueDetailsFromDB)
                    {
                        if (AddedIssedItem.AdditionalWithIssueDetailID == rootIssedItem.Inv_IssueDetailID)
                        {
                            AddedIssedItem.IsProcessed = true;
                            AddedIssedItem.ParentChildGap = "----";
                            inv_IssueDetailsArranged.Add(AddedIssedItem);
                        }
                    }
                }
            }

            foreach (Inv_IssueDetail AddedIssedItem in inv_IssueDetailsFromDB)
            {
                if (!AddedIssedItem.IsProcessed)
                {
                    AddedIssedItem.IsProcessed = true;
                    AddedIssedItem.ParentChildGap = "";
                    inv_IssueDetailsArranged.Add(AddedIssedItem);
                }
            }

            string htmlTable = @" <table id='itemList_tbl' style='border:1px solid black;width:100%;' cellpadding='0' cellspacing='0'>
                        <tr id='tableHeader'>
                            <td  style='border-left:0px;'>SL no</td>
                            <td>Product ID</td>
                            <td>Description</td>
                            <td>Quantity</td>
                            <td>Rate</td>
                            <td>Amount</td>
                        </tr>";
            decimal totalQuantity = 0;
            decimal totalAmount = 0;

            int serialNo = 1;
            foreach (Inv_IssueDetail AddedIssedItem in inv_IssueDetailsArranged)
            {
                htmlTable += @"<tr class='itemCss'>
                            <td  style='border-left:0px;'>" + (serialNo++).ToString() + @"</td>
                            <td>" + AddedIssedItem.ItemCode + @"</td>
                            <td>" + AddedIssedItem.ItemName + @"</td>
                            <td style='text-align:right;'>" + AddedIssedItem.Quantity.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + AddedIssedItem.PricePerUnit.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + AddedIssedItem.TotalPrice.ToString("0,0.00") + @"</td>
                        </tr>";

                totalQuantity += AddedIssedItem.Quantity;
                totalAmount += AddedIssedItem.TotalPrice;
            }

            htmlTable += @"<tr  id='lastRow'>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>Grand Total</td>
                            <td>" + totalQuantity.ToString("0,0.00") + @"</td>
                            <td>&nbsp;</td>
                            <td>" + totalAmount.ToString("0,0.00") + @"</td>
                        </tr></table>";

            lblItemList.Text = htmlTable;
            #endregion
        }
    }

    private void initialLoad()
    {
        lblPrintDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
    }
}