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
using CPMS_Accounting.Models;

namespace CPMS_Accounting.Forms
{
    public partial class frmBranches : Form
    {
        public frmBranches()
        {
            InitializeComponent();
        }

        ProcessServices proc = new ProcessServices();
        BranchesModel branch = new BranchesModel();
        int AddorUpdate = 0;
        private void frmBranches_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmOrdering main = new frmOrdering();
            main.Show();
            this.Hide();
        }

        private void frmBranches_Load(object sender, EventArgs e)
        {

            WindowState = FormWindowState.Maximized;
            isActive();
        }
        private void isActive()
        {
            
            dgvBranchList.DataSource = "";
            DataTable dt = new DataTable();
            DtableColumns(dt);

            frmOrdering.listofBranches.ForEach(x => {

                dt.Rows.Add(new object[] { x.BRSTN, x.Address1, x.Address2, x.Address3, x.Address4, x.Address5, x.Address6, x.BranchCode });
            });

            dgvBranchList.DataSource = dt;
            ProcessServices.bg_dtg(dgvBranchList);
            DataGridDesign();

        }
        private void DataGridDesign()
        {
            dgvBranchList.Columns[0].Width = 90;
            dgvBranchList.Columns[1].Width = 250;
            dgvBranchList.Columns[2].Width = 250;
            dgvBranchList.Columns[3].Width = 250;
            dgvBranchList.Columns[4].Width = 200;
            dgvBranchList.Columns[5].Width = 130;
            dgvBranchList.Columns[6].Width = 100;
        }
        private void DtableColumns(DataTable dt)
        {
             
            dt.Columns.Add("BRSTN");
            dt.Columns.Add("BRANCH NAME");
            dt.Columns.Add("ADDRESS2");
            dt.Columns.Add("ADDRESS3");
            dt.Columns.Add("ADDRESS4");
            dt.Columns.Add("ADDRESS5");
            dt.Columns.Add("ADDRESS6");
            dt.Columns.Add("BRANCH CODE");
        }
        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            proc.DisableControls(saveToolStripMenuItem);
            updateToolStripMenuItem.Enabled = false;
            AddorUpdate = 1;
            RefreshControls(true);
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            proc.DisableControls(saveToolStripMenuItem);
            generateToolStripMenuItem.Enabled = false;
            AddorUpdate = 2;
            RefreshControls(true);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DataInialization(branch);
            if (branch != null)
            {
                if (AddorUpdate == 1)
                {
                    proc.fAddOrUPdate(branch, AddorUpdate);
                }
                else if (AddorUpdate == 2)
                {
                    proc.fAddOrUPdate(branch, AddorUpdate);
                }

                RefreshControls(false);
            }
            else
                MessageBox.Show("Data is Null!!!", "Iniatilization", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private BranchesModel DataInialization(BranchesModel branch)
        {
            branch.BRSTN = txtBrstn.Text;
            branch.Address1 = txtAddress1.Text;
            branch.Address2 = txtAddress2.Text;
            if (txtAddress3.Text != null)
                branch.Address3 = txtAddress3.Text;
            else
                branch.Address3 = "";
            if (txtAddress4.Text != null)
                branch.Address4 = txtAddress4.Text;
            else
                branch.Address4 = "";
            if (txtAddress5.Text != null)
                branch.Address5 = txtAddress5.Text;
            else
                branch.Address5 = "";
            if (txtAddress6.Text != null)
                branch.Address6 = txtAddress6.Text;
            else
                branch.Address6 = "";
            if (txtBranchCode.Text != null)
                branch.BranchCode = txtBranchCode.Text;
            else
                branch.BranchCode = "0000";

            return branch;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            txtSearch.CharacterCasing = CharacterCasing.Upper;
            dgvBranchList.DataSource = "";
            DataTable dt = new DataTable();
            dt.Clear();
            DtableColumns(dt);
            var b = frmOrdering.listofBranches.Where(x => x.BRSTN.Contains(txtSearch.Text) || x.Address1.Contains(txtSearch.Text)).ToList();
            b.ForEach(x => { 
                dt.Rows.Add(new object[] {x.BRSTN, x.Address1, x.Address2, x.Address3, x.Address4, x.Address5, x.Address6, x.BranchCode });
            });

            dgvBranchList.DataSource = dt;
            ProcessServices.bg_dtg(dgvBranchList);
            DataGridDesign();
            
        }
        private void RefreshControls(bool _method)
        {
            txtBrstn.MaxLength = 9;
            
            txtBrstn.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtAddress3.Text = "";
            txtAddress4.Text = "";
            txtAddress5.Text = "";
            txtAddress6.Text = "";
            txtBranchCode.Text = "";
            txtBrstn.Enabled = _method;
            txtAddress1.Enabled = _method;
            txtAddress2.Enabled = _method;
            txtAddress3.Enabled = _method;
            txtAddress4.Enabled = _method;
            txtAddress5.Enabled = _method;
            txtAddress6.Enabled = _method;
            txtBranchCode.Enabled = _method;

        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshControls(false);
            updateToolStripMenuItem.Enabled = true;
            generateToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Enabled = false;
        }
    }
}
