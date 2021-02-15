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
    public partial class frmCorrection : Form
    {
        Main frm;
        public frmCorrection(Main frm1)
        {

            InitializeComponent();
            this.frm = frm1;
        }
        ProcessServices proc = new ProcessServices();
        List<TempModel> temp = new List<TempModel>();
        List<TempModel> tempData = new List<TempModel>();
        List<TempModel> selectedData = new List<TempModel>();
        DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
       
        private void frmCorrection_Load(object sender, EventArgs e)
        {

        }

        private void txtBatch_TextChanged(object sender, EventArgs e)
        {
          
            temp.Clear();
            proc.DisplayAllBatches2(txtBatch.Text, temp);
            DataTable dt = new DataTable();

            dt.Clear();

            dt.Columns.Add("Batch");
            dt.Columns.Add("Date Processed");
            dt.Columns.Add("Delivery Date");
            dt.Columns.Add("Quantity");


            temp.ForEach(r =>
            {
                dt.Rows.Add(new object[] { r.Batch, r.DateProcessed.ToString("yyyy-MM-dd"), r.DeliveryDate.ToString("yyyy-MM-dd"), r.Qty });
            });

            dgvView.DataSource = dt;
            ProcessServices.bg_dtg(dgvView);
            dgvView.Columns[0].Width = 70;
            dgvView.Columns[3].Width = 60;
        }

        private void dgvView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tempData.Clear();
            int rowindex = dgvView.CurrentCell.RowIndex;
            int columnindex = dgvView.CurrentCell.ColumnIndex;

            // student.Stud_ID = int.Parse(dtgList.Rows[rowindex].Cells[columnindex].Value.ToString());

            txtBatch.Text = dgvView.Rows[rowindex].Cells[columnindex].Value.ToString();
         
            proc.DisplayAllBatchData(txtBatch.Text, tempData);

            DataTable dt2 = new DataTable();

                dt2.Clear();


            dt2.Columns.Add("Batch");
            dt2.Columns.Add("Delivery Receipt No.");
            dt2.Columns.Add("Sales Invoice No.");
            dt2.Columns.Add("Document Stamp No.");
            dt2.Columns.Add("Cheque Type");
            dt2.Columns.Add("Cheque Name");
            dt2.Columns.Add("Quantity");
            dt2.Columns.Add("Delivery Date");
            dt2.Columns.Add("Date Processed");


      
            tempData.ForEach(r =>
            {
                dt2.Rows.Add(new object[] { r.Batch,r.DrNumber,r.SalesInvoice,r.DocStampNumber,r.ChkType,r.ChequeName,
                    r.Qty ,r.DeliveryDate.ToString("yyyy-MM-dd"), r.DateProcessed.ToString("yyyy-MM-dd") 
                });
            });
            chk.Name = "Chk";
            chk.HeaderText = "";
            chk.Width = 30;
            cbHeader.Visible = true;
            //CheckBox headerCheckBox = new CheckBox();
            //Find the Location of Header Cell.
            // Point headerCellLocation = this.re(0, -1, true).Location;
            //Place the Header CheckBox in the Location of the Header Cell.
            //cbHeader.Location = new Point(headerCellLocation.X + 40, headerCellLocation.Y + 20);
            Point header = new Point();
            cbHeader.Location =  new Point(header.X + 49, header.Y + 15);
            cbHeader.Size = new Size(18, 18);
            //Assign Click event to the Header CheckBox.
            cbHeader.Click += new EventHandler(cbHeader_CheckedChanged);
            dgvData.Controls.Add(cbHeader);

            if (dgvData.Columns.Contains(chk))
            {

            }
            else
            {
                dgvData.Columns.Add(chk);
            }
            dgvData.DataSource = dt2;
            ProcessServices.bg_dtg(dgvData);
            //Assign Click event to the DataGridView Cell.
            // dgvData.CellContentClick += new DataGridViewCellEventHandler(dgvData_CellClick);
            dgvData.CurrentCell = dgvData.Rows[0].Cells[1];
            dgvData.Columns[1].Width = 70;
            dgvData.Columns[2].Width = 100;
            dgvData.Columns[3].Width = 100;
            dgvData.Columns[4].Width = 70;
            dgvData.Columns[5].Width = 70;
            dgvData.Columns[6].Width = 150;
            dgvData.Columns[7].Width = 70;
            dgvData.Columns[8].Width = 70;
            dgvData.Columns[9].Width = 70;
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbHeader_CheckedChanged(object sender, EventArgs e)
        {
            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in dgvData.Rows)
            {

                DataGridViewCheckBoxCell checkBox = (row.Cells[0] as DataGridViewCheckBoxCell);
                checkBox.Value = cbHeader.Checked;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            selectedData.Clear();   
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                TempModel dtemp = new TempModel();
                bool isSelected = Convert.ToBoolean(row.Cells["Chk"].Value);
                 
                    if (isSelected)
                    {
                            dtemp.DrNumber = row.Cells["Delivery Receipt No."].Value.ToString();
                        selectedData.Add(dtemp);
                    }
                 
            }

            if (selectedData.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you wnat to delete this data?", "Delivery Receipt Number delete", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    //do something

                    proc.DeleteItems(selectedData);
                    MessageBox.Show("Data has succesfully deleted!!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearTools();
                }
                else
                    MessageBox.Show("Deletion has been cancelled!!!", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("Please select item to delete!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            TempModel tempModel = new TempModel();
            //if (dgvData.SelectedRows != null && dgvData.SelectedRows.Count > 0)
            //{
            string val = txtNewDr.Text;
            
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    bool isSelected = Convert.ToBoolean(row.Cells["Chk"].Value);
                    if(isSelected)
                    {
                        tempModel.DrNumber = row.Cells["Delivery Receipt No."].Value.ToString();
                    //  MessageBox.Show(tempModel.DrNumber);
                        
                    }
                }
            // 
            if (txtNewDr.Text != "")
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to update this data?", "Delivery Receipt Number Update", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    //do something
               
                 proc.UpdateItem(tempModel, val);
                
                MessageBox.Show("Data has been Updated!!","Done",MessageBoxButtons.OK,MessageBoxIcon.Information);
                ClearTools();
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                    MessageBox.Show("Updating has been cancelled!!!","Cancelled",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
            }
            else
                MessageBox.Show("Please Input new Series", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
        private void ClearTools()
        {
            txtNewDr.Text = "";
            temp.Clear();
            tempData.Clear();
            dgvData.Refresh();
            dgvData.DataSource = "";
            cbHeader.Visible = false;
            txtBatch.Text = "";

            txtBatch.TabIndex = 0;
            cbHeader.Checked = false;
        }
        

     
    }
}
