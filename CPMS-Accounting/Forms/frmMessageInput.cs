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
//Added for RCBC
using static CPMS_Accounting.GlobalVariables;

namespace CPMS_Accounting
{
    public partial class frmMessageInput : Form
    {
        //02152021 Log4Net
        private log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public string labelMessage1;
        public string labelMessage2;
        public string userInput;
        

        public frmMessageInput()
        {
            InitializeComponent();
            lblMessage2.Text = labelMessage2;
            lblMessage2.Visible = false;

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInput.Text))
            {
                MessageBox.Show("Please input Sales Invoice Number.");
                return;
            }
            
            userInput = txtInput.Text.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (p.IsKeyPressedNumeric(ref sender, ref e))
            {
                e.Handled = true;
            }
        }

        private void frmMessageInput_Load(object sender, EventArgs e)
        {
            //Set Control locations if message 2 has value.
            if (!string.IsNullOrEmpty(labelMessage2))
            {
                lblMessage2.Visible = true;
                lblMessage2.Location = new Point(12, 41);
                this.Size = new Size(326, 180);
                txtInput.Location = new Point(12, 67);
                btnOk.Location = new Point(142, 106);
                btnCancel.Location = new Point(223, 106);
                
            }
           
            lblMessage1.Text = labelMessage1;
            lblMessage2.Text = labelMessage2;
            txtInput.SelectAll();
            txtInput.Text = userInput;
            txtInput.Focus();

            log.Info("frmMessage Loaded. " + lblMessage1.Text.Replace(':', ' '));

        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(txtInput.Text))
                {
                    MessageBox.Show("Please input Sales Invoice Number.");
                    return;
                }
                userInput = txtInput.Text.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (p.IsKeyPressedNumeric(ref sender, ref e))
            {
                e.Handled = true;
            }
        }








    }
}
