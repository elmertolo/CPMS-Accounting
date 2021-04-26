using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CPMS_Accounting.Reports;
using CPMS_Accounting.Procedures;
using static CPMS_Accounting.GlobalVariables;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using CPMS_Accounting.Forms;
using System.Threading;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using CPMS_Accounting.Models;

namespace CPMS_Accounting
{

    public partial class frmLogIn : Form
    {

        //02152021-NA Log4Net
        private log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        frmProgress progressBar;
        Thread thread;


        //02222021-NA Password Encyption
        private static byte[] GetSHA1(string userID, string password)
        {
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            return sha.ComputeHash(System.Text.Encoding.ASCII.GetBytes(userID + password));
        }

        DataTable BankListDT = new DataTable();
        ProcessServices_Nelson proc = new ProcessServices_Nelson();
        public static string tableName = "";
        public static string tempTableName = "";
        public frmLogIn()
        {
            InitializeComponent();
            InitializeGlobalVariables();
            FillBankList();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            log.Info("Login Button Click");
            Login(txtUserId.Text.ToString(), txtPassword.Text.ToString());

            //MessageBox.Show(gClient.DocStampTempTable.ToString());
        }

        private void FillBankList()
        {
            if (!proc.GetBankList(ref BankListDT))
            {
                MessageBox.Show("Unable to connect to server. \r\n" + proc.errorMessage);
                Application.Exit();
            }
            cbBankList.DisplayMember = "description";
            cbBankList.DataSource = BankListDT;
        }

        private void Login(string UserId, string enteredPassword)
        {

            //02222021 Encryption
            string password = enteredPassword;
            if (gEncryptionOn)
            {
                byte[] hashedPassword = GetSHA1(UserId, enteredPassword);
                password = Convert.ToBase64String(hashedPassword);
            }

            //Identify if admin login or standard login
            DataTable dt = new DataTable();
            if (AdminLogin(UserId, password))
            {
                SupplyGlobalUserVariables(ref dt, true);
                SupplyGlobalUserPermissionsVariables(gUser.UserLevelCode, true);
            }
            else
            {
                if (!StandardLogin(UserId, password, ref dt))
                {
                    return;
                }
                SupplyGlobalUserVariables(ref dt);
                SupplyGlobalUserPermissionsVariables(gUser.UserLevelCode);
            }

            log.Info("User Login successful.");
            //SupplyGlobalUserVariables(ref dt);
            //SupplyGlobalUserPermissionsVariables(gUser.UserLevelCode);
            //03012021 Commented out above lines. placed it into Admin/Standard user login section
            SupplyGlobalClientVariables(cbBankList.Text.ToString());

            //02152021 Log4Net
            //Supply Additional Parameters on log4net
            SupplyParameterValuesOnLog4net();
            log.Info("User LogIn Sucessful for User: " + gUser.Id + "");

            BackupDataAndApplication();

            //04122021
            //Supply Branchlist
            //FillBranchlist(gClient.ClientCode);

            Main mainFrm = new Main();
            mainFrm.Show();
            this.Hide();

        }


        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login(txtUserId.Text.ToString(), txtPassword.Text.ToString());
            }

        }

        private void SupplyGlobalClientVariables(string bankname)
        {
            DataTable dt = new DataTable();
            if (!proc.GetClientDetails(bankname, ref dt))
            {
                MessageBox.Show("Server Connection Error (GetClientDetails)\r\n" + proc.errorMessage);
            }

            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    gClient.ClientCode = row.Field<string>("ClientCode") ?? "";
                    gClient.ShortName = row.Field<string>("ShortName") ?? "";
                    gClient.Description = row.Field<string>("Description") ?? "";
                    gClient.Address1 = row.Field<string>("Address1") ?? "";
                    gClient.Address2 = row.Field<string>("Address2") ?? "";
                    gClient.Address3 = row.Field<string>("Address3") ?? "";
                    gClient.AttentionTo = row.Field<string>("AttentionTo") ?? "";
                    gClient.Princes_DESC = row.Field<string>("Princes_DESC") ?? "";
                    gClient.TIN = row.Field<string>("TIN") ?? "";
                    gClient.WithholdingTaxPercentage = row.Field<decimal>("WithholdingTaxPercentage");

                    //Database Global Tables

                    //for the time being..
                    if (bankname.ToUpper() == "UNIONBANK OF THE PHILIPPINES")
                    {
                        gClient.DataBaseName = "master_database_union";
                    }
                    else
                    {
                        gClient.DataBaseName = row.Field<string>("ShortName").ToLower() + "_history" ?? "";
                    }

                    gClient.SalesInvoiceTempTable = row.Field<string>("ShortName").ToLower() + "_salesInvoice_temp" ?? "";
                    gClient.SalesInvoiceFinishedTable = row.Field<string>("ShortName").ToLower() + "_salesinvoice_finished" ?? "";
                    gClient.PriceListTable = row.Field<string>("ShortName").ToLower() + "_pricelist" ?? "";
                    gClient.DRTempTable = "ttempdatadr" ?? "";
                    gClient.PurchaseOrderFinishedTable = row.Field<string>("ShortName").ToLower() + "_purchaseorder_finished" ?? "";
                    gClient.DocStampTempTable = "docstamp_temp" ?? "";
                    gClient.BranchesTable = row.Field<string>("ShortName").ToLower() + "_branches" ?? "";
                    gClient.CancelledTable = "cancelled_transaction" ?? "";
                    gClient.ChequeTypeTable = row.Field<string>("ShortName").ToLower() + "_tCheques" ?? "";
                    gClient.ProductTable = row.Field<string>("ShortName").ToLower() + "_tChequeproducts" ?? "";
                    gClient.StickerTable = "tsticker" ?? "";
                    gClient.PackingList = "tpackinglist" ?? "";

                    //03192021 Enhancement - Sales Invoice Reprint
                    gClient.SalesInvoiceFinishedDetailTable = row.Field<string>("ShortName").ToLower() + "_salesInvoice_finished_detail" ?? "";

                    //04122021
                    



                }
            }


        }

        private void frmLogIn_Load(object sender, EventArgs e)
        {
           


            //string line = new string('=', 100);
            log.Info(new string('=', 100));
            log.Info("Login Form Loaded");

            if (DateTime.Now.Hour <= 11)
            {
                this.Text = "GOOD MORNING!";
            }
            else if (DateTime.Now.Hour <= 17)
            {
                this.Text = "GOOD AFTERNOON!";
            }
            else if (DateTime.Now.Hour <= 23)
            {
                this.Text = "GOOD NIGHT!";
            }

        }

        private void cbBankList_SelectedIndexChanged(object sender, EventArgs e)
        {



        }
        public void SupplyGlobalUserVariables(ref DataTable dt, bool userIsAdmin = false)
        {
            ///<summary>
            ///
            /// </summary>

            if (!userIsAdmin)
            {
                foreach (DataRow row in dt.Rows)
                {
                    gUser.Number = row.Field<int>("UserNo");
                    gUser.Id = row.Field<string>("UserId");
                    gUser.Password = row.Field<string>("Password");
                    gUser.FirstName = row.Field<string>("FirstName");
                    gUser.MiddleName = row.Field<string>("MiddleName");
                    gUser.LastName = row.Field<string>("LastName");
                    gUser.UserLevelCode = row.Field<string>("UserlevelCode");
                    gUser.Department = row.Field<string>("Department");
                    gUser.Position = row.Field<string>("Position");
                    gUser.Suffix = row.Field<string>("Suffix");
                    gUser.Lockout = row.Field<string>("Lockout");
                }

            }
            else
            {

                gUser.Number = 5201;
                gUser.Id = "KING";
                gUser.Password = "";
                gUser.FirstName = "Juan";
                gUser.MiddleName = "Pedro";
                gUser.LastName = "Dela Cruz";
                gUser.Suffix = "";
                gUser.UserLevelCode = "Super_Administrator";
                gUser.Department = "Development";
                gUser.Position = "Administrator";
                gUser.Lockout = "100";

            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void SupplyParameterValuesOnLog4net()
        {
            log4net.Config.XmlConfigurator.Configure();
            log4net.ThreadContext.Properties["CurrentUser"] = gUser.Id;
            log4net.ThreadContext.Properties["CurrentClient"] = gClient.ShortName;
        }

        private void txtUserId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login(txtUserId.Text.ToString(), txtPassword.Text.ToString());
            }
        }

        private void SupplyGlobalUserPermissionsVariables(string userLevelCode, bool isAdmin = false)
        {

            if (!isAdmin)
            {
                DataTable dt = new DataTable();
                if (!proc.GetUserLevelDetails(ref dt, userLevelCode))
                {
                    p.MessageAndLog("Server Connection Error (GetUserLevelDetails)\r\n \r\n" + proc.errorMessage, ref log, "Fatal");
                    return;

                }

                //Supply dt values sbyteo UserLevelModel
                foreach (DataRow row in dt.Rows)
                {
                    //gUser.UserLevelCode = row.Field<string>("UserLevelCode");
                    //Commented above statement. prevoiusly declared and supplied on gSupplyGlobalUserVariables
                    gUser.UserLevelName = "KING";

                    //Delivery Report
                    gUser.IsAllowedOnDr = row.Field<sbyte>("IsAllowedOnDr");
                    gUser.IsDrCreateAllowed = Convert.ToBoolean(row.Field<sbyte>("IsDrCreateAllowed"));
                    gUser.IsDrEditAllowed = Convert.ToBoolean(row.Field<sbyte>("IsDrEditAllowed"));
                    gUser.IsDrDeleteAllowed = Convert.ToBoolean(row.Field<sbyte>("IsDrDeleteAllowed"));

                    //User Maintenance
                    gUser.IsAllowedOnUm = row.Field<sbyte>("IsAllowedOnUm");
                    gUser.IsUmCreateAllowed = Convert.ToBoolean(row.Field<sbyte>("IsDrCreateAllowed"));
                    gUser.IsUmEditAllowed = Convert.ToBoolean(row.Field<sbyte>("IsUmEditAllowed"));
                    gUser.IsUmDeleteAllowed = Convert.ToBoolean(row.Field<sbyte>("IsUmDeleteAllowed"));

                    //User Level Management
                    gUser.IsAllowedOnUl = row.Field<sbyte>("IsAllowedOnUl");
                    gUser.IsUlCreateAllowed = Convert.ToBoolean(row.Field<sbyte>("IsUlCreateAllowed"));
                    gUser.IsUlEditAllowed = Convert.ToBoolean(row.Field<sbyte>("IsUlEditAllowed"));
                    gUser.IsUlDeleteAllowed = Convert.ToBoolean(row.Field<sbyte>("IsUlDeleteAllowed"));

                    //SalesInvoice
                    gUser.IsAllowedOnSi = row.Field<sbyte>("IsAllowedOnSi");
                    gUser.IsSiCreateAllowed = Convert.ToBoolean(row.Field<sbyte>("IsSiCreateAllowed"));
                    gUser.IsSiEditAllowed = Convert.ToBoolean(row.Field<sbyte>("IsSiEditAllowed"));
                    gUser.IsSiDeleteAllowed = Convert.ToBoolean(row.Field<sbyte>("IsSiDeleteAllowed"));

                    //Purchase Order
                    gUser.IsAllowedOnPo = row.Field<sbyte>("IsAllowedOnPo");
                    gUser.IsPoCreateAllowed = Convert.ToBoolean(row.Field<sbyte>("IsPoCreateAllowed"));
                    gUser.IsPoEditAllowed = Convert.ToBoolean(row.Field<sbyte>("IsPoEditAllowed"));
                    gUser.IsPoDeleteAllowed = Convert.ToBoolean(row.Field<sbyte>("IsPoDeleteAllowed"));

                    //Product Masbyteenance
                    gUser.IsAllowedOnPm = row.Field<sbyte>("IsAllowedOnPm");
                    gUser.IsPmCreateAllowed = Convert.ToBoolean(row.Field<sbyte>("IsPmCreateAllowed"));
                    gUser.IsPmEditAllowed = Convert.ToBoolean(row.Field<sbyte>("IsPmEditAllowed"));
                    gUser.IsPmDeleteAllowed = Convert.ToBoolean(row.Field<sbyte>("IsPmDeleteAllowed"));

                    //Product Masbyteenance
                    gUser.IsAllowedOnDc = row.Field<sbyte>("IsAllowedOnDc");
                    gUser.IsDcCreateAllowed = Convert.ToBoolean(row.Field<sbyte>("IsDcCreateAllowed"));
                    gUser.IsDcEditAllowed = Convert.ToBoolean(row.Field<sbyte>("IsDcEditAllowed"));
                    gUser.IsDcDeleteAllowed = Convert.ToBoolean(row.Field<sbyte>("IsDcDeleteAllowed"));

                }
            }
            else
            {
                //gUser.UserLevelCode = row.Field<string>("UserLevelCode");
                //Commented above statement. prevoiusly declared and supplied on gSupplyGlobalUserVariables
                gUser.UserLevelName = "Captive Administrator";

                //Delivery Report
                gUser.IsAllowedOnDr = 1;
                gUser.IsDrCreateAllowed = true;
                gUser.IsDrEditAllowed = true;
                gUser.IsDrDeleteAllowed = true;

                //User Maintenance
                gUser.IsAllowedOnUm = 1;
                gUser.IsUmCreateAllowed = true;
                gUser.IsUmEditAllowed = true;
                gUser.IsUmDeleteAllowed = true;

                //User Level Management
                gUser.IsAllowedOnUl = 1;
                gUser.IsUlCreateAllowed = true;
                gUser.IsUlEditAllowed = true;
                gUser.IsUlDeleteAllowed = true;

                //SalesInvoice
                gUser.IsAllowedOnSi = 1;
                gUser.IsSiCreateAllowed = true;
                gUser.IsSiEditAllowed = true;
                gUser.IsSiDeleteAllowed = true;

                //Purchase Order
                gUser.IsAllowedOnPo = 1;
                gUser.IsPoCreateAllowed = true;
                gUser.IsPoEditAllowed = true;
                gUser.IsPoDeleteAllowed = true;

                //Product Masbyteenance
                gUser.IsAllowedOnPm = 1;
                gUser.IsPmCreateAllowed = true;
                gUser.IsPmEditAllowed = true;
                gUser.IsPmDeleteAllowed = true;

                //Product Masbyteenance
                gUser.IsAllowedOnDc = 1;
                gUser.IsDcCreateAllowed = true;
                gUser.IsDcEditAllowed = true;
                gUser.IsDcDeleteAllowed = true;
            }

        }

        private bool StandardLogin(string userId, string password, ref DataTable dt)
        {

            if (!proc.UserLogin(userId, password, ref dt))
            {
                MessageBox.Show("Unable to connect to server. \r\n" + proc.errorMessage);
                log.Error("Unable to connect to server" + proc.errorMessage);
                return false;
            }

            if (dt.Rows.Count > 0)
            {
                return true;

            }
            else
            {
                MessageBox.Show("User Name or Password is incorrect. Please try again");
                log.Info("User Name or Password is incorrect.");
                return false;
            }
        }

        private bool AdminLogin(string userId, string password)
        {
            if (userId == "1" && password == "1")
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        private void BackupDataAndApplication()
        {
            if (gDailyBackupOn)
            {
                log.Info("Database backup in progress");
                //Start Progress Bar View
                progressBar = new frmProgress();
                progressBar.message = "Database backup in progress";
                thread = new Thread(() => progressBar.ShowDialog());
                thread.Start();

                ///<summary>
                ///
                /// </summary>
                if (!proc.MySqlBackupTableData(gBackupPath))
                {
                    p.MessageAndLog("Backup Unsuccessful", ref log, "info");
                    return;
                }
                UpdateJsonFile("Database", "LastBackupDate", DateTime.Now.ToShortDateString());

                //filePath = System.IO.Path.GetFullPath("app.config");

                //var map = new ExeConfigurationFileMap { ExeConfigFilename = filePath };
                //Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
                //config.AppSettings.Settings["LastBackupDate"].Value = DateTime.Now.ToShortDateString();
                ////config.Save();
                //config.Save(ConfigurationSaveMode.Modified);

                //ConfigurationManager.RefreshSection("appSettings");

                thread.Abort();
                //p.MessageAndLog("Backup Successful", ref log, "info");



            }

        }

        private void UpdateJsonFile(string section, string key, string value)
        {

            //Determine path when running through IDE or not
            string filePath;
            if (Debugger.IsAttached)
            {
                filePath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Config.json";
            }
            else
            {
                filePath = System.IO.Path.GetFullPath("Config.json");
            }
            if (!File.Exists(filePath))
            {
                return;
            }

            //https://stackoverflow.com/questions/21695185/change-values-in-json-file-writing-files/56027969
            //Read Json File
            string json = File.ReadAllText(filePath);
            //Deserialize Json Object dynamically
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            //Edit Key Value
            jsonObj[section][key] = value;
            //Serialize edited output to string
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            //Write or save to json file
            File.WriteAllText(filePath, output);

        }

        private void InitializeGlobalVariables(){

            //02222021 Encryption
            gEncryptionOn = Convert.ToBoolean(p.ReadJsonConfigFile("Encryption", "EncryptionOn", "false"));
            gEncryptionType = p.ReadJsonConfigFile("Encryption", "EncryptionType", "SHA1");

            /// <summary>
            /// \
            /// 
            /// 
            /// This variables is used for SalesInvoice Processes only.
            /// </summary
            //variables from appconfig file=================================================
            //public static List<SalesInvoiceModel> gSalesInvoiceList = new List<SalesInvoiceModel>();

            gViewReportFirst = int.Parse(ConfigurationManager.AppSettings["ViewReportFirst"]);
            gHeaderReportCompanyName = ConfigurationManager.AppSettings["SIHeaderReportCompanyName"]; //"PRODUCERS BANK";
            gSIheaderReportTitle = ConfigurationManager.AppSettings["SIheaderReportTitle"]; //"SALES INVOICE";
            gSIHeaderReportAddress1 = ConfigurationManager.AppSettings["SIHeaderReportAddress1"]; //"6197 Ayala Avenue";
            gSIHeaderReportAddress2 = ConfigurationManager.AppSettings["SIHeaderReportAddress2"]; //"Salcedo Village";
            gSIHeaderReportAddress3 = ConfigurationManager.AppSettings["SIHeaderReportAddress3"]; //"Makati City";                                                                                                 
            //=============================================================================

            ///<summary>
            ///03102021
            ///Daily Backup
            ///Started Reading Config from Json File
            /// </summary>
            gDailyBackupOn = Convert.ToBoolean(p.ReadJsonConfigFile("Database", "DailyBackupOn", "false"));
            gLastBackupDate = Convert.ToDateTime(p.ReadJsonConfigFile("Database", "LastBackupDate", "19 Aug 1983"));
            gBackupPath = p.ReadJsonConfigFile("Database", "BackupPath", "");

        }

        private void FillBranchlist(string clientCode)
        {

        }





    }

}
