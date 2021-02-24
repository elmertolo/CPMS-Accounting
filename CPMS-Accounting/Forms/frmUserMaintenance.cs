using CPMS_Accounting.Models;
using CPMS_Accounting.Procedures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CPMS_Accounting.GlobalVariables;

namespace CPMS_Accounting.Forms
{
    public partial class frmUserMaintenance : Form
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ProcessServices_Nelson proc = new ProcessServices_Nelson();
        frmProgress progressBar;
        Thread thread;
        Main frm;

        public frmUserMaintenance(Main frm1)
        {
            InitializeComponent();
            ConfigureDesignLabels();
            FillUserLevelCombox();
            this.frm = frm1;
            
        }

        private void frmUserMaintenance_Load(object sender, EventArgs e)
        {

            RefreshView();

            
            

        }

        public void ConfigureDesignLabels()
        {
            string fullname = gUser.FirstName + " " + gUser.LastName;

            lblUserName.Text = fullname.ToUpper();
            lblBankName.Text = gClient.Description.ToUpper();

        }

        private void lblFirstName_Click(object sender, EventArgs e)
        {

        }

        private void RefreshView()
        {

            log.Info("Refreshing Display");

            gbSearch.Enabled = true;

            DisableControls();
            ActionPanelInitialLoadView();
            

            txtSearch.Text = "";
            txtUserId.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            cbUserLevel.Text = "";
            cbDeparment.Text = "";
            txtPosition.Text = "";
            txtFirstName.Text = "";
            txtMiddleName.Text = "";
            txtLastName.Text = "";
            txtSuffix.Text = "";

            btnSaveRecord.Text = "SAVE";

            cbUserLevel.Text = "";

            txtUserId.Focus();
        }

        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            AddRecord();
        }

        private void AddRecord()
        {
            if (!string.IsNullOrEmpty(txtUserId.Text))
            {

                string userId = txtUserId.Text.ToString();
                DataTable dt = new DataTable();
                if (!proc.GetUserDetails(ref dt, userId))
                {
                    p.MessageAndLog("Server connection Error. (proc.GetUserDetails)\r\n \r\n " + proc.errorMessage, ref log, "Fatal");
                    return;
                }
                if (dt.Rows.Count > 0)
                {
                    log.Info("Existing User Record.");
                    DisplayExistingUserDetails(ref dt);
                }
                else
                {
                    log.Info("New User Record");
                    EnableControls();
                    ActionPanelNewRecordView();
                }
            }
            else
            {
                p.MessageAndLog("Please provide User Id to continue.", ref log, "warn");
                txtUserId.Focus();
                return;
            }
        }

        private void DisplayExistingUserDetails(ref DataTable dt)
        {
            log.Info("Fetching Existing User Record");
            //Start Progress Bar View
            progressBar = new frmProgress();
            progressBar.message = "Fetching Existing Record. Please Wait.";
            thread = new Thread(() => progressBar.ShowDialog());
            thread.Start();

            //Display values on Front End from Finished Table
            foreach (DataRow row in dt.Rows)
            {
                txtUserId.Text = row.Field<string>("UserId");
                txtPassword.Text = row.Field<string>("Password");
                cbUserLevel.Text = Convert.ToString(proc.SeekReturn("Select UserLevelName from userlevels where userlevelcode ='" + row.Field<string>("UserLevelCode") + "'", ""));
                cbDeparment.Text = row.Field<string>("Department");
                txtPosition.Text = row.Field<string>("Position");
                txtFirstName.Text = row.Field<string>("FirstName");
                txtMiddleName.Text = row.Field<string>("MiddleName");
                txtLastName.Text = row.Field<string>("LastName");
                txtSuffix.Text = row.Field<string>("Suffix");

            }

            //02222021 Password Encryption
            //byte[] hashedPassword = p.GetSHA1(txtUserId.Text, txtPassword.Text);
            //string toDecrypt = Convert.ToBase64String(hashedPassword);
            //string dencryptedPassword = p.HashSHA1Decryption(txtPassword.Text);
            //txtPassword.Text = dencryptedPassword;



            EnableControls();
            ActionPanelEditRecordView();

            thread.Abort();
        }

        private void DisableControls()
        {
            gbUserId.Enabled = true;
            gbDetails.Enabled = false;
        }

        private void EnableControls()
        {
            gbUserId.Enabled = false;
            gbDetails.Enabled = true;
            txtPassword.Focus();
        }

        private void ActionPanelInitialLoadView()
        {
            btnRefreshView.Enabled = false;
            btnEditRecord.Enabled = false;
            btnDeleteRecord.Enabled = false;
            btnSaveRecord.Enabled = false;
        }

        private void ActionPanelNewRecordView()
        {
            btnRefreshView.Enabled = true;
            btnEditRecord.Enabled = false;
            btnDeleteRecord.Enabled = false;
            btnSaveRecord.Enabled = true;

            btnSaveRecord.Text = "SAVE";
        }

        private void ActionPanelEditRecordView()
        {
            btnRefreshView.Enabled = true;
            btnEditRecord.Enabled = false;
            btnDeleteRecord.Enabled = true;
            btnSaveRecord.Enabled = true;

            btnSaveRecord.Text = "UPDATE";
        }

        private void btnRefreshView_Click(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void FillUserLevelCombox()
        {
            DataTable dt = new DataTable();
            if (!proc.GetUserLevels(ref dt))
            {
                p.MessageAndLog("Error fetching User Levels (GetUserLevels)\r\n \r\n" + proc.errorMessage, ref log, "Fatal");
            }
            cbUserLevel.DataSource = dt;
            cbUserLevel.DisplayMember = "UserLevelName";

        }

        private void btnSaveRecord_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        private void SaveRecord()
        {
            string userId = txtUserId.Text.ToString();

            if (!string.IsNullOrEmpty(userId))
            {
                DataTable dt = new DataTable();
                if (!proc.GetUserDetails(ref dt, userId))
                {
                    p.MessageAndLog("Server connection Error. (proc.GetUserDetails)\r\n \r\n " + proc.errorMessage, ref log, "Fatal");
                    return;
                }
                if (dt.Rows.Count > 0)
                {
                    UpdateUserRecord();
                    RefreshView();
                }
                else
                {
                    InsertNewUserRecord();
                    RefreshView();
                }
            }
            else
            {
                p.MessageAndLog("Invalid User Id.", ref log, "warn");
                return;
            }
        }

        private void UpdateUserRecord()
        {
            UserListModel user = new UserListModel();
            user.Id = txtUserId.Text;
            user.Password = txtPassword.Text;
            user.FirstName = txtFirstName.Text;
            user.MiddleName = txtMiddleName.Text;
            user.LastName = txtLastName.Text;
            user.Suffix = txtSuffix.Text;
            user.UserLevelCode = Convert.ToString(proc.SeekReturn("select userlevelcode from userlevels where UserLevelName ='" + cbUserLevel.Text + "'", ""));
            user.Department = cbDeparment.Text;
            user.Position = txtPosition.Text;

            if (!proc.UpdateExistingUserRecord(user))
            {
                p.MessageAndLog("Server Connection Error (UpdateExistingUserRecord)\r\n \r\n" + proc.errorMessage, ref log, "Fatal");
                return;
            }
            p.MessageAndLog("User Update Successful.", ref log, "info");
        }

        private void InsertNewUserRecord()
        {
            UserListModel user = new UserListModel();

            user.Id = txtUserId.Text;
            user.Password = txtPassword.Text;
            user.FirstName = txtFirstName.Text;
            user.MiddleName = txtMiddleName.Text;
            user.LastName = txtLastName.Text;
            user.Suffix = txtSuffix.Text;
            user.UserLevelCode = Convert.ToString(proc.SeekReturn("select userlevelcode from userLevels where UserLevelName ='" + cbUserLevel.Text + "'", ""));
            user.Department = cbDeparment.Text;
            user.Position = txtPosition.Text;

            if (!proc.InsertNewUserRecord(user))
            {
                p.MessageAndLog("Server Connection Error (UpdateExistingUserRecord)\r\n \r\n" + proc.errorMessage, ref log, "Fatal");
                return;
            }
            p.MessageAndLog("User Update Successful.", ref log, "info");

        }

        private void txtUserId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddRecord();
            }
        }

        private void btnCancelClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
