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
    public partial class frmOrdering : Form
    {
        public DateTime deliveryDate;
        DateTime dateTime;
        public  string batchFile;
        List<OrderingModel> orderList = new List<OrderingModel>();
        ProcessServices proc = new ProcessServices();
        public static List<BranchesModel> listofBranches = new List<BranchesModel>();
        public static List<ChequeTypesModel> productList = new List<ChequeTypesModel>();
        public frmOrdering()
        {
            InitializeComponent();
            dateTime = dateTimePicker1.MinDate = DateTime.Now; //Disable selection of backdated dates to prevent errors  
        }
        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            deliveryDate = dateTimePicker1.Value;
            if (deliveryDate == dateTime)
                MessageBox.Show("Please select Delivery date!!");
            else
            {
                if (txtBatch.Text == "")
                    MessageBox.Show("Please Enter Batch name!!");
                else
                {
                    batchFile = txtBatch.Text;
                    //if (gClient.BankCode == "028")
                    proc.CheckData(dgvOrdering, orderList,listofBranches, batchFile, deliveryDate);
                    TotalChecks(orderList);
                    lblGrandTotal.Text = orderList.Count().ToString();
                
                    generateToolStripMenuItem.Enabled = true;
                    reportsToolStripMenuItem.Enabled = false;
                }  
            }
        }
        private void frmOrdering_Load(object sender, EventArgs e)
        {

            
            WindowState = FormWindowState.Maximized;
            isBankActive();
           // proc.OpenFile();
        }
        private void isBankActive()
        {
            productList.Clear();
            DataTable dt = new DataTable();
            dt.Clear();
            dgvTypes.DataSource = "";
            if (gClient.BankCode == "028")
                cbDeliveryTo.Checked = true;
            else
                cbDeliveryTo.Checked = false;

            proc.getOrderingBranches(listofBranches);
            proc.GetChequeTypesOrdering(productList);
            
            dt.Columns.Add("Cheque Name");
            dt.Columns.Add("Quantity");

            productList.ForEach(x => { dt.Rows.Add(new object[] { x.ChequeName, "" }); });


            dgvTypes.DataSource = dt;
            if (dgvTypes.Rows.Count > 9)
            {
                dgvTypes.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
            }
            else
                dgvTypes.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);

            dgvTypes.Columns[0].Width = 300;
            dgvTypes.Columns[1].Width = 40;
            

        }
        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //con.DumpMySQL();
            if (orderList != null)
            {
                proc.DeleteTextFile(orderList,"Output");//Deleting All Text file in designated folder in the order file
                proc.Process(orderList, this,Application.StartupPath+ "\\Output");//Generating TextFile and dbf file Data output
               string zipFile =  proc.ZipFileS(gUser.FirstName, this, orderList); // Zipping Folders
                proc.SaveData(orderList, zipFile); // Saving Data to database

            }

            
            MessageBox.Show("Data has been processed!!!!");
         
            Environment.Exit(0);
        }
        private List<int> DisplayTotal(List<OrderingModel> _orderlist)
        {
            List<int> list = new List<int>();
            int _total = 0;
            //List<ChequeTypesModel> listofChkType = new List<ChequeTypesModel>();
            //proc.GetChequeTypesOre(listofChkType);
            for (int i = 0; i < productList.Count; i++)
            {
                //if (gClient.ShortName == "RCBC")
               _total = GetTotalChecks(productList[i].Type, productList[i].BookStyle,_orderlist);
                //else
                //    _total = GetTotalChecksDefault(listofChkType[i].Type);
                //_total = fGetTotalChecks(listofChkType[i].ChequeName, orderList);
                list.Add(_total);
            }
            return list;
        }
        private void TotalChecks(List<OrderingModel> _orderlist)
        {

            List<int> Total = DisplayTotal(_orderlist);

            for (int i = 0; i < dgvTypes.Rows.Count; i++)
            {
                if (dgvTypes.Rows[i].Index == i)
                    dgvTypes.Rows[i].Cells[1].Value = Total[i];
                if (dgvTypes.Rows[i].Index == i + 1)
                    dgvTypes.Rows[i].Cells[1].Value = Total[i];
                if (dgvTypes.Rows[i].Index == i + 2)
                    dgvTypes.Rows[i].Cells[1].Value = Total[i];
                if (dgvTypes.Rows[i].Index == i + 3)
                    dgvTypes.Rows[i].Cells[1].Value = Total[i];
                if (dgvTypes.Rows[i].Index == i + 4)
                    dgvTypes.Rows[i].Cells[1].Value = Total[i];
            }

        }
        private int GetTotalChecks(string _type, int _style,List<OrderingModel> _orderList)
        {

            int _total = 0;
            var list = _orderList.Where(x => x.ChkType == _type && x.Style == _style).ToList();

            _total = list.Count();
          return _total;
        }
        private void frmOrdering_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void branchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBranches frm = new frmBranches();
            frm.Show();
            this.Hide();
        }

        private void manualEncodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManualEncode frm = new frmManualEncode();
            frm.Show();
            this.Hide();
        }
    }
}
