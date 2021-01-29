using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CPMS_Accounting.Models;
using CPMS_Accounting.Procedures;
using CrystalDecisions.Shared;
using static CPMS_Accounting.GlobalVariables;


namespace CPMS_Accounting
{
    public partial class DeliveryReport : Form
    {

        public static string report = "DR";
        List<OrderModel> orderList = new List<OrderModel>();
        ProcessServices proc = new ProcessServices();
        List<TempModel> tempDr = new List<TempModel>();
        List<TempModel> tempSticker = new List<TempModel>();
        public DateTime deliveryDate;
        DateTime dateTime;
        BranchesModel branch = new BranchesModel();
        List<Int64> DrNumbers = new List<long>();
        List<Int32> PNumbers = new List<Int32>();
        Int32 pNumber = 0;
        Int64 _dr = 0;
        Main frm;
        string errorMessage = "";
        public DeliveryReport(Main frm1)
        {
            InitializeComponent();

            dateTime = dateTimePicker1.MinDate = DateTime.Now; //Disable selection of backdated dates to prevent errors  
            this.frm = frm1;
        }

        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {

            deliveryDate = dateTimePicker1.Value;
            if (deliveryDate == dateTime)
            {
                MessageBox.Show("Please set Delivery Date!");
            }
            else
            {


                if(gClient.DataBaseName != "producers_history")
                    proc.Process2(orderList, this, int.Parse(txtDrNumber.Text), int.Parse(txtPackNumber.Text));
                else
                    proc.Process(orderList, this, int.Parse(txtDrNumber.Text), int.Parse(txtPackNumber.Text));

                proc.GetDRDetails(orderList[0].Batch, tempDr);
                proc.GetStickerDetails(tempSticker, orderList[0].Batch);

                MessageBox.Show("Data has been process!!!");
                ViewReports vp = new ViewReports();
                // vp.MdiParent = this;
                vp.Show();
                reportsToolStripMenuItem.Enabled = true;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            GetData();
            generateToolStripMenuItem.Enabled = true;

        }
        private void ChequeName()
        {
            comboBox1.Items.Add("Regular Checks");
            comboBox1.Items.Add("Manager's Checks");
            comboBox1.SelectedIndex = 0;
        }
        //private void BankName()
        //{
        //    cmbBank.Items.Add("PRODUCERS BANK");
        //    cmbBank.Items.Add("PNB");
        //    cmbBank.SelectedIndex = 0;
        //}

        private void GetData()
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;


            OpenFileDialog op = new OpenFileDialog();

            //op.InitialDirectory = Application.StartupPath;
            op.InitialDirectory = @"\\192.168.10.254\Accounting_Files\Packing";
            op.Filter = "dbf files (*.dbf)|*.dbf|All files (*.*)|*.*";
            op.FilterIndex = 2;
            op.RestoreDirectory = true;
            try
            {

                if (op.ShowDialog().Equals(DialogResult.OK))
                {
                    string ConString = "Provider = VFPOLEDB.1; Data Source = " + op.FileName + ";";
                    OleDbConnection con = new OleDbConnection(ConString);



                    //Get the path of specified file
                    filePath = Path.GetFileNameWithoutExtension(op.FileName);

                    //Read the contents of the file into a stream
                    var fileStream = op.OpenFile();
                    string sql = "";

                    if (gClient.DataBaseName == "producers_history")// Checking what table was selected to read the packing file
                    {
                        sql = "Select BATCHNO,RT_NO,BRANCH,ACCT_NO,CHKTYPE,ACCT_NAME1,ACCT_NAME2," +
                                 "CK_NO_B,CK_NO_E FROM " + filePath;
                    }
                    else if (gClient.DataBaseName == "pnb_history")
                    {
                        sql = "Select BATCHNO,RT_NO,BRANCH,ACCT_NO,CHKTYPE,ACCT_NAME1,ACCT_NAME2," +
                                 "CK_NO_B,CK_NO_E,BRANCHCODE,OLDBCODE FROM " + filePath;

                    }
                    OleDbCommand cmd = new OleDbCommand(sql, con);
                    con.Open();
                    OleDbDataReader myReader = cmd.ExecuteReader();

                    while (myReader.Read())
                    {
                        OrderModel order = new OrderModel();
                        order.Batch = !myReader.IsDBNull(0) ? myReader.GetString(0) : "";
                        order.BRSTN = !myReader.IsDBNull(1) ? myReader.GetString(1) : "";
                        order.BranchName = !myReader.IsDBNull(2) ? myReader.GetString(2) : "";
                        order.AccountNo = !myReader.IsDBNull(3) ? myReader.GetString(3) : "";
                        order.ChkType = !myReader.IsDBNull(4) ? myReader.GetString(4) : "";
                        order.Name1 = !myReader.IsDBNull(5) ? myReader.GetString(5) : "";
                        order.Name2 = !myReader.IsDBNull(6) ? myReader.GetString(6) : "";

                        order.StartingSerial = !myReader.IsDBNull(7) ? myReader.GetString(7) : "";
                        order.EndingSerial = !myReader.IsDBNull(8) ? myReader.GetString(8) : "";
                        //PNB Required fields
                        if (gClient.DataBaseName == "pnb_history") // Additional  field if the  Bank is PNB
                        {
                            order.BranchCode = !myReader.IsDBNull(9) ? myReader.GetString(9) : "";
                            order.OldBranchCode = !myReader.IsDBNull(10) ? myReader.GetString(10) : "";
                            proc.GetBranchLocation(branch, order.BranchCode); // Getting the Flag from bRanch Table
                            if (branch.Flag == 0)
                                order.Location = "Direct";
                            else
                                order.Location = "Provincial";

                        }

                        if (order.ChkType == "A" && comboBox1.Text == "Regular Checks")
                            order.ChequeName = "Regular Personal Checks";
                        else if (order.ChkType == "B" && comboBox1.Text == "Regular Checks")
                            order.ChequeName = "Regular Commercial Checks";
                        else
                        {
                            errorMessage += "\r\nCheque Type " + order.ChkType + " on batch : " + order.BRSTN.Trim() + ": " + order.BranchName.Trim() + " does not match\r\n";
                        }
                        if (order.ChkType == "A" && comboBox1.Text == "Manager's Checks")
                            order.ChequeName = "Manager's Checks";


                        orderList.Add(order);
                    }


                }
                else
                {
                    errorMessage += "The file :" + op.FileName + " is not a dbf file!\r\n";
                Application.Exit();
                }
                var totalB = orderList.Where(a => a.ChkType == "B").ToList();
                var totalA = orderList.Where(a => a.ChkType == "A").ToList();
                // BindingSource checkBind = new BindingSource();
                // checkBind.DataSource = orderList;
                //     proc.CheckBatchifExisted(orderList[0].Batch.Trim());


                if (proc.CheckBatchifExisted(orderList[0].Batch.Trim()) == true)
                {
                    errorMessage += "\r\nBatch : " + orderList[0].Batch + " Is Already Existed!!";
                    MessageBox.Show(errorMessage);



                }


                //                }
                if (errorMessage != "")
                {
                    ProcessServices.ErrorMessage(errorMessage);
                    MessageBox.Show("Checking files done! with errors found! Check ErrorMessage.txt for references", "Error!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Checking files done! No Errors found");
                    dataGridView1.DataSource = orderList;
                    lblTotalA.Text = totalA.Count.ToString();
                    lblTotalB.Text = totalB.Count.ToString();
                    lblTotalChecks.Text = orderList.Count.ToString();

                }

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, error.StackTrace);
            }
        }

        private void DeliveryReport_Load(object sender, EventArgs e)
        {
            
            ChequeName();
            //  BankName();
            // MessageBox.Show(proc.myConnect.ConnectionString);
        }
        private void GetPack()
        {
            PNumbers = proc.GetMaxPackNumber();

            for (int a = 0; a < PNumbers.Count; a++)
            {

                if (pNumber > PNumbers[a])
                {
                    // MessageBox.Show("Lah");
                }
                else
                    pNumber = PNumbers[a];
                    
            }
            txtPackNumber.Text = (pNumber+1).ToString();
        }
        private void GetDR()
        {
        //    Int64 liCnt = 1;
           // Int64 liCount = 0;
            proc.GetMaxDr(DrNumbers);

            // Int64.Parse(txtDrNumber.Text)
            for (int i = 0; i < DrNumbers.Count; i++)
            {
                if (_dr > DrNumbers[i])
                {

                }
                else
                    _dr = DrNumbers[i];
        
            }
            txtDrNumber.Text = (_dr+1).ToString();
            return;

        }

        private void mainMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //Main m = new Main();
            //m.Show();
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            //RecentBatch rb = new RecentBatch(this);
            //rb.Show();
            //this.Hide();
        }

        private void btnDr_Click(object sender, EventArgs e)
        {
            GetDR();
            MessageBox.Show("Generate Delivery Receipt Number done!");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DeliveryReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            report = "";
            //this.Hide();
            //Form  f = new Main();
            //f.Show();
        }

        private void stickerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            report = "STICKER";
            ViewReports vp = new ViewReports();
            vp.Show();
        }

        private void packingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            report = "Packing";
            ViewReports vp = new ViewReports();
            vp.Show();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
           
            Cursor.Current = Cursors.WaitCursor;
            Cursor.Show();
            GetDR();
            GetPack();
         
            MessageBox.Show("Getting DrNumber and PackNumber done!!");
        }
    }
}
