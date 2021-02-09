using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CPMS_Accounting.Models;
using static CPMS_Accounting.GlobalVariables;
using CPMS_Accounting.Procedures;
//using FastMember;
using CrystalDecisions.CrystalReports.Engine;
using CPMS_Accounting.Forms;
using System.Threading;

namespace CPMS_Accounting
{
    public partial class frmSalesInvoice : Form
    {

        List<SalesInvoiceModel> salesInvoiceList = new List<SalesInvoiceModel>();
        ProcessServices_Nelson proc = new ProcessServices_Nelson();
        Main frm;
        frmProgress progressBar;
        Thread thread;

        public frmSalesInvoice(Main frm1)
        {

            //Added Validation when unable to connect to server upon Opening salesinvoice form
            if (proc.errorMessage != null)
            {
                MessageBox.Show("Unable connecting to Server (pOpenDB) \r\n" + proc.errorMessage);
                Application.Exit();
            }

            InitializeComponent();
            ConfigureGrids();
            FillComboBoxes();
            ConfigureDesignLabels();
            salesInvoiceList.Clear();
            this.frm = frm1;


            bgwLoadBatchList.WorkerReportsProgress = true;

        }

        private void frmSalesInvoice_Load(object sender, EventArgs e)
        {

            //DataTable dt = new DataTable();
            //if (!proc.LoadUnprocessedSalesInvoiceData(ref dt))
            //{
            //    MessageBox.Show("Server Connection Error (LoadInitialData) \r\n" + proc.errorMessage);
            //    return;
            //}

            //dgvDRList.DataSource = dt;
            //dgvDRList.ClearSelection(); // remove first highlighted row in datagrid

            //txtSalesInvoiceNumber.Focus();

            RefreshView();
        }

        private void frmSalesInvoice_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.Hide();
            //Main main = new Main();
            //main.Show();
        }

        private void ConfigureGrids()
        {
            //GRID 1
            //dgvDRList.AutoGenerateColumns = false;
            dgvDRList.AllowUserToAddRows = false;
            dgvDRList.AllowUserToResizeColumns = false;
            dgvDRList.AllowUserToDeleteRows = false;
            dgvDRList.AllowUserToOrderColumns = false;
            dgvDRList.AllowUserToResizeRows = false;
            dgvDRList.AllowUserToAddRows = false;
            dgvDRList.ScrollBars = ScrollBars.Vertical;
            dgvDRList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //Rename datagrid columns programmatically
            dgvDRList.EditMode = DataGridViewEditMode.EditProgrammatically;

            //Added location field if PNB
            if (gClient.ShortName == "PNB")
            {
                //Column names and width setup
                dgvDRList.ColumnCount = 6; //COUNT OF COLUMNS THAT WILL DISPLAY IN GRID

                dgvDRList.Columns[0].Name = "QUANTITY";
                dgvDRList.Columns[0].Width = 70;
                dgvDRList.Columns[0].DataPropertyName = "Quantity";

                dgvDRList.Columns[1].Name = "BATCH NAME";
                dgvDRList.Columns[1].Width = 150;
                dgvDRList.Columns[1].DataPropertyName = "batch"; //this must be the actual table name in sql

                dgvDRList.Columns[2].Name = "CHECK NAME";
                dgvDRList.Columns[2].Width = 200;
                dgvDRList.Columns[2].DataPropertyName = "chequename";

                dgvDRList.Columns[3].Name = "CHECK TYPE";
                dgvDRList.Columns[3].Width = 104;
                dgvDRList.Columns[3].DataPropertyName = "ChkType";

                dgvDRList.Columns[4].Name = "DELIVERY DATE";
                dgvDRList.Columns[4].Width = 100;
                dgvDRList.Columns[4].DataPropertyName = "deliverydate";

                dgvDRList.Columns[5].Name = "LOCATION";
                dgvDRList.Columns[5].Width = 500;
                dgvDRList.Columns[5].DataPropertyName = "location";
            }
            else
            {
                //Column names and width setup
                dgvDRList.ColumnCount = 5; //COUNT OF COLUMNS THAT WILL DISPLAY IN GRID

                dgvDRList.Columns[0].Name = "QUANTITY";
                dgvDRList.Columns[0].Width = 70;
                dgvDRList.Columns[0].DataPropertyName = "Quantity";

                dgvDRList.Columns[1].Name = "BATCH NAME";
                dgvDRList.Columns[1].Width = 150;
                dgvDRList.Columns[1].DataPropertyName = "batch"; //this must be the actual table name in sql

                dgvDRList.Columns[2].Name = "CHECK NAME";
                dgvDRList.Columns[2].Width = 200;
                dgvDRList.Columns[2].DataPropertyName = "chequename";

                dgvDRList.Columns[3].Name = "CHECK TYPE";
                dgvDRList.Columns[3].Width = 104;
                dgvDRList.Columns[3].DataPropertyName = "ChkType";

                dgvDRList.Columns[4].Name = "DELIVERY DATE";
                dgvDRList.Columns[4].Width = 500;
                dgvDRList.Columns[4].DataPropertyName = "deliverydate";
            }
           

            //GRID 2
            //dgvDRList.AutoGenerateColumns = true;
            dgvListToProcess.AllowUserToAddRows = false;
            dgvListToProcess.AllowUserToResizeColumns = false;
            dgvListToProcess.AllowUserToDeleteRows = false;
            dgvListToProcess.AllowUserToOrderColumns = false;
            dgvListToProcess.AllowUserToResizeRows = false;
            dgvListToProcess.AllowUserToAddRows = false;
            dgvListToProcess.ScrollBars = ScrollBars.Vertical;
            dgvListToProcess.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //Rename datagrid columns programmatically
            dgvListToProcess.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvListToProcess.ColumnCount = 8; //COUNT OF COLUMNS THAT WILL DISPLAY IN GRID

            //Column names and width setup

            dgvListToProcess.Columns[0].Name = "QTY";
            dgvListToProcess.Columns[0].Width = 40;
            dgvListToProcess.Columns[0].DataPropertyName = "Quantity";

            dgvListToProcess.Columns[1].Name = "BATCH";
            dgvListToProcess.Columns[1].Width = 70;
            dgvListToProcess.Columns[1].DataPropertyName = "Batch"; //this must be the actual table name in sql

            dgvListToProcess.Columns[2].Name = "CHECK NAME";
            dgvListToProcess.Columns[2].Width = 180;
            dgvListToProcess.Columns[2].DataPropertyName = "CheckName";

            dgvListToProcess.Columns[3].Name = "DR LIST";
            dgvListToProcess.Columns[3].Width = 400;
            dgvListToProcess.Columns[3].DataPropertyName = "DRList";

            dgvListToProcess.Columns[4].Name = "CHECK TYPE";
            dgvListToProcess.Columns[4].Width = 50;
            dgvListToProcess.Columns[4].DataPropertyName = "checktype";

            dgvListToProcess.Columns[5].Name = "INVOICE DATE";
            dgvListToProcess.Columns[5].Width = 80;
            dgvListToProcess.Columns[5].DataPropertyName = "SalesInvoiceDate";

            dgvListToProcess.Columns[6].Name = "UNIT PRICE";
            dgvListToProcess.Columns[6].DefaultCellStyle.Format = "#,0.00##";
            dgvListToProcess.Columns[6].Width = 100;
            dgvListToProcess.Columns[6].DataPropertyName = "UnitPrice";

            dgvListToProcess.Columns[7].Name = "AMOUNT";
            dgvListToProcess.Columns[7].DefaultCellStyle.Format = "#,0.00##";
            dgvListToProcess.Columns[7].Width = 100;
            dgvListToProcess.Columns[7].DataPropertyName = "LineTotalAmount";

        }

        private void dgvDRList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        
        private void btnViewSelected_Click(object sender, EventArgs e)
        {

            AddSelectedDRRow();
            

        }

        private void AddSelectedDRRow()
        {
            
            if (dgvDRList.SelectedRows != null && dgvDRList.SelectedRows.Count > 0)
            {

                foreach (DataGridViewRow row in dgvDRList.SelectedRows)
                {
                    SalesInvoiceModel line = new SalesInvoiceModel();
                    
                    line.Batch = row.Cells["batch Name"].Value.ToString();
                    line.checkName = row.Cells["check name"].Value.ToString();
                    line.checkType = row.Cells["check type"].Value.ToString();
                    line.salesInvoiceDate = DateTime.Parse(dtpInvoiceDate.Value.ToShortDateString());
                    line.deliveryDate = DateTime.Parse(row.Cells["Delivery Date"].Value.ToString());
                    line.Quantity = int.Parse(row.Cells["Quantity"].Value.ToString());

                    //Include Location field if PNB
                    if (gClient.ShortName == "PNB")
                    {
                        line.Location = row.Cells["location"].Value.ToString();
                    }

                   
                    line.unitPrice = proc.GetUnitPrice(line.checkName);
                    line.lineTotalAmount = Math.Round(line.Quantity * line.unitPrice, 2);
                    
                    //Check if record is already inserted
                    if (p.BatchRecordHasDuplicate(line, salesInvoiceList))
                    {
                        MessageBox.Show("Selected Batch already added");
                        return;
                    }

                    //(Validation) Checing of Onhand quantity for PNB
                    if (gClient.ShortName == "PNB")
                    {
                       
                        frmMessageInput xfrm = new frmMessageInput();
                        xfrm.labelMessage = "Input Purchase Order Number:";
                        DialogResult result = xfrm.ShowDialog();
                        
                        if (result == DialogResult.OK)
                        {
                            //Use this when data is large
                            //Added this for large data processing
                            //progressBar = new frmProgress();
                            //progressBar.message = "Validating selected item";
                            //thread = new Thread(() => progressBar.ShowDialog());
                            //thread.Start();

                            line.PurchaseOrderNumber = int.Parse(xfrm.userInput);
                            double remainingQuantity = 0;
                            
                            //Check if quantity is sufficient
                            if (!proc.IsQuantityOnHandSufficient(line.Quantity, line.checkName, line.PurchaseOrderNumber, ref remainingQuantity, ref salesInvoiceList))
                            {
                                //thread.Abort();
                                MessageBox.Show("Error on (Procedure ChequeQuantityIsSufficient) \r\n \r\n" + proc.errorMessage);
                                return;
                            }
                            line.RemainingQuantity = remainingQuantity;


                        }
                        else if (result == DialogResult.Cancel)
                        {
                            //thread.Abort();
                            return;
                        }
                    }

                    line.drList = proc.GetDRList(line.Batch, line.checkType, line.deliveryDate, line.Location);
                    salesInvoiceList.Add(line);

                }

                //created 'list' variable column sorting by line for datagrid view 
                var sortedList = salesInvoiceList
                    .Select
                    (i => new { i.Quantity, i.Batch, i.checkName, i.drList, i.checkType, i.salesInvoiceDate, i.unitPrice, i.lineTotalAmount })

                    .ToList();

                dgvListToProcess.DataSource = sortedList;
                dgvListToProcess.ClearSelection();

            }
            else
            {
                MessageBox.Show("Please select at least one record");
            }


            
                //thread.Abort();




        }

        private void btnPrintSalesInvoice_Click(object sender, EventArgs e)
        {


            if (dgvListToProcess.Rows.Count == 0)
            {
                MessageBox.Show("Please select record from Batch List.");
            }
            else if (!p.ValidateInputFieldsSI(txtSalesInvoiceNumber.Text.ToString(), cbCheckedBy.Text.ToString(), cbApprovedBy.Text.ToString()))
            {
                MessageBox.Show("Please supply values in blank field(s)");
            }
            else
            {

                DialogResult result = MessageBox.Show("This will process Sales Invoice on selected DR's. \r\n Select 'YES' to proceed.", "Confirmation", MessageBoxButtons.YesNo);
                
                if (result == DialogResult.Yes)
                {

                    ProcessServices_Nelson proc = new ProcessServices_Nelson();

                    if (!proc.UpdateTempTableSI(salesInvoiceList))
                    {
                        MessageBox.Show("Sales Invoice Temp Table Update Error (UpdateTempTable). \r\n" + proc.errorMessage);
                        return;
                    }

                    //Fill gSalesInvoiceFinished Model Class
                    //gSalesInvoiceList = salesInvoiceList;
                    gSalesInvoiceFinished.ClientCode = gClient.ClientCode.ToString();
                    gSalesInvoiceFinished.SalesInvoiceDateTime = dtpInvoiceDate.Value;
                    gSalesInvoiceFinished.GeneratedBy = gUser.UserName.ToString();
                    gSalesInvoiceFinished.CheckedBy = cbCheckedBy.Text.ToString();
                    gSalesInvoiceFinished.ApprovedBy = cbApprovedBy.Text.ToString();
                    gSalesInvoiceFinished.SalesInvoiceNumber = double.Parse(txtSalesInvoiceNumber.Text.ToString());
                    gSalesInvoiceFinished.TotalAmount = double.Parse(salesInvoiceList.Sum(x => x.lineTotalAmount).ToString());
                    gSalesInvoiceFinished.VatAmount = p.GetVatAmount(gSalesInvoiceFinished.TotalAmount);
                    gSalesInvoiceFinished.NetOfVatAmount = p.GetNetOfVatAmount(gSalesInvoiceFinished.TotalAmount);
                   
                    ///Sort Sales Invoice By Batch before saving and Printing
                    var sortedList = salesInvoiceList.OrderBy(x => x.Batch).ToList();

                    ///Update Database
                    if (!proc.UpdateSalesInvoiceHistory(sortedList))
                    {
                        MessageBox.Show("Error updating sales invoice record to server. (proc.UpdateSalesInvoiceHistory) \r\n" + proc.errorMessage);
                        return;
                    }

                    //Update Quantity On hand for PNB
                    if (gClient.ShortName == "PNB")
                    {
                        foreach(var item in sortedList)
                        {

                            if (!proc.UpdateItemQuantityOnhand(item.Quantity, item.checkName, item.PurchaseOrderNumber))
                            {
                                MessageBox.Show("Error on (Procedure ChequeQuantityIsSufficient) \r\n \r\n" + proc.errorMessage);
                                return;
                            }
                        }
                    }

                    //Create new instance of the document/ Prepare report using Crystal Reports
                    ReportDocument crystalDocument = new ReportDocument();
                    
                    //Check RPT File
                    if (!p.LoadReportPath("SalesInvoice", ref crystalDocument))
                    {
                        MessageBox.Show("SalesInvoice.rpt File not found");
                        return;
                    }

                    //Supply Data source to document
                    crystalDocument.SetDataSource(sortedList);

                    //Install Fastmember from nuGet for Fast (List to Datatable) Conversion
                    //DataTable dt = new DataTable();
                    //using (var reader = ObjectReader.Create(sortedList))
                    //{
                    //    dt.Load(reader);
                    //}
                    /////Supply datatable from the list converted
                    //gReportDT = dt;


                    //Supply details on report parameters
                    p.FillCRReportParameters("SalesInvoice", ref crystalDocument);


                    //Supply these details into Global ReportDocument to be able for the report viewer to initialize the rerport
                    gCrystalDocument = crystalDocument;

                    frmReportViewer crForm = new frmReportViewer();
                    crForm.Show();
                    RefreshView();

                }

            }

        }

        private void txtSalesInvoiceNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSalesInvoiceNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (p.IsKeyPressedNumeric( ref sender, ref e))
            {
                e.Handled = true;
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            SearchText();
           
        }

        private void btnReloadDrList_Click(object sender, EventArgs e)
        {

            RefreshView();
        }

        private void RefreshView()
        {

            salesInvoiceList.Clear();
            txtSearch.Text = "";
            txtSalesInvoiceNumber.Text = "";
            txtSalesInvoiceNumber.Focus();
            cbCheckedBy.Text = "";
            cbApprovedBy.Text = "";

            salesInvoiceList.Clear();
            

            DisableControls();

           


        }

        private void SearchText()
        {
            DataTable dt = new DataTable();
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MessageBox.Show("Please input batch number to search");
                RefreshDrList();

            }
            else
            {
                if (!proc.BatchSearch(txtSearch.Text, ref dt))
                {
                    MessageBox.Show("Unable to connect to server. (proc.BatchSearch)\r\n" + proc.errorMessage);
                    return;
                }



                //_ = dt.Rows.Count != 0 ? dgvDRList.DataSource = dt : MessageBox.Show("No results found");
                ///02032021 Updated statement above. Made Dr List refreshed if  no records found
                if (dt.Rows.Count > 0)
                {
                    dgvDRList.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("No results found");
                    RefreshDrList();
                }
                

                
                txtSearch.Focus();
                txtSearch.SelectAll();
                dgvDRList.ClearSelection();
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FillComboBoxes()
        {
            DataTable dt = new DataTable();
            if (!proc.GetUserNames(ref dt))
            {
                MessageBox.Show("Unable to connect to server. \r\n" + proc.errorMessage);
            }

            _ = dt.Rows.Count != 0 ? cbCheckedBy.DataSource = dt : cbCheckedBy.DataSource = null;
            cbCheckedBy.BindingContext = new BindingContext();
            cbCheckedBy.DisplayMember = "UserName";
            cbCheckedBy.SelectedIndex = -1;

            _ = dt.Rows.Count != 0 ? cbApprovedBy.DataSource = dt : cbApprovedBy.DataSource = null;
            cbApprovedBy.BindingContext = new BindingContext();
            cbApprovedBy.DisplayMember = "UserName";
            cbApprovedBy.SelectedIndex = -1;

        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchText();
            }
        }

        private void btnReprint_Click(object sender, EventArgs e)
        {
            int salesInvoiceNumber = int.Parse(txtSalesInvoiceNumber.Text);
            DialogResult result = MessageBox.Show("Reprint Invoice Number " + salesInvoiceNumber.ToString() + "?","Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ReprintSalesInvoice(salesInvoiceNumber);
            }
        }

        public void ReprintSalesInvoice(int salesInvoiceNumber)
        {

            //get Finished Sales Inbvoice details if exist
            DataTable siFinishedDT = new DataTable();
            if (!proc.SalesInvoiceExist(salesInvoiceNumber, ref siFinishedDT))
            {
                MessageBox.Show("Unable to find provided Sales Invoice Number. (proc.SalesInvoiceExist)\r\n" + proc.errorMessage);
                return;
            }

            //Supply Global SalesInvoiceFinished Object variables based on fetched data
            foreach (DataRow row in siFinishedDT.Rows)
            {

                gSalesInvoiceFinished.ClientCode = row.Field<string>("ClientCode");
                gSalesInvoiceFinished.SalesInvoiceNumber = row.Field<double>("SalesInvoiceNumber");
                gSalesInvoiceFinished.SalesInvoiceDateTime = row.Field<DateTime>("SalesInvoiceDateTime");
                gSalesInvoiceFinished.GeneratedBy = row.Field<string>("GeneratedBy");
                gSalesInvoiceFinished.CheckedBy = row.Field<string>("CheckedBy");
                gSalesInvoiceFinished.ApprovedBy = row.Field<string>("ApprovedBy");
                gSalesInvoiceFinished.TotalAmount = row.Field<double>("TotalAmount");
                gSalesInvoiceFinished.VatAmount = row.Field<double>("VatAmount");
                gSalesInvoiceFinished.NetOfVatAmount = row.Field<double>("NetOfVatAmount");

                //PNB
                if (gClient.ShortName == "PNB")
                {
                }

            }

            //Get Sales Invoice List Details to be supplied to Global Report Datatable
            DataTable siListDT = new DataTable();
            if (!proc.GetOldSalesInvoiceList(salesInvoiceNumber, ref siListDT))
            {
                MessageBox.Show("Unable to connect to server. (proc.SalesInvoiceExist)\r\n" + proc.errorMessage);
                return;
            }


            //Create new instance of the document.
            ReportDocument crystalDocument = new ReportDocument();


            //Load path of the report
            if (!p.LoadReportPath("SalesInvoice", ref crystalDocument))
            {
                MessageBox.Show("SalesInvoice.rpt File not found");
                return;
            }

            //Supply report Data Source
            crystalDocument.SetDataSource(siListDT);

            //Supply values on report parameters.
            p.FillCRReportParameters("SalesInvoice", ref crystalDocument);

            //Tag newly created report to global Crystal Document
            gCrystalDocument = crystalDocument;

            //Load report Viewer
            frmReportViewer crForm = new frmReportViewer();
            crForm.Show();

        }

        public void ConfigureDesignLabels()
        {
            string fullname = gUser.UserName + " " + gUser.LastName ;

            lblUserName.Text = fullname;
            lblBankName.Text = gClient.Description;

        }

        private void dgvDRList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AddSelectedDRRow();
        }

        public void DisableControls()
        {

            gbSearch.Enabled = false;
            gbBatchList.Enabled = false;
            gbDetails.Enabled = false;
            gbBatchToProcess.Enabled = false;
            pnlActionButtons.Enabled = false;
            gbSINo.Enabled = true;

            dgvDRList.ClearSelection();

            var sortedList = salesInvoiceList
                    .Select
                    (i => new { i.Quantity, i.Batch, i.checkName, i.drList, i.checkType, i.salesInvoiceDate, i.unitPrice, i.lineTotalAmount })
                    .ToList();

            dgvListToProcess.DataSource = sortedList;
            dgvListToProcess.ClearSelection();

            txtSalesInvoiceNumber.Focus();
          
        }

        public void EnableControls()
        {

            //Enable ProgressbarView
            progressBar = new frmProgress();
            progressBar.message = "Loading Data. Please Wait.";
            thread = new Thread(() => progressBar.ShowDialog());
            thread.Start();

            gbSearch.Enabled = true;
            gbBatchList.Enabled = true;
            gbDetails.Enabled = true;
            gbBatchToProcess.Enabled = true;
            pnlActionButtons.Enabled = true;
            gbSINo.Enabled = false;

            //Enable all Action Buttons
            btnCancelSiRecord.Enabled = false;
            btnReprint.Enabled = false;
            btnReloadDrList.Enabled = true;
            btnPrintSalesInvoice.Enabled = true;
            btnViewSelected.Enabled = true;

            

            DataTable dt = new DataTable();
            if (!proc.LoadUnprocessedSalesInvoiceData(ref dt))
            {
                thread.Abort();
                MessageBox.Show("Error on (proc.LoadUnprocessedSalesInvoiceData)\r\n \r\n" + proc.errorMessage);
                return;
            }
            

            dgvDRList.DataSource = dt;


            //Thread getBatchListJob = new Thread(new ThreadStart(RefreshDrList));
            //getBatchListJob.Start();
            //if (getBatchListJob.IsAlive)
            //{
            //    DialogResult result = progressBar.ShowDialog();
            //}
            

            dgvDRList.ClearSelection();

            var sortedList = salesInvoiceList
                    .Select
                    (i => new { i.Quantity, i.Batch, i.checkName, i.drList, i.checkType, i.salesInvoiceDate, i.unitPrice, i.lineTotalAmount })
                    .ToList();

            dgvListToProcess.DataSource = sortedList;
            dgvListToProcess.ClearSelection();

            txtSalesInvoiceNumber.Focus();

            //Abort progressbar view
            thread.Abort();

        }

        private void btnAddRecord_Click(object sender, EventArgs e)
        {

            AddRecord();

        }

        private void txtSalesInvoiceNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddRecord();

            }
           
        }

        private void DisplayOldSalesInvoiceList(int salesInvoiceNumber, ref DataTable dt)
        {
            //Start Progress Bar View
            progressBar = new frmProgress();
            progressBar.message = "Fetching Existing Record. Please Wait.";
            thread = new Thread(() => progressBar.ShowDialog());
            thread.Start();

            //Get Sales Invoice List Details to be supplied to Global Report Datatable
            DataTable siListDT = new DataTable();
            if (!proc.GetOldSalesInvoiceList(salesInvoiceNumber, ref siListDT))
            {
                thread.Abort();
                MessageBox.Show("Unable to connect to server. (proc.SalesInvoiceExist)\r\n" + proc.errorMessage);
                RefreshView();
                return;
            }
            

            //Display values on Front End from Finished Table
            foreach (DataRow row in dt.Rows)
            {

                gSalesInvoiceFinished.ClientCode = row.Field<string>("ClientCode");
                gSalesInvoiceFinished.SalesInvoiceDateTime = row.Field<DateTime>("SalesInvoiceDateTime");
                gSalesInvoiceFinished.GeneratedBy = row.Field<string>("GeneratedBy");
                gSalesInvoiceFinished.CheckedBy = row.Field<string>("CheckedBy");
                gSalesInvoiceFinished.ApprovedBy = row.Field<string>("ApprovedBy");
                gSalesInvoiceFinished.SalesInvoiceNumber = row.Field<double>("SalesInvoiceNumber");
                gSalesInvoiceFinished.TotalAmount = row.Field<double>("TotalAmount");
                gSalesInvoiceFinished.VatAmount = row.Field<double>("VatAmount");
                gSalesInvoiceFinished.NetOfVatAmount = row.Field<double>("NetOfVatAmount");

                dtpInvoiceDate.Value = gSalesInvoiceFinished.SalesInvoiceDateTime;
                cbCheckedBy.Text = gSalesInvoiceFinished.CheckedBy;
                cbApprovedBy.Text = gSalesInvoiceFinished.ApprovedBy;

            }

           

            foreach (DataRow row in siListDT.Rows)
            {

                SalesInvoiceModel line = new SalesInvoiceModel();

                line.Batch = row.Field<string>("Batch");
                line.checkName = row.Field<string>("CheckName");
                line.checkType = row.Field<string>("ChkType");
                line.deliveryDate = row.Field<DateTime>("deliverydate");
                line.Quantity = Convert.ToInt32(row.Field<Int64>("Quantity"));
                line.drList = row.Field<string>("DRList");
                line.unitPrice = row.Field<double>("UnitPrice");
                line.lineTotalAmount = row.Field<double>("LineTotalAmount");
                line.salesInvoiceDate = row.Field<DateTime>("SalesInvoiceDate");

                if (gClient.ShortName == "PNB")
                {
                    //ABANG MUNA
                    //line.PurchaseOrderNumber = Convert.ToInt32(row.Field<Int64>("PurchaseOrderNumber"));
                }
               
                salesInvoiceList.Add(line);
            }
           


            //created 'list' variable column sorting by line for datagrid view 
            var sortedList = salesInvoiceList
                .Select
                (i => new { i.Quantity, i.Batch, i.checkName, i.drList, i.checkType, i.salesInvoiceDate, i.unitPrice, i.lineTotalAmount })

                .ToList();

            dgvListToProcess.DataSource = sortedList;
            dgvListToProcess.ClearSelection();

            gbSINo.Enabled = false;
            pnlActionButtons.Enabled = true;
            btnCancelSiRecord.Enabled = true;
            btnReprint.Enabled = true;
            btnReloadDrList.Enabled = true;
            btnPrintSalesInvoice.Enabled = false;
            btnViewSelected.Enabled = false;


            //abort progress bar view
            thread.Abort();


        }

        private void AddRecord()
        {
            if (!string.IsNullOrWhiteSpace(txtSalesInvoiceNumber.Text.ToString()))
            {
                DataTable dt = new DataTable();
                int salesInvoiceNumber = int.Parse(txtSalesInvoiceNumber.Text.ToString());
                bool isSalesInvoiceCancelled = Convert.ToBoolean(proc.SeekReturn("select iscancelled from "+ gClient.SalesInvoiceFinishedTable +" where salesinvoicenumber = "+ salesInvoiceNumber +"", false));

                if (proc.SalesInvoiceExist(salesInvoiceNumber, ref dt) && isSalesInvoiceCancelled == true)
                {
                    MessageBox.Show("Entered S.I. Number is already cancelled");
                    RefreshView();
                }
                else if (proc.SalesInvoiceExist(int.Parse(txtSalesInvoiceNumber.Text.ToString()), ref dt))
                {
                    DisplayOldSalesInvoiceList(int.Parse(txtSalesInvoiceNumber.Text.ToString()), ref dt);

                    //DisplayOldSalesInvoiceList(int.Parse(txtSalesInvoiceNumber.Text.ToString()), ref dt);
                }
                else
                {
                    EnableControls();
                }
            }
        }

        private void btnCancelSiRecord_Click(object sender, EventArgs e)
        {
            CancelSalesInvoiceRecord();
        }

        private void CancelSalesInvoiceRecord()
        {

            int salesInvoiceNumber = int.Parse(txtSalesInvoiceNumber.Text.ToString());

            DialogResult result = MessageBox.Show("Cancel Invoice Number " + salesInvoiceNumber.ToString() + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                if (!proc.RemoveSalesInvoiceTagOnHistory(salesInvoiceNumber))
                {
                    MessageBox.Show("Error on (proc.RemoveSalesInvoiceTag)\r\n \r\n" + proc.errorMessage);
                    return;
                }
                lblRowsAffected.Text = "Total Rows Updated: " + proc.RowNumbersAffected.ToString();

                if (!proc.UpdateSalesInvoiceStatusOnfinished(salesInvoiceNumber, 1))
                {
                    MessageBox.Show("Error on (proc.UpdateSalesInvoiceStatusOnfinished()\r\n \r\n" + proc.errorMessage);
                    return;
                }
                lblRowsAffected.Text = "Total Rows Updated: " + proc.RowNumbersAffected.ToString();
                MessageBox.Show("Record Updated!");
                RefreshView();
            }

        }


        //Huge Data Handling
        private void RefreshDrList()
        {
            DataTable dt = new DataTable();
            proc.LoadUnprocessedSalesInvoiceData(ref dt);
            p.setDataSource(ref dt, ref progressBar, dgvDRList);
        }

        private void btnCancelClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bgwLoadBatchList_DoWork(object sender, DoWorkEventArgs e)
        {

            frmProgress progressbar = new frmProgress();
            progressbar.DialogResult = DialogResult.Cancel;
            RefreshDrList();
           
        }

        private void bgwLoadBatchList_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        public void SeekDRList(ref string drList, string Batch, string checkType, DateTime deliveryDate, string Location)
        {
            DataTable dt = new DataTable();
            drList = proc.GetDRList(Batch, checkType, deliveryDate, Location);
            p.setDataSource(ref dt, ref progressBar, dgvDRList);

        }

        private void CallProgressBar()
        {
            progressBar.BringToFront();
            progressBar1.Size = new Size(673, 23);
            progressBar1.Location = new Point(6, 153);
             
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.Visible = true;
        }








    }

}
