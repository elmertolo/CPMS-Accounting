using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using CPMS_Accounting.Models;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using static CPMS_Accounting.GlobalVariables;
using System.IO;
using System.Diagnostics;
using System.Threading;
using CPMS_Accounting.Forms;

namespace CPMS_Accounting.Procedures
{
    public class ProcessServices_Nelson
    {
        //02152021 Log4Net
        private log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //For Number of Affected Rows upon CRUD
        private int rowNumbersAffected;
        public int RowNumbersAffected
        {
            get { return rowNumbersAffected; }
        }

        private string _errorMessage;
        MySqlConnection con;
        MySqlDataAdapter da;

        public string conStr
        {
            get { return ConfigurationManager.AppSettings["ConnectionString"]; }
        }

        public string errorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public ProcessServices_Nelson()
        {

            OpenDB();

        }

        private bool OpenDB()
        {
            try
            {
                con = new MySqlConnection(conStr);
                con.Open();
                return true;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }
        }

        public bool LoadUnprocessedSalesInvoiceData(ref DataTable dt)
        {
            
            try
            {
                string sql;

                if (gClient.ShortName == "PNB")
                {
                    sql = "select batch, chequename, ChkType, deliverydate, count(ChkType) as Quantity, ProductCode, location from " + gClient.DataBaseName + " where salesinvoice is null group by batch, chequename, ChkType, location";
                }
                else
                {
                    sql = "select batch, chequename, ChkType, deliverydate, count(ChkType) as Quantity, ProductCode from " + gClient.DataBaseName + " where salesinvoice is null group by batch, chequename, ChkType";
                }
                
                //string sql = "select count(*) as count from producers_history";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                da = new MySqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                da.Fill(dt);
                return true;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }

        }

        public bool LoadPriceListData(ref DataTable dt)
        {
            try
            {
                string sql = "select productcode, bankcode, chequeName, description, UnitPrice, Docstamp from " + gClient.PriceListTable + "";
                //string sql = "select count(*) as count from producers_history";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                da = new MySqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                da.Fill(dt);
                return true;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }

        }


        public bool LoadSearchedItem(string inputText ,ref DataTable dt)
        {
            try
            {
                string sql = "select productcode, bankcode, chequeName, description, UnitPrice, Docstamp from " + gClient.PriceListTable + " where chequeName like '%" + inputText + "%';";
                //string sql = "select count(*) as count from producers_history";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                da = new MySqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                da.Fill(dt);
                return true;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }

        }

        public string GetDRList(string batch, string checktype, DateTime deliveryDate, string location)
        {
           
            try
            {


                DataTable dt = new DataTable();
                string sql;

                //Include Location on query if PNB
                if (gClient.ShortName == "PNB")
                {
                    sql = "select group_concat(distinct(drnumber) separator ', ') from " + gClient.DataBaseName + " " +
                    "WHERE salesinvoice is null " +
                    "and batch = '" + batch + "' " +
                    "and chktype = '" + checktype + "' " +
                    "and location = '" + location + "' " +
                    "and deliverydate = '" + deliveryDate.ToString("yyyy-MM-dd") + "';";
                }
                else
                {
                   sql = "select group_concat(distinct(drnumber) separator ', ') from " + gClient.DataBaseName + " " +
                   "WHERE salesinvoice is null " +
                   "and batch = '" + batch + "' " +
                   "and chktype = '" + checktype + "' " +
                   "and deliverydate = '" + deliveryDate.ToString("yyyy-MM-dd") + "';";
                }

                
                MySqlCommand cmd = new MySqlCommand(sql, con);
                da = new MySqlDataAdapter(cmd);

                
               

                cmd.ExecuteNonQuery();
                
                //cmd.ExecuteNonQuery();
                da.Fill(dt);

                string drList = dt.Rows[0].Field<string>(0).ToString(); // get concatenated delivery number list 
                return drList is null ? "" : drList; // return concatenated delivery number list if not null

            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return null;
            }
        }
        
        public bool UpdateTempTableSI(List<SalesInvoiceModel> SalesInvoiceList)
        {
            try
            {
                foreach (var row in SalesInvoiceList)
                {
                    string sql = "insert into " + gClient.SalesInvoiceTempTable + " " +
                                 "(Quantity, Batch, CheckName, DRList) values " +
                                 "(" + row.Quantity + "," +
                                 " '" + row.Batch.ToString() + "'," +
                                 " '" + row.checkName.ToString() + "'," +
                                 " '" + row.drList.ToString() + "'" +

                                 ");";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                //MySqlCommand cmd = new MySqlCommand("delete * from producers_tempdatadr;", con);
                //cmd.ExecuteNonQuery();
                _errorMessage = ex.Message;
                return false;
            }

        }

        public bool GetProcessedSalesInvoiceList(ref DataTable dt)
        {
            try
            {

                MySqlCommand cmd = new MySqlCommand("select * from producers_tempdatadr", con);
                da = new MySqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                da.Fill(dt);

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public double GetUnitPrice(string checkName)
        {

            MySqlCommand cmd;
            //validation
            if (checkName.Contains("'"))
            {

                checkName = checkName.Replace("'", "''");

                ////List<string> words = checkName.Split('\'').ToList();
                ////string concat = "";

                ////foreach (string word in words)
                ////{
                ////    concat += "'" + word + "'";
                ////}
                ////checkName = concat;

                //cmd = new MySqlCommand("select unitprice as UnitPrice from " + gClient.PriceListTable + " where chequename = '" + newCheckName + "';", con);
            }
           
            cmd = new MySqlCommand("select unitprice as UnitPrice from " + gClient.PriceListTable + " where chequename = '" + checkName + "';", con);

            

            var result = cmd.ExecuteScalar() ?? 0;

            
            return Convert.ToDouble(result);

        }

        public bool UpdateSalesInvoiceHistory(List<SalesInvoiceModel> siListToProcess)
        {
            try
            {
                //database update
                MySqlCommand cmd;
                foreach (var item in siListToProcess)
                {
                    string sql;
                    //PNB Update. Added Purchase Order Field upon updating
                    if (gClient.ShortName == "PNB")
                    {
                        //Update History Table
                         sql = "update " + gClient.DataBaseName + " set " +
                        "unitprice = " + item.unitPrice + ", " +
                        "purchaseordernumber = " + item.PurchaseOrderNumber + ", " +
                        "SalesInvoice = " + gSalesInvoiceFinished.SalesInvoiceNumber + ", " +
                        "Salesinvoicedate = '" + item.salesInvoiceDate.ToString("yyyy-MM-dd") + "', " +
                        "SalesInvoiceGeneratedBy = '" + gSalesInvoiceFinished.GeneratedBy + "' " +
                        " where drnumber in(" + item.drList.ToString() +
                        ") and batch = '" + item.Batch + "'" +
                        " and deliverydate = '" + item.deliveryDate.ToString("yyyy-MM-dd") + "'" +
                        " and chktype = '" + item.checkType.ToString() + "'" +
                        " and location = '" + item.Location + "'" +
                        " and chequename = '" + item.checkName + "';";
                    }
                    else
                    {
                        //Update History Table
                        sql = "update " + gClient.DataBaseName + " set " +
                        "unitprice = " + item.unitPrice + ", " +
                        "SalesInvoice = " + gSalesInvoiceFinished.SalesInvoiceNumber + ", " +
                        "Salesinvoicedate = '" + item.salesInvoiceDate.ToString("yyyy-MM-dd") + "', " +
                        "SalesInvoiceGeneratedBy = '" + gSalesInvoiceFinished.GeneratedBy + "' " +
                        " where drnumber in(" + item.drList.ToString() +
                        ") and batch = '" + item.Batch + "'" +
                        " and deliverydate = '" + item.deliveryDate.ToString("yyyy-MM-dd") + "'" +
                        " and chktype = '" + item.checkType.ToString() + "'" +
                        " and chequename = '" + item.checkName + "';";
                    }

                    cmd = new MySqlCommand(sql, con);
                    rowNumbersAffected = cmd.ExecuteNonQuery();

                }

                //Insert to SalesInvoice Finished
                string sql2 = "insert into " + gClient.SalesInvoiceFinishedTable + " " +
                "(ClientCode, SalesInvoiceNumber, salesInvoiceDateTime, GeneratedBy, CheckedBy, ApprovedBy, TotalAmount, VatAmount, NetOfVatAmount) " +
                "Values ('" +
                gSalesInvoiceFinished.ClientCode + "', " +
                gSalesInvoiceFinished.SalesInvoiceNumber + ", '" +
                gSalesInvoiceFinished.SalesInvoiceDateTime.ToString("yyyy-MM-dd HH:mm") + "', '" +
                gSalesInvoiceFinished.GeneratedBy + "', '" +
                gSalesInvoiceFinished.CheckedBy + "', '" +
                gSalesInvoiceFinished.ApprovedBy + "', " +
                gSalesInvoiceFinished.TotalAmount + ", " +
                gSalesInvoiceFinished.VatAmount + ", " +
                gSalesInvoiceFinished.NetOfVatAmount + ");";

                cmd = new MySqlCommand(sql2, con);
                rowNumbersAffected = cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {

                _errorMessage = ex.Message;
                return false;
            }

        }

        public bool UpdatePurchaseOrderFinished(List<PurchaseOrderModel> poListToProcess)
        {

            try
            {
                //database update
                MySqlCommand cmd;
                foreach (var item in poListToProcess)
                {

                    //Insert Purchase Order Finished
                    string sql = "insert into " + gClient.PurchaseOrderFinishedTable + " " +
                    "(purchaseorderno, purchaseorderdatetime, clientcode, productcode, quantity, chequename, description, unitprice, docstamp, generatedby, checkedby, approvedby) " +
                    "Values (@PurchaseOrderNumber, @PurchaseOrderDateTime, @ClientCode, @ProductCode, @Quantity, @ChequeName, @Description, @UnitPrice, @Docstamp, @GeneratedBy, @CheckedBy, @ApprovedBy)";

                    cmd = new MySqlCommand(sql, con);

                    cmd.Parameters.Add("@PurchaseOrderNumber", MySqlDbType.Int32).Value = item.PurchaseOrderNumber;
                    cmd.Parameters.Add("@PurchaseOrderDateTime", MySqlDbType.DateTime).Value = item.PurchaseOrderDateTime;
                    cmd.Parameters.Add("@ClientCode", MySqlDbType.String).Value = item.ClientCode;
                    cmd.Parameters.Add("@ProductCode", MySqlDbType.String).Value = item.ProductCode;
                    cmd.Parameters.Add("@Quantity", MySqlDbType.Int32).Value = item.Quantity;
                    cmd.Parameters.Add("@ChequeName", MySqlDbType.String).Value = item.ChequeName;
                    cmd.Parameters.Add("@Description", MySqlDbType.String).Value = item.Description;
                    cmd.Parameters.Add("@UnitPrice", MySqlDbType.Double).Value = item.UnitPrice;
                    cmd.Parameters.Add("@Docstamp", MySqlDbType.Double).Value = item.Docstamp;
                    cmd.Parameters.Add("@GeneratedBy", MySqlDbType.String).Value = item.GeneratedBy;
                    cmd.Parameters.Add("@CheckedBy", MySqlDbType.String).Value = item.CheckedBy;
                    cmd.Parameters.Add("@ApprovedBy", MySqlDbType.String).Value = item.ApprovedBy;

                    rowNumbersAffected = cmd.ExecuteNonQuery();

                }

                

                return true;
            }
            catch (Exception ex)
            {

                _errorMessage = ex.Message;
                return false;
            }

        }

        public bool BatchSearch(string batchToSearch, ref DataTable dt)
        {
            try
            {
                MySqlDataAdapter da;
                string sql = "select batch, chequename, ChkType, deliverydate, count(ChkType) as Quantity from " + gClient.DataBaseName +
                    " where salesinvoice is null and batch = '" + batchToSearch + "' group by batch, chequename, ChkType;";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                da = new MySqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                da.Fill(dt);
                return true;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }

        }

        public bool GetUserDetails(ref DataTable dt, string UserId = "*")
        {
            try
            {
                string sql;

                if (UserId == "*")
                {
                    sql = "select * from userlist;";
                }
                else
                {
                    sql = "select * from userlist where userId ='"+ UserId +"'";
                }

                MySqlCommand cmd = new MySqlCommand(sql, con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                da.Fill(dt);


                return true;

            }
            catch (Exception ex)
            {

                _errorMessage = ex.Message;
                return false;
            }

        }

        public bool UserLogin(string userId, string password, ref DataTable dt)
        {

            try
            {

                MySqlDataAdapter da;
                MySqlCommand cmd = new MySqlCommand("select * from userlist where userId = ? and password = ?", con);
                cmd.Parameters.Add(new MySqlParameter("userId", userId));
                cmd.Parameters.Add(new MySqlParameter("password", password));
                da = new MySqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                da.Fill(dt);
                return true;

            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }
        }

        public bool GetBankList(ref DataTable dt)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("select clientcode, shortname, description, address1, address2, address3, attentionto, Princes_DESC, TIN, WithholdingTaxPercentage, " +
                    "databasename from clientlist order by shortname;", con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                da.Fill(dt);
                return true;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }
        }

        public bool SalesInvoiceExist(int salesInvoiceNumber, ref DataTable dt)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from " + gClient.SalesInvoiceFinishedTable + " where salesinvoicenumber = " + salesInvoiceNumber + ";", con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }

        }

        public bool PurchaseOrderExist(int purchaseOrderNumber, ref DataTable dt)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from " + gClient.PurchaseOrderFinishedTable + " where purchaseorderno = " + purchaseOrderNumber + ";", con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }

        }

        public bool GetOldSalesInvoiceList(double salesInvoiceNumber, ref DataTable dt)
        {
            try
            {
                string sql;
                
                if (gClient.ShortName == "PNB")
                {
                    sql =
                    "select count(ChkType) as Quantity, batch, chequename as CheckName, group_concat(distinct(drnumber) separator ', ') as DRList , " +
                    "ChkType, deliverydate, UnitPrice, " +
                    "count(ChkType) * UnitPrice as LineTotalAmount, SalesInvoiceDate, location " +
                    "from " + gClient.DataBaseName + " " +
                    "where salesinvoice = " + salesInvoiceNumber + " " +
                    "group by batch, CheckName, ChkType, location order by Batch;";

                }
                else
                {
                    sql =
                    "select count(ChkType) as Quantity, batch, chequename as CheckName, group_concat(distinct(drnumber) separator ', ') as DRList, " +
                    "ChkType, deliverydate, Unitprice, " +
                    "count(ChkType) * UnitPrice as LineTotalAmount, SalesInvoiceDate " +
                    "from " + gClient.DataBaseName + " " +
                    "where salesinvoice = " + salesInvoiceNumber + " " +
                    "group by batch, CheckName, ChkType order by Batch;";

                }

                //"select batch as BatchName, chequename as CheckName, ChkType, deliverydate, count(ChkType) as Quantity, group_concat(distinct(drnumber) separator ', ') as drList " +
                //    "from " + gHistoryTable + " " +


                //    "where salesinvoice = " + salesInvoiceNumber + " group by batch, CheckName, ChkType order by BatchName";

                //string sql = "select count(*) as count from producers_history";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                da = new MySqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                da.Fill(dt);
                return true;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }
        }

        public bool GetOldPurchaseOrderList(double purchaseOrderNumber, ref DataTable dt)
        {
            try
            {
                string sql = "";
                if (gClient.ShortName == "PNB")
                {

                    sql = "select purchaseorderno, purchaseorderdatetime, clientcode, productcode, quantity, chequename, description, unitprice,docstamp, generatedby, checkedby,approvedby " +
                    "from " + gClient.PurchaseOrderFinishedTable + " where purchaseorderno = " + purchaseOrderNumber + "";

                }
                MySqlCommand cmd = new MySqlCommand(sql, con);
                da = new MySqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                da.Fill(dt);
                return true;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }

        }
        public object SeekReturn(string query, object def)
        {
           
            MySqlCommand cmd = new MySqlCommand(query, con);
            var result = cmd.ExecuteScalar();

            if (result is null || result.ToString() == "")
            {
                return def;
            }
            else
            {
                return result;
            }


        }

        public bool GetClientDetails(string clientDescription, ref DataTable dt)
        {
            try
            {
                string sql = "select clientcode, shortname, description, address1, address2, address3, attentionto, Princes_DESC, TIN, WithholdingTaxPercentage, " +
                    "databasename from clientlist " +
                    "where description = '" + clientDescription + "' order by shortname;";
                MySqlCommand cmd = new MySqlCommand(sql , con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                da.Fill(dt);
                return true;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }
        }

        //PNB
        public bool IsQuantityOnHandSufficient(double toProcessQuantity, string productCode, int purchaseOrderNumber, ref double remainingQuantity, ref List<SalesInvoiceModel> salesInvoiceList)
        {
            
            try
            {
                
                //Check Onhand quantity first. cancel update if onhand quantity is insufficient
                //double onhandQuantity = double.Parse(SeekReturn("select (quantityonhand) from " + gClient.PriceListTable + " where chequename = '" + chequeName + "'").ToString() ?? "");
                //NA_01252021 Revision from above statement. changed target field when checking onhand quantity of chequename
                double onhandQuantity = Convert.ToDouble(SeekReturn("select quantity from " + gClient.PurchaseOrderFinishedTable + " where productcode = '" + productCode + "' and purchaseorderno = " + purchaseOrderNumber + "", 0));
                double processedQuantity = Convert.ToDouble(SeekReturn("select count(chequename) as quantity from " + gClient.DataBaseName + " where productcode = '" + productCode + "' and purchaseordernumber = " + purchaseOrderNumber + "", 0));
                int totalPunchedItemQuantity = 0;

                //Check and add Punched Item on grid

                foreach (var item in salesInvoiceList)
                {
                    if (item.ProductCode == productCode)
                    {
                        totalPunchedItemQuantity += item.Quantity;
                    }
                }
                
                remainingQuantity = onhandQuantity - processedQuantity - totalPunchedItemQuantity - toProcessQuantity;

                if (remainingQuantity < 0)
                {
                   
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }
        }

        //PNB
        public bool UpdateItemQuantityOnhand(double toProcessQuantity, string chequeName, int purchaseOrderNumber)
        {
            try
            {
                //Check Onhand quantity first. cancel update if onhand quantity is insufficient
                //double onhandQuantity = double.Parse(SeekReturn("select quantityonhand from " + gClient.PriceListTable + " where chequename = '" + chequeName + "'").ToString() ?? "");
                //double newItemQuantity = onhandQuantity - quantity;

                double onhandQuantity = Convert.ToDouble(SeekReturn("select quantity from " + gClient.PurchaseOrderFinishedTable + " where chequename = '" + chequeName + "' and purchaseorderno = " + purchaseOrderNumber + "", 0));
                double processedQuantity = Convert.ToDouble(SeekReturn("select count(chequename) as quantity from " + gClient.DataBaseName + " where chequename = '" + chequeName + "' and purchaseordernumber = " + purchaseOrderNumber + "", 0));
                double newItemQuantity = onhandQuantity - processedQuantity;

                MySqlCommand cmd = new MySqlCommand("Update " + gClient.PriceListTable + " set quantityonhand = " + newItemQuantity + " where chequeName = '" + chequeName + "'", con);
                rowNumbersAffected = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }
        }

        public bool RemoveSalesInvoiceTagOnHistory(double salesInvoiceNumber)
        {
            try
            {
                string sql;
                if (gClient.ShortName == "PNB")
                {
                    sql = "update " + gClient.DataBaseName + " set salesinvoice = null, purchaseordernumber = null where salesinvoice = " + salesInvoiceNumber + "";
                }
                else
                {
                    sql = "update " + gClient.DataBaseName + " set salesinvoice = null where salesinvoice = " + salesInvoiceNumber + "";
                }
                
                MySqlCommand cmd = new MySqlCommand(sql, con);
                rowNumbersAffected = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public bool UpdateSalesInvoiceStatusOnfinished(double salesInvoiceNumber, int status)
        {
            try
            {
                string sql = "update " + gClient.SalesInvoiceFinishedTable + " set IsCancelled = " + status + " where salesinvoicenumber = " + salesInvoiceNumber + "";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                rowNumbersAffected = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public bool DeletePurchaseOrderRecordOnfinished(int purchaseOrderNumber)
        {
            try
            {
                string sql = "delete from " + gClient.PurchaseOrderFinishedTable + " where purchaseorderno = " + purchaseOrderNumber + "";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                rowNumbersAffected = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public bool GetProductDetails(string productCode, ref DataTable dt)
        {
            try
            {
                string sql = "select * from "+ gClient.PriceListTable +" where productcode = '" + productCode + "';";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                da = new MySqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                da.Fill(dt);
                return true;
                
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
                return false;
            }
        }

        public bool UpdateIsAllowedOnForm(string formIntial, int value)
        {
            try
            {
                string sql = "update userlevel set isallowedon" + formIntial + " =" + value + "";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                rowNumbersAffected = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public bool InsertInitialUserLevelRecord(string userLevelCode, string userLevelName)
        {
            try
            {
                string sql = "insert into userlevels (UserLevelCode, UserLevelName) values ('"+ userLevelCode +"', '"+ userLevelName +"')";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                rowNumbersAffected = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
       
        }

        public bool UpdateUserLevelRecord(UserLevelModel userLevel, string formInitial, int radioButtonValue, int isCreateAllowedValue, int isEditAllowedValue, int isDeleteAllowedValue)
        {
            try
            {
                string sql = "update userlevels set userlevelname = '" + userLevel.Name + "', isallowedon" + formInitial + " =" + radioButtonValue + ", " +
                    "is" + formInitial + "CreateAllowed =" + isCreateAllowedValue + ", " +
                    "is" + formInitial + "EditAllowed =" + isEditAllowedValue + ", " +
                    "is" + formInitial + "DeleteAllowed =" + isDeleteAllowedValue + " " +
                    "Where userlevelcode ='" + userLevel.Code + "'";

                MySqlCommand cmd = new MySqlCommand(sql, con);
                rowNumbersAffected = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public bool UserLevelExist(string userLevelCode)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("select userlevelcode from userlevels where userlevelcode = '" + userLevelCode + "';", con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }
        }

        public bool GetUserLevelDetails(ref DataTable dt, string userLevelCode = "*")
        {
            try
            {
                string sql;
                if (userLevelCode == "*")
                {
                    sql = "select * from userlevels";
                }
                else
                {
                    sql = "select * from userlevels where userlevelcode = '" + userLevelCode + "';";
                }
                MySqlCommand cmd = new MySqlCommand(sql, con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }


        }

        public bool DeleteUserLevelRecord(string userLevelCode)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("delete from userlevels where userlevelcode = '" + userLevelCode + "';", con);
                rowNumbersAffected =  cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }
        }
        
        public bool UserExist(string userId)
        {

            DataTable dt = new DataTable();
            MySqlCommand cmd = new MySqlCommand("select userId from userlist where userId = '" + userId + "';", con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            rowNumbersAffected = cmd.ExecuteNonQuery();
            da.Fill(dt);
            
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public bool InsertNewUserRecord(UserListModel user)
        {
            try
            {
                string sql = "insert into userlist (UserId, Password, FirstName, MiddleName, LastName, Suffix, UserLevelCode, Department, Position) values (" +
                    "@UserId, @Password, @FirstName, @MiddleName, @LastName, @Suffix, @UserLevelCode, @Department, @Position)";

                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@UserId", user.Id);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@MiddleName", user.MiddleName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@Suffix", user.Suffix);
                cmd.Parameters.AddWithValue("@UserLevelCode", user.UserLevelCode);
                cmd.Parameters.AddWithValue("@Department", user.Department);
                cmd.Parameters.AddWithValue("@Position", user.Position);

                rowNumbersAffected = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }

        }

        public bool UpdateExistingUserRecord(UserListModel user)
        {
            try
            {
                string sql = "Update Userlist set password = @Password, firstname = @FirstName, middlename = @MiddleName, lastname = @LastName, suffix = @Suffix, userlevelCode = @UserLevelCode, department = @Department, position = @Position " +
                    "Where UserId = @UserId;";
                MySqlCommand cmd = new MySqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@UserId", user.Id);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@MiddleName", user.MiddleName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@Suffix", user.Suffix);
                cmd.Parameters.AddWithValue("@UserLevelCode", user.UserLevelCode);
                cmd.Parameters.AddWithValue("@Department", user.Department);
                cmd.Parameters.AddWithValue("@Position", user.Position);
    

                rowNumbersAffected = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }


        public bool GetUserLevels(ref DataTable dt)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from userlevels;", con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                da.Fill(dt);
                return true;

            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }
        }

        public bool DeleteUserRecord(string userId)
        {
            
            try
            {
                MySqlCommand cmd = new MySqlCommand("delete from userlist where userId = '" + userId + "';", con);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
               
            }
        }

        //public bool Authorize(string formName, string authorization)
        //{
        //    string formAlias = "";

        //    switch (formName)
        //    {
        //        case "frmDEliveryReport":
        //            formAlias = "Dr";
        //            break;
        //        case "frmSalesInvoice":
        //            formAlias = "Si";
        //            break;
        //        case "frmPurchaseOrder":
        //            formAlias = "Si";
        //            break;
        //        case "frmUserLevelManagement":
        //            formAlias = "Si";
        //            break;
        //        case "frmUserMaintenance":
        //            formAlias = "Si";
        //            break;
        //        case "frmProductPriceList":
        //            formAlias = "Si";
        //            break;
        //    }

        //    try
        //    {
        //        MySqlCommand cmd = new MySqlCommand("select * from userlevels;", con);
        //        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        //        cmd.ExecuteNonQuery();
        //        da.Fill(dt);
        //        return true;

        //    }
        //    catch (Exception ex)
        //    {
        //        _errorMessage = ex.Message;
        //        return false;
        //    }
        //}


        public bool MySqlBackupTableData(string backupPath)
        {

            log.Info("Attempting to backup database");
            string fileName = backupPath + gClient.ShortName + "_" + DateTime.Now.ToShortDateString() + @"_backup.sql";
            if (File.Exists(fileName))
            {
                log.Info("Database backup file already exist.");
                return true;
            }

            try
            {

                MySqlCommand cmd = new MySqlCommand("",con);
                using (MySqlBackup mb = new MySqlBackup(cmd))
                {
                    //Whole Database
                    //mb.ExportToFile(file);
                    mb.ExportInfo.TablesToBeExportedList = new List<string>
                    {
                        gClient.DataBaseName,
                        gClient.SalesInvoiceTempTable,
                        gClient.SalesInvoiceFinishedTable,
                        gClient.PriceListTable,
                        gClient.DRTempTable,
                        gClient.PurchaseOrderFinishedTable,
                        gClient.DocStampTempTable,
                        gClient.BranchesTable,
                        gClient.CancelledTable,
                        gClient.ChequeTypeTable,
                        gClient.ProductTable,
                        gClient.StickerTable
                    };
                    mb.ExportToFile(fileName);

                }
                    return true;

            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }
        }


    }
}
