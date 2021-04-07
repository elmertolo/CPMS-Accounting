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
        string ConString = ConfigurationManager.AppSettings["ConnectionStringOrdering"];
        MySqlConnection con;

        public void DBConnect()
        {
            try
            {
                string DBConnection = "";

                //   if (frmLogIn.userName == "elmer")
                //  {
                DBConnection = ConfigurationManager.AppSettings["ConnectionString"];

      //        DBConnection =  p.ReadJsonConfigFile("Database", "ConnectionString", "");
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

            }
            catch (Exception Error)
            {

                MessageBox.Show(Error.Message, "System Error");
            }
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
        public List<OrderModel> Process2(List<OrderModel> _orders, DeliveryReport _main, int DrNumber, int packNumber,int _dReportStyle, int _pReportStyle, int _withDeliveryTo)
        {
            TypeofCheckModel checkType = new TypeofCheckModel();
            //int counter = 0;
            checkType.Regular_Personal = new List<OrderModel>();
            checkType.Regular_Commercial = new List<OrderModel>();
            checkType.Regular_Personal_Direct = new List<OrderModel>();
            checkType.Regular_Personal_Provincial = new List<OrderModel>();
            checkType.Regular_Commercial_Direct = new List<OrderModel>();
            checkType.Regular_Commercial_Provincial = new List<OrderModel>();
            checkType.ManagersCheck = new List<OrderModel>();
            checkType.ManagersCheck_Direct = new List<OrderModel>();
            checkType.ManagersCheck_Provincial = new List<OrderModel>();
            checkType.ExecutiveOnline_Direct = new List<OrderModel>();
            checkType.ExecutiveOnline_Provincial = new List<OrderModel>();
            List<ChequeTypesModel> type = new List<ChequeTypesModel>();
            GetChequeTypes(type);
            foreach (OrderModel _check in _orders)
            {


                if (_dReportStyle != 0 && _pReportStyle != 0)
                {
                    //foreach (var t in type)
                    //{


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
                        if (_check.ChkType == "C" && _check.Location == "Direct")
                        {
                            checkType.ManagersCheck_Direct.Add(_check);
                        }
                        if (_check.ChkType == "C" && _check.Location == "Provincial")
                        {
                            checkType.ManagersCheck_Provincial.Add(_check);
                        }
                        if (_check.ChkType == "D" && _check.Location == "Direct")
                        {
                            checkType.ExecutiveOnline_Direct.Add(_check);
                        }
                        if (_check.ChkType =="D" && _check.Location == "Provincial")
                        {
                            checkType.ExecutiveOnline_Provincial.Add(_check);
                        }
                    //}
                }
                //else if (_dReportStyle == 0 && _pReportStyle == 0 && _withDeliveryTo == 1)
                //{

                //    if (_check.ChkType == "A" && _check.Location == "Direct")
                //    {
                //        checkType.Regular_Personal_Direct.Add(_check);
                //    }
                //    if (_check.ChkType == "A" && _check.Location == "Provincial")
                //    {
                //        checkType.Regular_Personal_Provincial.Add(_check);
                //    }
                //    if (_check.ChkType == "B" && _check.Location == "Direct")
                //    {
                //        checkType.Regular_Commercial_Direct.Add(_check);
                //    }
                //    if (_check.ChkType == "B" && _check.Location == "Provincial")
                //    {
                //        checkType.Regular_Commercial_Provincial.Add(_check);
                //    }
                //    if (_check.ChkType == "C" && _check.Location == "Direct")
                //    {
                //        checkType.ManagersCheck_Direct.Add(_check);
                //    }
                //    if (_check.ChkType == "C" && _check.Location == "Provincial")
                //    {
                //        checkType.ManagersCheck_Provincial.Add(_check);
                //    }
                //}
                else
                {
                    //foreach (var t in type)
                    //{


                        if (_check.ChkType == "A")
                        {
                            checkType.Regular_Personal.Add(_check);
                        }
                        if (_check.ChkType == "B")
                        {
                            checkType.Regular_Commercial.Add(_check);
                        }
                        if (_check.ChkType == "C")
                        {
                            checkType.ManagersCheck.Add(_check);

                        }
                    //}
                }
                
            }
            checkType.Regular_Personal.OrderBy(r => r.BranchName).ToList();
            checkType.Regular_Commercial.OrderBy(r => r.BranchName).ToList();
            checkType.ManagersCheck.OrderBy(r => r.BranchName).ToList();
            checkType.Regular_Personal_Direct.OrderBy(r => r.BranchName).ToList();
            checkType.Regular_Commercial_Direct.OrderBy(r => r.BranchName).ToList();
            checkType.Regular_Personal_Provincial.OrderBy(r => r.BranchName).ToList();
            checkType.Regular_Commercial_Provincial.OrderBy(r => r.BranchName).ToList();
            
            checkType.ManagersCheck_Direct.OrderBy(r => r.BranchName).ToList();
            checkType.ManagersCheck_Provincial.OrderBy(r => r.BranchName).ToList();
            checkType.ExecutiveOnline_Direct.OrderBy(r => r.BranchName).ToList();
            checkType.ExecutiveOnline_Provincial.OrderBy(r => r.BranchName).ToList();
            //  if (gClient.DataBaseName != "producers_history")
            GenerateData2(checkType, DrNumber, _main.deliveryDate, gUser.Id, packNumber, _dReportStyle,_pReportStyle, _withDeliveryTo);
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
        public void GenerateData2(TypeofCheckModel _checks, int _DrNumber, DateTime _deliveryDate, string _username, int _packNumber,int _dReportStyle,int _pReportStyle, int _withDeliveryTo)
        {

            if (_dReportStyle == 0 && _pReportStyle == 0)
            {
             
                ByChequetType(_checks, _DrNumber, _deliveryDate, _username, _packNumber); //Default Report for All Banks


            }
            else if (_dReportStyle == 0 && _pReportStyle == 1)
            {

            }
            else if (_dReportStyle == 0 && _pReportStyle == 2)
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
                if (_withDeliveryTo == 1)
                    ByLocationAndTypeWithDeliveryTO(_checks, _DrNumber, _deliveryDate, _username, _packNumber); //Default Report for All Banks
                else
                    ByLocationAndTypeFilterByBranchCode(_checks, _DrNumber, _deliveryDate, _username, _packNumber);
                //ByLocationAndType(_checks, _DrNumber, _deliveryDate, _username, _packNumber);
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
            try
            {
                DBConnect();
                Sql = "SELECT DRNumber, PackNumber, BRSTN, ChkType, BranchName, COUNT(BRSTN)," +
                     "MIN(StartingSerial), MAX(EndingSerial),ChequeName, Batch,username,BranchCode,OldBranchCode,location,PurchaseOrderNumber,Bank,Address2,Address3,Address4," +
                     "Name1,Name2,AccountNo,DeliveryToBrstn,DeliveryToBranch,AttentionTo FROM " +
                     gClient.DataBaseName + " WHERE  Batch = '" + _batch.TrimEnd() + "' GROUP BY DRNumber, BRSTN, ChkType, BranchName," +
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
                    order.BankCode = !myReader.IsDBNull(15) ? myReader.GetString(15) : "";
                    order.Address2 = !myReader.IsDBNull(16) ? myReader.GetString(16) : "";
                    order.Address3 = !myReader.IsDBNull(17) ? myReader.GetString(17) : "";
                    order.Address4 = !myReader.IsDBNull(18) ? myReader.GetString(18) : "";
                    order.Name1 = !myReader.IsDBNull(19) ? myReader.GetString(19) : "";
                    order.Name2 = !myReader.IsDBNull(20) ? myReader.GetString(20) : "";
                    order.AccountNo = !myReader.IsDBNull(21) ? myReader.GetString(21) : "";
                    order.DeliveryToBrstn = !myReader.IsDBNull(22) ? myReader.GetString(22) : "";
                    order.DeliveryToBranch = !myReader.IsDBNull(23) ? myReader.GetString(23) : "";
                    order.AttentionTo = !myReader.IsDBNull(24) ? myReader.GetString(24) : "";

                    list.Add(order);
                }
                DBClosed();
                DBConnect();
                string sqldel = "Delete from " + gClient.DRTempTable + ";";
                MySqlCommand comdel = new MySqlCommand(sqldel, myConnect);
                comdel.ExecuteNonQuery();

                DBClosed();
                var chk = list.Select(r => r.ChkType).Distinct().ToList();

                //foreach (var item in chk)
                //{
                //list.ForEach(x =>
                //{
                //    if(x.ChkType == "A" && x.Location == "Direct")
                //    {
                //        string concat = ConcatDRNumbers(x.Batch, x.ChkType, x.Location);
                //    }
                //});

                //string concatDA = ConcatDRNumbers(list[0].Batch, "A", "Direct");
                //string concatDB = ConcatDRNumbers(list[0].Batch, "B", "Direct");
                //string concatPA = ConcatDRNumbers(list[0].Batch, "A", "Provincial");
                //string concatPB = ConcatDRNumbers(list[0].Batch, "B", "Provincial");
                //string concatECD = ConcatDRNumbers(list[0].Batch, "D", "Direct");
                //string concatECP = ConcatDRNumbers(list[0].Batch, "D", "Provincial");
                //string concatMCD = ConcatDRNumbers(list[0].Batch, "C", "Direct");
                //string concatMCP = ConcatDRNumbers(list[0].Batch, "C", "Provincial");
                ////}

                for (int i = 0; i < list.Count; i++)
                {
                    
                    string concat = "";
                    if (list[i].ChkType == "A" && list[i].Location == "Direct")
                    {
                         concat = ConcatDRNumbers(list[i].Batch, list[i].ChkType, list[i].Location);
                    }
                    else if (list[i].ChkType == "B" && list[i].Location == "Direct")
                    {
                        concat = ConcatDRNumbers(list[i].Batch, list[i].ChkType, list[i].Location);
                    }
                    else if (list[i].ChkType == "C" && list[i].Location == "Direct")
                    {
                        concat = ConcatDRNumbers(list[i].Batch, list[i].ChkType, list[i].Location);
                    }
                    else if (list[i].ChkType == "D" && list[i].Location == "Direct")
                    {
                        concat = ConcatDRNumbers(list[i].Batch, list[i].ChkType, list[i].Location);
                    }
                    else if (list[i].ChkType == "A" && list[i].Location == "Provincial")
                    {
                        concat = ConcatDRNumbers(list[i].Batch, list[i].ChkType, list[i].Location);
                    }
                    else if (list[i].ChkType == "B" && list[i].Location == "Provincial")
                    {
                        concat = ConcatDRNumbers(list[i].Batch, list[i].ChkType, list[i].Location);
                    }
                    else if (list[i].ChkType == "C" && list[i].Location == "Provincial")
                    {
                        concat = ConcatDRNumbers(list[i].Batch, list[i].ChkType, list[i].Location);
                    }
                    else if (list[i].ChkType == "D" && list[i].Location == "Provincial")
                    {
                        concat = ConcatDRNumbers(list[i].Batch, list[i].ChkType, list[i].Location);
                    }

                    var TotalA = list.Where(x => x.ChkType == "A");
                    var TotalB = list.Where(x => x.ChkType == "B");
                    var TotalC = list.Where(x => x.ChkType == "C");
                    var TotalD = list.Where(x => x.ChkType == "D");
                    DBConnect();

                    //Original Script
                    //string sql2 = "Insert into " + gClient.DRTempTable + " (DRNumber,PackNumber,BRSTN, ChkType, BranchName,Qty,StartingSerial," +
                    //              "EndingSerial,ChequeName,Batch,username,BranchCode,OldBranchCode,Location,PONumber,ConcatinatedDRA," +
                    //              "ConcatinatedDRB,ConcatinatedDRC,ConcatinatedDRD,Bank,Address2,Address3,Address4,Name1,Name2,AccountNo,ConcatinatedDRDD,ConcatinatedDRPD)" +
                    //              " Values('" + list[i].DrNumber + "','" + list[i].PackNumber +
                    //              "','" + list[i].BRSTN + "','" + list[i].ChkType + "','" + list[i].BranchName + "'," + list[i].Qty +
                    //              ",'" + list[i].StartingSerial + "','" + list[i].EndingSerial + "','" + list[i].ChequeName.Replace("'","''") + "','" +
                    //              list[i].Batch + "','" + list[i].username + "','" + list[i].BranchCode + "','" + list[i].OldBranchCode + "','" +
                    //              list[i].Location + "'," + list[i].PONumber + ",'" + concat + "','" + concatDB + "','" + concatPA + "','" + concatPB + "','" +
                    //              list[i].BankCode + "','" + list[i].Address2.Replace("'", "''") + "','" + list[i].Address3.Replace("'", "''") + "','" +
                    //              list[i].Address4.Replace("'", "''") + "','" + list[i].Name1.Replace("'", "''") + "','" + list[i].Name2.Replace("'", "''") + "','" +
                    //              list[i].AccountNo + "','" + concatECD + "','" + concatECP + "');";
                    string sql2 = "Insert into " + gClient.DRTempTable + " (DRNumber,PackNumber,BRSTN, ChkType, BranchName,Qty,StartingSerial," +
                                  "EndingSerial,ChequeName,Batch,username,BranchCode,OldBranchCode,Location,PONumber,ConcatinatedDRA," +
                                  "Bank,Address2,Address3,Address4,Name1,Name2,AccountNo,TotalA,TotalB,BankName,AttentionTo,TIN,DeliveryToBranch,DeliveryToBrstn)" +
                                  " Values('" + list[i].DrNumber + "','" + list[i].PackNumber +
                                  "','" + list[i].BRSTN + "','" + list[i].ChkType + "','" + list[i].BranchName + "'," + list[i].Qty +
                                  ",'" + list[i].StartingSerial + "','" + list[i].EndingSerial + "','" + list[i].ChequeName.Replace("'", "''") + "','" +
                                  list[i].Batch + "','" + list[i].username + "','" + list[i].BranchCode + "','" + list[i].OldBranchCode + "','" +
                                  list[i].Location + "'," + list[i].PONumber + ",'" + concat + "','" + list[i].BankCode + "','" + list[i].Address2.Replace("'", "''") +
                                  "','" + list[i].Address3.Replace("'", "''") + "','" + list[i].Address4.Replace("'", "''") + "','" + list[i].Name1.Replace("'", "''") + 
                                  "','" + list[i].Name2.Replace("'", "''") + "','" + list[i].AccountNo + "','" + TotalA.Count() + "','" + TotalB.Count() +
                                  "','" + gClient.Description.ToUpper().Replace("'","''").TrimEnd() + "','" + list[i].AttentionTo.Replace("'","''").TrimEnd() + 
                                  "','" + gClient.TIN + "','" + list[i].DeliveryToBranch + "','" + list[i].DeliveryToBrstn + "');";
                    MySqlCommand cmd2 = new MySqlCommand(sql2, myConnect);
                    cmd2.ExecuteNonQuery();
                }

                DBClosed();
                return _batch;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "GetDrDetails", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
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


                Sql = "Select DrNumber from " + item + " where Date > '2020-12-01' order by DrNumber desc Limit 1";
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


                Sql = "Select PackNumber from " + item + " where Date >= '2020-12-01' order by PackNumber desc Limit 1";

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
        public Int32 GetMaxPackNumber2()
        {
  //          GetBankTables();
            //InsertToMaxTB();
            DBConnect();
//            List<Int32> pack = new List<Int32>();
            Int32 dr = 0;
         

                Sql = "Select PackNumber from " + gClient.DataBaseName + " where Date >= '2020-12-01' order by PackNumber desc Limit 1";

                cmd = new MySqlCommand(Sql, myConnect);
                MySqlDataReader read = cmd.ExecuteReader();

                    while (read.Read())
                    {
                        dr = !read.IsDBNull(0) ? read.GetInt32(0) : 0;
                    }

                read.Close();
                DBClosed();
                return dr;
        }

        public List<TempModel> GetStickerDetails(List<TempModel> _temp, string _batch)
        {
            try
            {
                Sql = "SELECT BranchName, BRSTN, ChkType,MIN(StartingSerial), MAX(EndingSerial), Count(ChkType), Bank,Address2,Address3,Address4,Block,Segment,ProductType, " +
                      "DeliveryToBranch FROM " + gClient.DataBaseName + "  WHERE Batch = '" + _batch + "'" +
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
                        BankCode = !myReader.IsDBNull(6) ? myReader.GetString(6) : "",
                        Address2 = !myReader.IsDBNull(7) ? myReader.GetString(7) : "",
                        Address3 = !myReader.IsDBNull(8) ? myReader.GetString(8) : "",
                        Address4 = !myReader.IsDBNull(9) ? myReader.GetString(9) : "",
                        Block = !myReader.IsDBNull(10) ? myReader.GetInt32(10) : 0,
                        Segment = !myReader.IsDBNull(11) ? myReader.GetInt32(11) : 0,
                        ProducType = !myReader.IsDBNull(12) ? myReader.GetString(12): "",
                        DeliveryToBranch = !myReader.IsDBNull(13) ? myReader.GetString(13) : ""

                        //ChequeName = !myReader.IsDBNull(6) ? myReader.GetString(6): ""


                    };
                    _temp.Add(t);
                }
                DBClosed();
                DBConnect();
                string sqldel = "Delete from " + gClient.StickerTable;
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
                    else if (_temp[r].ChkType == "C")
                        Type = "MC";
                    if (licnt == 1)
                    {
                        string sql2 = "Insert into " + gClient.StickerTable + "(Batch,BRSTN,BranchName,Qty,ChkType,ChequeName,StartingSerial,EndingSerial," +
                                      "Bank,Address2,Address3,Address4,Block,Segment,ProductType,DeliveryToBranch)" +
                                      "values('" + _batch + "','" + _temp[r].BRSTN + "','" + _temp[r].BranchName + "'," + _temp[r].Qty + ",'" + _temp[r].ChkType +
                                      "','" + Type + "','" + _temp[r].StartingSerial + "','" + _temp[r].EndingSerial + "','" + gClient.Description.ToUpper().Replace("'","''")
                                      + "','" + _temp[r].Address2.Replace("'", "''") +
                                      "', '" + _temp[r].Address3.Replace("'", "''") + "','" + _temp[r].Address4.Replace("'", "''") + "'," + _temp[r].Block + "," +
                                      _temp[r].Segment + ",'" + _temp[r].ProducType +"','" + _temp[r].DeliveryToBranch + "' ); ";


                        MySqlCommand cmd2 = new MySqlCommand(sql2, myConnect);
                        cmd2.ExecuteNonQuery();
                        licnt++;
                    }
                    else if (licnt == 2)
                    {
                        string sql2 = "Update " + gClient.StickerTable + " set BRSTN2 = '" + _temp[r].BRSTN + "',BranchName2 = '" + _temp[r].BranchName.Replace("'","''") +
                                    "',Qty2 = " + _temp[r].Qty +",ChkType2 = '" + _temp[r].ChkType + "', ChequeName2 = '" + Type + "',StartingSerial2 = '" + _temp[r].StartingSerial +
                                      "',EndingSerial2 = '" + _temp[r].EndingSerial + "',Address22 ='" + _temp[r].Address2.Replace("'", "''") +
                                      "', Address32 ='" + _temp[r].Address3.Replace("'", "''") + "',Address42 ='" + _temp[r].Address4.Replace("'", "''") + 
                                      "', Block2 = " + _temp[r].Block + ", Segment2 = " + _temp[r].Segment  + ",ProductType2 = '" + _temp[r].ProducType + "' " +
                                      ", DeliveryToBranch1 = '" + _temp[r].DeliveryToBranch + "' " +
                                      "where BRSTN = '" + _temp[r - 1].BRSTN + "' and ChkType = '" + _temp[r - 1].ChkType + "';";
                                      
                        MySqlCommand cmd2 = new MySqlCommand(sql2, myConnect);
                        cmd2.ExecuteNonQuery();
                        licnt++;
                    }
                    else if (licnt == 3)
                    {
                        string sql2 = "Update " + gClient.StickerTable + " set BRSTN3 = '" + _temp[r].BRSTN + "',BranchName3 = '" + _temp[r].BranchName + "',Qty3 = " + _temp[r].Qty +
                                      ",ChkType3 = '" + _temp[r].ChkType + "',ChequeName3 = '" + Type + "',StartingSerial3 = '" + _temp[r].StartingSerial +
                                      "',EndingSerial3 = '" + _temp[r].EndingSerial + "',Address23  = '" + _temp[r].Address2.Replace("'", "''") + "'" +
                                      ",Address33 = '" + _temp[r].Address3.Replace("'", "''") + "',Address43 = '" + _temp[r].Address4.Replace("'", "''") +
                                      "',Block3 = " + _temp[r].Block + ", Segment3 = " + _temp[r].Segment + ",ProductType3 = '" + _temp[r].ProducType +
                                      "',DeliveryToBranch2 = '" + _temp[r].DeliveryToBranch + "'" +
                                      "  where BRSTN2 = '" + _temp[r - 1].BRSTN + "' and ChkType2 = '" + _temp[r - 1].ChkType + "';";
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
                MessageBox.Show(e.Message,"GetStickerDetails",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            return _temp;
        }
        public List<TempModel> GetStickerDetailsForPNB(List<TempModel> _temp, string _batch)
        {
            Sql = "SELECT BranchName, BRSTN, ChkType,MIN(StartingSerial), MAX(EndingSerial), Count(ChkType), Bank,Address2,Address3,Address4,Name1,Name2" +
                ",ChequeName, BranchCode,AccountNo  FROM " + gClient.DataBaseName + " WHERE Batch = '" + _batch + "'" +
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
                    BankCode = !myReader.IsDBNull(6) ? myReader.GetString(6) : "",
                    Address2 = !myReader.IsDBNull(7) ? myReader.GetString(7) : "",
                    Address3 = !myReader.IsDBNull(8) ? myReader.GetString(8) : "",
                    Address4 = !myReader.IsDBNull(9) ? myReader.GetString(9) : "",
                    Name1 = !myReader.IsDBNull(10) ? myReader.GetString(10) : "",
                    Name2 = !myReader.IsDBNull(11) ? myReader.GetString(11) : "",
                    ChequeName = !myReader.IsDBNull(12) ? myReader.GetString(12) : "",
                    BranchCode  = !myReader.IsDBNull(13) ? myReader.GetString(13) : "",
                    AccountNo = !myReader.IsDBNull(14) ? myReader.GetString(14) : ""
                    


                };
                _temp.Add(t);
            }
            DBClosed();
            DBConnect();
            string sqldel = "Delete from " + gClient.StickerTable;
            MySqlCommand comdel = new MySqlCommand(sqldel, myConnect);
            comdel.ExecuteNonQuery();

            DBClosed();
            DBConnect();
            _temp.ForEach(r => { 
            Sql = "Insert into " + gClient.StickerTable + " (Batch,BRSTN,BranchName,Qty,ChkType,ChequeName,StartingSerial,EndingSerial,Bank,Address2,Address3,Address4" +
                ",Name1,Name2,BranchCode,AccountNo) values('" + _batch + "','" + r.BRSTN + "','" + r.BranchName + "'," + r.Qty + ",'" + r.ChkType +
                                      "','" +r.ChequeName.Substring(0,r.ChequeName.Length - 6)+ "','" + r.StartingSerial + "','" + r.EndingSerial + "','" +
                                      r.BankCode + "','" + r.Address2.Replace("'", "''") + "', '" + r.Address3.Replace("'", "''") + "','" +
                                      r.Address4.Replace("'", "''") + "','" + r.Name1.Replace("'","''") + "','" +r.Name2.Replace("'","''") +"','" + r.BranchCode +"','" +
                                      r.AccountNo + "'); ";

                cmd = new MySqlCommand(Sql, myConnect);
                cmd.ExecuteNonQuery();
            });
            DBClosed();
            return _temp;
        }
        //public string FillCRReportParameters()
        //{
        //    string reportPath = "";
        //    try
        //    {
                
        //        if (Debugger.IsAttached)
        //        {
        //            if (gClient.DataBaseName == "")
        //                MessageBox.Show("There is no table selected!");
        //            else
        //            {

        //                if (gClient.DataBaseName != "pnb_history")
        //                {
        //                    if (RecentBatch.report == "STICKER" || DeliveryReport.report == "STICKER")
        //                        reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\Stickers.rpt";
        //                    else if (RecentBatch.report == "Packing" || DeliveryReport.report == "Packing")
        //                        reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\PackingReport.rpt";
        //                    else if (RecentBatch.report == "DOC" || DeliveryReport.report == "DOC")
        //                        reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\DocStamp.rpt";
        //                    else if (RecentBatch.report == "DR" || DeliveryReport.report == "DR")
        //                        reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\DeliveryReceipt.rpt";

        //                }
        //                else 
        //                {
        //                    if (RecentBatch.report == "STICKER" || DeliveryReport.report == "STICKER")
        //                        reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\PNBStickers2.rpt";
        //                    else if (RecentBatch.report == "PackingList" || DeliveryReport.report == "PackingList")
        //                        reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\PNBPackingList.rpt";
        //                    else if (RecentBatch.report == "Packing" || DeliveryReport.report == "Packing")
        //                        reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\PackingReport.rpt";
        //                    else if (RecentBatch.report == "DOC" || DeliveryReport.report == "DOC")
        //                        reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\DocStamp.rpt";
                            
        //                    else if (RecentBatch.report == "DRR" || DeliveryReport.report == "DRR")
        //                        reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\PNBDeliveryReport.rpt";
        //                    else if (RecentBatch.report == "DR" || DeliveryReport.report == "DR")
        //                        reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\PNBDeliveryReceipt.rpt";

        //                }
        //                //if (RecentBatch.report == "STICKER" || DeliveryReport.report == "STICKER")
        //                //    reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Stickers.rpt";
        //                //else if (RecentBatch.report == "Packing" || DeliveryReport.report == "Packing")
        //                //    reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\PackingReport.rpt";
        //            }


        //        }
        //        else
        //        {
        //            if (gClient.DataBaseName == "")
        //                MessageBox.Show("There is no table selected!");
        //            else
        //            {

        //                if (gClient.DataBaseName == "producers_history")
        //                {

        //                    if (RecentBatch.report == "STICKER" || DeliveryReport.report == "STICKER")
        //                        reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\Stickers.rpt";
        //                    else if (RecentBatch.report == "Packing" || DeliveryReport.report == "Packing")
        //                        reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\PackingReport.rpt";
        //                    else if (RecentBatch.report == "DR" || DeliveryReport.report == "DR")
        //                        reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\DeliveryReceipt.rpt";
        //                }
        //                else if (gClient.DataBaseName == "pnb_history")
        //                {
        //                    if (RecentBatch.report == "STICKER" || DeliveryReport.report == "STICKER")
        //                        reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\PNBStickers2.rpt";
        //                    else if (RecentBatch.report == "PackingList" || DeliveryReport.report == "PackingList")
        //                        reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\PNBPackingList.rpt";
        //                    else if (RecentBatch.report == "Packing" || DeliveryReport.report == "Packing")
        //                        reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\PackingReport.rpt";
        //                    else if (RecentBatch.report == "DOC" || DeliveryReport.report == "DOC")
        //                        reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\DocStamp.rpt";
                           
        //                    else if (RecentBatch.report == "DRR" || DeliveryReport.report == "DRR")
        //                        reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\PNBDeliveryReport.rpt";
        //                    else if (RecentBatch.report == "DR" || DeliveryReport.report == "DR")
        //                        reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\PNBDeliveryReceipt.rpt";
        //                }
        //            }

        //        }

        //        return reportPath;
        //    }
        //    catch (Exception error)
        //    {
        //        MessageBox.Show(error.Message, error.InnerException.Message);
        //        return reportPath;
        //    }
        //}
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

                        if (gClient.BankCode == "008")
                        {
                            if (RecentBatch.report == "STICKER" || DeliveryReport.report == "STICKER")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\PNBStickers2.rpt";
                            else if (RecentBatch.report == "PackingList" || DeliveryReport.report == "PackingList")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\PNBPackingList.rpt";
                            else if (RecentBatch.report == "Packing" || DeliveryReport.report == "Packing")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\PackingReport.rpt";
                            else if (RecentBatch.report == "DOC" || DeliveryReport.report == "DOC")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\DocStamp.rpt";

                            else if (RecentBatch.report == "DRR" || DeliveryReport.report == "DRR")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\PNBDeliveryReport.rpt";
                            else if (RecentBatch.report == "DR" || DeliveryReport.report == "DR")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\PNBDeliveryReceipt.rpt";

                        }
                        else if (gClient.BankCode == "028")
                        {
                            if (RecentBatch.report == "STICKER" || DeliveryReport.report == "STICKER")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\DeliverToStickers.rpt";
                            else if (RecentBatch.report == "Packing" || DeliveryReport.report == "Packing")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\PackingReport.rpt";
                            else if (RecentBatch.report == "DOC" || DeliveryReport.report == "DOC")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\DocStamp.rpt";
                            else if (RecentBatch.report == "DRP" || DeliveryReport.report == "DRP")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\DeliveryReceiptProvincial.rpt";
                            else if (RecentBatch.report == "DR" || DeliveryReport.report == "DR")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\DeliveryReceiptDirect.rpt";
                        }
                        else
                        {
                            if (RecentBatch.report == "STICKER" || DeliveryReport.report == "STICKER")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\Stickers.rpt";
                            else if (RecentBatch.report == "Packing" || DeliveryReport.report == "Packing")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\PackingReport.rpt";
                            else if (RecentBatch.report == "DOC" || DeliveryReport.report == "DOC")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\DocStamp.rpt";
                            else if (RecentBatch.report == "DR" || DeliveryReport.report == "DR")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\DeliveryReceipt.rpt";


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

                        if (gClient.BankCode == "008")
                        {
                            if (RecentBatch.report == "STICKER" || DeliveryReport.report == "STICKER")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\PNBStickers2.rpt";
                            else if (RecentBatch.report == "PackingList" || DeliveryReport.report == "PackingList")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\PNBPackingList.rpt";
                            else if (RecentBatch.report == "Packing" || DeliveryReport.report == "Packing")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\PackingReport.rpt";
                            else if (RecentBatch.report == "DOC" || DeliveryReport.report == "DOC")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\DocStamp.rpt";

                            else if (RecentBatch.report == "DRR" || DeliveryReport.report == "DRR")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\PNBDeliveryReport.rpt";
                            else if (RecentBatch.report == "DR" || DeliveryReport.report == "DR")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\PNBDeliveryReceipt.rpt";


                        }
                        else if (gClient.BankCode == "028")
                        {
                            if (RecentBatch.report == "STICKER" || DeliveryReport.report == "STICKER")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\DeliverToStickers.rpt";
                            else if (RecentBatch.report == "Packing" || DeliveryReport.report == "Packing")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\PackingReport.rpt";
                            else if (RecentBatch.report == "DOC" || DeliveryReport.report == "DOC")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\DocStamp.rpt";
                            else if (RecentBatch.report == "DRP" || DeliveryReport.report == "DRP")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\DeliveryReceiptProvincial.rpt";
                            else if (RecentBatch.report == "DR" || DeliveryReport.report == "DR")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\DeliveryReceiptDirect.rpt";
                        }
                        else
                        {
                            if (RecentBatch.report == "STICKER" || DeliveryReport.report == "STICKER")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\Stickers.rpt";
                            else if (RecentBatch.report == "Packing" || DeliveryReport.report == "Packing")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\PackingReport.rpt";
                            else if (RecentBatch.report == "DR" || DeliveryReport.report == "DR")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\DeliveryReceipt.rpt";
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
                MessageBox.Show(error.Message, "DisplayAllBatches",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
                MessageBox.Show(error.Message, "DisplayAllBatches2", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(error.Message, "DisplayAllBatchData",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return "";
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
            Sql = "Update " + gClient.DataBaseName + " set DRNumber = '"+_newDR+" ' where DRNumber = '" + _temp.DrNumber + "' ";
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
                    SalesInvoiceDate = !reader.IsDBNull(0) ? reader.GetDateTime(0) : DateTime.Now,
                    SalesInvoiceNumber = !reader.IsDBNull(1) ? reader.GetDouble(1) : 0,
                    Quantity = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0,
                    Checktype = !reader.IsDBNull(3) ? reader.GetString(3) : "",
                    CheckName = !reader.IsDBNull(4) ? reader.GetString(4) : "",
                    Batch = !reader.IsDBNull(5) ? reader.GetString(5) : ""


                };
                _SI.Add(sales);

            }
            reader.Close();
            DBClosed();
            return _SI;
        }

        public string ContcatSalesInvoice(string batch, string checktype, string _location,DateTime salesinvoicedate)
        {

            DataTable dt = new DataTable();

            Sql = "select group_concat(distinct(SalesInvoice) separator ', ') from " + gClient.DataBaseName + " " +
           "WHERE salesinvoice is not null " +
           "and batch = '" + batch + "' " +
           "and chktype = '" + checktype + "'" +
           "and Location = '" + _location +"'; ";
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

                    price.BankCode = !reader.IsDBNull(0) ? reader.GetString(0) : "";
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
            try
            {


                //Orginal Query
                Sql = "Select P.BankCode, DocStampNumber,SalesInvoice,Count(ChkType) as Quantity,ChkType, P.Description, H.DocStamp, " +
                      "Username_DocStamp, CheckedByDS,PurchaseOrderNumber,P.QuantityOnHand,H.Batch," +
                      "(Count(ChkType) * H.DocStamp) as TotalAmount,H.location from " + gClient.DataBaseName +
                      " H left join " + gClient.PriceListTable + "  P on H.ChkType = P.FinalChkType and H.ProductCode = P.ProductCode" +
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
                                ", " + d.SalesInvoiceNumber + "," + d.TotalQuantity + ",'" + d.ChkType + "','" + d.DocDesc.Replace("'", "''") +
                                "'," + d.DocStampPrice + ",'" + d.PreparedBy + "','" + d.CheckedBy + "'," + d.POorder + "," + d.QuantityOnHand +
                                ",'" + d.batches + "'," + d.TotalAmount + ",'" + d.Location + "')";
                    MySqlCommand cmd2 = new MySqlCommand(Sql2, myConnect);
                    cmd2.ExecuteNonQuery();
                });
                //  });
                DBClosed();
                return;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "GetDocStampDetails", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void GetUsers(List<UserListModel> _users)
        {
            Sql = "Select FirstName from userlist";
            DBConnect();
            cmd = new MySqlCommand(Sql, myConnect);
            MySqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                UserListModel user = new UserListModel
                {
                    Id = !reader.IsDBNull(0) ? reader.GetString(0) : ""
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
            try
            {
                Sql = "Select LocationFlag,Address1,Address2,Address3,Address4,Address5,Address6  from " + gClient.BranchesTable + " where BranchCode = " + _branchCode.TrimEnd() + ";";
                DBConnect();
                cmd = new MySqlCommand(Sql, myConnect);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _branches.Flag = reader.GetInt32(0);
                    _branches.Address1 = !reader.IsDBNull(1) ? reader.GetString(1) : "";
                    _branches.Address2 = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    _branches.Address3 = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    _branches.Address4 = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    _branches.Address5 = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    _branches.Address6 = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                }
                reader.Close();
                DBClosed();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "GetBranchLocation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }
        public void GetBranchLocationbyBrstn(BranchesModel _branches, string _brstn)
        {
            try
            {
                Sql = "Select fBranchCode,fBranchName, fAddress2,fAddress3,fAddress4,fAddress5,fAddress6 from tlibbranch where fBrstn = '" + _brstn.Trim() + "';";
                //Sql = "Select LocationFlag,Address1,Address2,Address3,Address4,Address5,Address6  from " + gClient.BranchesTable + " where Brstn = '" + _brstn.TrimEnd() + "';";
                con = new MySqlConnection(ConString);
                cmd = new MySqlCommand(Sql, con);
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _branches.BranchCode = !reader.IsDBNull(0) ? reader.GetString(0) : "";
                    _branches.Address1 = !reader.IsDBNull(1) ? reader.GetString(1) : "";
                    _branches.Address2 = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    _branches.Address3 = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    _branches.Address4 = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    _branches.Address5 = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    _branches.Address6 = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                }
                reader.Close();
                con.Close();
                //DBClosed();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "GetBranchLocation", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        } //getting Branch Details from Ordering System Branch Table
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

        private void ByLocationAndTypeFilterByBranchCode(TypeofCheckModel _checks, int _DrNumber, DateTime _deliveryDate, string _username, int _packNumber)
        {
            int counter = 0;
            DBConnect();

            if (_checks.Regular_Personal_Direct.Count > 0)
            {
                // generating DR number per Branches with ChkType A
                var dBranch = _checks.Regular_Personal_Direct.Select(a => a.BranchCode).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Regular_Personal_Direct.Where(g => g.BranchCode == y).ToList();
                    dRecord.ForEach(r =>
                    {
                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);
                        

                    });
                    
                    _packNumber++;
                    _DrNumber++;
                    
                });

            }

            if (_checks.Regular_Personal_Provincial.Count > 0)
            {
                //Generating DR per CheckType in Provincial Branches
                var dBranch = _checks.Regular_Personal_Provincial.Select(a => a.BranchCode).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Regular_Personal_Provincial.Where(g => g.BranchCode == y).ToList();
                    dRecord.ForEach(r =>
                    {
                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);

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
            if (_checks.Regular_Commercial_Direct.Count > 0)
            {
                // generating DR number per Branches with ChkType B in Direct Branches
                var dBranch = _checks.Regular_Commercial_Direct.Select(a => a.BranchCode).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Regular_Commercial_Direct.Where(g => g.BranchCode == y).ToList();
                    dRecord.ForEach(r =>
                    {
                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);
            
                    });
                    _packNumber++;
                    _DrNumber++;
                    


                });

            }

            if (_checks.Regular_Commercial_Provincial.Count > 0)
            {
                //Generating DR per CheckType in Provincial Branches
                var dBranch = _checks.Regular_Commercial_Provincial.Select(a => a.BranchCode).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Regular_Commercial_Provincial.Where(g => g.BranchCode == y).ToList();
                    dRecord.ForEach(r =>
                    {

                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);


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

            if (_checks.ManagersCheck_Direct.Count > 0)
            {
                
                var dBranch = _checks.ManagersCheck_Direct.Select(a => a.BranchCode).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    //_DrNumber++;
                    var dRecord = _checks.ManagersCheck_Direct.Where(g => g.BranchCode == y).ToList();
                    dRecord.ForEach(r =>
                    {
                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);

                    });
                   
                    _packNumber++;
                   
                    _DrNumber++;

                });
            }

            if (_checks.ManagersCheck_Provincial.Count > 0)
            {
                var dBranch = _checks.ManagersCheck_Provincial.Select(a => a.BranchCode).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.ManagersCheck_Provincial.Where(g => g.BranchCode == y).ToList();
                    dRecord.ForEach(r =>
                    {
                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);

                    });
                    
                    _packNumber++;
                    if (counter == 10)
                    {
                    _DrNumber++;
                        counter = 0;
                    }


                });
            }
            if (_checks.ExecutiveOnline_Direct.Count > 0)
            {
                var dBranch = _checks.ExecutiveOnline_Direct.Select(a => a.BranchCode).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.ExecutiveOnline_Direct.Where(g => g.BranchCode == y).ToList();
                    dRecord.ForEach(r =>
                    {
                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);

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
            if (_checks.ExecutiveOnline_Provincial.Count > 0)
            {
                //Generating DR per CheckType in Provincial Branches
                var dBranch = _checks.ExecutiveOnline_Provincial.Select(a => a.BranchCode).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.ExecutiveOnline_Provincial.Where(g => g.BranchCode == y).ToList();
                    dRecord.ForEach(r =>
                    {
                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);

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
            DBClosed();

        }
        public void ByChequetType(TypeofCheckModel _checks, int _DrNumber, DateTime _deliveryDate, string _username, int _packNumber)
        {
            DBConnect();
            int counter = 0;
            var Personal = _checks.Regular_Personal.OrderBy(t => t.BranchName).ToList();
            if (Personal.Count > 0)
            {
                
                var _list = Personal.Select(r => r.BRSTN).Distinct().ToList();
             

                //Working Proccess Jan. 27 2021
                foreach (string Brstn in _list)
                {
                    var _model = Personal.Where(t => t.BRSTN == Brstn);

                    foreach (var r in _model)
                    {


                        Script(gClient.DataBaseName,r, _DrNumber, _deliveryDate, _username, _packNumber);


                    }
                    _packNumber++;
                    counter++;
                    
                    if (counter == 10)
                    {
                        _DrNumber++;
                        counter = 0;
                    }


                }

            }
            counter = 0;
            //_DrNumber++;

            var Comm = _checks.Regular_Commercial.OrderBy(r => r.BranchName).ToList();
            if (Comm.Count > 0)
            {
                _DrNumber++;
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
                    
                    _packNumber++;
                    counter++;
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
        public void ByLocationAndTypeWithDeliveryTO(TypeofCheckModel _checks, int _DrNumber, DateTime _deliveryDate, string _username, int _packNumber)
        {
            bool isInsertData = false;
            DBConnect();
            int counter = 0;
            //_checks.Regular_Personal_Direct.OrderBy(e => e.Location).ToList();
            //_checks.Regular_Commercial_Direct.OrderBy(e => e.Location).ToList();
            //_checks.Regular_Commercial_Provincial.OrderBy(e => e.Location).ToList();
            //_checks.Regular_Personal_Provincial.OrderBy(e => e.Location).ToList();
            //DeliveryTo same as BRSTN
            if (_checks.Regular_Personal_Direct.Count > 0)
            {

                // generating DR number per Branches with ChkType A
                var dBranch = _checks.Regular_Personal_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Regular_Personal_Direct.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() == g.BRSTN).ToList();
                    dRecord.ForEach(r =>
                    {
                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);

                        isInsertData = true;
                    });
                    if (isInsertData)
                    {
                        _packNumber++;
                        _DrNumber++;
                    }
                    isInsertData = false;

                    
                });

                    var dBranch2 = _checks.Regular_Personal_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch2.ForEach(y =>
                {
                    var dRecord = _checks.Regular_Personal_Direct.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != g.BRSTN).ToList();
                    dRecord.ForEach(r =>
                    {
                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);
                        isInsertData = true;
                    });



                        //isInsertData = false;

                });
            }
            if (_checks.Regular_Commercial_Direct.Count > 0)
            {
                // generating DR number per Branches with ChkType B in Direct Branches
                var dBranch = _checks.Regular_Commercial_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Regular_Commercial_Direct.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() == g.BRSTN).ToList();
                    dRecord.ForEach(r =>
                    {
                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);
                        isInsertData = true;
                    });
                    if (isInsertData)
                    {
                        _packNumber++;
                        _DrNumber++;
                    }
                    isInsertData = false;
                });
                // generating DR number per Branches with ChkType B in Direct Branches
                var dBranch2 = _checks.Regular_Commercial_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch2.ForEach(y =>
                {
                    var dRecord = _checks.Regular_Commercial_Direct.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != g.BRSTN).ToList();
                    dRecord.ForEach(r =>
                    {
                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);
                        isInsertData = true;
                    });
                    if (isInsertData)
                    {
                        _packNumber++;
                        _DrNumber++;
                    }

                       isInsertData = false;

                });


            }

            //With deliveryto 
            //if (_checks.Regular_Personal_Direct.Count > 0)
            //{
            //    if (!isInsertData)
            //    {
            //        _packNumber++;
            //        _DrNumber++;
            //    }
            //    // generating DR number per Branches with ChkType A
            //    var dBranch = _checks.Regular_Personal_Direct.Select(a => a.BRSTN).Distinct().ToList();
            //    dBranch.ForEach(y =>
            //    {
            //        var dRecord = _checks.Regular_Personal_Direct.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != g.BRSTN).ToList();
            //        dRecord.ForEach(r =>
            //        {
            //            Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);
            //            isInsertData = true;
            //        });



            //        //isInsertData = false;
            //    });

            //}
            //if (_checks.Regular_Commercial_Direct.Count > 0)
            //{
            //    if (!isInsertData)
            //    {
            //        _packNumber++;
            //        _DrNumber++;
            //    }
            //    // generating DR number per Branches with ChkType B in Direct Branches
            //    var dBranch = _checks.Regular_Commercial_Direct.Select(a => a.BRSTN).Distinct().ToList();
            //    dBranch.ForEach(y =>
            //    {
            //        var dRecord = _checks.Regular_Commercial_Direct.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != g.BRSTN).ToList();
            //        dRecord.ForEach(r =>
            //        {
            //            Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);
            //            isInsertData = true;
            //        });


            //        //   isInsertData = false;

            //    });

            //}



            if (_checks.Regular_Personal_Provincial.Count > 0)
            {
                //_DrNumber++;
                //Generating DR per CheckType in Provincial Branches
                var dBranch = _checks.Regular_Personal_Provincial.Select(a => a.BRSTN).Distinct().ToList();

                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Regular_Personal_Provincial.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() == g.BRSTN).ToList();
                    dRecord.ForEach(r =>
                    {
                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);
                        isInsertData = true;
                    });

                    if (isInsertData)
                    {
                        counter++;
                        _packNumber++;
                        if (counter == 10) // Increment DrNumber when the number of data in 1 (one) DrNumber reach 10 rows 
                        {
                            _DrNumber++;
                            counter = 0;
                        }
                    }
                    isInsertData = false;
                });

            }


            if (_checks.Regular_Personal_Provincial.Count > 0)
            {

                //Generating DR per CheckType in Provincial Branches
                var dBranch = _checks.Regular_Personal_Provincial.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {

                    var dRecord = _checks.Regular_Personal_Provincial.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != g.BRSTN).ToList();

                    dRecord.ForEach(r =>
                    {
                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);
                        isInsertData = true;
                    });
                    if (isInsertData)
                    {
                        _packNumber++;

                        _DrNumber++;
                    }

                    isInsertData = false;
                });

            }
            



            if (_checks.Regular_Commercial_Provincial.Count > 0)
            {
                //_DrNumber++;
                //Generating DR per CheckType in Provincial Branches
                var dBranch = _checks.Regular_Commercial_Provincial.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Regular_Commercial_Provincial.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() == g.BRSTN).ToList();
                    //if (dRecord != null)
                    //    _DrNumber++;

                    dRecord.ForEach(r =>
                    {

                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);

                        isInsertData = true;
                    });
                    if (isInsertData)
                    {
                        counter++;
                        _packNumber++;
                        if (counter == 10)
                        {
                            _DrNumber++;
                            counter = 0;
                        }
                    }
                    isInsertData = false;
                });

            }
            //Without DeliveryTo

            

            
            
            if (_checks.Regular_Commercial_Provincial.Count > 0)
            {
                //Generating DR per CheckType in Provincial Branches
                var dBranch = _checks.Regular_Commercial_Provincial.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Regular_Commercial_Provincial.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != y).ToList();
                    dRecord.ForEach(r =>
                    {

                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);

                        isInsertData = true;
                    });
                    if (isInsertData)
                    {
                        _packNumber++;

                        _DrNumber++;
                    }
                    isInsertData = false;
                });

            }
            // end with deliveryTO
             
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
            Sql = "Select PurchaseOrderNo from " + gClient.PurchaseOrderFinishedTable + " where ChequeName = '" + _chkType.Replace("'","''") + "'";
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
        public int GetPONUmberforSearching(string _chkType)
        {
            int _poNumber = 0;

            Sql = "Select PurchaseOrderNo from " + gClient.PurchaseOrderFinishedTable + " where ChequeName like '%" + _chkType.Replace("|", "''") + "%' ";
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
            if (gClient.BankCode == "008")
            {
                Sql = "Insert into " +_table+ " (BRSTN,BranchName,AccountNo,AcctNoWithHyphen,Name1,Name2,ChkType," +
                          "ChequeName,StartingSerial,EndingSerial,DRNumber,DeliveryDate,username,batch,PackNumber,Date,Time,location, BranchCode,OldBranchCode,PurchaseOrderNumber,Bank" +
                          ",Address2,Address3,Address4,Address5,Address6,ProductCode,Block,Segment,ProductType)" +
                          "VALUES('" + r.BRSTN + "','" + r.BranchName.Replace("'", "''") + "','" + r.AccountNo + "','" + r.AccountNoWithHypen + "','" + r.Name1.Replace("'", "''") +
                          "','" + r.Name2.Replace("'", "''") + "','" + r.ChkType + "','" + r.ChequeName.Replace("'","''") + "','" + r.StartingSerial + "','" + r.EndingSerial +
                          "','" + _DrNumber + "','" + _deliveryDate.ToString("yyyy-MM-dd") + "','" + _username + "','" +
                          r.Batch.TrimEnd() + "','" + _packNumber + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("hh:mm:ss") +
                          "','" + r.Location + "','" + r.BranchCode + "','" + r.OldBranchCode + "',"+r.PONumber+",'" + gClient.ShortName + "'," +
                          "'" + r.Address2.Replace("'","''") + "','" + r.Address3.Replace("'", "''") + "','" + r.Address4.Replace("'", "''") + "','" + r.Address5.Replace("'", "''") +
                          "','" + r.Address6.Replace("'", "''") + "','" + r.ProductCode +
                          "'," + r.Block  + "," + r.Segment+",'" + r.ProductType +"');";
                cmd = new MySqlCommand(Sql, myConnect);
                cmd.ExecuteNonQuery();
            }
            else
            {
                Sql = "Insert into " + _table + " (BRSTN,BranchName,AccountNo,AcctNoWithHyphen,Name1,Name2,ChkType," +
                          "ChequeName,StartingSerial,EndingSerial,DRNumber,DeliveryDate,username,batch,PackNumber,Date,Time,location,Bank,ProductCode," +
                          "BranchCode,DeliveryToBrstn,DeliveryToBranch)VALUES('" + r.BRSTN + "','" + r.BranchName + "','" + r.AccountNo + 
                          "','" + r.AccountNoWithHypen + "','" + r.Name1.Replace("'", "''") +
                          "','" + r.Name2.Replace("'", "''") + "','" + r.ChkType + "','" + r.ChequeName + "','" + r.StartingSerial + "','" + r.EndingSerial +
                          "','" + _DrNumber + "','" + _deliveryDate.ToString("yyyy-MM-dd") + "','" + _username + "','" +
                          r.Batch.TrimEnd() + "','" + _packNumber + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("hh:mm:ss") +
                          "','" + r.Location + "','" + gClient.ShortName + "','" + r.ProductCode + "','"+ r.BranchCode + "','" + r.DeliveryTo + "','" + r.DeliverytoBranch + "');";
                cmd = new MySqlCommand(Sql, myConnect);
                cmd.ExecuteNonQuery();
            }
        }
        public int CheckPOQuantity(int _PO, string _chkType )
        {
            int _remainingbalance = 0;
            int IniatailBalance = 0;
            int ProcessedQuantity = 0;
            Sql = "Select Quantity  from "+gClient.PurchaseOrderFinishedTable +" where PurchaseOrderNo = " + _PO + " and ChequeName = '" + _chkType.Replace("'","''") + "';";
            DBConnect();
            cmd = new MySqlCommand(Sql, myConnect);
            MySqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                IniatailBalance = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0;
            }
            reader.Close();
            DBClosed();
            string Sql2 = "Select Count(PurchaseOrderNumber) from " + gClient.DataBaseName + " where PurchaseOrderNumber = " + _PO + " and ChequeName = '" + _chkType.Replace("'", "''") + "' ";
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
        public string ConcatDRNumbers(string _batch, string _chkType, string _location)
        {
            try
            {
                DBConnect();
                string _dr = "";
                Sql = "Select Distinct(DRNumber) from " + gClient.DataBaseName + " where Batch = '" + _batch.Trim() +
                      "' and ChkType = '" + _chkType + "' and Location = '" + _location + "'";

                cmd = new MySqlCommand(Sql, myConnect);
                MySqlDataReader reader = cmd.ExecuteReader();
                List<int> AllDr = new List<int>();
                int oldDr = 0;

                int existingDr = 0;
                int rightDR = 0;
                // string lastDr = "";
                string drDisplay = "";
                while (reader.Read())
                {
                    rightDR = reader.GetInt32(0);
                    AllDr.Add(rightDR);
                }
                reader.Close();
                var sortedDR = AllDr.OrderBy(x => x).ToList();
                for (int i = 0; i < sortedDR.Count; i++)
                {
                    existingDr = sortedDR[i];
                    if (oldDr == 0)
                    {

                        if (drDisplay == "")
                        {
                            drDisplay += existingDr.ToString();
                        }
                    }
                    else
                    {
                        if ((oldDr + 1) == existingDr)
                        {
                            //drDisplay = drDisplay + " - " + existingDr.ToString().Substring(existingDr.ToString().Length - (existingDr.ToString().Length - 4), 3);
                            drDisplay = drDisplay + " - " + existingDr.ToString();
                        }
                        else

                            drDisplay = drDisplay + ", " + existingDr.ToString();

                    }
                    oldDr = existingDr;
                }
                _dr = drDisplay;
                DBClosed();
                return _dr;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Concat DR Numbers", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }
        public  void DisableControls(ToolStripMenuItem _item)
        {
            if (gClient.DataBaseName != "producers_history")
            {
                _item.Enabled = true;

            }
            else
            {
                _item.Enabled = false;
            }
        }
        public void GetProducts(List<ProductModel> _products)
        {
            Sql = "Select ProductCode, BankCode,ChequeName,Description,FinalChkType," +
                "DocStamp,UnitPrice, DatetimeModified, Unit,QuantityOnHand,Location from " + gClient.PriceListTable;
            DBConnect();
            cmd = new MySqlCommand(Sql, myConnect);
            MySqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                ProductModel product = new ProductModel
                {
                    ProductCode = !reader.IsDBNull(0) ? reader.GetString(0) : "",
                    BankCode = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                    ChequeName = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                    Description = !reader.IsDBNull(3) ? reader.GetString(3) : "",
                    ChkType = !reader.IsDBNull(4) ? reader.GetString(4) : "",
                    DocStampPrice = !reader.IsDBNull(5) ? reader.GetDouble(5) : 0,
                    UnitPrice = !reader.IsDBNull(6) ? reader.GetDouble(6) : 0,
                    DateModified = !reader.IsDBNull(7) ? reader.GetDateTime(7) : DateTime.Now,
                    Unit = !reader.IsDBNull(8) ? reader.GetString(8) : "",
                    BalanceQuantity = !reader.IsDBNull(9) ? reader.GetInt32(9) : 0,
                    DeliveryLocation = !reader.IsDBNull(10) ? reader.GetString(10): ""
                };
                _products.Add(product);
            }
            reader.Close();
            DBClosed();
        }
        public void AddProductPrice(ProductModel _product)
        {
            Sql = "Insert into " + gClient.PriceListTable + " (ProductCode,BankCode, ChequeName,Description,FinalChkType,Docstamp,UnitPrice,DatetimeModified,Unit,Location)" +
                    "Values('" +_product.ProductCode + "','" + _product.BankCode + "', '"+ _product.ChequeName.Replace("'","''") +"','"+_product.Description.Replace("'","''")+
                    "','"+_product.ChkType+"',"+_product.DocStampPrice+"," + _product.UnitPrice+",'" +
                    _product.DateModified.ToString("yyyy-MM-dd")+"','"+_product.Unit+"','"+_product.DeliveryLocation + "');";
            DBConnect();
            cmd = new MySqlCommand(Sql, myConnect);
            cmd.ExecuteNonQuery();
            DBClosed();
        }
        public void ModifyProductsPrice(ProductModel _product)
        {
            Sql = "Update " + gClient.PriceListTable + " set BankCode = '"+_product.BankCode+"'," +
                " ChequeName = '"+_product.ChequeName.Replace("'","''")+"', Description = '"+_product.Description.Replace("'","''")+ "' ,FinalChkType = '" +_product.ChkType+"'" +
                ",Docstamp = "+_product.DocStampPrice + ",UnitPrice = " +_product.UnitPrice + " ,DatetimeModified = '" + _product.DateModified.ToString("yyyy-MM-dd hh:mm:ss")+
                "',Unit = '"+_product.Unit+ "', Location = '"+_product.DeliveryLocation+ "' where ProductCode ='" + _product.ProductCode + "'";
            DBConnect();
            cmd = new MySqlCommand(Sql, myConnect);
            cmd.ExecuteNonQuery();
            DBClosed();
        }
        public static void bg_dtg(DataGridView dgv)
        {
            try
            {

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if (IsOdd(i))
                    {

                        dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }
        public static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }
        public List<ChequeTypesModel> GetChequeTypes(List<ChequeTypesModel> _cheques)
        {
            Sql = "Select Type, ChequeName, Description, A.DateTimeModified,ProductName  from " + gClient.ChequeTypeTable  + " A " +
                "inner join "  + gClient.ProductTable + " B on A.CProductCode = B.CProductCode ;";
            DBConnect();
            cmd = new MySqlCommand(Sql,myConnect);
            MySqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                ChequeTypesModel c = new ChequeTypesModel
                {
                    Type = !reader.IsDBNull(0) ? reader.GetString(0) : "",
                    ChequeName = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                    Description = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                    DateModified = !reader.IsDBNull(3) ? reader.GetDateTime(3) : DateTime.Now,
                    ProductName = !reader.IsDBNull(4) ? reader.GetString(4) : ""
                };
                _cheques.Add(c);
            }
            reader.Close();
            DBClosed();
            return _cheques;
        }
        public void AddChequeType(ChequeTypesModel _cheque)
        {
            Sql = "Insert into " + gClient.ChequeTypeTable + " (Type,ChequeName,Description,DatetimeModified,CProductCode)" +
                    "Values('" + _cheque.Type + "', '" + _cheque.ChequeName.Replace("'", "''") + "','" + _cheque.Description.Replace("'", "''") +
                     "','"+ _cheque.DateModified.ToString("yyyy-MM-dd hh:mm:ss") + "'," + _cheque.ProductCode + ");";
            DBConnect();
            cmd = new MySqlCommand(Sql, myConnect);
            cmd.ExecuteNonQuery();
            DBClosed();
        }
        public void ModifyChequeTypes(ChequeTypesModel _cheque)
        {
            Sql = "Update " + gClient.ChequeTypeTable + " set  ChequeName = '" + _cheque.ChequeName.Replace("'", "''") + "', Description = '" +
                _cheque.Description.Replace("'", "''") + "' ,DatetimeModified = '" + _cheque.DateModified.ToString("yyyy-MM-dd hh:mm:ss") + "'" +
                " where CProductCode = " + _cheque.ProductCode + " and Type = '" + _cheque.Type + "'" ;
            DBConnect();
            cmd = new MySqlCommand(Sql, myConnect);
            cmd.ExecuteNonQuery();
            DBClosed();
        }
        public List<ChequeProductModel> GetProducts(List<ChequeProductModel> _cheques)
        {
            Sql = "Select CProductCode, ProductName, DateTimeModified from " + gClient.ProductTable;
            DBConnect();
            cmd = new MySqlCommand(Sql, myConnect);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ChequeProductModel c = new ChequeProductModel
                {
                    ProductCode = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                    ProductName = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                    DateModified = !reader.IsDBNull(2) ? reader.GetDateTime(2) : DateTime.Now
                };
                _cheques.Add(c);
            }
            reader.Close();
            DBClosed();
            return _cheques;
        }
        public void AddProducts(ChequeProductModel _cheque)
        {
            Sql = "Insert into " + gClient.ProductTable + " (CProductCode,ProductName,DatetimeModified)" +
                    "Values('" + _cheque.ProductCode + "', '" + _cheque.ProductName.Replace("'", "''")  +
                     "','" + _cheque.DateModified.ToString("yyyy-MM-dd hh:mm:ss") + "');";
            DBConnect();
            cmd = new MySqlCommand(Sql, myConnect);
            cmd.ExecuteNonQuery();
            DBClosed();
        }
        public void ModifyProduct(ChequeProductModel _cheque)
        {
            Sql = "Update " + gClient.ProductTable + " set  ProductName = '" + _cheque.ProductName.Replace("'", "''") 
                 + "' ,DatetimeModified = '" + _cheque.DateModified.ToString("yyyy-MM-dd hh:mm:ss") + "' where CProductCode = '"+_cheque.ProductCode+"'";
            DBConnect();
            cmd = new MySqlCommand(Sql, myConnect);
            cmd.ExecuteNonQuery();
            DBClosed();
        }
        public List<ChequeTypesModel> fChequeTypes(List<ChequeTypesModel> _cheques)
        {
            Sql = "SELECT C.Type, C.ChequeName, C.Description, P.CProductCode, P.ProductName FROM "+gClient.ChequeTypeTable+ " C" +
                    " inner join "+ gClient.ProductTable+ " P on C.CProductCode = P.CProductCode; ";
            DBConnect();
            cmd = new MySqlCommand(Sql, myConnect);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ChequeTypesModel c = new ChequeTypesModel
                {
                    Type = !reader.IsDBNull(0) ? reader.GetString(0) : "",
                    ChequeName = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                    Description = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                    ProductCode = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0,
                    ProductName = !reader.IsDBNull(4) ? reader.GetString(4) :""

                };
                _cheques.Add(c);
            }
            return _cheques;
        }
        public string GetLastProductCode()
         {
            string _pCode = "";
            Sql = "Select CProductCode from " + gClient.ProductTable + " order by CProductCode desc Limit 1";

            DBConnect();
            cmd = new MySqlCommand(Sql, myConnect);
            MySqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                _pCode = !reader.IsDBNull(0) ? reader.GetInt32(0).ToString() : "";
            }
            reader.Close();
            DBClosed();
            return _pCode;
        }
        public string GetChequeNamewithProductCode(string _chkType, string _productName)
        {
            string chequeName = "";
            Sql = " SELECT ChequeName FROM " + gClient.ChequeTypeTable + " A" +
                 " inner join " + gClient.ProductTable + " B on A.CProductCode = B.CProductCode where Type = '" + _chkType + "' and ChequeName like '" + _productName.TrimEnd() + "%';";
            //Sql = "Select ChequeName from " + gClient.ChequeTypeTable + " where Type ='" + _chkType + "' and  inner join";
            DBConnect();
            cmd = new MySqlCommand(Sql, myConnect);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                chequeName = !reader.IsDBNull(0) ? reader.GetString(0) : "";
            }
            reader.Close();
            DBClosed();
            return chequeName;
        }
        public string GetChequeName(string _chkType)
        {
            string chequeName = "";
            Sql = " SELECT ChequeName FROM " + gClient.ChequeTypeTable + " A" +
                 " inner join " + gClient.ProductTable + " B on A.CProductCode = B.CProductCode where Type = '" + _chkType + "';";
            //Sql = "Select ChequeName from " + gClient.ChequeTypeTable + " where Type ='" + _chkType + "' and  inner join";
            DBConnect();
            cmd = new MySqlCommand(Sql, myConnect);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                chequeName = !reader.IsDBNull(0) ? reader.GetString(0) : "";
            }
            reader.Close();
            DBClosed();
            return chequeName;
        }
        public string GetPackingListwithSticker(string _batch, List<TempModel> list)
        {
            try
            {
                DBConnect();
                list.Clear();
                Sql = "SELECT DRNumber, PackNumber, BRSTN, ChkType, BranchName, COUNT(BRSTN)," +
                     "MIN(StartingSerial), MAX(EndingSerial),ChequeName, Batch,username,BranchCode,OldBranchCode,location,PurchaseOrderNumber,Bank,Address2,Address3,Address4," +
                     "Name1,Name2,AccountNo FROM " +
                     gClient.DataBaseName + " WHERE  Batch = '" + _batch.TrimEnd() + "' GROUP BY DRNumber, BRSTN, ChkType,BranchName," +
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
                    order.BankCode = !myReader.IsDBNull(15) ? myReader.GetString(15) : "";
                    order.Address2 = !myReader.IsDBNull(16) ? myReader.GetString(16) : "";
                    order.Address3 = !myReader.IsDBNull(17) ? myReader.GetString(17) : "";
                    order.Address4 = !myReader.IsDBNull(18) ? myReader.GetString(18) : "";
                    order.Name1 = !myReader.IsDBNull(19) ? myReader.GetString(19) : "";
                    order.Name2 = !myReader.IsDBNull(20) ? myReader.GetString(20) : "";
                    order.AccountNo = !myReader.IsDBNull(21) ? myReader.GetString(21) : "";


                    list.Add(order);
                }
                DBClosed();
                DBConnect();
                string sqldel = "Delete from " + gClient.PackingList + ";";
                MySqlCommand comdel = new MySqlCommand(sqldel, myConnect);
                comdel.ExecuteNonQuery();

                DBClosed();
                int licnt = 1;
                DBConnect();
                for (int i = 0; i < list.Count; i++)
                {
                    if (licnt == 1)
                    {
                        string sql2 = "Insert into " + gClient.PackingList + " (DRNumber,PackNumber,BRSTN, ChkType, BranchName,Qty,StartingSerial," +
                                  "EndingSerial,ChequeName,Batch,username,BranchCode,OldBranchCode,Location,PONumber," +
                                  "Bank,Address2,Address3,Address4,Name1,Name2,AccountNo)" +
                                  " Values('" + list[i].DrNumber + "','" + list[i].PackNumber +
                                  "','" + list[i].BRSTN + "','" + list[i].ChkType + "','" + list[i].BranchName + "'," + list[i].Qty +
                                  ",'" + list[i].StartingSerial + "','" + list[i].EndingSerial + "','" + list[i].ChequeName.Replace("'", "''") + "','" +
                                  list[i].Batch + "','" + list[i].username + "','" + list[i].BranchCode + "','" + list[i].OldBranchCode.TrimEnd() + "','" +
                                  list[i].Location + "'," + list[i].PONumber + ",'" + list[i].BankCode + "','" + list[i].Address2.Replace("'", "''") + "','" + list[i].Address3.Replace("'", "''") + "','" +
                                  list[i].Address4.Replace("'", "''") + "','" + list[i].Name1.Replace("'", "''") + "','" + list[i].Name2.Replace("'", "''") + "','" +
                                  list[i].AccountNo + "');";
                        MySqlCommand cmd2 = new MySqlCommand(sql2, myConnect);
                        cmd2.ExecuteNonQuery();
                        licnt++;
                    }
                    else if (licnt == 2)
                    {
                        string sql2 = "Update " + gClient.PackingList + " set BRSTN2 = '" + list[i].BRSTN + "',BranchName2 = '" + list[i].BranchName.Replace("'", "''") +
                                    "',Qty2 = " + list[i].Qty + ",ChkType2 = '" + list[i].ChkType + "', ChequeName2 = '" + list[i].ChequeName.Replace("'","''") + "',StartingSerial2 = '" + list[i].StartingSerial +
                                      "',EndingSerial2 = '" + list[i].EndingSerial + "',Address22 ='" + list[i].Address2.Replace("'", "''") +
                                      "', Address32 ='" + list[i].Address3.Replace("'", "''") + "',Address42 ='" + list[i].Address4.Replace("'", "''") +
                                       "' , BranchCode2 = '"+ list[i].BranchCode+ "'" +
                                      "where BRSTN = '" + list[i - 1].BRSTN + "' and ChkType = '" + list[i - 1].ChkType + "';";

                        MySqlCommand cmd2 = new MySqlCommand(sql2, myConnect);
                        cmd2.ExecuteNonQuery();
                        licnt = 1;
                    }
                }

                DBClosed();
                return _batch;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "GetPackingListwithSticker", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }
        public List<OrderModel> GetProcessedDataFromDB(List<OrderModel> _tempList, string _bankCode, string _batch) // Updated By ET for getting history data from the Ordering system March 31, 2021
        {

            //Sql = "Select A.fBatchNo,A.fBrstn,fBranchCode,A.fBranchName,A.fAccountNo,fAccountName1,fAccountName2, " +
            //        "A.fNoOfBooks,fStartSerial, fEndSerial,fBlocks,fBlock,A.fDeliveryDate " +
            //        "from tmaincheque A inner join tmainbatch B on A.fBankCode = B.fBankCode " +
            //        "where A.fBankCode = '" + _bankCode + "' and A.fBatchNo = '"+_batch+"' limit 50000; ";
            Sql = "Select A.fBatchNo,A.fBrstn,A.fBranchName,fAccountNo,fAccountName1,fAccountName2,fNoOfBooks," +
                    "fStartSerial,fEndSerial,fBlocks,fBlock,fDeliveryDate,fBranchCode,C.fChequeTypeName, C.fTag,fBranchCode2,fProductCode " +
                    "from tmaincheque A inner join tlibbranch B on (B.fBRSTN = A.fBRSTN) " +
                    "  inner join tlibchequetype C on (A.fChequeTypeSeq = C.fSeq) " +
                    " where A.fBankCode = '" + _bankCode + "' and A.fBatchNo = '" + _batch + "';";
            con = new MySqlConnection(ConString);
            con.Open();
            cmd = new MySqlCommand(Sql, con);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                OrderModel t = new OrderModel();

                t.Batch = !reader.IsDBNull(0) ? reader.GetString(0) : "";
                t.BRSTN = !reader.IsDBNull(1) ? reader.GetString(1) : "";
                t.BranchName = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                t.AccountNo = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                t.Name1 = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                t.Name2 = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                t.Quantity = !reader.IsDBNull(6) ? int.Parse(reader.GetString(6)) : 0;
                t.StartingSerial = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                t.EndingSerial = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                t.Block = !reader.IsDBNull(9) ? int.Parse(reader.GetString(9)) : 0;
                t.Segment = !reader.IsDBNull(10) ? int.Parse(reader.GetString(10)) : 0;
                t.DeliveryDate = !reader.IsDBNull(11) ? DateTime.Parse(reader.GetString(11)) : DateTime.Now;
                t.BranchCode = !reader.IsDBNull(12) ? reader.GetString(12) : "";
                t.ChequeName = !reader.IsDBNull(13) ? reader.GetString(13) : "";
                t.ChkType = !reader.IsDBNull(14) ? reader.GetString(14) : "";
                t.OldBranchCode = !reader.IsDBNull(15) ? reader.GetString(15) : "";
                t.ProductType = !reader.IsDBNull(16) ? reader.GetString(16) : "";

                _tempList.Add(t);
            }
            reader.Close();
            con.Close();
            return _tempList;
        } 
        public int fGetTotalChecks(string _chkName, List<OrderModel> _list)
        {
            int _total = 0;
            con = new MySqlConnection(ConString);

            var chkType = _list.Where(x => x.ChequeName == _chkName).ToList();
            chkType.Count();
            string Sql = "SELECT f FROM   WHERE CHKNAME LIKE '" + _chkName + "%' ";
            cmd = new MySqlCommand(Sql, con);
            con.Open();
            MySqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                _total++;
            }
            myReader.Close();
            con.Close();
            return _total;
        }
        public List<TempModel> fGetDrDirect(string _batch, List<TempModel> list)
        {
            try
            {
                DBConnect();
                Sql = "SELECT DRNumber, PackNumber, BRSTN, ChkType, BranchName, COUNT(BRSTN)," +
                     "MIN(StartingSerial), MAX(EndingSerial),ChequeName, Batch,username,BranchCode,OldBranchCode,location,PurchaseOrderNumber,Bank,Address2,Address3,Address4," +
                     "Name1,Name2,AccountNo,DeliveryToBrstn,DeliveryToBranch,AttentionTo FROM " +
                     gClient.DataBaseName + " WHERE  Batch = '" + _batch.TrimEnd() + "' and Location = 'Direct' GROUP BY DRNumber, BRSTN, ChkType, BranchName," +
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
                    order.BankCode = !myReader.IsDBNull(15) ? myReader.GetString(15) : "";
                    order.Address2 = !myReader.IsDBNull(16) ? myReader.GetString(16) : "";
                    order.Address3 = !myReader.IsDBNull(17) ? myReader.GetString(17) : "";
                    order.Address4 = !myReader.IsDBNull(18) ? myReader.GetString(18) : "";
                    order.Name1 = !myReader.IsDBNull(19) ? myReader.GetString(19) : "";
                    order.Name2 = !myReader.IsDBNull(20) ? myReader.GetString(20) : "";
                    order.AccountNo = !myReader.IsDBNull(21) ? myReader.GetString(21) : "";
                    order.DeliveryToBrstn = !myReader.IsDBNull(22) ? myReader.GetString(22) : "";
                    order.DeliveryToBranch = !myReader.IsDBNull(23) ? myReader.GetString(23) : "";
                    order.AttentionTo = !myReader.IsDBNull(24) ? myReader.GetString(24) : "";

                    list.Add(order);
                }
                DBClosed();
                DBConnect();
                string sqldel = "Delete from " + gClient.DRTempTable + ";";
                MySqlCommand comdel = new MySqlCommand(sqldel, myConnect);
                comdel.ExecuteNonQuery();

                DBClosed();
                
                var TotalA = list.Where(x => x.ChkType == "A");
                var TotalB = list.Where(x => x.ChkType == "B");
                var TotalC = list.Where(x => x.ChkType == "C");
                var TotalD = list.Where(x => x.ChkType == "D");
                DBConnect();
                for (int i = 0; i < list.Count; i++)
                {
                    string sql2 = "Insert into " + gClient.DRTempTable + " (DRNumber,PackNumber,BRSTN, ChkType, BranchName,Qty,StartingSerial," +
                                  "EndingSerial,ChequeName,Batch,username,BranchCode,OldBranchCode,Location,PONumber,ConcatinatedDRA," +
                                  "Bank,Address2,Address3,Address4,Name1,Name2,AccountNo,TotalA,TotalB,BankName,AttentionTo,TIN,DeliveryToBranch,DeliveryToBrstn)" +
                                  " Values('" + list[i].DrNumber + "','" + list[i].PackNumber +
                                  "','" + list[i].BRSTN + "','" + list[i].ChkType + "','" + list[i].BranchName + "'," + list[i].Qty +
                                  ",'" + list[i].StartingSerial + "','" + list[i].EndingSerial + "','" + list[i].ChequeName.Replace("'", "''") + "','" +
                                  list[i].Batch + "','" + list[i].username + "','" + list[i].BranchCode + "','" + list[i].OldBranchCode + "','" +
                                  list[i].Location + "'," + list[i].PONumber + ",'','" + gClient.Description.ToUpper() + "','" + list[i].Address2.Replace("'", "''") +
                                  "','" + list[i].Address3.Replace("'", "''") + "','" + list[i].Address4.Replace("'", "''") + "','" + list[i].Name1.Replace("'", "''") +
                                  "','" + list[i].Name2.Replace("'", "''") + "','" + list[i].AccountNo + "','" + TotalA.Count() + "','" + TotalB.Count() +
                                  "','" + gClient.Description.ToUpper().Replace("'", "''").TrimEnd() + "','" + list[i].AttentionTo.Replace("'", "''").TrimEnd() +
                                  "','" + gClient.TIN + "','" + list[i].DeliveryToBranch + "','" + list[i].DeliveryToBrstn + "');";
                    MySqlCommand cmd2 = new MySqlCommand(sql2, myConnect);
                    cmd2.ExecuteNonQuery();
                }
                DBClosed();
                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GetDRDirectBranches", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return list;
            }

        }
        public List<TempModel> fGetDrProvincial(string _batch, List<TempModel> list)
        {
            try
            {
                DBConnect();
                Sql = "SELECT DRNumber, PackNumber, BRSTN, ChkType, BranchName, COUNT(BRSTN)," +
                     "MIN(StartingSerial), MAX(EndingSerial),ChequeName, Batch,username,BranchCode,OldBranchCode,location,PurchaseOrderNumber,Bank,Address2,Address3,Address4," +
                     "Name1,Name2,AccountNo,DeliveryToBrstn,DeliveryToBranch,AttentionTo FROM " +
                     gClient.DataBaseName + " WHERE  Batch = '" + _batch.TrimEnd() + "' and Location = 'Provincial' GROUP BY DRNumber, BRSTN, ChkType, BranchName," +
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
                    order.BankCode = !myReader.IsDBNull(15) ? myReader.GetString(15) : "";
                    order.Address2 = !myReader.IsDBNull(16) ? myReader.GetString(16) : "";
                    order.Address3 = !myReader.IsDBNull(17) ? myReader.GetString(17) : "";
                    order.Address4 = !myReader.IsDBNull(18) ? myReader.GetString(18) : "";
                    order.Name1 = !myReader.IsDBNull(19) ? myReader.GetString(19) : "";
                    order.Name2 = !myReader.IsDBNull(20) ? myReader.GetString(20) : "";
                    order.AccountNo = !myReader.IsDBNull(21) ? myReader.GetString(21) : "";
                    order.DeliveryToBrstn = !myReader.IsDBNull(22) ? myReader.GetString(22) : "";
                    order.DeliveryToBranch = !myReader.IsDBNull(23) ? myReader.GetString(23) : "";
                    order.AttentionTo = !myReader.IsDBNull(24) ? myReader.GetString(24) : "";

                    list.Add(order);
                }
                DBClosed();
                DBConnect();
                string sqldel = "Delete from " + gClient.DRTempTable + ";";
                MySqlCommand comdel = new MySqlCommand(sqldel, myConnect);
                comdel.ExecuteNonQuery();

                DBClosed();
                
                var TotalA = list.Where(x => x.ChkType == "A");
                var TotalB = list.Where(x => x.ChkType == "B");
                var TotalC = list.Where(x => x.ChkType == "C");
                var TotalD = list.Where(x => x.ChkType == "D");
                DBConnect();
                for (int i = 0; i < list.Count; i++)
                {


                    string sql2 = "Insert into " + gClient.DRTempTable + " (DRNumber,PackNumber,BRSTN, ChkType, BranchName,Qty,StartingSerial," +
                                  "EndingSerial,ChequeName,Batch,username,BranchCode,OldBranchCode,Location,PONumber,ConcatinatedDRA," +
                                  "Bank,Address2,Address3,Address4,Name1,Name2,AccountNo,TotalA,TotalB,BankName,AttentionTo,TIN,DeliveryToBranch,DeliveryToBrstn)" +
                                  " Values('" + list[i].DrNumber + "','" + list[i].PackNumber +
                                  "','" + list[i].BRSTN + "','" + list[i].ChkType + "','" + list[i].BranchName + "'," + list[i].Qty +
                                  ",'" + list[i].StartingSerial + "','" + list[i].EndingSerial + "','" + list[i].ChequeName.Replace("'", "''") + "','" +
                                  list[i].Batch + "','" + list[i].username + "','" + list[i].BranchCode + "','" + list[i].OldBranchCode + "','" +
                                  list[i].Location + "'," + list[i].PONumber + ",'','" + gClient.Description.ToUpper() + "','" + list[i].Address2.Replace("'", "''") +
                                  "','" + list[i].Address3.Replace("'", "''") + "','" + list[i].Address4.Replace("'", "''") + "','" + list[i].Name1.Replace("'", "''") +
                                  "','" + list[i].Name2.Replace("'", "''") + "','" + list[i].AccountNo + "','" + TotalA.Count() + "','" + TotalB.Count() +
                                  "','" + gClient.Description.ToUpper().Replace("'", "''").TrimEnd() + "','" + list[i].AttentionTo.Replace("'", "''").TrimEnd() +
                                  "','" + gClient.TIN + "','" + list[i].DeliveryToBranch + "','" + list[i].DeliveryToBrstn + "');";
                    MySqlCommand cmd2 = new MySqlCommand(sql2, myConnect);
                    cmd2.ExecuteNonQuery();
                }
                DBClosed();
                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GetDRProvincialBranches", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return list;
            }

        }

    }
}
    