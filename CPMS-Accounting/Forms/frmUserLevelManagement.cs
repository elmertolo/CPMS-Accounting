using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPMS_Accounting.Forms
{
    public partial class frmUserLevelManagement : Form
    {

        //02152021 Log4Net
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

            CheckAllRadioButtons();
            DisableControls();
            ActionPanelInitialLoadView();
            txtUserLeveNo.Focus();
        }

        private void CheckAllRadioButtons()
        {
            foreach (GroupBox gbox in this.Controls.OfType<GroupBox>())
            {

                foreach (TabControl tab in gbox.Controls.OfType<TabControl>())
                {

                    foreach (GroupBox gbox2 in tab.Controls.OfType<GroupBox>())
                    {


                        foreach (RadioButton rdBtn in gbox2.Controls.OfType<RadioButton>())
                        {
                            if (!rdBtn.Name.Contains("Yes"))
                            {
                                rdBtn.Checked = true;
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
                gbDetails.Enabled = true;
            }

            private void ActionPanelInitialLoadView()
            {
                btnRefreshView.Enabled = false;
                btnAddRecord.Enabled = true;
                btnEditRecord.Enabled = false;
                btnDeleteRecord.Enabled = false;
                btnSaveRecord.Enabled = false;
            }

            private void ActionPanelNewRecordView()
            {
                btnRefreshView.Enabled = true;
                btnAddRecord.Enabled = false;
                btnEditRecord.Enabled = false;
                btnDeleteRecord.Enabled = false;
                btnSaveRecord.Enabled = true;
            }

            private void ActionPanelEditRecordView()
            {
                btnRefreshView.Enabled = true;
                btnAddRecord.Enabled = false;
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


    }
}

