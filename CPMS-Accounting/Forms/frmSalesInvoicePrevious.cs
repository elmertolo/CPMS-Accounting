using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProducersBank.Services;
using static ProducersBank.GlobalVariables;
using ProducersBank.Models;

namespace ProducersBank
{
    
    public partial class frmSalesInvoicePrevious : Form
    {

        List<SalesInvoiceModel> SalesInvoiceList = new List<SalesInvoiceModel>();

        ProcessServices_Nelson p = new ProcessServices_Nelson();

        public frmSalesInvoicePrevious()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchText();
        }

        private void SearchText()
        {
           
            
            if (string.IsNullOrWhiteSpace(txtSearch.Text.ToString()))
            {
                MessageBox.Show("Please input SalesInvoice number to search");
            }
            else
            {
                DataTable dt = new DataTable();
                if (!p.SalesInvoiceSearch(txtSearch.Text, ref dt))
                {
                    MessageBox.Show("Unable to connect to server. (proc.BatchSearch)\r\n" + p.errorMessage);
                    return;
                }

                gReportName = "SalesInvoiceReprint";
                gReportDT = ;
                gSalesInvoiceDate = dt.Rows[0].Field;
                gSalesInvoicePreparedBy = cbPreparedBy.Text.ToString();
                gSalesinvoiceCheckedBy = cbCheckedBy.Text.ToString();
                gSalesInvoiceApprovedBy = cbApprovedBy.Text.ToString();
                gSalesInvoiceNumber = double.Parse(txtSalesInvoiceNumber.Text.ToString());
                gSalesInvoiceSubtotalAmount = double.Parse(SalesInvoiceList.Sum(x => x.lineTotalAmount).ToString());
                gSalesInvoiceVatAmount = p.GetVatAmount(gSalesInvoiceSubtotalAmount);
                gSalesInvoiceNetOfVatAmount = p.GetNetOfVatAmount(gSalesInvoiceSubtotalAmount);
                gSalesInvoicegeneratedBy = lblUserName.Text.ToString();


                //_ = dt.Rows.Count != 0 ? dgvDRList.DataSource = dt : MessageBox.Show("No results found");
                txtSearch.Focus();
                txtSearch.SelectAll();
                
            }
        }



    }
}
