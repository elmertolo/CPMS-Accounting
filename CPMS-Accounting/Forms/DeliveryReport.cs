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
        OpenFileDialog op = new OpenFileDialog();
        List<OrderModel> orderList = new List<OrderModel>();
        ProcessServices proc = new ProcessServices();
        List<TempModel> tempDr = new List<TempModel>();
        List<TempModel> tempSticker = new List<TempModel>();
        List<ChequeTypesModel> productList = new List<ChequeTypesModel>();
        public DateTime deliveryDate;
        DateTime dateTime;
        BranchesModel branch = new BranchesModel();
        List<Int64> DrNumbers = new List<long>();
        List<Int32> PNumbers = new List<Int32>();
        Int32 pNumber = 0;
        Int64 _dr = 0;
        Main frm;
        int withDeliveryTo = 0;
        List<int> TotalPerChecks = new List<int>();
        int DirectReportStyle = 0;
        int ProvincialReportStyle = 0;
        string errorMessage = "";
        int AremainingBalance = 0;
        int BremainingBalance = 0;
   //     int MCremainingBalance = 0;
        TextBox tb = new TextBox();
        List<string> chkType = new List<string>();
        Label lb = new Label();
        public static OleDbConnection con;
        int A, B, C, D;
            List<ProductModel> listofProducts = new List<ProductModel>();
        //List<int> pIndex = new List<int>();
        //int count = 0;
        //int index = 1;
        //int totalA = 0;
        //int totalB = 0;
      //  List<int> poNumber;
        public DeliveryReport(Main frm1)
        {
            InitializeComponent();

            dateTime = dateTimePicker1.MinDate = DateTime.Now; //Disable selection of backdated dates to prevent errors  
            this.frm = frm1;
            txtDrNumber.MaxLength = 7;
            txtPackNumber.MaxLength = 7;
        }

        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gClient.BankCode == "028")
                ProcessData();
            else
                ProcessDataDefault();
        }
        private bool isValidateGeneration()
        {
            try
            {
                if (txtDrNumber.Text == null || txtDrNumber.Text == "")
                {
                    MessageBox.Show("Please Enter Delivery Receipt Number!", "isValidateGeneration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                else
                {


                    if (txtPackNumber.Text == null || txtPackNumber.Text == "")
                    {
                        MessageBox.Show("Please Enter Pack Number!", "isValidateGeneration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    else
                    {

                        if (txtDrNumber.TextLength < 7)
                        {
                            MessageBox.Show("Delivery Receipt Number should not be less than 7 digits!", "isValidateGeneration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                        else
                        {

                            if (txtPackNumber.TextLength < 7)
                            {
                                MessageBox.Show("Pack Number should not be less than 7 digits!", "isValidateGeneration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return false;
                            }
                            else
                                return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "isValidateGeneration", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (gClient.ShortName != "PNB")
                GetData();
            else
                GetData2();


            generateToolStripMenuItem.Enabled = true;

        }
        //private void ChequeName()
        //{
        //    proc.fChequeTypes(productList);
        //    var prod = productList.Select(z => z.ProductName).Distinct().ToList();
        //    prod.ForEach(x => 
        //    { 
        //        comboBox1.Items.Add(x); 
        //        //count++;
        //        //pIndex.Add(count);
        //    });
        //    //comboBox1.Items.Add("Regular Checks");
        //    //comboBox1.Items.Add("Manager's Checks");
        //    comboBox1.SelectedIndex = 0;
        //}

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
            if(gClient.ShortName == "PNB")
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
        //private string DynamicCheques(string _chkType,string _brstn,string _branchName)
        //{
        //    string _chkChequeName = "";
        //    for (int i = 0; i < productList.Count; i++)
        //    {

        //        if (comboBox1.SelectedIndex == i)
        //        {


        //            var chk = productList.Where(c => c.Type == _chkType && c.ProductCode == i + 1).ToList();
        //            if (chk.Count == 0)
        //            {
        //                MessageBox.Show("Cheque Type is invalid " + _chkType + " for " + comboBox1.Text);
        //                errorMessage += "\r\nCheque Type " + _chkType + " on batch : " + _brstn.Trim() + ": " + _branchName.Trim() + " does not match\r\n";
        //            }
        //            else
        //            {
        //                //index = chk[0].ProductCode;
        //                _chkChequeName = chk[0].ChequeName;
                                                

                        
        //            }
                    
        //        }
                
        //    }
        //    return _chkChequeName;
        //}
        private void GetData()
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;


           

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
                    con = new OleDbConnection(ConString);

                    dataGridView1.DataSource = "";
                    orderList.Clear();
                    //Get the path of specified file
                    filePath = Path.GetFileNameWithoutExtension(op.FileName);

                    //Read the contents of the file into a stream
                    var fileStream = op.OpenFile();
                    string sql = "";


                // Checking what table was selected to read the packing file


                //sql = "Select BATCHNO,RT_NO,BRANCH,ACCT_NO,CHKTYPE,ACCT_NAME1,ACCT_NAME2," +
                //         "CK_NO_B,CK_NO_E FROM " + filePath  + " where CHKTYPE = '" + z + "'";
                if (gClient.ShortName == "RCBC")
                {
                    sql = "Select BATCHNO,RT_NO,BRANCH,ACCT_NO,CHKTYPE,ACCT_NAME1,ACCT_NAME2," +
                                    "CK_NO_B,CK_NO_E,DELIVERTO,CHKNAME FROM " + filePath;
                }
                else
                {
                    sql = "Select BATCHNO,RT_NO,BRANCH,ACCT_NO,CHKTYPE,ACCT_NAME1,ACCT_NAME2," +
                                 "CK_NO_B,CK_NO_E,DELIVERTO FROM " + filePath;
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
                     
                            //order.ChequeName = DynamicCheques(order.ChkType,order.BRSTN,order.BranchName);
                    

                            order.Name1 = !myReader.IsDBNull(5) ? myReader.GetString(5) : "";
                            order.Name2 = !myReader.IsDBNull(6) ? myReader.GetString(6) : "";
                            order.StartingSerial = !myReader.IsDBNull(7) ? myReader.GetString(7) : "";
                            order.EndingSerial = !myReader.IsDBNull(8) ? myReader.GetString(8) : "";
                            order.DeliveryTo = !myReader.IsDBNull(9) ? myReader.GetString(9) : "";
                    order.DeliveryTo.TrimEnd();
                    if (gClient.ShortName == "RCBC")
                    {
                        order.ProductName = !myReader.IsDBNull(10) ? myReader.GetString(10) : "";
                    }
                                order.ChequeName = proc.GetChequeNamewithProductCode(order.ChkType,order.ProductName);
                    //  order.ChequeName = proc.GetChequeName(order.ChkType);
                                proc.GetBranchLocationbyBrstn(branch, order.DeliveryTo); // Getting the Flag from bRanch Table
                                order.DeliverytoBranch = branch.Address1;
                            order.BranchCode = branch.BranchCode;
                                order.Quantity = 1;
                                proc.GetProducts(listofProducts);
                                listofProducts.ForEach(x =>
                                {
                                    if (order.ChkType == x.ChkType)
                                    {
                                        order.ProductCode = x.ProductCode;
                                    }
                                });
                    if(order.BRSTN.StartsWith("01"))
                    order.Location = "Direct";
                    else
                    order.Location = "Provincial";

                    CountChkType(order.ChequeName);
                 // TotalPerChecks = GetTotalChecks(order.ChequeName.Substring(0,10));
                    orderList.Add(order);

                        }
                           
                }
                else
                {
                        errorMessage += "The file :" + op.FileName + " is not a dbf file!\r\n";
                        Application.Exit();
                }
            //var totalA = orderList.Where(a => a.ChkType == "A" || a.ChkType == "C").ToList();
            //var totalB = orderList.Where(a => a.ChkType == "B"  || a.ChkType == "D").ToList();
            


            if (orderList.Count > 0)
              {
                if (proc.CheckBatchifExisted(orderList[0].Batch.Trim()) == true)
                {

                    errorMessage += "\r\nBatch : " + orderList[0].Batch + " Is Already Existed!!";
                    MessageBox.Show(errorMessage);

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
                            
                            ProcessServices.bg_dtg(dataGridView1);
                //     TotalPerChecks =   DisplayTotal();
                        //Totalchecks(TotalPerChecks[0].ToString(), B.ToString(), C.ToString(), D.ToString(), E.ToString());
                        TotalChecks();
                        //    MessageBox.Show(TotalPerChecks[0].ToString());
                            //lblTotalA.Text = totalA.Count.ToString();
                            //lblTotalB.Text = totalB.Count.ToString();
                            lblTotalChecks.Text = orderList.Count.ToString();

                        }
                    
                }

            }


            //}




            
            //catch (Exception error)
            //{
            //    MessageBox.Show(error.Message, error.StackTrace);
            //}
        }
        private void GetData2()
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;




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
                con = new OleDbConnection(ConString);

                    dataGridView1.DataSource = "";
                    orderList.Clear();
                //Get the path of specified file
                filePath = Path.GetFileNameWithoutExtension(op.FileName);

                //Read the contents of the file into a stream
                var fileStream = op.OpenFile();
                string sql = "";



                //sql = "Select BATCHNO,RT_NO,BRANCH,ACCT_NO,CHKTYPE,ACCT_NAME1,ACCT_NAME2," +
                //           "CK_NO_B,CK_NO_E,DELIVERTO,BRANCHCODE,BRANCHCD2,ACCT_NAME3,BLOCK,SEGMENT,PRODCODE FROM " + filePath;
                sql = "Select BATCHNO,RT_NO,BRANCH,ACCT_NO,CHKTYPE,ACCT_NAME1,ACCT_NAME2," +
                           "CK_NO_B,CK_NO_E,DELIVERTO,BRANCHCODE,BRANCHCD2,BLOCK,SEGMENT,PRODCODE FROM " + filePath;


                OleDbCommand cmd = new OleDbCommand(sql, con);
                con.Open();
                OleDbDataReader myReader = cmd.ExecuteReader();

                while (myReader.Read())
                {

                    OrderModel order = new OrderModel();
                    order.Batch = !myReader.IsDBNull(0) ? myReader[0].ToString() : "";
                    order.BRSTN = !myReader.IsDBNull(1) ? myReader[1].ToString(): "";
                    order.BranchName = !myReader.IsDBNull(2) ? myReader[2].ToString() : "";
                    order.AccountNo = !myReader.IsDBNull(3) ? myReader[3].ToString() : "";

                    order.ChkType = !myReader.IsDBNull(4) ? myReader[4].ToString() : "";

                    //order.ChequeName = DynamicCheques(order.ChkType,order.BRSTN,order.BranchName);
                  
                    //order.ChequeName.Replace("'", "''");
                    order.Name1 = !myReader.IsDBNull(5) ? myReader.GetString(5) : "";
                    order.Name2 = !myReader.IsDBNull(6) ? myReader.GetString(6) : "";
                    order.StartingSerial = !myReader.IsDBNull(7) ? myReader.GetString(7) : "";
                    order.EndingSerial = !myReader.IsDBNull(8) ? myReader.GetString(8) : "";
                    order.DeliveryTo = !myReader.IsDBNull(9) ? myReader.GetString(9) : "";
                    order.Quantity = 1;
                    //PNB Required fields
            
                        order.BranchCode = !myReader.IsDBNull(10) ? myReader.GetString(10) : "";
                        order.OldBranchCode = !myReader.IsDBNull(11) ? myReader.GetString(11) : "";
                        //order.Name3 = !myReader.IsDBNull(12) ? myReader.GetString(12) : "";
                       //  order.Name3.Trim();
                        order.Name2.TrimEnd();
                        order.Name1.TrimEnd();
                        
                        order.Block = !myReader.IsDBNull(12) ? int.Parse(myReader[12].ToString()) : 0;
                        
                        order.Segment = !myReader.IsDBNull(13) ? int.Parse(myReader[13].ToString()) : 0;
                        order.ProductType = !myReader.IsDBNull(14) ? myReader.GetString(14) : "";
                        order.BranchCode.TrimEnd();
                        proc.GetBranchLocation(branch, order.BranchCode); // Getting the Flag from bRanch Table
                        order.ChequeName = proc.GetChequeName(order.ChkType);
                           // order.ChequeName = proc.GetChequeName(order.ChkType,order.ProductName);
                            order.PONumber = proc.GetPONUmber(order.ChequeName);//getting Purchase Order Number from the database 
            
                        order.Address2 = branch.Address2.Replace("'", "''");
                        order.Address3 = branch.Address3.Replace("'", "''");
                        order.Address4 = branch.Address4.Replace("'", "''");
                        order.Address5 = branch.Address5.Replace("'", "''");
                        order.Address6 = branch.Address6.Replace("'", "''");
                        //proc.GetPONUmber(order.ChequeName, poNumber);
                        //poNumber.ForEach(x => { 

                        //});
                        if (order.PONumber == 0) // Checking if there is Purchase order Number from the database
                        {

                            MessageBox.Show("Please add Purchase Order Number for Cheque Type " + order.ChkType + "!!! ", "Error!");
                            errorMessage += "There is no Purchase Order for Cheque Type : " + order.ChkType;
                            break;
                        }


                        if (branch.Flag == 0)
                            order.Location = "Direct";
                        else
                            order.Location = "Provincial";

                    proc.GetProducts(listofProducts);
                    listofProducts.ForEach(x =>
                    {
                        if(order.ChkType == x.ChkType  && order.Location == x.DeliveryLocation)
                        {
                            order.ProductCode = x.ProductCode;
                        }
                    });
                    CountChkType(order.ChkType);
                    orderList.Add(order);



                }

                }
            else
            {
                errorMessage += "The file :" + op.FileName + " is not a dbf file!\r\n";
                Application.Exit();
            }
            var totalA = orderList.Where(a => a.ChkType == "A" || a.ChkType == "C").ToList();
            var totalB = orderList.Where(a => a.ChkType == "B" || a.ChkType == "D").ToList();



            if (orderList.Count > 0)
            {
                if (proc.CheckBatchifExisted(orderList[0].Batch.Trim()) == true)
                {

                    errorMessage += "\r\nBatch : " + orderList[0].Batch + " Is Already Existed!!";
                    MessageBox.Show(errorMessage);

                }
                else
                {
                        var po = orderList.Select(x => x.PONumber).Distinct().ToList();
                        var chkType = orderList.Select(x => x.ChequeName).Distinct().ToList();
                        po.ForEach(x =>
                        {

                            chkType.ForEach(d =>
                            {
                                //if (d == comboBox1.Text.Substring(0,comboBox1.Text.Length - 6) +" Personal Checks" || d == "Manager's Checks")
                                //    AremainingBalance = proc.CheckPOQuantity(x, d);
                                //else if (d == comboBox1.Text.Substring(0, comboBox1.Text.Length - 6)+ " Personal Checks")
                                //    BremainingBalance = proc.CheckPOQuantity(x, d);
                                //else if (d == "C")
                                //    AremainingBalance = proc.CheckPOQuantity(x, d);


                                if (AremainingBalance < 0)
                                {
                                    MessageBox.Show("Insufficient Balance for Purchase Order No. :" + x + " for Cheque Name: " + d.Replace("'", "''"));

                                }
                                else if (BremainingBalance < 0)
                                {
                                    //MessageBox.Show(AremainingBalance.ToString() + " - " + BremainingBalance.ToString());
                                    MessageBox.Show("Insufficient Balance for Purchase Order No. :" + x + " for Cheque Name: " + d.Replace("'", "''"));

                                }
                                //else if (MCremainingBalance < 0)
                                //{
                                //    //MessageBox.Show(AremainingBalance.ToString() + " - " + BremainingBalance.ToString());
                                //    MessageBox.Show("Insufficient Balance for Purchase Order No. :" + x + " for Cheque Name: " + d);

                                //}
                            });
                        });


                        AremainingBalance -= totalA.Count;
                        BremainingBalance -= totalB.Count;


                    

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

                        ProcessServices.bg_dtg(dataGridView1);
                        //   Totalchecks(A.ToString(), B.ToString(), C.ToString(), D.ToString(), E.ToString());
                        TotalChecks();
                        //lblTotalA.Text = totalA.Count.ToString();
                        //lblTotalB.Text = totalB.Count.ToString();
                        lblTotalChecks.Text = orderList.Count.ToString();

                    }

                }

            }


            //}




            //catch (Exception error)
            //{
            //    MessageBox.Show(error.Message, "Get Data For PNB" , MessageBoxButtons.OK,MessageBoxIcon.Error);
            //}
        }

        private void DeliveryReport_Load(object sender, EventArgs e)
        {

            isBankActive();
            //ChequeName();
            ReporStyle();
            lb.Text = "Total";

            
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
        
         
            MessageBox.Show("Getting DrNumber done!!");
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

        private void btnGenerate2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Cursor.Show();
            //GetPack();
            Int32 pack = proc.GetMaxPackNumber2();
            txtPackNumber.Text = (pack + 1).ToString();
            MessageBox.Show("Getting PackNumber done!!");
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                int sum = 0;

                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)

                {

                    sum += Convert.ToInt32(this.dataGridView1[16, i].Value);

                }

                tb.Text = sum.ToString();



                int X = this.dataGridView1.GetCellDisplayRectangle(0, -1, true).Location.X;

                lb.Width = this.dataGridView1.Columns[0].Width + X;

                lb.Location = new Point(0, this.dataGridView1.Height - tb.Height);



                tb.Width = this.dataGridView1.Columns[1].Width;

                X = this.dataGridView1.GetCellDisplayRectangle(1, -1, true).Location.X;

                tb.Location = new Point(X, this.dataGridView1.Height - tb.Height);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Color for View ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadSubtotal()
        {
            lb.Height = tb.Height;

            lb.AutoSize = false;

            lb.TextAlign = ContentAlignment.MiddleCenter;

            int X = this.dataGridView1.GetCellDisplayRectangle(0, -1, true).Location.X;

            lb.Width = this.dataGridView1.Columns[0].Width + X;

            lb.Location = new Point(0, this.dataGridView1.Height - tb.Height);

            this.dataGridView1.Controls.Add(lb);



            tb.Width = this.dataGridView1.Columns[1].Width;

            X = this.dataGridView1.GetCellDisplayRectangle(1, -1, true).Location.X;

            tb.Location = new Point(X, this.dataGridView1.Height - tb.Height);

            this.dataGridView1.Controls.Add(tb);



            this.dataGridView1.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridView1_CellPainting);

        }
        private List<string> GetChkType(List<string> _chkType)
        {
           
            string ConString = "Provider = VFPOLEDB.1; Data Source = " + op.FileName + ";";
            con = new OleDbConnection(ConString);

            string filePath;
        
            //Get the path of specified file
            filePath = Path.GetFileNameWithoutExtension(op.FileName);
            string sql = "";

                sql = "Select DISTINCT(CHKTYPE)  FROM " + filePath  ;

            OleDbCommand cmd = new OleDbCommand(sql, con);
            con.Open();
            OleDbDataReader myReader = cmd.ExecuteReader();

            while (myReader.Read())
            {

               string chkType = myReader.GetString(0);

                _chkType.Add(chkType);
            }
            myReader.Close();
            con.Close();
                return _chkType;
        }

        private void pToolStripMenuItem_Click(object sender, EventArgs e)
        {
            report = "PackingList";
            ViewReports vp = new ViewReports();
            vp.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        
        private void isBankActive()
        {
            proc.GetChequeTypes(productList);
            DataTable dt = new DataTable();
            dt.Columns.Add("Cheque Name");
            dt.Columns.Add("Quantity");

            productList.ForEach(x => { dt.Rows.Add(new object[] { x.ChequeName, "0" }); });


            dgvProducts.DataSource = dt;
            if(dgvProducts.Rows.Count > 10)
            {
                dgvProducts.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
            }
            else
            dgvProducts.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);

            dgvProducts.Columns[0].Width = 300;

            //for (int i = 0; i < productList.Count; i++)
            //{
            //    Label mylab = new Label();
            //    mylab.Text = ;
            //    mylab.Location = new Point(34, 460);// 34, 448 //213, 474
            //    mylab.AutoSize = true;
            //    mylab.Font = new Font("Microsoft Sans Serif", 12);
            //    mylab.ForeColor = Color.Black;
            //    mylab.Padding = new Padding(6);
            //    //mylab.BringToFront();
            //    Label mylab2 = new Label();
            //    mylab2.Text = "0";
            //    mylab2.Location = new Point(213, 460);// 34, 448 //213, 474
            //    mylab2.AutoSize = true;
            //    mylab2.Font = new Font("Microsoft Sans Serif", 12);
            //    mylab2.ForeColor = Color.Black;
            //    mylab2.Padding = new Padding(6);
            //    // Adding this control to the form 
            //    this.Controls.Add(mylab);
            //    this.Controls.Add(mylab2);
            //}
        }
      //  private void Totalchecks(string chk, string chkB, string chkC,string chkD,string chkE)
        private void TotalChecks()
        {
           
            List<int> Total = DisplayTotal();
            
            for (int i = 0; i < dgvProducts.Rows.Count; i++)
            {
                if (dgvProducts.Rows[i].Index == i)
                    dgvProducts.Rows[i].Cells[1].Value = Total[i];
                if (dgvProducts.Rows[i].Index == i+1)
                    dgvProducts.Rows[i].Cells[1].Value = Total[i];
                if (dgvProducts.Rows[i].Index == i + 2)
                    dgvProducts.Rows[i].Cells[1].Value = Total[i];
                if (dgvProducts.Rows[i].Index == i + 3)
                    dgvProducts.Rows[i].Cells[1].Value = Total[i];
                if (dgvProducts.Rows[i].Index == i + 4)
                    dgvProducts.Rows[i].Cells[1].Value = Total[i];
            }

        }
        private void TotalChecksfromSearching()
        {

            List<int> Total = DisplayTotalforSearching();

            for (int i = 0; i < dgvProducts.Rows.Count; i++)
            {
                if (dgvProducts.Rows[i].Index == i)
                    dgvProducts.Rows[i].Cells[1].Value = Total[i];
                if (dgvProducts.Rows[i].Index == i + 1)
                    dgvProducts.Rows[i].Cells[1].Value = Total[i];
                if (dgvProducts.Rows[i].Index == i + 2)
                    dgvProducts.Rows[i].Cells[1].Value = Total[i];
                if (dgvProducts.Rows[i].Index == i + 3)
                    dgvProducts.Rows[i].Cells[1].Value = Total[i];
                if (dgvProducts.Rows[i].Index == i + 4)
                    dgvProducts.Rows[i].Cells[1].Value = Total[i];
            }

        }
        private void CountChkType(string chk)
        {
            //for (int i = 0; i < dgvProducts.Rows.Count; i++)
            //{
            //    if (chk == dgvProducts.Rows[0].Cells[0].Value.ToString())
            //    {
            //        A++;
            //    }
            //}
            if (chk == dgvProducts.Rows[0].Cells[0].Value.ToString())
            {
              //  dgvProducts.Rows[0].Cells[1].Value += dgvProducts.Rows[0].Cells[1].Value;
                A++;
            }
            else if (chk == dgvProducts.Rows[1].Cells[0].Value.ToString())
            {
                B++;
            }
            else if (chk == dgvProducts.Rows[2].Cells[0].Value.ToString())
            {
                C++;
            }
            else if (chk == dgvProducts.Rows[3].Cells[0].Value.ToString())
            {
                D++;
            }
            //else if (chk == dgvProducts.Rows[4].Cells[0].Value.ToString())
            //{
            //    E++;
            //}
        }
        private int GetTotalChecks(string _chkName)
        {
            int _total = 0;
            string ConString = "Provider = VFPOLEDB.1; Data Source = " + op.FileName + ";";
         string   filePath = Path.GetFileNameWithoutExtension(op.FileName);
            con = new OleDbConnection(ConString);
            string Sql = "SELECT CHKTYPE FROM " + filePath + " WHERE CHKNAME LIKE '" + _chkName + "%' ";
           // string Sql = "SELECT CHKTYPE FROM " + filePath + " WHERE CHKTYPE = '" + _chkName +"' ";
            OleDbCommand cmd = new OleDbCommand(Sql, con);
            con.Open();
            OleDbDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                //_total = int.Parse(myReader.GetString(0));
                _total++;
            }
            myReader.Close();
            con.Close();
            return _total;
        }
        private List<int> DisplayTotal()
        {
            List<int> list = new List<int>();
            int _total = 0;
            List<ChequeTypesModel> listofChkType = new List<ChequeTypesModel>();
            proc.GetChequeTypes(listofChkType);
            for (int i = 0; i < listofChkType.Count; i++)
            {
                if(gClient.ShortName == "RCBC")
                    _total = GetTotalChecks(listofChkType[i].ChequeName.Substring(0, listofChkType[i].ChequeName.Length - 6));
                else
                _total = GetTotalChecksDefault(listofChkType[i].Type);
                //_total = fGetTotalChecks(listofChkType[i].ChequeName, orderList);
                list.Add(_total);
            }
            return list;
        }
        private List<int> DisplayTotalforSearching()
        {
            List<int> list = new List<int>();
            int _total = 0;
            List<ChequeTypesModel> listofChkType = new List<ChequeTypesModel>();
            proc.GetChequeTypes(listofChkType);
            for (int i = 0; i < listofChkType.Count; i++)
            {
                //if (gClient.ShortName == "RCBC")
                //    _total = GetTotalChecks(listofChkType[i].ChequeName.Substring(0, listofChkType[i].ChequeName.Length - 6));
                //else
                //    _total = GetTotalChecksDefault(listofChkType[i].Type);
                _total = fGetTotalChecks(listofChkType[i].ChequeName, orderList);
                list.Add(_total);
            }
            return list;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(gClient.BankCode == "008")
                GetDataFromDB2();
            else
                GetDataFromDB();
        }

        private int GetTotalChecksDefault(string _chkName)
        {
            int _total = 0;
            string ConString = "Provider = VFPOLEDB.1; Data Source = " + op.FileName + ";";
            string filePath = Path.GetFileNameWithoutExtension(op.FileName);
            con = new OleDbConnection(ConString);
            //string Sql = "SELECT COUNT(CHKTYPE) FROM " + filePath + " WHERE CHKNAME LIKE '" + _chkName + "%' ";
            string Sql = "SELECT CHKTYPE FROM " + filePath + " WHERE CHKTYPE = '" + _chkName + "' ";
            OleDbCommand cmd = new OleDbCommand(Sql, con);
            con.Open();
            OleDbDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                //_total = int.Parse(myReader.GetString(0));
                _total++;
            }
            myReader.Close();
            con.Close();
            return _total;
        }
        private int fGetTotalChecks(string _chkName, List<OrderModel> _list)
        {
            int _total = 0;
            // con = new MySqlConnection(ConString);

            var chkType = _list.Where(x => x.ChequeName.Contains(_chkName)).ToList();
            _total = chkType.Count();
            //string Sql = "SELECT f FROM   WHERE CHKNAME LIKE '" + _chkName + "%' ";
            //  cmd = new MySqlCommand(Sql, con);
            //   con.Open();
            //   MySqlDataReader myReader = cmd.ExecuteReader();
            //while (myReader.Read())
            //{
            //    _total++;
            //}
            //myReader.Close();
            //con.Close();
            return _total;
        }
        private void GetDataFromDB2()
        {

            //if (gClient.ShortName == "Producers")
            //    bankCode = "122";
            try
            {
                lblTotalChecks.Text = "0";
                dataGridView1.DataSource = "";
                orderList.Clear();
                proc.GetProcessedDataFromDB(orderList, gClient.BankCode, txtBatch.Text);
                listofProducts.Clear();
                proc.GetProducts(listofProducts);
                orderList.ForEach(x =>
                {
                    x.ChequeName.Replace("Check", "Cheques");
                    x.PONumber = proc.GetPONUmberforSearching(x.ChequeName);
                    if (x.BRSTN.StartsWith("01"))
                        x.Location = "Direct";
                    else
                        x.Location = "Provincial";

                    listofProducts.ForEach(d =>
                    {
                        if (x.ChkType == d.ChkType && x.Location == d.DeliveryLocation)
                        {
                            x.ProductCode = d.ProductCode;
                        }
                    });

                    if (x.PONumber == 0)// Checking if there is Purchase order Number from the database
                    {
                        MessageBox.Show("Please add Purchase Order Number for Cheque Type " + x.ChkType + "!!! ", "Error!");
                        errorMessage += "There is no Purchase Order for Cheque Type : " + x.ChkType;
                        //break;
                    }

                });


                //    dt.Clear();
                //    dt.Columns.Clear();
                //    dt.Columns.Add("Batch"); //0
                //    dt.Columns.Add("BRSTN");//1
                //    dt.Columns.Add("Branch Name"); //2
                //    dt.Columns.Add("Account No."); //3
                //    dt.Columns.Add("Name 1"); //4 
                //    dt.Columns.Add("Name 2"); //5
                //    dt.Columns.Add("No. of Books"); //6
                //    dt.Columns.Add("Starting Serial"); //7
                //    dt.Columns.Add("Ending Serial"); //8
                //    dt.Columns.Add("Block No."); //9
                //    dt.Columns.Add("Segment No."); //10
                //    dt.Columns.Add("Delivery Date"); //11

                //    tempList.ForEach(x =>
                //    {
                //        dt.Rows.Add(new object[] { x.Batch , x.BRSTN, x.BranchName, x.AccountNo, x.Name1, x.Name2, x.Qty, x.StartingSerial, x.EndingSerial, 
                //             x.Block, x.Segment, x.DeliveryDate.ToString("yyyy-MM-dd") });
                //});

                if (orderList.Count > 0)
                {
                    if (proc.CheckBatchifExisted(orderList[0].Batch.Trim()) == true)
                    {

                        errorMessage += "\r\nBatch : " + orderList[0].Batch + " Is Already Existed!!";
                        MessageBox.Show(errorMessage);

                    }
                    else
                    {
                        var po = orderList.Select(x => x.PONumber).Distinct().ToList();
                        var chkType = orderList.Select(x => x.ChequeName).Distinct().ToList();
                        po.ForEach(x =>
                        {

                            chkType.ForEach(d =>
                            {
                                //if (d == comboBox1.Text.Substring(0,comboBox1.Text.Length - 6) +" Personal Checks" || d == "Manager's Checks")
                                //    AremainingBalance = proc.CheckPOQuantity(x, d);
                                //else if (d == comboBox1.Text.Substring(0, comboBox1.Text.Length - 6)+ " Personal Checks")
                                //    BremainingBalance = proc.CheckPOQuantity(x, d);
                                //else if (d == "C")
                                //    AremainingBalance = proc.CheckPOQuantity(x, d);


                                if (AremainingBalance < 0)
                                {
                                    MessageBox.Show("Insufficient Balance for Purchase Order No. :" + x + " for Cheque Name: " + d.Replace("'", "''"));

                                }
                                else if (BremainingBalance < 0)
                                {
                                    //MessageBox.Show(AremainingBalance.ToString() + " - " + BremainingBalance.ToString());
                                    MessageBox.Show("Insufficient Balance for Purchase Order No. :" + x + " for Cheque Name: " + d.Replace("'", "''"));

                                }
                                //else if (MCremainingBalance < 0)
                                //{
                                //    //MessageBox.Show(AremainingBalance.ToString() + " - " + BremainingBalance.ToString());
                                //    MessageBox.Show("Insufficient Balance for Purchase Order No. :" + x + " for Cheque Name: " + d);

                                //}
                            });
                        });


                        //AremainingBalance -= totalA.Count;
                        //BremainingBalance -= totalB.Count;





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
                            TotalChecksfromSearching();
                            ProcessServices.bg_dtg(dataGridView1);
                            dataGridView1.Columns[0].Width = 80;
                            dataGridView1.Columns[1].Width = 80;
                            dataGridView1.Columns[2].Width = 90;
                            dataGridView1.Columns[3].Width = 120;
                            dataGridView1.Columns[4].Width = 100;
                            dataGridView1.Columns[5].Width = 100;
                            dataGridView1.Columns[6].Width = 60;
                            dataGridView1.Columns[7].Width = 80;
                            dataGridView1.Columns[8].Width = 80;
                            dataGridView1.Columns[9].Width = 30;
                            dataGridView1.Columns[10].Width = 30;
                            dataGridView1.Columns[11].Width = 90;
                            lblTotalChecks.Text = orderList.Count().ToString();
                            generateToolStripMenuItem.Enabled = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Data From Ordering Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GetDataFromDB()
        {



            //if (gClient.ShortName == "Producers")
            //    bankCode = "122";
            try
            {
                lblTotalChecks.Text = "0";
                dataGridView1.DataSource = "";
                orderList.Clear();
                proc.GetProcessedDataFromDB(orderList, gClient.BankCode, txtBatch.Text);
                orderList.ForEach(x =>
                {
                    if (x.BRSTN.StartsWith("01"))
                        x.Location = "Direct";
                    else
                        x.Location = "Provincial";


                });

                //    dt.Clear();
                //    dt.Columns.Clear();
                //    dt.Columns.Add("Batch"); //0
                //    dt.Columns.Add("BRSTN");//1
                //    dt.Columns.Add("Branch Name"); //2
                //    dt.Columns.Add("Account No."); //3
                //    dt.Columns.Add("Name 1"); //4 
                //    dt.Columns.Add("Name 2"); //5
                //    dt.Columns.Add("No. of Books"); //6
                //    dt.Columns.Add("Starting Serial"); //7
                //    dt.Columns.Add("Ending Serial"); //8
                //    dt.Columns.Add("Block No."); //9
                //    dt.Columns.Add("Segment No."); //10
                //    dt.Columns.Add("Delivery Date"); //11

                //    tempList.ForEach(x =>
                //    {
                //        dt.Rows.Add(new object[] { x.Batch , x.BRSTN, x.BranchName, x.AccountNo, x.Name1, x.Name2, x.Qty, x.StartingSerial, x.EndingSerial, 
                //             x.Block, x.Segment, x.DeliveryDate.ToString("yyyy-MM-dd") });
                //});

                if (orderList.Count > 0)
                {
                    if (proc.CheckBatchifExisted(orderList[0].Batch.Trim()) == true)
                    {

                        errorMessage += "\r\nBatch : " + orderList[0].Batch + " Is Already Existed!!";
                        MessageBox.Show(errorMessage);

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
                            TotalChecksfromSearching();
                            ProcessServices.bg_dtg(dataGridView1);
                            dataGridView1.Columns[0].Width = 80;
                            dataGridView1.Columns[1].Width = 80;
                            dataGridView1.Columns[2].Width = 90;
                            dataGridView1.Columns[3].Width = 120;
                            dataGridView1.Columns[4].Width = 100;
                            dataGridView1.Columns[5].Width = 100;
                            dataGridView1.Columns[6].Width = 60;
                            dataGridView1.Columns[7].Width = 80;
                            dataGridView1.Columns[8].Width = 80;
                            dataGridView1.Columns[9].Width = 30;
                            dataGridView1.Columns[10].Width = 30;
                            dataGridView1.Columns[11].Width = 90;
                            lblTotalChecks.Text = orderList.Count().ToString();
                            generateToolStripMenuItem.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Data From Ordering Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ProcessDataDefault()
        {
            try
            {
                deliveryDate = dateTimePicker1.Value;
                if (deliveryDate == dateTime)
                {
                    MessageBox.Show("Please set Delivery Date!");
                }

                else
                {
                    if (isValidateGeneration())
                    {
                        sReport();
                        if (cbDeliveryTo.Checked == true)
                            withDeliveryTo = 1;
                        else
                            withDeliveryTo = 0;
                        //  if (gClient.DataBaseName != "producers_history")
                        proc.Process2(orderList, this, int.Parse(txtDrNumber.Text), int.Parse(txtPackNumber.Text), DirectReportStyle, ProvincialReportStyle,withDeliveryTo);
                        ///  else
                        //   proc.Process(orderList, this, int.Parse(txtDrNumber.Text), int.Parse(txtPackNumber.Text));

                        tempDr.Clear();
                        proc.GetDRDetails(orderList[0].Batch.Trim(), tempDr);

                        tempDr.Clear();
                        proc.GetPackingListwithSticker(orderList[0].Batch, tempDr);

                        //if (gClient.ShortName == "PNB")
                        //    proc.GetStickerDetailsForPNB(tempSticker, orderList[0].Batch);
                        //else
                        proc.GetStickerDetails(tempSticker, orderList[0].Batch);

                        MessageBox.Show("Data has been process!!!");
                        ViewReports vp = new ViewReports();
                        // vp.MdiParent = this;
                        vp.Show();
                        reportsToolStripMenuItem.Enabled = true;
                        proc.DisableControls(deliveryReportToolStripMenuItem);
                        generateToolStripMenuItem.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Generate data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ProcessData()
        {
            try
            {
                deliveryDate = dateTimePicker1.Value;
                if (deliveryDate == dateTime)
                {
                    MessageBox.Show("Please set Delivery Date!");
                }

                else
                {
                    if (isValidateGeneration())
                    {
                        sReport();
                        if (cbDeliveryTo.Checked == true)
                            withDeliveryTo = 1;
                        else
                            withDeliveryTo = 0;
                        //  if (gClient.DataBaseName != "producers_history")
                        proc.Process2(orderList, this, int.Parse(txtDrNumber.Text), int.Parse(txtPackNumber.Text), DirectReportStyle, ProvincialReportStyle,withDeliveryTo);
                        ///  else
                        //   proc.Process(orderList, this, int.Parse(txtDrNumber.Text), int.Parse(txtPackNumber.Text));
                        MessageBox.Show("Data has been process!!!");
                        tempDr.Clear();
                        proc.fGetDrDirect(orderList[0].Batch.Trim(), tempDr);
                        ViewReports vpDrD = new ViewReports();
                        vpDrD.Show();
                        vpDrD.Text = "Delivery Receipt Direct Branches";

                        tempDr.Clear();
                        proc.fGetDrProvincial(orderList[0].Batch.Trim(), tempDr);
                        report = "DRP";
                        ViewReports vpDrP = new ViewReports();
                        vpDrP.Show();
                        vpDrP.Text = "Delivery Receipt Provincial Branches";

                        tempDr.Clear();
                        proc.GetPackingListwithSticker(orderList[0].Batch, tempDr);

                        //if (gClient.ShortName == "PNB")
                        //    proc.GetStickerDetailsForPNB(tempSticker, orderList[0].Batch);
                        //else
                        proc.GetStickerDetails(tempSticker, orderList[0].Batch);
                        

                        //ViewReports vp = new ViewReports();
                        //// vp.MdiParent = this;
                        //vp.Show();
                        reportsToolStripMenuItem.Enabled = true;
                        proc.DisableControls(deliveryReportToolStripMenuItem);
                        generateToolStripMenuItem.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Generate data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
