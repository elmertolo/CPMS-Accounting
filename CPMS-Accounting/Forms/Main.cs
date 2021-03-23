using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CPMS_Accounting.GlobalVariables;
using CPMS_Accounting.Forms;
using CPMS_Accounting.Models;
using System.Diagnostics;
using CPMS_Accounting.Procedures;

namespace CPMS_Accounting
{
    public partial class Main : Form
    {


        ProcessServices_Nelson proc = new ProcessServices_Nelson();

        //02152021 Log4Net
        private log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Main()
        {
            InitializeComponent();
            
        }

        private void btnDR_Click(object sender, EventArgs e)
        {
            //DeliveryReport dr = new DeliveryReport();
            //dr.Show();
            //this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //RecentBatch rb = new RecentBatch();
            //rb.Show();
            //this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //frmSalesInvoice si = new frmSalesInvoice();
            //si.Show();
            //this.Hide();

        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!new StackTrace().GetFrames().Any(x => x.GetMethod().Name == "Close"))
            {
                log.Info("Form Closed by Pressing X or Alt-F4");
            } 

            Environment.Exit(0);

        }

        private void deliveryReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log.Info("Mouse Click ToolStripMenuItem (Delivery Receipt)");
            if (gUser.IsAllowedOnDr == 2)
            {
                p.MessageAndLog("You do not have permission to do this operation. \r\nPlease contact Administrator for more information.", ref log, "info");
                return;
            }

            Form frm = new DeliveryReport(this);
            frm.ShowDialog();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            

            WindowState = FormWindowState.Maximized;
           // if (gClient.DataBaseName != "producers_history")
           // {
                documentStampToolStripMenuItem.Enabled = true;
               
            //}
           // else
              //  documentStampToolStripMenuItem.Enabled = false;

            this.Text = gClient.Description;

            log.Info("Main Form Loaded");
            
        }

        private void recentBatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log.Info("Clicked ToolStripMenuItem (Recent Batch)");
            Form frm = new RecentBatch(this);
            frm.ShowDialog();
        }

        private void salesInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log.Info("Mouse Click ToolStripMenuItem (Sales Invoice)");
            if (gUser.IsAllowedOnSi == 2)
            {
                p.MessageAndLog("You do not have permission to do this operation. \r\nPlease contact Administrator for more information.", ref log, "info");
                return;
            }

            Form frm = new frmSalesInvoice(this);
            frm.ShowDialog();
        }

        private void documentStampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log.Info("Clicked ToolStripMenuItem (Document Stamp)");
            Form frm = new frmDocStamp(this);
            frm.ShowDialog();
        }

        private void purchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log.Info("Clicked ToolStripMenuItem (Purchase Order)");
            if (gUser.IsAllowedOnPo == 2)
            {
                p.MessageAndLog("You do not have permission to do this operation. \r\nPlease contact Administrator for more information.", ref log, "info");
                return;
            }
            if (gClient.ShortName != "PNB")
            {
                MessageBox.Show("Purchase Order Feature is applicable on PNB transctions only for the meantime", "Ooooops..");
                return;
            }
            frmPurchaseOrder poFrm = new frmPurchaseOrder(this);
            poFrm.Show();
        }

        private void changeBankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log.Info("Clicked ToolStripMenuItem (Change Bank)");
            Form frm = new frmLogIn();
            frm.Show();
            this.Hide();
         }

        private void changeDRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log.Info("Clicked ToolStripMenuItem (Data Correction)");
            if (gUser.IsAllowedOnDc == 2)
            {
                p.MessageAndLog("You do not have permission to do this operation. \r\nPlease contact Administrator for more information.", ref log, "info");
                return;
            }
            Form frm = new frmCorrection(this);
            frm.ShowDialog();
        }

        private void chequesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log.Info("Clicked ToolStripMenuItem (Product Price List)");
            if (gUser.IsAllowedOnPm == 2)
            {
                p.MessageAndLog("You do not have permission to do this operation. \r\n Please contact Administrator for more information.", ref log, "info");
                return;
            }
            Form frm = new frmProductPriceList(this);
            frm.ShowDialog();
        }

        private void AddchequesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new frmChequeTypes(this);
            frm.ShowDialog();
        }

        private void chequeProductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmChequeProducts(this);
            frm.ShowDialog();
        }

        private void transactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log.Info("Mouse Click ToolStripMenuItem (TRANSACTIONS)");
        }

        private void userMaintenanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log.Info("Mouse Click ToolStripMenuItem (User Maintenance)");
            if (gUser.IsAllowedOnUm == 2)
            {
                p.MessageAndLog("You do not have permission to do this operation. \r\nPlease contact Administrator for more information.", ref log, "info");
                return;
            }

            Form frm = new frmUserMaintenance(this);
            frm.ShowDialog();
        }

        private void userLevelAndSecurityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log.Info("Mouse Click ToolStripMenuItem (User Level Management)");
            if (gUser.IsAllowedOnUl == 2)
            {
                p.MessageAndLog("You do not have permission to do this operation. \r\nPlease contact Administrator for more information.", ref log, "info");
                return;
            }

            Form frm = new frmUserLevelManagement(this);
            frm.ShowDialog();
        }






    }
}
