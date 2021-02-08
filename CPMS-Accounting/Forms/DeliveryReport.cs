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
using CPMS_Accounting.Forms;
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
        int DirectReportStyle = 0;
        int ProvincialReportStyle = 0;
        string errorMessage = "";
        int AremainingBalance = 0;
        int BremainingBalance = 0;
      //  List<int> poNumber;
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

                sReport();
              //  if (gClient.DataBaseName != "producers_history")
                    proc.Process2(orderList, this, int.Parse(txtDrNumber.Text), int.Parse(txtPackNumber.Text), DirectReportStyle,ProvincialReportStyle);
              ///  else
                 //   proc.Process(orderList, this, int.Parse(txtDrNumber.Text), int.Parse(txtPackNumber.Text));

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
        private void sReport()
        {
            if (cbDirect.Text == "By Cheque Type")
                DirectReportStyle = 0;
            else if (cbDirect.Text == "By Branches")
                DirectReportStyle = 1;
            else if (cbDirect.Text == "By Location")
                DirectReportStyle = 2;
            else if (cbDirect.Text == "Cheque Type per Branch")
                DirectReportStyle = 3;
            else if (cbDirect.Text == "Cheque Type per Location")
                DirectReportStyle = 4;
            else if (cbDirect.Text == "Location,Cheque Type and Branch")
                DirectReportStyle = 5;

            if (cbProvincial.Text == "By Cheque Type")
                ProvincialReportStyle = 0;
            else if (cbProvincial.Text == "By Branches")
                ProvincialReportStyle = 1;
            else if (cbProvincial.Text == "By Location")
                ProvincialReportStyle = 2;
            else if (cbProvincial.Text == "Cheque Type per Branch")
                ProvincialReportStyle = 3;
            else if (cbProvincial.Text == "Cheque Type per Location")
                ProvincialReportStyle = 4;
            else if (cbProvincial.Text == "Location,Cheque Type and Branch")
                ProvincialReportStyle = 5;
        }
        private void ReporStyle()
        {
            cbDirect.Items.Add("By Cheque Type");
            cbDirect.Items.Add("By Location");
            cbDirect.Items.Add("By Branches");
            cbDirect.Items.Add("Cheque Type per Branch");
            cbDirect.Items.Add("Cheque Type per Location");
            cbDirect.Items.Add("Location,Cheque Type and Branch");
            cbProvincial.Items.Add("By Cheque Type");
            cbProvincial.Items.Add("By Location");
            cbProvincial.Items.Add("By Branches");
            cbProvincial.Items.Add("Cheque Type per Branch");
            cbProvincial.Items.Add("Cheque Type per Location");
            cbProvincial.Items.Add("Location,Cheque Type and Branch");
            if(gClient.DataBaseName != "producers_history")
            {
                cbDirect.SelectedIndex = 5;
                cbProvincial.SelectedIndex = 4;
            }
            else
            {
                cbDirect.SelectedIndex = 0;
                cbProvincial.SelectedIndex = 0;
            }
        }
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
            //try
            //{

                if (op.ShowDialog().Equals(DialogResult.OK))
                {
                    string ConString = "Provider = VFPOLEDB.1; Data Source = " + op.FileName + ";";
                    OleDbConnection con = new OleDbConnection(ConString);


                    orderList.Clear();
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
                                 "CK_NO_B,CK_NO_E,BRANCHCODE,BRANCHCD2,ACCT_NAME3 FROM " + filePath;

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
                        if (order.ChkType == "A")
                            order.ChequeName = "Regular Personal Checks";
                        else if (order.ChkType == "B")
                            order.ChequeName = "Regular Commercial Checks";
                        else if (order.ChkType == "C")
                            order.ChequeName = "Manager's Checks";
                        else
                        {
                            MessageBox.Show("Cheque Type is invalid :" + order.ChkType);
                            errorMessage += "\r\nCheque Type " + order.ChkType + " on batch : " + order.BRSTN.Trim() + ": " + order.BranchName.Trim() + " does not match\r\n";
                        }
                        //PNB Required fields
                        if (gClient.DataBaseName == "pnb_history") // Additional  field if the  Bank is PNB
                        {
                            order.BranchCode = !myReader.IsDBNull(9) ? myReader.GetString(9) : "";
                            order.OldBranchCode = !myReader.IsDBNull(10) ? myReader.GetString(10) : "";
                            order.Name3 = !myReader.IsDBNull(11) ? myReader.GetString(11) : "";
                            proc.GetBranchLocation(branch, order.BranchCode); // Getting the Flag from bRanch Table

                            order.PONumber = proc.GetPONUmber(order.ChequeName);
                            //proc.GetPONUmber(order.ChequeName, poNumber);
                            //poNumber.ForEach(x => { 
                                                                  
                            //});

//                            order.PONumber = proc.GetPONUmber(order.ChequeName); //getting Purchase Order Number from the database 
                            if (order.PONumber == 0) // Checking if there is Purchase order Number from the database
                            {
                                MessageBox.Show("Please add Purchase Order Number!!");
                                break;
                            }
                            

                            if (branch.Flag == 0)
                                order.Location = "Direct";
                            else
                                order.Location = "Provincial";

                        }
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

            
              if (orderList.Count > 0)
              {
                if (proc.CheckBatchifExisted(orderList[0].Batch.Trim()) == true)
                {




                    errorMessage += "\r\nBatch : " + orderList[0].Batch + " Is Already Existed!!";
                    MessageBox.Show(errorMessage);



                }
                else
                {
                    if (gClient.DataBaseName != "producers_history")
                    {
                        var po = orderList.Select(x => x.PONumber).Distinct().ToList();
                        var chkType = orderList.Select(x => x.ChequeName).Distinct().ToList();
                        AremainingBalance = proc.CheckPOQuantity(po[0], chkType[0]);
                        BremainingBalance = proc.CheckPOQuantity(po[1], chkType[1]);

                        AremainingBalance -= totalA.Count;
                        BremainingBalance -= totalB.Count;
                        if (AremainingBalance < 0)
                        {
                            MessageBox.Show("Insufficient Balance for Purchase Order No. :" + po[0] + " for Cheque Name: " + chkType[0]);

                        }
                        else if (BremainingBalance < 0)
                        {
                            //MessageBox.Show(AremainingBalance.ToString() + " - " + BremainingBalance.ToString());
                            MessageBox.Show("Insufficient Balance for Purchase Order No. :" + po[1] + " for Cheque Name: " + chkType[1]);

                        }
                    }
                    else
                    {
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
                            frmCorrection.bg_dtg(dataGridView1);
                            lblTotalA.Text = totalA.Count.ToString();
                            lblTotalB.Text = totalB.Count.ToString();
                            lblTotalChecks.Text = orderList.Count.ToString();

                        }
                    }
                }

              }


            //}



            
            //catch (Exception error)
            //{
            //    MessageBox.Show(error.Message, error.StackTrace);
            //}
        }
       
        private void DeliveryReport_Load(object sender, EventArgs e)
        {
            
            ChequeName();
            ReporStyle();


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

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void cbDirect_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void cbProvincial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void deliveryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            report = "DRR";
            ViewReports vp = new ViewReports();
            vp.Show();
        }
    }
}
