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
using CPMS_Accounting.Forms;
using System.IO.Compression;

namespace CPMS_Accounting.Procedures
{
    class ProcessServices
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // MySqlConnection con = new MySqlConnection();
        public MySqlConnection myConnect;
        public string databaseName = "";
        List<string> db = new List<string>();
        List<Int64> listofDR = new List<long>();
        MySqlCommand cmd;
        string Sql = "";
        string ErrorText;
        List<ChequeTypesModel> chequeTypesList = new List<ChequeTypesModel>();
        string ConString = ConfigurationManager.AppSettings["ConnectionStringOrdering"];
        MySqlConnection con;
        
        public void DBConnect()
        {
            try
            {
                log.Info("Opening Database Connections..");
                string DBConnection = "";

                if (frmProgramSelection.selectSystem != "Ordering System")
                {
                    DBConnection = ConfigurationManager.AppSettings["ConnectionString"];
                }
                else
                {
                    DBConnection = ConfigurationManager.AppSettings["ConnectionStringOrdering1"];
                }

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
            log.Info("Closing Connections Database Connections..");
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
        internal static bool GetBankList(ref DataTable dt)
        {
            throw new NotImplementedException();
        }
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
            checkType.DragonBlue_Personal_Direct = new List<OrderModel>();
            checkType.DragonBlue_Commercial_Direct = new List<OrderModel>();
            checkType.DragonBlue_Commercial_Provicial = new List<OrderModel>();
            checkType.DragonBlue_Personal_Provincial = new List<OrderModel>();
            checkType.DragonYellow_Commercial_Direct = new List<OrderModel>();
            checkType.DragonYellow_Commercial_Provincial = new List<OrderModel>();
            checkType.DragonYellow_Personal_Direct = new List<OrderModel>();
            checkType.DragonYellow_Personal_Provincial = new List<OrderModel>();
            checkType.Reca_Commercial_Direct = new List<OrderModel>();
            checkType.Reca_Commercial_Provincial = new List<OrderModel>();
            checkType.Reca_Personal_Direct = new List<OrderModel>();
            checkType.Reca_Personal_Provincial = new List<OrderModel>();
            checkType.Online_Commercial_Direct = new List<OrderModel>();
            checkType.Online_Commercial_Provincial = new List<OrderModel>();
            checkType.Online_Personal_Direct = new List<OrderModel>();
            checkType.Online_Personal_Provincial = new List<OrderModel>();
            checkType.Customized_Direct = new List<OrderModel>();
            checkType.Customized_Provincial = new List<OrderModel>();

            List<ChequeTypesModel> type = new List<ChequeTypesModel>();
            GetChequeTypes(type);
            foreach (OrderModel _check in _orders)
            {


                if (_dReportStyle != 0 && _pReportStyle != 0)
                {
                    //foreach (var t in type)
                    //{
                    if(gClient.BankCode == "008")
                    { 
                        log.Info("Separating Data by Checktype and Location ");
                        log.Info("Adding Data into a List"); 

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

                    }
                    //else if (_dReportStyle != 0 && _pReportStyle != 0 && _withDeliveryTo == 1)
                    else

                    {
                        List<ProductModel> _listofCheques = new List<ProductModel>();
                        GetProducts(_listofCheques);
                        //for (int i = 0; i < _listofCheques.Count; i++)
                        //{



                        for (int i = 0; i < _listofCheques.Count; i++)
                        {


                            if (_check.ChkType == _listofCheques[i].ChkType && _check.Location == "Direct" && _check.ProductCode == _listofCheques[i].ProductCode)
                            {
                                if(_check.ProductCode == _listofCheques[0].ProductCode)
                                    checkType.Regular_Personal_Direct.Add(_check);
                                else if (_check.ProductCode == _listofCheques[2].ProductCode)
                                    checkType.Regular_Commercial_Direct.Add(_check);
                                else if (_check.ProductCode == _listofCheques[5].ProductCode)
                                    checkType.DragonBlue_Personal_Direct.Add(_check);
                                else if (_check.ProductCode == _listofCheques[6].ProductCode)
                                    checkType.DragonBlue_Commercial_Direct.Add(_check);
                                else if (_check.ProductCode == _listofCheques[8].ProductCode)
                                    checkType.DragonYellow_Personal_Direct.Add(_check);
                                else if (_check.ProductCode == _listofCheques[10].ProductCode)
                                    checkType.DragonYellow_Commercial_Direct.Add(_check);
                                else if (_check.ProductCode == _listofCheques[12].ProductCode)
                                    checkType.Online_Commercial_Direct.Add(_check);
                                else if (_check.ProductCode == _listofCheques[14].ProductCode)
                                    checkType.Reca_Commercial_Direct.Add(_check);

                            }
                            else if (_check.ChkType == _listofCheques[i].ChkType && _check.Location == "Provincial" && _check.ProductCode == _listofCheques[i].ProductCode)
                            {
                                if (_check.ProductCode == _listofCheques[1].ProductCode)
                                    checkType.Regular_Personal_Provincial.Add(_check);
                                else if (_check.ProductCode == _listofCheques[3].ProductCode)
                                    checkType.Regular_Commercial_Provincial.Add(_check);
                                else if (_check.ProductCode == _listofCheques[4].ProductCode)
                                    checkType.DragonBlue_Personal_Provincial.Add(_check);
                                else if (_check.ProductCode == _listofCheques[7].ProductCode)
                                    checkType.DragonBlue_Commercial_Provicial.Add(_check);
                                else if (_check.ProductCode == _listofCheques[9].ProductCode)
                                    checkType.DragonYellow_Personal_Provincial.Add(_check);
                                else if (_check.ProductCode == _listofCheques[11].ProductCode)
                                    checkType.DragonYellow_Commercial_Provincial.Add(_check);
                                else if (_check.ProductCode == _listofCheques[13].ProductCode)
                                    checkType.Online_Commercial_Provincial.Add(_check);
                                else if (_check.ProductCode == _listofCheques[15].ProductCode)
                                    checkType.Reca_Commercial_Provincial.Add(_check);
                            }

                            //else if (_check.ChkType == _listofCheques[i].ChkType && _check.Location == "Direct" && _check.ProductCode == _listofCheques[i].ProductCode)
                            //{
                            //    checkType.Regular_Commercial_Direct.Add(_check);
                            //}
                            //else if (_check.ChkType == _listofCheques[i].ChkType && _check.Location == "Provincial" && _check.ProductCode == _listofCheques[i].ProductCode)
                            //{
                            //    checkType.Regular_Commercial_Provincial.Add(_check);
                            //}
                            ////else if (_check.ChkType == "C" && _check.Location == "Direct" && _check.ProductCode == _listofCheques[i].ProductCode)
                            ////{
                            ////    checkType.ManagersCheck_Direct.Add(_check);
                            ////}
                            ////else if (_check.ChkType == "C" && _check.Location == "Provincial" && _check.ProductCode == _listofCheques[i].ProductCode)
                            ////{
                            ////    checkType.ManagersCheck_Provincial.Add(_check);
                            ////}
                            //else if (_check.ChkType == _listofCheques[i].ChkType && _check.Location == "Provincial" && _check.ProductCode == _listofCheques[i].ProductCode)
                            //{
                            //    checkType.DragonBlue_Personal_Provincial.Add(_check);
                            //}
                            //else if (_check.ChkType == _listofCheques[i].ChkType && _check.Location == "Direct" && _check.ProductCode == _listofCheques[i].ProductCode)
                            //{
                            //    checkType.DragonBlue_Personal_Direct.Add(_check);
                            //}
                            //else if (_check.ChkType == _listofCheques[i].ChkType && _check.Location == "Direct" && _check.ProductCode == _listofCheques[i].ProductCode)
                            //{
                            //    checkType.DragonBlue_Commercial_Direct.Add(_check);
                            //}
                            //else if (_check.ChkType == _listofCheques[i].ChkType && _check.Location == "Provincial" && _check.ProductCode == _listofCheques[i].ProductCode)
                            //{
                            //    checkType.DragonBlue_Commercial_Provicial.Add(_check);
                            //}
                            //else if (_check.ChkType == _listofCheques[i].ChkType && _check.Location == "Direct" && _check.ProductCode == _listofCheques[i].ProductCode)
                            //{
                            //    checkType.DragonYellow_Personal_Direct.Add(_check);
                            //}
                            //else if (_check.ChkType == _listofCheques[i].ChkType && _check.Location == "Provincial" && _check.ProductCode == _listofCheques[i].ProductCode)
                            //{
                            //    checkType.DragonYellow_Personal_Provincial.Add(_check);
                            //}
                            //else if (_check.ChkType == _listofCheques[i].ChkType && _check.Location == "Direct" && _check.ProductCode == _listofCheques[i].ProductCode)
                            //{
                            //    checkType.DragonYellow_Commercial_Direct.Add(_check);
                            //}
                            //else if (_check.ChkType == _listofCheques[i].ChkType && _check.Location == "Provincial" && _check.ProductCode == _listofCheques[i].ProductCode)
                            //{
                            //    checkType.DragonYellow_Commercial_Provincial.Add(_check);
                            //}
                            //else if (_check.ChkType == _listofCheques[i].ChkType && _check.Location == "Direct" && _check.ProductCode == _listofCheques[i].ProductCode)
                            //{
                            //    checkType.Online_Commercial_Direct.Add(_check);
                            //}
                            //else if (_check.ChkType == _listofCheques[i].ChkType && _check.Location == "Provincial" && _check.ProductCode == _listofCheques[i].ProductCode)
                            //{
                            //    checkType.Online_Commercial_Provincial.Add(_check);
                            //}
                            //else if (_check.ChkType == _listofCheques[i].ChkType && _check.Location == "Direct" && _check.ProductCode == _listofCheques[i].ProductCode)
                            //{
                            //    checkType.Reca_Commercial_Direct.Add(_check);
                            //}
                            //else if (_check.ChkType == _listofCheques[i].ChkType && _check.Location == "Provincial" && _check.ProductCode == _listofCheques[i].ProductCode)
                            //{
                            //    checkType.Reca_Commercial_Provincial.Add(_check);
                            //}

                        }



                        //    else if (_check.ChkType == _listofCheques[2].ChkType && _check.Location == "Direct" && _check.ProductCode == _listofCheques[2].ProductCode)
                        //{
                        //    checkType.Regular_Commercial_Direct.Add(_check);
                        //}
                        //else if (_check.ChkType == _listofCheques[3].ChkType && _check.Location == "Provincial" && _check.ProductCode == _listofCheques[3].ProductCode)
                        //{
                        //    checkType.Regular_Commercial_Provincial.Add(_check);
                        //}
                        ////else if (_check.ChkType == "C" && _check.Location == "Direct" && _check.ProductCode == _listofCheques[i].ProductCode)
                        ////{
                        ////    checkType.ManagersCheck_Direct.Add(_check);
                        ////}
                        ////else if (_check.ChkType == "C" && _check.Location == "Provincial" && _check.ProductCode == _listofCheques[i].ProductCode)
                        ////{
                        ////    checkType.ManagersCheck_Provincial.Add(_check);
                        ////}
                        //else if (_check.ChkType == _listofCheques[4].ChkType && _check.Location == "Provincial" && _check.ProductCode == _listofCheques[4].ProductCode)
                        //{
                        //    checkType.DragonBlue_Personal_Provincial.Add(_check);
                        //}
                        //else if (_check.ChkType == _listofCheques[5].ChkType && _check.Location == "Direct" && _check.ProductCode == _listofCheques[5].ProductCode)
                        //{
                        //    checkType.DragonBlue_Personal_Direct.Add(_check);
                        //}
                        //else if (_check.ChkType == _listofCheques[6].ChkType && _check.Location == "Direct" && _check.ProductCode == _listofCheques[6].ProductCode)
                        //{
                        //    checkType.DragonBlue_Commercial_Direct.Add(_check);
                        //}
                        //else if (_check.ChkType == _listofCheques[7].ChkType && _check.Location == "Provincial" && _check.ProductCode == _listofCheques[7].ProductCode)
                        //{
                        //    checkType.DragonBlue_Commercial_Provicial.Add(_check);
                        //}
                        //else if (_check.ChkType == _listofCheques[8].ChkType && _check.Location == "Direct" && _check.ProductCode == _listofCheques[8].ProductCode)
                        //{
                        //    checkType.DragonYellow_Personal_Direct.Add(_check);
                        //}
                        //else if (_check.ChkType == _listofCheques[9].ChkType && _check.Location == "Provincial" && _check.ProductCode == _listofCheques[9].ProductCode)
                        //{
                        //    checkType.DragonYellow_Personal_Provincial.Add(_check);
                        //}
                        //else if (_check.ChkType == _listofCheques[10].ChkType && _check.Location == "Direct" && _check.ProductCode == _listofCheques[10].ProductCode)
                        //{
                        //    checkType.DragonYellow_Commercial_Direct.Add(_check);
                        //}
                        //else if (_check.ChkType == _listofCheques[11].ChkType && _check.Location == "Provincial" && _check.ProductCode == _listofCheques[11].ProductCode)
                        //{
                        //    checkType.DragonYellow_Commercial_Provincial.Add(_check);
                        //}
                        //else if (_check.ChkType == _listofCheques[12].ChkType && _check.Location == "Direct" && _check.ProductCode == _listofCheques[12].ProductCode)
                        //{
                        //    checkType.Online_Commercial_Direct.Add(_check);
                        //}
                        //else if (_check.ChkType == _listofCheques[13].ChkType && _check.Location == "Provincial" && _check.ProductCode == _listofCheques[13].ProductCode)
                        //{
                        //    checkType.Online_Commercial_Provincial.Add(_check);
                        //}
                        //else if (_check.ChkType == _listofCheques[14].ChkType && _check.Location == "Direct" && _check.ProductCode == _listofCheques[14].ProductCode)
                        //{
                        //    checkType.Reca_Commercial_Direct.Add(_check);
                        //}
                        //else if (_check.ChkType == _listofCheques[15].ChkType && _check.Location == "Provincial" && _check.ProductCode == _listofCheques[15].ProductCode)
                        //{
                        //    checkType.Reca_Commercial_Provincial.Add(_check);
                        //}


                        //else if (_check.ChkType == _listofCheques[i].ChkType && _check.Location == "Direct" && _check.ProductCode == _listofCheques[i].ProductCode)
                        //{
                        //    checkType.Online_Commercial_Direct.Add(_check);
                        //}
                        //else if (_check.ChkType == _listofCheques[i].ChkType && _check.Location == "Provincial" && _check.ProductCode == _listofCheques[i].ProductCode)
                        //{
                        //    checkType.Online_Commercial_Provincial.Add(_check);
                        //}
                        //else if (_check.ChkType == _listofCheques[i].ChkType && _check.Location == "Direct" && _check.ProductCode == _listofCheques[i].ProductCode)
                        //{
                        //    checkType.Online_Personal_Direct.Add(_check);
                        //}
                        //else if (_check.ChkType == _listofCheques[i].ChkType && _check.Location == "Provincial" && _check.ProductCode == _listofCheques[i].ProductCode)
                        //{
                        //    checkType.Online_Personal_Provincial.Add(_check);
                        //}
                        //else if (_check.ChkType == _listofCheques[i].ChkType && _check.Location == "Direct" && _check.ProductCode == _listofCheques[i].ProductCode)
                        //{
                        //    checkType.Customized_Direct.Add(_check);
                        //}
                        //else if (_check.ChkType == _listofCheques[i].ChkType && _check.Location == "Provincial" && _check.ProductCode == _listofCheques[i].ProductCode)
                        //{
                        //    checkType.Customized_Provincial.Add(_check);
                        //}
                        //if (_check.ChkType == r.ChkType && _check.Location == "Direct" && _check.ProductCode == r.ProductCode)
                        //{
                        //    checkType.Regular_Personal_Direct.Add(_check);
                        //}
                        //if (_check.ChkType == "A" && _check.Location == "Provincial" && _check.ProductCode == r.ProductCode)
                        //{
                        //    checkType.Regular_Personal_Provincial.Add(_check);
                        //}
                        //if (_check.ChkType == "B" && _check.Location == "Direct")
                        //{
                        //    checkType.Regular_Commercial_Direct.Add(_check);
                        //}
                        //if (_check.ChkType == "B" && _check.Location == "Provincial")
                        //{
                        //    checkType.Regular_Commercial_Provincial.Add(_check);
                        //}
                        //if (_check.ChkType == "C" && _check.Location == "Direct")
                        //{
                        //    checkType.ManagersCheck_Direct.Add(_check);
                        //}
                        //if (_check.ChkType == "C" && _check.Location == "Provincial")
                        //{
                        //    checkType.ManagersCheck_Provincial.Add(_check);
                        //}
                        //if (_check.ChkType == "C" && _check.Location == "Provincial")
                        //{
                        //    checkType.ManagersCheck_Provincial.Add(_check);
                        //}
                    }
//                    }
                }
                
                else
                {
                    //foreach (var t in type)
                    //{

                    log.Info("Separating Data by Checktype"); 
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
            checkType.Customized_Direct.OrderBy(r => r.BranchName).ToList();
            checkType.Customized_Provincial.OrderBy(r => r.BranchName).ToList();
            checkType.DragonBlue_Commercial_Direct.OrderBy(r => r.BranchName).ToList();
            checkType.DragonBlue_Commercial_Provicial.OrderBy(r => r.BranchName).ToList();
            checkType.DragonBlue_Personal_Direct.OrderBy(r => r.BranchName).ToList();
            checkType.DragonBlue_Personal_Provincial.OrderBy(r => r.BranchName).ToList();
            checkType.DragonYellow_Commercial_Direct.OrderBy(r => r.BranchName).ToList();
            checkType.DragonYellow_Commercial_Provincial.OrderBy(r => r.BranchName).ToList();
            checkType.DragonYellow_Personal_Direct.OrderBy(r => r.BranchName).ToList();
            checkType.DragonYellow_Personal_Provincial.OrderBy(r => r.BranchName).ToList();
            checkType.Reca_Commercial_Direct.OrderBy(r => r.BranchName).ToList();
            checkType.Reca_Commercial_Provincial.OrderBy(r => r.BranchName).ToList();
            checkType.Reca_Personal_Direct.OrderBy(r => r.BranchName).ToList();
            checkType.Reca_Personal_Provincial.OrderBy(r => r.BranchName).ToList();
            checkType.Online_Commercial_Direct.OrderBy(r => r.BranchName).ToList();
            checkType.Online_Commercial_Provincial.OrderBy(r => r.BranchName).ToList();
            checkType.Online_Personal_Direct.OrderBy(r => r.BranchName).ToList();
            checkType.Online_Personal_Provincial.OrderBy(r => r.BranchName).ToList();
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
            log.Info("Generating Data by Style of Report");
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
        //        else if(gClient.DataBaseName == "RCBC_history")
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
        //        else if (gClient.DataBaseName == "RCBC_history")
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
            log.Info("Getting Last Delivery Receipt Number from Database..");
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
                log.Info("Generating Delivery Report Details..");
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
                    log.Info("Inserting Data into Database Done..");
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
            log.Info("Getting Last Delivery Receipt Number from Database..");
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
                log.Info("Generating Sticker data..");
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
                    log.Info("Inserting data into Database done..");
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
        public List<TempModel> GetStickerDetailsForRCBC(List<TempModel> _temp, string _batch)
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

        //                if (gClient.DataBaseName != "RCBC_history")
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
        //                        reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\RCBCStickers2.rpt";
        //                    else if (RecentBatch.report == "PackingList" || DeliveryReport.report == "PackingList")
        //                        reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\RCBCPackingList.rpt";
        //                    else if (RecentBatch.report == "Packing" || DeliveryReport.report == "Packing")
        //                        reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\PackingReport.rpt";
        //                    else if (RecentBatch.report == "DOC" || DeliveryReport.report == "DOC")
        //                        reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\DocStamp.rpt";
                            
        //                    else if (RecentBatch.report == "DRR" || DeliveryReport.report == "DRR")
        //                        reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\RCBCDeliveryReport.rpt";
        //                    else if (RecentBatch.report == "DR" || DeliveryReport.report == "DR")
        //                        reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\RCBCDeliveryReceipt.rpt";

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
        //                else if (gClient.DataBaseName == "RCBC_history")
        //                {
        //                    if (RecentBatch.report == "STICKER" || DeliveryReport.report == "STICKER")
        //                        reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\RCBCStickers2.rpt";
        //                    else if (RecentBatch.report == "PackingList" || DeliveryReport.report == "PackingList")
        //                        reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\RCBCPackingList.rpt";
        //                    else if (RecentBatch.report == "Packing" || DeliveryReport.report == "Packing")
        //                        reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\PackingReport.rpt";
        //                    else if (RecentBatch.report == "DOC" || DeliveryReport.report == "DOC")
        //                        reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\DocStamp.rpt";
                           
        //                    else if (RecentBatch.report == "DRR" || DeliveryReport.report == "DRR")
        //                        reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\RCBCDeliveryReport.rpt";
        //                    else if (RecentBatch.report == "DR" || DeliveryReport.report == "DR")
        //                        reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\RCBCDeliveryReceipt.rpt";
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
                log.Info("Generating Report to Display..");

                if (Debugger.IsAttached)
                {
                    if (gClient.DataBaseName == "")
                        MessageBox.Show("There is no table selected!");
                    else
                    {

                        if (gClient.BankCode == "008")
                        {
                            if (RecentBatch.report == "STICKER" || DeliveryReport.report == "STICKER")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\RCBCStickers2.rpt";
                            else if (RecentBatch.report == "PackingList" || DeliveryReport.report == "PackingList")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\RCBCPackingList.rpt";
                            else if (RecentBatch.report == "Packing" || DeliveryReport.report == "Packing")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\PackingReport.rpt";
                            else if (RecentBatch.report == "DOC" || DeliveryReport.report == "DOC")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\DocStamp.rpt";

                            else if (RecentBatch.report == "DRR" || DeliveryReport.report == "DRR")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\RCBCDeliveryReport.rpt";
                            else if (RecentBatch.report == "DR" || DeliveryReport.report == "DR")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\RCBCDeliveryReceipt.rpt";

                        }
                        else if (gClient.BankCode == "028")
                        {
                            if (RecentBatch.report == "STICKER" || DeliveryReport.report == "STICKER")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\DeliverToStickers.rpt";
                            else if (RecentBatch.report == "Packing" || DeliveryReport.report == "Packing")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\RCBCPackingReport.rpt";
                            else if (RecentBatch.report == "DOC" || DeliveryReport.report == "DOC")
                                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\RCBCDocStamp.rpt";
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
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\RCBCStickers2.rpt";
                            else if (RecentBatch.report == "PackingList" || DeliveryReport.report == "PackingList")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\RCBCPackingList.rpt";
                            else if (RecentBatch.report == "Packing" || DeliveryReport.report == "Packing")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\PackingReport.rpt";
                            else if (RecentBatch.report == "DOC" || DeliveryReport.report == "DOC")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\" + gClient.ShortName + "DocStamp.rpt";

                            else if (RecentBatch.report == "DRR" || DeliveryReport.report == "DRR")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\RCBCDeliveryReport.rpt";
                            else if (RecentBatch.report == "DR" || DeliveryReport.report == "DR")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\RCBCDeliveryReceipt.rpt";


                        }
                        else if (gClient.BankCode == "028")
                        {
                            if (RecentBatch.report == "STICKER" || DeliveryReport.report == "STICKER")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\DeliverToStickers.rpt";
                            else if (RecentBatch.report == "Packing" || DeliveryReport.report == "Packing")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\PackingReport.rpt";
                            else if (RecentBatch.report == "DOC" || DeliveryReport.report == "DOC")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\" + gClient.ShortName+"DocStamp.rpt";
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
                            else if (RecentBatch.report == "DOC" || DeliveryReport.report == "DOC")
                                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\DocStamp.rpt";
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
                log.Info("Getting Data from database..");
                DBConnect();
                if (gClient.BankCode == "008")
                {
                    Sql = "select batch, chequename, ChkType, deliverydate, count(ChkType) as Quantity,SalesInvoice,DocStampNumber from  " + gClient.DataBaseName +
                          " where DrNumber is not null  and (Batch Like '%" + _batch + "%' OR SalesInvoice Like '%" + _batch + "%' OR DocStampNumber Like '%" + _batch + "%' )" +
                          " group by location,batch, chequename, ChkType";
                   
                }
                else
                {
                    Sql = "select batch, chequename, ChkType, deliverydate, count(ChkType) as Quantity,SalesInvoice,DocStampNumber from  " + gClient.DataBaseName +
                           " where DrNumber is not null  and (Batch Like '%" + _batch + "%' OR SalesInvoice Like '%" + _batch + "%' OR DocStampNumber Like '%" + _batch + "%' )" +
                           " and isCancelled = 0 group by batch, chequename, ChkType";
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
                log.Info("Getting Data from database..");
                DBConnect();
               
                    Sql = "select batch, chequename, ChkType, deliverydate, count(ChkType) as Quantity,SalesInvoice,DocStampNumber,Date from  " + gClient.DataBaseName +
                           " where DrNumber is not null  and (Batch Like '%" + _batch + "%' OR SalesInvoice Like '%" + _batch + "%' OR DocStampNumber Like '%" + _batch + "%' )" +
                           " and isCancelled = 0 group by batch order by Date desc";
                
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
                log.Info("Getting Data from database..");
                DBConnect();

                Sql = "select batch, chequename, ChkType, deliverydate, count(ChkType) as Quantity,SalesInvoice,DocStampNumber,Date," +
                        "DrNumber,PrimaryKey from  " + gClient.DataBaseName +
                        " where DrNumber is not null  and Batch = '" + _batch + "'" +
                        " and isCancelled = 0 group by batch,DrNumber,ChkType order by DrNumber,ChkType ";

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
            log.Info("Checking Batch if Existed ..");
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
                
                StreamWriter sw = new StreamWriter(Application.StartupPath + "\\ErrorMessage.txt",true);
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
            log.Info("Deleting Data from Database..");
            //for (int i = 0; i < _temp.Count; i++)
            //{
            //    Sql = "insert into " + gClient.CancelledTable + " select * from " + gClient.DataBaseName + " where DRNumber = " + _temp[i].DrNumber + ";";
            //    DBConnect();
            //    cmd = new MySqlCommand(Sql, myConnect);
            //    cmd.ExecuteNonQuery();
            //    Sql = "Delete from " + gClient.DataBaseName + " where DRNumber = " + _temp[i].DrNumber ;
            //    DBConnect();
            //    cmd = new MySqlCommand(Sql, myConnect);
            //    cmd.ExecuteNonQuery();
            //    DBClosed();
            //}
            for (int i = 0; i < _temp.Count; i++)
            {
                Sql = "Update " + gClient.DataBaseName + " set isCancelled = 1 where DRNumber = " + _temp[i].DrNumber + "";
                DBConnect();
                cmd = new MySqlCommand(Sql, myConnect);
                cmd.ExecuteNonQuery();

            }

            return;
        }
        public void UpdateItem(List<TempModel> _temp)
        {
            log.Info("Updating Data....");
            for (int i = 0; i < _temp.Count; i++)
            {


                Sql = "insert into " + gClient.UpdateTable + "(Batch,DRNumber,salesinvoice,DocstampNumber,DeliveryDate,AccountNo,ChkType,Brstn)" +
                    " Select Batch,DRNumber,Salesinvoice,DocstampNumber,DeliveryDate,AccountNo,ChkType,Brstn from " + gClient.DataBaseName+
                    " where DRNumber = " + _temp[i].DrNumber + ";";
                DBConnect();
                cmd = new MySqlCommand(Sql, myConnect);
                cmd.ExecuteNonQuery();
                DBClosed();
                Sql = "Update " + gClient.DataBaseName + " set DRNumber = '" + _temp[i].DrNumber + " ', AccountNo = '" + _temp[i].AccountNo + "' " +
                    " , salesinvoice = " + _temp[i].SalesInvoice + " , docstampnumber = " + _temp[i].DocStampNumber + ", Chktype = '" + _temp[i].ChkType + "', DeliveryDate = '" + _temp[i].DeliveryDate.ToString("yyyy-MM-dd") + "'" +
                    " where Primarykey = " + _temp[i].PrimaryKey;
                DBConnect();
                cmd = new MySqlCommand(Sql, myConnect);
                cmd.ExecuteNonQuery();
                DBClosed();
            }
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
            log.Info("Getting Data from database..");
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
        public PriceListModel GetPriceList(PriceListModel price, string chkType,string _cProductCode)
        {
            try
            {
                log.Info("Getting Price list data from database..");
                Sql = "Select BankCode, Description, Docstamp from " + gClient.PriceListTable + " where FinalChkType ='" + chkType + "' and ProductCode = '"+_cProductCode+"'; ";
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
                MessageBox.Show(error.Message, "Get Pricelist Data ", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return price;
            }
        }
        public void UpdateDocstamp(List<DocStampModel> _docStamp)
        {
            try
            {

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

                log.Info("Updating DocStamp in Database..");
            DBConnect();
            for (int i = 0; i < _docStamp.Count; i++)
            {
                    if (gClient.BankCode == "008")
                    {
                        Sql = "Update " + gClient.DataBaseName + " set DocStamp = " + _docStamp[i].DocStampPrice + ", DocStampNumber = " + _docStamp[i].DocStampNumber +
                            ", Date_DocStamp = '" + _docStamp[i].DocStampDate.ToString("yyyy-MM-dd") + "',Username_DocStamp ='" + _docStamp[i].PreparedBy +
                            "', CheckedByDS = '" + _docStamp[i].CheckedBy + "'  where SalesInvoice = " + _docStamp[i].SalesInvoiceNumber + " and ChkType = '" +
                            _docStamp[i].ChkType + "' and Location ='" + _docStamp[i].Location + "';";
                    }
                    else
                    {
                        Sql = "Update " + gClient.DataBaseName + " set DocStamp = " + _docStamp[i].DocStampPrice + ", DocStampNumber = " + _docStamp[i].DocStampNumber +
                            ", Date_DocStamp = '" + _docStamp[i].DocStampDate.ToString("yyyy-MM-dd") + "',Username_DocStamp ='" + _docStamp[i].PreparedBy +
                            "', CheckedByDS = '" + _docStamp[i].CheckedBy + "'  where SalesInvoice = " + _docStamp[i].SalesInvoiceNumber + " and ChkType = '" +
                            _docStamp[i].ChkType + "' and ChequeName = '" +_docStamp[i].ChequeName + "';";
                    }
                cmd = new MySqlCommand(Sql, myConnect);
                cmd.ExecuteNonQuery();
            }
                
                DBClosed();



            return;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Updating Docstamp ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        public string DisplayAllSalesInvoice(string _batch, List<TempModel> _temp)
        {
            log.Info("Getting Data from database..");
            DBConnect();
            if (gClient.BankCode == "008")
            {
                Sql = "Select SalesInvoiceDate,SalesInvoice, Count(ChkType) as Quantity,ChkType, ChequeName, Batch,location,ProductCode from  " + gClient.DataBaseName +
                            " where (DocStampNumber is null or DocStampNumber = 0) and SalesInvoice != 0  and (Batch Like '%" + _batch + "%' OR SalesInvoice Like '%" + _batch + "%') " +
                            "group by location,SalesInvoice, ChkType order by SalesInvoice, Batch,ChkType;";
            }
            else if (gClient.BankCode == "028")
            {
                Sql = "Select SalesInvoiceDate,SalesInvoice, Count(ChkType) as Quantity,ChkType, ChequeName, Batch,location,ProductCode from  " + gClient.DataBaseName +
                            " where (DocStampNumber is null or DocStampNumber = 0) and SalesInvoice != 0  and (Batch Like '%" + _batch + "%' OR SalesInvoice Like '%" + _batch + "%') " +
                            " and isCancelled = 0 group by SalesInvoice, ChequeName,ChkType order by SalesInvoice, Batch,ChkType;";
            }
            else
            {
                Sql = "Select SalesInvoiceDate,SalesInvoice, Count(ChkType) as Quantity,ChkType, ChequeName, Batch,location,ProductCode from  " + gClient.DataBaseName +
                    " where (DocStampNumber is null or DocStampNumber = 0 ) and SalesInvoice != 0  and (Batch Like '%" + _batch + "%' OR SalesInvoice Like '%" + _batch + "%') " +
                    " and isCancelled = 0 group by SalesInvoice, ChkType order by SalesInvoice, Batch,ChkType;";
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
                    Location = !reader.IsDBNull(6) ? reader.GetString(6):"",
                    ProductCode = !reader.IsDBNull(7) ? reader.GetString(7): ""

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
            log.Info("Generating Document Stamp Details..");
            try
            {


                //Orginal Query
                Sql = "Select P.BankCode, DocStampNumber,SalesInvoice,Count(ChkType) as Quantity,ChkType, P.Description, H.DocStamp, " +
                      "Username_DocStamp, CheckedByDS,PurchaseOrderNumber,P.QuantityOnHand,H.Batch," +
                      "(Count(ChkType) * H.DocStamp) as TotalAmount,H.location,SalesInvoiceDate from " + gClient.DataBaseName +
                      " H left join " + gClient.PriceListTable + "  P on H.ChkType = P.FinalChkType and H.ProductCode = P.ProductCode" +
                      " where  DocStampNumber= " + _docStampNumber + " Group by DocStampNumber,ChkType order by DocStampNumber, ChkType";
                //_docStampNumber.ForEach(x => { 
                //    Sql = "Select P.BankCode, DocStampNumber,SalesInvoice,Count(ChkType) as Quantity,ChkType, P.CDescription, H.DocStamp, " + //Based on RCBC requirements
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
                    doc.DocStampDate = !reader.IsDBNull(14) ? reader.GetDateTime(14) : DateTime.Now;

                    _temp.Add(doc);
                
                }
                reader.Close();
                DBClosed();


                string Sql3 = "Select Distinct(Batch) from " + gClient.DataBaseName + " where DocStampNumber = '"+_docStampNumber+"';";
                DBConnect();
                MySqlCommand cmd3 = new MySqlCommand(Sql3, myConnect);
                cmd3.ExecuteNonQuery();
                string batches = "";
                
                MySqlDataReader reader2 = cmd3.ExecuteReader();
              //  int counter = 0;
                while (reader2.Read())
                {
                    batches += !reader2.IsDBNull(0) ? reader2.GetString(0) : "";
                    if(reader2.HasRows)
                    {
                        batches += ",";
                    }
                    
                    

                    //counter++;
                }
                reader2.Close();
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
                                "PreparedBy, CheckedBy, PONumber,BalanceOrder,Batch,TotalAmount,Location,DocStampDate)Values('" + gClient.Description + "'," + d.DocStampNumber +
                                ", " + d.SalesInvoiceNumber + "," + d.TotalQuantity + ",'" + d.ChkType + "','" + d.DocDesc.Replace("'", "''") +
                                "'," + d.DocStampPrice + ",'" + d.PreparedBy + "','" + d.CheckedBy + "'," + d.POorder + "," + d.QuantityOnHand +
                                ",'" + batches.Substring(0,batches.Length -1) + "'," + d.TotalAmount + ",'" + d.Location + "','" + d.DocStampDate.ToString("yyyy-MM-dd") + "')";
                    MySqlCommand cmd2 = new MySqlCommand(Sql2, myConnect);
                    cmd2.ExecuteNonQuery();
                    log.Info("Inserting to Docstamp table Done..");
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
            log.Info("Getting User First Name from the Database..");
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
            log.Info("Getting Last Document Stamp from database..");
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
            log.Info("Getting Branch Details..");
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
            log.Info("Getting Branch Delivery Location by BRSTN");
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
            log.Info("Assigning Delivery Receipt Number..");
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
            log.Info("Assigning Delivery Receipt Number..");
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
            log.Info("Assigning Delivery Receipt Number..");
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
            log.Info("Assigning Delivery Receipt Number");
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
                //_DrNumber++;
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

            log.Info("Assigning Delivery Receipt Number..");
            bool isInsertData = false;
            DBConnect();
            int counter = 0;

            
            //_checks.Regular_Commercial_Direct.OrderBy(e => e.Location).ToList();
            //_checks.Regular_Commercial_Provincial.OrderBy(e => e.Location).ToList();
            //_checks.Regular_Personal_Provincial.OrderBy(e => e.Location).ToList();
            //DeliveryTo same as BRSTN
            if (_checks.Regular_Personal_Direct.Count > 0)
            {
                // without deliveryTO
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
                // END without deliveryTO
                // with deliveryTO
                var dBranch2 = _checks.Regular_Personal_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch2.ForEach(y =>
                {
                    var dRecord = _checks.Regular_Personal_Direct.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != g.BRSTN).ToList();
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


                    //isInsertData = false;

                });
                // END with deliveryTO
            }
            if (_checks.Regular_Commercial_Direct.Count > 0)
            {
                // generating DR number per Branches with ChkType B in Direct Branches
                //Without DeliveryTo
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
                //END Without DeliveryTo
                // with deliveryTO
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
                //END with deliveryTO

            }
            // ------------------------------------------------ End of Regular Checks Direct Branches --------------------------------------- //

            if (_checks.DragonBlue_Personal_Direct.Count > 0)
            {
                // without deliveryTO
                // generating DR number per Branches with ChkType A
                var dBranch = _checks.DragonBlue_Personal_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.DragonBlue_Personal_Direct.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() == g.BRSTN).ToList();
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
                // END without deliveryTO
                // with deliveryTO
                var dBranch2 = _checks.DragonBlue_Personal_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch2.ForEach(y =>
                {
                    var dRecord = _checks.DragonBlue_Personal_Direct.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != g.BRSTN).ToList();
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


                    //isInsertData = false;

                });
                // END with deliveryTO
            }
            if (_checks.DragonBlue_Commercial_Direct.Count > 0)
            {
                // generating DR number per Branches with ChkType B in Direct Branches
                //Without DeliveryTo
                var dBranch = _checks.DragonBlue_Commercial_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.DragonBlue_Commercial_Direct.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() == g.BRSTN).ToList();
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
                //END Without DeliveryTo
                // with deliveryTO
                // generating DR number per Branches with ChkType B in Direct Branches
                var dBranch2 = _checks.DragonBlue_Commercial_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch2.ForEach(y =>
                {
                    var dRecord = _checks.DragonBlue_Commercial_Direct.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != g.BRSTN).ToList();
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
                //END with deliveryTO

            }
            // ------------------------------------------------ End of Dragon Blue Checks Direct Branches --------------------------------------- //

            if (_checks.DragonYellow_Personal_Direct.Count > 0)
            {
                // without deliveryTO
                // generating DR number per Branches with ChkType A
                var dBranch = _checks.DragonYellow_Personal_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.DragonYellow_Personal_Direct.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() == g.BRSTN).ToList();
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
                // END without deliveryTO
                // with deliveryTO
                var dBranch2 = _checks.DragonYellow_Personal_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch2.ForEach(y =>
                {
                    var dRecord = _checks.DragonYellow_Personal_Direct.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != g.BRSTN).ToList();
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


                    //isInsertData = false;

                });
                // END with deliveryTO
            }
            if (_checks.DragonYellow_Commercial_Direct.Count > 0)
            {
                // generating DR number per Branches with ChkType B in Direct Branches
                //Without DeliveryTo
                var dBranch = _checks.DragonYellow_Commercial_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.DragonYellow_Commercial_Direct.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() == g.BRSTN).ToList();
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
                //END Without DeliveryTo
                // with deliveryTO
                // generating DR number per Branches with ChkType B in Direct Branches
                var dBranch2 = _checks.DragonYellow_Commercial_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch2.ForEach(y =>
                {
                    var dRecord = _checks.DragonYellow_Commercial_Direct.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != g.BRSTN).ToList();
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
                //END with deliveryTO

            }
            // ------------------------------------------------ End of Dragon Yellow Checks Direct Branches --------------------------------------- //

            if (_checks.Reca_Personal_Direct.Count > 0)
            {
                // without deliveryTO
                // generating DR number per Branches with ChkType A
                var dBranch = _checks.Reca_Personal_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Reca_Personal_Direct.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() == g.BRSTN).ToList();
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
                // END without deliveryTO
                // with deliveryTO
                var dBranch2 = _checks.Reca_Personal_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch2.ForEach(y =>
                {
                    var dRecord = _checks.Reca_Personal_Direct.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != g.BRSTN).ToList();
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


                    //isInsertData = false;

                });
                // END with deliveryTO
            }
            isInsertData = false;
            if (_checks.Reca_Commercial_Direct.Count > 0)
            {
                // generating DR number per Branches with ChkType B in Direct Branches
                //Without DeliveryTo
                var dBranch = _checks.Reca_Commercial_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Reca_Commercial_Direct.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() == g.BRSTN).ToList();
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
                //END Without DeliveryTo
                // with deliveryTO
                // generating DR number per Branches with ChkType B in Direct Branches
                isInsertData = false;
                var dBranch2 = _checks.Reca_Commercial_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch2.ForEach(y =>
                {
                    var dRecord = _checks.Reca_Commercial_Direct.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != g.BRSTN).ToList();
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
                //END with deliveryTO

            }

            // ------------------------------------------------ End of Reca Checks Direct Branches --------------------------------------- //

            if (_checks.Online_Personal_Direct.Count > 0)
            {
                // without deliveryTO
                // generating DR number per Branches with ChkType A
                var dBranch = _checks.Online_Personal_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Online_Personal_Direct.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() == g.BRSTN).ToList();
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
                // END without deliveryTO
                // with deliveryTO
                var dBranch2 = _checks.Online_Personal_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch2.ForEach(y =>
                {
                    var dRecord = _checks.Online_Personal_Direct.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != g.BRSTN).ToList();
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


                    //isInsertData = false;

                });
                // END with deliveryTO
            }
            if (_checks.Online_Commercial_Direct.Count > 0)
            {
                // generating DR number per Branches with ChkType B in Direct Branches
                //Without DeliveryTo
                var dBranch = _checks.Online_Commercial_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Online_Commercial_Direct.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() == g.BRSTN).ToList();
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
                //END Without DeliveryTo
                // with deliveryTO
                // generating DR number per Branches with ChkType B in Direct Branches
                var dBranch2 = _checks.Online_Commercial_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch2.ForEach(y =>
                {
                    var dRecord = _checks.Online_Commercial_Direct.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != g.BRSTN).ToList();
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
                //END with deliveryTO

            }
            // ------------------------------------------------ End of Online Checks Direct Branches --------------------------------------- //

            if (_checks.Customized_Direct.Count > 0)
            {
                //_DrNumber++;
                //Generating DR per CheckType in Provincial Branches
                //Without DeliveryTo
                var dBranch = _checks.Customized_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Customized_Direct.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() == g.BRSTN).ToList();
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
                //End Without DeliveryTo
                
             //   _DrNumber++;
                // with deliveryTO
                var dBranch2 = _checks.Customized_Direct.Select(a => a.BRSTN).Distinct().ToList();
                dBranch2.ForEach(y =>
                {
                    var dRecord = _checks.Customized_Direct.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != y).ToList();
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
                // end with deliveryTO
            }
            // ------------------------------------------------ End of Customized Checks Direct Branches --------------------------------------- //


            // ------------------------------------------------ Start of Process for Provincial Branches --------------------------------------- //
            
            if (_checks.Regular_Personal_Provincial.Count > 0)
            {

                //_DrNumber++;
                //Generating DR per CheckType in Provincial Branches
                var dBranch = _checks.Regular_Personal_Provincial.Select(a => a.BRSTN).Distinct().ToList();
                //Without DeliveryTo

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
                //END Without DeliveryTo
                // with deliveryTO
                isInsertData = true;
                dBranch.ForEach(y =>
                {

                    var dRecord = _checks.Regular_Personal_Provincial.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != g.BRSTN).ToList();
                    if (!isInsertData)
                    {
                        if (dRecord.Count > 0)
                        {
                            _packNumber++;

                            _DrNumber++;
                        }
                    }
                    
                    dRecord.ForEach(r =>
                    {
                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);
                        isInsertData = false;
                    });

                    isInsertData = true;
                });
                //ENd with deliveryTO

                //isInsertData = false;

            }
            //if (_checks.Regular_Personal_Provincial.Count > 0)
            //{
            //    var dBranch = _checks.Regular_Personal_Provincial.Select(a => a.BRSTN).Distinct().ToList();


            //}
            isInsertData = false;
            if (_checks.Regular_Commercial_Provincial.Count > 0)
            {
               // _DrNumber++;
                //Generating DR per CheckType in Provincial Branches
                //Without DeliveryTo
                //counter = 10;
                //isInsertData = true;
                //if (isInsertData)
                //{
                //   // counter++;
                // //   _packNumber++;
                //    if (counter == 10)
                //    {
                //        _DrNumber++;
                //        counter = 0;
                //    }
                //}
                var dBranch = _checks.Regular_Commercial_Provincial.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                //    isInsertData = false;
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
                //End Without DeliveryTo
                //Generating DR per CheckType in Provincial Branches
                //_DrNumber++;
                // with deliveryTO
                
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Regular_Commercial_Provincial.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != g.BRSTN).ToList();

                    if (!isInsertData)
                    {
                        if (dRecord.Count > 0)
                        {
                            _packNumber++;

                            _DrNumber++;
                        }

                    }
                    

                    dRecord.ForEach(r =>
                    {

                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);

                        isInsertData = false;
                    });

                    //if (isInsertData)
                    //{
                    //    _packNumber++;

                    //    _DrNumber++;
                    //}
                    isInsertData = true;
                });
                // end with deliveryTO
            }
            //if (_checks.Regular_Commercial_Provincial.Count  > 0)
            //{
            //    var dBranch = _checks.Regular_Commercial_Provincial.Select(a => a.BRSTN).Distinct().ToList();
            //    // isInsertData = true;

            //}
            //------------------------------------------ END of Regular Cheques Provincial Branches ---------------------------------//
            isInsertData = false;
            if (_checks.DragonBlue_Personal_Provincial.Count > 0)
            {
                 //_DrNumber++;
                //counter = 10;
                //isInsertData = true;
                //if (isInsertData)
                //{
                //    //counter++;
                //    //_packNumber++;
                //    if (counter == 10)
                //    {
                //        _DrNumber++;
                //        counter = 0;
                //    }
                //}
                //Generating DR per CheckType in Provincial Branches
                var dBranch = _checks.DragonBlue_Personal_Provincial.Select(a => a.BRSTN).Distinct().ToList();
                //Without DeliveryTo
                dBranch.ForEach(y =>
                {
                   
                //    isInsertData = false;
                    var dRecord = _checks.DragonBlue_Personal_Provincial.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() == g.BRSTN).ToList();
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
                //END Without DeliveryTo
                // with deliveryTO
                //_DrNumber++;
                dBranch.ForEach(y =>
                {
                    // isInsertData = false;
                    var dRecord = _checks.DragonBlue_Personal_Provincial.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != g.BRSTN).ToList();
                    if (!isInsertData)
                    {
                        if (dRecord.Count > 0)
                        {
                            _packNumber++;

                            _DrNumber++;
                        }

                    }
                    dRecord.ForEach(r =>
                    {
                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);
                        isInsertData = false;
                    });

                    //if (isInsertData)
                    //{
                    //    _packNumber++;

                    //    _DrNumber++;
                    //}
                    isInsertData = true;
                });
                //ENd with deliveryTO


            }

            isInsertData = false;
            if (_checks.DragonBlue_Commercial_Provicial.Count > 0)
            {
                //counter = 10;
                //isInsertData = true;
                //_DrNumber++;
                ////Generating DR per CheckType in Provincial Branches
                ////Without DeliveryTo
                //if (isInsertData)
                //{
                //    //counter++;
                // //   _packNumber++;
                //    if (counter == 10)
                //    {
                //        _DrNumber++;
                //        counter = 0;
                //    }
                //}

                var dBranch = _checks.DragonBlue_Commercial_Provicial.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.DragonBlue_Commercial_Provicial.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() == g.BRSTN).ToList();
                    //if (dRecord != null)
                    //    _DrNumber++;
                //    isInsertData = false;
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
                //_DrNumber++;
                //End Without DeliveryTo
                //Generating DR per CheckType in Provincial Branches
                //_DrNumber++;
                // with deliveryTO
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.DragonBlue_Commercial_Provicial.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != y).ToList();
                    if (!isInsertData)
                    {
                        if (dRecord.Count > 0)
                        {
                            _packNumber++;

                            _DrNumber++;
                        }

                    }
                    dRecord.ForEach(r =>
                    {

                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);

                        isInsertData = false;
                    });
                    //if (isInsertData)
                    //{
                    //    _packNumber++;

                    //    _DrNumber++;
                    //}
                    isInsertData = true;
                });
                // end with deliveryTO            
            }
            //if (_checks.DragonYellow_Commercial_Provincial.Count > 0)
            //{
            //    var dBranch = _checks.DragonBlue_Commercial_Provicial.Select(a => a.BRSTN).Distinct().ToList();
            //}
            //------------------------------------------ END of Dragon Blue Cheques Provincial Branches ---------------------------------//

            if (_checks.DragonYellow_Personal_Provincial.Count > 0)
            {
                //counter = 10;
                //isInsertData = true;
                //if (isInsertData)
                //{
                //    //counter++;
                //    //_packNumber++;
                //    if (counter == 10)
                //    {
                //        _DrNumber++;
                //        counter = 0;
                //    }
                //}

                //_DrNumber++;
                //Generating DR per CheckType in Provincial Branches
                var dBranch = _checks.DragonYellow_Personal_Provincial.Select(a => a.BRSTN).Distinct().ToList();
                //Without DeliveryTo
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.DragonYellow_Personal_Provincial.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() == g.BRSTN).ToList();
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
                //END Without DeliveryTo
                // with deliveryTO
                dBranch.ForEach(y =>
                {

                    var dRecord = _checks.DragonYellow_Personal_Provincial.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != g.BRSTN).ToList();
                    if (!isInsertData)
                    {
                        if (dRecord.Count > 0)
                        {
                            _packNumber++;

                            _DrNumber++;
                        }

                    }
                    dRecord.ForEach(r =>
                    {
                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);
                        isInsertData = false;
                    });

                    //if (isInsertData)
                    //{
                    //    _packNumber++;

                    //    _DrNumber++;
                    //}
                    isInsertData = true;
                });
                //ENd with deliveryTO



            }
           

            if (_checks.DragonYellow_Commercial_Provincial.Count > 0)
            {
                //counter = 10;
                //isInsertData = true;
                //if (isInsertData)
                //{
                ////    counter++;
                //  //  _packNumber++;
                //    if (counter == 10)
                //    {
                //        _DrNumber++;
                //        counter = 0;
                //    }
                //}

                _DrNumber++;
                //Generating DR per CheckType in Provincial Branches
                //Without DeliveryTo
                var dBranch = _checks.DragonYellow_Commercial_Provincial.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.DragonYellow_Commercial_Provincial.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() == g.BRSTN).ToList();
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
                //End Without DeliveryTo
                //Generating DR per CheckType in Provincial Branches
                //_DrNumber++;

                // with deliveryTO
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.DragonYellow_Commercial_Provincial.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != y).ToList();
                    if (!isInsertData)
                    {
                        if (dRecord.Count > 0)
                        {
                            _packNumber++;

                            _DrNumber++;
                        }

                    }
                    dRecord.ForEach(r =>
                    {

                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);

                        isInsertData = false;
                    });
                    //if (isInsertData)
                    //{
                    //    _packNumber++;

                    //    _DrNumber++;
                    //}
                    isInsertData = true;
                });
                // end with deliveryTO
            }
            
            //------------------------------------------ END of Dragon Yellow Cheques Provincial Branches ---------------------------------//

            if (_checks.Reca_Personal_Provincial.Count > 0)
            {
                //counter = 10;
                //isInsertData = true;
                //if (isInsertData)
                //{
                //    //    counter++;
                //    //  _packNumber++;
                //    if (counter == 10)
                //    {
                //        _DrNumber++;
                //        counter = 0;
                //    }
                //}
                  _DrNumber++;
                //Generating DR per CheckType in Provincial Branches
                var dBranch = _checks.Reca_Personal_Provincial.Select(a => a.BRSTN).Distinct().ToList();
                //Without DeliveryTo
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Reca_Personal_Provincial.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() == g.BRSTN).ToList();
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
                //END Without DeliveryTo
                // with deliveryTO
                //_DrNumber++;
                dBranch.ForEach(y =>
                {

                    var dRecord = _checks.Reca_Personal_Provincial.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != g.BRSTN).ToList();
                    if (!isInsertData)
                    {
                        if (dRecord.Count > 0)
                        {
                            _packNumber++;

                            _DrNumber++;
                        }

                    }
                    dRecord.ForEach(r =>
                    {
                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);
                        isInsertData = false;
                    });

                    //if (isInsertData)
                    //{
                    //    _packNumber++;

                    //    _DrNumber++;
                    //}
                    isInsertData = true;
                });
                //ENd with deliveryTO

            }
            
            if (_checks.Reca_Commercial_Provincial.Count > 0)
            {
                //counter = 10;
                //isInsertData = true;
                //if (isInsertData)
                //{
                //    //    counter++;
                //    //  _packNumber++;
                //    if (counter == 10)
                //    {
                //        _DrNumber++;
                //        counter = 0;
                //    }
                //}
                _DrNumber++;
                //Generating DR per CheckType in Provincial Branches
                //Without DeliveryTo
                var dBranch = _checks.Reca_Commercial_Provincial.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Reca_Commercial_Provincial.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() == g.BRSTN).ToList();
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
                //End Without DeliveryTo
                //Generating DR per CheckType in Provincial Branches
             //   _DrNumber++;
                // with deliveryTO
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Reca_Commercial_Provincial.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != y).ToList();
                    if (!isInsertData)
                    {
                        if (dRecord.Count > 0)
                        {
                            _packNumber++;

                            _DrNumber++;
                        }

                    }
                    dRecord.ForEach(r =>
                    {

                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);

                        isInsertData = false;
                    });
                    //if (isInsertData)
                    //{
                    //    _packNumber++;

                    //    _DrNumber++;
                    //}
                    isInsertData = true;
                });
                // end with deliveryTO
            }
            
            //------------------------------------------ END of Reca Cheques Provincial Branches ---------------------------------//

            if (_checks.Online_Personal_Provincial.Count > 0)
            {
                //counter = 10;
                //isInsertData = true;
                //if (isInsertData)
                //{
                //    //    counter++;
                //    //  _packNumber++;
                //    if (counter == 10)
                //    {
                //        _DrNumber++;
                //        counter = 0;
                //    }
                //}
                  _DrNumber++;
                //Generating DR per CheckType in Provincial Branches
                var dBranch = _checks.Online_Personal_Provincial.Select(a => a.BRSTN).Distinct().ToList();
                //Without DeliveryTo
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Online_Personal_Provincial.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() == g.BRSTN).ToList();
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
                //END Without DeliveryTo
                // with deliveryTO
                //_DrNumber++;
                dBranch.ForEach(y =>
                {

                    var dRecord = _checks.Online_Personal_Provincial.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != g.BRSTN).ToList();
                    if (!isInsertData)
                    {
                        if (dRecord.Count > 0)
                        {
                            _packNumber++;

                            _DrNumber++;
                        }

                    }
                    dRecord.ForEach(r =>
                    {
                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);
                        isInsertData = false;
                    });

                    //if (isInsertData)
                    //{
                    //    _packNumber++;

                    //    _DrNumber++;
                    //}
                    isInsertData = true;
                });
                //ENd with deliveryTO

            }
            
            if (_checks.Online_Commercial_Provincial.Count > 0)
            {
                //counter = 10;
                //isInsertData = true;
                //if (isInsertData)
                //{
                //    //    counter++;
                //    //  _packNumber++;
                //    if (counter == 10)
                //    {
                //        _DrNumber++;
                //        counter = 0;
                //    }
                //}
                _DrNumber++;
                //Generating DR per CheckType in Provincial Branches
                //Without DeliveryTo
                var dBranch = _checks.Online_Commercial_Provincial.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Online_Commercial_Provincial.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() == g.BRSTN).ToList();
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
                //End Without DeliveryTo
                //Generating DR per CheckType in Provincial Branches
                //     _DrNumber++;
                // with deliveryTO
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Online_Commercial_Provincial.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != y).ToList();
                    if (!isInsertData)
                    {
                        if (dRecord.Count > 0)
                        {
                            _packNumber++;

                            _DrNumber++;
                        }

                    }
                    dRecord.ForEach(r =>
                    {

                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);

                        isInsertData = false;
                    });
                    //if (isInsertData)
                    //{
                    //    _packNumber++;

                    //    _DrNumber++;
                    //}
                    isInsertData = true;
                });
                // end with deliveryTO
            }
            
            //------------------------------------------ END of Online Cheques Provincial Branches ---------------------------------//
            if (_checks.Customized_Provincial.Count > 0)
            {
                counter = 10;
                isInsertData = true;
                if (isInsertData)
                {
                    //    counter++;
                    //  _packNumber++;
                    if (counter == 10)
                    {
                        _DrNumber++;
                        counter = 0;
                    }
                }
                //_DrNumber++;
                //Generating DR per CheckType in Provincial Branches
                //Without DeliveryTo
                var dBranch = _checks.Customized_Provincial.Select(a => a.BRSTN).Distinct().ToList();
                dBranch.ForEach(y =>
                {
                    var dRecord = _checks.Customized_Provincial.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() == g.BRSTN).ToList();
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
                //End Without DeliveryTo
                //Generating DR per CheckType in Provincial Branches
            //    _DrNumber++;
                // with deliveryTO
                var dBranch2 = _checks.Customized_Provincial.Select(a => a.BRSTN).Distinct().ToList();
                dBranch2.ForEach(y =>
                {
                    var dRecord = _checks.Customized_Provincial.Where(g => g.BRSTN == y && g.DeliveryTo.Trim() != y).ToList();
                    if (!isInsertData)
                    {
                        if (dRecord.Count > 0)
                        {
                            _packNumber++;

                            _DrNumber++;
                        }
                    }
                    dRecord.ForEach(r =>
                    {

                        Script(gClient.DataBaseName, r, _DrNumber, _deliveryDate, _username, _packNumber);

                        isInsertData = true;
                    });
    
                    isInsertData = false;
                });
                // end with deliveryTO
            }
            //------------------------------------------ END of Customized Cheques Provincial Branches ---------------------------------//

            DBClosed();
            return;
        }
        public void ByLocation(TypeofCheckModel _checks, int _DrNumber, DateTime _deliveryDate, string _username, int _packNumber)
        {
            log.Info("Assigning Delivery Receipt Number..");
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
            log.Info("Getting Purchase Order Number from Database per Cheque Name..");
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
            log.Info("Searchig Purchase order Number in Database..");
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
        public int GetPONUmberforSearchingforRCBC(string _chkType)
        {
            log.Info("Searchig Purchase order Number in Database..");
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
            log.Info("Inserting Data into Database...");
            if (gClient.BankCode == "008")
            {
                Sql = "Insert into " +_table+ " (BRSTN,BranchName,AccountNo,AcctNoWithHyphen,Name1,Name2,ChkType," +
                          "ChequeName,StartingSerial,EndingSerial,DRNumber,DeliveryDate,username,batch,PackNumber,Date,Time,location, BranchCode,OldBranchCode,PurchaseOrderNumber,Bank" +
                          ",Address2,Address3,Address4,Address5,Address6,ProductCode,Block,Segment,ProductType,AttentionTo)" +
                          "VALUES('" + r.BRSTN + "','" + r.BranchName.Replace("'", "''") + "','" + r.AccountNo + "','" + r.AccountNoWithHypen + "','" + r.Name1.Replace("'", "''") +
                          "','" + r.Name2.Replace("'", "''") + "','" + r.ChkType + "','" + r.ChequeName.Replace("'","''") + "','" + r.StartingSerial + "','" + r.EndingSerial +
                          "','" + _DrNumber + "','" + _deliveryDate.ToString("yyyy-MM-dd") + "','" + _username + "','" +
                          r.Batch.TrimEnd() + "','" + _packNumber + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("hh:mm:ss") +
                          "','" + r.Location + "','" + r.BranchCode + "','" + r.OldBranchCode + "',"+r.PONumber+",'" + gClient.ShortName + "'," +
                          "'" + r.Address2.Replace("'","''") + "','" + r.Address3.Replace("'", "''") + "','" + r.Address4.Replace("'", "''") + "','" + r.Address5.Replace("'", "''") +
                          "','" + r.Address6.Replace("'", "''") + "','" + r.ProductCode +
                          "'," + r.Block  + "," + r.Segment+",'" + r.ProductType +"','" + gClient.AttentionTo + "');";
                cmd = new MySqlCommand(Sql, myConnect);
                cmd.ExecuteNonQuery();
                log.Info("Inserting data in Database done..");
            }
            else
            {
                Sql = "Insert into " + _table + " (BRSTN,BranchName,AccountNo,AcctNoWithHyphen,Name1,Name2,ChkType," +
                          "ChequeName,StartingSerial,EndingSerial,DRNumber,DeliveryDate,username,batch,PackNumber,Date,Time,location,Bank,ProductCode," +
                          "BranchCode,DeliveryToBrstn,DeliveryToBranch,AttentionTo,OldBranchCode,PurchaseOrderNumber,isCancelled)VALUES('" + r.BRSTN + "','" + r.BranchName + "','" + r.AccountNo + 
                          "','" + r.AccountNoWithHypen + "','" + r.Name1.Replace("'", "''") +
                          "','" + r.Name2.Replace("'", "''") + "','" + r.ChkType + "','" + r.ChequeName + "','" + r.StartingSerial + "','" + r.EndingSerial +
                          "','" + _DrNumber + "','" + _deliveryDate.ToString("yyyy-MM-dd") + "','" + _username + "','" +
                          r.Batch.TrimEnd() + "','" + _packNumber + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("hh:mm:ss") +
                          "','" + r.Location + "','" + gClient.ShortName + "','" + r.ProductCode + "','"+ r.BranchCode + "','" + r.DeliveryTo + "','" + r.DeliverytoBranch +
                          "','" + gClient.AttentionTo + "','" + r.OldBranchCode + "'," + r.PONumber + ",0);";
                cmd = new MySqlCommand(Sql, myConnect);
                cmd.ExecuteNonQuery();
                log.Info("Inserting data in Database done..");
            }
        }
        public int CheckPOQuantity(int _PO, string _chkType )
        {
            log.Info("Checking Balance Quantity of Purchage Order Number per Cheque Type..");
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
                log.Info("Concatination of Delivery Receipt Number..");
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
            log.Info("Disable/Enable Controls..");
            //if (gClient.DataBaseName != "producers_history")
            //{
                _item.Enabled = true;

            //}
            //else
            //{
            //    _item.Enabled = false;
            //}
        }
        public void GetProducts(List<ProductModel> _products)
        {
            log.Info("Getting List of Products from Database..");
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
            log.Info("Inserting Data in Product Table..");
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
            log.Info("Updating Product Price ..");
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
            log.Info("Getting List of Cheque Types..");
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
        public List<ChequeTypesModel> GetChequeTypesOrdering(List<ChequeTypesModel> _cheques)
        {
            log.Info("Getting List of Cheque Types..");
            Sql = "Select Type, ChequeName, Description, A.DateTimeModified,ProductName,BookStyle  from " + gClient.ChequeTypeTable + " A " +
            "inner join " + gClient.ProductTable + " B on A.CProductCode = B.CProductCode ;";
            //Sql = "Select Type, ChequeName, Description, A.DateTimeModified,ProductName,BookStyle  from rcbc_tcheques A " +
            //    "inner join rcbc_tchequeproducts B on A.CProductCode = B.CProductCode ;";
            DBConnect();
            //con = new MySqlConnection(ConString);
            cmd = new MySqlCommand(Sql, myConnect);
          //  con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ChequeTypesModel c = new ChequeTypesModel
                {
                    Type = !reader.IsDBNull(0) ? reader.GetString(0) : "",
                    ChequeName = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                    Description = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                    DateModified = !reader.IsDBNull(3) ? reader.GetDateTime(3) : DateTime.Now,
                    ProductName = !reader.IsDBNull(4) ? reader.GetString(4) : "",
                    BookStyle = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0
                };
                _cheques.Add(c);
            }
            reader.Close();
          //  con.Close();
            DBClosed();
            return _cheques;
        }
        public void AddChequeType(ChequeTypesModel _cheque)
        {
            log.Info("Inserting Data to ChequeType Table..");
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
            log.Info("Updating Data in ChequeType Table..");
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
            log.Info("Getting List of Products..");
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
            log.Info("Inserting Data to Products Table..");
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
            log.Info("Updating Data from Product Table..");
            Sql = "Update " + gClient.ProductTable + " set  ProductName = '" + _cheque.ProductName.Replace("'", "''") 
                 + "' ,DatetimeModified = '" + _cheque.DateModified.ToString("yyyy-MM-dd hh:mm:ss") + "' where CProductCode = '"+_cheque.ProductCode+"'";
            DBConnect();
            cmd = new MySqlCommand(Sql, myConnect);
            cmd.ExecuteNonQuery();
            DBClosed();
        }
        public List<ChequeTypesModel> fChequeTypes(List<ChequeTypesModel> _cheques)
        {
            log.Info("Getting List of Cheque Types..");
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
            log.Info("Getting Last Product Code from database..");
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
            log.Info("Getting Cheque Name with Product Code from Database..");
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
                log.Info("Getting Sticker Data from Database..");
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
            log.Info("Getting Data from  Ordering database..");
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
            log.Info(" Data from database..");
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
                log.Info("Getting Direct Branches data from Database..");
                DBConnect();
                Sql = "SELECT DRNumber, PackNumber, BRSTN, ChkType, BranchName, COUNT(BRSTN)," +
                     "MIN(StartingSerial), MAX(EndingSerial),ChequeName, Batch,username,BranchCode,OldBranchCode,location,PurchaseOrderNumber,Bank,Address2,Address3,Address4," +
                     "Name1,Name2,AccountNo,DeliveryToBrstn,DeliveryToBranch,AttentionTo FROM " +
                     gClient.DataBaseName + " WHERE  Batch = '" + _batch.TrimEnd() + "' and Location = 'Direct' and isCancelled = 0 GROUP BY DRNumber, BRSTN, ChkType, BranchName," +
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
                                  "','" + list[i].BRSTN + "','" + list[i].ChkType + "','" + list[i].BranchName.TrimEnd()  + " (" + list[i].BranchCode + ")'," + list[i].Qty +
                                  ",'" + list[i].StartingSerial + "','" + list[i].EndingSerial + "','" + list[i].ChequeName.Replace("'", "''") + "','" +
                                  list[i].Batch + "','" + list[i].username + "','" + list[i].BranchCode + "','" + list[i].OldBranchCode + "','" +
                                  list[i].Location + "'," + list[i].PONumber + ",'','" + gClient.Description.ToUpper() + "','" + list[i].Address2.Replace("'", "''") +
                                  "','" + list[i].Address3.Replace("'", "''") + "','" + list[i].Address4.Replace("'", "''") + "','" + list[i].Name1.Replace("'", "''") +
                                  "','" + list[i].Name2.Replace("'", "''") + "','" + list[i].AccountNo + "','" + TotalA.Count() + "','" + TotalB.Count() +
                                  "','" + gClient.Description.ToUpper().Replace("'", "''").TrimEnd() + "','" + list[i].AttentionTo.Replace("'", "''").TrimEnd() +
                                  "','" + gClient.TIN + "','" + list[i].DeliveryToBranch.TrimEnd() + " (" + list[i].OldBranchCode + ")','" + list[i].DeliveryToBrstn + "');";
                    MySqlCommand cmd2 = new MySqlCommand(sql2, myConnect);
                    cmd2.ExecuteNonQuery();
                    log.Info("Inserting Data to Database Done..");
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
                log.Info("Getting Provincial Branches data from Database..");
                DBConnect();
                Sql = "SELECT DRNumber, PackNumber, BRSTN, ChkType, BranchName, COUNT(BRSTN)," +
                     "MIN(StartingSerial), MAX(EndingSerial),ChequeName, Batch,username,BranchCode,OldBranchCode,location,PurchaseOrderNumber,Bank,Address2,Address3,Address4," +
                     "Name1,Name2,AccountNo,DeliveryToBrstn,DeliveryToBranch,AttentionTo FROM " +
                     gClient.DataBaseName + " WHERE  Batch = '" + _batch.TrimEnd() + "' and Location = 'Provincial' and isCancelled = 0  GROUP BY DRNumber, BRSTN, ChkType, BranchName," +
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
                                  "','" + list[i].BRSTN + "','" + list[i].ChkType + "','" + list[i].BranchName.TrimEnd() + " (" + list[i].BranchCode + ")'," + list[i].Qty +
                                  ",'" + list[i].StartingSerial + "','" + list[i].EndingSerial + "','" + list[i].ChequeName.Replace("'", "''") + "','" +
                                  list[i].Batch + "','" + list[i].username + "','" + list[i].BranchCode + "','" + list[i].OldBranchCode + "','" +
                                  list[i].Location + "'," + list[i].PONumber + ",'','" + gClient.Description.ToUpper() + "','" + list[i].Address2.Replace("'", "''") +
                                  "','" + list[i].Address3.Replace("'", "''") + "','" + list[i].Address4.Replace("'", "''") + "','" + list[i].Name1.Replace("'", "''") +
                                  "','" + list[i].Name2.Replace("'", "''") + "','" + list[i].AccountNo + "','" + TotalA.Count() + "','" + TotalB.Count() +
                                  "','" + gClient.Description.ToUpper().Replace("'", "''").TrimEnd() + "','" + list[i].AttentionTo.Replace("'", "''").TrimEnd() +
                                  "','" + gClient.TIN + "','" + list[i].DeliveryToBranch.TrimEnd() + " (" + list[i].OldBranchCode + ")','" + list[i].DeliveryToBrstn + "');";
                    MySqlCommand cmd2 = new MySqlCommand(sql2, myConnect);
                    cmd2.ExecuteNonQuery();
                    log.Info("Inserting Data into Database done..");
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
        public List<TempModel> GetStickerDetailsWithDeliveryTo(List<TempModel> _temp, string _batch)
        {
            try
            {
                log.Info("Generating Sticker data..");
                Sql = "SELECT BranchName, BRSTN, ChkType,MIN(StartingSerial), MAX(EndingSerial), Count(ChkType), Bank,Address2,Address3,Address4,Block,Segment,ProductType, " +
                      "DeliveryToBranch,ChequeName FROM " + gClient.DataBaseName + "  WHERE Batch = '" + _batch + "'" +
                       " and isCancelled = 0 GROUP BY ChkType,ChequeName,BranchName,DeliveryToBranch ORDER BY ChkType,BranchName";
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
                        ProducType = !myReader.IsDBNull(12) ? myReader.GetString(12) : "",
                        DeliveryToBranch = !myReader.IsDBNull(13) ? myReader.GetString(13) : "",
                        ChequeName = !myReader.IsDBNull(14) ? myReader.GetString(14): ""


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
               //s string Type = "";
                int licnt = 1;

                for (int r = 0; r < _temp.Count; r++)
                {
                    //if (_temp[r].ChkType == "A")
                    //    Type = "Personal";
                    //else if (_temp[r].ChkType == "B")
                    //    Type = "Commercial";
                    //else if (_temp[r].ChkType == "C")
                    //    Type = "MC";
                    if (licnt == 1)
                    {
                        string sql2 = "Insert into " + gClient.StickerTable + "(Batch,BRSTN,BranchName,Qty,ChkType,ChequeName,StartingSerial,EndingSerial," +
                                      "Bank,Address2,Address3,Address4,Block,Segment,ProductType,DeliveryToBranch)" +
                                      "values('" + _batch + "','" + _temp[r].BRSTN + "','" + _temp[r].BranchName + "'," + _temp[r].Qty + ",'" + _temp[r].ChkType +
                                      "','" + _temp[r].ChequeName.Substring(0,_temp[r].ChequeName.Length - 6) +   "','" + _temp[r].StartingSerial + "','" + _temp[r].EndingSerial + "','" + gClient.Description.ToUpper().Replace("'", "''")
                                      + "','" + _temp[r].Address2.Replace("'", "''") +
                                      "', '" + _temp[r].Address3.Replace("'", "''") + "','" + _temp[r].Address4.Replace("'", "''") + "'," + _temp[r].Block + "," +
                                      _temp[r].Segment + ",'" + _temp[r].ProducType + "','" + _temp[r].DeliveryToBranch + "' ); ";


                        MySqlCommand cmd2 = new MySqlCommand(sql2, myConnect);
                        cmd2.ExecuteNonQuery();
                        licnt++;
                    }
                    else if (licnt == 2)
                    {
                        string sql2 = "Update " + gClient.StickerTable + " set BRSTN2 = '" + _temp[r].BRSTN + "',BranchName2 = '" + _temp[r].BranchName.Replace("'", "''") +
                                    "',Qty2 = " + _temp[r].Qty + ",ChkType2 = '" + _temp[r].ChkType + "', ChequeName2 = '"+_temp[r].ChequeName.Substring(0, _temp[r].ChequeName.Length - 6) +  
                                    "',StartingSerial2 = '" + _temp[r].StartingSerial +
                                      "',EndingSerial2 = '" + _temp[r].EndingSerial + "',Address22 ='" + _temp[r].Address2.Replace("'", "''") +
                                      "', Address32 ='" + _temp[r].Address3.Replace("'", "''") + "',Address42 ='" + _temp[r].Address4.Replace("'", "''") +
                                      "', Block2 = " + _temp[r].Block + ", Segment2 = " + _temp[r].Segment + ",ProductType2 = '" + _temp[r].ProducType + "' " +
                                      ", DeliveryToBranch1 = '" + _temp[r].DeliveryToBranch + "' " +
                                      "where BRSTN = '" + _temp[r - 1].BRSTN + "' and ChkType = '" + _temp[r - 1].ChkType + "';";

                        MySqlCommand cmd2 = new MySqlCommand(sql2, myConnect);
                        cmd2.ExecuteNonQuery();
                        licnt++;
                    }
                    else if (licnt == 3)
                    {
                        string sql2 = "Update " + gClient.StickerTable + " set BRSTN3 = '" + _temp[r].BRSTN + "',BranchName3 = '" + _temp[r].BranchName + "',Qty3 = " + _temp[r].Qty +
                                      ",ChkType3 = '" + _temp[r].ChkType + "',ChequeName3 = '"+ _temp[r].ChequeName.Substring(0, _temp[r].ChequeName.Length - 6) + "',StartingSerial3 = '" + _temp[r].StartingSerial +
                                      "',EndingSerial3 = '" + _temp[r].EndingSerial + "',Address23  = '" + _temp[r].Address2.Replace("'", "''") + "'" +
                                      ",Address33 = '" + _temp[r].Address3.Replace("'", "''") + "',Address43 = '" + _temp[r].Address4.Replace("'", "''") +
                                      "',Block3 = " + _temp[r].Block + ", Segment3 = " + _temp[r].Segment + ",ProductType3 = '" + _temp[r].ProducType +
                                      "',DeliveryToBranch2 = '" + _temp[r].DeliveryToBranch + "'" +
                                      "  where BRSTN2 = '" + _temp[r - 1].BRSTN + "' and ChkType2 = '" + _temp[r - 1].ChkType + "';";
                        MySqlCommand cmd2 = new MySqlCommand(sql2, myConnect);
                        cmd2.ExecuteNonQuery();
                        licnt = 1;
                    }
                    log.Info("Inserting data into Database done..");
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
                MessageBox.Show(e.Message, "GetStickerDetails", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return _temp;
        }
        public void GetDocStampDetailsRCBC(List<DocStampModel> _temp, int _docStampNumber)
        {
            log.Info("Generating Document Stamp Details..");
            try
            {
                


                //Orginal Query
                Sql = "Select P.BankCode, DocStampNumber,SalesInvoice,Count(ChkType) as Quantity,ChkType, P.Description, H.DocStamp, " +
                      "Username_DocStamp, CheckedByDS,PurchaseOrderNumber,P.QuantityOnHand,H.Batch," +
                      "(Count(ChkType) * H.DocStamp) as TotalAmount,H.location,SalesInvoiceDate from " + gClient.DataBaseName +
                      " H left join " + gClient.PriceListTable + "  P on H.ChkType = P.FinalChkType and H.ProductCode = P.ProductCode" +
                      " where  DocStampNumber= " + _docStampNumber + " and isCancelled = 0  Group by H.ChequeName order by DocStampNumber, ChkType";
                //_docStampNumber.ForEach(x => { 
                //    Sql = "Select P.BankCode, DocStampNumber,SalesInvoice,Count(ChkType) as Quantity,ChkType, P.CDescription, H.DocStamp, " + //Based on RCBC requirements
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
                    doc.DocStampDate = !reader.IsDBNull(14) ? reader.GetDateTime(14) : DateTime.Now;

                    _temp.Add(doc);
                   
                }
                reader.Close();
                DBClosed();
                //DBConnect();

                //string Sql4 = "Select P.BankCode, DocStampNumber,SalesInvoice,Count(ChkType) as Quantity,ChkType, P.Description, H.DocStamp, " +
                //      "Username_DocStamp, CheckedByDS,PurchaseOrderNumber,P.QuantityOnHand,H.Batch," +
                //      "(Count(ChkType) * H.DocStamp) as TotalAmount,H.location,SalesInvoiceDate from cancelled_transaction " +
                //      " H left join " + gClient.PriceListTable + "  P on H.ChkType = P.FinalChkType and H.ProductCode = P.ProductCode" +
                //      " where  DocStampNumber= " + _docStampNumber + " Group by H.ChequeName order by DocStampNumber, ChkType ";

                //MySqlCommand cmd4 = new MySqlCommand(Sql4, myConnect);
                //cmd4.ExecuteNonQuery();
                //MySqlDataReader reader3 = cmd4.ExecuteReader();
                //while(reader3.Read())
                //{

                //}
                //reader3.Close();
                //DBClosed();



                DBConnect();
                string Sql1 = "Delete from " + gClient.DocStampTempTable;
                cmd = new MySqlCommand(Sql1, myConnect);
                cmd.ExecuteNonQuery();
                DBClosed();

                string Sql3 = "Select Distinct(Batch) from " + gClient.DataBaseName + " where DocStampNumber = '" + _docStampNumber + "';";

                DBConnect();
                MySqlCommand cmd3 = new MySqlCommand(Sql3, myConnect);
                cmd3.ExecuteNonQuery();
                string batches2 = "";

                MySqlDataReader reader2 = cmd3.ExecuteReader();
                int counter = 1;
                while (reader2.Read())
                {
                    batches2 += !reader2.IsDBNull(0) ? reader2.GetString(0) : "";
                    if (reader2.FieldCount != counter)
                    {
                        batches2 += ",";
                    }

                    counter++;
                }
                reader2.Close();
                DBClosed();



                DBConnect();
                _temp.ForEach(d =>
                {

                    string Sql2 = "Insert into " + gClient.DocStampTempTable + "(Bank, DocStampNumber,SalesInvoice,Quantity,ChkType, ChequeDesc, DocStampPrice, " +
                                "PreparedBy, CheckedBy, PONumber,BalanceOrder,Batch,TotalAmount,Location,DocStampDate)Values('" + gClient.Description + "'," + d.DocStampNumber +
                                ", " + d.SalesInvoiceNumber + "," + d.TotalQuantity + ",'" + d.ChkType + "','" + d.DocDesc.Replace("'", "''") +
                                "'," + d.DocStampPrice + ",'" + d.PreparedBy + "','" + d.CheckedBy + "'," + d.POorder + "," + d.QuantityOnHand +
                                ",'" + batches2 + "'," + d.TotalAmount + ",'" + d.Location + "','" + d.DocStampDate.ToString("yyyy-MM-dd") + "')";
                    MySqlCommand cmd2 = new MySqlCommand(Sql2, myConnect);
                    cmd2.ExecuteNonQuery();
                    log.Info("Inserting to Docstamp table Done..");
                });
                //  });
                DBClosed();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GetDocStampDetails", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void fUpdateDocstamp(List<DocStampModel> _docStamp)
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

            log.Info("Updating DocStamp in Database..");
            DBConnect();
            for (int i = 0; i < _docStamp.Count; i++)
            {
                Sql = "Update " + gClient.DataBaseName + " set DocStamp = " + _docStamp[i].DocStampPrice + ", DocStampNumber = " + _docStamp[i].DocStampNumber +
                    ", Date_DocStamp = '" + _docStamp[i].DocStampDate.ToString("yyyy-MM-dd") + "',Username_DocStamp ='" + _docStamp[i].PreparedBy +
                    "', CheckedByDS = '" + _docStamp[i].CheckedBy + "'  where SalesInvoice = " + _docStamp[i].SalesInvoiceNumber;
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
        public string fGetDataByBatch(List<TempModel> _temp, string _batch)
        {
            try
            {
                DBConnect();
                Sql = "Select Bank, Batch, Brstn,BranchName,BranchCode,Date,ChkType, ChequeName,AccountNo,Name1,Name2, StartingSerial, EndingSerial, SalesInvoice," +
                    "UnitPrice,DocStamp,DeliveryDate,username,SalesInvoiceGeneratedBy,SalesInvoiceDate,location,DrNumber,PackNumber,DocStampNumber,AttentionTo," +
                    "ProductCode,Block,Segment,ProductType,DeliveryToBrstn,DeliveryToBranch,OldBranchCode,CheckedByDS,PurchaseOrderNumber,Count(ChkType) as Quantity " +
                    " from " + gClient.DataBaseName + " where Batch Like '" + _batch + "%' and isCancelled = 0 group by Batch,ChequeName,ChkType;";
                //" from "  + gClient.DataBaseName + " where Batch Like '" + _batch + "%' or (DocStampNumber Like '%"+_batch+"%') group by Batch,ChequeName,ChkType;";
                //Added or (DocStampNumber ='"+_batch+"%')
                cmd = new MySqlCommand(Sql, myConnect);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TempModel t = new TempModel
                    {
                        BankCode = !reader.IsDBNull(0) ? reader.GetString(0) : "",
                        Batch = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                        BRSTN = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                        BranchName = !reader.IsDBNull(3) ? reader.GetString(3) : "",
                        BranchCode = !reader.IsDBNull(4) ? reader.GetString(4) : "",
                        DateProcessed = !reader.IsDBNull(5) ? reader.GetDateTime(5) : DateTime.Now,
                        ChkType = !reader.IsDBNull(6) ? reader.GetString(6) : "",
                        ChequeName = !reader.IsDBNull(7) ? reader.GetString(7) : "",
                        AccountNo = !reader.IsDBNull(8) ? reader.GetString(8) : "",
                        Name1 = !reader.IsDBNull(9) ? reader.GetString(9) : "",
                        Name2 = !reader.IsDBNull(10) ? reader.GetString(10) : "",
                        StartingSerial = !reader.IsDBNull(11) ? reader.GetString(11) : "",
                        EndingSerial = !reader.IsDBNull(12) ? reader.GetString(12) : "",
                        SalesInvoice = !reader.IsDBNull(13) ? reader.GetInt32(13) : 0,
                        UnitPrice = !reader.IsDBNull(14) ? reader.GetDouble(14) : 0,
                        DocStampPrice = !reader.IsDBNull(15) ? reader.GetDouble(15) : 0,
                        DeliveryDate = !reader.IsDBNull(16) ? reader.GetDateTime(16) : DateTime.Now,
                        username = !reader.IsDBNull(17) ? reader.GetString(17) : "",
                        SalesInvoiceGeneratedBy = !reader.IsDBNull(18) ? reader.GetString(18) : "",
                        SalesInvoiceDate = !reader.IsDBNull(19) ? reader.GetDateTime(19) : DateTime.Now,
                        Location = !reader.IsDBNull(20) ? reader.GetString(20) : "",
                        DrNumber = !reader.IsDBNull(21) ? reader.GetString(21) : "",
                        PackNumber = !reader.IsDBNull(22) ? reader.GetString(22) : "",
                        DocStampNumber = !reader.IsDBNull(23) ? reader.GetInt32(23) : 0,
                        AttentionTo = !reader.IsDBNull(24) ? reader.GetString(24) : "",
                        ProductCode = !reader.IsDBNull(25) ? reader.GetString(25) : "",
                        Block = !reader.IsDBNull(26) ? reader.GetInt32(26) : 0,
                        Segment = !reader.IsDBNull(27) ? reader.GetInt32(27) : 0,
                        ProducType = !reader.IsDBNull(28) ? reader.GetString(28) : "",
                        DeliveryToBrstn = !reader.IsDBNull(29) ? reader.GetString(29) : "",
                        DeliveryToBranch = !reader.IsDBNull(30) ? reader.GetString(30) : "",
                        OldBranchCode = !reader.IsDBNull(31) ? reader.GetString(31) : "",
                        CheckedBy = !reader.IsDBNull(32) ? reader.GetString(32) : "",
                        PONumber = !reader.IsDBNull(33) ? reader.GetInt32(33) : 0, 
                        Qty = !reader.IsDBNull(34) ? reader.GetInt32(34) : 0

                       
                    };
                    _temp.Add(t);
                }
                reader.Close();
                DBClosed();

                return _batch;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Populating data from database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return _batch;
            }
        }
        public List<PurchaseOrderModel> gListofPurchaseOrder(List<PurchaseOrderModel> _list)
        {
            try
            {
                Sql = "Select PurchaseOrderNo, PurchaseOrderDateTime, ProductCode, Quantity, ChequeName, Description, UnitPrice, DocStamp," +
                    "CheckType, ClientCode,GeneratedBy, CheckedBy, ApprovedBy from " + gClient.PurchaseOrderFinishedTable + ";";
                DBConnect();
                cmd = new MySqlCommand(Sql,myConnect);
                MySqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    PurchaseOrderModel p = new PurchaseOrderModel
                    {
                        PurchaseOrderNumber  = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                        PurchaseOrderDateTime = !reader.IsDBNull(1) ? reader.GetDateTime(1) : DateTime.Now,
                        ProductCode = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                        Quantity = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0,
                        ChequeName = !reader.IsDBNull(4) ? reader.GetString(4) : "",
                        Description = !reader.IsDBNull(5) ? reader.GetString(5) : "",
                        UnitPrice = !reader.IsDBNull(6) ? reader.GetDouble(6) : 0,
                        Docstamp = !reader.IsDBNull(7) ? reader.GetDouble(7) : 0,
                        CheckType = !reader.IsDBNull(8) ? reader.GetString(8) : "",
                        ClientCode = !reader.IsDBNull(9) ? reader.GetString(9) : "",
                        GeneratedBy = !reader.IsDBNull(10) ? reader.GetString(10) : "",
                        CheckedBy = !reader.IsDBNull(11) ? reader.GetString(11) : "",
                        ApprovedBy = !reader.IsDBNull(12) ? reader.GetString(12) : ""
                    };

                    _list.Add(p);
                }
                reader.Close();
                DBClosed();
                return _list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Getting List of Purchase Order ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return _list;
            }
        }
        public List<TempModel> sGetChequeName(List<TempModel> _temp)
        {
            try
            {
                
                Sql = "Select A.Type,A.ChequeName,A.Description,A.CProductCode,B.ProductName from rcbc_tcheques A inner join rcbc_tchequeproducts B on " +
                            "A.CProductCode = B.CProductCode;";
                DBConnect();
                cmd = new MySqlCommand(Sql, myConnect);
                MySqlDataReader reader = cmd.ExecuteReader();
                
                while(reader.Read())
                {
                    TempModel t = new TempModel
                    {
                        ChkType = !reader.IsDBNull(0) ? reader.GetString(0) : "",
                        ChequeName = !reader.IsDBNull(1) ? reader.GetString(1) :"",
                        CheqDesc = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                        PCode = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0,
                        ProductName = !reader.IsDBNull(4) ? reader.GetString(4) : ""
                    };
                    _temp.Add(t);
                }
                reader.Close();
                DBClosed();
               
                return _temp;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Cheque Name ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }
        public void fGetBalancePO(List<int> _listofPo)
        {
            try
            {
                

            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.Message, "Get List of Purchase Order Balance", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }
        public void CheckData(DataGridView _dgv,List<OrderingModel> _orderList,List<BranchesModel> _branches,string _batch,DateTime _deliveryDate)
        {
            try
            {
                //process.DeleteSQL();
                //process.DeleteErrorMessage(Application.StartupPath);
                //con.GetAllBranches(branches);
                if (Directory.GetFiles("C:\\Head\\").Length == 0) // if the path folder is empty
                    MessageBox.Show("No files found in directory path", "***System Error***");
                else
                {
                    string[] list = Directory.GetFiles( "C:\\Head\\");

                    string Extension = "";

                    foreach (string FileName in list)
                    {
                        //Get the Extension Name
                        int LoopCount = FileName.ToString().Length - 2;
                        while (LoopCount > 0)
                        {

                            if (FileName.ToString().Substring(LoopCount, 1) == "." && Extension == "")
                            {
                                Extension = FileName.ToString().Substring(LoopCount + 1, FileName.ToString().Length - LoopCount - 1).ToUpper();
                            }

                            LoopCount = LoopCount - 1;
                        }

                        //MessageBox.Show(Extension);
                        // string Cont = "";
                        if (Extension == "TXT")
                        {
                            MessageBox.Show("WTF");
                            for (int i = 0; i < list.Length; i++)
                            {
                                Int64 SN = 0;
                                Int64 EN = 0;
                                string[] lines = File.ReadAllLines(list[i]);
                                //string[] errors;

                                // con.GetAllBranches(listofBranch);
                                for (int b = 1; b < lines.Length; b++)
                                {
                                    //WriteHash(lines[b]);

                                    if (lines[b].TrimEnd().Length < 20)
                                    {
                                        //footer = lines[b].Substring(0, 16);
                                    }
                                    else
                                    {
                                        if (lines[b].Substring(25, 3) != "PER" && lines[b].Substring(25, 3) != "COR" && lines[b].Substring(25, 3) != "EC1" &&
                                             lines[b].Substring(25, 3) != "EP1" && lines[b].Substring(25, 3) != "MC1")
                                        {
                                            //ErrorText = "CheckType: " + lines[b].Substring(25, 3) + " is not valid";

                                            //ErrorMessage(ErrorText);
                                        }
                                        else
                                        {

                                            int bkperpcs = 0;
                                            if (lines[b].Substring(25, 3) == "PER" || lines[b].Substring(25, 3) == "EP1" || lines[b].Substring(25, 3) == "MC1")
                                                bkperpcs = 50;
                                            else
                                                bkperpcs = 100;

                                            int qty = int.Parse(lines[b].Substring(38, 3)) / bkperpcs;

                                            //header = lines[0].Substring(0, 11);

                                            SN = Int64.Parse(lines[b].Substring(28, 10)); // getting starting serial per account
                                                                                          //for (int r = 0; r < qty; r++) //Loop for quantity of the order
                                                                                          //{

                                            OrderModel order = new OrderModel();
                                            // initializing data to list 

                                            //order.Default = lines[b].Substring(0, 3);
                                            //order.BankCode = lines[b].Substring(3, 2);
                                            //order.CurrencyCode = lines[b].Substring(5, 3);
                                            //order.Filler = lines[b].Substring(8, 3);
                                            //order.AccBranchCode = lines[b].Substring(11, 4);
                                            //order.Filler2 = lines[b].Substring(15, 2);
                                            //order.AccountNo = order.AccBranchCode + lines[b].Substring(17, 8);
                                            //order.ChkType = lines[b].Substring(25, 3);
                                            //order.tQty = lines[b].Substring(38, 3);
                                            //order.Channel = lines[b].Substring(42, 3);

                                            //var rb = branches.Find(a => a.BranchCode == order.AccBranchCode);// Checking if the branchCode does exist in the database!!
                                            //if (rb == null)
                                            //{
                                            //    ErrorText = "BranchCode: " + order.AccBranchCode + " does not exist in Database";

                                            //    ErrorMessage(ErrorText);
                                            //}
                                            //if (order.ChkType == "PER")
                                            //{

                                            //    order.PcsPerBook = 50;
                                            //    order.ChequeName = "Personal Checks";
                                            //    order.ChkTypeDbf = "A";
                                            //    order.Quantity = 1;
                                            //    order.StartingSerial = SN.ToString();
                                            //    order.outputFolder = "RegularChecks";
                                            //    EN = SN + 49;
                                            //    TotalA += 1;

                                            //}
                                            //if (order.ChkType == "EP1")
                                            //{
                                            //    order.ChequeName = "E-Personal Checks";
                                            //    order.PcsPerBook = 50;
                                            //    order.ChkTypeDbf = "A";
                                            //    order.Quantity = 1;
                                            //    order.StartingSerial = SN.ToString();
                                            //    EN = SN + 49;
                                            //    order.outputFolder = "E1";
                                            //    TotalEP += 1;
                                            //}
                                            //if (order.ChkType == "MC1")
                                            //{
                                            //    order.ChequeName = "Managers Checks";
                                            //    order.PcsPerBook = 50;
                                            //    order.ChkTypeDbf = "A";
                                            //    order.Quantity = 1;
                                            //    order.StartingSerial = SN.ToString();
                                            //    EN = SN + 49;
                                            //    TotalMC += 1;
                                            //    order.outputFolder = "MC";
                                            //}
                                            //if (order.ChkType == "COR")
                                            //{

                                            //    order.PcsPerBook = 100;
                                            //    order.ChequeName = "Commercial Checks";
                                            //    order.ChkTypeDbf = "B";
                                            //    order.Quantity = 1;
                                            //    order.StartingSerial = SN.ToString();
                                            //    EN = SN + 99;
                                            //    TotalB += 1;
                                            //    order.outputFolder = "RegularChecks";
                                            //}
                                            //if (order.ChkType == "EC1")
                                            //{
                                            //    order.PcsPerBook = 100;
                                            //    order.ChequeName = "E-Commercial Checks";
                                            //    order.ChkTypeDbf = "B";
                                            //    order.Quantity = 1;
                                            //    order.StartingSerial = SN.ToString();
                                            //    EN = SN + 99;
                                            //    TotalEC += 1;
                                            //    order.outputFolder = "E1";
                                            //}



                                            //order.Flag = lines[b].Substring(41, 1);
                                            //if (order.Flag == "Y")
                                            //{
                                            //    order.PrinterFileName = lines[b].Substring(90, 40).TrimEnd();
                                            //    order.PrinterFileName2 = lines[b].Substring(130, 40).TrimEnd();
                                            //    order.Name1 = lines[b].Substring(90, 40).TrimEnd();
                                            //    order.Name2 = lines[b].Substring(130, 40).TrimEnd();
                                            //}
                                            //else
                                            //{
                                            //    order.PrinterFileName = "";
                                            //    order.PrinterFileName2 = "";
                                            //    order.Name1 = lines[b].Substring(90, 40).TrimEnd();
                                            //    order.Name2 = lines[b].Substring(130, 40).TrimEnd();
                                            //}

                                            //order.FillerBranchName = lines[b].Substring(49, 40);
                                            //order.BranchName = rb.Address1.Replace('Ñ', 'N');
                                            //order.Address2 = rb.Address2.Replace('Ñ', 'N');
                                            //order.Address3 = rb.Address3.Replace('Ñ', 'N');
                                            //order.Address4 = rb.Address4.Replace('Ñ', 'N');
                                            //order.Address5 = rb.Address5.Replace('Ñ', 'N');
                                            //order.Address6 = rb.Address6.Replace('Ñ', 'N');
                                            //order.productType = lines[b].Substring(210, 3);
                                            //order.BRSTNFiller = lines[b].Substring(213, 9);
                                            //order.BRSTN = rb.BRSTN;
                                            //order.OldBranchCode = rb.OldBranchCode;
                                            //order.EndingSerial = EN.ToString();
                                            //orderList.Add(order); // adding data to List model

                                            SN = EN + 1;

                                        }

                                        //for (int j= 0; j < orderList.Count; j++)
                                        //{



                                    }
                                    //}
                                }
                            }

                        }
                        else if (Extension == "CSV")
                        {
                            DataTable dt = new DataTable();
                            dt.Columns.Add("TYPE");
                            dt.Columns.Add("BRSTN");
                            dt.Columns.Add("ACCOUNT NO.");
                            dt.Columns.Add("ACCOUNT NAME");
                            dt.Columns.Add("ACCOUNT NAME2");
                            dt.Columns.Add("BOOK TYPE");
                            dt.Columns.Add("STYLE");
                            dt.Columns.Add("QUANTITY");
                            dt.Columns.Add("STARTING SERIAL");
                            dt.Columns.Add("PICK_RC");
                            dt.Columns.Add("ADDRESS");
                            dt.Columns.Add("ADDRESS 2");
                            dt.Columns.Add("DELIVERY_0");
                            dt.Columns.Add("DELIVERY BRANCH");
                            dt.Columns.Add("DELIVERY BRSTN");
                            dt.Columns.Add("ORDER DATE");
                            dt.Columns.Add("CHANNEL ID");
                            
                            StreamReader sr = new StreamReader(FileName);
                            string[] listData = new string[File.ReadAllLines(FileName).Length];
                            int counter = 0;
                            listData = sr.ReadLine().Split(';'); // Skipping First Line of CSV File
                            while(!sr.EndOfStream) //Reading all lines
                            {

                                listData = sr.ReadLine().Split(';'); //splitting record
                            
                                //Adding record to Datatables
                                dt.Rows.Add(listData[counter], listData[counter + 1], listData[counter + 2],
                                            listData[counter+3], listData[counter + 4], listData[counter + 5],
                                            listData[counter+6], listData[counter + 7], listData[counter + 8],
                                            listData[counter+9], listData[counter + 10], listData[counter + 11],
                                            listData[counter +12], listData[counter + 13], listData[counter + 14],
                                            listData[counter +15], listData[counter + 16]);

                                OrderingModel order = new OrderingModel
                                {
                                            
                                    ChkType =listData[counter],                     //    TYPE,BRSTN,ACCOUNT NO.,
                                    BRSTN = listData[counter + 1],                  //    ACCOUNT NAME,ACCOUNT NAME2,BOOK TYPE,
                                    AccountNo = listData[counter +2],               //    STYLE,QUANTITY,STARTING SERIAL,
                                    AccountName = listData[counter + 3],            //    PICK_RC,ADDRESS,ADDRESS 2,
                                    AccountName2 = listData[counter + 4],          //    DELIVERY_0,DELIVERY BRANCH,DELIVERY BRSTN,ORDER DATE,CHANNEL ID
                                    BookType = listData[counter + 5],
                                    Style = int.Parse(listData[counter + 6]),
                                    OrdQuantiy = int.Parse(listData[counter + 7]),
                                    StartingSerial = listData[counter + 8],
                                    PickUpRc = listData[counter + 9],
                                //    BranchName = listData[counter + 10],
                                //    Address = listData[counter + 11],
                                    Delivery0 = listData[counter + 12],
                                //    DeliveryBranch = listData[counter + 13],
                                    DeliveryBrstn = listData[counter + 14],
                                    OrderDate = DateTime.Parse(listData[counter + 15]),
                                    Channel = listData[counter + 16]
                                    


                                };
                                order.Batch = _batch;
                                order.DeliveryDate = _deliveryDate;
                                var branches = _branches.Where(x => x.BRSTN == order.BRSTN).Distinct().ToList();
                                order.BranchName = branches[0].Address1.Replace("'","''").TrimEnd();
                                order.BranchName = branches[0].Address1.Replace("Ñ", "N").TrimEnd();
                                order.Address = branches[0].Address2.Replace("'", "''").TrimEnd();
                                order.Address = branches[0].Address2.Replace("Ñ", "N").TrimEnd();
                                order.Address2 = branches[0].Address3.Replace("'", "''").TrimEnd();
                                order.Address2 = branches[0].Address3.Replace("Ñ", "N").TrimEnd();
                                order.Address3 = branches[0].Address4.Replace("'", "''").TrimEnd();
                                order.Address3 = branches[0].Address4.Replace("Ñ", "N").TrimEnd();
                                order.Address4 = branches[0].Address5.Replace("'", "''").TrimEnd();
                                order.Address4 = branches[0].Address5.Replace("Ñ", "N").TrimEnd();
                                order.Address5 = branches[0].Address6.Replace("'", "''").TrimEnd();
                                order.Address5 = branches[0].Address6.Replace("Ñ", "N").TrimEnd();
                                order.BranchCode = branches[0].BranchCode;
                                var dBranches = _branches.Where(x => x.BRSTN == order.DeliveryBrstn).Distinct().ToList();
                                order.DeliveryBranch = dBranches[0].Address1.Replace("'", "''");
                                order.DeliveryBranch = dBranches[0].Address1.Replace("Ñ", "N");
                                order.DeliveryBranchCode = dBranches[0].BranchCode.Replace("'", "''");

                                if (order.ChkType == "A")
                                    order.EndingSerial = (int.Parse(order.StartingSerial) + 49).ToString();
                                else
                                    order.EndingSerial = (int.Parse(order.StartingSerial) + 99).ToString();

                                          
                                _orderList.Add(order);
                              
                            }
                       
                            for (int i = 0; i < _orderList.Count; i++)
                            {
                                _orderList[i].ID = i + 1;
                                var prodStyle = frmOrdering.productList.Select(x => x.BookStyle).Distinct().ToList();
                                for (int x = 0; x < prodStyle.Count; x++)
                                {
                                    if (_orderList[i].Style == prodStyle[0] && _orderList[i].ChkType == "A")
                                    {
                                        _orderList[i].CheckName = frmOrdering.productList[0].ChequeName;
                                        _orderList[i].outputFolder = "RegularChecks";

                                    }
                                    if (_orderList[i].Style == prodStyle[0] && _orderList[i].ChkType == "B")
                                    {
                                        _orderList[i].CheckName = frmOrdering.productList[1].ChequeName;
                                        _orderList[i].outputFolder = "RegularChecks";

                                    }
                                    else if (_orderList[i].Style == prodStyle[1])
                                    {
                                        _orderList[i].CheckName = frmOrdering.productList[2].ChequeName;
                                        _orderList[i].outputFolder = "Reca";
                                    }
                                    else if (_orderList[i].Style == prodStyle[2] && _orderList[i].ChkType == "A")
                                    {
                                        _orderList[i].CheckName = frmOrdering.productList[3].ChequeName;
                                        _orderList[i].outputFolder = "DragonBlue";
                                    }
                                    else if (_orderList[i].Style == prodStyle[3] && _orderList[i].ChkType == "B")
                                    {
                                        _orderList[i].CheckName = frmOrdering.productList[4].ChequeName;
                                        _orderList[i].outputFolder = "DragonBlue";
                                    }
                                    else if (_orderList[i].Style == prodStyle[4] && _orderList[i].ChkType == "A")
                                    {
                                        _orderList[i].CheckName = frmOrdering.productList[5].ChequeName;
                                        _orderList[i].outputFolder = "DragonYellow";
                                    }
                                    else if (_orderList[i].Style == prodStyle[5] && _orderList[i].ChkType == "B")
                                    {
                                        _orderList[i].CheckName = frmOrdering.productList[6].ChequeName;
                                        _orderList[i].outputFolder = "DragonYellow";
                                    }
                                    else if (_orderList[i].Style == prodStyle[6])
                                    {
                                        _orderList[i].CheckName = frmOrdering.productList[7].ChequeName;
                                        _orderList[i].outputFolder = "Online";
                                    }
                                }
                                    

                                
                            }
                            
                        }

                        if (ErrorText == null || ErrorText == "")
                        {
                            
                            _dgv.DataSource = _orderList;
                            bg_dtg(_dgv);
                            _dgv.Columns[0].Width = 50;
                            MessageBox.Show("No Errors Found!!!", "Checking Data!",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Please check ErrorMessage.txt for error references!!","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            Application.Exit();
                        }
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Check Data",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private static string Seperator()
        {
            return "";
        }
        public void DoBlockProcess(TypeofCheckModel _orders, frmOrdering _mainForm,string outputFolder)
        {
            StreamWriter file;
            //DbConServices db = new DbConServices();

            if (_orders.Ordering_Regular_Personal.Count > 0)
            {

                for (int i = 0; i < _orders.Ordering_Regular_Personal.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + _orders.Ordering_Regular_Personal[0].outputFolder + "\\BlockP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        //for (int i = 0; i < check; i++)
                        //{

                        string output = ConvertToBlockText(_orders.Ordering_Regular_Personal, "REGULAR CHECKS", "PERSONAL", _mainForm.batchFile, _mainForm.deliveryDate, gUser.FirstName, _mainForm);
                        //  }
                        file.WriteLine(output);
                    }

                }
            }
            if (_orders.Ordering_Regular_Commercial.Count > 0)
            {
                for (int i = 0; i < _orders.Ordering_Regular_Commercial.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + _orders.Ordering_Regular_Commercial[0].outputFolder + "\\BlockC.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        //for (int i = 0; i < check; i++)
                        //{

                        string output = ConvertToBlockText(_orders.Ordering_Regular_Commercial, "REGULAR CHECKS", "COMMERCIAL", _mainForm.batchFile, _mainForm.deliveryDate, gUser.FirstName, _mainForm);
                        //  }
                        file.WriteLine(output);
                    }

                }
            }
            if (_orders.Ordering_DragonBlue_Personal.Count > 0)
            {
                for (int i = 0; i < _orders.Ordering_DragonBlue_Personal.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + _orders.Ordering_DragonBlue_Personal[0].outputFolder + "\\BlockP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        //for (int i = 0; i < check; i++)
                        //{

                        string output = ConvertToBlockText(_orders.Ordering_DragonBlue_Personal, "DRAGON BLUE CHECKS", "PERSONAL", _mainForm.batchFile, _mainForm.deliveryDate, gUser.FirstName, _mainForm);
                        //  }
                        file.WriteLine(output);
                    }

                }
            }
            if (_orders.Ordering_DragonBlue_Commercial.Count > 0)
            {
                for (int i = 0; i < _orders.Ordering_DragonBlue_Commercial.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + _orders.Ordering_DragonBlue_Commercial[0].outputFolder + "\\BlockC.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        //for (int i = 0; i < check; i++)
                        //{

                        string output = ConvertToBlockText(_orders.Ordering_DragonBlue_Commercial, "DRAGON BLUE CHECKS", "COMMERCIAL", _mainForm.batchFile, _mainForm.deliveryDate, gUser.FirstName, _mainForm);
                        //  }
                        file.WriteLine(output);
                    }

                }
            }
            if (_orders.Ordering_DragonYellow_Personal.Count > 0)
            {
                for (int i = 0; i < _orders.Ordering_DragonYellow_Personal.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + _orders.Ordering_DragonYellow_Personal[0].outputFolder + "\\BlockP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        //for (int i = 0; i < check; i++)
                        //{

                        string output = ConvertToBlockText(_orders.Ordering_DragonYellow_Personal, "MANAGER'S CHECKS", "MANAGER'S", _mainForm.batchFile, _mainForm.deliveryDate, gUser.FirstName, _mainForm);
                        //  }
                        file.WriteLine(output);
                    }

                }
            }
            if (_orders.Ordering_DragonYellow_Commercial.Count > 0)
            {
                for (int i = 0; i < _orders.Ordering_DragonYellow_Commercial.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + _orders.Ordering_DragonYellow_Commercial[0].outputFolder + "\\BlockC.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        //for (int i = 0; i < check; i++)
                        //{

                        string output = ConvertToBlockText(_orders.Ordering_DragonYellow_Commercial, "MANAGER'S CHECKS", "MANAGER'S", _mainForm.batchFile, _mainForm.deliveryDate, gUser.FirstName, _mainForm);
                        //  }
                        file.WriteLine(output);
                    }

                }
            }
            if (_orders.Ordering_Online_Commercial.Count > 0)
            {
                for (int i = 0; i < _orders.Ordering_Online_Commercial.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + _orders.Ordering_Online_Commercial[0].outputFolder + "\\BlockC.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        //for (int i = 0; i < check; i++)
                        //{

                        string output = ConvertToBlockText(_orders.Ordering_Online_Commercial, "MANAGER'S CHECKS", "MANAGER'S", _mainForm.batchFile, _mainForm.deliveryDate, gUser.FirstName, _mainForm);
                        //  }
                        file.WriteLine(output);
                    }

                }
            }
            if (_orders.Ordering_Reca_Commercial.Count > 0)
            {
                for (int i = 0; i < _orders.Ordering_Reca_Commercial.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + _orders.Ordering_Reca_Commercial[0].outputFolder + "\\BlockC.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        //for (int i = 0; i < check; i++)
                        //{

                        string output = ConvertToBlockText(_orders.Ordering_DragonYellow_Personal, "MANAGER'S CHECKS", "MANAGER'S", _mainForm.batchFile, _mainForm.deliveryDate, gUser.FirstName, _mainForm);
                        //  }
                        file.WriteLine(output);
                    }

                }
            }
        }
        public void DoBlockProcessManual(TypeofCheckModel _orders, frmManualEncode _mainForm, string outputFolder)
        {
            StreamWriter file;
            //DbConServices db = new DbConServices();

            if (_orders.Ordering_Customized.Count > 0)
            {

                for (int i = 0; i < _orders.Ordering_Customized.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + _orders.Ordering_Customized[0].outputFolder + "\\BlockP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        //for (int i = 0; i < check; i++)
                        //{

                        string output = ConvertToBlockTextManual(_orders.Ordering_Customized,"CUSTOMIZED ", "COMMERCIAL", _mainForm.batchFile, _mainForm.deliveryDate, gUser.FirstName, _mainForm);
                        //  }
                        file.WriteLine(output);
                    }

                }
            }
           
        }
        public void PackingText(TypeofCheckModel _checksModel, frmOrdering _mainForm,string outputFolder)
        {

            StreamWriter file;
           // DbConServices db = new DbConServices();

            if (_checksModel.Ordering_Regular_Personal.Count > 0)
            {

                for (int i = 0; i < _checksModel.Ordering_Regular_Personal.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + _checksModel.Ordering_Regular_Personal[0].outputFolder + "\\PackingP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = ConvertToPackingList(_checksModel.Ordering_Regular_Personal, "PERSONAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Ordering_Regular_Commercial.Count > 0)
            {

                for (int i = 0; i < _checksModel.Ordering_Regular_Commercial.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + _checksModel.Ordering_Regular_Commercial[0].outputFolder + "\\PackingC.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = ConvertToPackingList(_checksModel.Ordering_Regular_Commercial, "COMMERCIAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Ordering_DragonBlue_Personal.Count > 0)
            {

                for (int i = 0; i < _checksModel.Ordering_DragonBlue_Personal.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + _checksModel.Ordering_DragonBlue_Personal[0].outputFolder + "\\PackingP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = ConvertToPackingList(_checksModel.Ordering_DragonBlue_Personal, "DRAGON BLUE CHECK", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Ordering_DragonBlue_Commercial.Count > 0)
            {

                for (int i = 0; i < _checksModel.Ordering_DragonBlue_Commercial.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + _checksModel.Ordering_DragonBlue_Commercial[0].outputFolder + "\\PackingC.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = ConvertToPackingList(_checksModel.Ordering_DragonBlue_Commercial, "DRAGON BLUE CHECK", _mainForm);
                         file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Ordering_DragonYellow_Personal.Count > 0)
            {

                for (int i = 0; i < _checksModel.Ordering_DragonYellow_Personal.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + _checksModel.Ordering_DragonYellow_Personal[0].outputFolder + "\\PackingA.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = ConvertToPackingList(_checksModel.Ordering_DragonYellow_Personal, "DRAGON YELLOW CHECK", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Ordering_DragonYellow_Commercial.Count > 0)
            {

                for (int i = 0; i < _checksModel.Ordering_DragonYellow_Commercial.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + _checksModel.Ordering_DragonYellow_Commercial[0].outputFolder + "\\PackingB.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = ConvertToPackingList(_checksModel.Ordering_DragonYellow_Commercial, "DRAGON YELLOW CHECK", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Ordering_Online_Commercial.Count > 0)
            {

                for (int i = 0; i < _checksModel.Ordering_Online_Commercial.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + _checksModel.Ordering_Online_Commercial[0].outputFolder + "\\PackingB.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = ConvertToPackingList(_checksModel.Ordering_Online_Commercial, "ONLINE COMMERCIAL CHECK", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Ordering_Reca_Commercial.Count > 0)
            {

                for (int i = 0; i < _checksModel.Ordering_Reca_Commercial.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + _checksModel.Ordering_Reca_Commercial[0].outputFolder + "\\PackingB.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = ConvertToPackingList(_checksModel.Ordering_Reca_Commercial, "RECA COMMERCIAL CHECK", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
        }// End of Function
        public void PackingTextManual(TypeofCheckModel _checksModel, frmManualEncode _mainForm, string outputFolder)
        {

            StreamWriter file;
            // DbConServices db = new DbConServices();

            if (_checksModel.Ordering_Customized.Count > 0)
            {

                for (int i = 0; i < _checksModel.Ordering_Customized.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + _checksModel.Ordering_Customized[0].outputFolder + "\\PackingC.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = ConvertToPackingListManual(_checksModel.Ordering_Customized, "COMMERCIAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
    
        }// End of Function
        public void PrinterFile(TypeofCheckModel _checkModel, frmOrdering _mainForm)
        {


            StreamWriter file;


            if (_checkModel.Ordering_Regular_Personal.Count > 0)
            {

                string printerFilePathA = Application.StartupPath +"\\Output\\" + _checkModel.Ordering_Regular_Personal[0].outputFolder + "\\RCBC" + _mainForm.batchFile.Substring(0, 4) + "P.txt";


                if (File.Exists(printerFilePathA))
                    File.Delete(printerFilePathA);

                file = File.CreateText(printerFilePathA);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                {
                    string output = ConvertToPrinterFile(_checkModel.Ordering_Regular_Personal);

                    file.WriteLine(output);
                }
            }
            if (_checkModel.Ordering_Regular_Commercial.Count > 0)
            {

                string printerFilePathA = Application.StartupPath +"\\Output\\" + _checkModel.Ordering_Regular_Commercial[0].outputFolder + "\\RCBC" + _mainForm.batchFile.Substring(0, 4) + "C.txt";


                if (File.Exists(printerFilePathA))
                    File.Delete(printerFilePathA);

                file = File.CreateText(printerFilePathA);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                {
                    string output = ConvertToPrinterFile(_checkModel.Ordering_Regular_Commercial);

                    file.WriteLine(output);
                }
            }
            if (_checkModel.Ordering_DragonBlue_Personal.Count > 0)
            {

                string printerFilePathA = Application.StartupPath + "\\Output\\" + _checkModel.Ordering_DragonBlue_Personal[0].outputFolder + "\\RCBC" + _mainForm.batchFile.Substring(0, 4) + "DBP.txt";


                if (File.Exists(printerFilePathA))
                    File.Delete(printerFilePathA);

                file = File.CreateText(printerFilePathA);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                {
                    string output = ConvertToPrinterFile(_checkModel.Ordering_DragonBlue_Personal);

                    file.WriteLine(output);
                }
            }
            if (_checkModel.Ordering_DragonBlue_Commercial.Count > 0)
            {

                string printerFilePathA = Application.StartupPath + "\\Output\\" + _checkModel.Ordering_DragonBlue_Commercial[0].outputFolder + "\\RCBC" + _mainForm.batchFile.Substring(0, 4) + "DBC.txt";


                if (File.Exists(printerFilePathA))
                    File.Delete(printerFilePathA);

                file = File.CreateText(printerFilePathA);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                {
                    string output = ConvertToPrinterFile(_checkModel.Ordering_DragonBlue_Commercial);

                    file.WriteLine(output);
                }
            }
            if (_checkModel.Ordering_DragonYellow_Personal.Count > 0)
            {

                string printerFilePathA = Application.StartupPath + "\\Output\\" + _checkModel.Ordering_DragonYellow_Personal[0].outputFolder + "\\RCBC" + _mainForm.batchFile.Substring(0, 4) + "DYP.txt";


                if (File.Exists(printerFilePathA))
                    File.Delete(printerFilePathA);

                file = File.CreateText(printerFilePathA);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                {
                    string output = ConvertToPrinterFile(_checkModel.Ordering_DragonYellow_Personal);

                    file.WriteLine(output);
                }
            }
            if (_checkModel.Ordering_DragonYellow_Personal.Count > 0)
            {

                string printerFilePathA = Application.StartupPath + "\\Output\\" + _checkModel.Ordering_DragonYellow_Personal[0].outputFolder + "\\RCBC" + _mainForm.batchFile.Substring(0, 4) + "DYC.txt";


                if (File.Exists(printerFilePathA))
                    File.Delete(printerFilePathA);

                file = File.CreateText(printerFilePathA);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                {
                    string output = ConvertToPrinterFile(_checkModel.Ordering_DragonYellow_Personal);

                    file.WriteLine(output);
                }
            }
            if (_checkModel.Ordering_Online_Commercial.Count > 0)
            {

                string printerFilePathA = Application.StartupPath + "\\Output\\" + _checkModel.Ordering_Online_Commercial[0].outputFolder + "\\RCBC" + _mainForm.batchFile.Substring(0, 4) + "DYP.txt";


                if (File.Exists(printerFilePathA))
                    File.Delete(printerFilePathA);

                file = File.CreateText(printerFilePathA);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                {
                    string output = ConvertToPrinterFile(_checkModel.Ordering_Online_Commercial);

                    file.WriteLine(output);
                }
            }
            if (_checkModel.Ordering_Reca_Commercial.Count > 0)
            {

                string printerFilePathA = Application.StartupPath + "\\Output\\" + _checkModel.Ordering_Reca_Commercial[0].outputFolder + "\\RCBC" + _mainForm.batchFile.Substring(0, 4) + "DYP.txt";


                if (File.Exists(printerFilePathA))
                    File.Delete(printerFilePathA);

                file = File.CreateText(printerFilePathA);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                {
                    string output = ConvertToPrinterFile(_checkModel.Ordering_Reca_Commercial);

                    file.WriteLine(output);
                }
            }
        }
        public void PrinterFileManual(TypeofCheckModel _checkModel, frmManualEncode _mainForm)
        {


            StreamWriter file;


            if (_checkModel.Ordering_Customized.Count > 0)
            {

                string printerFilePathA = Application.StartupPath + "\\Output\\" + _checkModel.Ordering_Customized[0].outputFolder + "\\RCBC" + _mainForm.batchFile.Substring(0, 4) + "P.txt";


                if (File.Exists(printerFilePathA))
                    File.Delete(printerFilePathA);

                file = File.CreateText(printerFilePathA);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                {
                    string output = ConvertToPrinterFile(_checkModel.Ordering_Customized);

                    file.WriteLine(output);
                }
            }
        }
        private static string GenerateSpace(int _noOfSpaces)
        {
            string output = "";

            for (int x = 0; x < _noOfSpaces; x++)
            {
                output += " ";
            }

            return output;

        }//END OF FUNCTION
        public static string ConvertToBlockText(List<OrderingModel> _check, string _prodType, string _ChkType, string _batchNumber, DateTime _deliveryDate, string _preparedBy, frmOrdering _mainForm)

        {

            int page = 1, lineCount = 14, blockCounter = 1, blockContent = 1;
            string date = DateTime.Now.ToString("MMM. dd, yyyy");
            bool noFooter = true;
            ///string countText = "";
            string output = "";
            string txtFileName = "";
            //Sort Check List
            var sort = (from c in _check
                        orderby c.BRSTN
                        ascending
                        select c).ToList();

            output += "\r\n" + GenerateSpace(8) + "Page No. " + page.ToString() + "\r\n" +
            GenerateSpace(8) + date +
            "\r\n";

            output += GenerateSpace(27) + "SUMMARY OF BLOCK - " + _ChkType.ToUpper() + "\r\n" +
              GenerateSpace(30) + "RCBC " + _prodType + "\r\n";

            output += GenerateSpace(8) + "BLOCK RT_NO" + GenerateSpace(5) + "M ACCT_NO" + GenerateSpace(9) + "START_NO." + GenerateSpace(2) + "END_NO.\r\n\r\n";
            //            Int64 checkTypeCount = 0;
            foreach (var check in sort)
            {

                if (check.ChkType == "A" && check.Style == 1)
                    txtFileName = "RCBC" + _mainForm.batchFile.Substring(0, 4) + "P";
                else if (check.ChkType == "B" && check.Style == 1)
                    txtFileName = "RCBC" + _mainForm.batchFile.Substring(0, 4) + "C";
                else if (check.ChkType == "A" && check.Style == 9)
                    txtFileName = "RCBC" + _mainForm.batchFile.Substring(0, 4) + "DBP";
                else if (check.ChkType == "B" && check.Style == 8)
                    txtFileName = "RCBC" + _mainForm.batchFile.Substring(0, 4) + "DBC";
                else if (check.ChkType == "B" && check.Style == 5)
                    txtFileName = "RCBC" + _mainForm.batchFile.Substring(0, 4) + "RC";
                else if (check.ChkType == "B" && check.Style == 10)
                    txtFileName = "RCBC" + _mainForm.batchFile.Substring(0, 4) + "OC";
                else if (check.ChkType == "A" && check.Style == 6)
                    txtFileName = "RCBC" + _mainForm.batchFile.Substring(0, 4) + "DYP";
                else if (check.ChkType == "B" && check.Style == 7)
                    txtFileName = "RCBC" + _mainForm.batchFile.Substring(0, 4) + "DYC";
                
                //if (_ChkType == "PERSONAL")
                //{
                //    checkTypeCount = check.Quantity;
                //    while (check.StartingSerial.Length < 7)
                //        check.StartingSerial = "0" + check.StartingSerial;

                //    while (check.EndingSerial.Length < 7)
                //        check.EndingSerial = "0" + check.EndingSerial;
                //}
                //else
                //{

                while (check.StartingSerial.Length < 10)
                    check.StartingSerial = "0" + check.StartingSerial;

                while (check.EndingSerial.Length < 10)
                    check.EndingSerial = "0" + check.EndingSerial;
                // }


                if (blockContent == 1)
                {
                    output += "\r\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\r\n";
                    lineCount += 2;
                }

                if (blockContent == 5)
                {
                    blockContent = 2;

                    blockCounter++;

                    output += "\r\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\r\n";

                    output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                    GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\r\n";
                }
                else
                {
                    output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                    GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\r\n";

                    lineCount += 1;

                    blockContent++;
                }
            }
            //if (lineCount >=61 )
            //{
            if (noFooter) //ADD FOOTER
            {
                output += "\r\n " + _batchNumber + GenerateSpace(46) + "DLVR: " + _deliveryDate.ToString("MM-dd(ddd)") + "\r\n\r\n";
                if (_ChkType == "PERSONAL")
                {
                    var chk = _check.Where(x => x.ChkType == "A").ToList();
                    output += "  A = " + chk.Count() + GenerateSpace(20) + txtFileName + ".txt\r\n";
                    
                }
                else
                {
                    var chk = _check.Where(x => x.ChkType == "B").ToList();
                    output += "  B = " + chk.Count() + GenerateSpace(20) + txtFileName + ".txt\r\n";
                }

                 output +=   GenerateSpace(4) + "Prepared By" + GenerateSpace(3) + ": " + _preparedBy + "\t\t\t\t RECHECKED BY:\r\n" +
                    GenerateSpace(4) + "Updated By" + GenerateSpace(4) + ": " + _preparedBy + "\r\n" +
                    GenerateSpace(4) + "Time Start" + GenerateSpace(4) + ": " + DateTime.Now.ToShortTimeString() + "\r\n" +
                    GenerateSpace(4) + "Time Finished :\r\n" +
                    GenerateSpace(4) + "File rcvd" + GenerateSpace(5) + ":\r\n";

                noFooter = false;
            }

             output += Seperator();

            lineCount = 1;
            //}

            return output;

        }// end of function
        public static string ConvertToBlockTextManual(List<OrderingModel> _check, string _prodType, string _ChkType, string _batchNumber, DateTime _deliveryDate, string _preparedBy, frmManualEncode _mainForm)

        {

            int page = 1, lineCount = 14, blockCounter = 1, blockContent = 1;
            string date = DateTime.Now.ToString("MMM. dd, yyyy");
            bool noFooter = true;
            ///string countText = "";
            string output = "";
            string txtFileName = "";
            //Sort Check List
            var sort = (from c in _check
                        orderby c.BRSTN
                        ascending
                        select c).ToList();

            output += "\r\n" + GenerateSpace(8) + "Page No. " + page.ToString() + "\r\n" +
            GenerateSpace(8) + date +
            "\r\n";

            output += GenerateSpace(27) + "SUMMARY OF BLOCK - " + _ChkType.ToUpper() + "\r\n" +
              GenerateSpace(30) + "RCBC " + _prodType + "\r\n";

            output += GenerateSpace(8) + "BLOCK RT_NO" + GenerateSpace(5) + "M ACCT_NO" + GenerateSpace(9) + "START_NO." + GenerateSpace(2) + "END_NO.\r\n\r\n";
            //            Int64 checkTypeCount = 0;
            foreach (var check in sort)
            {

                if (check.ChkType == "A" && check.Style == 1)
                    txtFileName = "RCBC" + _mainForm.batchFile.Substring(0, 4) + "P";
                else if (check.ChkType == "B" && check.Style == 1)
                    txtFileName = "RCBC" + _mainForm.batchFile.Substring(0, 4) + "C";
                else if (check.ChkType == "A" && check.Style == 9)
                    txtFileName = "RCBC" + _mainForm.batchFile.Substring(0, 4) + "DBP";
                else if (check.ChkType == "B" && check.Style == 8)
                    txtFileName = "RCBC" + _mainForm.batchFile.Substring(0, 4) + "DBC";
                else if (check.ChkType == "B" && check.Style == 5)
                    txtFileName = "RCBC" + _mainForm.batchFile.Substring(0, 4) + "RC";
                else if (check.ChkType == "B" && check.Style == 10)
                    txtFileName = "RCBC" + _mainForm.batchFile.Substring(0, 4) + "OC";
                else if (check.ChkType == "A" && check.Style == 6)
                    txtFileName = "RCBC" + _mainForm.batchFile.Substring(0, 4) + "DYP";
                else if (check.ChkType == "B" && check.Style == 7)
                    txtFileName = "RCBC" + _mainForm.batchFile.Substring(0, 4) + "DYC";

                //if (_ChkType == "PERSONAL")
                //{
                //    checkTypeCount = check.Quantity;
                //    while (check.StartingSerial.Length < 7)
                //        check.StartingSerial = "0" + check.StartingSerial;

                //    while (check.EndingSerial.Length < 7)
                //        check.EndingSerial = "0" + check.EndingSerial;
                //}
                //else
                //{

                while (check.StartingSerial.Length < 10)
                    check.StartingSerial = "0" + check.StartingSerial;

                while (check.EndingSerial.Length < 10)
                    check.EndingSerial = "0" + check.EndingSerial;
                // }


                if (blockContent == 1)
                {
                    output += "\r\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\r\n";
                    lineCount += 2;
                }

                if (blockContent == 5)
                {
                    blockContent = 2;

                    blockCounter++;

                    output += "\r\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\r\n";

                    output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                    GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\r\n";
                }
                else
                {
                    output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                    GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\r\n";

                    lineCount += 1;

                    blockContent++;
                }
            }
            //if (lineCount >=61 )
            //{
            if (noFooter) //ADD FOOTER
            {
                output += "\r\n " + _batchNumber + GenerateSpace(46) + "DLVR: " + _deliveryDate.ToString("MM-dd(ddd)") + "\r\n\r\n";
                if (_ChkType == "PERSONAL")
                {
                    var chk = _check.Where(x => x.ChkType == "A").ToList();
                    output += "  A = " + chk.Count() + GenerateSpace(20) + txtFileName + ".txt\r\n";

                }
                else
                {
                    var chk = _check.Where(x => x.ChkType == "B").ToList();
                    output += "  B = " + chk.Count() + GenerateSpace(20) + txtFileName + ".txt\r\n";
                }

                output += GenerateSpace(4) + "Prepared By" + GenerateSpace(3) + ": " + _preparedBy + "\t\t\t\t RECHECKED BY:\r\n" +
                   GenerateSpace(4) + "Updated By" + GenerateSpace(4) + ": " + _preparedBy + "\r\n" +
                   GenerateSpace(4) + "Time Start" + GenerateSpace(4) + ": " + DateTime.Now.ToShortTimeString() + "\r\n" +
                   GenerateSpace(4) + "Time Finished :\r\n" +
                   GenerateSpace(4) + "File rcvd" + GenerateSpace(5) + ":\r\n";

                noFooter = false;
            }

             output += Seperator();

            lineCount = 1;
            //}

            return output;

        }// end of function
        public static string ConvertToPackingList(List<OrderingModel> _checks, string _checkType, frmOrdering _mainForm)
        {
            var listofbrstn = _checks.Select(e => e.BRSTN).Distinct().ToList();
            int page = 1;
            string date = DateTime.Now.ToShortDateString();
            string output = "";
            int i = 0;

            for (int x = 0; x < listofbrstn.Count; x++)
            {

            
                //foreach (string brstn in listofbrstn)
           // {

                output += "\r\n Page No. " + page.ToString() + "\r\n " +
                                  date + "\r\n" +
                                  GenerateSpace(29) + "CAPTIVE PRINTING CORPORATION\r\n" +
                                  GenerateSpace(28) + "RCBC - " + _checkType + " Checks Summary\r\n\r\n" +
                                  GenerateSpace(2) + "ACCT_NO" + GenerateSpace(9) + "ACCOUNT NAME" + GenerateSpace(21) + "QTY CT START #" + GenerateSpace(4) + "END #\r\n\r\n\r\n";

                
                var listofchecks = _checks.Where(e => e.BRSTN == listofbrstn[x]).ToList();
                
                output += "  ** DELIVERY TO: " + listofchecks[0].DeliveryBrstn + " " + listofchecks[0].DeliveryBranch.ToUpper().TrimEnd() + " ("+listofchecks[0].DeliveryBranchCode+")\r\n\r\n";
                output += "  ** ORDERS OF BRSTN: " + listofchecks[0].BRSTN + " " + listofchecks[0].BranchName.TrimEnd() + " (" + listofchecks[0].BranchCode + ")\r\n\r\n";
                
                if(listofchecks[0].StartingSerial.Substring(4,6) == "000001")
                output += " *  BATCH #: " + _mainForm.batchFile  + "_Pre-Encoded" + "\r\n\r\n";
                else
                    output += " * Re-Order BATCH #: " + _mainForm.batchFile + "_Re-Order" + "\r\n\r\n";


                foreach (var check in listofchecks)
                {
                    i++;
                    //if (_checkType == "PERSONAL")
                    //{
                    //    while (check.StartingSerial.Length < 7)
                    //        check.StartingSerial = "0" + check.StartingSerial;

                    //    while (check.EndingSerial.Length < 7)
                    //        check.EndingSerial = "0" + check.EndingSerial;
                    //}
                    //else
                    //{
                    while (check.StartingSerial.Length < 10)
                        check.StartingSerial = "0" + check.StartingSerial;

                    while (check.EndingSerial.Length < 10)
                        check.EndingSerial = "0" + check.EndingSerial;
                    //}//END OF ADDING ZERO IN SERIES NUMBER

                    output += GenerateSpace(2) + check.AccountNo.Substring(0, 1) + "-" + check.AccountNo.Substring(1, 3) + "-"
                        + check.AccountNo.Substring(4, 5) + "-" + check.AccountNo.Substring(9, 1) + GenerateSpace(4);

                    if (check.AccountName.TrimEnd().Length < 61)
                        output += check.AccountName.TrimEnd() + GenerateSpace(60 - check.AccountName.TrimEnd().Length);

                    output += "  1 " + check.ChkType + GenerateSpace(2) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\r\n";

                    if (check.AccountName2 != "")
                        output += GenerateSpace(19) + check.AccountName2 + GenerateSpace(60 - check.AccountName2.TrimEnd().Length) + "\r\n";


                    if (i >= 50)
                    {
                        output += Seperator();
                        i = 0;
                    }

                }

                output += "\r\n";
                output += "  * * * Sub Total * * * " + listofchecks.Count + "\r\n\r\n";

                if(x < (listofbrstn.Count - 1))
                output += Seperator();

                page++;
                //i++;
               
            }
            

            output += "  * * * Grand Total * * * " + _checks.Count + "\r\n";
            return output;

        }// end of function
        public static string ConvertToPackingListManual(List<OrderingModel> _checks, string _checkType, frmManualEncode _mainForm)
        {
            var listofbrstn = _checks.Select(e => e.BRSTN).Distinct().ToList();
            int page = 1;
            string date = DateTime.Now.ToShortDateString();
            string output = "";
            int i = 0;

            foreach (string brstn in listofbrstn)
            {

                output += "\r\n Page No. " + page.ToString() + "\r\n " +
                                  date + "\r\n" +
                                  GenerateSpace(29) + "CAPTIVE PRINTING CORPORATION\r\n" +
                                  GenerateSpace(28) + "RCBC - " + _checkType + " Checks Summary\r\n\r\n" +
                                  GenerateSpace(2) + "ACCT_NO" + GenerateSpace(9) + "ACCOUNT NAME" + GenerateSpace(21) + "QTY CT START #" + GenerateSpace(4) + "END #\r\n\r\n\r\n";


                var listofchecks = _checks.Where(e => e.BRSTN == brstn).ToList();
                //output += "  ** DELIVERY TO: " + listofchecks[0].DeliveryBrstn + " " + listofchecks[0].DeliveryBranch.ToUpper().TrimEnd() + " (" + listofchecks[0].DeliveryBranchCode + ")\r\n\r\n";
                output += "  ** ORDERS OF BRSTN: " + listofchecks[0].BRSTN + " " + listofchecks[0].BranchName.TrimEnd() + " (" + listofchecks[0].BranchCode + ")\r\n\r\n" +
                              " * BATCH #: " + _mainForm.batchFile + "\r\n\r\n";



                foreach (var check in listofchecks)
                {

                    //if (_checkType == "PERSONAL")
                    //{
                    //    while (check.StartingSerial.Length < 7)
                    //        check.StartingSerial = "0" + check.StartingSerial;

                    //    while (check.EndingSerial.Length < 7)
                    //        check.EndingSerial = "0" + check.EndingSerial;
                    //}
                    //else
                    //{
                    while (check.StartingSerial.Length < 10)
                        check.StartingSerial = "0" + check.StartingSerial;

                    while (check.EndingSerial.Length < 10)
                        check.EndingSerial = "0" + check.EndingSerial;
                    //}//END OF ADDING ZERO IN SERIES NUMBER

                    output += GenerateSpace(2) + check.AccountNo.Substring(0, 1) + "-" + check.AccountNo.Substring(1, 3) + "-"
                        + check.AccountNo.Substring(4, 5) + "-" + check.AccountNo.Substring(9, 1) + GenerateSpace(4);

                    if (check.AccountName.TrimEnd().Length < 61)
                        output += check.AccountName.TrimEnd() + GenerateSpace(60 - check.AccountName.TrimEnd().Length);

                    output += "  1 " + check.ChkType + GenerateSpace(2) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\r\n";

                    if (check.AccountName2 != "")
                        output += GenerateSpace(19) + check.AccountName2 + GenerateSpace(60 - check.AccountName2.TrimEnd().Length) + "\r\n";




                }

                output += "\r\n";
                output += "  * * * Sub Total * * * " + listofchecks.Count + "\r\n\r\n";

                page++;
                i++;

            }
            output += Seperator();
            output += "  * * * Grand Total * * * " + _checks.Count + "\r\n";
            return output;

        }// end of function
        public static string ConvertToPrinterFile(List<OrderingModel> _checkModels)
        {

            //var listofcheck = _checkModel.Select(e => e.BRSTN).OrderBy(e => e).ToList();

            string output = "";
            var sort = (from c in _checkModels
                        orderby c.BRSTN, c.AccountNo
                        ascending
                        select c).ToList();


            foreach (var check in sort)
            {
                Int64 Series = 0;
                if (check.ChkType == "B")
                {
                    Series = Int64.Parse(check.StartingSerial) + 100;
                }
                else
                {
                    Series = Int64.Parse(check.StartingSerial) + 50;
                }
                Int64 endSeries = Series - 1;

                string outputStartSeries = check.StartingSerial.ToString();

                string outputEndSeries = endSeries.ToString();
                string Sseries = Series.ToString();

                //   string brstnFormat = "";

                string txtSeries = Series.ToString();

                //if (check.ChkType == "A")
                //{
                //    while (check.StartingSerial.Length < 7)
                //    {
                //        check.StartingSerial = "0" + check.StartingSerial;
                //        Sseries = "0" + Sseries;
                //    }
                //    while (check.EndingSerial.Length < 7)
                //        check.EndingSerial = "0" + check.EndingSerial;
                //}
                //else
                //{
                    while (check.StartingSerial.Length < 10)
                    {
                        check.StartingSerial = "0" + check.StartingSerial;
                        Sseries = "0" + Sseries;
                    }
                    while (check.EndingSerial.Length < 10)
                    check.EndingSerial = "0" + check.EndingSerial;
                //   }
                while (Sseries.Length <10)
                {
                    Sseries = "0" + Sseries;
                }

                output += "10\r\n" + //1 (FIXED)
                         check.BRSTN + "\r\n" + //2  (BRSTN)                                             
                         check.AccountNo + "\r\n" + //3 (ACCT NUMBER)                                              
                         Sseries + "\r\n" + //4 (Start Series + PCS per Book)                                                    
                         check.ChkType + "\r\n" + //5                                             
                         "\r\n" + //6 (BLANK)                           
                         check.BRSTN.Substring(0, 5) + "\r\n" +//7 BRSTN FORMATTED
                         " " + check.BRSTN.Substring(5, 4) + "\r\n" + //8 BRSTN FORMATTED                                                                    
                         check.AccountNo.Substring(0, 1) + "-" + check.AccountNo.Substring(1, 3) + "-"
                            + check.AccountNo.Substring(4, 5) + "-" + check.AccountNo.Substring(9, 1) + "\r\n" + //9 (ACCT NUMBER)                
                         check.AccountName + "\r\n" + //10 (NAME 1)           
                         "SN\r\n" + //11 (FIXED)           
                         "\r\n" + //12 (BLANK)                
                         check.AccountName2 + "\r\n" + //13 (NAME 2)                
                         "\r\n" + //14  (BLANK)   
                         "\r\n" + //15 (BLANK)          
                         "\r\n" + //16 (BLANK)           
                         check.BranchName + "\r\n" + //17 (ADDRESS 1)                
                         check.Address + "\r\n" + //18 (ADDRESS 2)  
                         check.Address2 + "\r\n" + //19 (ADDRESS 3)                            
                         check.Address3 + "\r\n" + //20 (ADDRESS 4)
                         check.Address4 + "\r\n" + //21 (ADDRESS 5)
                         check.Address5 + "\r\n" + //22 (ADDRESS 6)
                         gClient.Description.ToUpper() +"\r\n" +//23 (FIXED)
                         "\r\n" + //24 (BLANK)//   
                         "\r\n" + //25 (BLANK)  
                         "\r\n" + //26 (BLANK)
                         "\r\n" + //27 (BLANK)                         
                         "\r\n" + //28 (BLANK)               
                         "\r\n" + //29 (BLANK)                  
                         "\r\n" + //30 (BLANK)                                        
                         check.StartingSerial + "\r\n" + //31 (STARTING SERIES)               
                         check.EndingSerial + "\r\n";  //32 (ENDING SERIES)

            }

            return output;
        }//end of function
        public static string ConvertToPrinterFileManual(List<OrderingModel> _checkModels)
        {

            //var listofcheck = _checkModel.Select(e => e.BRSTN).OrderBy(e => e).ToList();

            string output = "";
            var sort = (from c in _checkModels
                        orderby c.BRSTN, c.AccountNo
                        ascending
                        select c).ToList();


            foreach (var check in sort)
            {
                Int64 Series = 0;
                if (check.ChkType == "B")
                {
                    Series = Int64.Parse(check.StartingSerial) + 100;
                }
                else
                {
                    Series = Int64.Parse(check.StartingSerial) + 50;
                }
                Int64 endSeries = Series - 1;

                string outputStartSeries = check.StartingSerial.ToString();

                string outputEndSeries = endSeries.ToString();

                //   string brstnFormat = "";

                string txtSeries = Series.ToString();

                //if (check.ChkType == "A")
                //{
                //    while (check.StartingSerial.Length < 7)
                //        check.StartingSerial = "0" + check.StartingSerial;

                //    while (check.EndingSerial.Length < 7)
                //        check.EndingSerial = "0" + check.EndingSerial;
                //}
                //else
                //{
                while (check.StartingSerial.Length < 10)
                    check.StartingSerial = "0" + check.StartingSerial;

                while (check.EndingSerial.Length < 10)
                    check.EndingSerial = "0" + check.EndingSerial;
                //}
                while (txtSeries.Length < 10)
                {
                    txtSeries = "0" + txtSeries;
                }
                output += "10\r\n" + //1 (FIXED)
                         check.BRSTN + "\r\n" + //2  (BRSTN)                                             
                         check.AccountNo + "\r\n" + //3 (ACCT NUMBER)                                              
                         txtSeries + "\r\n" + //4 (Start Series + PCS per Book)                                                    
                         check.ChkType + "\r\n" + //5                                             
                         "\r\n" + //6 (BLANK)                           
                         check.BRSTN.Substring(0, 5) + "\r\n" +//7 BRSTN FORMATTED
                         " " + check.BRSTN.Substring(5, 4) + "\r\n" + //8 BRSTN FORMATTED                                                                    
                         check.AccountNo.Substring(0, 1) + "-" + check.AccountNo.Substring(1, 3) + "-"
                            + check.AccountNo.Substring(4, 5) + "-" + check.AccountNo.Substring(9, 1) + "\r\n" + //9 (ACCT NUMBER)                
                         check.AccountName + "\r\n" + //10 (NAME 1)           
                         "SN\r\n" + //11 (FIXED)           
                         "\r\n" + //12 (BLANK)                
                         check.AccountName2 + "\r\n" + //13 (NAME 2)                
                         "\r\n" + //14  (BLANK)   
                         "\r\n" + //15 (BLANK)          
                         "\r\n" + //16 (BLANK)           
                         check.BranchName + "\r\n" + //17 (ADDRESS 1)                
                         check.Address + "\r\n" + //18 (ADDRESS 2)  
                         check.Address2 + "\r\n" + //19 (ADDRESS 3)                            
                         check.Address3 + "\r\n" + //20 (ADDRESS 4)
                         check.Address4 + "\r\n" + //21 (ADDRESS 5)
                         check.Address5 + "\r\n" + //22 (ADDRESS 6)
                         gClient.Description.ToUpper() + "\r\n" +//23 (FIXED)
                         "\r\n" + //24 (BLANK)//   
                         "\r\n" + //25 (BLANK)  
                         "\r\n" + //26 (BLANK)
                         "\r\n" + //27 (BLANK)                         
                         "\r\n" + //28 (BLANK)               
                         "\r\n" + //29 (BLANK)                  
                         "\r\n" + //30 (BLANK)                                        
                         check.StartingSerial + "\r\n" + //31 (STARTING SERIES)               
                         check.EndingSerial + "\r\n";  //32 (ENDING SERIES)

            }

            return output;
        }//end of function
        public bool getOrderingBranches(List<BranchesModel> _branches)
        {
            try
            {
                DBConnect();
                Sql = "Select  BRSTN,BranchName,Address,Address2,Address3,Address4,Address5,BranchCode,LastSeriesA,LastSeriesB from " + gClient.BranchesTable;
                cmd = new MySqlCommand(Sql, myConnect);
                MySqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    BranchesModel b = new BranchesModel
                    {
                        BRSTN = !reader.IsDBNull(0) ? reader.GetString(0) : "",
                        Address1 = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                        Address2 = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                        Address3 = !reader.IsDBNull(3) ? reader.GetString(3) : "",
                        Address4 = !reader.IsDBNull(4) ? reader.GetString(4) : "",
                        Address5 = !reader.IsDBNull(5) ? reader.GetString(5) : "",
                        Address6 = !reader.IsDBNull(6) ? reader.GetString(6) : "",
                        BranchCode = !reader.IsDBNull(7) ? reader.GetString(7) : "",
                        LastSeriesA = !reader.IsDBNull(8) ? reader.GetString(8) : "",
                        LastSeriesB = !reader.IsDBNull(9) ? reader.GetString(9) : ""
                    };

                    _branches.Add(b);
                }

                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"getOrderingBranches ",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
        }
        public bool fAddOrUPdate(BranchesModel _branch,int _process)
        {
            try
            {
                DBConnect();
                if(_process == 1)
                {
                    Sql = "Insert into " + gClient.BranchesTable + "(BRSTN,BranchName,Address,Address2,Address3,Address4,Address5,BranchCode)" +
                            "values('" + _branch.BRSTN + "','" + _branch.Address1.Replace("'","''") + "','" + _branch.Address2.Replace("'", "''") + "'," +
                            "'" + _branch.Address3.Replace("'", "''") + "','" + _branch.Address4.Replace("'", "''") + "','" + _branch.Address5.Replace("'", "''")+ "'," +
                            "'" + _branch.Address6.Replace("'", "''") + "','" + _branch.BranchCode + "'); ";
                }
                else if(_process == 2)
                {
                    Sql = "Update " + gClient.BranchesTable + " set BranchName = '"+ _branch.Address1.Replace("'", "''") + "', Address = '" + _branch.Address2.Replace("'", "''") + "'," +
                        "Address2  = '" + _branch.Address3.Replace("'", "''") + "', Address3 = '" + _branch.Address4.Replace("'", "''")+ "', Address4 = '" + _branch.Address5.Replace("'", "''") + "'," +
                        "Address5 = '" + _branch.Address6.Replace("'", "''") + "', BranchCode = '"+ _branch.BranchCode + "' where BRSTN = '" + _branch.BRSTN + "';" ;
                }
                cmd = new MySqlCommand(Sql, myConnect);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data has been Saved Successfully", "Add or Update Branch!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DBClosed();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Add or Update Branch!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            

        }
        public void Process(List<OrderingModel> _orders, frmOrdering _main,string _outputFolder)
        {

            TypeofCheckModel checkType = new TypeofCheckModel();
            checkType.Ordering_Regular_Personal = new List<OrderingModel>();
            checkType.Ordering_Regular_Commercial = new List<OrderingModel>();
            checkType.Ordering_DragonBlue_Personal = new List<OrderingModel>();
            checkType.Ordering_DragonBlue_Commercial = new List<OrderingModel>();
            checkType.Ordering_DragonYellow_Personal = new List<OrderingModel>();
            checkType.Ordering_DragonYellow_Commercial = new List<OrderingModel>();
            checkType.Ordering_Online_Commercial = new List<OrderingModel>();
            checkType.Ordering_Reca_Commercial = new List<OrderingModel>();
            
        
            _orders.ForEach(x =>
                {
                    if (x.ChkType == "A" &&  x.Style == 1)
                    {
                        checkType.Ordering_Regular_Personal.Add(x);
                        
                        DoBlockProcess(checkType, _main, _outputFolder);
                        PackingText(checkType, _main, _outputFolder);
                        //SaveToPackingDBF(checkType, _main.batchfile, _main);
                        PrinterFile(checkType, _main);
                    }
                    else if (x.ChkType == "B" && x.Style == 1)
                    {

                        checkType.Ordering_Regular_Commercial.Add(x);
                        DoBlockProcess(checkType, _main, _outputFolder);
                        PackingText(checkType, _main, _outputFolder);
                        //SaveToPackingDBF(checkType, _main.batchfile, _main);
                        PrinterFile(checkType, _main);
                    }
                    else if (x.ChkType == "A" && x.Style == 9)
                    {
                        checkType.Ordering_DragonBlue_Personal.Add(x);
                        DoBlockProcess(checkType, _main, _outputFolder);
                        PackingText(checkType, _main, _outputFolder);
                        //SaveToPackingDBF(checkType, _main.batchfile, _main);
                        PrinterFile(checkType, _main);
                    }
                    else if (x.ChkType == "B" && x.Style == 8)
                    {
                        checkType.Ordering_DragonBlue_Commercial.Add(x);
                        DoBlockProcess(checkType, _main, _outputFolder);
                        PackingText(checkType, _main, _outputFolder);
                        //SaveToPackingDBF(checkType, _main.batchfile, _main);
                        PrinterFile(checkType, _main);
                    }
                    else if (x.ChkType == "A" && x.Style ==6 )
                    {
                        checkType.Ordering_DragonYellow_Personal.Add(x);
                        DoBlockProcess(checkType, _main, _outputFolder);
                        PackingText(checkType, _main, _outputFolder);
                        //SaveToPackingDBF(checkType, _main.batchfile, _main);
                        PrinterFile(checkType, _main);
                    }
                    else if (x.ChkType == "B" && x.Style == 7)
                    {
                        checkType.Ordering_DragonYellow_Commercial.Add(x);
                        DoBlockProcess(checkType, _main, _outputFolder);
                        PackingText(checkType, _main, _outputFolder);
                        //SaveToPackingDBF(checkType, _main.batchfile, _main);
                        PrinterFile(checkType, _main);
                    }
                    else if (x.ChkType == "B" && x.Style == 5)
                    {
                        checkType.Ordering_Reca_Commercial.Add(x);
                        DoBlockProcess(checkType, _main, _outputFolder);
                        PackingText(checkType, _main, _outputFolder);
                        //SaveToPackingDBF(checkType, _main.batchfile, _main);
                        PrinterFile(checkType, _main);
                    }
                    else if (x.ChkType == "B" && x.Style == 10)
                    {
                        checkType.Ordering_Online_Commercial.Add(x);
                        DoBlockProcess(checkType, _main, _outputFolder);
                        PackingText(checkType, _main, _outputFolder);
                        //SaveToPackingDBF(checkType, _main.batchfile, _main);
                        PrinterFile(checkType, _main);
                    }
                   
                    
            });
                
        }
        public void DeleteTextFile(List<OrderingModel> _checks,string _tempPath)
        {
            for (int i = 0; i < _checks.Count; i++)
            {


                DirectoryInfo di = new DirectoryInfo(Application.StartupPath + "\\"+_tempPath+"\\" + _checks[i].outputFolder + "");

                FileInfo[] files = di.GetFiles("*.txt")
                         .Where(p => p.Extension == ".txt").ToArray();
                foreach (FileInfo file in files)
                {
                    file.Attributes = FileAttributes.Normal;
                    File.Delete(file.FullName);
                }
            }
        }
        public void DeleteZipFile()
        {
       

                DirectoryInfo di = new DirectoryInfo(Application.StartupPath);

                FileInfo[] files = di.GetFiles("*.zip")
                         .Where(p => p.Extension == ".zip").ToArray();
                foreach (FileInfo file in files)
                {
                    file.Attributes = FileAttributes.Normal;
                    File.Delete(file.FullName);
                }
            
        }
        public void SaveData(List<OrderingModel> _orderList,string _zipFile)
        {
            try
            {
                DBConnect();
                _orderList.ForEach(x =>
                {
                    Sql = "Insert into " + gClient.DataBaseName + "(Batch,DateProccessed,DeliveryDate,BRSTN,AccountNo,AccountName,AccountName2," +
                        "ChkType,CheckName,StartingSerial,EndingSerial,DeliveryBrstn,DeliveryBranch,DeliveryBranchCode,Status)" +
                        "values('" + x.Batch + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                        "'" + x.DeliveryDate.ToString("yyyy-MM-dd") + "','" + x.BRSTN + "','" + x.AccountNo + "', '" + x.AccountName.Replace("'", "''") +
                        "','" + x.AccountName2.Replace("'", "''") + "','" + x.ChkType + "','" + x.CheckName.Replace("'", "''") + "','" + x.StartingSerial + "'," +
                        "'" + x.EndingSerial + "','" + x.DeliveryBrstn + "','" + x.DeliveryBranch + "','" + x.DeliveryBranchCode + "',1);";
                        

                    cmd = new MySqlCommand(Sql, myConnect);
                    cmd.ExecuteNonQuery();
                });

                //DBClosed();
                //DBConnect();

                //FileStream fs = new FileStream(_zipFile, FileMode.Open, FileAccess.Read);
                //int FileSize = (int)fs.Length;
                //byte[] rawData  = new byte[FileSize];
                //fs.Read(rawData, 0, FileSize);
                //fs.Close();
                //Sql =  "Insert into " + gClient.ZipTable + " (Batch_ID, ZipFile, DateProcessed) values ('" + _orderList[0].Batch + "','"+rawData+"','" +
                //       DateTime.Now.ToString("yyyy-MM-dd") + "');";

                //cmd = new MySqlCommand(Sql, myConnect);
                //cmd.ExecuteNonQuery();
                MessageBox.Show("Data has been Successfully saved!", "Saving Data to Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DBClosed();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Saving Data to Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void SaveDataManual(List<OrderingModel> _orderList, string _zipFile)
        {
            try
            {
                DBConnect();
                _orderList.ForEach(x =>
                {
                    Sql = "Insert into " + gClient.DataBaseName + "(Batch,DateProccessed,DeliveryDate,BRSTN,AccountNo,AccountName,AccountName2," +
                        "ChkType,CheckName,StartingSerial,EndingSerial,Status)" +
                        "values('" + x.Batch + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                        "'" + x.DeliveryDate.ToString("yyyy-MM-dd") + "','" + x.BRSTN + "','" + x.AccountNo + "', '" + x.AccountName.Replace("'", "''") +
                        "','" + x.AccountName2.Replace("'", "''") + "','" + x.ChkType + "','" + x.CheckName.Replace("'", "''") + "','" + x.StartingSerial + "'," +
                        "'" + x.EndingSerial + "',1); ";

                    if (x.CheckName.Contains("TRUBANK"))
                    {
                        if (x.ChkType == "A")
                        {
                            Sql += " Update " + gClient.BranchesTable + " set LastSeriesA = '" + x.EndingSerial + "' where BRSTN = '" + x.BRSTN + "'";
                        }
                        else
                        {
                            Sql += " Update " + gClient.BranchesTable + " set LastSeriesB = '" + x.EndingSerial + "' where BRSTN = '" + x.BRSTN + "'";
                        }
                    }

                    cmd = new MySqlCommand(Sql, myConnect);
                    cmd.ExecuteNonQuery();

                    
                });

                //DBClosed();
                //DBConnect();

                //FileStream fs = new FileStream(_zipFile, FileMode.Open, FileAccess.Read);
                //int FileSize = (int)fs.Length;
                //byte[] rawData  = new byte[FileSize];
                //fs.Read(rawData, 0, FileSize);
                //fs.Close();
                //Sql =  "Insert into " + gClient.ZipTable + " (Batch_ID, ZipFile, DateProcessed) values ('" + _orderList[0].Batch + "','"+rawData+"','" +
                //       DateTime.Now.ToString("yyyy-MM-dd") + "');";

                //cmd = new MySqlCommand(Sql, myConnect);
                //cmd.ExecuteNonQuery();
                MessageBox.Show("Data has been Successfully saved!", "Saving Data to Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DBClosed();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Saving Data to Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void OpenFile()
        {
            string strCmdText;
            Process process = new Process();
            strCmdText = "/C type C:\\Head\\*.txt > combined.txt " ;
            process.StartInfo = new ProcessStartInfo("cmd.exe", strCmdText);

            process.Start();
            //process.WaitForExit();
            process.Close();
        }
        public void CreateZipFile(string _sourcePath, string _destinationPath)
        {

            ZipFile.CreateFromDirectory(_sourcePath, _destinationPath);
        }
        public void ExtractZipFile(string sourcePath, string destinationPath)
        {

            ZipFile.ExtractToDirectory(sourcePath, destinationPath);
        }
        public string ZipFileS(string _processby, frmOrdering main,List<OrderingModel> _ordrList)
        {
            DeleteZipFile();
            string sPath = Application.StartupPath + "\\Output\\";
            string zPath = Application.StartupPath + "\\Test\\";
            string dPath = Application.StartupPath + "\\AFT_" + main.batchFile + "_" + _processby + ".zip";
            //DeleteZipFile();
            //deleting existing file
            if (File.Exists(dPath))
                File.Delete(dPath);
            //create zip file



            //  DeleteTextFileinZipFile(_ordrList);
            DeleteTextFile(_ordrList, "Test");
            var orders = _ordrList.Select(x => x.outputFolder).Distinct().ToList();
            for (int i = 0; i < orders.Count; i++)
            {
                
                if (Directory.Exists(zPath + orders[i]))
                    Directory.Delete(zPath + orders[i]);


                Directory.CreateDirectory(zPath + orders[i]);
                // Get the subdirectories for the specified directory.
                DirectoryInfo dir = new DirectoryInfo(sPath+ orders[i]);
                DirectoryInfo[] dirs = dir.GetDirectories();
             
                FileInfo[] files = dir.GetFiles();
                foreach(FileInfo zfileitem in files)
                {
                    string tempPath = Path.Combine(zPath + orders[i], zfileitem.Name);
                    zfileitem.CopyTo(tempPath, false);
                }
                
                
            }
            CreateZipFile(zPath, dPath);

            Ionic.Zip.ZipFile zips = new Ionic.Zip.ZipFile(dPath);
            //Adding order file to zip file
            zips.AddItem("C:\\Head");
            zips.Save();

            //DeleteSQl();
            return dPath;

        }
        public void DeleteTextFileinZipFile(List<OrderingModel> _checks,string _zip)
        {
            for (int i = 0; i < _checks.Count; i++)
            {

                FileStream zfile = new FileStream(_zip, FileMode.Open);
                ZipArchive zip = new ZipArchive(zfile, ZipArchiveMode.Update);
                foreach (var item in zip.Entries)
                {
                   // if(item.)
                }

                DirectoryInfo di = new DirectoryInfo(Application.StartupPath + "\\Output\\" + _checks[i].outputFolder + "");

                FileInfo[] files = di.GetFiles("*.txt")
                         .Where(p => p.Extension == ".txt").ToArray();
                foreach (FileInfo file in files)
                {
                    file.Attributes = FileAttributes.Normal;
                    File.Delete(file.FullName);
                }
            }
        }
        public bool getDatafromOrdering(List<OrderModel> _tempList, string _batch)
        {
            try
            {
                con = new MySqlConnection(ConString);
                Sql = "Select Batch,DeliveryDate,H.BRSTN,AccountNo,AccountName,AccountName2,ChkType,CheckName,StartingSerial,EndingSerial" +
                    ",DeliveryBrstn,DeliveryBranch,BranchName,Address,Address2,Address3,Address4,Address5,BranchCode,DeliveryBranchCode " +
                    "  from " +gClient.DataBaseName + " as H  inner join "+gClient.BranchesTable + " as B on H.BRSTN = B.BRSTN  where Batch ='" + _batch+"'";
                con.Open();
                cmd = new MySqlCommand(Sql, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    OrderModel order = new OrderModel
                    {
                        Batch = !reader.IsDBNull(0) ? reader.GetString(0): "",
                        DeliveryDate = !reader.IsDBNull(1) ? reader.GetDateTime(1) : DateTime.Now,
                        BRSTN = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                        AccountNo = !reader.IsDBNull(3) ? reader.GetString(3) : "",
                        Name1 = !reader.IsDBNull(4) ? reader.GetString(4) : "",
                        Name2 = !reader.IsDBNull(5) ? reader.GetString(5) : "",
                        ChkType = !reader.IsDBNull(6) ? reader.GetString(6) : "",
                        ChequeName = !reader.IsDBNull(7) ? reader.GetString(7): "",
                        StartingSerial =!reader.IsDBNull(8) ? reader.GetString(8) : "",
                        EndingSerial = !reader.IsDBNull(9) ? reader.GetString(9) : "",
                        DeliveryTo = !reader.IsDBNull(10) ? reader.GetString(10) : "",
                        DeliverytoBranch = !reader.IsDBNull(11) ? reader.GetString(11) : "",
                        BranchName = !reader.IsDBNull(12) ? reader.GetString(12) : "",
                        Address2 = !reader.IsDBNull(13) ? reader.GetString(13) : "",
                        Address3 = !reader.IsDBNull(14) ? reader.GetString(14) : "",
                        Address4 = !reader.IsDBNull(15) ? reader.GetString(15) : "",
                        Address5 = !reader.IsDBNull(16) ? reader.GetString(16) : "",
                        Address6 = !reader.IsDBNull(17) ? reader.GetString(17) : "",
                        BranchCode = !reader.IsDBNull(18) ? reader.GetString(18) : "",
                        OldBranchCode = !reader.IsDBNull(19) ? reader.GetString(19): "" 
                    };

                    _tempList.Add(order);
                }
                reader.Close();
                con.Close();
                if(_tempList.Count > 0)

                return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "getDatafromOrdering ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
}
        public void ManualProcess(List<OrderingModel> _orders, frmManualEncode _main, string _outputFolder)
        {
            TypeofCheckModel checkType = new TypeofCheckModel();
            checkType.Ordering_Customized = new List<OrderingModel>();
            _orders.ForEach(x =>
            {
                checkType.Ordering_Customized.Add(x);
                DoBlockProcessManual(checkType, _main, _outputFolder);
                PackingTextManual(checkType, _main, _outputFolder);
                //SaveToPackingDBF(checkType, _main.batchfile, _main);
                PrinterFileManual(checkType, _main);
            });
        }
        public void SaveDataToAccounting(List<OrderingModel> _orderList, string _zipFile)
        {
            try
            {
                MySqlConnection con;
                string conString = ConfigurationManager.AppSettings["ConnectionStringOrdering"];
                con = new MySqlConnection(conString);
                con.Open();
                _orderList.ForEach(x =>
                {
                    Sql = "Insert into  (Batch,DateProccessed,DeliveryDate,BRSTN,AccountNo,AccountName,AccountName2," +
                        "ChkType,CheckName,StartingSerial,EndingSerial,DeliveryBrstn,DeliveryBranch,DeliveryBranchCode,Status)" +
                        "values('" + x.Batch + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                        "'" + x.DeliveryDate.ToString("yyyy-MM-dd") + "','" + x.BRSTN + "','" + x.AccountNo + "', '" + x.AccountName.Replace("'", "''") +
                        "','" + x.AccountName2.Replace("'", "''") + "','" + x.ChkType + "','" + x.CheckName.Replace("'", "''") + "','" + x.StartingSerial + "'," +
                        "'" + x.EndingSerial + "','" + x.DeliveryBrstn + "','" + x.DeliveryBranch + "','" + x.DeliveryBranchCode + "',1);";


                    cmd = new MySqlCommand(Sql, con);
                    cmd.ExecuteNonQuery();
                });
                con.Close();
                //DBClosed();
                //DBConnect();

                //FileStream fs = new FileStream(_zipFile, FileMode.Open, FileAccess.Read);
                //int FileSize = (int)fs.Length;
                //byte[] rawData  = new byte[FileSize];
                //fs.Read(rawData, 0, FileSize);
                //fs.Close();
                //Sql =  "Insert into " + gClient.ZipTable + " (Batch_ID, ZipFile, DateProcessed) values ('" + _orderList[0].Batch + "','"+rawData+"','" +
                //       DateTime.Now.ToString("yyyy-MM-dd") + "');";

                //cmd = new MySqlCommand(Sql, myConnect);
                //cmd.ExecuteNonQuery();
                MessageBox.Show("Data has been Successfully saved!", "Saving Data to Accounting Database ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Saving Data to Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
    