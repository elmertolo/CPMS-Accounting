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
using CPMS_Accounting.Procedures;


namespace CPMS_Accounting.Forms
{
    public partial class frmUserLevelManagement : Form
    {

        //02152021 Log4Net
        private log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ProcessServices_Nelson proc = new ProcessServices_Nelson();
        frmProgress progressBar;
        Thread thread;
        Main frm;


        public frmUserLevelManagement(Main frm1)
        {
            InitializeComponent();

            this.frm = frm1;
        }

        private void rdDrYes_CheckedChanged(object sender, EventArgs e)
        {
            if (rdDrYes.Checked)
            {
                pnlDr.Enabled = true;
                chkDrCreate.Checked = true;
                chkDrEdit.Checked = true;
                chkDrDelete.Checked = true;

            }
            else
            {
                pnlDr.Enabled = false;
            }
        }

        private void frmUserLevelManagement_Load(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void RefreshView()
        {
            log.Info("Refreshing Display");

            gbUserLevelCode.Enabled = true;
            gbSearch.Enabled = true;

            CheckAllRadioButtons();
            DisableControls();
            ActionPanelInitialLoadView();
            txtUserLeveCode.Focus();
        }

        private void CheckAllRadioButtons()
        {
            foreach (GroupBox groupBox in this.Controls.OfType<GroupBox>())
            {
                foreach (TabControl tabControl in groupBox.Controls.OfType<TabControl>())
                {
                    foreach (TabPage tabPage in tabControl.Controls.OfType<TabPage>())
                    {
                        foreach (GroupBox gbox2 in tabPage.Controls.OfType<GroupBox>())
                        {
                            foreach (RadioButton rdBtn in gbox2.Controls.OfType<RadioButton>())
                            {
                                if (rdBtn.Name.Contains("Yes"))
                                {
                                    rdBtn.Checked = true;
                                }
                            }

                        }

                    }
                }    
            }
    
        }

        private void DisableControls()
        {
            gbDetails.Enabled = false;
        }

        private void EnableControls()
        {
            gbUserLevelCode.Enabled = false;
            gbSearch.Enabled = false;
            gbDetails.Enabled = true;
            txtUserLevelName.Focus();
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
        }

        private void ActionPanelEditRecordView()
        {
            btnRefreshView.Enabled = true;
            btnEditRecord.Enabled = false;
            btnDeleteRecord.Enabled = true;
            btnSaveRecord.Enabled = true;
        }

        private void btnCreateNewRecord_Click(object sender, EventArgs e)
            {
                EnableControls();
                ActionPanelNewRecordView();
                CheckAllRadioButtons();
                


            }

        private void btnRefreshView_Click(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void rdUmYes_CheckedChanged(object sender, EventArgs e)
        {
            if (rdUmYes.Checked)
            {
                pnlUm.Enabled = true;
                chkUmCreate.Checked = true;
                chkUmEdit.Checked = true;
                chkUmDelete.Checked = true;

            }
            else
            {
                pnlUm.Enabled = false;
            }
        }

        private void rdUlYes_CheckedChanged(object sender, EventArgs e)
        {
            if (rdUlYes.Checked)
            {
                pnlUl.Enabled = true;
                chkUlCreate.Checked = true;
                chkUlEdit.Checked = true;
                chkUlDelete.Checked = true;

            }
            else
            {
                pnlUl.Enabled = false;
            }
        }

        private void rdSiYes_CheckedChanged(object sender, EventArgs e)
        {

            if (rdSiYes.Checked)
            {
                pnlSi.Enabled = true;
                chkSiCreate.Checked = true;
                chkSiEdit.Checked = true;
                chkSiDelete.Checked = true;

            }
            else
            {
                pnlSi.Enabled = false;
            }
        }

        private void rdPoYes_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPoYes.Checked)
            {
                pnlPo.Enabled = true;
                chkPoCreate.Checked = true;
                chkPoEdit.Checked = true;
                chkPoDelete.Checked = true;

            }
            else
            {
                pnlPo.Enabled = false;
            }
        }

        private void rdPmYes_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPmYes.Checked)
            {
                pnlPm.Enabled = true;
                chkPmCreate.Checked = true;
                chkPmEdit.Checked = true;
                chkPmDelete.Checked = true;

            }
            else
            {
                pnlPm.Enabled = false;
            }
        }

        private void rdDcYes_CheckedChanged(object sender, EventArgs e)
        {
            if (rdDcYes.Checked)
            {
                pnlDc.Enabled = true;
                chkDcCreate.Checked = true;
                chkDcEdit.Checked = true;
                chkDcDelete.Checked = true;

            }
            else
            {
                pnlDc.Enabled = false;
            }
        }

        private void rdDrNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdDrNo.Checked)
            {
               
                chkDrCreate.Checked = false;
                chkDrEdit.Checked = false;
                chkDrDelete.Checked = false;
                pnlDr.Enabled = false;

            }
           
        }

        private void rdUmNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdUmNo.Checked)
            {

                chkUmCreate.Checked = false;
                chkUmEdit.Checked = false;
                chkUmDelete.Checked = false;
                pnlUm.Enabled = false;

            }
        }

        private void rdUlNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdUlNo.Checked)
            {

                chkUlCreate.Checked = false;
                chkUlEdit.Checked = false;
                chkUlDelete.Checked = false;
                pnlUl.Enabled = false;

            }
        }

        private void rdSiNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSiNo.Checked)
            {

                chkSiCreate.Checked = false;
                chkSiEdit.Checked = false;
                chkSiDelete.Checked = false;
                pnlSi.Enabled = false;

            }
        }

        private void rdPoNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPoNo.Checked)
            {

                chkPoCreate.Checked = false;
                chkPoEdit.Checked = false;
                chkPoDelete.Checked = false;
                pnlPo.Enabled = false;

            }
        }

        private void rdPmNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPmNo.Checked)
            {
                chkPmCreate.Checked = false;
                chkPmEdit.Checked = false;
                chkPmDelete.Checked = false;
                pnlPm.Enabled = false;

            }
        }

        private void rdDcNo_CheckedChanged(object sender, EventArgs e)
        {

            if (rdDcNo.Checked)
            {
                chkDcCreate.Checked = false;
                chkDcEdit.Checked = false;
                chkDcDelete.Checked = false;
                pnlDc.Enabled = false;

            }
        }

        private void rdDrRead_CheckedChanged(object sender, EventArgs e)
        {
            if (rdDrRead.Checked)
            {

                chkDrCreate.Checked = false;
                chkDrEdit.Checked = false;
                chkDrDelete.Checked = false;
                pnlDr.Enabled = false;

            }
        }

        private void rdUmRead_CheckedChanged(object sender, EventArgs e)
        {
            if (rdUmRead.Checked)
            {

                chkUmCreate.Checked = false;
                chkUmEdit.Checked = false;
                chkUmDelete.Checked = false;
                pnlUm.Enabled = false;

            }
        }

        private void rdUlRead_CheckedChanged(object sender, EventArgs e)
        {
            if (rdUlRead.Checked)
            {

                chkUlCreate.Checked = false;
                chkUlEdit.Checked = false;
                chkUlDelete.Checked = false;
                pnlUl.Enabled = false;

            }
        }

        private void rdSiRead_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSiRead.Checked)
            {

                chkSiCreate.Checked = false;
                chkSiEdit.Checked = false;
                chkSiDelete.Checked = false;
                pnlSi.Enabled = false;

            }
        }

        private void rdPoRead_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPoRead.Checked)
            {

                chkPoCreate.Checked = false;
                chkPoEdit.Checked = false;
                chkPoDelete.Checked = false;
                pnlPo.Enabled = false;

            }
        }

        private void rdPmRead_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPmRead.Checked)
            {
                chkPmCreate.Checked = false;
                chkPmEdit.Checked = false;
                chkPmDelete.Checked = false;
                pnlPm.Enabled = false;

            }
        }

        private void rdDcRead_CheckedChanged(object sender, EventArgs e)
        {
            if (rdDcRead.Checked)
            {
                chkDcCreate.Checked = false;
                chkDcEdit.Checked = false;
                chkDcDelete.Checked = false;
                pnlDc.Enabled = false;

            }
        }

        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(txtUserLeveCode.Text))
            {

                string UserLevelCode = txtUserLeveCode.Text.ToString();

                if (proc.UserLevelExist(UserLevelCode))
                {
                    log.Info("Existing User Level Record");
                    DisplayOldUserLevelDetails(UserLevelCode);
                }
                else
                {
                    log.Info("New Sales Invoice Record");
                    EnableControls();
                    ActionPanelNewRecordView();
                    CheckAllRadioButtons();
                }
            }
           

            
        }

        private void SaveRecord()
        {
            string userLevelCode = txtUserLeveCode.Text.ToString();
            string userLevelName = txtUserLevelName.Text.ToString();
            string formInitial;
            int radioButtonValue = 0;
            int isCreateAllowed = 0;
            int isEditAllowed = 0;
            int isDeleteAllowed = 0;


            proc.InsertUserLevelRecord(userLevelCode, userLevelName);

           
            //Kung Nahihiwagaan ka kung bakit ganito ginawa ko? let me explain...
            //Ang Outer Control Kasi ay Groupbox
            foreach (GroupBox mainGroupBox in this.Controls.OfType<GroupBox>())
            {
                //Nasa loob kasi ng groupbox ang Tab Control
                foreach (TabControl tabControl in mainGroupBox.Controls.OfType<TabControl>())
                {   //Nasa loob kasi ng tab control ang tab page
                    foreach (TabPage tabPage in tabControl.Controls.OfType<TabPage>())
                    {   //Saka ko pa lang makukuha ng Group Boxes ng mga authrozation. Now you know.....
                        foreach (GroupBox groupBox in tabPage.Controls.OfType<GroupBox>())
                        {

                            //get groupbox initials
                            formInitial = groupBox.Name.Substring(groupBox.Name.Length - 2, 2);

                            //Check which radiobutton is selected
                            foreach (RadioButton radioButton in groupBox.Controls.OfType<RadioButton>())
                            {
                                if (radioButton.Checked)
                                {
                                    string selectedRd = radioButton.Text;
                                    

                                    switch (selectedRd)
                                    {
                                        case "YES":
                                            radioButtonValue = 1;
                                            break;
                                        case "NO":
                                            radioButtonValue = 2;
                                            break;
                                        case "READ ONLY":
                                            radioButtonValue = 3;
                                            break;
                                    }

                                    //get ReadWriteValues
                                    foreach (Panel panel in groupBox.Controls.OfType<Panel>())
                                    {
                                        foreach (CheckBox checkBox in panel.Controls.OfType<CheckBox>())
                                        {
                                            if (checkBox.Text == "Create Record")
                                            {
                                                isCreateAllowed = Convert.ToInt32(checkBox.CheckState);
                                            }
                                            if (checkBox.Text == "Edit Record")
                                            {
                                                isEditAllowed = Convert.ToInt32(checkBox.CheckState);
                                            }
                                            if (checkBox.Text == "Delete Record")
                                            {
                                                isDeleteAllowed = Convert.ToInt32(checkBox.CheckState);
                                            }
                                        }
                                    }

                                    //Update UserLevels Record
                                    if (!proc.UpdateUserLevelRecord(userLevelCode, formInitial, radioButtonValue, isCreateAllowed, isEditAllowed, isDeleteAllowed))
                                    {
                                        p.MessageAndLog("Error Updating User Level Record (UpdateUserLevelRecord)\r\n \r\n" + proc.errorMessage, ref log, "error");
                                    }

                                }

                            }

                        }

                    }

                }

            }

        }

        private void btnSaveRecord_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        private void txtUserLeveCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                log.Info("Pressed Enter on Keyboard with text on txtUserLeveCode: " + txtUserLeveCode.Text.ToString());
               

            }
        }

        private void DisplayOldUserLevelDetails(string userLevelCode)
        {
            DataTable dt = new DataTable();

            log.Info("Fetching Existing User Level Record");
            //Start Progress Bar View
            progressBar = new frmProgress();
            progressBar.message = "Fetching Existing Record. Please Wait.";
            thread = new Thread(() => progressBar.ShowDialog());
            thread.Start();

            //Get Sales Invoice List Details to be supplied to Global Report Datatable
            if (!proc.GetUserLevelDetails(userLevelCode, ref dt))
            {
                thread.Abort();
                MessageBox.Show("Unable to connect to server. (proc.SalesInvoiceExist)\r\n" + proc.errorMessage);
                RefreshView();
                return;
            }


            //Display values on Front End from Finished Table
            foreach (DataRow row in dt.Rows)
            {

                txtUserLeveCode.Text = row.Field<string>("UserLevelCode");
                txtUserLevelName.Text = row.Field<string>("UserLevelName");

                FillUserLevelGroupBoxes(row);

                //gSalesInvoiceFinished.ClientCode = row.Field<string>("ClientCode");
                //gSalesInvoiceFinished.SalesInvoiceDateTime = row.Field<DateTime>("SalesInvoiceDateTime");
                //gSalesInvoiceFinished.GeneratedBy = row.Field<string>("GeneratedBy");
                //gSalesInvoiceFinished.CheckedBy = row.Field<string>("CheckedBy");
                //gSalesInvoiceFinished.ApprovedBy = row.Field<string>("ApprovedBy");
                //gSalesInvoiceFinished.SalesInvoiceNumber = row.Field<double>("SalesInvoiceNumber");
                //gSalesInvoiceFinished.TotalAmount = row.Field<double>("TotalAmount");
                //gSalesInvoiceFinished.VatAmount = row.Field<double>("VatAmount");
                //gSalesInvoiceFinished.NetOfVatAmount = row.Field<double>("NetOfVatAmount");

                //dtpInvoiceDate.Value = gSalesInvoiceFinished.SalesInvoiceDateTime;
                //cbCheckedBy.Text = gSalesInvoiceFinished.CheckedBy;
                //cbApprovedBy.Text = gSalesInvoiceFinished.ApprovedBy;

            }
            thread.Abort();

        }

        private void FillUserLevelGroupBoxes(DataRow row)
        {
            string fieldNameToFetch;
            string formInitial;
            int radioButtonValue;
            int isCreateAllowed;
            int isEditAllowed;
            int isDeleteAllowed;



            foreach (GroupBox mainGroupBox in this.Controls.OfType<GroupBox>())
            {
                //Nasa loob kasi ng groupbox ang Tab Control
                foreach (TabControl tabControl in mainGroupBox.Controls.OfType<TabControl>())
                {   //Nasa loob kasi ng tab control ang tab page
                    foreach (TabPage tabPage in tabControl.Controls.OfType<TabPage>())
                    {   //Saka ko pa lang makukuha ng Group Boxes ng mga authrozation. Now you know.....
                        foreach (GroupBox groupBox in tabPage.Controls.OfType<GroupBox>())
                        {

                            //get groupbox initials
                            formInitial = groupBox.Name.Substring(groupBox.Name.Length - 2, 2);
                            fieldNameToFetch = "isAllowedOn" + formInitial;
                            int fieldNameToFetchValue = row.Field<int>(fieldNameToFetch);

                            switch (fieldNameToFetchValue)
                            {
                                case 1:
                                    foreach (RadioButton radiobutton in groupBox.Controls.OfType<RadioButton>())
                                    {
                                        if (radiobutton.Name == "YES") { radiobutton.Checked = true; }
                                    }
                                    break;
                                case 2:
                                    foreach (RadioButton radiobutton in groupBox.Controls.OfType<RadioButton>())
                                    {
                                        if (radiobutton.Name == "NO") { radiobutton.Checked = true; }
                                    }
                                    break;
                                case 3:
                                    foreach (RadioButton radiobutton in groupBox.Controls.OfType<RadioButton>())
                                    {
                                        if (radiobutton.Name == "READ\r\nONLY") { radiobutton.Checked = true; }
                                    }
                                    break;
                            }


                            
                                
                           

                            ////get ReadWriteValues
                            //foreach (Panel panel in groupBox.Controls.OfType<Panel>())
                            //{
                            //    foreach (CheckBox checkBox in panel.Controls.OfType<CheckBox>())
                            //    {
                            //        if (checkBox.Text == "Create Record")
                            //        {
                            //            isCreateAllowed = Convert.ToInt32(checkBox.CheckState);
                            //        }
                            //        if (checkBox.Text == "Edit Record")
                            //        {
                            //            isEditAllowed = Convert.ToInt32(checkBox.CheckState);
                            //        }
                            //        if (checkBox.Text == "Delete Record")
                            //        {
                            //            isDeleteAllowed = Convert.ToInt32(checkBox.CheckState);
                            //        }
                            //    }
                            //}

                                   

                                

                            

                        }

                    }

                }

            }
        }



        private void txtUserLeveCode_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

