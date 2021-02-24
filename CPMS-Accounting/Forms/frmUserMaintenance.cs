using CPMS_Accounting.Procedures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            FillUserLevelComboBox();
            this.frm = frm1;
            
        }

        private void frmUserMaintenance_Load(object sender, EventArgs e)
        {

            RefreshView();

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
                if (proc.GetUserDetails(ref dt, userId))
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
                cbUserLevel.Text = row.Field<string>("UserLevel");
                cbDeparment.Text = row.Field<string>("Department");
                txtPosition.Text = row.Field<string>("Position");
                txtFirstName.Text = row.Field<string>("FirstName");
                txtMiddleName.Text = row.Field<string>("MiddleName");
                txtLastName.Text = row.Field<string>("LastName");
                txtSuffix.Text = row.Field<string>("Suffix");

            }

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

        private void txtUserId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddRecord();
            }
        }

        private void FillUserLevelComboBox()
        {
            DataTable dt = new DataTable();
            if (!proc.GetUserLevelDetails(ref dt))
            {
                MessageBox.Show("Unable to connect to server. \r\n" + proc.errorMessage);
            }

            _ = dt.Rows.Count != 0 ? cbUserLevel.DataSource = dt : cbUserLevel.DataSource = null;
            cbUserLevel.BindingContext = new BindingContext();
            cbUserLevel.DisplayMember = "UserLevelName";
            cbUserLevel.SelectedIndex = -1;



        }

        private void btnSaveRecord_Click(object sender, EventArgs e)
        {

        }
    }
}
