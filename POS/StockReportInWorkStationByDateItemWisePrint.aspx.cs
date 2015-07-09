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
        int workStationID = 0;
        try
        {
            workStationID = Int32.Parse(Request.QueryString["WorkStationID"]);
        }
        catch (Exception ex)
        {
            workStationID = 0;
        }

        string fromDate = (Request.QueryString["FromDate"] != null ? Request.QueryString["FromDate"].ToString() : "");
        string toDate = (Request.QueryString["ToDate"] != null ? Request.QueryString["ToDate"].ToString() : "");
        lblStockDate.Text = DateTime.Parse(fromDate).ToString("dd/MM/yyyy") + " to " + DateTime.Parse(toDate).ToString("dd/MM/yyyy");
        //purchase info
        string sql = @"declare @FromDate datetime
declare @ToDate datetime
set @FromDate='" + fromDate + @"'
set @ToDate='" + toDate + @"'

";

        List<Pos_TransactionType> allTransactionType = Pos_TransactionTypeManager.GetAllPos_TransactionTypes();

        foreach (Pos_TransactionType type in allTransactionType)
        {
           try
                {
                    if (int.Parse(type.ShowRoomFormula) != 0)
                    {
                       
                    
                sql += @"--" + type.TransactionTypeName + @" Stock 
                    Select Sum(Pos_Transaction.Quantity) as Qty, Sum(Pos_Transaction.Quantity * Pos_Product.SalePrice) as QtyPrice
                    ,Pos_Product.Pos_ProductID as ProductID
                    ,Pos_Product.ProductName as ChartOfAccountLabel4Text
                    ,Pos_Product.BarCode as ExtraField1
                     from Pos_Transaction
                    inner join Pos_Product on pos_Product.Pos_ProductID = Pos_Transaction.Pos_ProductID
                    inner join Pos_TransactionMaster on Pos_TransactionMaster.Pos_TransactionMasterID = Pos_Transaction.Pos_ProductTransactionMasterID
                    --inner join ACC_ChartOfAccountLabel4 on Pos_Product.ProductID = ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
                    where Pos_Transaction.Pos_ProductTrasactionTypeID =" + type.Pos_TransactionTypeID + @"
                    and " + (type.Pos_TransactionTypeID == 10 ? "Pos_TransactionMaster.WorkSatationID=" : "Pos_Transaction.WorkStationID=") + workStationID.ToString() + @"
                    and Pos_Transaction.RowStatusID=1 
                    " + ((type.Pos_TransactionTypeID == 12 && type.Pos_TransactionTypeID == 9) ? " and Pos_TransactionMaster.ExtraField5 <> 'Pending'" : "") + @"
                    and Pos_TransactionMaster.TransactionDate < @FromDate
                    group by Pos_Product.Pos_ProductID
                    ,Pos_Product.ProductName
                    ,Pos_Product.BarCode
                    order by Pos_Product.ProductName,Pos_Product.BarCode

                    -- " + type.TransactionTypeName + @" Current 
                    Select Sum(Pos_Transaction.Quantity) as Qty, Sum(Pos_Transaction.Quantity * Pos_Product.SalePrice) as QtyPrice
                    ,Pos_Product.Pos_ProductID as ProductID
                    ,Pos_Product.ProductName as ChartOfAccountLabel4Text
                    ,Pos_Product.BarCode as ExtraField1
                     from Pos_Transaction
                    inner join Pos_Product on pos_Product.Pos_ProductID = Pos_Transaction.Pos_ProductID
                    inner join Pos_TransactionMaster on Pos_TransactionMaster.Pos_TransactionMasterID = Pos_Transaction.Pos_ProductTransactionMasterID
                    --inner join ACC_ChartOfAccountLabel4 on Pos_Product.ProductID = ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
                    where Pos_Transaction.Pos_ProductTrasactionTypeID =" + type.Pos_TransactionTypeID + @"
                    and " + (type.Pos_TransactionTypeID == 10 ? "Pos_TransactionMaster.WorkSatationID=" : "Pos_Transaction.WorkStationID=") +  workStationID.ToString() + @"
                    and Pos_Transaction.RowStatusID=1 
                    " + ((type.Pos_TransactionTypeID == 12 && type.Pos_TransactionTypeID == 9) ? " and Pos_TransactionMaster.ExtraField5 <> 'Pending'" : "") + @"
                    and Pos_TransactionMaster.TransactionDate >= @FromDate and Pos_TransactionMaster.TransactionDate <= @ToDate
                    group by Pos_Product.Pos_ProductID
                    ,Pos_Product.ProductName
                    ,Pos_Product.BarCode
                    order by Pos_Product.ProductName,Pos_Product.BarCode
                    ";
                    }
                }
           catch (Exception ex)
           { }
        }

        sql += @" 
        Select ChartOfAccountLabel4Text from ACC_ChartOfAccountLabel4 where ACC_ChartOfAccountLabel4ID=" + Request.QueryString["WorkStationID"] + @";
        ";

        DataSet ds= CommonManager.SQLExec(sql);

        List<Pos_Product> Products = new List<Pos_Product>();

        foreach (DataTable tbl in ds.Tables)
        {
            foreach (DataRow dr in tbl.Rows)
            {
                Pos_Product newProduct = new Pos_Product();
                bool isFound = false;
                foreach (Pos_Product pos_Product in Products)
                {
                    try
                    {
                        if (dr["ProductID"].ToString() == pos_Product.ProductID.ToString())
                        {
                            isFound = true;
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                }

                if (!isFound)
                {
                    try
                    {
                        newProduct.ProductID = int.Parse(dr["ProductID"].ToString());
                    }
                    catch (Exception ex)
                    {
                        break;
                    } 
                    
                    newProduct.BarCode = dr["ExtraField1"].ToString();
                    newProduct.ProductName = dr["ChartOfAccountLabel4Text"].ToString();
                    newProduct.Production_Stock=0;
                    newProduct.Production_Current=0;
                    newProduct.ProductionAdjustment_Stock=0;
                    newProduct.ProductionAdjustment_Current=0;
                    newProduct.Purchase_Stock=0;
                    newProduct.Purchase_Current=0;
                    newProduct.PurchaseAdjustment_Stock=0;
                    newProduct.PurchaseAdjustment_Current=0;
                    newProduct.PurchaseReturn_Stock=0;
                    newProduct.PurchaseReturn_Current=0;
                    newProduct.Lost_HO_Stock=0;
                    newProduct.Lost_HO_Current=0;
                    newProduct.Wastage_HO_Stock=0;
                    newProduct.Wastage_HO_Current=0;
                    newProduct.Issue_Wash_HO_Stock=0;
                    newProduct.Issue_Wash_HO_Current=0;
                    newProduct.Back_After_Wash_Stock=0;
                    newProduct.Back_After_Wash_Current=0;
                    newProduct.Issue_for_Repair_Stock=0;
                    newProduct.Issue_for_Repair_Current=0;
                    newProduct.Back_after_repair_Stock=0;
                    newProduct.Back_after_repair_Current=0;
                    newProduct.Issue_to_Showroom_Stock=0;
                    newProduct.Issue_to_Showroom_Current=0;
                    newProduct.Issue_Return_HO_Stock=0;
                    newProduct.Issue_Return_HO_Current=0;
                    newProduct.Send_another_Showroom_Stock=0;
                    newProduct.Send_another_Showroom_Current=0;
                    newProduct.Received_another_Showroom_Stock=0;
                    newProduct.Received_another_Showroom_Current=0;
                    newProduct.Sales_Stock=0;
                    newProduct.Sales_Current=0;
                    newProduct.Sales_return_Stock=0;
                    newProduct.Sales_return_Current=0;
                    newProduct.Gift_HO_Stock=0;
                    newProduct.Gift_HO_Current=0;
                    newProduct.Gift_Showroom_Stock=0;
                    newProduct.Gift_Showroom_Current=0;
                    newProduct.Send_wash_Showroom_Stock=0;
                    newProduct.Send_wash_Showroom_Current=0;
                    newProduct.Received_wash_Showroom_Stock=0;
                    newProduct.Received_wash_Showroom_Current=0;
                    newProduct.Send_repair_Showroom_Stock=0;
                    newProduct.Send_repair_Showroom_Current=0;
                    newProduct.Received_repair_Showroom_Stock=0;
                    newProduct.Received_repair_Showroom_Current=0;
                    newProduct.Lost_Showroom_Stock=0;
                    newProduct.Lost_Showroom_Current=0;
                    newProduct.Wastage_Showroom_Stock=0;
                    newProduct.Wastage_Showroom_Current=0;
                    newProduct.Adjustment_Add_Showroom_Stock=0;
                    newProduct.Adjustment_Add_Showroom_Current=0;
                    newProduct.Adjustment_Sub_Showroom_Stock=0;
                    newProduct.Adjustment_Sub_Showroom_Current=0;

                    newProduct.Production_Stock_price = 0;
                    newProduct.Production_Current_price = 0;
                    newProduct.ProductionAdjustment_Stock_price = 0;
                    newProduct.ProductionAdjustment_Current_price = 0;
                    newProduct.Purchase_Stock_price = 0;
                    newProduct.Purchase_Current_price = 0;
                    newProduct.PurchaseAdjustment_Stock_price = 0;
                    newProduct.PurchaseAdjustment_Current_price = 0;
                    newProduct.PurchaseReturn_Stock_price = 0;
                    newProduct.PurchaseReturn_Current_price = 0;
                    newProduct.Lost_HO_Stock_price = 0;
                    newProduct.Lost_HO_Current_price = 0;
                    newProduct.Wastage_HO_Stock_price = 0;
                    newProduct.Wastage_HO_Current_price = 0;
                    newProduct.Issue_Wash_HO_Stock_price = 0;
                    newProduct.Issue_Wash_HO_Current_price = 0;
                    newProduct.Back_After_Wash_Stock_price = 0;
                    newProduct.Back_After_Wash_Current_price = 0;
                    newProduct.Issue_for_Repair_Stock_price = 0;
                    newProduct.Issue_for_Repair_Current_price = 0;
                    newProduct.Back_after_repair_Stock_price = 0;
                    newProduct.Back_after_repair_Current_price = 0;
                    newProduct.Issue_to_Showroom_Stock_price = 0;
                    newProduct.Issue_to_Showroom_Current_price = 0;
                    newProduct.Issue_Return_HO_Stock_price = 0;
                    newProduct.Issue_Return_HO_Current_price = 0;
                    newProduct.Send_another_Showroom_Stock_price = 0;
                    newProduct.Send_another_Showroom_Current_price = 0;
                    newProduct.Received_another_Showroom_Stock_price = 0;
                    newProduct.Received_another_Showroom_Current_price = 0;
                    newProduct.Sales_Stock_price = 0;
                    newProduct.Sales_Current_price = 0;
                    newProduct.Sales_return_Stock_price = 0;
                    newProduct.Sales_return_Current_price = 0;
                    newProduct.Gift_HO_Stock_price = 0;
                    newProduct.Gift_HO_Current_price = 0;
                    newProduct.Gift_Showroom_Stock_price = 0;
                    newProduct.Gift_Showroom_Current_price = 0;
                    newProduct.Send_wash_Showroom_Stock_price = 0;
                    newProduct.Send_wash_Showroom_Current_price = 0;
                    newProduct.Received_wash_Showroom_Stock_price = 0;
                    newProduct.Received_wash_Showroom_Current_price = 0;
                    newProduct.Send_repair_Showroom_Stock_price = 0;
                    newProduct.Send_repair_Showroom_Current_price = 0;
                    newProduct.Received_repair_Showroom_Stock_price = 0;
                    newProduct.Received_repair_Showroom_Current_price = 0;
                    newProduct.Lost_Showroom_Stock_price = 0;
                    newProduct.Lost_Showroom_Current_price = 0;
                    newProduct.Wastage_Showroom_Stock_price = 0;
                    newProduct.Wastage_Showroom_Current_price = 0;
                    newProduct.Adjustment_Add_Showroom_Stock_price = 0;
                    newProduct.Adjustment_Add_Showroom_Current_price = 0;
                    newProduct.Adjustment_Sub_Showroom_Stock_price = 0;
                    newProduct.Adjustment_Sub_Showroom_Current_price = 0;

                    Products.Add(newProduct);
                }
            }

        }

        sql = "";
        foreach (Pos_Product pos_Product in Products)
        {
            sql += @"select top 1 Inv_QuantityUnit.QuantityUnitName,'" +pos_Product.ProductID.ToString()+ @"' as ProductID from Pos_Product
                        inner join Inv_QuantityUnit on Inv_QuantityUnit.Inv_QuantityUnitID= Pos_Product.Inv_QuantityUnitID
                        where Pos_Product.Pos_ProductID=" + pos_Product.ProductID.ToString() + ";";
        }

        DataSet ds2 = CommonManager.SQLExec(sql);

        for (int i = 0; i < Products.Count; i++)
        {
            if (ds2.Tables[i].Rows[0]["ProductID"].ToString() == Products[i].Pos_ProductID.ToString())
            {
                Products[i].Inv_QuantityUniteName = ds2.Tables[i].Rows[0]["QuantityUnitName"].ToString();
            }
        }


        foreach (Pos_Product pos_Product in Products)
        {
            //Issue_Wash_HO_Stock
            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
            //    {
            pos_Product.Issue_Wash_HO_Stock = 0;// decimal.Parse(dr["Qty"].ToString());
            pos_Product.Issue_Wash_HO_Stock_price = 0;// decimal.Parse(dr["QtyPrice"].ToString());
            //        break;
            //    }
            //}

            //Issue_Wash_HO_Current
            //foreach (DataRow dr in ds.Tables[1].Rows)
            //{
            //    if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
            //    {
            pos_Product.Issue_Wash_HO_Current = 0;// decimal.Parse(dr["Qty"].ToString());
            pos_Product.Issue_Wash_HO_Current_price = 0;//decimal.Parse(dr["QtyPrice"].ToString());
            //        break;
            //    }
            //}

            //Back_After_Wash_Stock
            //foreach (DataRow dr in ds.Tables[2].Rows)
            //{
            //    if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
            //    {
            pos_Product.Back_After_Wash_Stock = 0;//decimal.Parse(dr["Qty"].ToString());
            pos_Product.Back_After_Wash_Stock_price = 0;//decimal.Parse(dr["QtyPrice"].ToString());
            //        break;
            //    }
            //}

            //Back_After_Wash_Current
            //foreach (DataRow dr in ds.Tables[1].Rows)
            //{
            //    if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
            //    {
            pos_Product.Back_After_Wash_Current = 0;//decimal.Parse(dr["Qty"].ToString());
            pos_Product.Back_After_Wash_Current_price = 0;// decimal.Parse(dr["QtyPrice"].ToString());
            //        break;
            //    }
            //}

            //Issue_for_Repair_Stock
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Issue_for_Repair_Stock = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Issue_for_Repair_Stock_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }

            //Issue_for_Repair_Current
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Issue_for_Repair_Current = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Issue_for_Repair_Current_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }

            //Back_after_repair_Stock
            foreach (DataRow dr in ds.Tables[2].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Back_after_repair_Stock = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Back_after_repair_Stock_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }

            //Back_after_repair_Current
            foreach (DataRow dr in ds.Tables[3].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Back_after_repair_Current = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Back_after_repair_Current_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }

            //Issue_to_Showroom_Stock
            foreach (DataRow dr in ds.Tables[4].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Issue_to_Showroom_Stock = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Issue_to_Showroom_Stock_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }

            //Issue_to_Showroom_Current
            foreach (DataRow dr in ds.Tables[5].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Issue_to_Showroom_Current = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Issue_to_Showroom_Current_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }


            //Issue_Return_HO_Stock
            foreach (DataRow dr in ds.Tables[6].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Issue_Return_HO_Stock = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Issue_Return_HO_Stock_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }

            //Issue_Return_HO_Current
            foreach (DataRow dr in ds.Tables[7].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Issue_Return_HO_Current = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Issue_Return_HO_Current_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }


            //Send_another_Showroom_Stock
            foreach (DataRow dr in ds.Tables[8].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Send_another_Showroom_Stock = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Send_another_Showroom_Stock_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }

            //Send_another_Showroom_Current
            foreach (DataRow dr in ds.Tables[9].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Send_another_Showroom_Current = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Send_another_Showroom_Current_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }


            //Received_another_Showroom_Stock
            foreach (DataRow dr in ds.Tables[10].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Received_another_Showroom_Stock = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Received_another_Showroom_Stock_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }

            //Received_another_Showroom_Current
            foreach (DataRow dr in ds.Tables[11].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Received_another_Showroom_Current = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Received_another_Showroom_Current_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }


            //Sales_Stock
            foreach (DataRow dr in ds.Tables[12].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Sales_Stock = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Sales_Stock_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }

            //Sales_Current
            foreach (DataRow dr in ds.Tables[13].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Sales_Current = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Sales_Current_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }


            //Sales_return_Stock
            foreach (DataRow dr in ds.Tables[14].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Sales_return_Stock = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Sales_return_Stock_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }

            //Sales_return_Current
            foreach (DataRow dr in ds.Tables[15].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Sales_return_Current = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Sales_return_Current_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }


            //Gift_Showroom_Stock
            foreach (DataRow dr in ds.Tables[16].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Gift_Showroom_Stock = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Gift_Showroom_Stock_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }

            //Gift_Showroom_Current
            foreach (DataRow dr in ds.Tables[17].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Gift_Showroom_Current = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Gift_Showroom_Current_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }



            //Send_wash_Showroom_Stock
            foreach (DataRow dr in ds.Tables[18].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Send_wash_Showroom_Stock = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Send_wash_Showroom_Stock_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }

            //Send_wash_Showroom_Current
            foreach (DataRow dr in ds.Tables[19].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Send_wash_Showroom_Current = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Send_wash_Showroom_Current_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }



            //Received_wash_Showroom_Stock
            foreach (DataRow dr in ds.Tables[20].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Received_wash_Showroom_Stock = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Received_wash_Showroom_Stock_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }

            //Received_wash_Showroom_Current
            foreach (DataRow dr in ds.Tables[21].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Received_wash_Showroom_Current = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Received_wash_Showroom_Current_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }



            //Send_repair_Showroom_Stock
            foreach (DataRow dr in ds.Tables[22].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Send_repair_Showroom_Stock = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Send_repair_Showroom_Stock_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }

            //Send_repair_Showroom_Current
            foreach (DataRow dr in ds.Tables[23].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Send_repair_Showroom_Current = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Send_repair_Showroom_Current_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }



            //Received_repair_Showroom_Stock
            foreach (DataRow dr in ds.Tables[24].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Received_repair_Showroom_Stock = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Received_repair_Showroom_Stock_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }

            //Received_repair_Showroom_Current
            foreach (DataRow dr in ds.Tables[25].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Received_repair_Showroom_Current = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Received_repair_Showroom_Current_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }


            //Lost_Showroom_Stock
            foreach (DataRow dr in ds.Tables[26].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Lost_Showroom_Stock = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Lost_Showroom_Stock_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }

            //Lost_Showroom_Current
            foreach (DataRow dr in ds.Tables[27].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Lost_Showroom_Current = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Lost_Showroom_Current_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }



            //Wastage_Showroom_Stock
            foreach (DataRow dr in ds.Tables[28].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Wastage_Showroom_Stock = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Wastage_Showroom_Stock_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }

            //Wastage_Showroom_Current
            foreach (DataRow dr in ds.Tables[29].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Wastage_Showroom_Current = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Wastage_Showroom_Current_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }



            //Adjustment_Add_Showroom_Stock
            foreach (DataRow dr in ds.Tables[30].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Adjustment_Add_Showroom_Stock = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Adjustment_Add_Showroom_Stock_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }

            //Adjustment_Add_Showroom_Current
            foreach (DataRow dr in ds.Tables[31].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Adjustment_Add_Showroom_Current = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Adjustment_Add_Showroom_Current_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }


            //Adjustment_Sub_Showroom_Stock
            foreach (DataRow dr in ds.Tables[32].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Adjustment_Sub_Showroom_Stock = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Adjustment_Sub_Showroom_Stock_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }

            //Adjustment_Sub_Showroom_Current
            foreach (DataRow dr in ds.Tables[33].Rows)
            {
                if (pos_Product.ProductID.ToString() == dr["ProductID"].ToString())
                {
                    pos_Product.Adjustment_Sub_Showroom_Current = decimal.Parse(dr["Qty"].ToString());
                    pos_Product.Adjustment_Sub_Showroom_Current_price = decimal.Parse(dr["QtyPrice"].ToString());
                    break;
                }
            }

            /*
                Production
                Production Adjustment
                Lost in HO
                Wastage in HO
                Issue for Wash from HO
                Back after wash
                Issue for Repair
                Back after repair
                Issue to Showroom
                Issue Return to HO
                Gift in HO
                Adjustment(+) in Showroom
                Adjustment(-) in Showroom
                Purchase
                Purchase Adjustment
                Purchase Return
             
             */
            //Opining Stock
            pos_Product.OpiningStock =
                pos_Product.Issue_Wash_HO_Stock
                -
                pos_Product.Back_After_Wash_Stock
                +
                pos_Product.Issue_for_Repair_Stock
                -
                pos_Product.Back_after_repair_Stock
                +
                pos_Product.Issue_to_Showroom_Stock
                -
                pos_Product.Issue_Return_HO_Stock
                -
                pos_Product.Send_another_Showroom_Stock
                +
                pos_Product.Received_another_Showroom_Stock
                -
                pos_Product.Sales_Stock
                +
                pos_Product.Sales_return_Stock
                -
                pos_Product.Gift_Showroom_Stock
                -
                pos_Product.Send_wash_Showroom_Stock
                +
                pos_Product.Received_wash_Showroom_Stock
                -
                pos_Product.Send_repair_Showroom_Stock
                +
                pos_Product.Received_repair_Showroom_Stock
                -
                pos_Product.Lost_Showroom_Stock
                -
                pos_Product.Wastage_Showroom_Stock
                +
                pos_Product.Adjustment_Add_Showroom_Stock
                -
                pos_Product.Adjustment_Sub_Showroom_Stock;
 
            //Stock in hand
            pos_Product.StockInHand =
                pos_Product.OpiningStock
                +
                pos_Product.Issue_Wash_HO_Current
                -
                pos_Product.Back_After_Wash_Current
                +
                pos_Product.Issue_for_Repair_Current
                -
                pos_Product.Back_after_repair_Current
                +
                pos_Product.Issue_to_Showroom_Current
                -
                pos_Product.Issue_Return_HO_Current
                -
                pos_Product.Send_another_Showroom_Current
                +
                pos_Product.Received_another_Showroom_Current
                -
                pos_Product.Sales_Current
                +
                pos_Product.Sales_return_Current
                -
                pos_Product.Gift_Showroom_Current
                -
                pos_Product.Send_wash_Showroom_Current
                +
                pos_Product.Received_wash_Showroom_Current
                -
                pos_Product.Send_repair_Showroom_Current
                +
                pos_Product.Received_repair_Showroom_Current
                -
                pos_Product.Lost_Showroom_Current
                -
                pos_Product.Wastage_Showroom_Current
                +
                pos_Product.Adjustment_Add_Showroom_Current
                -
                pos_Product.Adjustment_Sub_Showroom_Current;

            //Opining Stock price
            pos_Product.OpiningStockPrice =
                pos_Product.Issue_Wash_HO_Stock_price
                -
                pos_Product.Back_After_Wash_Stock_price
                +
                pos_Product.Issue_for_Repair_Stock_price
                -
                pos_Product.Back_after_repair_Stock_price
                +
                pos_Product.Issue_to_Showroom_Stock_price
                -
                pos_Product.Issue_Return_HO_Stock_price
                -
                pos_Product.Send_another_Showroom_Stock_price
                +
                pos_Product.Received_another_Showroom_Stock_price
                -
                pos_Product.Sales_Stock_price
                +
                pos_Product.Sales_return_Stock_price
                -
                pos_Product.Gift_Showroom_Stock_price
                -
                pos_Product.Send_wash_Showroom_Stock_price
                +
                pos_Product.Received_wash_Showroom_Stock_price
                -
                pos_Product.Send_repair_Showroom_Stock_price
                +
                pos_Product.Received_repair_Showroom_Stock_price
                -
                pos_Product.Lost_Showroom_Stock_price
                -
                pos_Product.Wastage_Showroom_Stock_price
                +
                pos_Product.Adjustment_Add_Showroom_Stock_price
                -
                pos_Product.Adjustment_Sub_Showroom_Stock_price;

            //Stock in hand
            pos_Product.StockInHandPrice =
                pos_Product.OpiningStockPrice
                +
                pos_Product.Issue_Wash_HO_Current_price
                -
                pos_Product.Back_After_Wash_Current_price
                +
                pos_Product.Issue_for_Repair_Current_price
                -
                pos_Product.Back_after_repair_Current_price
                +
                pos_Product.Issue_to_Showroom_Current_price
                -
                pos_Product.Issue_Return_HO_Current_price
                -
                pos_Product.Send_another_Showroom_Current_price
                +
                pos_Product.Received_another_Showroom_Current_price
                -
                pos_Product.Sales_Current_price
                +
                pos_Product.Sales_return_Current_price
                -
                pos_Product.Gift_Showroom_Current_price
                -
                pos_Product.Send_wash_Showroom_Current_price
                +
                pos_Product.Received_wash_Showroom_Current_price
                -
                pos_Product.Send_repair_Showroom_Current_price
                +
                pos_Product.Received_repair_Showroom_Current_price
                -
                pos_Product.Lost_Showroom_Current_price
                -
                pos_Product.Wastage_Showroom_Current_price
                +
                pos_Product.Adjustment_Add_Showroom_Current_price
                -
                pos_Product.Adjustment_Sub_Showroom_Current_price;


            
        }


        decimal t_opining_price = 0;
        decimal t_StockInhandPrice = 0;

        decimal t_Production_Current_price = 0;
        decimal t_ProductionAdjustment_Current_price = 0;
        decimal t_Purchase_Current_price = 0;
        decimal t_PurchaseAdjustment_Current_price = 0;
        decimal t_PurchaseReturn_Current_price = 0;
        decimal t_Lost_HO_Current_price = 0;
        decimal t_Wastage_HO_Current_price = 0;
        decimal t_Issue_Wash_HO_Current_price = 0;
        decimal t_Back_After_Wash_Current_price = 0;
        decimal t_Issue_for_Repair_Current_price = 0;
        decimal t_Back_after_repair_Current_price = 0;
        decimal t_Issue_to_Showroom_Current_price = 0;
        decimal t_Issue_Return_HO_Current_price = 0;
        decimal t_Send_another_Showroom_Current_price = 0;
        decimal t_Received_another_Showroom_Current_price = 0;
        decimal t_Sales_Current_price = 0;
        decimal t_Sales_return_Current_price = 0;
        decimal t_Gift_HO_Current_price = 0;
        decimal t_Gift_Showroom_Current_price = 0;
        decimal t_Send_wash_Showroom_Current_price = 0;
        decimal t_Received_wash_Showroom_Current_price = 0;
        decimal t_Send_repair_Showroom_Current_price = 0;
        decimal t_Received_repair_Showroom_Current_price = 0;
        decimal t_Lost_Showroom_Current_price = 0;
        decimal t_Wastage_Showroom_Current_price = 0;
        decimal t_Adjustment_Add_Showroom_Current_price = 0;
        decimal t_Adjustment_Sub_Showroom_Current_price = 0;


        decimal t_opining = 0;
        decimal t_StockInhand = 0;

        decimal t_Production_Current = 0;
        decimal t_ProductionAdjustment_Current = 0;
        decimal t_Purchase_Current = 0;
        decimal t_PurchaseAdjustment_Current = 0;
        decimal t_PurchaseReturn_Current = 0;
        decimal t_Lost_HO_Current = 0;
        decimal t_Wastage_HO_Current = 0;
        decimal t_Issue_Wash_HO_Current = 0;
        decimal t_Back_After_Wash_Current = 0;
        decimal t_Issue_for_Repair_Current = 0;
        decimal t_Back_after_repair_Current = 0;
        decimal t_Issue_to_Showroom_Current = 0;
        decimal t_Issue_Return_HO_Current = 0;
        decimal t_Send_another_Showroom_Current = 0;
        decimal t_Received_another_Showroom_Current = 0;
        decimal t_Sales_Current = 0;
        decimal t_Sales_return_Current = 0;
        decimal t_Gift_HO_Current = 0;
        decimal t_Gift_Showroom_Current = 0;
        decimal t_Send_wash_Showroom_Current = 0;
        decimal t_Received_wash_Showroom_Current = 0;
        decimal t_Send_repair_Showroom_Current = 0;
        decimal t_Received_repair_Showroom_Current = 0;
        decimal t_Lost_Showroom_Current = 0;
        decimal t_Wastage_Showroom_Current = 0;
        decimal t_Adjustment_Add_Showroom_Current = 0;
        decimal t_Adjustment_Sub_Showroom_Current = 0;


        decimal t_opining_subtotal = 0;
        decimal t_StockInhand_subtotal = 0;

        decimal t_Production_Current_subtotal = 0;
        decimal t_ProductionAdjustment_Current_subtotal = 0;
        decimal t_Purchase_Current_subtotal = 0;
        decimal t_PurchaseAdjustment_Current_subtotal = 0;
        decimal t_PurchaseReturn_Current_subtotal = 0;
        decimal t_Lost_HO_Current_subtotal = 0;
        decimal t_Wastage_HO_Current_subtotal = 0;
        decimal t_Issue_Wash_HO_Current_subtotal = 0;
        decimal t_Back_After_Wash_Current_subtotal = 0;
        decimal t_Issue_for_Repair_Current_subtotal = 0;
        decimal t_Back_after_repair_Current_subtotal = 0;
        decimal t_Issue_to_Showroom_Current_subtotal = 0;
        decimal t_Issue_Return_HO_Current_subtotal = 0;
        decimal t_Send_another_Showroom_Current_subtotal = 0;
        decimal t_Received_another_Showroom_Current_subtotal = 0;
        decimal t_Sales_Current_subtotal = 0;
        decimal t_Sales_return_Current_subtotal = 0;
        decimal t_Gift_HO_Current_subtotal = 0;
        decimal t_Gift_Showroom_Current_subtotal = 0;
        decimal t_Send_wash_Showroom_Current_subtotal = 0;
        decimal t_Received_wash_Showroom_Current_subtotal = 0;
        decimal t_Send_repair_Showroom_Current_subtotal = 0;
        decimal t_Received_repair_Showroom_Current_subtotal = 0;
        decimal t_Lost_Showroom_Current_subtotal = 0;
        decimal t_Wastage_Showroom_Current_subtotal = 0;
        decimal t_Adjustment_Add_Showroom_Current_subtotal = 0;
        decimal t_Adjustment_Sub_Showroom_Current_subtotal = 0;

        decimal t_opining_price_subtotal = 0;
        decimal t_StockInhand_price_subtotal = 0;

        decimal t_Production_Current_price_subtotal = 0;
        decimal t_ProductionAdjustment_Current_price_subtotal = 0;
        decimal t_Purchase_Current_price_subtotal = 0;
        decimal t_PurchaseAdjustment_Current_price_subtotal = 0;
        decimal t_PurchaseReturn_Current_price_subtotal = 0;
        decimal t_Lost_HO_Current_price_subtotal = 0;
        decimal t_Wastage_HO_Current_price_subtotal = 0;
        decimal t_Issue_Wash_HO_Current_price_subtotal = 0;
        decimal t_Back_After_Wash_Current_price_subtotal = 0;
        decimal t_Issue_for_Repair_Current_price_subtotal = 0;
        decimal t_Back_after_repair_Current_price_subtotal = 0;
        decimal t_Issue_to_Showroom_Current_price_subtotal = 0;
        decimal t_Issue_Return_HO_Current_price_subtotal = 0;
        decimal t_Send_another_Showroom_Current_price_subtotal = 0;
        decimal t_Received_another_Showroom_Current_price_subtotal = 0;
        decimal t_Sales_Current_price_subtotal = 0;
        decimal t_Sales_return_Current_price_subtotal = 0;
        decimal t_Gift_HO_Current_price_subtotal = 0;
        decimal t_Gift_Showroom_Current_price_subtotal = 0;
        decimal t_Send_wash_Showroom_Current_price_subtotal = 0;
        decimal t_Received_wash_Showroom_Current_price_subtotal = 0;
        decimal t_Send_repair_Showroom_Current_price_subtotal = 0;
        decimal t_Received_repair_Showroom_Current_price_subtotal = 0;
        decimal t_Lost_Showroom_Current_price_subtotal = 0;
        decimal t_Wastage_Showroom_Current_price_subtotal = 0;
        decimal t_Adjustment_Add_Showroom_Current_price_subtotal = 0;
        decimal t_Adjustment_Sub_Showroom_Current_price_subtotal = 0;



        int serialNo = 1;

        string htmlTable = @"<h3>" + ds.Tables[34].Rows[0][0].ToString() + @"</h3> <table id='itemList_tbl' style='border:1px solid black;width:100%;' cellpadding='0' cellspacing='0'>
                        <tr id='tableHeader'>
                            <td  style='border-left:0px;'>S/L</td>
                            <td>Product Code</td>
                            <td>Product Name</td>
                            <td>Unit</td>
                            <td>Opining</td>
                            ";
            foreach (Pos_TransactionType type in allTransactionType)
            {
                try
                {
                    if (int.Parse(type.ShowRoomFormula) != 0)
                    {
                        if(type.ExtraField3.Split('-')[1]=="1")
                        htmlTable += @"<td>" + type.TransactionTypeName.Replace(" ", "<br/>") + "</td>";
                    }
                }
                catch(Exception ex)
                { }
            }

            htmlTable += @"<td>Stock<br/>Quantity</td>
                            <td>Stock<br/>Amount</td>
                        </tr>";
            string lastProductName="";
            foreach (Pos_Product pos_Product in Products)
            {
                if (lastProductName != "" && lastProductName != pos_Product.ProductName)
                {
                    htmlTable += @"<tr class='subtotalRow'>
                            <td>&nbsp;</td>
                            <td colspan='3'>SubTotal Qty</td>
                            <td style='text-align:right;'>" + t_opining_subtotal.ToString("0,0.00") + @"</td>";

                    //htmlTable += @"<td style='text-align:right;'>" +t_Issue_Wash_HO_Current_price_subtotal.ToString("0,0.00") + "</td>";//
                    //htmlTable += @"<td style='text-align:right;'>" +t_Back_After_Wash_Current_price_subtotal.ToString("0,0.00") + "</td>";//
                    //htmlTable += @"<td style='text-align:right;'>" +t_Issue_for_Repair_Current_price_subtotal.ToString("0,0.00") + "</td>";//
                    //htmlTable += @"<td style='text-align:right;'>" +t_Back_after_repair_Current_price_subtotal.ToString("0,0.00") + "</td>";//
                    htmlTable += @"<td style='text-align:right;'>" + t_Issue_to_Showroom_Current_subtotal.ToString("0,0.00") + "</td>";//
                    htmlTable += @"<td style='text-align:right;'>" + t_Issue_Return_HO_Current_subtotal.ToString("0,0.00") + "</td>";
                    htmlTable += @"<td style='text-align:right;'>" + t_Send_another_Showroom_Current_subtotal.ToString("0,0.00") + "</td>";
                    htmlTable += @"<td style='text-align:right;'>" + t_Received_another_Showroom_Current_subtotal.ToString("0,0.00") + "</td>";
                    htmlTable += @"<td style='text-align:right;'>" + t_Sales_Current_subtotal.ToString("0,0.00") + "</td>";
                    htmlTable += @"<td style='text-align:right;'>" + t_Sales_return_Current_subtotal.ToString("0,0.00") + "</td>";
                    //htmlTable += @"<td style='text-align:right;'>" +t_Gift_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
                    //htmlTable += @"<td style='text-align:right;'>" +t_Send_wash_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
                    //htmlTable += @"<td style='text-align:right;'>" +t_Received_wash_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
                    //htmlTable += @"<td style='text-align:right;'>" +t_Send_repair_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
                    //htmlTable += @"<td style='text-align:right;'>" +t_Received_repair_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
                    //htmlTable += @"<td style='text-align:right;'>" +t_Lost_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
                    //htmlTable += @"<td style='text-align:right;'>" +t_Wastage_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
                    htmlTable += @"<td style='text-align:right;'>" + t_Adjustment_Add_Showroom_Current_subtotal.ToString("0,0.00") + "</td>";
                    htmlTable += @"<td style='text-align:right;'>" + t_Adjustment_Sub_Showroom_Current_subtotal.ToString("0,0.00") + "</td>";

                    htmlTable += @"<td style='text-align:right;'>" + t_StockInhand_subtotal.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + t_StockInhand_price_subtotal.ToString("0,0.00") + @"</td>
                        </tr>";


                    htmlTable += @"<tr class='subtotalRow_price'>
                            <td>&nbsp;</td>
                            <td colspan='3'>SubTotal Price</td>
                            <td style='text-align:right;'>" + t_opining_price_subtotal.ToString("0,0.00") + @"</td>";

                    //htmlTable += @"<td style='text-align:right;'>" +t_Issue_Wash_HO_Current_price_subtotal.ToString("0,0.00") + "</td>";//
                    //htmlTable += @"<td style='text-align:right;'>" +t_Back_After_Wash_Current_price_subtotal.ToString("0,0.00") + "</td>";//
                    //htmlTable += @"<td style='text-align:right;'>" +t_Issue_for_Repair_Current_price_subtotal.ToString("0,0.00") + "</td>";//
                    //htmlTable += @"<td style='text-align:right;'>" +t_Back_after_repair_Current_price_subtotal.ToString("0,0.00") + "</td>";//
                    htmlTable += @"<td style='text-align:right;'>" + t_Issue_to_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";//
                    htmlTable += @"<td style='text-align:right;'>" + t_Issue_Return_HO_Current_price_subtotal.ToString("0,0.00") + "</td>";
                    htmlTable += @"<td style='text-align:right;'>" + t_Send_another_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
                    htmlTable += @"<td style='text-align:right;'>" + t_Received_another_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
                    htmlTable += @"<td style='text-align:right;'>" + t_Sales_Current_price_subtotal.ToString("0,0.00") + "</td>";
                    htmlTable += @"<td style='text-align:right;'>" + t_Sales_return_Current_price_subtotal.ToString("0,0.00") + "</td>";
                    //htmlTable += @"<td style='text-align:right;'>" +t_Gift_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
                    //htmlTable += @"<td style='text-align:right;'>" +t_Send_wash_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
                    //htmlTable += @"<td style='text-align:right;'>" +t_Received_wash_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
                    //htmlTable += @"<td style='text-align:right;'>" +t_Send_repair_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
                    //htmlTable += @"<td style='text-align:right;'>" +t_Received_repair_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
                    //htmlTable += @"<td style='text-align:right;'>" +t_Lost_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
                    //htmlTable += @"<td style='text-align:right;'>" +t_Wastage_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
                    htmlTable += @"<td style='text-align:right;'>" + t_Adjustment_Add_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
                    htmlTable += @"<td style='text-align:right;'>" + t_Adjustment_Sub_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";

                    htmlTable += @"<td style='text-align:right;'>" + t_StockInhand_subtotal.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + t_StockInhand_price_subtotal.ToString("0,0.00") + @"</td>
                        </tr>";

                    t_opining_subtotal = 0;// pos_Product.OpiningStock;
                    t_opining_price_subtotal = 0;// pos_Product.OpiningStockPrice;
                    t_StockInhand_subtotal = 0;// pos_Product.StockInHand;
                    t_StockInhand_price_subtotal = 0;// pos_Product.StockInHandPrice;


                    t_Issue_Wash_HO_Current_subtotal = 0;// pos_Product.Issue_Wash_HO_Current;
                    t_Back_After_Wash_Current_subtotal = 0;// pos_Product.Back_After_Wash_Current;
                    t_Issue_for_Repair_Current_subtotal = 0;// pos_Product.Issue_for_Repair_Current;
                    t_Back_after_repair_Current_subtotal = 0;// pos_Product.Back_after_repair_Current;
                    t_Issue_to_Showroom_Current_subtotal = 0;// pos_Product.Issue_to_Showroom_Current;
                    t_Issue_Return_HO_Current_subtotal = 0;// pos_Product.Issue_Return_HO_Current;
                    t_Send_another_Showroom_Current_subtotal = 0;// pos_Product.Send_another_Showroom_Current;
                    t_Received_another_Showroom_Current_subtotal = 0;// pos_Product.Received_another_Showroom_Current;
                    t_Sales_Current_subtotal = 0;// pos_Product.Sales_Current;
                    t_Sales_return_Current_subtotal = 0;// pos_Product.Sales_return_Current;
                    t_Gift_Showroom_Current_subtotal = 0;// pos_Product.Gift_Showroom_Current;
                    t_Send_wash_Showroom_Current_subtotal = 0;// pos_Product.Send_wash_Showroom_Current;
                    t_Received_wash_Showroom_Current_subtotal = 0;// pos_Product.Received_wash_Showroom_Current;
                    t_Send_repair_Showroom_Current_subtotal = 0;// pos_Product.Send_repair_Showroom_Current;
                    t_Received_repair_Showroom_Current_subtotal = 0;// pos_Product.Received_repair_Showroom_Current;
                    t_Lost_Showroom_Current_subtotal = 0;// pos_Product.Lost_Showroom_Current;
                    t_Wastage_Showroom_Current_subtotal = 0;// pos_Product.Wastage_Showroom_Current;
                    t_Adjustment_Add_Showroom_Current_subtotal = 0;// pos_Product.Adjustment_Add_Showroom_Current;
                    t_Adjustment_Sub_Showroom_Current_subtotal = 0;// pos_Product.Adjustment_Sub_Showroom_Current;


                    t_Issue_Wash_HO_Current_price_subtotal = 0;// pos_Product.Issue_Wash_HO_Current_price;
                    t_Back_After_Wash_Current_price_subtotal = 0;// pos_Product.Back_After_Wash_Current_price;
                    t_Issue_for_Repair_Current_price_subtotal = 0;// pos_Product.Issue_for_Repair_Current_price;
                    t_Back_after_repair_Current_price_subtotal = 0;// pos_Product.Back_after_repair_Current_price;
                    t_Issue_to_Showroom_Current_price_subtotal = 0;// pos_Product.Issue_to_Showroom_Current_price;
                    t_Issue_Return_HO_Current_price_subtotal = 0;// pos_Product.Issue_Return_HO_Current_price;
                    t_Send_another_Showroom_Current_price_subtotal = 0;// pos_Product.Send_another_Showroom_Current_price;
                    t_Received_another_Showroom_Current_price_subtotal = 0;// pos_Product.Received_another_Showroom_Current_price;
                    t_Sales_Current_price_subtotal = 0;// pos_Product.Sales_Current_price;
                    t_Sales_return_Current_price_subtotal = 0;// pos_Product.Sales_return_Current_price;
                    t_Gift_Showroom_Current_price_subtotal = 0;// pos_Product.Gift_Showroom_Current_price;
                    t_Send_wash_Showroom_Current_price_subtotal = 0;// pos_Product.Send_wash_Showroom_Current_price;
                    t_Received_wash_Showroom_Current_price_subtotal = 0;// pos_Product.Received_wash_Showroom_Current_price;
                    t_Send_repair_Showroom_Current_price_subtotal = 0;// pos_Product.Send_repair_Showroom_Current_price;
                    t_Received_repair_Showroom_Current_price_subtotal = 0;// pos_Product.Received_repair_Showroom_Current_price;
                    t_Lost_Showroom_Current_price_subtotal = 0;// pos_Product.Lost_Showroom_Current_price;
                    t_Wastage_Showroom_Current_price_subtotal = 0;// pos_Product.Wastage_Showroom_Current_price;
                    t_Adjustment_Add_Showroom_Current_price_subtotal = 0;// pos_Product.Adjustment_Add_Showroom_Current_price;
                    t_Adjustment_Sub_Showroom_Current_price_subtotal = 0;// pos_Product.Adjustment_Sub_Showroom_Current_price;
                    lastProductName = pos_Product.ProductName;
                }
                else
                {
                    lastProductName = pos_Product.ProductName;

                    t_opining_subtotal += pos_Product.OpiningStock;
                    t_opining_price_subtotal += pos_Product.OpiningStockPrice;
                    t_StockInhand_subtotal += pos_Product.StockInHand;
                    t_StockInhand_price_subtotal += pos_Product.StockInHandPrice;


                    t_Issue_Wash_HO_Current_subtotal += pos_Product.Issue_Wash_HO_Current;
                    t_Back_After_Wash_Current_subtotal += pos_Product.Back_After_Wash_Current;
                    t_Issue_for_Repair_Current_subtotal += pos_Product.Issue_for_Repair_Current;
                    t_Back_after_repair_Current_subtotal += pos_Product.Back_after_repair_Current;
                    t_Issue_to_Showroom_Current_subtotal += pos_Product.Issue_to_Showroom_Current;
                    t_Issue_Return_HO_Current_subtotal += pos_Product.Issue_Return_HO_Current;
                    t_Send_another_Showroom_Current_subtotal += pos_Product.Send_another_Showroom_Current;
                    t_Received_another_Showroom_Current_subtotal += pos_Product.Received_another_Showroom_Current;
                    t_Sales_Current_subtotal += pos_Product.Sales_Current;
                    t_Sales_return_Current_subtotal += pos_Product.Sales_return_Current;
                    t_Gift_Showroom_Current_subtotal += pos_Product.Gift_Showroom_Current;
                    t_Send_wash_Showroom_Current_subtotal += pos_Product.Send_wash_Showroom_Current;
                    t_Received_wash_Showroom_Current_subtotal += pos_Product.Received_wash_Showroom_Current;
                    t_Send_repair_Showroom_Current_subtotal += pos_Product.Send_repair_Showroom_Current;
                    t_Received_repair_Showroom_Current_subtotal += pos_Product.Received_repair_Showroom_Current;
                    t_Lost_Showroom_Current_subtotal += pos_Product.Lost_Showroom_Current;
                    t_Wastage_Showroom_Current_subtotal += pos_Product.Wastage_Showroom_Current;
                    t_Adjustment_Add_Showroom_Current_subtotal += pos_Product.Adjustment_Add_Showroom_Current;
                    t_Adjustment_Sub_Showroom_Current_subtotal += pos_Product.Adjustment_Sub_Showroom_Current;


                    t_Issue_Wash_HO_Current_price_subtotal += pos_Product.Issue_Wash_HO_Current_price;
                    t_Back_After_Wash_Current_price_subtotal += pos_Product.Back_After_Wash_Current_price;
                    t_Issue_for_Repair_Current_price_subtotal += pos_Product.Issue_for_Repair_Current_price;
                    t_Back_after_repair_Current_price_subtotal += pos_Product.Back_after_repair_Current_price;
                    t_Issue_to_Showroom_Current_price_subtotal += pos_Product.Issue_to_Showroom_Current_price;
                    t_Issue_Return_HO_Current_price_subtotal += pos_Product.Issue_Return_HO_Current_price;
                    t_Send_another_Showroom_Current_price_subtotal += pos_Product.Send_another_Showroom_Current_price;
                    t_Received_another_Showroom_Current_price_subtotal += pos_Product.Received_another_Showroom_Current_price;
                    t_Sales_Current_price_subtotal += pos_Product.Sales_Current_price;
                    t_Sales_return_Current_price_subtotal += pos_Product.Sales_return_Current_price;
                    t_Gift_Showroom_Current_price_subtotal += pos_Product.Gift_Showroom_Current_price;
                    t_Send_wash_Showroom_Current_price_subtotal += pos_Product.Send_wash_Showroom_Current_price;
                    t_Received_wash_Showroom_Current_price_subtotal += pos_Product.Received_wash_Showroom_Current_price;
                    t_Send_repair_Showroom_Current_price_subtotal += pos_Product.Send_repair_Showroom_Current_price;
                    t_Received_repair_Showroom_Current_price_subtotal += pos_Product.Received_repair_Showroom_Current_price;
                    t_Lost_Showroom_Current_price_subtotal += pos_Product.Lost_Showroom_Current_price;
                    t_Wastage_Showroom_Current_price_subtotal += pos_Product.Wastage_Showroom_Current_price;
                    t_Adjustment_Add_Showroom_Current_price_subtotal += pos_Product.Adjustment_Add_Showroom_Current_price;
                    t_Adjustment_Sub_Showroom_Current_price_subtotal += pos_Product.Adjustment_Sub_Showroom_Current_price;

                }

                
                //if (pos_Product.PurchasedQuantity == 0 
                //    && pos_Product.PurchasedQuantityPrice == 0 &&
                //    pos_Product.ExtraField2 == "0"&&
                //    pos_Product.ExtraField4== "0"&&
                //    pos_Product.ExtraField6 == "0"&&
                //    pos_Product.ExtraField8 == "0"&&
                //    pos_Product.ExtraField10 == "0"&&
                //    pos_Product.ExtraFieldQuantity2==0 &&
                //    pos_Product.ExtraFieldQuantity4==0 
                //    )
                //{
                //    continue;
                //}

                htmlTable += @"<tr  class='itemCss'>
                            <td  style='border-left:0px;'>" + (serialNo++).ToString() + @"</td>
                            <td>" +pos_Product.BarCode + @"</td>
                            <td>" + pos_Product.ProductName + @"</td>
                            <td >" + pos_Product.Inv_QuantityUniteName + @"</td>
                            <td style='text-align:right;'>" + pos_Product.OpiningStock.ToString("0,0.00") + @"</td>";
                //htmlTable += @"<td style='text-align:right;'>" +pos_Product.Issue_Wash_HO_Current+"</td>";
                //htmlTable += @"<td style='text-align:right;'>" +pos_Product.Back_After_Wash_Current+"</td>";
                //htmlTable += @"<td style='text-align:right;'>" +pos_Product.Issue_for_Repair_Current+"</td>";
                //htmlTable += @"<td style='text-align:right;'>" +pos_Product.Back_after_repair_Current+"</td>";
                htmlTable += @"<td style='text-align:right;'>" +pos_Product.Issue_to_Showroom_Current+"</td>";
                htmlTable += @"<td style='text-align:right;'>" +pos_Product.Issue_Return_HO_Current+"</td>";
                htmlTable += @"<td style='text-align:right;'>" +pos_Product.Send_another_Showroom_Current+"</td>";
                htmlTable += @"<td style='text-align:right;'>" +pos_Product.Received_another_Showroom_Current+"</td>";
                htmlTable += @"<td style='text-align:right;'>" +pos_Product.Sales_Current+"</td>";
                htmlTable += @"<td style='text-align:right;'>" +pos_Product.Sales_return_Current+"</td>";
                //htmlTable += @"<td style='text-align:right;'>" +pos_Product.Gift_Showroom_Current+"</td>";
                //htmlTable += @"<td style='text-align:right;'>" +pos_Product.Send_wash_Showroom_Current+"</td>";
                //htmlTable += @"<td style='text-align:right;'>" +pos_Product.Received_wash_Showroom_Current+"</td>";
                //htmlTable += @"<td style='text-align:right;'>" +pos_Product.Send_repair_Showroom_Current+"</td>";
                //htmlTable += @"<td style='text-align:right;'>" +pos_Product.Received_repair_Showroom_Current+"</td>";
                //htmlTable += @"<td style='text-align:right;'>" +pos_Product.Lost_Showroom_Current+"</td>";
                //htmlTable += @"<td style='text-align:right;'>" +pos_Product.Wastage_Showroom_Current+"</td>";
                htmlTable += @"<td style='text-align:right;'>" +pos_Product.Adjustment_Add_Showroom_Current+"</td>";
                htmlTable += @"<td style='text-align:right;'>" +pos_Product.Adjustment_Sub_Showroom_Current+"</td>";

                htmlTable += @"<td style='text-align:right;'>" + pos_Product.StockInHand.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + pos_Product.StockInHandPrice.ToString("0,0.00") + @"</td>
                        </tr>";

                
                t_opining += pos_Product.OpiningStock;
                t_opining_price += pos_Product.OpiningStockPrice;
                t_StockInhand += pos_Product.StockInHand;
                t_StockInhandPrice += pos_Product.StockInHandPrice;


                t_Issue_Wash_HO_Current += pos_Product.Issue_Wash_HO_Current;
                t_Back_After_Wash_Current += pos_Product.Back_After_Wash_Current;
                t_Issue_for_Repair_Current += pos_Product.Issue_for_Repair_Current;
                t_Back_after_repair_Current += pos_Product.Back_after_repair_Current;
                t_Issue_to_Showroom_Current += pos_Product.Issue_to_Showroom_Current;
                t_Issue_Return_HO_Current += pos_Product.Issue_Return_HO_Current;
                t_Send_another_Showroom_Current += pos_Product.Send_another_Showroom_Current;
                t_Received_another_Showroom_Current += pos_Product.Received_another_Showroom_Current;
                t_Sales_Current += pos_Product.Sales_Current;
                t_Sales_return_Current += pos_Product.Sales_return_Current;
                t_Gift_Showroom_Current += pos_Product.Gift_Showroom_Current;
                t_Send_wash_Showroom_Current += pos_Product.Send_wash_Showroom_Current;
                t_Received_wash_Showroom_Current += pos_Product.Received_wash_Showroom_Current;
                t_Send_repair_Showroom_Current += pos_Product.Send_repair_Showroom_Current;
                t_Received_repair_Showroom_Current += pos_Product.Received_repair_Showroom_Current;
                t_Lost_Showroom_Current += pos_Product.Lost_Showroom_Current;
                t_Wastage_Showroom_Current += pos_Product.Wastage_Showroom_Current;
                t_Adjustment_Add_Showroom_Current += pos_Product.Adjustment_Add_Showroom_Current;
                t_Adjustment_Sub_Showroom_Current += pos_Product.Adjustment_Sub_Showroom_Current;


                t_Issue_Wash_HO_Current_price += pos_Product.Issue_Wash_HO_Current_price;
                t_Back_After_Wash_Current_price += pos_Product.Back_After_Wash_Current_price;
                t_Issue_for_Repair_Current_price += pos_Product.Issue_for_Repair_Current_price;
                t_Back_after_repair_Current_price += pos_Product.Back_after_repair_Current_price;
                t_Issue_to_Showroom_Current_price += pos_Product.Issue_to_Showroom_Current_price;
                t_Issue_Return_HO_Current_price += pos_Product.Issue_Return_HO_Current_price;
                t_Send_another_Showroom_Current_price += pos_Product.Send_another_Showroom_Current_price;
                t_Received_another_Showroom_Current_price += pos_Product.Received_another_Showroom_Current_price;
                t_Sales_Current_price += pos_Product.Sales_Current_price;
                t_Sales_return_Current_price += pos_Product.Sales_return_Current_price;
                t_Gift_Showroom_Current_price += pos_Product.Gift_Showroom_Current_price;
                t_Send_wash_Showroom_Current_price += pos_Product.Send_wash_Showroom_Current_price;
                t_Received_wash_Showroom_Current_price += pos_Product.Received_wash_Showroom_Current_price;
                t_Send_repair_Showroom_Current_price += pos_Product.Send_repair_Showroom_Current_price;
                t_Received_repair_Showroom_Current_price += pos_Product.Received_repair_Showroom_Current_price;
                t_Lost_Showroom_Current_price += pos_Product.Lost_Showroom_Current_price;
                t_Wastage_Showroom_Current_price += pos_Product.Wastage_Showroom_Current_price;
                t_Adjustment_Add_Showroom_Current_price += pos_Product.Adjustment_Add_Showroom_Current_price;
                t_Adjustment_Sub_Showroom_Current_price += pos_Product.Adjustment_Sub_Showroom_Current_price;
            }

            htmlTable += @"<tr class='subtotalRow'>
                            <td>&nbsp;</td>
                            <td colspan='3'>SubTotal Qty</td>
                            <td style='text-align:right;'>" + t_opining_subtotal.ToString("0,0.00") + @"</td>";

            //htmlTable += @"<td style='text-align:right;'>" +t_Issue_Wash_HO_Current_price_subtotal.ToString("0,0.00") + "</td>";//
            //htmlTable += @"<td style='text-align:right;'>" +t_Back_After_Wash_Current_price_subtotal.ToString("0,0.00") + "</td>";//
            //htmlTable += @"<td style='text-align:right;'>" +t_Issue_for_Repair_Current_price_subtotal.ToString("0,0.00") + "</td>";//
            //htmlTable += @"<td style='text-align:right;'>" +t_Back_after_repair_Current_price_subtotal.ToString("0,0.00") + "</td>";//
            htmlTable += @"<td style='text-align:right;'>" + t_Issue_to_Showroom_Current_subtotal.ToString("0,0.00") + "</td>";//
            htmlTable += @"<td style='text-align:right;'>" + t_Issue_Return_HO_Current_subtotal.ToString("0,0.00") + "</td>";
            htmlTable += @"<td style='text-align:right;'>" + t_Send_another_Showroom_Current_subtotal.ToString("0,0.00") + "</td>";
            htmlTable += @"<td style='text-align:right;'>" + t_Received_another_Showroom_Current_subtotal.ToString("0,0.00") + "</td>";
            htmlTable += @"<td style='text-align:right;'>" + t_Sales_Current_subtotal.ToString("0,0.00") + "</td>";
            htmlTable += @"<td style='text-align:right;'>" + t_Sales_return_Current_subtotal.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Gift_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Send_wash_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Received_wash_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Send_repair_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Received_repair_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Lost_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Wastage_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
            htmlTable += @"<td style='text-align:right;'>" + t_Adjustment_Add_Showroom_Current_subtotal.ToString("0,0.00") + "</td>";
            htmlTable += @"<td style='text-align:right;'>" + t_Adjustment_Sub_Showroom_Current_subtotal.ToString("0,0.00") + "</td>";

            htmlTable += @"<td style='text-align:right;'>" + t_StockInhand_subtotal.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + t_StockInhand_price_subtotal.ToString("0,0.00") + @"</td>
                        </tr>";


            htmlTable += @"<tr class='subtotalRow_price'>
                            <td>&nbsp;</td>
                            <td colspan='3'>SubTotal Price</td>
                            <td style='text-align:right;'>" + t_opining_price_subtotal.ToString("0,0.00") + @"</td>";

            //htmlTable += @"<td style='text-align:right;'>" +t_Issue_Wash_HO_Current_price_subtotal.ToString("0,0.00") + "</td>";//
            //htmlTable += @"<td style='text-align:right;'>" +t_Back_After_Wash_Current_price_subtotal.ToString("0,0.00") + "</td>";//
            //htmlTable += @"<td style='text-align:right;'>" +t_Issue_for_Repair_Current_price_subtotal.ToString("0,0.00") + "</td>";//
            //htmlTable += @"<td style='text-align:right;'>" +t_Back_after_repair_Current_price_subtotal.ToString("0,0.00") + "</td>";//
            htmlTable += @"<td style='text-align:right;'>" + t_Issue_to_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";//
            htmlTable += @"<td style='text-align:right;'>" + t_Issue_Return_HO_Current_price_subtotal.ToString("0,0.00") + "</td>";
            htmlTable += @"<td style='text-align:right;'>" + t_Send_another_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
            htmlTable += @"<td style='text-align:right;'>" + t_Received_another_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
            htmlTable += @"<td style='text-align:right;'>" + t_Sales_Current_price_subtotal.ToString("0,0.00") + "</td>";
            htmlTable += @"<td style='text-align:right;'>" + t_Sales_return_Current_price_subtotal.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Gift_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Send_wash_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Received_wash_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Send_repair_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Received_repair_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Lost_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Wastage_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
            htmlTable += @"<td style='text-align:right;'>" + t_Adjustment_Add_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";
            htmlTable += @"<td style='text-align:right;'>" + t_Adjustment_Sub_Showroom_Current_price_subtotal.ToString("0,0.00") + "</td>";

            htmlTable += @"<td style='text-align:right;'>" + t_StockInhand_subtotal.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + t_StockInhand_price_subtotal.ToString("0,0.00") + @"</td>
                        </tr>";

            htmlTable += @"<tr class='subtotalRow'>
                            <td>&nbsp;</td>
                            <td colspan='3'>Grand Total Qty</td>
                            <td style='text-align:right;'>" + t_opining.ToString("0,0.00") + @"</td>";

            //htmlTable += @"<td style='text-align:right;'>" +t_Issue_Wash_HO_Current_price.ToString("0,0.00") + "</td>";//
            //htmlTable += @"<td style='text-align:right;'>" +t_Back_After_Wash_Current_price.ToString("0,0.00") + "</td>";//
            //htmlTable += @"<td style='text-align:right;'>" +t_Issue_for_Repair_Current_price.ToString("0,0.00") + "</td>";//
            //htmlTable += @"<td style='text-align:right;'>" +t_Back_after_repair_Current_price.ToString("0,0.00") + "</td>";//
            htmlTable += @"<td style='text-align:right;'>" +t_Issue_to_Showroom_Current.ToString("0,0.00") + "</td>";//
            htmlTable += @"<td style='text-align:right;'>" +t_Issue_Return_HO_Current.ToString("0,0.00") + "</td>";
            htmlTable += @"<td style='text-align:right;'>" +t_Send_another_Showroom_Current.ToString("0,0.00") + "</td>";
            htmlTable += @"<td style='text-align:right;'>" +t_Received_another_Showroom_Current.ToString("0,0.00") + "</td>";
            htmlTable += @"<td style='text-align:right;'>" +t_Sales_Current.ToString("0,0.00") + "</td>";
            htmlTable += @"<td style='text-align:right;'>" +t_Sales_return_Current.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Gift_Showroom_Current_price.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Send_wash_Showroom_Current_price.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Received_wash_Showroom_Current_price.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Send_repair_Showroom_Current_price.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Received_repair_Showroom_Current_price.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Lost_Showroom_Current_price.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Wastage_Showroom_Current_price.ToString("0,0.00") + "</td>";
            htmlTable += @"<td style='text-align:right;'>" + t_Adjustment_Add_Showroom_Current.ToString("0,0.00") + "</td>";
            htmlTable += @"<td style='text-align:right;'>" + t_Adjustment_Sub_Showroom_Current.ToString("0,0.00") + "</td>"; 

            htmlTable += @"<td style='text-align:right;'>" + t_StockInhand.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + t_StockInhandPrice.ToString("0,0.00") + @"</td>
                        </tr>";


            htmlTable += @"<tr class='subtotalRow_price'>
                            <td>&nbsp;</td>
                            <td colspan='3'>Grand Total Price</td>
                            <td style='text-align:right;'>" + t_opining_price.ToString("0,0.00") + @"</td>";

            //htmlTable += @"<td style='text-align:right;'>" +t_Issue_Wash_HO_Current_price.ToString("0,0.00") + "</td>";//
            //htmlTable += @"<td style='text-align:right;'>" +t_Back_After_Wash_Current_price.ToString("0,0.00") + "</td>";//
            //htmlTable += @"<td style='text-align:right;'>" +t_Issue_for_Repair_Current_price.ToString("0,0.00") + "</td>";//
            //htmlTable += @"<td style='text-align:right;'>" +t_Back_after_repair_Current_price.ToString("0,0.00") + "</td>";//
            htmlTable += @"<td style='text-align:right;'>" + t_Issue_to_Showroom_Current_price.ToString("0,0.00") + "</td>";//
            htmlTable += @"<td style='text-align:right;'>" + t_Issue_Return_HO_Current_price.ToString("0,0.00") + "</td>";
            htmlTable += @"<td style='text-align:right;'>" + t_Send_another_Showroom_Current_price.ToString("0,0.00") + "</td>";
            htmlTable += @"<td style='text-align:right;'>" + t_Received_another_Showroom_Current_price.ToString("0,0.00") + "</td>";
            htmlTable += @"<td style='text-align:right;'>" + t_Sales_Current_price.ToString("0,0.00") + "</td>";
            htmlTable += @"<td style='text-align:right;'>" + t_Sales_return_Current_price.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Gift_Showroom_Current_price.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Send_wash_Showroom_Current_price.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Received_wash_Showroom_Current_price.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Send_repair_Showroom_Current_price.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Received_repair_Showroom_Current_price.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Lost_Showroom_Current_price.ToString("0,0.00") + "</td>";
            //htmlTable += @"<td style='text-align:right;'>" +t_Wastage_Showroom_Current_price.ToString("0,0.00") + "</td>";
            htmlTable += @"<td style='text-align:right;'>" + t_Adjustment_Add_Showroom_Current_price.ToString("0,0.00") + "</td>";
            htmlTable += @"<td style='text-align:right;'>" + t_Adjustment_Sub_Showroom_Current_price.ToString("0,0.00") + "</td>";

            htmlTable += @"<td style='text-align:right;'>" + t_StockInhand.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + t_StockInhandPrice.ToString("0,0.00") + @"</td>
                        </tr></table>";

            lblItemList.Text = htmlTable;
    }

    private void initialLoad()
    {
        lblPrintDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
    }
}