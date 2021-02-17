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
    public partial class frmChequeTypes : Form
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        Main frm;
        public frmChequeTypes(Main frm1)
        {
            InitializeComponent();
            this.frm = frm1;
        }
        List<ChequeTypesModel> listofCheques = new List<ChequeTypesModel>();
        ChequeTypesModel cheque = new ChequeTypesModel();
        ProcessServices proc = new ProcessServices();
        List<ChequeProductModel> productList = new List<ChequeProductModel>();
        DataTable dt = new DataTable();
        int liaddmod = 0;
        //  List<int> pCode = new List<int>();
        int pCode = 0;

        private void LoadProducts()
        {
            proc.GetProducts(productList);
          
            productList.ForEach(x =>
            {
                cmbProducts.Items.Add(x.ProductName);
               // pCode.Add(x.ProductCode);
            });
            cmbProducts.SelectedIndex = 0;
        }
        private void DynamicCheques()
        {
            for (int i = 0; i < productList.Count; i++)
            {
                if (cmbProducts.SelectedIndex == i)
                {
                    txtCheckName.Text = productList[i].ProductName.Substring(0, productList[i].ProductName.Length - 6);
                    txtDescription.Text = productList[i].ProductName.Substring(0, productList[i].ProductName.Length - 6);
                    pCode = productList[i].ProductCode;
                }
                

            }
        }

        private void DisplayAllCheques()
        {
            listofCheques.Clear();
            proc.GetChequeTypes(listofCheques);

            dt.Clear();
            dt.Columns.Clear();
            dt.Columns.Add("Type");
            dt.Columns.Add("Cheque Name");
            dt.Columns.Add("Description");
            dt.Columns.Add("Date Modified");
            

            listofCheques.ForEach(x =>
            {
                dt.Rows.Add(new object[] { x.Type, x.ChequeName, x.Description, x.DateModified.ToString("yyyy-MM-dd") });
            });

            DgvCheques.DataSource = dt;
            ProcessServices.bg_dtg(DgvCheques);
            DgvCheques.Columns[0].Width = 50;
            DgvCheques.Columns[1].Width = 150;
            DgvCheques.Columns[2].Width = 200;
            DgvCheques.Columns[3].Width = 100;


        }
        private void EnableControls(bool _bool, int _addmod)
        {
            txtType.Enabled = _bool;
            cmbProducts.Enabled = _bool;
            txtDescription.Enabled = _bool;
            txtCheckName.Enabled = _bool;
            //_item.Enabled = _bool;
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
          
            txtDescription.Text = "";
            txtCheckName.Text = "";
            txtType.Text = "";
         
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void frmChequeTypes_Load(object sender, EventArgs e)
        {
            DisplayAllCheques();
            LoadProducts();
            addToolStripMenuItem.Enabled = true;
            modifyToolStripMenuItem.Enabled = true;

            EnableControls(false, liaddmod);

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
               // var prodCode = pCode.Where(x => x == );
                cheque.ProductCode = pCode;
                cheque.Type = txtType.Text;
                cheque.ChequeName = txtCheckName.Text;
                cheque.Description = txtDescription.Text;
                cheque.DateModified = DateTime.Now;


                if (liaddmod == 2)
                {
                    proc.AddChequeType(cheque);
                    MessageBox.Show("Data has been Added!!!", "Saving Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (liaddmod == 1)
                {
                    proc.ModifyChequeTypes(cheque);
                    MessageBox.Show("Data has been Updated!!!", "Updating Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                ClearTools();
                liaddmod = 0;
                EnableControls(false, liaddmod);
                DisplayAllCheques();
            }
            else
                MessageBox.Show("Process has been cancelled!!!", "Cancel Process", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
         
        }

        private void txtCheckName_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void cmbProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            DynamicCheques();
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

            txtType.Text = DgvCheques.Rows[rowindex].Cells[columnindex].Value.ToString();
            txtCheckName.Text = DgvCheques.Rows[rowindex].Cells[columnindex + 1].Value.ToString();
            txtDescription.Text = DgvCheques.Rows[rowindex].Cells[columnindex + 2].Value.ToString();
        }
    }
}
