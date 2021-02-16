using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CPMS_Accounting.Models;
using CPMS_Accounting.Procedures;

namespace CPMS_Accounting.Forms
{
    public partial class frmChequeProducts : Form
    {
        Main frm;
        public frmChequeProducts(Main frm1)
        {
            InitializeComponent();
            this.frm = frm1;
        }
        List<ChequeProductModel> productList = new List<ChequeProductModel>();
        ChequeProductModel product = new ChequeProductModel();
        ProcessServices proc = new ProcessServices();
        DataTable dt = new DataTable();
        int liaddmod = 0;
        private void DisplayAllProducts()
        {
            productList.Clear();
            proc.GetProducts(productList);

            dt.Clear();
            dt.Columns.Clear();
            dt.Columns.Add("Product Code");
            dt.Columns.Add("Product Name");
            dt.Columns.Add("Date Modified");


            productList.ForEach(x =>
            {
                dt.Rows.Add(new object[] { x.ProductCode, x.ProductName, x.DateModified.ToString("yyyy-MM-dd") });
            });

            DgvCheques.DataSource = dt;
            ProcessServices.bg_dtg(DgvCheques);
            DgvCheques.Columns[0].Width = 70;
            DgvCheques.Columns[1].Width = 210;
            DgvCheques.Columns[2].Width = 95;
            


        }
        private void EnableControls(bool _bool, int _addmod)
        {
            txtPcode.Enabled = _bool;
            txtProductName  .Enabled = _bool;
            
            if (_addmod == 1)
            {
                addToolStripMenuItem.Enabled = false;
            }
            else if (_addmod == 2)
                modifyToolStripMenuItem.Enabled = false;
            else
            {
                addToolStripMenuItem.Enabled = true;
                modifyToolStripMenuItem.Enabled = true;
            }

            saveToolStripMenuItem.Enabled = _bool;
        }
        private void ClearTools()
        {

            txtPcode.Text = "";
            //  txtProductName.Text = "";
            txtProductName.Text = "";

        }
        private void frmChequeProducts_Load(object sender, EventArgs e)
        {
            
            addToolStripMenuItem.Enabled = true;
            modifyToolStripMenuItem.Enabled = true;

            EnableControls(false, liaddmod);
            DisplayAllProducts();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            liaddmod = 2;
            EnableControls(true, liaddmod);
        }

        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            liaddmod = 1;
            EnableControls(true, liaddmod);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to save this data?", "Delivery Receipt Number Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                product.ProductCode = int.Parse(txtPcode.Text);
              
                product.ProductName = txtProductName.Text;
                product.DateModified = DateTime.Now;


                if (liaddmod == 2)
                {
                    proc.AddProducts(product);
                    MessageBox.Show("Data has been Added!!!", "Saving Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (liaddmod == 1)
                {
                    proc.ModifyProduct(product);
                    MessageBox.Show("Data has been Updated!!!", "Updating Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                ClearTools();
                liaddmod = 0;
                EnableControls(false, liaddmod);
                DisplayAllProducts();
            }
            else
                MessageBox.Show("Process has been cancelled!!!", "Cancel Process", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            liaddmod = 0;
            EnableControls(false, liaddmod);

            ClearTools();
        }

        private void DgvCheques_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = DgvCheques.CurrentCell.RowIndex;
            int columnindex = DgvCheques.CurrentCell.ColumnIndex;

            // student.Stud_ID = int.Parse(dtgList.Rows[rowindex].Cells[columnindex].Value.ToString());

            txtPcode.Text = DgvCheques.Rows[rowindex].Cells[columnindex].Value.ToString();
            txtProductName.Text = DgvCheques.Rows[rowindex].Cells[columnindex+1].Value.ToString();
        }
    }
}
