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
    public partial class frmProgress : Form
    {
        public delegate void ToDoDelegate();

        public frmProgress()
        {
            InitializeComponent();
        }

        private void frmProgress_Load(object sender, EventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Marquee;


            //do this when closing
            

            
        }
    }
}
