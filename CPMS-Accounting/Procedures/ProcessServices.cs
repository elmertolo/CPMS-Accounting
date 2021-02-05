using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMS_Accounting.Models;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using AxShockwaveFlashObjects;
using System.Collections.Concurrent;
using System.Net.Http.Headers;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using CrystalDecisions.CrystalReports.Engine;
using static CPMS_Accounting.GlobalVariables;
using System.Drawing;
using System.Data;

namespace CPMS_Accounting.Procedures
{
    class ProcessServices
    {
        // MySqlConnection con = new MySqlConnection();
        public MySqlConnection myConnect;
        public string databaseName = "";
        List<string> db = new List<string>();
        List<Int64> listofDR = new List<long>();
        MySqlCommand cmd;
        string Sql = "";

        public void DBConnect()
        {
            //try
            //{
                string DBConnection = "";

                //   if (frmLogIn.userName == "elmer")
                //  {
                DBConnection = ConfigurationManager.AppSettings["ConnectionString"];

                //databaseName = "captive_accounting";
                //  MessageBox.Show(databaseName);
                //   }
                //else
                //{
                //    //  DBConnection = "";
                //  DBConnection = "datasource=192.168.0.254;port=3306;username=root;password=CorpCaptive; convert zero datetime=True;";
                // MessageBox.Show("HELLO");
                //  databaseName = "captive_accounting";
                //    // MessageBox.Show(databaseName);

                //}

                myConnect = new MySqlConnection(DBConnection);

                myConnect.Open();

            //}
            //catch (Exception Error)
            //{

            //    MessageBox.Show(Error.Message, "System Error");
            //}
        }// end of function

        public void DBClosed()
        {
            myConnect.Close();
        }
        public static void gsStatusBar(Label pTool, string msg)
        {
            pTool.Text = msg;
        }
        public static void gsProgressBar(ProgressBar pTool, int pValue)
        {
            pTool.Value = pValue;
        }// end of function

        //public List<OrderModel> Process(List<OrderModel> _orders, DeliveryReport _main, int DrNumber, int packNumber)
        //{
        //    TypeofCheckModel checkType = new TypeofCheckModel();
        //    //int counter = 0;
        //    checkType.Regular_Personal = new List<OrderModel>();
        //    checkType.Regular_Commercial = new List<OrderModel>();


        //    foreach (OrderModel _check in _orders)
        //    {


        //        if (_check.ChkType == "A")
        //        {
        //            checkType.Regular_Personal.Add(_check);


        //        }
        //        if (_check.ChkType == "B")
        //        {
        //            checkType.Regular_Commercial.Add(_check);


        //        }
        //    }
        //    checkType.Regular_Personal.OrderBy(r => r.BranchName).ToList();
        //    checkType.Regular_Commercial.OrderBy(r => r.BranchName).ToList();
        //    //if(gClient.DataBaseName != "producers_history")
        //    //    GenerateData2(checkType, DrNumber, _main.deliveryDate, gUser.UserName, packNumber);
        //    //else
        //        Generate(checkType, DrNumber, _main.deliveryDate, gUser.UserName, packNumber);
        //    // Generate(checkType, DrNumber, _main.deliveryDate, "ELMER", packNumber);

        //    return _orders;
        //}
        public List<OrderModel> Process2(List<OrderModel> _orders, DeliveryReport _main, int DrNumber, int packNumber,int _dReportStyle, int _pReportStyle)
        {
            TypeofCheckModel checkType = new TypeofCheckModel();
            //int counter = 0;
            checkType.Regular_Personal = new List<OrderModel>();
            checkType.Regular_Commercial = new List<OrderModel>();
            checkType.Regular_Personal_Direct = new List<OrderModel>();
            checkType.Regular_Personal_Provincial = new List<OrderModel>();
            checkType.Regular_Commercial_Direct = new List<OrderModel>();
            checkType.Regular_Commercial_Provincial = new List<OrderModel>();
            

            foreach (OrderModel _check in _orders)
            {

                if (_dReportStyle != 0 && _pReportStyle != 0)
                {
                    if (_check.ChkType == "A" && _check.Location == "Direct")
                    {
                        checkType.Regular_Personal_Direct.Add(_check);
                    }
                    if (_check.ChkType == "A" && _check.Location == "Provincial")
                    {
                        checkType.Regular_Personal_Provincial.Add(_check);
                    }
                    if (_check.ChkType == "B" && _check.Location == "Direct")
                    {
                        checkType.Regular_Commercial_Direct.Add(_check);
                    }
                    if (_check.ChkType == "B" && _check.Location == "Provincial")
                    {
                        checkType.Regular_Commercial_Provincial.Add(_check);
                    }
                    
                }
                else
                {

                    if (_check.ChkType == "A" )
                    {
                        checkType.Regular_Personal.Add(_check);
                    }
                    if (_check.ChkType == "B")
                    {
                        checkType.Regular_Commercial.Add(_check);
                    }
                  
                }
                
            }
            checkType.Regular_Personal.OrderBy(r => r.BranchName).ToList();
            checkType.Regular_Commercial.OrderBy(r => r.BranchName).ToList();
            checkType.Regular_Personal_Direct.OrderBy(r => r.BranchName).ToList();
            checkType.Regular_Commercial_Direct.OrderBy(r => r.BranchName).ToList();
            checkType.Regular_Personal_Provincial.OrderBy(r => r.BranchName).ToList();
            checkType.Regular_Commercial_Provincial.OrderBy(r => r.BranchName).ToList();
          //  if (gClient.DataBaseName != "producers_history")
                GenerateData2(checkType, DrNumber, _main.deliveryDate, gUser.UserName, packNumber, _dReportStyle,_pReportStyle);
          //  else
          //      Generate(checkType, DrNumber, _main.deliveryDate, gUser.UserName, packNumber);
            // Generate(checkType, DrNumber, _main.deliveryDate, "ELMER", packNumber);

            return _orders;
        }
        public void Generate(TypeofCheckModel _checks, int _DrNumber, DateTime _deliveryDate, string _username, int _packNumber)
        {
            DBConnect();
            int counter = 0;
            var Personal = _checks.Regular_Personal.OrderBy(t => t.BranchName).ToList();
            if (Personal.Count > 0 )
            {
                //counter++;
                //_checks.Regular_Personal.ForEach(r =>
                //{
                //_checks.Regular_Personal.OrderBy(r => r.BranchName).ToList();
                var _list = Personal.Select(r => r.BRSTN).Distinct().ToList();
                //var sorted = (from c in _checks.Regular_Personal
                //                orderby c.BranchName
                //                         ascending
                //                select c).ToList();


                //Working Proccess Jan. 27 2021
                foreach (string Brstn in _list)
                {
                    var _model = Personal.Where(t => t.BRSTN == Brstn);

                    foreach (var r in _model)
                    {
                        

                            Sql = "Insert into " + gClient.DataBaseName + " (BRSTN,BranchName,AccountNo,AcctNoWithHyphen,Name1,Name2,ChkType," +
                                  "ChequeName,StartingSerial,EndingSerial,DRNumber,DeliveryDate,username,batch,PackNumber,Date,Time,location )" +
                                  "VALUES('" + r.BRSTN + "','" + r.BranchName + "','" + r.AccountNo + "','" + r.AccountNoWithHypen + "','" + r.Name1.Replace("'","''") +
                                  "','" + r.Name2.Replace("'","''") + "','" + r.ChkType + "','" + r.ChequeName + "','" + r.StartingSerial + "','" + r.EndingSerial +
                                  "','" + _DrNumber + "','" + _deliveryDate.ToString("yyyy-MM-dd") + "','" + _username + "','" +
                                  r.Batch.TrimEnd() + "','" + _packNumber + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("hh:mm:ss") +
                                  "','" + r.Location + "');";
                            cmd = new MySqlCommand(Sql, myConnect);
                            cmd.ExecuteNonQuery();
                      
                         
                       
                    }
                    counter++;
                    _packNumber++;
                    if (counter == 10)
                    {
                        _DrNumber++;
                        counter = 0;
                    }


                }

            }
            counter = 0;
            _DrNumber++;

            var Comm = _checks.Regular_Commercial.OrderBy(r => r.BranchName).ToList();
            if (Comm.Count > 0)
            {
                var _List = Comm.Select(r => r.BRSTN).Distinct().ToList();
                //var sorted = (from c in _checks.Regular_Commercial
                //              orderby c.BranchName
                //                       ascending
                //              select c).ToList();

                foreach (string Brstn in _List)
                {
                    var _model = Comm.Where(a => a.BRSTN == Brstn);
                    foreach (var r in _model)
                    {

                     
                            Sql = "Insert into " + gClient.DataBaseName + " (BRSTN,BranchName,AccountNo,AcctNoWithHyphen,Name1,Name2,ChkType," +
                                  "ChequeName,StartingSerial,EndingSerial,DRNumber,DeliveryDate,username,batch,PackNumber,Date,Time )" +
                                  "VALUES('" + r.BRSTN + "','" + r.BranchName + "','" + r.AccountNo + "','" + r.AccountNoWithHypen + "','" + r.Name1.Replace("'","''") +
                                  "','" + r.Name2.Replace("'","''") + "','" + r.ChkType + "','" + r.ChequeName + "','" + r.StartingSerial + "','" + r.EndingSerial +
                                  "','" + _DrNumber + "','" + _deliveryDate.ToString("yyyy-MM-dd") + "','" + _username + "','" +
                                  r.Batch.TrimEnd() + "','" + _packNumber + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("hh:mm:ss") +
                                  "');";
                            cmd = new MySqlCommand(Sql, myConnect);
                            cmd.ExecuteNonQuery();
                      

                    }
                    counter++;
                    _packNumber++;
                    if (counter == 10)
                    {
                        _DrNumber++;
                        counter = 0;
                    }
                }
            }
            DBClosed();
            return;
        }
        
        public void GenerateData2(TypeofCheckModel _checks, int _DrNumber, DateTime _deliveryDate, string _username, int _packNumber,int _dReportStyle,int _pReportStyle)
        {

            if (_dReportStyle == 0 && _pReportStyle == 0)
                ByChequetType(_checks, _DrNumber, _deliveryDate, _username, _packNumber);
            else if(_dReportStyle == 0 && _pReportStyle == 1 )
            {

            }
            else if(_dReportStyle == 0 && _pReportStyle == 2)
            {

            }
            else if (_dReportStyle == 0 && _pReportStyle == 3)
            {

            }
            else if (_dReportStyle == 0 && _pReportStyle == 4)
            {

            }
            else if (_dReportStyle == 0 && _pReportStyle == 5)
            {

            }
            else if (_dReportStyle == 1 && _pReportStyle == 0)
            {

            }
            else if (_dReportStyle == 1 && _pReportStyle == 1)
            {
               
            }
            else if (_dReportStyle == 1 && _pReportStyle == 3)
            {

            }
            else if (_dReportStyle == 1 && _pReportStyle == 4)
            {

            }
            else if (_dReportStyle == 1 && _pReportStyle == 5)
            {

            }
            else if (_dReportStyle == 2 && _pReportStyle == 0)
            {

            }
            else if (_dReportStyle == 2 && _pReportStyle == 1)
            {

            }
            else if (_dReportStyle == 2 && _pReportStyle == 2)
            {
                ByLocation(_checks, _DrNumber, _deliveryDate, _username, _packNumber);
            }
            else if (_dReportStyle == 2 && _pReportStyle == 3)
            {

            }
            else if (_dReportStyle == 2 && _pReportStyle == 4)
            {

            }
            else if (_dReportStyle == 2 && _pReportStyle == 5)
            {

            }
            else if (_dReportStyle == 3 && _pReportStyle == 0)
            {

            }
            else if (_dReportStyle == 3 && _pReportStyle == 1)
            {

            }
            else if (_dReportStyle == 3 && _pReportStyle == 2)
            {

            }
            else if (_dReportStyle == 3 && _pReportStyle == 3)
            {

            }
            else if (_dReportStyle == 3 && _pReportStyle == 4)
            {

            }
            else if (_dReportStyle == 3 && _pReportStyle == 5)
            {

            }
            else if (_dReportStyle == 4 && _pReportStyle == 0)
            {

            }
            else if (_dReportStyle == 4 && _pReportStyle == 1)
            {

            }
            else if (_dReportStyle == 4 && _pReportStyle == 2)
            {

            }
            else if (_dReportStyle == 4 && _pReportStyle == 3)
            {

            }
            else if (_dReportStyle == 4 && _pReportStyle == 4)
            {

            }
            else if (_dReportStyle == 4 && _pReportStyle == 5)
            {
                ByLocationAndType(_checks, _DrNumber, _deliveryDate, _username, _packNumber);
            }
            else if (_dReportStyle == 5 && _pReportStyle == 0)
            {

            }
            else if (_dReportStyle == 5 && _pReportStyle == 1)
            {

            }
            else if (_dReportStyle == 5 && _pReportStyle == 2)
            {

            }
            else if (_dReportStyle == 5 && _pReportStyle == 3)
            {

            }
            else if (_dReportStyle == 5 && _pReportStyle == 4)
            {
                ByLocationAndType(_checks, _DrNumber, _deliveryDate, _username, _packNumber);
            }
            else if (_dReportStyle == 5 && _pReportStyle == 5)
            {

            }
        }
        //public void GenerateData(List<OrderModel> _orderList, int _DrNumber, DateTime _deliveryDate, string _username, int _packNumber)
        //{
        //    DBConnect();

        //        var listofBRSTN = _orderList.Select(e => e.BRSTN).Distinct().ToList();


        //    int counter = 0;
        //    foreach (string brstn in listofBRSTN)
        //    {

        //        var _list = _orderList.Where(r => r.BRSTN == brstn && r.ChkType == "A");
        //        if (gClient.DataBaseName == "producers_history")
        //        {
        //            foreach (var check in _list)
        //            {
        //                Sql = "Insert into " + gClient.DataBaseName + " (BRSTN,BranchName,AccountNo,AcctNoWithHyphen,Name1,Name2,ChkType,ChequeName," +
        //                    "StartingSerial,EndingSerial,DRNumber,DeliveryDate,username,batch,PackNumber )"
        //               + "VALUES('" + check.BRSTN + "','" + check.BranchName + "','" + check.AccountNo + "','" + check.AccountNoWithHypen + "','" +
        //               check.Name1 + "','" + check.Name2 + "','" + check.ChkType + "','" + check.ChequeName + "','" + check.StartingSerial + "','" +
        //               check.EndingSerial + "','" + _DrNumber + "','" + _deliveryDate.ToString("yyyy-MM-dd") + "','" + _username + "','" +
        //               check.Batch.TrimEnd() + "','" + _packNumber + "');";
        //                cmd = new MySqlCommand(Sql, myConnect);
        //                cmd.ExecuteNonQuery();


        //            }
        //        }
        //        else if(gClient.DataBaseName == "pnb_history")
        //        {
        //            foreach (var check in _list)
        //            {
        //                Sql = "Insert into " + gClient.DataBaseName + " (BRSTN,BranchName,AccountNo,AcctNoWithHyphen,Name1,Name2,ChkType,ChequeName," +
        //                    "StartingSerial,EndingSerial,DRNumber,DeliveryDate,username,batch,PackNumber,BranchCode,OldBranchCode )"
        //               + "VALUES('" + check.BRSTN + "','" + check.BranchName + "','" + check.AccountNo + "','" + check.AccountNoWithHypen + "','" +
        //               check.Name1 + "','" + check.Name2 + "','" + check.ChkType + "','" + check.ChequeName + "','" + check.StartingSerial + "','" +
        //               check.EndingSerial + "','" + _DrNumber + "','" + _deliveryDate.ToString("yyyy-MM-dd") + "','" + _username + "','" +
        //               check.Batch.TrimEnd() + "','" + _packNumber + "','"+check.BranchCode+"','" +check.OldBranchCode+ "');";
        //                cmd = new MySqlCommand(Sql, myConnect);
        //                cmd.ExecuteNonQuery();

        //            }
        //        }

        //            counter++;
        //            if (counter == 10)
        //            {
        //                _DrNumber++;
        //                counter = 0;
        //            }


        //    }

        //    DBClosed();
        //    counter = 0;

        //    foreach (var brstn in listofBRSTN)
        //    {
        //        _DrNumber = GetLastDRFromHistory();
        //        if (counter == 0)
        //          {
        //            _DrNumber++;
        //          }

        //        var _listb = _orderList.Where(r => r.BRSTN == brstn && r.ChkType == "B");

        //        DBConnect();
        //        if (gClient.DataBaseName == "producers_history")
        //        {
        //            foreach (var check in _listb)
        //            {
        //                Sql = "Insert into " + gClient.DataBaseName + " (BRSTN,BranchName,AccountNo,AcctNoWithHyphen,Name1,Name2,ChkType,ChequeName," +
        //                   "StartingSerial,EndingSerial,DRNumber,DeliveryDate,username,batch,PackNumber )"
        //              + "VALUES('" + check.BRSTN + "','" + check.BranchName + "','" + check.AccountNo + "','" + check.AccountNoWithHypen + "','" +
        //              check.Name1 + "','" + check.Name2 + "','" + check.ChkType + "','" + check.ChequeName + "','" + check.StartingSerial + "','" +
        //              check.EndingSerial + "','" + _DrNumber + "','" + _deliveryDate.ToString("yyyy-MM-dd") + "','" + _username + "','" +
        //              check.Batch.TrimEnd() + "','" + _packNumber + "');";
        //                cmd = new MySqlCommand(Sql, myConnect);
        //                cmd.ExecuteNonQuery();


        //            }
        //        }
        //        else if (gClient.DataBaseName == "pnb_history")
        //        {
        //            foreach (var check in _listb)
        //            {
        //                Sql = "Insert into " + gClient.DataBaseName + " (BRSTN,BranchName,AccountNo,AcctNoWithHyphen,Name1,Name2,ChkType,ChequeName," +
        //                   "StartingSerial,EndingSerial,DRNumber,DeliveryDate,username,batch,PackNumber,BranchCode,OldBranchCode )"
        //              + "VALUES('" + check.BRSTN + "','" + check.BranchName + "','" + check.AccountNo + "','" + check.AccountNoWithHypen + "','" +
        //              check.Name1 + "','" + check.Name2 + "','" + check.ChkType + "','" + check.ChequeName + "','" + check.StartingSerial + "','" +
        //              check.EndingSerial + "','" + _DrNumber + "','" + _deliveryDate.ToString("yyyy-MM-dd") + "','" + _username + "','" +
        //              check.Batch.TrimEnd() + "','" + _packNumber + "','" + check.BranchCode + "','" + check.OldBranchCode + "');";
        //                cmd = new MySqlCommand(Sql, myConnect);
        //                cmd.ExecuteNonQuery();


        //            }
        //        }

        //            counter++;
        //            if (counter == 10)
        //            {
        //               counter = 0;
        //            }

        //    }

        //    DBClosed();
        //    return;

        //}
        public int GetLastDRFromHistory()
        {
            int LdrNumber = 0;
            Sql = "Select Max(DrNumber) from " + gClient.DataBaseName;
            DBConnect();
            cmd = new MySqlCommand(Sql, myConnect);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                LdrNumber = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0;
            }
            reader.Close();
            DBClosed();

            return LdrNumber;
        }
        public string GetDRDetails(string _batch, List<TempModel> list)
        {

            DBConnect();
            Sql = "SELECT DRNumber, PackNumber, BRSTN, ChkType, BranchName, COUNT(BRSTN)," +
                 "MIN(StartingSerial), MAX(EndingSerial),ChequeName, Batch,username,BranchCode,OldBranchCode,location,PurchaseOrderNumber FROM " +
                 gClient.DataBaseName + " WHERE  Batch = '" + _batch + "' GROUP BY DRNumber, BRSTN, ChkType, BranchName," +
                 "ChequeName ,Batch ORDER BY DRNumber, PackNumber;";

            cmd = new MySqlCommand(Sql, myConnect);
            MySqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                TempModel order = new TempModel();
                order.DrNumber = !myReader.IsDBNull(0) ? myReader.GetString(0) : "";
                order.PackNumber = !myReader.IsDBNull(1) ? myReader.GetString(1) : "";
                order.BRSTN = !myReader.IsDBNull(2) ? myReader.GetString(2) : "";
                order.ChkType = !myReader.IsDBNull(3) ? myReader.GetString(3) : "";
                order.BranchName = !myReader.IsDBNull(4) ? myReader.GetString(4) : "";
                order.Qty = !myReader.IsDBNull(5) ? myReader.GetInt32(5) : 0;
                order.StartingSerial = !myReader.IsDBNull(6) ? myReader.GetString(6) : "";
                order.EndingSerial = !myReader.IsDBNull(7) ? myReader.GetString(7) : "";
                order.ChequeName = !myReader.IsDBNull(8) ? myReader.GetString(8) : "";
                order.Batch = !myReader.IsDBNull(9) ? myReader.GetString(9) : "";
                order.username = !myReader.IsDBNull(10) ? myReader.GetString(10) : "";
                order.BranchCode = !myReader.IsDBNull(11) ? myReader.GetString(11) : "";
                order.OldBranchCode = !myReader.IsDBNull(12) ? myReader.GetString(12) : "";
                order.Location = !myReader.IsDBNull(13) ? myReader.GetString(13) : "";
                order.PONumber = !myReader.IsDBNull(14) ? myReader.GetInt32(14) : 0;

                list.Add(order);
            }
            DBClosed();
            DBConnect();
            string sqldel = "Delete from producers_tempdatadr;";
            MySqlCommand comdel = new MySqlCommand(sqldel, myConnect);
            comdel.ExecuteNonQuery();

            DBClosed();
            DBConnect();
            for (int i = 0; i < list.Count; i++)
            {

                string sql2 = "Insert into producers_tempdatadr (DRNumber,PackNumber,BRSTN, ChkType, BranchName,Qty,StartingSerial," +
                              "EndingSerial,ChequeName,Batch,username,BranchCode,OldBranchCode,Location,PONumber) Values('" + list[i].DrNumber + "','" + list[i].PackNumber +
                              "','" + list[i].BRSTN + "','" + list[i].ChkType + "','" + list[i].BranchName + "'," + list[i].Qty +
                              ",'" + list[i].StartingSerial + "','" + list[i].EndingSerial + "','" + list[i].ChequeName + "','" +
                              list[i].Batch + "','" + list[i].username + "','" + list[i].BranchCode + "','" + list[i].OldBranchCode + "','" +
                              list[i].Location+ "'," +list[i].PONumber +");";
                MySqlCommand cmd2 = new MySqlCommand(sql2, myConnect);
                cmd2.ExecuteNonQuery();
            }

            DBClosed();
            return _batch;
        }
        public void GetBankTables()
        {
            //    int counter = 0;

            Sql = "Select Distinct(DatabaseName) from Clients";
            DBConnect();
            cmd = new MySqlCommand(Sql, myConnect);
            MySqlDataReader myReader = cmd.ExecuteReader();

            while (myReader.Read())
            {
                string data;
                data = myReader.GetString(0);


                db.Add(data);

            }

            DBClosed();
            //return _DrNumber; 
        }
        public List<Int64> GetMaxDr(List<Int64> _Dr)
        {
            GetBankTables();

            DBConnect();
            Int64 dr = 0;
            foreach (var item in db)
            {


                Sql = "Select Max(DrNumber) from " + item + " where Date > '2020-12-01'";
                cmd = new MySqlCommand(Sql, myConnect);
                MySqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    dr = !read.IsDBNull(0) ? read.GetInt64(0) : 0;

                    _Dr.Add(dr);
                }

                read.Close();
            }
            DBClosed();


            return _Dr;
        }
        private void InsertToMaxTB()
        {
            Int64 _drNumber = 0;
            string _salesInvoice = "", _docStamp = "";
            string Sql2;
            GetBankTables();
            DBConnect();

            foreach (var item in db)
            {


                Sql2 = "Select Max(DrNumber) , Max(SalesInvoice), Max(DocStamp) from " + item.TrimEnd();
                MySqlCommand cmd2 = new MySqlCommand(Sql2, myConnect);
                MySqlDataReader reader = cmd2.ExecuteReader();

                while (reader.Read())
                {
                    _drNumber = !reader.IsDBNull(0) ? reader.GetInt64(0) : 0;
                    _salesInvoice = !reader.IsDBNull(1) ? reader.GetString(1) : "";
                    _docStamp = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                }


                reader.Close();
                Sql = "Insert into MaxTb (DrNumber,SalesInvoice,Docstamp)values('" + _drNumber.ToString() + "','" + _salesInvoice + "','" + _docStamp + "');";
                cmd = new MySqlCommand(Sql, myConnect);
                cmd.ExecuteNonQuery();

            }


            DBClosed();
            //  return _drNumber;
        }
        public List<Int32> GetMaxPackNumber()
        {
            GetBankTables();
            //InsertToMaxTB();
            DBConnect();
            List<Int32> pack = new List<Int32>();
            Int32 dr = 0;
            foreach (var item in db)
            {


                Sql = "Select Max(PackNumber) from " + item + " where Date >= '2020-12-01'";

                cmd = new MySqlCommand(Sql, myConnect);
                MySqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    dr = !read.IsDBNull(0) ? read.GetInt32(0) : 0;
                    pack.Add(dr);
                }

                read.Close();
            }

            DBClosed();


            return pack;
        }

        public List<TempModel> GetStickerDetails(List<TempModel> _temp, string _batch)
        {
            try
            {
                Sql = "SELECT BranchName, BRSTN, ChkType,MIN(StartingSerial), MAX(EndingSerial), Count(ChkType) " +
                      "FROM " + gClient.DataBaseName + " WHERE Batch = '" + _batch + "'" +
                       " GROUP BY ChkType,BranchName ORDER BY ChkType,BranchName";
                DBConnect();
                cmd = new MySqlCommand(Sql, myConnect);
                MySqlDataReader myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    TempModel t = new TempModel
                    {

                        BranchName = !myReader.IsDBNull(0) ? myReader.GetString(0) : "",
                        BRSTN = !myReader.IsDBNull(1) ? myReader.GetString(1) : "",
                        ChkType = !myReader.IsDBNull(2) ? myReader.GetString(2) : "",
                        StartingSerial = !myReader.IsDBNull(3) ? myReader.GetString(3) : "",
                        EndingSerial = !myReader.IsDBNull(4) ? myReader.GetString(4) : "",
                        Qty = !myReader.IsDBNull(5) ? myReader.GetInt32(5) : 0,
                        //ChequeName = !myReader.IsDBNull(6) ? myReader.GetString(6): ""


                    };
                    _temp.Add(t);
                }
                DBClosed();
                DBConnect();
                string sqldel = "Delete from producers_sticker;";
                MySqlCommand comdel = new MySqlCommand(sqldel, myConnect);
                comdel.ExecuteNonQuery();

                DBClosed();
                DBConnect();
                //_temp.OrderBy(b => b.BranchName).ToList();
                //var sorted = (from c in _temp
                //              orderby c.ChkType,c.BranchName
                //                       ascending
                //              select c).ToList();
                // int dataCount = 0;
                string Type = "";
                int licnt = 1;

                for (int r = 0; r < _temp.Count; r++)
                {
                    if (_temp[r].ChkType == "A")
                        Type = "Personal";
                    else if (_temp[r].ChkType == "B")
                        Type = "Commercial";

                    if (licnt == 1)
                    {
                        string sql2 = "Insert into producers_sticker (Batch,BRSTN,BranchName,Qty,ChkType,ChequeName,StartingSerial,EndingSerial)" +
                                      "values('" + _batch + "','" + _temp[r].BRSTN + "','" + _temp[r].BranchName + "'," + _temp[r].Qty + ",'" + _temp[r].ChkType +
                                      "','" + Type + "','" + _temp[r].StartingSerial + "','" + _temp[r].EndingSerial + "');";


                        MySqlCommand cmd2 = new MySqlCommand(sql2, myConnect);
                        cmd2.ExecuteNonQuery();
                        licnt++;
                    }
                    else if (licnt == 2)
                    {
                        string sql2 = "Update producers_sticker set BRSTN2 = '" + _temp[r].BRSTN + "',BranchName2 = '" + _temp[r].BranchName + "',Qty2 = " + _temp[r].Qty +
                                      ",ChkType2 = '" + _temp[r].ChkType + "',ChequeName2 = '" + Type + "',StartingSerial2 = '" + _temp[r].StartingSerial +
                                      "',EndingSerial2 = '" + _temp[r].EndingSerial + "' where BRSTN = '" + _temp[r - 1].BRSTN + "' and ChkType = '" + _temp[r - 1].ChkType + "';";
                        MySqlCommand cmd2 = new MySqlCommand(sql2, myConnect);
                        cmd2.ExecuteNonQuery();
                        licnt++;
                    }
                    else if (licnt == 3)
                    {
                        string sql2 = "Update producers_sticker set BRSTN3 = '" + _temp[r].BRSTN + "',BranchName3 = '" + _temp[r].BranchName + "',Qty3 = " + _temp[r].Qty +
                                      ",ChkType3 = '" + _temp[r].ChkType + "',ChequeName3 = '" + Type + "',StartingSerial3 = '" + _temp[r].StartingSerial +
                                      "',EndingSerial3 = '" + _temp[r].EndingSerial + "' where BRSTN2 = '" + _temp[r - 1].BRSTN + "' and ChkType2 = '" + _temp[r - 1].ChkType + "';";
                        MySqlCommand cmd2 = new MySqlCommand(sql2, myConnect);
                        cmd2.ExecuteNonQuery();
                        licnt = 1;
                    }

                }
                //for (int z = 0; z < _temp.Count; z++)
                //{

                //    if (licnt == 3)
                //    {
                //        //if ((z % 3) == 0)
                //        //{

                //        if (_temp[z + dataCount].ChkType == "A")
                //            Type = "PERSONAL";
                //        else
                //            Type = "COMMERCIAL";

                //        string sql2 = "Insert into producers_sticker (Batch,BRSTN,BranchName,Qty,ChkType,ChequeName,StartingSerial,EndingSerial,BRSTN2,"
                //               + "BranchName2,Qty2,ChkType2,ChequeName2,StartingSerial2,EndingSerial2,BRSTN3,BranchName3,Qty3,ChkType3,ChequeName3,StartingSerial3,EndingSerial3)"
                //               + "Values('" + _temp[z].Batch + "','" + _temp[z].BRSTN + "','" + _temp[z].BranchName + "'," + _temp[z].Qty
                //               + ",'" + _temp[z].ChkType + "'," + "'" + Type + "','" + _temp[z].StartingSerial + "','" + _temp[z].EndingSerial
                //               + "','" + _temp[z + 1].BRSTN + "','" + _temp[z + 1].BranchName + "'," + _temp[z + 1].Qty + ",'" + _temp[z + 1].ChkType + "','" + Type
                //               + "','" + _temp[z + 1].StartingSerial + "','" + _temp[z + 1].EndingSerial + "','" + _temp[z + 2].BRSTN + "','" + _temp[z + 2].BranchName + "',"
                //               + _temp[z + 2].Qty + ",'" + _temp[z + 2].ChkType + "','" + Type + "','" + _temp[z + 2].StartingSerial + "','" + _temp[z + 2].EndingSerial + "');";


                //        MySqlCommand cmd2 = new MySqlCommand(sql2, myConnect);
                //        cmd2.ExecuteNonQuery();


                //        if (licnt == 3) licnt = 1;
                //    }
                //    else
                //    {
                //        licnt++;

                //        //if (z == _temp.Count)
                //        //{
                //        //    string sql2 = "Insert into producers_sticker (Batch,BRSTN,BranchName,Qty,ChkType,ChequeName,StartingSerial,EndingSerial,BRSTN2,"
                //        //       + "BranchName2,Qty2,ChkType2,ChequeName2,StartingSerial2,EndingSerial2,BRSTN3,BranchName3,Qty3,ChkType3,ChequeName3,StartingSerial3,EndingSerial3)"
                //        //       + "Values('" + _temp[z].Batch + "','" + _temp[z].BRSTN + "','" + _temp[z].BranchName + "'," + _temp[z].Qty
                //        //       + ",'" + _temp[z].ChkType + "'," + "'" + Type + "','" + _temp[z].StartingSerial + "','" + _temp[z].EndingSerial
                //        //       + "','" + _temp[z + 1].BRSTN + "','" + _temp[z + 1].BranchName + "'," + _temp[z + 1].Qty + ",'" + _temp[z + 1].ChkType + "','" + Type
                //        //       + "','" + _temp[z + 1].StartingSerial + "','" + _temp[z + 1].EndingSerial + "','" + _temp[z + 2].BRSTN + "','" + _temp[z + 2].BranchName + "',"
                //        //       + _temp[z + 2].Qty + ",'" + _temp[z + 2].ChkType + "','" + Type + "','" + _temp[z + 2].StartingSerial + "','" + _temp[z + 2].EndingSerial + "');";
                //        //    MySqlCommand cmd2 = new MySqlCommand(sql2, myConnect);
                //        //    cmd2.ExecuteNonQuery();
                //        //}
                //    }


                //}

                DBClosed();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return _temp;
        }
        public string FillCRReportParameters()
        {
            string reportPath = "";
            try
            {
                
                if (Debugger.IsAttached)
                {
                    if (gClient.DataBaseName == "")
                        MessageBox.Show("There is no table selected!");
                    else
                    {

                        if (gClient.DataBaseName == "producers_history")
                        {
                            if (RecentBatch.report == "STICKER" || DeliveryReport.report == "STICKER")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\Stickers.rpt";
                            else if (RecentBatch.report == "Packing" || DeliveryReport.report == "Packing")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\PackingReport.rpt";
                            else if (RecentBatch.report == "DR" || DeliveryReport.report == "DR")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\DeliveryReceipt.rpt";
                            
                        }
                        else if (gClient.DataBaseName == "pnb_history")
                        {
                            if (RecentBatch.report == "STICKER" || DeliveryReport.report == "STICKER")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\Stickers.rpt";

                            else if (RecentBatch.report == "Packing" || DeliveryReport.report == "Packing")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\PackingReport.rpt";
                            else if (RecentBatch.report == "DOC" || DeliveryReport.report == "DOC")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\DocStamp.rpt";
                            
                            else if (RecentBatch.report == "DRR" || DeliveryReport.report == "DRR")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\PNBDeliveryReport.rpt";
                            else if (RecentBatch.report == "DR" || DeliveryReport.report == "DR")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\PNBDeliveryReceipt.rpt";

                        }
                        //if (RecentBatch.report == "STICKER" || DeliveryReport.report == "STICKER")
                        //    reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Stickers.rpt";
                        //else if (RecentBatch.report == "Packing" || DeliveryReport.report == "Packing")
                        //    reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\PackingReport.rpt";
                    }


                }
                else
                {
                    if (gClient.DataBaseName == "")
                        MessageBox.Show("There is no table selected!");
                    else
                    {

                        if (gClient.DataBaseName == "producers_history")
                        {

                            if (RecentBatch.report == "STICKER" || DeliveryReport.report == "STICKER")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\Stickers.rpt";
                            else if (RecentBatch.report == "Packing" || DeliveryReport.report == "Packing")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\PackingReport.rpt";
                            else if (RecentBatch.report == "DR" || DeliveryReport.report == "DR")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\DeliveryReceipt.rpt";
                        }
                        else if (gClient.DataBaseName == "pnb_history")
                        {
                            if (RecentBatch.report == "STICKER" || DeliveryReport.report == "STICKER")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\Stickers.rpt";
                            else if (RecentBatch.report == "Packing" || DeliveryReport.report == "Packing")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\PackingReport.rpt";
                            else if (RecentBatch.report == "DOC" || DeliveryReport.report == "DOC")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\DocStamp.rpt";
                           
                            else if (RecentBatch.report == "DRR" || DeliveryReport.report == "DRR")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\PNBDeliveryReport.rpt";
                            else if (RecentBatch.report == "DR" || DeliveryReport.report == "DR")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\PNBDeliveryReceipt.rpt";
                        }
                    }

                }

                return reportPath;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, error.InnerException.Message);
                return reportPath;
            }
        }

        public string DisplayAllBatches(string _batch, List<TempModel> _temp)
        {
            try
            {
                DBConnect();
                if (gClient.DataBaseName != "producers_history")
                {
                    Sql = "select batch, chequename, ChkType, deliverydate, count(ChkType) as Quantity,SalesInvoice,DocStampNumber from  " + gClient.DataBaseName +
                          " where DrNumber is not null  and (Batch Like '%" + _batch + "%' OR SalesInvoice Like '%" + _batch + "%' OR DocStampNumber Like '%" + _batch + "%' )" +
                          " group by location,batch, chequename, ChkType";
                   
                }
                else
                {
                    Sql = "select batch, chequename, ChkType, deliverydate, count(ChkType) as Quantity,SalesInvoice,DocStampNumber from  " + gClient.DataBaseName +
                           " where DrNumber is not null  and (Batch Like '%" + _batch + "%' OR SalesInvoice Like '%" + _batch + "%' OR DocStampNumber Like '%" + _batch + "%' )" +
                           " group by batch, chequename, ChkType";
                }
                cmd = new MySqlCommand(Sql, myConnect);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TempModel t = new TempModel
                    {
                        Batch = !reader.IsDBNull(0) ? reader.GetString(0) : "",
                        ChequeName = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                        ChkType = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                        DeliveryDate = !reader.IsDBNull(3) ? reader.GetDateTime(3) : DateTime.Now,
                        Qty = !reader.IsDBNull(4) ? reader.GetInt32(4) : 0,
                        SalesInvoice = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0,
                        DocStampNumber = !reader.IsDBNull(6) ? reader.GetInt32(6) : 0

                    };
                    _temp.Add(t);
                }
                reader.Close();
                DBClosed();
                return _batch;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, error.Source);
                return _batch;
            }
            
        }
        public string DisplayAllBatches2(string _batch, List<TempModel> _temp)
        {
            try
            {
                DBConnect();
               
                    Sql = "select batch, chequename, ChkType, deliverydate, count(ChkType) as Quantity,SalesInvoice,DocStampNumber,Date from  " + gClient.DataBaseName +
                           " where DrNumber is not null  and (Batch Like '%" + _batch + "%' OR SalesInvoice Like '%" + _batch + "%' OR DocStampNumber Like '%" + _batch + "%' )" +
                           " group by batch order by Date desc";
                
                cmd = new MySqlCommand(Sql, myConnect);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TempModel t = new TempModel
                    {
                        Batch = !reader.IsDBNull(0) ? reader.GetString(0) : "",
                        ChequeName = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                        ChkType = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                        DeliveryDate = !reader.IsDBNull(3) ? reader.GetDateTime(3) : DateTime.Now,
                        Qty = !reader.IsDBNull(4) ? reader.GetInt32(4) : 0,
                        SalesInvoice = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0,
                        DocStampNumber = !reader.IsDBNull(6) ? reader.GetInt32(6) : 0,
                        DateProcessed = !reader.IsDBNull(3) ? reader.GetDateTime(3) : DateTime.Now


                    };
                    _temp.Add(t);
                }
                reader.Close();
                DBClosed();
                return _batch;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, error.Source);
                return _batch;
            }

        }
        public string DisplayAllBatchData(string _batch, List<TempModel> _temp)
        {
            try
            {
                DBConnect();

                Sql = "select batch, chequename, ChkType, deliverydate, count(ChkType) as Quantity,SalesInvoice,DocStampNumber,Date," +
                        "DrNumber,PrimaryKey from  " + gClient.DataBaseName +
                        " where DrNumber is not null  and Batch = '" + _batch + "'" +
                        " group by batch,DrNumber,ChkType order by DrNumber,ChkType ";

                cmd = new MySqlCommand(Sql, myConnect);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TempModel t = new TempModel
                    {
                        Batch = !reader.IsDBNull(0) ? reader.GetString(0) : "",
                        ChequeName = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                        ChkType = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                        DeliveryDate = !reader.IsDBNull(3) ? reader.GetDateTime(3) : DateTime.Now,
                        Qty = !reader.IsDBNull(4) ? reader.GetInt32(4) : 0,
                        SalesInvoice = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0,
                        DocStampNumber = !reader.IsDBNull(6) ? reader.GetInt32(6) : 0,
                        DateProcessed = !reader.IsDBNull(7) ? reader.GetDateTime(7) : DateTime.Now,
                        DrNumber = !reader.IsDBNull(8) ? reader.GetString(8) : "",
                        PrimaryKey = !reader.IsDBNull(9) ? reader.GetInt32(9) : 0


                    };
                    _temp.Add(t);
                }
                reader.Close();
                DBClosed();
                return _batch;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, error.Source);
                return _batch;
            }

        }
        public bool CheckBatchifExisted(string _batch)
        {
            string batch = "";
            Sql = "Select Distinct(Batch) from " + gClient.DataBaseName + " where Batch  = '" + _batch + "'";
            DBConnect();
            cmd = new MySqlCommand(Sql, myConnect);

            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                batch = reader.GetString(0);
            }

            DBClosed();
            if (batch != "")
            {
                return true;
            }
            else
                return false;

        }
        public static string ErrorMessage(string _errorMessage)
        {
            try
            {

                StreamWriter sw = new StreamWriter(Application.StartupPath + "\\ErrorMessage.txt");
                sw.WriteLine(_errorMessage);
                sw.Close();
                return _errorMessage;

            }
            catch (Exception error)
            {
                return error.Message;
            }
        }
        public void DeleteItems(List<TempModel> _temp)
        {

            for (int i = 0; i < _temp.Count; i++)
            {
                Sql = "insert into " + gClient.CancelledTable + " select * from " + gClient.DataBaseName + " where DRNumber = " + _temp[i].DrNumber + ";";
                DBConnect();
                cmd = new MySqlCommand(Sql, myConnect);
                cmd.ExecuteNonQuery();
                Sql = "Delete from " + gClient.DataBaseName + " where DRNumber = " + _temp[i].DrNumber ;
                DBConnect();
                cmd = new MySqlCommand(Sql, myConnect);
                cmd.ExecuteNonQuery();
                DBClosed();
            }
               
            
            return ;
        }
        public void UpdateItem(TempModel _temp, string _newDR)
        {
            Sql = "insert into " + gClient.CancelledTable + " select * from " + gClient.DataBaseName + " where DRNumber = " + _temp.DrNumber + ";";
            DBConnect();
            cmd = new MySqlCommand(Sql, myConnect);
            cmd.ExecuteNonQuery();
            DBClosed();
            Sql = "Update " + gClient.DataBaseName + " set DRNumber = '"+_newDR+" ' where DRNumber = '" + _temp.DrNumber + "'";
                DBConnect();
                cmd = new MySqlCommand(Sql, myConnect);
                cmd.ExecuteNonQuery();
                DBClosed();

            return;
        }
        public void GetDr(string _batch, List<TempModel> _DR)
        {
            Sql = " SELECT MIN(DRNumber) FROM producers_history where Batch ='" + _batch + "';";
            DBConnect();
            cmd = new MySqlCommand(Sql, myConnect);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TempModel temp = new TempModel
                {
                    DrNumber = !reader.IsDBNull(0) ? reader.GetString(0) : ""
                };
                _DR.Add(temp);
            }
            reader.Close();
            DBClosed();
            return;
        }
  
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {

            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        public List<SalesInvoiceModel> ListofProcessSI(List<SalesInvoiceModel> _SI)
        {
            Sql = "Select SalesInvoiceDate,SalesInvoice, Count(ChkType) as Quantity,ChkType, ChequeName, Batch from " + gClient.DataBaseName +
                    " Group by SalesInvoice,ChkType order by SalesInvoice, ChkType;";
            DBConnect();
            cmd = new MySqlCommand(Sql, myConnect);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                SalesInvoiceModel sales = new SalesInvoiceModel
                {
                    salesInvoiceDate = !reader.IsDBNull(0) ? reader.GetDateTime(0) : DateTime.Now,
                    salesInvoiceNumber = !reader.IsDBNull(1) ? reader.GetDouble(1) : 0,
                    Quantity = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0,
                    checkType = !reader.IsDBNull(3) ? reader.GetString(3) : "",
                    checkName = !reader.IsDBNull(4) ? reader.GetString(4) : "",
                    Batch = !reader.IsDBNull(5) ? reader.GetString(5) : ""


                };
                _SI.Add(sales);

            }
            reader.Close();
            DBClosed();
            return _SI;
        }

        public string ContcatSalesInvoice(string batch, string checktype, DateTime salesinvoicedate)
        {

            DataTable dt = new DataTable();

            Sql = "select group_concat(distinct(SalesInvoice) separator ', ') from " + gClient.DataBaseName + " " +
           "WHERE salesinvoice is not null " +
           "and batch = '" + batch + "' " +
           "and chktype = '" + checktype + "'; ";
            //"and salesinvoicedate = '" + salesinvoicedate.ToString("yyyy-MM-dd") + "';";
            DBConnect();
            MySqlCommand cmd = new MySqlCommand(Sql, myConnect);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
            da.Fill(dt);

            string siList = dt.Rows[0].Field<string>(0).ToString(); // get concatenated delivery number list 
            DBClosed();
            return siList is null ? "" : siList; // return concatenated delivery number list if not null


        }
        //public string ConcatBatches(string batch, string checktype, DateTime deliveryDate)
        //{

        //    DataTable dt = new DataTable();

        //    Sql = "select group_concat(distinct(Batches) separator ', ') from " + gClient.DataBaseName + " " +
        //   "WHERE salesinvoice is not null " +
        //   "and batch = '" + batch + "' " +
        //   "and chktype = '" + checktype + "' " +
        //   "and deliverydate = '" + deliveryDate.ToString("yyyy-MM-dd") + "';";

        //    MySqlCommand cmd = new MySqlCommand(Sql, myConnect);
        //    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        //    cmd.ExecuteNonQuery();
        //    da.Fill(dt);
        //    string siList = dt.Rows[0].Field<string>(0).ToString(); // get concatenated delivery number list 
        //    return siList is null ? "" : siList; // return concatenated delivery number list if not null


        //}
        public PriceListModel GetPriceList(PriceListModel price, string chkType)
        {
            try
            {
                Sql = "Select BankCode, Description, Docstamp from " + gClient.PriceListTable + " where FinalChkType ='" + chkType + "'; ";
                DBConnect();
                cmd = new MySqlCommand(Sql, myConnect);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    price.Bank = !reader.IsDBNull(0) ? reader.GetString(0) : "";
                    price.ChequeDescription = !reader.IsDBNull(1) ? reader.GetString(1) : "";
                    price.DocStampPrice = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0;
                    // price.unitprice = !reader.IsDBNull(3) ? reader.GetDouble(3) : 0;


                }
                reader.Close();
                DBClosed();
                return price;
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message, error.Source);
                return price;
            }
        }
        public void UpdateDocstamp(List<DocStampModel> _docStamp)
        {
            //try
            //{

            //_docStamp.ForEach(r =>
            //{
            //    DBConnect();
            //    Sql = "Update " + gClient.DataBaseName + " set DocStamp = " + r.DocStampPrice + ", DocStampNumber = " + r.DocStampNumber +
            //        ", Date_DocStamp = '" + r.DocStampDate.ToString("yyyy-MM-dd") + "',Username_DocStamp ='" + r.PreparedBy +
            //        "', CheckedByDS = '" + r.CheckedBy + "'  where SalesInvoice = " + r.SalesInvoiceNumber + " and ChkType = '" + r.ChkType + "' and Location ='"+r.Location+"';";
            //    cmd = new MySqlCommand(Sql, myConnect);
            //    cmd.ExecuteNonQuery();
            //    DBClosed();

            //});


                DBConnect();
            for (int i = 0; i < _docStamp.Count; i++)
            {
                Sql = "Update " + gClient.DataBaseName + " set DocStamp = " + _docStamp[i].DocStampPrice + ", DocStampNumber = " + _docStamp[i].DocStampNumber +
                    ", Date_DocStamp = '" + _docStamp[i].DocStampDate.ToString("yyyy-MM-dd") + "',Username_DocStamp ='" + _docStamp[i].PreparedBy +
                    "', CheckedByDS = '" + _docStamp[i].CheckedBy + "'  where SalesInvoice = " + _docStamp[i].SalesInvoiceNumber + " and ChkType = '" +
                    _docStamp[i].ChkType + "' and Location ='" + _docStamp[i].Location + "';";
                cmd = new MySqlCommand(Sql, myConnect);
                cmd.ExecuteNonQuery();
            }
                
                DBClosed();



            return;
            //}
            //catch(Exception error)
            //{
            //    MessageBox.Show(error.Message, error.Source);
            //    return;
            //}
        }

        public string DisplayAllSalesInvoice(string _batch, List<TempModel> _temp)
        {
            DBConnect();
            if (gClient.DataBaseName != "producers_history")
            {
                Sql = "Select SalesInvoiceDate,SalesInvoice, Count(ChkType) as Quantity,ChkType, ChequeName, Batch,location from  " + gClient.DataBaseName +
                            " where (DocStampNumber is null or DocStampNumber = 0) and SalesInvoice != 0  and (Batch Like '%" + _batch + "%' OR SalesInvoice Like '%" + _batch + "%') " +
                            "group by location,SalesInvoice, ChkType order by SalesInvoice, Batch,ChkType;";
            }
            else
            {
                Sql = "Select SalesInvoiceDate,SalesInvoice, Count(ChkType) as Quantity,ChkType, ChequeName, Batch,location from  " + gClient.DataBaseName +
                    " where (DocStampNumber is null or DocStampNumber = 0 ) and SalesInvoice != 0  and (Batch Like '%" + _batch + "%' OR SalesInvoice Like '%" + _batch + "%') " +
                    "group by SalesInvoice, ChkType order by SalesInvoice, Batch,ChkType;";
            }
            cmd = new MySqlCommand(Sql, myConnect);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TempModel t = new TempModel
                {
                    SI_Date = !reader.IsDBNull(0) ? reader.GetDateTime(0) : DateTime.Now,
                    SalesInvoice = !reader.IsDBNull(1) ? reader.GetInt32(1) : 0,
                    Qty = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0,
                    ChkType = !reader.IsDBNull(3) ? reader.GetString(3) : "",
                    ChequeName = !reader.IsDBNull(4) ? reader.GetString(4) : "",
                    Batch = !reader.IsDBNull(5) ? reader.GetString(5) : "",
                    Location = !reader.IsDBNull(6) ? reader.GetString(6):""

                };
                _temp.Add(t);
            }
            reader.Close();
            DBClosed();

            return _batch;
        }
        //public void GetDocStampDetails(List<DocStampModel> _temp, int _docStampNumber)
        //{
         public void GetDocStampDetails(List<DocStampModel> _temp, int _docStampNumber)
         {
            //Orginal Query
            Sql = "Select P.BankCode, DocStampNumber,SalesInvoice,Count(ChkType) as Quantity,ChkType, P.Description, H.DocStamp, " +
                  "Username_DocStamp, CheckedByDS,PurchaseOrderNumber,P.QuantityOnHand,Batch," +
                  "(Count(ChkType) * H.DocStamp) as TotalAmount,location from " + gClient.DataBaseName +
                  " H left join " + gClient.PriceListTable + "  P on H.Bank = P.BankCode and H.ChkType = P.FinalChkType" +
                  " where  DocStampNumber= " + _docStampNumber + " Group by DocStampNumber,ChkType order by DocStampNumber, ChkType";
            //_docStampNumber.ForEach(x => { 
            //    Sql = "Select P.BankCode, DocStampNumber,SalesInvoice,Count(ChkType) as Quantity,ChkType, P.CDescription, H.DocStamp, " + //Based on pnb requirements
            //      "Username_DocStamp, CheckedByDS,PurchaseOrderNumber,P.QuantityOnHand,Batch," +
            //      "(Count(ChkType) * H.DocStamp) as TotalAmount,location from " + gClient.DataBaseName +
            //      " H left join " + gClient.PriceListTable + "  P on H.Bank = P.BankCode and H.ChkType = P.FinalChkType" +
            //      " where  DocStampNumber= " + x + " Group by DocStampNumber,location,ChkType order by DocStampNumber, ChkType";
            DBConnect();
                cmd = new MySqlCommand(Sql, myConnect);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                DocStampModel doc = new DocStampModel();

                    doc.BankCode = !reader.IsDBNull(0) ? reader.GetString(0) : "";
                    doc.DocStampNumber = !reader.IsDBNull(1) ? reader.GetInt32(1) : 0;
                    doc.SalesInvoiceNumber = !reader.IsDBNull(2) ? reader.GetString(2) : "0";
                    doc.TotalQuantity = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0;
                    doc.ChkType = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    doc.DocDesc = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                //  TotalAmount = !reader.IsDBNull(5) ? reader.GetDouble(5) : 0,
                    doc.DocStampPrice = !reader.IsDBNull(6) ? reader.GetInt32(6) : 0;
                    doc.PreparedBy = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    doc.CheckedBy = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    doc.POorder = !reader.IsDBNull(9) ? reader.GetInt32(9) : 0;
                    doc.QuantityOnHand = !reader.IsDBNull(10) ? reader.GetInt32(10) : 0;
                    doc.batches = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                    doc.TotalAmount = !reader.IsDBNull(12) ? reader.GetDouble(12) : 0;
                    doc.Location = !reader.IsDBNull(13) ? reader.GetString(13) : "";
                
                    _temp.Add(doc);
                }
                    reader.Close();
                DBClosed();
                DBConnect();
                string Sql1 = "Delete from " + gClient.DocStampTempTable;
                cmd = new MySqlCommand(Sql1, myConnect);
                cmd.ExecuteNonQuery();
                DBClosed();
                DBConnect();
                _temp.ForEach(d =>
                {
                string Sql2 = "Insert into " + gClient.DocStampTempTable + "(Bank, DocStampNumber,SalesInvoice,Quantity,ChkType, ChequeDesc, DocStampPrice, " +
                            "PreparedBy, CheckedBy, PONumber,BalanceOrder,Batch,TotalAmount,Location)Values('" + d.BankCode + "'," + d.DocStampNumber +
                            ", " + d.SalesInvoiceNumber + "," + d.TotalQuantity + ",'" + d.ChkType + "','" + d.DocDesc.Replace("'","''") +
                            "'," + d.DocStampPrice + ",'" + d.PreparedBy + "','" + d.CheckedBy + "',"+d.POorder+","+d.QuantityOnHand+
                            ",'" + d.batches + "',"+d.TotalAmount+",'"+d.Location+"')";
                MySqlCommand cmd2 = new MySqlCommand(Sql2, myConnect);
                cmd2.ExecuteNonQuery();
                });
          //  });
            DBClosed();
            return;
        }
        public void GetUsers(List<UserListModel> _users)
        {
            Sql = "Select Username from userlist";
            DBConnect();
            cmd = new MySqlCommand(Sql, myConnect);
            MySqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                UserListModel user = new UserListModel
                {
                    UserName = !reader.IsDBNull(0) ? reader.GetString(0) : ""
                };
                _users.Add(user);
            }
            reader.Close();
            DBClosed();
            return;
        }
        public List<Int32> GetMaxDocStamp()
        {
            GetBankTables();
            //InsertToMaxTB();
            DBConnect();
            List<Int32> pack = new List<Int32>();
            Int32 dr = 0;
            foreach (var item in db)
            {


                Sql = "Select Max(DocStampNumber) from " + item + " where Date >= '2020-12-01'";

                cmd = new MySqlCommand(Sql, myConnect);
                MySqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    dr = !read.IsDBNull(0) ? read.GetInt32(0) : 0;
                    pack.Add(dr);
                }

                read.Close();
            }

            DBClosed();


            return pack;
        }
        public void GetBranchLocation(BranchesModel _branches , string _branchCode)
        {
            Sql = "Select LocationFlag  from " + gClient.BranchesTable + " where BranchCode = " + _branchCode + ";";
            DBConnect();
            cmd = new MySqlCommand(Sql, myConnect);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                _branches.Flag = reader.GetInt32(0);
            }
            reader.Close();
            DBClosed();

        }
        private void ByLocationAndType(TypeofCheckModel _checks, int _DrNumber, DateTime _deliveryDate, string _username, int _packNumber)
        {
            int counter = 0;
            DBConnect();

            if (_checks.Regular_Personal_Direct.Count > 0)
            {
                // generating DR number per Branches with ChkType A
                var dBranch = _checks.Regular_Personal_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Regular_Personal_Direct.Where(g => g.BRSTN == y).ToList();
                    dRecord.ForEach(r =>
                    {
                        Script(gClient.DataBaseName, r,_DrNumber,_deliveryDate,_username,_packNumber);
                        //Sql = "Insert into " + gClient.DataBaseName + " (BRSTN,BranchName,AccountNo,AcctNoWithHyphen,Name1,Name2,ChkType," +
                        //"ChequeName,StartingSerial,EndingSerial,DRNumber,DeliveryDate,username,batch,PackNumber,Date,Time,location, BranchCode,OldBranchCode )" +
                        //"VALUES('" + r.BRSTN + "','" + r.BranchName + "','" + r.AccountNo + "','" + r.AccountNoWithHypen + "','" + r.Name1.Replace("'", "''") +
                        //"','" + r.Name2.Replace("'", "''") + "','" + r.ChkType + "','" + r.ChequeName + "','" + r.StartingSerial + "','" + r.EndingSerial +
                        //"','" + _DrNumber + "','" + _deliveryDate.ToString("yyyy-MM-dd") + "','" + _username + "','" +
                        //r.Batch.TrimEnd() + "','" + _packNumber + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("hh:mm:ss") +
                        //"','" + r.Location + "','" + r.BranchCode + "','" + r.OldBranchCode + "');";
                        //cmd = new MySqlCommand(Sql, myConnect);
                        //cmd.ExecuteNonQuery();

                    });
                    // counter++;
                    _packNumber++;
                    // if (counter == 10)
                    // {
                    _DrNumber++;
                    //    counter = 0;
                    //  }

                });

            }
            DBClosed();
            DBConnect();
            if (_checks.Regular_Personal_Provincial.Count > 0)
            {
                //Generating DR per CheckType in Provincial Branches
                var dBranch = _checks.Regular_Personal_Provincial.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Regular_Personal_Provincial.Where(g => g.BRSTN == y).ToList();
                    dRecord.ForEach(r =>
                    {
                        Script(gClient.DataBaseName,r, _DrNumber, _deliveryDate, _username, _packNumber);

                        //Sql = "Insert into " + gClient.DataBaseName + " (BRSTN,BranchName,AccountNo,AcctNoWithHyphen,Name1,Name2,ChkType," +
                        //"ChequeName,StartingSerial,EndingSerial,DRNumber,DeliveryDate,username,batch,PackNumber,Date,Time,location, BranchCode,OldBranchCode )" +
                        //"VALUES('" + r.BRSTN + "','" + r.BranchName + "','" + r.AccountNo + "','" + r.AccountNoWithHypen + "','" + r.Name1.Replace("'", "''") +
                        //"','" + r.Name2.Replace("'", "''") + "','" + r.ChkType + "','" + r.ChequeName + "','" + r.StartingSerial + "','" + r.EndingSerial +
                        //"','" + _DrNumber + "','" + _deliveryDate.ToString("yyyy-MM-dd") + "','" + _username + "','" +
                        //r.Batch.TrimEnd() + "','" + _packNumber + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("hh:mm:ss") +
                        //"','" + r.Location + "','" + r.BranchCode + "','" + r.OldBranchCode + "');";
                        //cmd = new MySqlCommand(Sql, myConnect);
                        //cmd.ExecuteNonQuery();

                    });
                    counter++;
                    _packNumber++;
                    if (counter == 10) // Increment DrNumber when the number of data in 1 (one) DrNumber reach 10 rows 
                    {
                        _DrNumber++;
                        counter = 0;
                    }
                });

            }
            _DrNumber++;
            if (_checks.Regular_Commercial_Direct.Count > 0)
            {
                // generating DR number per Branches with ChkType B in Direct Branches
                var dBranch = _checks.Regular_Commercial_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Regular_Commercial_Direct.Where(g => g.BRSTN == y).ToList();
                    dRecord.ForEach(r =>
                    {
                        Script(gClient.DataBaseName,r, _DrNumber, _deliveryDate, _username, _packNumber);
                        //Sql = "Insert into " + gClient.DataBaseName + " (BRSTN,BranchName,AccountNo,AcctNoWithHyphen,Name1,Name2,ChkType," +
                        //"ChequeName,StartingSerial,EndingSerial,DRNumber,DeliveryDate,username,batch,PackNumber,Date,Time,location, BranchCode,OldBranchCode )" +
                        //"VALUES('" + r.BRSTN + "','" + r.BranchName + "','" + r.AccountNo + "','" + r.AccountNoWithHypen + "','" + r.Name1.Replace("'", "''") +
                        //"','" + r.Name2.Replace("'", "''") + "','" + r.ChkType + "','" + r.ChequeName + "','" + r.StartingSerial + "','" + r.EndingSerial +
                        //"','" + _DrNumber + "','" + _deliveryDate.ToString("yyyy-MM-dd") + "','" + _username + "','" +
                        //r.Batch.TrimEnd() + "','" + _packNumber + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("hh:mm:ss") +
                        //"','" + r.Location + "','" + r.BranchCode + "','" + r.OldBranchCode + "');";
                        //cmd = new MySqlCommand(Sql, myConnect);
                        //cmd.ExecuteNonQuery();

                    });
                    //counter++;
                    _packNumber++;
                    //if (counter == 10)
                    //{
                    _DrNumber++;
                    //  counter = 0;
                    //}


                });

            }

            if (_checks.Regular_Commercial_Provincial.Count > 0)
            {
                //Generating DR per CheckType in Provincial Branches
                var dBranch = _checks.Regular_Commercial_Provincial.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Regular_Commercial_Provincial.Where(g => g.BRSTN == y).ToList();
                    dRecord.ForEach(r =>
                    {

                        Script(gClient.DataBaseName,r, _DrNumber, _deliveryDate, _username, _packNumber);
                        //Sql = "Insert into " + gClient.DataBaseName + " (BRSTN,BranchName,AccountNo,AcctNoWithHyphen,Name1,Name2,ChkType," +
                        //"ChequeName,StartingSerial,EndingSerial,DRNumber,DeliveryDate,username,batch,PackNumber,Date,Time,location, BranchCode,OldBranchCode )" +
                        //"VALUES('" + r.BRSTN + "','" + r.BranchName + "','" + r.AccountNo + "','" + r.AccountNoWithHypen + "','" + r.Name1.Replace("'", "''") +
                        //"','" + r.Name2.Replace("'", "''") + "','" + r.ChkType + "','" + r.ChequeName + "','" + r.StartingSerial + "','" + r.EndingSerial +
                        //"','" + _DrNumber + "','" + _deliveryDate.ToString("yyyy-MM-dd") + "','" + _username + "','" +
                        //r.Batch.TrimEnd() + "','" + _packNumber + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("hh:mm:ss") +
                        //"','" + r.Location + "','" + r.BranchCode + "','" + r.OldBranchCode + "');";
                        //cmd = new MySqlCommand(Sql, myConnect);
                        //cmd.ExecuteNonQuery();

                    });
                    counter++;
                    _packNumber++;
                    if (counter == 10)
                    {
                        _DrNumber++;
                        counter = 0;
                    }


                });

            }
            DBClosed();

        }
        private void ByLocationAndType2(TypeofCheckModel _checks, int _DrNumber, DateTime _deliveryDate, string _username, int _packNumber)
        {
            int counter = 0;
            DBConnect();

            if (_checks.Regular_Personal_Direct.Count > 0)
            {
                // generating DR number per Branches with ChkType A
                var dBranch = _checks.Regular_Personal_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Regular_Personal_Direct.Where(g => g.BRSTN == y).ToList();
                    dRecord.ForEach(r =>
                    {

                        Script(gClient.DataBaseName,r, _DrNumber, _deliveryDate, _username, _packNumber);
                    });
                     counter++;
                    _packNumber++;
                    if (counter == 10)
                    {
                        _DrNumber++;
                        counter = 0;
                    }

                });

            }
            DBClosed();
            DBConnect();
            if (_checks.Regular_Personal_Provincial.Count > 0)
            {
                //Generating DR per CheckType in Provincial Branches
                var dBranch = _checks.Regular_Personal_Provincial.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Regular_Personal_Provincial.Where(g => g.BRSTN == y).ToList();
                    dRecord.ForEach(r =>
                    {

                        Script(gClient.DataBaseName,r, _DrNumber, _deliveryDate, _username, _packNumber);

                    });
                    //counter++;
                    _packNumber++;
                   // if (counter == 10) // Increment DrNumber when the number of data in 1 (one) DrNumber reach 10 rows 
                   // {
                        _DrNumber++;
                     //   counter = 0;
                   // }
                });

            }
            _DrNumber++;
            if (_checks.Regular_Commercial_Direct.Count > 0)
            {
                // generating DR number per Branches with ChkType B in Direct Branches
                var dBranch = _checks.Regular_Commercial_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Regular_Commercial_Direct.Where(g => g.BRSTN == y).ToList();
                    dRecord.ForEach(r =>
                    {

                        Script(gClient.DataBaseName,r, _DrNumber, _deliveryDate, _username, _packNumber);

                    });
                    counter++;
                    _packNumber++;
                    if (counter == 10)
                    {
                        _DrNumber++;
                        counter = 0;
                    }


                });

            }

            if (_checks.Regular_Commercial_Provincial.Count > 0)
            {
                //Generating DR per CheckType in Provincial Branches
                var dBranch = _checks.Regular_Commercial_Provincial.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Regular_Commercial_Provincial.Where(g => g.BRSTN == y).ToList();
                    dRecord.ForEach(r =>
                    {

                        Script(gClient.DataBaseName,r, _DrNumber, _deliveryDate, _username, _packNumber);

                    });
                    //counter++;
                    _packNumber++;
                    //if (counter == 10)
                    //{
                        _DrNumber++;
                      //  counter = 0;
                    //}


                });

            }
            DBClosed();

        }

        public void ByChequetType(TypeofCheckModel _checks, int _DrNumber, DateTime _deliveryDate, string _username, int _packNumber)
        {
            DBConnect();
            int counter = 0;
            var Personal = _checks.Regular_Personal.OrderBy(t => t.BranchName).ToList();
            if (Personal.Count > 0)
            {
                //counter++;
                //_checks.Regular_Personal.ForEach(r =>
                //{
                //_checks.Regular_Personal.OrderBy(r => r.BranchName).ToList();
                var _list = Personal.Select(r => r.BRSTN).Distinct().ToList();
                //var sorted = (from c in _checks.Regular_Personal
                //                orderby c.BranchName
                //                         ascending
                //                select c).ToList();


                //Working Proccess Jan. 27 2021
                foreach (string Brstn in _list)
                {
                    var _model = Personal.Where(t => t.BRSTN == Brstn);

                    foreach (var r in _model)
                    {


                        Script(gClient.DataBaseName,r, _DrNumber, _deliveryDate, _username, _packNumber);



                    }
                    counter++;
                    _packNumber++;
                    if (counter == 10)
                    {
                        _DrNumber++;
                        counter = 0;
                    }


                }

            }
            counter = 0;
            _DrNumber++;

            var Comm = _checks.Regular_Commercial.OrderBy(r => r.BranchName).ToList();
            if (Comm.Count > 0)
            {
                var _List = Comm.Select(r => r.BRSTN).Distinct().ToList();
                //var sorted = (from c in _checks.Regular_Commercial
                //              orderby c.BranchName
                //                       ascending
                //              select c).ToList();

                foreach (string Brstn in _List)
                {
                    var _model = Comm.Where(a => a.BRSTN == Brstn);
                    foreach (var r in _model)
                    {


                        Script(gClient.DataBaseName,r, _DrNumber, _deliveryDate, _username, _packNumber);

                    }
                    counter++;
                    _packNumber++;
                    if (counter == 10)
                    {
                        _DrNumber++;
                        counter = 0;
                    }
                }
            }
            DBClosed();
            return;
        }
        public void ByLocation(TypeofCheckModel _checks, int _DrNumber, DateTime _deliveryDate, string _username, int _packNumber)
        {
            DBConnect();
            int counter = 0;
            var PersonalD = _checks.Regular_Personal_Direct.OrderBy(t => t.BranchName).ToList();
            var PersonalP = _checks.Regular_Personal_Provincial.OrderBy(t => t.BranchName).ToList();
            var CommercialD = _checks.Regular_Commercial_Direct.OrderBy(t => t.BranchName).ToList();
            var CommercialP = _checks.Regular_Commercial_Provincial.OrderBy(t => t.BranchName).ToList();
            if (PersonalD.Count > 0 || CommercialD.Count > 0)
            {
                
                List<OrderModel> listofChecks = new List<OrderModel>();
                PersonalD.ForEach(x => {
                    listofChecks.Add(x);
                });
                CommercialD.ForEach(x => { 
                    listofChecks.Add(x); 
                });
                var _list = listofChecks.Select(r => r.BRSTN).Distinct().ToList();
                //Working Proccess Jan. 27 2021
                foreach (string Brstn in _list)
                {
                    var _model = listofChecks.Where(t => t.BRSTN == Brstn);

                    foreach (var r in _model)
                    {


                        Script(gClient.DataBaseName,r, _DrNumber, _deliveryDate, _username, _packNumber);



                    }
                    counter++;
                    _packNumber++;
                    if (counter == 10)
                    {
                        _DrNumber++;
                        counter = 0;
                    }


                }

            }
            counter = 0;
            _DrNumber++;
            if (PersonalP.Count > 0 || CommercialP.Count > 0)
            {

                List<OrderModel> listofChecks = new List<OrderModel>();
                PersonalP.ForEach(x => {
                    listofChecks.Add(x);
                });
                CommercialP.ForEach(x => {
                    listofChecks.Add(x);
                });
                var _list = listofChecks.Select(r => r.BRSTN).Distinct().ToList();
                //Working Proccess Jan. 27 2021
                foreach (string Brstn in _list)
                {
                    var _model = listofChecks.Where(t => t.BRSTN == Brstn);

                    foreach (var r in _model)
                    {


                        Script(gClient.DataBaseName,r, _DrNumber, _deliveryDate, _username, _packNumber);



                    }
                    counter++;
                    _packNumber++;
                    if (counter == 10)
                    {
                        _DrNumber++;
                        counter = 0;
                    }


                }

            }
           
           
            DBClosed();
            return;
        }
        //public List<int> GetPONUmber(string _chkType, List<int> _poNumber)
        //{
            
        //    Sql = "Select PurchaseOrderNo from " +gClient.PurchaseOrderFinishedTable+" where ChequeName = '"+_chkType + "'";
        //    DBConnect();
        //    cmd = new MySqlCommand(Sql, myConnect);
        //    MySqlDataReader reader = cmd.ExecuteReader();
        //    while(reader.Read())
        //    {

        //        int po = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0;

        //        _poNumber.Add(po);
        //    }
        //    reader.Close();
        //    DBClosed();
        //    return _poNumber;
        //}
        public int GetPONUmber(string _chkType)
        {
            int _poNumber = 0;
            Sql = "Select PurchaseOrderNo from " + gClient.PurchaseOrderFinishedTable + " where ChequeName = '" + _chkType + "'";
            DBConnect();
            cmd = new MySqlCommand(Sql, myConnect);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                _poNumber = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0;

              //  _poNumber.Add(po);
            }
            reader.Close();
            DBClosed();
            return _poNumber;
        }
        private void Script(string _table ,OrderModel r,int _DrNumber, DateTime _deliveryDate, string _username, int _packNumber)
        {
            if (gClient.DataBaseName != "producers_history")
            {
                Sql = "Insert into " +_table+ " (BRSTN,BranchName,AccountNo,AcctNoWithHyphen,Name1,Name2,ChkType," +
                          "ChequeName,StartingSerial,EndingSerial,DRNumber,DeliveryDate,username,batch,PackNumber,Date,Time,location, BranchCode,OldBranchCode,PurchaseOrderNumber )" +
                          "VALUES('" + r.BRSTN + "','" + r.BranchName + "','" + r.AccountNo + "','" + r.AccountNoWithHypen + "','" + r.Name1.Replace("'", "''") +
                          "','" + r.Name2.Replace("'", "''") + "','" + r.ChkType + "','" + r.ChequeName + "','" + r.StartingSerial + "','" + r.EndingSerial +
                          "','" + _DrNumber + "','" + _deliveryDate.ToString("yyyy-MM-dd") + "','" + _username + "','" +
                          r.Batch.TrimEnd() + "','" + _packNumber + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("hh:mm:ss") +
                          "','" + r.Location + "','" + r.BranchCode + "','" + r.OldBranchCode + "',"+r.PONumber+");";
                cmd = new MySqlCommand(Sql, myConnect);
                cmd.ExecuteNonQuery();
            }
            else
            {
                Sql = "Insert into " + _table + " (BRSTN,BranchName,AccountNo,AcctNoWithHyphen,Name1,Name2,ChkType," +
                          "ChequeName,StartingSerial,EndingSerial,DRNumber,DeliveryDate,username,batch,PackNumber,Date,Time,location)" +
                          "VALUES('" + r.BRSTN + "','" + r.BranchName + "','" + r.AccountNo + "','" + r.AccountNoWithHypen + "','" + r.Name1.Replace("'", "''") +
                          "','" + r.Name2.Replace("'", "''") + "','" + r.ChkType + "','" + r.ChequeName + "','" + r.StartingSerial + "','" + r.EndingSerial +
                          "','" + _DrNumber + "','" + _deliveryDate.ToString("yyyy-MM-dd") + "','" + _username + "','" +
                          r.Batch.TrimEnd() + "','" + _packNumber + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("hh:mm:ss") +
                          "','" + r.Location + "');";
                cmd = new MySqlCommand(Sql, myConnect);
                cmd.ExecuteNonQuery();
            }
        }
        public int CheckPOQuantity(int _PO, string _chkType )
        {
            int _remainingbalance = 0;
            int IniatailBalance = 0;
            int ProcessedQuantity = 0;
            Sql = "Select Quantity  from "+gClient.PurchaseOrderFinishedTable +" where PurchaseOrderNo = " + _PO + " and ChequeName = '" + _chkType + "';";
            DBConnect();
            cmd = new MySqlCommand(Sql, myConnect);
            MySqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                IniatailBalance = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0;
            }
            reader.Close();
            DBClosed();
            string Sql2 = "Select Count(PurchaseOrderNumber) from " + gClient.DataBaseName + " where PurchaseOrderNumber = " + _PO + " and ChequeName = '" + _chkType + "' ";
            DBConnect();
            cmd = new MySqlCommand(Sql2, myConnect);
            MySqlDataReader reader2 = cmd.ExecuteReader();
            while(reader2.Read())
            {
                ProcessedQuantity = !reader2.IsDBNull(0) ? reader2.GetInt32(0) : 0;
            }
            reader2.Close();
            DBClosed();

            _remainingbalance = IniatailBalance - ProcessedQuantity;

            return _remainingbalance;
        }
    }
}
