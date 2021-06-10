using CPMS_Accounting.Models;
using CPMS_Accounting.Procedures;
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
    public partial class frmManualEncode : Form
    {
        ProcessServices proc = new ProcessServices();
        
        List<OrderingModel> orderList = new List<OrderingModel>();   
        public string batchFile = "";
        public DateTime deliveryDate;
        DateTime dateTime;
        public frmManualEncode()
        {
            InitializeComponent();
            dateTime = dateTimePicker1.MinDate = DateTime.Now; //Disable selection of backdated dates to prevent errors  
        }
        
        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deliveryDate = dateTimePicker1.Value;
            if(deliveryDate == dateTime)
                MessageBox.Show("Please select Delivery date!!");
            else
            {
                if(txtBatch.Text == "")
                    MessageBox.Show("Please Enter Batch name!!");
                else
                {
                    batchFile = txtBatch.Text;
                    GenerateData();
                    ClearTools(true);
                }
            }
        }

        private bool GenerateData()
        {
            try
            {
                if (orderList == null)
                {
                    return false;
                }
                else
                {
                    proc.ManualProcess(orderList, this, Application.StartupPath + "\\Output");
                    proc.SaveDataManual(orderList, Application.StartupPath + "\\Output");
                    MessageBox.Show("Data has been processed!", "Generating Manual Data!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Generating Manual Data!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            
        }

        private void addDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            int Oquantity;
            int SN;

            Oquantity = int.Parse(txtQuantity.Text);
            SN = int.Parse(txtStartingSerial.Text);
            var branchDetails = frmOrdering.listofBranches.Where(x => x.BRSTN == txtBrstn.Text).ToList();

            for (int i = 0; i < Oquantity; i++)
            {
                OrderingModel order = new OrderingModel();
                order.BRSTN = txtBrstn.Text;
                order.AccountNo = txtAccountNo.Text;
                order.AccountName = txtAccName.Text;
                order.AccountName2 = txtAccName2.Text;
                order.outputFolder = "Customized";
                order.OrdQuantiy = 1;
                order.StartingSerial = SN.ToString();
                order.Batch = txtBatch.Text;
                order.CheckName = cbProductType.Text;

                if (cbProductType.SelectedIndex == 0)
                {
                    order.ChkType = "A";
                    order.EndingSerial = (SN + 49).ToString();
                }
                else
                {
                    order.ChkType = "B";
                    order.EndingSerial = (SN + 99).ToString();
                }
                branchDetails.ForEach(x => { 
                    //order.BranchName = x.Address1.Replace("'", "''").TrimEnd();
                    order.BranchName = x.Address1.Replace("Ñ", "N").TrimEnd();
                    order.Address = x.Address2.Replace("Ñ", "N").TrimEnd();
                    order.Address2 = x.Address3.Replace("Ñ", "N").TrimEnd();
                    order.Address3 = x.Address4.Replace("Ñ", "N").TrimEnd();
                    order.Address4 = x.Address5.Replace("Ñ", "N").TrimEnd();
                    order.Address5 = x.Address6.Replace("Ñ", "N").TrimEnd();
                    order.BranchCode = x.BranchCode;
                });

                orderList.Add(order);
                SN = int.Parse(order.EndingSerial) + 1;

            }

            DisplayData(orderList);
            ClearTools(false);
            generateToolStripMenuItem.Enabled = true;
        }
        private void DisplayData(List<OrderingModel> _orderList)
        {
            dgvOutput.DataSource = "";
            DataTable dt = new DataTable();
            DtableColumns(dt);

            _orderList.ForEach(x => {

                dt.Rows.Add(new object[] { x.BRSTN, x.AccountNo, x.AccountName, x.AccountName2,x.StartingSerial, x.EndingSerial, x.Batch });
            });

            dgvOutput.DataSource = dt;
            ProcessServices.bg_dtg(dgvOutput);
            DataGridDesign();
        }
        private void DtableColumns(DataTable dt)
        {
            dt.Columns.Add("BRSTN");
            dt.Columns.Add("ACCOUNT NUMBER");
            dt.Columns.Add("ACCOUNT NAME");
            dt.Columns.Add("ACCOUNT NAME 2");
            dt.Columns.Add("STARTING SERIAL");
            dt.Columns.Add("ENDING SERIAL");
            dt.Columns.Add("BATCH");
        }
        private void DataGridDesign()
        {
            
            dgvOutput.Columns[0].Width = 100;
            dgvOutput.Columns[1].Width = 120;
            dgvOutput.Columns[2].Width = 200;
            dgvOutput.Columns[3].Width = 210;
            dgvOutput.Columns[4].Width = 90;
            dgvOutput.Columns[5].Width = 90;
            dgvOutput.Columns[6].Width = 100;

        }

        private void txtBatch_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtBrstn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtAccountNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtStartingSerial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
 
        private void txtAccName_TextChanged(object sender, EventArgs e)
        {
            txtAccName.CharacterCasing = CharacterCasing.Upper;
        }
        private void txtAccName2_TextChanged(object sender, EventArgs e)
        {
            txtAccName2.CharacterCasing = CharacterCasing.Upper;
        }

        private void frmManualEncode_Load(object sender, EventArgs e)
        {
            txtBrstn.MaxLength = 9;
            txtAccountNo.MaxLength = 12;
            txtAccName.MaxLength = 60;
            txtAccName2.MaxLength = 60;
            txtStartingSerial.MaxLength = 10;
            ProductType();
        }
        private void ProductType()
        {
            cbProductType.Items.Add("TRUBANK PERSONAL");
            cbProductType.Items.Add("TRUBANK COMMERCIAL");
            cbProductType.Items.Add("VOUCHER CHECKS");
            cbProductType.Items.Add("CUSTOMIZED CHECKS");
            cbProductType.SelectedIndex = 3;
        }

        private void cbProductType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void cbProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if(cbProductType.SelectedIndex == 0 || cbProductType.SelectedIndex == 1)
            {
                
                txtStartingSerial.Enabled = false;
                var branchSeries = frmOrdering.listofBranches.Where(x => x.BRSTN == txtBrstn.Text).ToList();
                branchSeries.ForEach(x => {

                    if (x.BRSTN == txtBrstn.Text && cbProductType.SelectedIndex == 0)
                    {
                        int series = int.Parse(x.LastSeriesA);
                        txtStartingSerial.Text = (series + 1).ToString();
                    }
                    else
                    {
                        int series = int.Parse(x.LastSeriesB);
                        txtStartingSerial.Text = (series + 1).ToString();
                    }
                });
            }
            else
            {
                txtStartingSerial.Text = "";
                txtStartingSerial.Enabled = true;
            }
        }
        private bool ClearTools(bool _isActive)
        {
            
            if(!_isActive)
            {
                txtBatch.Enabled = false;
            }
            else
            {
                _isActive = true;
                txtBatch.Text = "";
                txtBatch.Enabled = true;
                dgvOutput.DataSource = "";
            }
            txtAccName.Text = "";
            txtAccName2.Text = "";
            txtBrstn.Text = "";
            txtQuantity.Text = "";
            txtStartingSerial.Text = "";
            txtAccountNo.Text = "";

            return _isActive;

        }
        private void frmManualEncode_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            frmOrdering frm = new frmOrdering();
            frm.Show();
            this.Hide();
        }


    }
}
