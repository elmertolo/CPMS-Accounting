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

namespace CPMS_Accounting
{
    public partial class frmLogIn : Form
    {
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

            Login(txtUserName.Text.ToString(), txtPassword.Text.ToString());
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

        private void Login(string userName, string password)
        {
            DataTable dt = new DataTable();
            if (!proc.UserLogin(userName, password, ref dt))
            {
                MessageBox.Show("Unable to connect to server. \r\n" + proc.errorMessage);
            }

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("User Name or Password is incorrect. Please try again");
                return;
            }




            SupplyGlobalUserVariables(ref dt);

            SupplyGlobalClientVariables(cbBankList.Text.ToString());

            Main mainFrm = new Main();
            mainFrm.Show();
            this.Hide();
           
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login(txtUserName.Text.ToString(), txtPassword.Text.ToString());
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login(txtUserName.Text.ToString(), txtPassword.Text.ToString());
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
                    gClient.DRTempTable = row.Field<string>("ShortName").ToLower() + "_dr_temp" ?? "";
                    gClient.PurchaseOrderFinishedTable = row.Field<string>("ShortName").ToLower() + "_purchaseorder_finished" ?? "";
                    gClient.DocStampTempTable =  "docstamp_temp" ?? "";
                    gClient.BranchesTable = row.Field<string>("ShortName").ToLower() + "_branches" ?? "";
                }
            }
           
            
        }

        private void frmLogIn_Load(object sender, EventArgs e)
        {
           
        }

        private void cbBankList_SelectedIndexChanged(object sender, EventArgs e)
        {



        }

        public void SupplyGlobalUserVariables(ref DataTable dt) 
        {
           
            foreach (DataRow row in dt.Rows)
            {
                gUser.UserName = row.Field<string>("UserName");
                gUser.Password = row.Field<string>("Password");
                gUser.FirstName = row.Field<string>("FirstName");
                gUser.MiddleName = row.Field<string>("MiddleName");
                gUser.LastName = row.Field<string>("LastName");
                gUser.Suffix = row.Field<string>("Suffix");
                gUser.Lockout = row.Field<string>("Lockout");

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

}
