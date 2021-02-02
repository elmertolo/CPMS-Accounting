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

namespace CPMS_Accounting
{
    public partial class Main : Form
    {
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
            Environment.Exit(0);
            

        }

        private void deliveryReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new DeliveryReport(this);
            frm.ShowDialog();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            
                WindowState = FormWindowState.Maximized;
            if (gClient.DataBaseName != "producers_history")
            {
                documentStampToolStripMenuItem.Enabled = true;
               
            }
            else
                documentStampToolStripMenuItem.Enabled = false;

            this.Text = gClient.Description;
        }

        private void recentBatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new RecentBatch(this);
            frm.ShowDialog();
        }

        private void salesInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmSalesInvoice(this);
            frm.ShowDialog();
        }

        private void documentStampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmDocStamp(this);
            frm.ShowDialog();
        }
        

        private void purchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPurchaseOrder poFrm = new frmPurchaseOrder(this);
            poFrm.Show();
        }

        private void changeBankToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            Form frm = new frmLogIn();
            frm.Show();
            this.Hide();
         }
    }
}
