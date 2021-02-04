using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CPMS_Accounting.Procedures;
using static CPMS_Accounting.GlobalVariables;
using CPMS_Accounting.Models;

namespace CPMS_Accounting.Forms
{
    public partial class frmDocStamp : Form
    {
        Main frm1;
        public frmDocStamp(Main frm)
        {
            InitializeComponent();
            this.frm1 = frm;
        }
        ProcessServices proc = new ProcessServices();
        List<SalesInvoiceModel> listofSI = new List<SalesInvoiceModel>();
        List<DocStampModel> docstamp = new List<DocStampModel>();
        List<DocStampModel> tempdocstamp = new List<DocStampModel>();
        List<int> _temp = new List<int>();
        List<PriceListModel> priceList = new List<PriceListModel>();
        PriceListModel priceA = new PriceListModel();
        List<TempModel> tempSI = new List<TempModel>();
        List<UserListModel> users = new List<UserListModel>();
        int TotalQty = 0;
        Int32 _dr = 0;
        List<Int32> iDocStampNumber ;
        private void frmDocStamp_Load(object sender, EventArgs e)
        {
            lblUser.Text = gUser.FirstName;
          //  LoadUsers(cboPreparedBy);
            LoadUsers(cboCheckBy);
           
        }
        private void GetDocStampNumber()
        {
            //    Int64 liCnt = 1;
            // Int64 liCount = 0;
            iDocStampNumber =  proc.GetMaxDocStamp();

            // Int64.Parse(txtDrNumber.Text)
            for (int i = 0; i < iDocStampNumber.Count; i++)
            {
                if (_dr > iDocStampNumber[i])
                {

                }
                else
                    _dr = iDocStampNumber[i];

            }
            txtDocStampNo.Text = (_dr + 1).ToString();
            return;

        }
        private void LoadUsers(ComboBox combo)
        {
            proc.GetUsers(users);
            users.ForEach(u => {
                combo.Items.Add(u.UserName);
            });
            combo.SelectedIndex = 0;
        }
        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (docstamp != null || docstamp.Count != 0)
                {
                    proc.UpdateDocstamp(docstamp);
                    docstamp.ForEach(x => {

                        proc.GetDocStampDetails(tempdocstamp, x.DocStampNumber);
                    });
                    
                    MessageBox.Show("Documetn Stamp has been process!!!");
                    ViewReports vp = new ViewReports();
                    DeliveryReport.report = "DOC";
                    vp.Show();
                }
                else
                    MessageBox.Show("No document stamp data!!");
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message, error.Source);
            }
        }

        private void txtBatch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                tempSI.Clear();
                proc.DisplayAllSalesInvoice(txtBatch.Text, tempSI);
                DataTable dt = new DataTable();

                dt.Clear();

                dt.Columns.Add("Sales Invoice Date");
                dt.Columns.Add("Sales Invoice No.");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("ChkType");
                dt.Columns.Add("Cheque Name");
                dt.Columns.Add("Batch");
                dt.Columns.Add("Branch Location");

                tempSI.ForEach(r =>
                {
                    dt.Rows.Add(new object[] { r.SI_Date.ToString("yyyy-MM-dd"), r.SalesInvoice, r.Qty, r.ChkType, r.ChequeName, r.Batch,r.Location });
                });

                DgvDSalesInvoice.DataSource = dt;
                DgvDSalesInvoice.Columns[0].Width = 150;
                DgvDSalesInvoice.Columns[1].Width = 150;
                DgvDSalesInvoice.Columns[2].Width = 70;
                DgvDSalesInvoice.Columns[3].Width = 70;
                DgvDSalesInvoice.Columns[4].Width = 190;
                DgvDSalesInvoice.Columns[5].Width = 100;
                DgvDSalesInvoice.Columns[6].Width = 100;
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message, error.Source);
            }
        }

        private void btnGenerateDocStampNo_Click(object sender, EventArgs e)
        {
            GetDocStampNumber();
            MessageBox.Show("Getting Document Stamp Number Succesful!");
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (txtDocStampNo.Text == "")
                    MessageBox.Show("Please input Document Stamp Number!");

                else
                {
                    docstamp.Clear();

                    int docs = int.Parse(txtDocStampNo.Text);
                   
                    if (DgvDSalesInvoice.SelectedRows != null && DgvDSalesInvoice.SelectedRows.Count > 0)
                    {
                       
                        foreach (DataGridViewRow row in DgvDSalesInvoice.SelectedRows)
                        {
                            DocStampModel doc = new DocStampModel();
                            doc.DocStampNumber = docs;
                            proc.GetPriceList(priceA, row.Cells["ChkType"].Value.ToString());
                            

                            doc.BankCode = priceA.Bank;
                          
                            doc.DocStampDate = dtpDocDate.Value;
                            doc.batches = row.Cells["Batch"].Value.ToString();
                            doc.SalesInvoiceNumber = proc.ContcatSalesInvoice(row.Cells["Batch"].Value.ToString(), row.Cells["ChkType"].Value.ToString(), dtpDocDate.Value);
                            doc.DocStampPrice = priceA.DocStampPrice;
                            doc.ChkType = row.Cells["ChkType"].Value.ToString();
                            doc.DocDesc = priceA.ChequeDescription;
                            doc.Location = row.Cells["Branch Location"].Value.ToString();
                            // doc.unitprice = priceA.unitprice;
                            doc.TotalQuantity = int.Parse(row.Cells["Quantity"].Value.ToString());
                            doc.TotalAmount = doc.TotalQuantity * doc.DocStampPrice;
                            // doc.PreparedBy = 
                            

                            docstamp.Add(doc);
                            docs++;
                            TotalQty += doc.TotalQuantity;
                            
                        }
                       
                        //created 'list' variable column sorting by line for datagrid view 
                        //var sortedList = docstamp
                        //    .Select
                        //    (i => new { i.batches, i.SalesInvoiceNumber, i.TotalQuantity, i.DocStampPrice, i.TotalAmount }).ToList();

                        DataTable dt = new DataTable();

                        dt.Columns.Add("Docstamp No.");
                        dt.Columns.Add("Sales Invoice No.");
                        dt.Columns.Add("Quantity");
                        dt.Columns.Add("Description");
                        dt.Columns.Add("Docstamp Price");
                        dt.Columns.Add("Total Amount");
                        dt.Columns.Add("Processed Date");

                        docstamp.ForEach(r =>
                        {
                            dt.Rows.Add(new object[] { r.DocStampNumber, r.SalesInvoiceNumber, r.TotalQuantity, r.DocDesc, r.DocStampPrice, r.TotalAmount, r.DocStampDate.ToString("yyyy-MM-dd") });
                        });
                        dgvOutput.DataSource = dt;
                        dgvOutput.Columns[0].Width = 100;
                        dgvOutput.Columns[1].Width = 120;
                        dgvOutput.Columns[2].Width = 70;
                        dgvOutput.Columns[3].Width = 270;
                        dgvOutput.Columns[4].Width = 70;
                        dgvOutput.Columns[5].Width = 90;
                        dgvOutput.Columns[6].Width = 100;
                        dgvOutput.ClearSelection();
                        txtTotalQty.Text = TotalQty.ToString();
                    }
                }
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message, error.Source);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            docstamp.Clear();
            dgvOutput.DataSource = null;
            //dgvOutput.Rows.Clear();
            dgvOutput.Refresh();
            TotalQty = 0;
            txtTotalQty.Text = "0";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
