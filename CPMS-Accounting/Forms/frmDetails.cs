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
    public partial class frmDetails : Form
    {
        public frmDetails()
        {
            InitializeComponent();
        }
        ProcessServices proc = new ProcessServices();
        List<TempModel> dataList = new List<TempModel>();
        List<TempModel> selectedData = new List<TempModel>();
        DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
        DataTable dt = new DataTable();
        private void frmDetails_Load(object sender, EventArgs e)
        {
            MessageBox.Show(frmCorrection.selectedDR);
            if (proc.GetDetailedperDr(dataList,frmCorrection.selectedDR))
            {
                //dataList.ForEach(x => {
                //    dt.Rows.Add(new { x.DrNumber,x.Batch,x.BRSTN,x.ChequeName,x.ChkType,x.DeliveryDate.ToString("yyyy-MM-dd")});
                //});
                Header();
               dataList.ForEach(x => 
                {
                    dt.Rows.Add(new object[] {x.DrNumber,x.Batch,x.BRSTN,x.ChequeName,x.ChkType,x.DeliveryDate.ToString("yyyy-MM-dd"),x.BranchName,
                        x.SalesInvoice,x.DocStampNumber,x.AccountNo,x.Name1,x.Name2,x.StartingSerial,x.EndingSerial,x.Location,x.PrimaryKey });

                });
                chk.Name = "Chk";
                chk.HeaderText = "";
                chk.Width = 30;
                cbHeader.Visible = true;
             
                Point header = new Point();
                cbHeader.Location = new Point(header.X + 49, header.Y + 15);
                cbHeader.Size = new Size(18, 18);
                //Assign Click event to the Header CheckBox.
                cbHeader.Click += new EventHandler(cbHeader_CheckedChanged);
                dgvDetailList.Controls.Add(cbHeader);
                if (dgvDetailList.Columns.Contains(chk))
                {

                }
                else
                {
                    dgvDetailList.Columns.Add(chk);
                }
                dgvDetailList.DataSource = dt;
                DgvDesign();
            }
        }
        private void Header()
        {
            dt.Columns.Add("DR NUMBER");
            dt.Columns.Add("BATCH NAME");
            dt.Columns.Add("BRSTN");
            dt.Columns.Add("CHECK NAME");
            dt.Columns.Add("CHECK TYPE");
            dt.Columns.Add("DELIVERY DATE");
            dt.Columns.Add("BRANCH NAME");
            dt.Columns.Add("SALES INVOICE NO.");
            dt.Columns.Add("DOCSTAMP NO.");
            dt.Columns.Add("ACCOUNTNO");
            dt.Columns.Add("ACCOUNT NAME 1");
            dt.Columns.Add("ACCOUNT NAME 2");
            dt.Columns.Add("STARTING SERIAL");
            dt.Columns.Add("ENDING SERIAL");
            dt.Columns.Add("LOCATION");
            dt.Columns.Add("PrimaryKey");
            
            
        }
        private void DgvDesign()
        {
            dgvDetailList.Columns[0].Width = 30;
            dgvDetailList.Columns[1].Width = 80;
            dgvDetailList.Columns[2].Width = 100;
            dgvDetailList.Columns[3].Width = 80;
            dgvDetailList.Columns[4].Width = 200;
            dgvDetailList.Columns[5].Width = 60;
            dgvDetailList.Columns[6].Width = 100;
            dgvDetailList.Columns[7].Width = 100;
            dgvDetailList.Columns[8].Width = 80;
            dgvDetailList.Columns[9].Width = 80;
            dgvDetailList.Columns[10].Width = 100;
            dgvDetailList.Columns[11].Width = 100;
            dgvDetailList.Columns[12].Width = 80;
            dgvDetailList.Columns[13].Width = 80;
            dgvDetailList.Columns[14].Width = 80;
            dgvDetailList.Columns[15].Width = 80;
            dgvDetailList.Columns[16].Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<TempModel> temp = new List<TempModel>();
           // temp.Clear();
            foreach (DataGridViewRow row in dgvDetailList.Rows)
            {
                bool isSelected = Convert.ToBoolean(row.Cells["Chk"].Value);
                if (isSelected)
                {
                    TempModel tempModel = new TempModel();
                    tempModel.DrNumber = row.Cells["DR NUMBER"].Value.ToString();
                    tempModel.Batch = row.Cells["BATCH NAME"].Value.ToString();
                    tempModel.AccountNo = row.Cells["ACCOUNTNO"].Value.ToString();
                    tempModel.StartingSerial = row.Cells["STARTING SERIAL"].Value.ToString();
                    tempModel.EndingSerial = row.Cells["ENDING SERIAL"].Value.ToString();
                    tempModel.BranchName = row.Cells["BRANCH NAME"].Value.ToString();
                    tempModel.ChequeName = row.Cells["CHECK NAME"].Value.ToString();
                    tempModel.SalesInvoice = int.Parse(row.Cells["SALES INVOICE NO."].Value.ToString());
                    tempModel.DocStampNumber = int.Parse(row.Cells["DOCSTAMP NO."].Value.ToString());
                    tempModel.BRSTN = row.Cells["BRSTN"].Value.ToString();
                    tempModel.ChkType = row.Cells["CHECK TYPE"].Value.ToString();
                    tempModel.DeliveryDate = DateTime.Parse(row.Cells["DELIVERY DATE"].Value.ToString());
                    tempModel.Location = row.Cells["LOCATION"].Value.ToString();
                    tempModel.Name1 = row.Cells["ACCOUNT NAME 1"].Value.ToString();
                    tempModel.Name2 = row.Cells["ACCOUNT NAME 2"].Value.ToString();
                    tempModel.PrimaryKey = int.Parse(row.Cells["PrimaryKey"].Value.ToString());

                    temp.Add(tempModel);

                }
            }
            // 
            if (temp.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure ?", "Delivery Receipt Number Update", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //do something

                    proc.UpdateDetailedItem(temp);

                    MessageBox.Show("Data has been Updated!!");
                    //   ClearTools();
                    this.Close();
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                    MessageBox.Show("Updating has been cancelled!!!");
                }
            }
           
        }
        private void cbHeader_CheckedChanged(object sender, EventArgs e)
        {
            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in dgvDetailList.Rows)
            {

                DataGridViewCheckBoxCell checkBox = (row.Cells[0] as DataGridViewCheckBoxCell);
                checkBox.Value = cbHeader.Checked;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            selectedData.Clear();
            foreach (DataGridViewRow row in dgvDetailList.Rows)
            {
                TempModel dtemp = new TempModel();
                bool isSelected = Convert.ToBoolean(row.Cells["Chk"].Value);

                if (isSelected)
                {
                    dtemp.PrimaryKey =int.Parse(row.Cells["PrimaryKey"].Value.ToString());
                    selectedData.Add(dtemp);
                }

            }

            if (selectedData.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure ?", "Delivery Receipt Number Update", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //do something

                    proc.DeleteDetailedItems(selectedData);
                    MessageBox.Show("Data has succesfully deleted!!");
                    // ClearTools();
                    this.Close();
                  
                }
                else
                    MessageBox.Show("Deletion has been cancelled!!!");
            }
            else
            {
                MessageBox.Show("Please select item to delete!");
            }
        }

        private void btnCancelClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
