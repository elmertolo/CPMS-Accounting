using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CPMS_Accounting.Procedures;
using static CPMS_Accounting.GlobalVariables;
//using NA_ExcelWriter;
using CPMS_Accounting.Models;
using System.Diagnostics;
using NA_ExcelWriter;
using System.IO;
using System.Reflection;


namespace CPMS_Accounting.Forms
{
    public partial class frmCostDistribution : Form
    {

        List<CostDistributionModel> costDistributionList = new List<CostDistributionModel>();


        ProcessServices_Nelson proc = new ProcessServices_Nelson();
        //02152021 Log4Net
        private log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        Main frm;
        frmProgress progressBar;
        Thread thread;


        public frmCostDistribution(Main frm1)
        {
            InitializeComponent();
            ConfigureGrids();
            FillComboBoxes();
            ConfigureDesignLabels();
            //costDistributionList.Clear();
            this.frm = frm1;
        }

        private void button1_Click(object sender, EventArgs e)
        {

      

           
        }

        private void frmCostDistribution_Load(object sender, EventArgs e)
        {
            log.Info("Sales Invoice Form Load");
            //DataTable dt = new DataTable();
          

            RefreshView();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ExportCDRToEcxel(string salesInvoiceNumber)
        {

            if (gClient.ShortName.ToUpper() == "UNIONBANK")
            {

                //Enable ProgressbarView
                progressBar = new frmProgress();
                progressBar.message = "Creating Excel Sheet. Please Wait.";
                thread = new Thread(() => progressBar.ShowDialog());
                thread.Start();

                Stopwatch timer = Stopwatch.StartNew();

                //Initialize Excel
                ExcelWriter excel = new ExcelWriter();

                int bookletGrandTotal = 0;
                int pcsGrandTotal = 0;
                double printingCostGT = 0;
                double documentStampGT = 0;
                double CDRGrandTotal = 0;

                excel.LastOccupiedRow = 20;

                #region Report Header Configurations Section

                //Insert LOGO
                //InsertImageToExelWorkSheet(p.GetApplicationPath() + @"\Image\ExcelReportHeaderLogo.jpg", 150, 5, 500, 59, ref workSheet);

                excel.InsertSingleCaption("ALLOCATION FOR RDS DISBURSEMENTS TEMPORARILY BOOKED IN S/D-901-394", "A2", "A2", true, "left");

                //Add List of Headers in a Continous cell.
                string[] captions =
                    { "Supplier/Contractor",
                        "Type of Service Rendered",
                        "GL Code for Type of expense",
                        "Requesting Unit",
                        "Request Number",
                        "Sales Invoice Number",
                        "Period Coverage",
                        "Total Amount" };
                excel.SupplySeriesOfCaptions(captions, "A4", false, false, "left");

                string[] captions2 =
                           { ":          CAPTIVE PRINTING CORP.",
                        ":          Services",
                        ":          (to be filled out by CPS Verifier)",
                        ":          CPS - PST",
                        ":          (to be filled out by CPS Verifier)",
                        ":          " + salesInvoiceNumber.ToString(),
                        ":          ",
                        ":          " };
                excel.SupplySeriesOfCaptions(captions2, "B4", false, false, "left");

                //Add List of Headers in a Continous cell.
                string[] captions3 =
                    { "Branch / Unit Name",
                        "Quantity",
                        "UOM",
                        "SOL",
                        "RC",
                        "FORACID \r\n(to be filled out by CPS Verifier.)",
                        "TRANCODE \r\n(to be filled out by CPS Verifier.)",
                        "PARTICULARS \r\n(to be filled out by CPS Verifier.)" };
                excel.SupplySeriesOfCaptions(captions3, "A13", true, false, "left");


                excel.SetRangeOfRowMerge("A13", "H13", 2);

                excel.SupplyBorderRange("A13", "J14");

                excel.AddBackgroundColor("A13", "J14", Color.LightBlue);

                string[] captions4 = { "Amount\r\nPrinting Cost", "Documentary\r\nStamps" };
                excel.SupplySeriesOfCaptions(captions4, "I13", true, false, "center");

                excel.AutoFitRangeOfColumns("I13", "J14");

                excel.InsertSingleCaption("Stationeries & Supplies Used.\r\nREIMBURSEMENT", "I14", "J14", true, "center");

                #endregion

                #region Report Body Configurations Section

                excel.SetRowToWrite(16);

                DataTable dt = new DataTable();
                if (!proc.GetData("Select chequename  from " + gClient.DataBaseName +
                            " where salesinvoice = " + salesInvoiceNumber + "" +
                            " group by chequename" +
                            " order by chequename;", ref dt))
                {
                    p.MessageAndLog("Server connection error (proc.GetData)\r\n\r\n" + proc.errorMessage + "", ref log, "error");
                }

                //Query records for each ChequeName..
                foreach (DataRow row in dt.Rows)
                {

                    string productType = row.Field<string>("chequename").Replace("'", "''");
                    //string brstn = row.Field<string>("brstn");
                    //string branchCode = proc.SeekReturn("select branchcode from " + gClient.BranchesTable + " where brstn = " + brstn + "", 0).ToString();

                    excel.InsertSingleCaption(productType.ToUpper(), "A" + excel.LastOccupiedRow, "A" + excel.LastOccupiedRow, true, "left", 1);
                    excel.LastOccupiedRow += 1;

                    //Separate queries for direct and provincial...
                    string[] locations = { "Direct", "Provincial" };
                    foreach (string location in locations)
                    {

                        //string location = row.Field<string>("location").Replace("'", "''");

                        //Insert datatable to cells beginning from starting cell provided.
                        //DIRECT
                        System.Data.DataTable data = new System.Data.DataTable();
                        if (!proc.GetData("select md.BranchName," +
                            " count(md.BranchName) * multiplier as Quantity," +
                            " md.unit as UOM," +
                            " br.SOL as SOL," +
                            " '''000' as RC," +
                            " '' as FORACID," +
                            " '' as TRANCODE," +
                            " '' as PARTICULARS, " +
                            " round(sum(unitPrice),2) as UnitPrice, round(sum(Docstamp2),2) as Docstamp" +
                            " from " + gClient.DataBaseName + " md" +
                            " inner join " + gClient.BranchesTable + " br" +
                            " on br.brstn = md.brstn" +
                            //" where salesInvoice = " + salesInvoiceNumber + " and location = '" + location + "' and chequename = '" + productType + "'" +
                            " where salesInvoice = " + salesInvoiceNumber + " and md.location = '" + location + "' and md.chequename = '" + productType + "'" +
                            " group by md.BranchName order by branchname", ref data))
                        {
                            p.MessageAndLog("Error upon retrieving data from database (ProcessCostDistribution (proc.GetData))\r\n\r\n" + proc.errorMessage, ref log, "error");
                            return;
                        }
                        ///Insert 'Direct' check type
                        if (data.Rows.Count > 0)
                        {


                            //Compute Cost Distribution grandtotals.
                            for (int i = 0; i < data.Rows.Count; i++)
                            {

                                //Sum Booklet and Pcs GrandTotal
                                if (data.Rows[i].Field<string>("UOM").ToUpper() == "BKT")
                                {
                                    bookletGrandTotal += Convert.ToInt32(data.Rows[i].Field<double?>("Quantity") ?? 0);
                                }
                                else if (data.Rows[i].Field<string>("UOM").ToUpper() == "PCS")
                                {
                                    pcsGrandTotal += Convert.ToInt32(data.Rows[i].Field<double?>("Quantity") ?? 0);
                                }
                                printingCostGT += data.Rows[i].Field<double?>("UnitPrice") ?? 0;
                                documentStampGT += data.Rows[i].Field<double?>("DocStamp") ?? 0;

                            }

                            //InsertSingleCaption(location.ToUpper() + " BRANCHES)", "A" + lastOccupiedRow, "A" + lastOccupiedRow, true, "left", ref workSheet, ref cellRange);
                            excel.InsertSingleCaption("(" + location.ToUpper() + " BRANCHES)", "A" + excel.LastOccupiedRow, "A" + excel.LastOccupiedRow, true, "left");
                            excel.LastOccupiedRow += 1;

                            excel.InsertDataTableToExcelCell("A" + excel.LastOccupiedRow, false, "left", ref data);
                            excel.LastOccupiedRow += 1;

                            excel.InsertSingleCaption("Subtotal", "A" + excel.LastOccupiedRow, "A" + excel.LastOccupiedRow, true, "right");
                            excel.InsertSeriesOfSum("B" + excel.StartingRowToSum, "B" + excel.EndingRowToSum, "#,##");
                            excel.InsertSeriesOfSum("I" + excel.StartingRowToSum, "J" + excel.EndingRowToSum);

                            //Insert single row.
                            excel.LastOccupiedRow += 1;
                            excel.BodyLastOccupiedRow = excel.LastOccupiedRow;

                        }

                    }

                }
                //Compute GrandTotal
                CDRGrandTotal = (printingCostGT + documentStampGT);



                #endregion

                #region Report Footer Configurations Section

                excel.LastOccupiedRow += 1;
                //Booklet GrandTotal
                excel.InsertSingleCaption("Booklet Grand Total", "A" + excel.LastOccupiedRow, "A" + excel.LastOccupiedRow, true, "right");
                excel.InsertSingleCaption(bookletGrandTotal.ToString(), "B" + excel.LastOccupiedRow, "B" + excel.LastOccupiedRow, true, "right");

                //Printing Cost Grand Total Display
                excel.InsertSingleCaption(printingCostGT.ToString(), "I" + excel.LastOccupiedRow, "I" + excel.LastOccupiedRow, true, "right", numberFormat: "#,##0.00");

                //Docstamp Display
                excel.InsertSingleCaption(documentStampGT.ToString(), "J" + excel.LastOccupiedRow, "J" + excel.LastOccupiedRow, true, "right", numberFormat: "#,##0.00");
                excel.LastOccupiedRow += 1;

                //Pcs Grand Total
                excel.InsertSingleCaption("PCS Grand Total", "A" + excel.LastOccupiedRow, "A" + excel.LastOccupiedRow, true, "right");
                excel.InsertSingleCaption(pcsGrandTotal.ToString(), "B" + excel.LastOccupiedRow, "B" + excel.LastOccupiedRow, true, "right");
                excel.LastOccupiedRow += 1;


                #endregion

                #region Additional Formatting Section

                //AutoFit columns from range provided.

                //cellRange = workSheet.get_Range("A1", "J" + lastOccupiedRow.ToString());
                //cellRange.Font.FontStyle = "Times New Roman";

                excel.SetColumnsWidth("A13", "A13", 27.71);

                excel.SetColumnsWidth("B13", "D13", 8.00);

                excel.SetColumnsWidth("E13", "G13", 13.57);

                excel.SetRowsHeight("A13", "I13", 30.00);

                excel.SetColumnsWidth("H13", "J13", 14.00);

                excel.SetColumnsWidth("A1", "A1", 63.14);

                excel.SetRowsHeight("A14", "I14", 36.75);

                excel.SetColumnsWidth("B1", "B1", 12.00);




                //WrapText
                //workSheet.Rows.WrapText = true;
                //workSheet.Rows[13].WrapText = true;

                //Apply Range Merge on supplied series of header
                //SetRangeOfRowMerge("D9", "G9", 2, ref cellRange, ref workSheet);
                //SetRangeOfRowMerge("I9", "L9", 2, ref cellRange, ref workSheet);

                //Set Font om a specified range
                //SetRangeFont("C9", "M9", "bold", ref workSheet);

                //Add Border on a specified range of cell
                // 1 = Solid Line, 2 = Dotted Line Border

                excel.SupplyBorderRange("A15", "J" + (excel.BodyLastOccupiedRow - 1));

                excel.SetHorizontalAlignment("E16", "E" + excel.LastOccupiedRow, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight);

                //Display Grand total in header
                excel.InsertSingleCaption(":          " + Math.Round(CDRGrandTotal, 2).ToString("#,##0.00"), "B11", "B11", true, "Left", numberFormat: "#,##0.00");

                #endregion

                #region Save Excel File
                //Make Directory for excel file to be saved if not exist.
                string excelFolderPath = p.ReadJsonConfigFile("CostDistribution", "OutputPathFolder", p.GetApplicationPath() + @"\Output");
                if (!excelFolderPath.EndsWith("\'")) { excelFolderPath += "\\"; }
                if (!Directory.Exists(excelFolderPath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(excelFolderPath);
                }
                string fileName = "CostDistribution_" + DateTime.Now.ToString("MMddyyyy") + ".";
                string fileNameExtension = p.ReadJsonConfigFile("CostDistribution", "OutputFileType", "xlsx");

                excel.Save(excelFolderPath + fileName + fileNameExtension);

                //Optional......Kill Excel task due to multiple running application.... 
                //Process.Start("C:\\Windows\\System32\\Taskkill.exe /F /IM Excel.exe");
                //foreach (Process proc in Process.GetProcessesByName("EXCEL"))
                //{
                //    proc.Kill();
                //}

                #endregion

                timer.Stop();
                TimeSpan timespan = timer.Elapsed;
                thread.Abort();

                //Give user an option to open containing folder.
                DialogResult result = MessageBox.Show("File saved. Open containing folder?\r\n\r\nElapsed Time: " + timespan.ToString("t") + "", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    p.OpenFolder(excelFolderPath);
                }

            }
            else
            {
                p.MessageAndLog("Cost Distribution Process for bank: " + gClient.ShortName.ToUpper() + " is not ready." +
                    "Please contact your system administrator.", ref log, "info");
            }



        }

       

        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            
            
            FindRecord();


        }

        private void RefreshView()
        {
            log.Info("Refreshing Display");


            txtSearch.Text = "";
            txtSalesInvoiceNumber.Text = "";
            txtSalesInvoiceNumber.Focus();
            cbCheckedBy.Text = "";
            cbApprovedBy.Text = "";

            costDistributionList.Clear();



            DisableControls();
        }

        private void DisableControls()
        {
            gbSearch.Enabled = false;
            gbBatchList.Enabled = false;
            gbDetails.Enabled = false;
            gbBatchToProcess.Enabled = false;
            pnlActionButtons.Enabled = false;
            gbSINo.Enabled = true;

            dgvCheckName.ClearSelection();

            var clearBranchlist = costDistributionList
                    .Select
                    (i => new
                    {
                        i.BranchName,
                        i.Quantity,
                        i.UOM,
                        i.SOL,
                        i.RC,
                        i.PrintingCost,
                        i.DocStampCost,
                    })
                    .ToList();

            dgvDirectBranches.DataSource = clearBranchlist;
            dgvDirectBranches.ClearSelection();
            dgvDirectBranches.DataSource = clearBranchlist;
            dgvDirectBranches.ClearSelection();

            var clearChequeNames = costDistributionList
                    .Select
                    (i => new
                    {
                        i.ProductCode,
                        i.Quantity
                    })
                    .ToList();


            tpnlBranches.Enabled = false;
            dgvDirectBranches.ClearSelection();
            dgvProvincialBranches.ClearSelection();


            

            txtSalesInvoiceNumber.Focus();
        }

        private void FindRecord()
        {


            if (!string.IsNullOrWhiteSpace(txtSalesInvoiceNumber.Text.ToString()))
            {
                DataTable dt = new DataTable();
                string salesInvoiceNumber = txtSalesInvoiceNumber.Text;

                if (!proc.CheckSalesInvoiceTransactionOnHistory(salesInvoiceNumber))
                {
                    p.MessageAndLog("No transaction found for sales invoice number.\r\n\r\n " + proc.errorMessage + "", ref log, "ERROR");
                    return;
                }

                log.Info("Pressed Enter Button with text on txtSalesInvoiceNumber: " + txtSalesInvoiceNumber.Text.ToString());
                log.Info("Fetching Existing Sales Invoice Record");
                //Start Progress Bar View
                progressBar = new frmProgress();
                progressBar.message = "Fetching Existing Record. Please Wait.";
                thread = new Thread(() => progressBar.ShowDialog());
                thread.Start();

                DisplayCostDistrubutionRecord(salesInvoiceNumber);
                EnableControls();

                thread.Abort();
            }

        }

        private void btnGenerateExcelFile_Click(object sender, EventArgs e)
        {
            log.Info("(Generate Excel File) Button Click");
            ExportCDRToEcxel(txtSalesInvoiceNumber.Text);
        }


        private void ConfigureGrids()
        {
            //GRID 1
            //dgvDRList.AutoGenerateColumns = false;
            dgvCheckName.AllowUserToAddRows = false;
            dgvCheckName.AllowUserToResizeColumns = false;
            dgvCheckName.AllowUserToDeleteRows = false;
            dgvCheckName.AllowUserToOrderColumns = false;
            dgvCheckName.AllowUserToResizeRows = false;
            dgvCheckName.AllowUserToAddRows = false;
            dgvCheckName.ScrollBars = ScrollBars.Vertical;
            dgvCheckName.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //Rename datagrid columns programmatically
            dgvCheckName.EditMode = DataGridViewEditMode.EditProgrammatically;

            //Column names and width setup
            dgvCheckName.ColumnCount = 2; //COUNT OF COLUMNS THAT WILL DISPLAY IN GRID

            dgvCheckName.Columns[0].Name = "PRODUCT CODE";
            dgvCheckName.Columns[0].Width = 150;
            dgvCheckName.Columns[0].DataPropertyName = "ProductCode";

            dgvCheckName.Columns[1].Name = "CHECK NAME";
            dgvCheckName.Columns[1].Width = 800;
            dgvCheckName.Columns[1].DataPropertyName = "ChequeName";

            //GRID 2
            //dgvDRList.AutoGenerateColumns = true;
            dgvDirectBranches.AllowUserToAddRows = false;
            dgvDirectBranches.AllowUserToResizeColumns = false;
            dgvDirectBranches.AllowUserToDeleteRows = false;
            dgvDirectBranches.AllowUserToOrderColumns = false;
            dgvDirectBranches.AllowUserToResizeRows = false;
            dgvDirectBranches.AllowUserToAddRows = false;
            dgvDirectBranches.ScrollBars = ScrollBars.Vertical;
            dgvDirectBranches.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //Rename datagrid columns programmatically
            dgvDirectBranches.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvDirectBranches.ColumnCount = 7; //COUNT OF COLUMNS THAT WILL DISPLAY IN GRID

            //Column names and width setup

            dgvDirectBranches.Columns[0].Name = "BRANCH / UNIT NAME";
            dgvDirectBranches.Columns[0].Width = 200;
            dgvDirectBranches.Columns[0].DataPropertyName = "BranchName";

            //dgvDirectBranches.Columns[1].Name = "CHECK NAME";
            //dgvDirectBranches.Columns[1].Width = 200;
            //dgvDirectBranches.Columns[1].DataPropertyName = "ChequeName"; //this must be the actual table name in sql

            dgvDirectBranches.Columns[1].Name = "QUANTITY";
            dgvDirectBranches.Columns[1].Width = 80;
            dgvDirectBranches.Columns[1].DataPropertyName = "Quantity";

            dgvDirectBranches.Columns[2].Name = "UOM";
            dgvDirectBranches.Columns[2].Width = 50;
            dgvDirectBranches.Columns[2].DataPropertyName = "UOM";

            dgvDirectBranches.Columns[3].Name = "SOL";
            dgvDirectBranches.Columns[3].Width = 50;
            dgvDirectBranches.Columns[3].DataPropertyName = "SOL";

            dgvDirectBranches.Columns[4].Name = "RC";
            dgvDirectBranches.Columns[4].Width = 50;
            dgvDirectBranches.Columns[4].DataPropertyName = "RC";

            dgvDirectBranches.Columns[5].Name = "PRINTING COST";
            dgvDirectBranches.Columns[5].DefaultCellStyle.Format = "#,0.00##";
            dgvDirectBranches.Columns[5].Width = 150;
            dgvDirectBranches.Columns[5].DataPropertyName = "PrintingCost";

            dgvDirectBranches.Columns[6].Name = "DOC STAMPS";
            dgvDirectBranches.Columns[6].DefaultCellStyle.Format = "#,0.00##";
            dgvDirectBranches.Columns[6].Width = 250;
            dgvDirectBranches.Columns[6].DataPropertyName = "DocStampCost";

            //GRID 3
            //dgvDRList.AutoGenerateColumns = true;
            dgvProvincialBranches.AllowUserToAddRows = false;
            dgvProvincialBranches.AllowUserToResizeColumns = false;
            dgvProvincialBranches.AllowUserToDeleteRows = false;
            dgvProvincialBranches.AllowUserToOrderColumns = false;
            dgvProvincialBranches.AllowUserToResizeRows = false;
            dgvProvincialBranches.AllowUserToAddRows = false;
            dgvProvincialBranches.ScrollBars = ScrollBars.Vertical;
            dgvProvincialBranches.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //Rename datagrid columns programmatically
            dgvProvincialBranches.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvProvincialBranches.ColumnCount = 7; //COUNT OF COLUMNS THAT WILL DISPLAY IN GRID

            //Column names and width setup

            dgvProvincialBranches.Columns[0].Name = "BRANCH / UNIT NAME";
            dgvProvincialBranches.Columns[0].Width = 200;
            dgvProvincialBranches.Columns[0].DataPropertyName = "BranchName";

            //dgvProvincialBranches.Columns[1].Name = "CHECK NAME";
            //dgvProvincialBranches.Columns[1].Width = 200;
            //dgvProvincialBranches.Columns[1].DataPropertyName = "ChequeName"; //this must be the actual table name in sql

            dgvProvincialBranches.Columns[1].Name = "QUANTITY";
            dgvProvincialBranches.Columns[1].Width = 80;
            dgvProvincialBranches.Columns[1].DataPropertyName = "Quantity";

            dgvProvincialBranches.Columns[2].Name = "UOM";
            dgvProvincialBranches.Columns[2].Width = 50;
            dgvProvincialBranches.Columns[2].DataPropertyName = "UOM";

            dgvProvincialBranches.Columns[3].Name = "SOL";
            dgvProvincialBranches.Columns[3].Width = 50;
            dgvProvincialBranches.Columns[3].DataPropertyName = "SOL";

            dgvProvincialBranches.Columns[4].Name = "RC";
            dgvProvincialBranches.Columns[4].Width = 50;
            dgvProvincialBranches.Columns[4].DataPropertyName = "RC";

            dgvProvincialBranches.Columns[5].Name = "PRINTING COST";
            dgvProvincialBranches.Columns[5].DefaultCellStyle.Format = "#,0.00##";
            dgvProvincialBranches.Columns[5].Width = 150;
            dgvProvincialBranches.Columns[5].DataPropertyName = "PrintingCost";

            dgvProvincialBranches.Columns[6].Name = "DOC STAMPS";
            dgvProvincialBranches.Columns[6].DefaultCellStyle.Format = "#,0.00##";
            dgvProvincialBranches.Columns[6].Width = 250;
            dgvProvincialBranches.Columns[6].DataPropertyName = "DocStampCost";
        }


        private void FillComboBoxes()
        {
            DataTable dt = new DataTable();
            if (!proc.GetUserDetails(ref dt))
            {
                MessageBox.Show("Unable to connect to server. \r\n" + proc.errorMessage);
            }

            _ = dt.Rows.Count != 0 ? cbCheckedBy.DataSource = dt : cbCheckedBy.DataSource = null;
            cbCheckedBy.BindingContext = new BindingContext();
            cbCheckedBy.DisplayMember = "FirstName";
            cbCheckedBy.SelectedIndex = -1;

            _ = dt.Rows.Count != 0 ? cbApprovedBy.DataSource = dt : cbApprovedBy.DataSource = null;
            cbApprovedBy.BindingContext = new BindingContext();
            cbApprovedBy.DisplayMember = "FirstName";
            cbApprovedBy.SelectedIndex = -1;

        }


        public void ConfigureDesignLabels()
        {
            string fullname = gUser.FirstName + " " + gUser.LastName;

            lblUserName.Text = fullname.ToUpper();
            lblBankName.Text = gClient.Description.ToUpper();

        }

        private void DisplayCostDistrubutionRecord(string salesInvoiceNumber)
        {
            //Get list first based on SalesInvoice Input.
            DataTable data = new  DataTable();
            if (!proc.GetData("select md.BranchName, md.ChequeName, md.Unit as UOM, md.UnitPrice as PrintingCost, md.docstamp2 as DocStampCost, md.multiplier as Multiplier, md.location as Location, br.SOL from " + gClient.DataBaseName + " md" +
                " inner join " + gClient.BranchesTable + " br" +
                " on md.brstn = br.brstn " +
                " where salesInvoice = " + salesInvoiceNumber + "", ref data))
            {
                p.MessageAndLog("Error upon retrieving data from database (ProcessCostDistribution (proc.GetData))\r\n\r\n" + proc.errorMessage, ref log, "error");
                return;
            }

            //Convert salesInvoice datatable to list.
            //costDistributionList = ConvertDataTable<CostDistributionModel>(data);
            costDistributionList = DataTableToListConverter.DataTableToList<CostDistributionModel>(data);

            var checkNames = costDistributionList
                .GroupBy(p => p.ChequeName)
                .Select(group => new { ChequeName = group.Key, checkNames = group.ToList() })
                .ToList();

            dgvCheckName.DataSource = checkNames;

            DisplaySelectedRow();
            
        }

        private void dgvCheckName_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void DisplaySelectedRow()
        {
            foreach (DataGridViewRow row in dgvCheckName.SelectedRows)
            {

                string[] locations = { "Direct", "Provincial" };

                foreach (string location in locations)
                {
                    var localQueryResult = costDistributionList

                    .Where(c => c.Location == location && c.ChequeName == row.Cells[1].Value.ToString())
                    .GroupBy(e => new { e.BranchName, e.ChequeName, e.UOM, e.SOL, e.PrintingCost, e.DocStampCost, e.Multiplier,  e.Location })
                    .Select(g => new
                    {
                        BranchName = g.Key.BranchName,
                        ChequeName = g.Key.ChequeName,
                        Quantity = g.Count(),
                        UOM = g.Key.UOM,
                        SOL = g.Key.SOL,
                        RC = "000",
                        //Location = g.Key.Location,
                        PrintingCost = Math.Round((Convert.ToDouble(g.Key.PrintingCost) * Convert.ToInt32(g.Key.Multiplier)) * g.Count()).ToString("#,0.00##"),
                        DocStampCost = (Convert.ToInt32(g.Key.Multiplier ?? "0") * g.Count()) * Convert.ToDouble(g.Key.DocStampCost),

                    })
                    .OrderBy(o => o.BranchName)
                    .ToList();

                    if (location == "Direct")
                    {
                        dgvDirectBranches.DataSource = localQueryResult;
                        dgvDirectBranches.ClearSelection();
                    }
                    else if (location == "Provincial")
                    {
                        dgvProvincialBranches.DataSource = localQueryResult;
                        dgvProvincialBranches.ClearSelection();
                    }

                }

                

            }

        }

        private void btnCancelClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Generic Datatable to List Converter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)

                        if (dr[column.ColumnName].ToString() == "")
                        {
                            if (pro.PropertyType.ToString() == "System.Double" || pro.PropertyType.ToString() == "System.Int")
                            {
                                
                                double val = 0;
                                pro.SetValue(obj, val, null);
                                
                            }
                            if (pro.PropertyType.ToString() == "System.String")
                            {
                                string val = "";
                                pro.SetValue(obj, val, null);
                            }

                        }
                        else
                        {
                            pro.SetValue(obj, dr[column.ColumnName], null);

                        }

                    else
                        continue;
                }
            }
            return obj;
        }


        private void EnableControls()
        {
            gbSearch.Enabled = true;
            gbBatchList.Enabled = true;
            gbDetails.Enabled = true;
            gbBatchToProcess.Enabled = true;
            pnlActionButtons.Enabled = true;
            gbSINo.Enabled = false;

            //Enable all Action Buttons
            //btnCancelSiRecord.Enabled = false;
            //btnReprint.Enabled = false;
            btnReloadDrList.Enabled = true;
            //btnPrintSalesInvoice.Enabled = true;
            //btnViewSelected.Enabled = true;
        }

        private void dgvDirectBranches_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvProvincialBranches_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvCheckName_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DisplaySelectedRow();
        }

        private void dgvCheckName_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DisplaySelectedRow();
        }

        private void txtSalesInvoiceNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSalesInvoiceNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FindRecord();
            }

           
        }

        private void btnReloadDrList_Click(object sender, EventArgs e)
        {
            RefreshView();
           
        }
    }
}
