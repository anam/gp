using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

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

        int suppliyerID = 0;
        try
        {
            suppliyerID = Int32.Parse(Request.QueryString["SuppliyerID"]);
        }
        catch (Exception ex)
        {
            suppliyerID = 0;
        }

        int RawmaterialsTypeID = 0;
        try
        {
            RawmaterialsTypeID = Int32.Parse(Request.QueryString["RawmaterialsTypeID"]);
        }
        catch (Exception ex)
        {
            RawmaterialsTypeID = 0;
        }

        int ItemID = 0;
        try
        {
            ItemID = Int32.Parse(Request.QueryString["ItemID"]);
        }
        catch (Exception ex)
        {
            ItemID = 0;
        }

        //lblStockDate.Text = DateTime.Parse(Request.QueryString["date"]).ToString("dd MMM yyyy");
        string fromDate = (Request.QueryString["FromDate"] != null ? Request.QueryString["FromDate"].ToString() : "");
        string toDate = (Request.QueryString["ToDate"] != null ? Request.QueryString["ToDate"].ToString() : "");
        lblStockDate.Text = DateTime.Parse(fromDate).ToString("dd/MM/yyyy") + " to " + DateTime.Parse(toDate).ToString("dd/MM/yyyy");
        //purchase info
        string rowMetarialSQL = "and ACC_ChartOfAccountLabel4.ACC_HeadTypeID=@ACC_HeadTypeID";

        string sql = @"declare @FromDate datetime
declare @ToDate datetime
set @FromDate='" + fromDate + @"'
set @ToDate='" + toDate + @"'

Declare @ACC_HeadTypeID int
set @ACC_HeadTypeID =" + RawmaterialsTypeID + @"

--purchase Stock
Select Sum(Inv_ItemTransaction.Quantity) as Qty, Sum(Inv_ItemTransaction.Quantity * Inv_Item.PricePerUnit) as QtyPrice,Inv_Item.RawMaterialID
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
 from Inv_ItemTransaction
inner join Inv_Item on Inv_Item.Inv_ItemID = Inv_ItemTransaction.ItemID
inner join Inv_Purchase on Inv_Purchase.Inv_PurchaseID = Inv_ItemTransaction.ReferenceID
inner join ACC_ChartOfAccountLabel4 on Inv_Item.RawMaterialID = ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
where Inv_ItemTransaction.ItemTrasactionTypeID=8
" + (RawmaterialsTypeID!=0?"and ACC_ChartOfAccountLabel4.ACC_HeadTypeID=@ACC_HeadTypeID":"") + @"
and Inv_ItemTransaction.RowStatusID=1 and
Inv_Purchase.PurchseDate < @FromDate
group by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
order by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1

--purchase Current
Select Sum(Inv_ItemTransaction.Quantity) as Qty, Sum(Inv_ItemTransaction.Quantity * Inv_Item.PricePerUnit) as QtyPrice,Inv_Item.RawMaterialID
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
 from Inv_ItemTransaction
inner join Inv_Item on Inv_Item.Inv_ItemID = Inv_ItemTransaction.ItemID
inner join Inv_Purchase on Inv_Purchase.Inv_PurchaseID = Inv_ItemTransaction.ReferenceID
inner join ACC_ChartOfAccountLabel4 on Inv_Item.RawMaterialID = ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
where Inv_ItemTransaction.ItemTrasactionTypeID=8
" + (RawmaterialsTypeID != 0 ? "and ACC_ChartOfAccountLabel4.ACC_HeadTypeID=@ACC_HeadTypeID" : "") + @"
and Inv_ItemTransaction.RowStatusID=1 and
Inv_Purchase.PurchseDate >= @FromDate and  Inv_Purchase.PurchseDate <= @ToDate
group by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
order by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1


--purchase Adjustment Stock
Select Sum(Inv_ItemTransaction.Quantity) as Qty, Sum(Inv_ItemTransaction.Quantity * Inv_Item.PricePerUnit) as QtyPrice,Inv_Item.RawMaterialID
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
 from Inv_ItemTransaction
inner join Inv_Item on Inv_Item.Inv_ItemID = Inv_ItemTransaction.ItemID
inner join Inv_PurchaseAdjustment on Inv_PurchaseAdjustment.Inv_PurchaseAdjustmentID = Inv_ItemTransaction.ReferenceID
inner join ACC_ChartOfAccountLabel4 on Inv_Item.RawMaterialID = ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
where Inv_ItemTransaction.ItemTrasactionTypeID=7
" + (RawmaterialsTypeID != 0 ? "and ACC_ChartOfAccountLabel4.ACC_HeadTypeID=@ACC_HeadTypeID" : "") + @"
and Inv_ItemTransaction.RowStatusID=1 and
Inv_PurchaseAdjustment.PurchseAdjustmentDate < @FromDate
group by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
order by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1

--purchase Adjustment current
Select Sum(Inv_ItemTransaction.Quantity) as Qty, Sum(Inv_ItemTransaction.Quantity * Inv_Item.PricePerUnit) as QtyPrice,Inv_Item.RawMaterialID
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
 from Inv_ItemTransaction
inner join Inv_Item on Inv_Item.Inv_ItemID = Inv_ItemTransaction.ItemID
inner join Inv_PurchaseAdjustment on Inv_PurchaseAdjustment.Inv_PurchaseAdjustmentID = Inv_ItemTransaction.ReferenceID
inner join ACC_ChartOfAccountLabel4 on Inv_Item.RawMaterialID = ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
where Inv_ItemTransaction.ItemTrasactionTypeID=7
" + (RawmaterialsTypeID != 0 ? "and ACC_ChartOfAccountLabel4.ACC_HeadTypeID=@ACC_HeadTypeID" : "") + @"
and Inv_ItemTransaction.RowStatusID=1 and
Inv_PurchaseAdjustment.PurchseAdjustmentDate >= @FromDate and  Inv_PurchaseAdjustment.PurchseAdjustmentDate <= @ToDate
group by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
order by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1


--Purchase Return Stock
Select Sum(Inv_ItemTransaction.Quantity) as Qty, Sum(Inv_ItemTransaction.Quantity * Inv_Item.PricePerUnit) as QtyPrice,Inv_Item.RawMaterialID
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
 from Inv_ItemTransaction
inner join Inv_Item on Inv_Item.Inv_ItemID = Inv_ItemTransaction.ItemID
inner join Inv_PurchaseReturn on Inv_PurchaseReturn.Inv_PurchaseReturenID = Inv_ItemTransaction.ReferenceID
inner join ACC_ChartOfAccountLabel4 on Inv_Item.RawMaterialID = ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
where Inv_ItemTransaction.ItemTrasactionTypeID=1
" + (RawmaterialsTypeID != 0 ? "and ACC_ChartOfAccountLabel4.ACC_HeadTypeID=@ACC_HeadTypeID" : "") + @"
and Inv_ItemTransaction.RowStatusID=1 and
Inv_PurchaseReturn.PurchseReturenDate < @FromDate
group by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
order by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1



--Purchase Return Current
Select Sum(Inv_ItemTransaction.Quantity) as Qty, Sum(Inv_ItemTransaction.Quantity * Inv_Item.PricePerUnit) as QtyPrice,Inv_Item.RawMaterialID
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
 from Inv_ItemTransaction
inner join Inv_Item on Inv_Item.Inv_ItemID = Inv_ItemTransaction.ItemID
inner join Inv_PurchaseReturn on Inv_PurchaseReturn.Inv_PurchaseReturenID = Inv_ItemTransaction.ReferenceID
inner join ACC_ChartOfAccountLabel4 on Inv_Item.RawMaterialID = ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
where Inv_ItemTransaction.ItemTrasactionTypeID=1
" + (RawmaterialsTypeID != 0 ? "and ACC_ChartOfAccountLabel4.ACC_HeadTypeID=@ACC_HeadTypeID" : "") + @"
and Inv_ItemTransaction.RowStatusID=1 and
Inv_PurchaseReturn.PurchseReturenDate >= @FromDate and  Inv_PurchaseReturn.PurchseReturenDate <= @ToDate
group by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
order by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1


--Issue Stock
Select Sum(Inv_ItemTransaction.Quantity) as Qty, Sum(Inv_ItemTransaction.Quantity * Inv_Item.PricePerUnit) as QtyPrice,Inv_Item.RawMaterialID
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
 from Inv_ItemTransaction
inner join Inv_Item on Inv_Item.Inv_ItemID = Inv_ItemTransaction.ItemID
inner join Inv_IssueMaster on Inv_IssueMaster.Inv_IssueMasterID = Inv_ItemTransaction.ReferenceID
inner join ACC_ChartOfAccountLabel4 on Inv_Item.RawMaterialID = ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
where Inv_ItemTransaction.ItemTrasactionTypeID=2
" + (RawmaterialsTypeID != 0 ? "and ACC_ChartOfAccountLabel4.ACC_HeadTypeID=@ACC_HeadTypeID" : "") + @"
and Inv_ItemTransaction.RowStatusID=1 and
Inv_IssueMaster.IssueDate <@FromDate
group by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
order by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1

--Issue Current
Select Sum(Inv_ItemTransaction.Quantity) as Qty, Sum(Inv_ItemTransaction.Quantity * Inv_Item.PricePerUnit) as QtyPrice,Inv_Item.RawMaterialID
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
 from Inv_ItemTransaction
inner join Inv_Item on Inv_Item.Inv_ItemID = Inv_ItemTransaction.ItemID
inner join Inv_IssueMaster on Inv_IssueMaster.Inv_IssueMasterID = Inv_ItemTransaction.ReferenceID
inner join ACC_ChartOfAccountLabel4 on Inv_Item.RawMaterialID = ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
where Inv_ItemTransaction.ItemTrasactionTypeID=2
" + (RawmaterialsTypeID != 0 ? "and ACC_ChartOfAccountLabel4.ACC_HeadTypeID=@ACC_HeadTypeID" : "") + @"
and Inv_ItemTransaction.RowStatusID=1 and
Inv_IssueMaster.IssueDate >= @FromDate and  Inv_IssueMaster.IssueDate <= @ToDate
group by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
order by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1



--Issue Return Stock
Select Sum(Inv_ItemTransaction.Quantity) as Qty, Sum(Inv_ItemTransaction.Quantity * Inv_Item.PricePerUnit) as QtyPrice,Inv_Item.RawMaterialID
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
 from Inv_ItemTransaction
inner join Inv_Item on Inv_Item.Inv_ItemID = Inv_ItemTransaction.ItemID
inner join Inv_IssueMasterReturn on Inv_IssueMasterReturn.Inv_IssueMasterReturnID = Inv_ItemTransaction.ReferenceID
inner join ACC_ChartOfAccountLabel4 on Inv_Item.RawMaterialID = ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
where Inv_ItemTransaction.ItemTrasactionTypeID=3
" + (RawmaterialsTypeID != 0 ? "and ACC_ChartOfAccountLabel4.ACC_HeadTypeID=@ACC_HeadTypeID" : "") + @"
and Inv_ItemTransaction.RowStatusID=1 and
Inv_IssueMasterReturn.IssueReturnDate <@FromDate
group by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
order by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1



--Issue Return Current
Select Sum(Inv_ItemTransaction.Quantity) as Qty, Sum(Inv_ItemTransaction.Quantity * Inv_Item.PricePerUnit) as QtyPrice,Inv_Item.RawMaterialID
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
 from Inv_ItemTransaction
inner join Inv_Item on Inv_Item.Inv_ItemID = Inv_ItemTransaction.ItemID
inner join Inv_IssueMasterReturn on Inv_IssueMasterReturn.Inv_IssueMasterReturnID = Inv_ItemTransaction.ReferenceID
inner join ACC_ChartOfAccountLabel4 on Inv_Item.RawMaterialID = ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
where Inv_ItemTransaction.ItemTrasactionTypeID=3
" + (RawmaterialsTypeID != 0 ? "and ACC_ChartOfAccountLabel4.ACC_HeadTypeID=@ACC_HeadTypeID" : "") + @"
and Inv_ItemTransaction.RowStatusID=1 and
Inv_IssueMasterReturn.IssueReturnDate >= @FromDate and  Inv_IssueMasterReturn.IssueReturnDate <= @ToDate
group by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
order by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1


--Wastage Stock
Select Sum(Inv_ItemTransaction.Quantity) as Qty, Sum(Inv_ItemTransaction.Quantity * Inv_Item.PricePerUnit) as QtyPrice,Inv_Item.RawMaterialID
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
 from Inv_ItemTransaction
inner join Inv_Item on Inv_Item.Inv_ItemID = Inv_ItemTransaction.ItemID
inner join Inv_Wastage on Inv_Wastage.Inv_WastageID = Inv_ItemTransaction.ReferenceID
inner join ACC_ChartOfAccountLabel4 on Inv_Item.RawMaterialID = ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
where Inv_ItemTransaction.ItemTrasactionTypeID=6
" + (RawmaterialsTypeID != 0 ? "and ACC_ChartOfAccountLabel4.ACC_HeadTypeID=@ACC_HeadTypeID" : "") + @"
and Inv_ItemTransaction.RowStatusID=1 
and Inv_Wastage.WastageDate <@FromDate
group by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
order by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1

--Wastage Current
Select Sum(Inv_ItemTransaction.Quantity) as Qty, Sum(Inv_ItemTransaction.Quantity * Inv_Item.PricePerUnit) as QtyPrice,Inv_Item.RawMaterialID
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
 from Inv_ItemTransaction
inner join Inv_Item on Inv_Item.Inv_ItemID = Inv_ItemTransaction.ItemID
inner join Inv_Wastage on Inv_Wastage.Inv_WastageID = Inv_ItemTransaction.ReferenceID
inner join ACC_ChartOfAccountLabel4 on Inv_Item.RawMaterialID = ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
where Inv_ItemTransaction.ItemTrasactionTypeID=6
" + (RawmaterialsTypeID != 0 ? "and ACC_ChartOfAccountLabel4.ACC_HeadTypeID=@ACC_HeadTypeID" : "") + @"
and Inv_ItemTransaction.RowStatusID=1 and
Inv_Wastage.WastageDate >= @FromDate and  Inv_Wastage.WastageDate <= @ToDate
group by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
order by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1

--Production Stock
Select Sum(Inv_ItemTransaction.Quantity) as Qty, Sum(Inv_ItemTransaction.Quantity * Inv_Item.PricePerUnit) as QtyPrice,Inv_Item.RawMaterialID
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
 from Inv_ItemTransaction
inner join Inv_Item on Inv_Item.Inv_ItemID = Inv_ItemTransaction.ItemID
inner join Inv_Utilization on Inv_Utilization.Inv_UtilizationID = Inv_ItemTransaction.ReferenceID
inner join ACC_ChartOfAccountLabel4 on Inv_Item.RawMaterialID = ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
where Inv_ItemTransaction.ItemTrasactionTypeID=4
" + (RawmaterialsTypeID != 0 ? "and ACC_ChartOfAccountLabel4.ACC_HeadTypeID=@ACC_HeadTypeID" : "") + @"
and Inv_ItemTransaction.RowStatusID=1 
and Inv_Utilization.UtilizationDate <@FromDate
group by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
order by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1



--Production Current
Select Sum(Inv_ItemTransaction.Quantity) as Qty, Sum(Inv_ItemTransaction.Quantity * Inv_Item.PricePerUnit) as QtyPrice,Inv_Item.RawMaterialID
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
 from Inv_ItemTransaction
inner join Inv_Item on Inv_Item.Inv_ItemID = Inv_ItemTransaction.ItemID
inner join Inv_Utilization on Inv_Utilization.Inv_UtilizationID = Inv_ItemTransaction.ReferenceID
inner join ACC_ChartOfAccountLabel4 on Inv_Item.RawMaterialID = ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
where Inv_ItemTransaction.ItemTrasactionTypeID=4
" + (RawmaterialsTypeID != 0 ? "and ACC_ChartOfAccountLabel4.ACC_HeadTypeID=@ACC_HeadTypeID" : "") + @"
and Inv_ItemTransaction.RowStatusID=1 
and 
Inv_Utilization.UtilizationDate >= @FromDate and  Inv_Utilization.UtilizationDate <= @ToDate
group by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1
order by Inv_Item.RawMaterialID,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ExtraField1

";

        DataSet ds= CommonManager.SQLExec(sql);

        List<Inv_Item> items = new List<Inv_Item>();
        foreach (DataTable tbl in ds.Tables)
        {
            foreach (DataRow dr in tbl.Rows)
            {
                Inv_Item newItem = new Inv_Item();
                bool isFound=false;
                foreach (Inv_Item inv_Item in items)
	            {
                    if (dr["RawMaterialID"].ToString() == inv_Item.Inv_ItemID.ToString())
                    {
                        isFound = true;
                        break;
                    }
	            }

                if (!isFound)
                {
                    newItem.Inv_ItemID = int.Parse(dr["RawMaterialID"].ToString());
                    newItem.ItemCode = dr["ExtraField1"].ToString();
                    newItem.ItemName = dr["ChartOfAccountLabel4Text"].ToString();
                    newItem.ExtraField1="0@0";
                    newItem.ExtraField2 = "0@0";
                    newItem.ExtraField3 = "0@0";
                    newItem.ExtraField4 = "0@0";
                    newItem.ExtraField5 = "0@0";
                    newItem.ExtraField6 = "0@0";
                    newItem.ExtraField7 = "0@0";
                    newItem.ExtraField8 = "0@0";
                    newItem.ExtraField9 = "0@0";
                    newItem.ExtraField10 = "0@0";
                    newItem.ExtraFieldQuantity1=0;
                    newItem.ExtraFieldQuantity2=0;
                    newItem.ExtraFieldQuantity3=0;
                    newItem.ExtraFieldQuantity4=0;
                    newItem.ExtraFieldQuantity5 = 0;
                    newItem.IssueReturedQuantity = 0;
                    newItem.LostQuantity = 0;
                    newItem.PurchasedQuantity = 0;
                    newItem.PurchasedQuantityPrice = 0;
                    newItem.QualityValue = 0;
                    

                    items.Add(newItem);
                }
            }

        }

        sql = "";
        foreach (Inv_Item inv_Item in items)
        {
            sql += @"select top 1 Inv_QuantityUnit.QuantityUnitName,'" +inv_Item.Inv_ItemID.ToString()+@"' as RawMetarialID from Inv_Item
                        inner join Inv_QuantityUnit on Inv_QuantityUnit.Inv_QuantityUnitID= Inv_Item.QuantityUnitID
                        where RawMaterialID=" +inv_Item.Inv_ItemID.ToString()+";";
        }

        DataSet ds2 = CommonManager.SQLExec(sql);

        for (int i = 0; i < items.Count; i++)
        {
            if (ds2.Tables[i].Rows[0]["RawMetarialID"].ToString() == items[i].Inv_ItemID.ToString())
            {
                items[i].QuantityUnitName = ds2.Tables[i].Rows[0]["QuantityUnitName"].ToString();
            }
        }


        foreach (Inv_Item inv_Item in items)
        {
            //Purchase stock
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (inv_Item.Inv_ItemID.ToString() == dr["RawMaterialID"].ToString())
                {
                    inv_Item.ExtraField1 = decimal.Parse(dr["Qty"].ToString()).ToString("0.00") + "@" + decimal.Parse(dr["QtyPrice"].ToString()).ToString("0.00");
                    break;
                }
            }

            //Purchase Current
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                if (inv_Item.Inv_ItemID.ToString() == dr["RawMaterialID"].ToString())
                {
                    inv_Item.ExtraField2 = decimal.Parse(dr["Qty"].ToString()).ToString("0.00") + "@" + decimal.Parse(dr["QtyPrice"].ToString()).ToString("0.00");;
                    break;
                }
            }

            //purchase Adjustment Stock
            foreach (DataRow dr in ds.Tables[2].Rows)
            {
                if (inv_Item.Inv_ItemID.ToString() == dr["RawMaterialID"].ToString())
                {
                    inv_Item.ExtraField3 = decimal.Parse(dr["Qty"].ToString()).ToString("0.00") + "@" + decimal.Parse(dr["QtyPrice"].ToString()).ToString("0.00");;
                    break;
                }
            }

            //purchase Adjustment Current
            foreach (DataRow dr in ds.Tables[3].Rows)
            {
                if (inv_Item.Inv_ItemID.ToString() == dr["RawMaterialID"].ToString())
                {
                    inv_Item.ExtraField4 = decimal.Parse(dr["Qty"].ToString()).ToString("0.00") + "@" + decimal.Parse(dr["QtyPrice"].ToString()).ToString("0.00");;
                    break;
                }
            }

            //purchase Return Stock
            foreach (DataRow dr in ds.Tables[4].Rows)
            {
                if (inv_Item.Inv_ItemID.ToString() == dr["RawMaterialID"].ToString())
                {
                    inv_Item.ExtraField5 = decimal.Parse(dr["Qty"].ToString()).ToString("0.00") + "@" + decimal.Parse(dr["QtyPrice"].ToString()).ToString("0.00");;
                    break;
                }
            }

            //purchase Return Current
            foreach (DataRow dr in ds.Tables[5].Rows)
            {
                if (inv_Item.Inv_ItemID.ToString() == dr["RawMaterialID"].ToString())
                {
                    inv_Item.ExtraField6 = decimal.Parse(dr["Qty"].ToString()).ToString("0.00") + "@" + decimal.Parse(dr["QtyPrice"].ToString()).ToString("0.00");;
                    break;
                }
            }

            //Issue Stock
            foreach (DataRow dr in ds.Tables[6].Rows)
            {
                if (inv_Item.Inv_ItemID.ToString() == dr["RawMaterialID"].ToString())
                {
                    inv_Item.ExtraField7 = decimal.Parse(dr["Qty"].ToString()).ToString("0.00") + "@" + decimal.Parse(dr["QtyPrice"].ToString()).ToString("0.00");;
                    break;
                }
            }

            //Issue Current
            foreach (DataRow dr in ds.Tables[7].Rows)
            {
                if (inv_Item.Inv_ItemID.ToString() == dr["RawMaterialID"].ToString())
                {
                    inv_Item.ExtraField8 = decimal.Parse(dr["Qty"].ToString()).ToString("0.00") + "@" + decimal.Parse(dr["QtyPrice"].ToString()).ToString("0.00");;
                    break;
                }
            }

            //Issue Return Stock
            foreach (DataRow dr in ds.Tables[8].Rows)
            {
                if (inv_Item.Inv_ItemID.ToString() == dr["RawMaterialID"].ToString())
                {
                    inv_Item.ExtraField9 = decimal.Parse(dr["Qty"].ToString()).ToString("0.00") + "@" + decimal.Parse(dr["QtyPrice"].ToString()).ToString("0.00");;
                    break;
                }
            }

            //Issue Return Current
            foreach (DataRow dr in ds.Tables[9].Rows)
            {
                if (inv_Item.Inv_ItemID.ToString() == dr["RawMaterialID"].ToString())
                {
                    inv_Item.ExtraField10 = decimal.Parse(dr["Qty"].ToString()).ToString("0.00") + "@" + decimal.Parse(dr["QtyPrice"].ToString()).ToString("0.00");;
                    break;
                }
            }


            //Wastage Stock
            foreach (DataRow dr in ds.Tables[10].Rows)
            {
                if (inv_Item.Inv_ItemID.ToString() == dr["RawMaterialID"].ToString())
                {
                    inv_Item.ExtraFieldQuantity1 = decimal.Parse(dr["Qty"].ToString());
                    inv_Item.LostQuantity = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }

            //Wastage Current
            foreach (DataRow dr in ds.Tables[11].Rows)
            {
                if (inv_Item.Inv_ItemID.ToString() == dr["RawMaterialID"].ToString())
                {
                    inv_Item.ExtraFieldQuantity2 = decimal.Parse(dr["Qty"].ToString());
                    inv_Item.QualityValue = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }


            //Utilization Stock
            foreach (DataRow dr in ds.Tables[12].Rows)
            {
                if (inv_Item.Inv_ItemID.ToString() == dr["RawMaterialID"].ToString())
                {
                    inv_Item.ExtraFieldQuantity3 = decimal.Parse(dr["Qty"].ToString());
                    inv_Item.UtilizedQuantity = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }

            //Utilization Current
            foreach (DataRow dr in ds.Tables[13].Rows)
            {
                if (inv_Item.Inv_ItemID.ToString() == dr["RawMaterialID"].ToString())
                {
                    inv_Item.ExtraFieldQuantity4 = decimal.Parse(dr["Qty"].ToString());
                    inv_Item.ExtraFieldQuantity5 = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }

            //Opining Stock
            inv_Item.PurchasedQuantity =
                decimal.Parse(inv_Item.ExtraField1.Split('@')[0]) //Purchase
                +
                decimal.Parse(inv_Item.ExtraField3.Split('@')[0]) //Purchase Adjustment
                -
                decimal.Parse(inv_Item.ExtraField5.Split('@')[0]) //Purchase Return
                -
                decimal.Parse(inv_Item.ExtraField7.Split('@')[0]) //Issue
                +
                decimal.Parse(inv_Item.ExtraField9.Split('@')[0]) //Issue Return
                -
                inv_Item.ExtraFieldQuantity1 //Wastage
                //-
                //inv_Item.ExtraFieldQuantity3
                ; //Utilization
 
            //Stock in hand
            inv_Item.PurchasedQuantityPrice =
               inv_Item.PurchasedQuantity
               +
                decimal.Parse(inv_Item.ExtraField2.Split('@')[0]) //Purchase
               +
               decimal.Parse(inv_Item.ExtraField4.Split('@')[0]) //Purchase Adjustment
               -
               decimal.Parse(inv_Item.ExtraField6.Split('@')[0]) //Purchase Return
               -
               decimal.Parse(inv_Item.ExtraField8.Split('@')[0]) //Issue
               +
               decimal.Parse(inv_Item.ExtraField10.Split('@')[0]) //Issue Return
               -
               inv_Item.ExtraFieldQuantity2 //Wastage
               //-
               //inv_Item.ExtraFieldQuantity4
               ; //Utilization

            //Stock Price
            inv_Item.IssueReturedQuantity =
                decimal.Parse(inv_Item.ExtraField1.Split('@')[1]) //Purchase
                +
                decimal.Parse(inv_Item.ExtraField3.Split('@')[1]) //Purchase Adjustment
                -
                decimal.Parse(inv_Item.ExtraField5.Split('@')[1]) //Purchase Return
                -
                decimal.Parse(inv_Item.ExtraField7.Split('@')[1]) //Issue
                +
                decimal.Parse(inv_Item.ExtraField9.Split('@')[1]) //Issue Return
                -
                inv_Item.LostQuantity //Wastage
                //-
                //inv_Item.UtilizedQuantity //Utilization
                +
                decimal.Parse(inv_Item.ExtraField2.Split('@')[1]) //Purchase
               +
               decimal.Parse(inv_Item.ExtraField4.Split('@')[1]) //Purchase Adjustment
               -
               decimal.Parse(inv_Item.ExtraField6.Split('@')[1]) //Purchase Return
               -
               decimal.Parse(inv_Item.ExtraField8.Split('@')[1]) //Issue
               +
               decimal.Parse(inv_Item.ExtraField10.Split('@')[1]) //Issue Return
               -
               inv_Item.QualityValue //Wastage
               //-
                //inv_Item.ExtraFieldQuantity5 //Utilization
               ; 


            //keep on the quantity
            inv_Item.ExtraField1 = inv_Item.ExtraField1.Split('@')[0];
            inv_Item.ExtraField2 = inv_Item.ExtraField2.Split('@')[0];
            inv_Item.ExtraField3 = inv_Item.ExtraField3.Split('@')[0];
            inv_Item.ExtraField4 = inv_Item.ExtraField4.Split('@')[0];
            inv_Item.ExtraField5 = inv_Item.ExtraField5.Split('@')[0];
            inv_Item.ExtraField6 = inv_Item.ExtraField6.Split('@')[0];
            inv_Item.ExtraField7 = inv_Item.ExtraField7.Split('@')[0];
            inv_Item.ExtraField8 = inv_Item.ExtraField8.Split('@')[0];
            inv_Item.ExtraField9 = inv_Item.ExtraField9.Split('@')[0];
            inv_Item.ExtraField10 = inv_Item.ExtraField10.Split('@')[0];
        }



        decimal OpiningQuantity = 0;
        decimal PurchasedQuantity = 0;
        decimal PurchaseAdjustMent = 0;
        decimal PurchaseReturn = 0;
        decimal IssuedQuantity = 0;
        decimal IssueReturedQuantity = 0;
        decimal WastageQuantity = 0;
        decimal UtilizedQuantity = 0;
        decimal AvailableInStore = 0;
        decimal StoreTotalAmount = 0;
            
        int serialNo = 1;
        
            string htmlTable = @" <table id='itemList_tbl' style='border:1px solid black;width:100%;' cellpadding='0' cellspacing='0'>
                        <tr id='tableHeader'>
                            <td  style='border-left:0px;'>S/L</td>
                            <td>Item Code</td>
                            <td>Item Name</td>
                            <td>Unit</td>
                            <td>Opining</td>
                            <td><a href='PurchaseReportSupplierWisePrint.aspx?RawmaterialsTypeID=" + RawmaterialsTypeID + @"&SuppliyerID=0&FromDate=" + fromDate + "&ToDate=" + toDate + "' target='_blank'>Purchase<br/>Quantity" + @"</a></td>
                            <td>Purchase<br/>Adjustment<br/>Quantity</td>
                            <td>Purchase<br/>Return<br/>Quantity</td>
                            <td><a href='IssueReportWorkStationWisePrint.aspx?RawmaterialsTypeID=" + RawmaterialsTypeID + @"&WorkstationID=0&FromDate=" + fromDate + "&ToDate=" + toDate + "' target='_blank'>Issue<br/>Quantity" + @"</a></td></td>
                            <td>Issue<br/>Return<br/>Quantity</td>
                            <td>Wastage<br/>Quantity</td>
                            <td>Utlized<br/>Quantity</td>
                            <td>Stock<br/>Quantity</td>
                            <td>Stock<br/>Amount</td>
                        </tr>";
            foreach (Inv_Item inv_Item in items)
            {
                if (inv_Item.PurchasedQuantity == 0 
                    && inv_Item.PurchasedQuantityPrice == 0 &&
                    inv_Item.ExtraField2 == "0"&&
                    inv_Item.ExtraField4== "0"&&
                    inv_Item.ExtraField6 == "0"&&
                    inv_Item.ExtraField8 == "0"&&
                    inv_Item.ExtraField10 == "0"&&
                    inv_Item.ExtraFieldQuantity2==0 &&
                    inv_Item.ExtraFieldQuantity4==0 
                    )
                {
                    continue;
                }

                htmlTable += @"<tr class='itemCss'>
                            <td  style='border-left:0px;'>" + (serialNo++).ToString() + @"</td>
                            <td>" +inv_Item.ItemCode + @"</td>
                            <td>" + inv_Item.ItemName + @"</td>
                            <td >" + inv_Item.QuantityUnitName + @"</td>
                            <td style='text-align:right;'>" + inv_Item.PurchasedQuantity.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(inv_Item.ExtraField2).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(inv_Item.ExtraField4).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(inv_Item.ExtraField6).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(inv_Item.ExtraField8).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(inv_Item.ExtraField10).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + inv_Item.ExtraFieldQuantity2.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + inv_Item.ExtraFieldQuantity4.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + inv_Item.PurchasedQuantityPrice.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + inv_Item.IssueReturedQuantity.ToString("0,0.00") + @"</td>
                        </tr>";

                OpiningQuantity += inv_Item.PurchasedQuantity;
                PurchasedQuantity += decimal.Parse(inv_Item.ExtraField2);
                PurchaseAdjustMent += decimal.Parse(inv_Item.ExtraField4);
                PurchaseReturn += decimal.Parse(inv_Item.ExtraField6);
                IssuedQuantity += decimal.Parse(inv_Item.ExtraField8);
                IssueReturedQuantity += decimal.Parse(inv_Item.ExtraField10);
                WastageQuantity += inv_Item.ExtraFieldQuantity2;
                UtilizedQuantity += inv_Item.ExtraFieldQuantity4;
                AvailableInStore += inv_Item.PurchasedQuantityPrice;
                StoreTotalAmount += inv_Item.IssueReturedQuantity;
            }

            htmlTable += @"<tr class='subtotalRow'>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>Total</td>
                            <td style='text-align:right;'>" + OpiningQuantity.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + PurchasedQuantity.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + PurchaseAdjustMent.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + PurchaseReturn.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + IssuedQuantity.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + IssueReturedQuantity.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + WastageQuantity.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + UtilizedQuantity.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + AvailableInStore.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + StoreTotalAmount.ToString("0,0.00") + @"</td>
                        </tr></table>";


        lblItemList.Text = htmlTable;
    }

    private void initialLoad()
    {
        lblPrintDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
    }
}