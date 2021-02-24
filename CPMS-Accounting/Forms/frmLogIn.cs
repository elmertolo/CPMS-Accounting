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

namespace CPMS_Accounting
{

   

    public partial class frmLogIn : Form
    {



        //02152021-NA Log4Net
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


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


            DataTable dt = new DataTable();
            if (!proc.UserLogin(UserId, password, ref dt))
            {
                MessageBox.Show("Unable to connect to server. \r\n" + proc.errorMessage);
                log.Error("Unable to connect to server" + proc.errorMessage);

                return;
            }
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("User Name or Password is incorrect. Please try again");
                log.Info("User Name or Password is incorrect.");
                return;
            }
            log.Info("User Login successful.");
            SupplyGlobalUserVariables(ref dt);
            SupplyGlobalClientVariables(cbBankList.Text.ToString());

            //02152021 Log4Net
            //Supply Additional Parameters on log4net
            SupplyParameterValuesOnLog4net();
            log.Info("User LogIn Sucessful for User: " + gUser.Id + "");

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
            if(!proc.GetClientDetails(bankname,ref dt))
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
                    gClient.DataBaseName = row.Field<string>("ShortName").ToLower() + "_history" ?? "";
                    gClient.SalesInvoiceTempTable = row.Field<string>("ShortName").ToLower() + "_salesInvoice_temp" ?? "";
                    gClient.SalesInvoiceFinishedTable = row.Field<string>("ShortName").ToLower() + "_salesinvoice_finished" ?? "";
                    gClient.PriceListTable = row.Field<string>("ShortName").ToLower() + "_pricelist" ?? "";
                    gClient.DRTempTable =  "ttempdatadr" ?? "";
                    gClient.PurchaseOrderFinishedTable = row.Field<string>("ShortName").ToLower() + "_purchaseorder_finished" ?? "";
                    gClient.DocStampTempTable =  "docstamp_temp" ?? "";
                    gClient.BranchesTable = row.Field<string>("ShortName").ToLower() + "_branches" ?? "";
                    gClient.CancelledTable = "cancelled_transaction" ?? "";
                    gClient.ChequeTypeTable = row.Field<string>("ShortName").ToLower() + "_tCheques" ?? "";
                    gClient.ProductTable = row.Field<string>("ShortName").ToLower() + "_tChequeproducts" ?? "";
                    gClient.StickerTable = "tsticker" ?? "";
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
            else if (DateTime.Now.Hour  <= 17)
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
        public void SupplyGlobalUserVariables(ref DataTable dt) 
        {
           
            foreach (DataRow row in dt.Rows)
            {
                gUser.Number = row.Field<int>("UserNo");
                gUser.Id = row.Field<string>("UserId");
                gUser.Password = row.Field<string>("Password");
                gUser.FirstName = row.Field<string>("FirstName");
                gUser.MiddleName = row.Field<string>("MiddleName");
                gUser.LastName = row.Field<string>("LastName");
                gUser.Level = row.Field<string>("UserLevel");
                gUser.Department = row.Field<string>("Department");
                gUser.Position = row.Field<string>("Position");
                gUser.Suffix = row.Field<string>("Suffix");
                gUser.Department = row.Field<string>("Department");
                gUser.Position = row.Field<string>("Position");
                gUser.Lockout = row.Field<string>("Lockout");
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
    }

}
