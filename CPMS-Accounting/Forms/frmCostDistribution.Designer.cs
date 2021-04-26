
namespace CPMS_Accounting.Forms
{
    partial class frmCostDistribution
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCostDistribution));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnCancelClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblRowsAffected = new System.Windows.Forms.Label();
            this.lblBankName = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Label5 = new System.Windows.Forms.Label();
            this.gbSINo = new System.Windows.Forms.GroupBox();
            this.btnAddRecord = new System.Windows.Forms.Button();
            this.txtSalesInvoiceNumber = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlActionButtons = new System.Windows.Forms.GroupBox();
            this.btnReloadDrList = new System.Windows.Forms.Button();
            this.btnGenerateExcelFile = new System.Windows.Forms.Button();
            this.bgwLoadBatchList = new System.ComponentModel.BackgroundWorker();
            this.gbBatchList = new System.Windows.Forms.GroupBox();
            this.dgvDirectBranches = new System.Windows.Forms.DataGridView();
            this.gbDetails = new System.Windows.Forms.GroupBox();
            this.lblDRList = new System.Windows.Forms.Label();
            this.dtpInvoiceDate = new System.Windows.Forms.DateTimePicker();
            this.cbApprovedBy = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbCheckedBy = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.gbSearch = new System.Windows.Forms.GroupBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.gbBatchToProcess = new System.Windows.Forms.GroupBox();
            this.dgvProvincialBranches = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvCheckName = new System.Windows.Forms.DataGridView();
            this.tpnlBranches = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            this.gbSINo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlActionButtons.SuspendLayout();
            this.gbBatchList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDirectBranches)).BeginInit();
            this.gbDetails.SuspendLayout();
            this.gbSearch.SuspendLayout();
            this.gbBatchToProcess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProvincialBranches)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheckName)).BeginInit();
            this.tpnlBranches.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancelClose
            // 
            this.btnCancelClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancelClose.BackgroundImage")));
            this.btnCancelClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancelClose.FlatAppearance.BorderSize = 0;
            this.btnCancelClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelClose.Font = new System.Drawing.Font("Cooper Black", 12F);
            this.btnCancelClose.ForeColor = System.Drawing.Color.White;
            this.btnCancelClose.Location = new System.Drawing.Point(832, 97);
            this.btnCancelClose.Name = "btnCancelClose";
            this.btnCancelClose.Size = new System.Drawing.Size(158, 62);
            this.btnCancelClose.TabIndex = 46;
            this.btnCancelClose.Text = "CANCEL / CLOSE";
            this.btnCancelClose.UseVisualStyleBackColor = true;
            this.btnCancelClose.Click += new System.EventHandler(this.btnCancelClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(684, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 15);
            this.label1.TabIndex = 39;
            this.label1.Text = "0";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(826, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(161, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 38;
            this.progressBar1.Visible = false;
            // 
            // lblRowsAffected
            // 
            this.lblRowsAffected.AutoSize = true;
            this.lblRowsAffected.Location = new System.Drawing.Point(578, 9);
            this.lblRowsAffected.Name = "lblRowsAffected";
            this.lblRowsAffected.Size = new System.Drawing.Size(100, 13);
            this.lblRowsAffected.TabIndex = 4;
            this.lblRowsAffected.Text = "Updated Record(s):";
            // 
            // lblBankName
            // 
            this.lblBankName.AutoSize = true;
            this.lblBankName.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblBankName.Location = new System.Drawing.Point(331, 8);
            this.lblBankName.Name = "lblBankName";
            this.lblBankName.Size = new System.Drawing.Size(91, 15);
            this.lblBankName.TabIndex = 3;
            this.lblBankName.Text = "Producers Bank";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblUserName.Location = new System.Drawing.Point(81, 8);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(45, 15);
            this.lblUserName.TabIndex = 2;
            this.lblUserName.Text = "Nelson";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(290, 9);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(39, 13);
            this.Label6.TabIndex = 1;
            this.Label6.Text = "BANK:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.lblRowsAffected);
            this.panel1.Controls.Add(this.lblBankName);
            this.panel1.Controls.Add(this.lblUserName);
            this.panel1.Controls.Add(this.Label6);
            this.panel1.Controls.Add(this.Label5);
            this.panel1.Location = new System.Drawing.Point(6, 691);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(990, 32);
            this.panel1.TabIndex = 42;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(12, 9);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(74, 13);
            this.Label5.TabIndex = 0;
            this.Label5.Text = "USER NAME:";
            // 
            // gbSINo
            // 
            this.gbSINo.BackColor = System.Drawing.Color.Transparent;
            this.gbSINo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gbSINo.Controls.Add(this.btnAddRecord);
            this.gbSINo.Controls.Add(this.txtSalesInvoiceNumber);
            this.gbSINo.Location = new System.Drawing.Point(6, 92);
            this.gbSINo.Name = "gbSINo";
            this.gbSINo.Size = new System.Drawing.Size(248, 68);
            this.gbSINo.TabIndex = 44;
            this.gbSINo.TabStop = false;
            this.gbSINo.Text = "SALES INVOICE NUMBER";
            // 
            // btnAddRecord
            // 
            this.btnAddRecord.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddRecord.BackgroundImage")));
            this.btnAddRecord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddRecord.FlatAppearance.BorderSize = 0;
            this.btnAddRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddRecord.Font = new System.Drawing.Font("Cooper Black", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddRecord.ForeColor = System.Drawing.Color.White;
            this.btnAddRecord.Location = new System.Drawing.Point(165, 27);
            this.btnAddRecord.Name = "btnAddRecord";
            this.btnAddRecord.Size = new System.Drawing.Size(77, 23);
            this.btnAddRecord.TabIndex = 19;
            this.btnAddRecord.Text = "Enter";
            this.btnAddRecord.UseVisualStyleBackColor = true;
            this.btnAddRecord.Click += new System.EventHandler(this.btnAddRecord_Click);
            // 
            // txtSalesInvoiceNumber
            // 
            this.txtSalesInvoiceNumber.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSalesInvoiceNumber.Location = new System.Drawing.Point(12, 27);
            this.txtSalesInvoiceNumber.Name = "txtSalesInvoiceNumber";
            this.txtSalesInvoiceNumber.Size = new System.Drawing.Size(147, 23);
            this.txtSalesInvoiceNumber.TabIndex = 3;
            this.txtSalesInvoiceNumber.TextChanged += new System.EventHandler(this.txtSalesInvoiceNumber_TextChanged);
            this.txtSalesInvoiceNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSalesInvoiceNumber_KeyDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(0, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1008, 80);
            this.pictureBox1.TabIndex = 43;
            this.pictureBox1.TabStop = false;
            // 
            // pnlActionButtons
            // 
            this.pnlActionButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlActionButtons.BackColor = System.Drawing.Color.Transparent;
            this.pnlActionButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlActionButtons.Controls.Add(this.btnReloadDrList);
            this.pnlActionButtons.Controls.Add(this.btnGenerateExcelFile);
            this.pnlActionButtons.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pnlActionButtons.Location = new System.Drawing.Point(826, 166);
            this.pnlActionButtons.Name = "pnlActionButtons";
            this.pnlActionButtons.Size = new System.Drawing.Size(170, 519);
            this.pnlActionButtons.TabIndex = 45;
            this.pnlActionButtons.TabStop = false;
            // 
            // btnReloadDrList
            // 
            this.btnReloadDrList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReloadDrList.BackColor = System.Drawing.SystemColors.Control;
            this.btnReloadDrList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReloadDrList.BackgroundImage")));
            this.btnReloadDrList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReloadDrList.FlatAppearance.BorderSize = 0;
            this.btnReloadDrList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReloadDrList.Font = new System.Drawing.Font("Cooper Black", 12F);
            this.btnReloadDrList.ForeColor = System.Drawing.Color.White;
            this.btnReloadDrList.Location = new System.Drawing.Point(6, 13);
            this.btnReloadDrList.Name = "btnReloadDrList";
            this.btnReloadDrList.Size = new System.Drawing.Size(158, 62);
            this.btnReloadDrList.TabIndex = 22;
            this.btnReloadDrList.Text = "REFRESH";
            this.btnReloadDrList.UseVisualStyleBackColor = false;
            this.btnReloadDrList.Click += new System.EventHandler(this.btnReloadDrList_Click);
            // 
            // btnGenerateExcelFile
            // 
            this.btnGenerateExcelFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerateExcelFile.BackColor = System.Drawing.SystemColors.Control;
            this.btnGenerateExcelFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGenerateExcelFile.BackgroundImage")));
            this.btnGenerateExcelFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGenerateExcelFile.FlatAppearance.BorderSize = 0;
            this.btnGenerateExcelFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateExcelFile.Font = new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateExcelFile.ForeColor = System.Drawing.Color.White;
            this.btnGenerateExcelFile.Location = new System.Drawing.Point(6, 451);
            this.btnGenerateExcelFile.Name = "btnGenerateExcelFile";
            this.btnGenerateExcelFile.Size = new System.Drawing.Size(158, 62);
            this.btnGenerateExcelFile.TabIndex = 10;
            this.btnGenerateExcelFile.Text = "GENERATE EXCEL FILE";
            this.btnGenerateExcelFile.UseVisualStyleBackColor = false;
            this.btnGenerateExcelFile.Click += new System.EventHandler(this.btnGenerateExcelFile_Click);
            // 
            // gbBatchList
            // 
            this.gbBatchList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbBatchList.BackColor = System.Drawing.Color.Transparent;
            this.gbBatchList.Controls.Add(this.dgvDirectBranches);
            this.gbBatchList.Location = new System.Drawing.Point(4, 4);
            this.gbBatchList.Name = "gbBatchList";
            this.gbBatchList.Size = new System.Drawing.Size(399, 328);
            this.gbBatchList.TabIndex = 41;
            this.gbBatchList.TabStop = false;
            this.gbBatchList.Text = "DIRECT BRANCHES";
            // 
            // dgvDirectBranches
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dgvDirectBranches.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDirectBranches.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDirectBranches.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvDirectBranches.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDirectBranches.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDirectBranches.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDirectBranches.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDirectBranches.Location = new System.Drawing.Point(6, 19);
            this.dgvDirectBranches.Name = "dgvDirectBranches";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDirectBranches.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDirectBranches.Size = new System.Drawing.Size(387, 303);
            this.dgvDirectBranches.TabIndex = 2;
            this.dgvDirectBranches.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDirectBranches_CellContentClick);
            // 
            // gbDetails
            // 
            this.gbDetails.BackColor = System.Drawing.Color.Transparent;
            this.gbDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gbDetails.Controls.Add(this.lblDRList);
            this.gbDetails.Controls.Add(this.dtpInvoiceDate);
            this.gbDetails.Controls.Add(this.cbApprovedBy);
            this.gbDetails.Controls.Add(this.label4);
            this.gbDetails.Controls.Add(this.label3);
            this.gbDetails.Controls.Add(this.cbCheckedBy);
            this.gbDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbDetails.Location = new System.Drawing.Point(6, 166);
            this.gbDetails.Name = "gbDetails";
            this.gbDetails.Size = new System.Drawing.Size(248, 158);
            this.gbDetails.TabIndex = 39;
            this.gbDetails.TabStop = false;
            this.gbDetails.Text = "DETAILS";
            // 
            // lblDRList
            // 
            this.lblDRList.AutoSize = true;
            this.lblDRList.BackColor = System.Drawing.Color.Transparent;
            this.lblDRList.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDRList.Location = new System.Drawing.Point(6, 16);
            this.lblDRList.Name = "lblDRList";
            this.lblDRList.Size = new System.Drawing.Size(78, 15);
            this.lblDRList.TabIndex = 1;
            this.lblDRList.Text = "Invoice Date:";
            // 
            // dtpInvoiceDate
            // 
            this.dtpInvoiceDate.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpInvoiceDate.Location = new System.Drawing.Point(10, 38);
            this.dtpInvoiceDate.Name = "dtpInvoiceDate";
            this.dtpInvoiceDate.Size = new System.Drawing.Size(226, 23);
            this.dtpInvoiceDate.TabIndex = 2;
            // 
            // cbApprovedBy
            // 
            this.cbApprovedBy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbApprovedBy.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbApprovedBy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbApprovedBy.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbApprovedBy.FormattingEnabled = true;
            this.cbApprovedBy.Location = new System.Drawing.Point(90, 96);
            this.cbApprovedBy.Name = "cbApprovedBy";
            this.cbApprovedBy.Size = new System.Drawing.Size(146, 23);
            this.cbApprovedBy.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "Approved By:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "Checked By:";
            // 
            // cbCheckedBy
            // 
            this.cbCheckedBy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbCheckedBy.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbCheckedBy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCheckedBy.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCheckedBy.FormattingEnabled = true;
            this.cbCheckedBy.Location = new System.Drawing.Point(90, 67);
            this.cbCheckedBy.Name = "cbCheckedBy";
            this.cbCheckedBy.Size = new System.Drawing.Size(146, 23);
            this.cbCheckedBy.TabIndex = 14;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Cooper Black", 9F);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(165, 27);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(77, 23);
            this.btnSearch.TabIndex = 18;
            this.btnSearch.Text = "Go";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // gbSearch
            // 
            this.gbSearch.BackColor = System.Drawing.Color.Transparent;
            this.gbSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gbSearch.Controls.Add(this.btnSearch);
            this.gbSearch.Controls.Add(this.txtSearch);
            this.gbSearch.Location = new System.Drawing.Point(266, 92);
            this.gbSearch.Name = "gbSearch";
            this.gbSearch.Size = new System.Drawing.Size(248, 68);
            this.gbSearch.TabIndex = 38;
            this.gbSearch.TabStop = false;
            this.gbSearch.Text = "SEARCH BATCH";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(6, 27);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(153, 21);
            this.txtSearch.TabIndex = 6;
            // 
            // gbBatchToProcess
            // 
            this.gbBatchToProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbBatchToProcess.BackColor = System.Drawing.Color.Transparent;
            this.gbBatchToProcess.Controls.Add(this.dgvProvincialBranches);
            this.gbBatchToProcess.Location = new System.Drawing.Point(410, 4);
            this.gbBatchToProcess.Name = "gbBatchToProcess";
            this.gbBatchToProcess.Size = new System.Drawing.Size(400, 328);
            this.gbBatchToProcess.TabIndex = 47;
            this.gbBatchToProcess.TabStop = false;
            this.gbBatchToProcess.Text = "PROVINCIAL BRANCHES";
            // 
            // dgvProvincialBranches
            // 
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            this.dgvProvincialBranches.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvProvincialBranches.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProvincialBranches.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvProvincialBranches.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProvincialBranches.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvProvincialBranches.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProvincialBranches.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvProvincialBranches.Location = new System.Drawing.Point(6, 19);
            this.dgvProvincialBranches.Name = "dgvProvincialBranches";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProvincialBranches.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvProvincialBranches.Size = new System.Drawing.Size(388, 303);
            this.dgvProvincialBranches.TabIndex = 3;
            this.dgvProvincialBranches.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProvincialBranches_CellContentClick);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.dgvCheckName);
            this.groupBox1.Location = new System.Drawing.Point(266, 166);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(403, 158);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CHECK NAME";
            // 
            // dgvCheckName
            // 
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            this.dgvCheckName.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvCheckName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCheckName.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvCheckName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCheckName.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvCheckName.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCheckName.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvCheckName.Location = new System.Drawing.Point(6, 19);
            this.dgvCheckName.Name = "dgvCheckName";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCheckName.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvCheckName.Size = new System.Drawing.Size(391, 133);
            this.dgvCheckName.TabIndex = 1;
            this.dgvCheckName.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCheckName_CellClick);
            // 
            // tpnlBranches
            // 
            this.tpnlBranches.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tpnlBranches.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tpnlBranches.ColumnCount = 2;
            this.tpnlBranches.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tpnlBranches.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tpnlBranches.Controls.Add(this.gbBatchToProcess, 1, 0);
            this.tpnlBranches.Controls.Add(this.gbBatchList, 0, 0);
            this.tpnlBranches.Location = new System.Drawing.Point(6, 349);
            this.tpnlBranches.Name = "tpnlBranches";
            this.tpnlBranches.RowCount = 1;
            this.tpnlBranches.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tpnlBranches.Size = new System.Drawing.Size(814, 336);
            this.tpnlBranches.TabIndex = 48;
            // 
            // frmCostDistribution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.tpnlBranches);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancelClose);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gbSINo);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pnlActionButtons);
            this.Controls.Add(this.gbDetails);
            this.Controls.Add(this.gbSearch);
            this.Name = "frmCostDistribution";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "COST DISTRIBUTION";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmCostDistribution_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbSINo.ResumeLayout(false);
            this.gbSINo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlActionButtons.ResumeLayout(false);
            this.gbBatchList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDirectBranches)).EndInit();
            this.gbDetails.ResumeLayout(false);
            this.gbDetails.PerformLayout();
            this.gbSearch.ResumeLayout(false);
            this.gbSearch.PerformLayout();
            this.gbBatchToProcess.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProvincialBranches)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheckName)).EndInit();
            this.tpnlBranches.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancelClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblRowsAffected;
        private System.Windows.Forms.Label lblBankName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.GroupBox gbSINo;
        private System.Windows.Forms.Button btnAddRecord;
        private System.Windows.Forms.TextBox txtSalesInvoiceNumber;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox pnlActionButtons;
        private System.Windows.Forms.Button btnReloadDrList;
        private System.Windows.Forms.Button btnGenerateExcelFile;
        private System.ComponentModel.BackgroundWorker bgwLoadBatchList;
        private System.Windows.Forms.GroupBox gbBatchList;
        private System.Windows.Forms.GroupBox gbDetails;
        private System.Windows.Forms.Label lblDRList;
        private System.Windows.Forms.DateTimePicker dtpInvoiceDate;
        private System.Windows.Forms.ComboBox cbApprovedBy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbCheckedBy;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox gbSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.GroupBox gbBatchToProcess;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvCheckName;
        private System.Windows.Forms.DataGridView dgvDirectBranches;
        private System.Windows.Forms.DataGridView dgvProvincialBranches;
        private System.Windows.Forms.TableLayoutPanel tpnlBranches;
    }
}