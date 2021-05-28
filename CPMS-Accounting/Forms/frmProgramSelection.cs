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
    public partial class frmProgramSelection : Form
    {
        public static string selectSystem;
        public frmProgramSelection()
        {
            InitializeComponent();
        }
        private void SelectProgram()
        {
            cmbProgram.Items.Add("Ordering System");
            cmbProgram.Items.Add("Accounting System");
            cmbProgram.SelectedIndex = 0;
        }

        private void frmProgramSelection_Load(object sender, EventArgs e)
        {
            
            SelectProgram();

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            selectSystem = cmbProgram.Text;
            frmLogIn frm = new frmLogIn();
            frm.Show();
            this.Hide();
        }

        private void cmbProgram_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                selectSystem = cmbProgram.Text;
                frmLogIn frm = new frmLogIn();
                frm.Show();
                this.Hide();
            }
        }
    }
}
